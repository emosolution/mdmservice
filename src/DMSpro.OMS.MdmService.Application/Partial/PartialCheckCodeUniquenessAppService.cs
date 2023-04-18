using System;
using System.Reflection;
using System.Threading.Tasks;
using Volo.Abp;

namespace DMSpro.OMS.MdmService.Partial;

public partial class PartialAppService<T, TDto, TRepository>
{
    protected async Task CheckCodeUniqueness(string code, Guid? id = null)
    {
        if (string.IsNullOrEmpty(code))
        {
            throw new UserFriendlyException(message: L["Error:PartialCheckCodeUniquenessAppService:553"], code: "0");
        }
        var entityProperties = GetEntityProperties();
        if (!entityProperties.ContainsKey("Code"))
        {
            return;
        }
        Type repoType = _repository.GetType();
        MethodInfo method = repoType.GetMethod("GetIdByCodeAsync");
        if (method == null)
        {
            throw new UserFriendlyException(message: L["Error:PartialCheckCodeUniquenessAppService:550"], code: "1");
        }

        object resultTask = method.Invoke(_repository, new object[] { code });
        if (resultTask is Task<Guid?> task)
        {
            Guid? result = await task;
            if (id == null)
            {
                if (result != null && result != default)
                {
                    throw new UserFriendlyException(message: L["Error:PartialCheckCodeUniquenessAppService:552"], code: "0");
                }
                return;
            }
            if (result != null && result != default && result != id)
            {
                throw new UserFriendlyException(message: L["Error:PartialCheckCodeUniquenessAppService:552"], code: "0");
            }
            return;
        }
        throw new UserFriendlyException(message: L["Error:PartialCheckCodeUniquenessAppService:551"], code: "1");
    }

    protected void CheckEffectiveDate(DateTime? effectiveDate, DateTime? endDate)
    {
        if(endDate is null)
        {
            return;
        }
        if(effectiveDate >= endDate)
        {
            throw new UserFriendlyException(message: L["Error:MdmService:Company:EffectiveDateCompareEndDate"], code: "1");
        }
        
    }
} 