﻿using DMSpro.OMS.MdmService.Permissions;
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
using Volo.Abp.Content;

namespace DMSpro.OMS.MdmService.ItemAttachments
{
    public partial class ItemAttachmentsAppService
    {
        [Authorize(MdmServicePermissions.Items.Create)]
        public virtual async Task<ItemAttachmentDto> CreateAsync(Guid itemId,
            IRemoteStreamContent inputFile,
            string description, bool active)
        {
            string contentType = inputFile.ContentType;
            if (!_fileManagementInfoAppService.AcceptedAttachmentContentTypes.Contains(contentType))
            {
                throw new UserFriendlyException(message: L["Error:FileManagement:551", contentType], code: "0");
            }
            var stream = new MemoryStream();
            await inputFile.GetStream().CopyToAsync(stream);
            var content = ByteString.CopyFrom(stream.ToArray());

            using GrpcChannel channel = GrpcChannel.ForAddress(_settingProvider["GrpcRemotes:FileManagementServiceUrl"]);
            UploadFileRequest request = new()
            {
                TenantId = _currentTenant.Id == null ? "" : _currentTenant.Id.ToString(),
                DirectoryId = _fileManagementInfoAppService.ItemAttachmentDirectoryId.ToString(),
                Name = inputFile.FileName,
                ContentType = contentType,
                Content = content,
            };
            var client = new FilesProtoAppService.FilesProtoAppServiceClient(channel);
            var response = await client.UploadFileAsync(request);
            OMS.Shared.Protos.FileManagementService.Files.File file = response.File;

            var itemAttachment = await _itemAttachmentManager.CreateAsync(itemId,
                description, active, Guid.Parse(file.Id));

            return ObjectMapper.Map<ItemAttachment, ItemAttachmentDto>(itemAttachment);
        }

        [Authorize(MdmServicePermissions.Items.Edit)]
        public virtual async Task<ItemAttachmentDto> UpdateAsync(Guid id, Guid itemId,
            IRemoteStreamContent inputFile,
            string description, bool active)
        {
            string contentType = inputFile.ContentType;
            if (!_fileManagementInfoAppService.AcceptedAttachmentContentTypes.Contains(contentType))
            {
                throw new UserFriendlyException(message: L["Error:FileManagement:551", contentType], code: "0");
            }

            var record = await _itemAttachmentRepository.GetAsync(id);
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
                DirectoryId = _fileManagementInfoAppService.ItemAttachmentDirectoryId.ToString(),
                Name = inputFile.FileName,
                ContentType = contentType,
                Content = content,
            };
            var uploadResponse = await client.UploadFileAsync(uploadRequest);
            OMS.Shared.Protos.FileManagementService.Files.File file = uploadResponse.File;

            var itemAttachment = await _itemAttachmentManager.UpdateAsync(
                id, itemId, description, active, Guid.Parse(file.Id));

            return ObjectMapper.Map<ItemAttachment, ItemAttachmentDto>(itemAttachment);
        }

        [Authorize(MdmServicePermissions.Items.Delete)]
        public virtual async Task DeleteManyAsync(List<Guid> ids)
        {
            var records = (await _itemAttachmentRepository.GetQueryableAsync()).Where(x => ids.Contains(x.Id));
            var fileIds = records.Select(x => x.FileId.ToString()).ToList();
            DeleteFilesRequest request = new()
            {
                TenantId = _currentTenant.Id == null ? "" : _currentTenant.Id.ToString(),
            };
            request.FileIds.Add(fileIds);
            using GrpcChannel channel = GrpcChannel.ForAddress(_settingProvider["GrpcRemotes:FileManagementServiceUrl"]);
            var client = new FilesProtoAppService.FilesProtoAppServiceClient(channel);
            await client.DeleteFilesAsync(request);

            await _itemAttachmentRepository.DeleteManyAsync(records.ToList());
        }

        [Authorize(MdmServicePermissions.Items.Default)]
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
