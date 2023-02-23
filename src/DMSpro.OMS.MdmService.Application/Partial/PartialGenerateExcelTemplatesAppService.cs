using JetBrains.Annotations;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Content;

namespace DMSpro.OMS.MdmService.Partial
{
    public partial class PartialAppService<T, TDto, TRepository>
    {
        private const string EXCEL_TEMPLATE_INFO_METHOD = "GetExcelTemplateInfo";
        private const string NOT_NULL_STRING_METHOD = "GetNotNullProperty";

        private static readonly List<string> _ignoredProperty = new()
        {
            "Id",
            "TenantId",
            "IsDeleted",
            "DeleterId",
            "ExtraProperties",
            "DeletionTime",
            "LastModificationTime",
            "LastModifierId",
            "CreationTime",
            "CreatorId",
            "ConcurrencyStamp",
        };

        private static readonly List<string> _structureSheetHeader = new()
        {
            "Column Name",
            "Data Type",
            "Allow Nulls",
            "GetIdByCodeFromDBAndSheet",
            "GetIdByCodeFromDBOnly",
            "GetIdFromGrpc",
            "GrpcConnectionString",
            "GrpcNamespace",
        };

        public virtual async Task<IRemoteStreamContent> GenerateExcelTemplatesAsync()
        {
            Type type = typeof(T);
            string entityName = type.Name;
            if (type == null)
            {
                var detailDict = new Dictionary<string, string> { ["entityName"] = entityName };
                string detailString = JsonSerializer.Serialize(detailDict).ToString();
                throw new BusinessException(message: L["Error:ImportHandler:584"], code: "0", details: detailString);
            }
            var package = CreateTemplate(type);
            var memoryStream = new MemoryStream();
            await package.SaveAsAsync(memoryStream);
            memoryStream.Seek(0, SeekOrigin.Begin);
            return new RemoteStreamContent(memoryStream, $"{entityName} Template.xlsx",
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public ExcelPackage CreateTemplate(Type type)
        {
            List<string> propertyNames = new();
            Dictionary<string, string> propertyDataType = new();
            Dictionary<string, bool> isPropertyNullable = new();

            List<PropertyInfo> properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public).ToList();
            object instance = Activator.CreateInstance(type);
            List<string> notNullString = GetNotNullString(instance, type);
            Dictionary<string, (int, string, string, string)> entityCheckInfo = GetEntityCheckInfo(instance, type);
            foreach (PropertyInfo property in properties)
            {
                string propertyName = property.Name;
                if (_ignoredProperty.Contains(propertyName))
                {
                    continue;
                }
                propertyNames.Add(propertyName);
                string propertyTypeName = property.PropertyType.FullName;
                bool isNullable = propertyTypeName.Contains("Nullable");
                if (!isNullable)
                {
                    if (propertyTypeName.CompareTo("System.String") == 0 && !notNullString.Contains(propertyName))
                    {
                        isNullable = true;
                    }
                }
                else
                {
                    Type underlyingType = Nullable.GetUnderlyingType(property.PropertyType);
                    if (underlyingType != null)
                    {
                        propertyTypeName = underlyingType.FullName;
                    }
                }
                isPropertyNullable.Add(propertyName, isNullable);
                switch (propertyTypeName)
                {
                    case "System.Guid":
                    case "System.String":
                        propertyDataType.Add(propertyName, "string");
                        break;
                    case "System.Int32":
                        propertyDataType.Add(propertyName, "int");
                        break;
                    case "System.Decimal":
                        propertyDataType.Add(propertyName, "decimal");
                        break;
                    case "System.DateTime":
                        propertyDataType.Add(propertyName, "DateTime");
                        break;
                    case "System.Boolean":
                        propertyDataType.Add(propertyName, "bit");
                        break;

                    default:
                        if (isNullable)
                        {
                            if (Nullable.GetUnderlyingType(property.PropertyType).IsEnum)
                            {
                                propertyDataType.Add(propertyName, "int");
                                break;
                            }
                        }
                        else
                        {
                            if (property.PropertyType.IsEnum)
                            {
                                propertyDataType.Add(propertyName, "int");
                                break;
                            }
                        }
                        var detailDict = new Dictionary<string, string> { ["propertyType"] = propertyTypeName };
                        string detailString = JsonSerializer.Serialize(detailDict).ToString();
                        throw new BusinessException(message: L["Error:ImportHandler:585"], code: "1", details: detailString);
                }
            }
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var package = new ExcelPackage();
            CreateStructureSheet(package, propertyNames, propertyDataType, isPropertyNullable, entityCheckInfo);
            CreateDataSheet(package, propertyNames);
            return package;
        }

        private static void CreateStructureSheet(ExcelPackage package, List<string> propertyNames,
            Dictionary<string, string> propertyDataType, Dictionary<string, bool> isPropertyNullable,
            Dictionary<string, (int CheckType, string RepoName, string GRPCConnecitonString, string GRPCNamespace)> entityCheckInfo)
        {
            var worksheet = package.Workbook.Worksheets.Add("Structure");
            for (int i = 0; i < _structureSheetHeader.Count; i++)
            {
                worksheet.Cells[1, i + 1].Value = _structureSheetHeader[i];
            }
            for (int i = 0; i < propertyNames.Count; i++)
            {
                int row = i + 2;
                string propertyName = propertyNames[i];
                worksheet.Cells[row, 1].Value = propertyName;
                worksheet.Cells[row, 2].Value = propertyDataType[propertyName];
                worksheet.Cells[row, 3].Value = isPropertyNullable[propertyName].ToString().ToUpper();
                if (entityCheckInfo.Count <= 0)
                {
                    continue;
                }
                if (!entityCheckInfo.ContainsKey(propertyName))
                {
                    continue;
                }
                int checkType = entityCheckInfo[propertyName].CheckType;
                if (checkType == ((int)CheckTypes.DB_AND_SHEET))
                {
                    worksheet.Cells[row, 4].Value = entityCheckInfo[propertyName].RepoName;
                }
                else if (checkType == (int)CheckTypes.DB_ONLY)
                {
                    worksheet.Cells[row, 5].Value = entityCheckInfo[propertyName].RepoName;
                }
                else if (checkType == (int)CheckTypes.GRPC)
                {
                    worksheet.Cells[row, 6].Value = entityCheckInfo[propertyName].RepoName;
                    worksheet.Cells[row, 7].Value = entityCheckInfo[propertyName].GRPCConnecitonString;
                    worksheet.Cells[row, 8].Value = entityCheckInfo[propertyName].GRPCNamespace;
                }
            }
            //worksheet.Protection.IsProtected = true;
            //worksheet.Hidden = eWorkSheetHidden.VeryHidden;
        }

        private static void CreateDataSheet(ExcelPackage package, List<string> propertyNames)
        {
            var worksheet = package.Workbook.Worksheets.Add("Data");
            for (var i = 0; i < propertyNames.Count; i++)
            {
                worksheet.Cells[1, i + 1].Value = propertyNames[i];
            }
            // Make this worksheet active to ensure sheet Structure is hidden
            worksheet.Select();
        }

        private Dictionary<string, (int, string, string, string)> GetEntityCheckInfo(object instance, Type type)
        {
            MethodInfo method = type.GetMethod(EXCEL_TEMPLATE_INFO_METHOD);
            if (method == null)
            {
                throw new BusinessException(message: L["Error:ImportHandler:586"], code: "1");
            }
            object result = method.Invoke(instance, null);
            if (result == null)
            {
                return new();
            }
            if (result.GetType() != typeof(Dictionary<string, (int, string, string, string)>))
            {
                return new();
            }
            return (Dictionary<string, (int, string, string, string)>)result;
        }

        private List<string> GetNotNullString(object instance, Type type)
        {
            MethodInfo method = type.GetMethod(NOT_NULL_STRING_METHOD);
            if (method == null)
            {
                throw new BusinessException(message: L["Error:ImportHandler:587"], code: "1");
            }
            object result = method.Invoke(instance, null);
            if (result == null)
            {
                return new();
            }
            if (result.GetType() != typeof(List<string>))
            {
                return new();
            }
            return (List<string>)result;
        }
    }
}
