using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System;
using Volo.Abp;
using DMSpro.OMS.Shared.Protos.FileManagementService.Files;
using Grpc.Net.Client;
using Google.Protobuf;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text.Json;
using Volo.Abp.Content;

namespace DMSpro.OMS.MdmService.EmployeeImages
{
    public partial class EmployeeImagesAppService
    {
        [Authorize(MdmServicePermissions.EmployeeProfiles.Create)]
        public virtual async Task<EmployeeImageDto> CreateAsync(EmployeeImageCreateDto input)
        {
            if (input.EmployeeProfileId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Item"]]);
            }
            if (input.File == null)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["File"]]);
            }
            string contentType = input.File.ContentType;
            if (!_fileManagementInfoAppService.AcceptedAttachmentContentTypes.Contains(contentType))
            {
                var detailDict = new Dictionary<string, string> { ["contentType"] = contentType };
                string detailString = JsonSerializer.Serialize(detailDict).ToString();
                throw new UserFriendlyException(message: L["Error:FileManagement:551"], code: "0", details: detailString);
            }
            var stream = new MemoryStream();
            await input.File.GetStream().CopyToAsync(stream);
            var content = ByteString.CopyFrom(stream.ToArray());

            using GrpcChannel channel = GrpcChannel.ForAddress(_settingProvider["GrpcRemotes:FileManagementServiceUrl"]);
            UploadFileRequest request = new()
            {
                TenantId = _currentTenant.Id == null ? "" : _currentTenant.Id.ToString(),
                DirectoryId = _fileManagementInfoAppService.EmployeeImageDirectoryId.ToString(),
                Name = input.File.FileName,
                ContentType = contentType,
                Content = content,
            };
            var client = new FilesProtoAppService.FilesProtoAppServiceClient(channel);
            var response = await client.UploadFileAsync(request);
            OMS.Shared.Protos.FileManagementService.Files.File file = response.File;

            var EmployeeImage = await _employeeImageManager.CreateAsync(input.EmployeeProfileId,
                input.Description, input.Active, input.IsAvatar, Guid.Parse(file.Id));

            return ObjectMapper.Map<EmployeeImage, EmployeeImageDto>(EmployeeImage);
        }

        [Authorize(MdmServicePermissions.EmployeeProfiles.Edit)]
        public virtual async Task<EmployeeImageDto> UpdateAsync(Guid id, EmployeeImageUpdateDto input)
        {
            if (input.EmployeeProfileId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Item"]]);
            }
            if (input.File == null)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["File"]]);
            }
            string contentType = input.File.ContentType;
            if (!_fileManagementInfoAppService.AcceptedAttachmentContentTypes.Contains(contentType))
            {
                var detailDict = new Dictionary<string, string> { ["contentType"] = contentType };
                string detailString = JsonSerializer.Serialize(detailDict).ToString();
                throw new UserFriendlyException(message: L["Error:FileManagement:551"], code: "0", details: detailString);
            }

            var record = await _employeeImageRepository.GetAsync(id);
            string tenantId = _currentTenant.Id == null ? "" : _currentTenant.Id.ToString();
            using GrpcChannel channel = GrpcChannel.ForAddress(_settingProvider["GrpcRemotes:FileManagementServiceUrl"]);
            DeleteFilesRequest deleteRequest = new()
            {
                TenantId = tenantId,
            };
            deleteRequest.FileIds.Add(record.FileId.ToString());
            var client = new FilesProtoAppService.FilesProtoAppServiceClient(channel);
            await client.DeleteFilesAsync(deleteRequest);

            var stream = new MemoryStream();
            await input.File.GetStream().CopyToAsync(stream);
            var content = ByteString.CopyFrom(stream.ToArray());
            UploadFileRequest uploadRequest = new()
            {
                TenantId = tenantId,
                DirectoryId = _fileManagementInfoAppService.EmployeeImageDirectoryId.ToString(),
                Name = input.File.FileName,
                ContentType = contentType,
                Content = content,
            };
            var uploadResponse = await client.UploadFileAsync(uploadRequest);
            OMS.Shared.Protos.FileManagementService.Files.File file = uploadResponse.File;

            var EmployeeImage = await _employeeImageManager.UpdateAsync(
                id, input.EmployeeProfileId, input.Description, input.Active, 
                input.IsAvatar, Guid.Parse(file.Id), input.ConcurrencyStamp);

            return ObjectMapper.Map<EmployeeImage, EmployeeImageDto>(EmployeeImage);
        }

        [Authorize(MdmServicePermissions.EmployeeProfiles.Delete)]
        public virtual async Task DeleteManyAsync(List<Guid> ids)
        {
            var records = (await _employeeImageRepository.GetQueryableAsync()).Where(x => ids.Contains(x.Id));
            var fileIds = records.Select(x => x.FileId.ToString()).ToList();
            DeleteFilesRequest request = new()
            {
                TenantId = _currentTenant.Id == null ? "" : _currentTenant.Id.ToString(),
            };
            request.FileIds.Add(fileIds);
            using GrpcChannel channel = GrpcChannel.ForAddress(_settingProvider["GrpcRemotes:FileManagementServiceUrl"]);
            var client = new FilesProtoAppService.FilesProtoAppServiceClient(channel);
            await client.DeleteFilesAsync(request);

            await _employeeImageRepository.DeleteManyAsync(records.ToList());
        }

        [Authorize(MdmServicePermissions.EmployeeProfiles.Default)]
        public virtual async Task<IRemoteStreamContent> GetFileAsync(Guid id)
        {
            using GrpcChannel channel = GrpcChannel.ForAddress(_settingProvider["GrpcRemotes:FileManagementServiceUrl"]);
            GetFileRequest request = new()
            {
                TenantId = _currentTenant.Id == null ? "" : _currentTenant.Id.ToString(),
                Id = id.ToString(),
            };
            var client = new FilesProtoAppService.FilesProtoAppServiceClient(channel);
            var response = await client.GetFileAsync(request);
            MemoryStream memoryStream = new MemoryStream(response.Content.ToByteArray());
            IRemoteStreamContent remoteStreamContent = new RemoteStreamContent(stream: memoryStream,
                fileName: response.File.FileName, contentType: response.File.ContentType);
            return remoteStreamContent;
        }
    }
}
