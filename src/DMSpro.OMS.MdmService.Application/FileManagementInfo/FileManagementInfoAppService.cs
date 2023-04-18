using System;
using System.Collections.Generic;
using Volo.Abp.MultiTenancy;
using DMSpro.OMS.Shared.Protos.FileManagementService.Directories;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using Volo.Abp;
using Volo.Abp.Application.Services;
using static DMSpro.OMS.Shared.Protos.FileManagementService.Directories.DirectoriesProtoAppService;
using DMSpro.OMS.MdmService.Localization;

namespace DMSpro.OMS.MdmService.FileManagementInfo
{
    public class FileManagementInfoAppService : ApplicationService, IFileManagementInfoAppService
    {
        private readonly ICurrentTenant _currentTenant;
        private readonly IConfiguration _settingProvider;

        public Guid ItemAttachmentDirectoryId { get; }
        public Guid ItemImageDirectoryId { get; }
        public Guid CustomerAttachmentDirectoryId { get; }
        public Guid EmployeeAttachmentDirectoryId { get; }
        public Guid EmployeeImageDirectoryId { get; }

        public List<string> AcceptedAttachmentContentTypes { get; } = new()
        {
            "application/msword",

            "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
            "application/vnd.openxmlformats-officedocument.wordprocessingml.template",
            "application/vnd.ms-word.document.macroEnabled.12",
            "application/vnd.ms-word.template.macroEnabled.12",

            "application/vnd.ms-excel",

            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            "application/vnd.openxmlformats-officedocument.spreadsheetml.template",
            "application/vnd.ms-excel.sheet.macroEnabled.12",
            "application/vnd.ms-excel.template.macroEnabled.12",
            "application/vnd.ms-excel.addin.macroEnabled.12",
            "application/vnd.ms-excel.sheet.binary.macroEnabled.12",

            "application/vnd.ms-powerpoint",
    
            "application/vnd.openxmlformats-officedocument.presentationml.presentation",
            "application/vnd.openxmlformats-officedocument.presentationml.template",
            "application/vnd.openxmlformats-officedocument.presentationml.slideshow",
            "application/vnd.ms-powerpoint.addin.macroEnabled.12",
            "application/vnd.ms-powerpoint.presentation.macroEnabled.12",
            "application/vnd.ms-powerpoint.template.macroEnabled.12",
            "application/vnd.ms-powerpoint.slideshow.macroEnabled.12",

            //"application/vnd.ms-access",

            "text/plain",
            "application/pdf",
        };

        public List<string> AcceptedImageContentTypes { get; } = new()
        {
            "image/bmp",
            "image/jpeg",
            "image/jpg",
            "image/x-png",
            "image/png" ,
            //"image/gif",
        };

        public FileManagementInfoAppService(ICurrentTenant currentTenant,
            IConfiguration settingProvider)
        {
            _currentTenant = currentTenant;
            _settingProvider = settingProvider;

            using GrpcChannel channel = GrpcChannel.ForAddress(_settingProvider["GrpcRemotes:FileManagementServiceUrl"]);
            string tenantIdString = _currentTenant.Id == null ? "" : _currentTenant.Id.ToString();
            var directoryClient = new DirectoriesProtoAppServiceClient(channel);
            ItemAttachmentDirectoryId = GetDirectoryId(directoryClient, null, tenantIdString, "ItemAttachments");
            ItemImageDirectoryId = GetDirectoryId(directoryClient, null, tenantIdString, "ItemImages");
            CustomerAttachmentDirectoryId = GetDirectoryId(directoryClient, null, tenantIdString, "CustomerAttachments");
            EmployeeAttachmentDirectoryId = GetDirectoryId(directoryClient, null, tenantIdString, "EmployeeAttachments");
            EmployeeImageDirectoryId = GetDirectoryId(directoryClient, null, tenantIdString, "EmployeeImages");

            LocalizationResource = typeof(MdmServiceResource);
        }

        private Guid GetDirectoryId(DirectoriesProtoAppServiceClient client, Guid? parentDirectoryId,
            string tenantIdString, string directoryName)
        {
            CreateDirectoryRequest request = new()
            {
                TenantId = tenantIdString,
                Name = directoryName,
                ParentId = parentDirectoryId == null ? "" : parentDirectoryId.ToString(),
            };
            var response = client.CreateDirectoryIfNotExists(request);
            if (response.Directory == null)
            {
                throw new UserFriendlyException(message: L["Error:FileManagement:550", directoryName], code: "1");
            }
            Directory directory = response.Directory;
            return Guid.Parse(directory.Id);
        }
    }
}
