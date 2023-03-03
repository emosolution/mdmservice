using DMSpro.OMS.MdmService.Permissions;
using DMSpro.OMS.Shared.Protos.FileManagementService.Files;
using Google.Protobuf;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Content;

namespace DMSpro.OMS.MdmService.CustomerAttachments
{
    public partial class CustomerAttachmentsAppService
    {
        [Authorize(MdmServicePermissions.Customers.Create)]
        public virtual async Task<CustomerAttachmentDto> CreateAsync(Guid customerId,
            IRemoteStreamContent inputFile,
            string description, bool active)
        {
            string contentType = inputFile.ContentType;
            if (!_fileManagementInfoAppService.AcceptedAttachmentContentTypes.Contains(contentType))
            {
                var detailDict = new Dictionary<string, string> { ["contentType"] = contentType };
                string detailString = JsonSerializer.Serialize(detailDict).ToString();
                throw new UserFriendlyException(message: L["Error:FileManagement:551"], code: "0", details: detailString);
            }
            var stream = new MemoryStream();
            await inputFile.GetStream().CopyToAsync(stream);
            var content = ByteString.CopyFrom(stream.ToArray());

            using GrpcChannel channel = GrpcChannel.ForAddress(_settingProvider["GrpcRemotes:FileManagementServiceUrl"]);
            UploadFileRequest request = new()
            {
                TenantId = _currentTenant.Id == null ? "" : _currentTenant.Id.ToString(),
                DirectoryId = _fileManagementInfoAppService.CustomerAttachmentDirectoryId.ToString(),
                Name = inputFile.FileName,
                ContentType = contentType,
                Content = content,
            };
            var client = new FilesProtoAppService.FilesProtoAppServiceClient(channel);
            var response = await client.UploadFileAsync(request);
            OMS.Shared.Protos.FileManagementService.Files.File file = response.File;

            var CustomerAttachment = await _customerAttachmentManager.CreateAsync(customerId,
                description, active, Guid.Parse(file.Id));

            return ObjectMapper.Map<CustomerAttachment, CustomerAttachmentDto>(CustomerAttachment);
        }

        [Authorize(MdmServicePermissions.Customers.Edit)]
        public virtual async Task<CustomerAttachmentDto> UpdateAsync(Guid id, Guid customerId,
            IRemoteStreamContent inputFile,
            string description, bool active)
        {
            string contentType = inputFile.ContentType;
            if (!_fileManagementInfoAppService.AcceptedAttachmentContentTypes.Contains(contentType))
            {
                var detailDict = new Dictionary<string, string> { ["contentType"] = contentType };
                string detailString = JsonSerializer.Serialize(detailDict).ToString();
                throw new UserFriendlyException(message: L["Error:FileManagement:551"], code: "0", details: detailString);
            }

            var record = await _customerAttachmentRepository.GetAsync(id);
            string tenantId = _currentTenant.Id == null ? "" : _currentTenant.Id.ToString();
            using GrpcChannel channel = GrpcChannel.ForAddress(_settingProvider["GrpcRemotes:FileManagementServiceUrl"]);
            var client = new FilesProtoAppService.FilesProtoAppServiceClient(channel);

            DeleteFilesRequest deleteRequest = new()
            {
                TenantId = tenantId,
            };
            deleteRequest.FileIds.Add(record.FileId.ToString());
            await client.DeleteFilesAsync(deleteRequest);

            var stream = new MemoryStream();
            await inputFile.GetStream().CopyToAsync(stream);
            var content = ByteString.CopyFrom(stream.ToArray());
            UploadFileRequest uploadRequest = new()
            {
                TenantId = tenantId,
                DirectoryId = _fileManagementInfoAppService.CustomerAttachmentDirectoryId.ToString(),
                Name = inputFile.FileName,
                ContentType = contentType,
                Content = content,
            };
            var uploadResponse = await client.UploadFileAsync(uploadRequest);
            OMS.Shared.Protos.FileManagementService.Files.File file = uploadResponse.File;

            var CustomerAttachment = await _customerAttachmentManager.UpdateAsync(
                id, customerId, description, active, Guid.Parse(file.Id));

            return ObjectMapper.Map<CustomerAttachment, CustomerAttachmentDto>(CustomerAttachment);
        }

        [Authorize(MdmServicePermissions.Customers.Delete)]
        public virtual async Task DeleteManyAsync(List<Guid> ids)
        {
            var records = (await _customerAttachmentRepository.GetQueryableAsync()).Where(x => ids.Contains(x.Id));
            var fileIds = records.Select(x => x.FileId.ToString()).ToList();
            DeleteFilesRequest request = new()
            {
                TenantId = _currentTenant.Id == null ? "" : _currentTenant.Id.ToString(),
            };
            request.FileIds.Add(fileIds);
            using GrpcChannel channel = GrpcChannel.ForAddress(_settingProvider["GrpcRemotes:FileManagementServiceUrl"]);
            var client = new FilesProtoAppService.FilesProtoAppServiceClient(channel);
            await client.DeleteFilesAsync(request);

            await _customerAttachmentRepository.DeleteManyAsync(records.ToList());
        }

        [Authorize(MdmServicePermissions.Customers.Default)]
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
            MemoryStream memoryStream = new(response.Content.ToByteArray());
            IRemoteStreamContent remoteStreamContent = new RemoteStreamContent(stream: memoryStream,
                fileName: response.File.FileName, contentType: response.File.ContentType);
            return remoteStreamContent;
        }
    }
}
