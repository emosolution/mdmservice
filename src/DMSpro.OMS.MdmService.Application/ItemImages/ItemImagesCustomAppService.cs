﻿using DMSpro.OMS.MdmService.Permissions;
using DMSpro.OMS.Shared.Protos.FileManagementService.Files;
using Google.Protobuf;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Content;

namespace DMSpro.OMS.MdmService.ItemImages
{
    public partial class ItemImagesAppService
    {
        [Authorize(MdmServicePermissions.Items.Create)]
        public virtual async Task<ItemImageDto> CreateAsync(Guid itemId,
            IRemoteStreamContent inputFile,
            string description, bool active, int displayOrder)
        { 
            string contentType = inputFile.ContentType;
            if (!_fileManagementInfoAppService.AcceptedImageContentTypes.Contains(contentType))
            {
                throw new UserFriendlyException(message: L["Error:FileManagement:551", contentType], code: "0");
            }
            var stream = new MemoryStream();
            await inputFile.GetStream().CopyToAsync(stream);
            var content = ByteString.CopyFrom(stream.ToArray());

            using GrpcChannel channel = GrpcChannel.ForAddress(_settingProvider["GrpcRemotes:FileManagementServiceUrl"]);
            var client = new FilesProtoAppService.FilesProtoAppServiceClient(channel);

            UploadFileRequest request = new()
            {
                TenantId = _currentTenant.Id == null ? "" : _currentTenant.Id.ToString(),
                DirectoryId = _fileManagementInfoAppService.ItemImageDirectoryId.ToString(),
                Name = inputFile.FileName,
                ContentType = contentType,
                Content = content,
            };
            var response = await client.UploadFileAsync(request);
            OMS.Shared.Protos.FileManagementService.Files.File file = response.File;

            var itemImage = await _itemImageManager.CreateAsync(itemId,
                description, active, displayOrder, Guid.Parse(file.Id));

            return ObjectMapper.Map<ItemImage, ItemImageDto>(itemImage);
        }

        [Authorize(MdmServicePermissions.Items.Edit)]
        public virtual async Task<ItemImageDto> UpdateAsync(Guid id, Guid itemId,
            IRemoteStreamContent inputFile,
            string description, bool active, int displayOrder)
        {
            string contentType = inputFile.ContentType;
            if (!_fileManagementInfoAppService.AcceptedImageContentTypes.Contains(contentType))
            {
                throw new UserFriendlyException(message: L["Error:FileManagement:551", contentType], code: "0");
            }

            var record = await _itemImageRepository.GetAsync(id);
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
            await inputFile.GetStream().CopyToAsync(stream);
            var content = ByteString.CopyFrom(stream.ToArray());
            UploadFileRequest uploadRequest = new()
            {
                TenantId = tenantId,
                DirectoryId = _fileManagementInfoAppService.ItemImageDirectoryId.ToString(),
                Name = inputFile.FileName,
                ContentType = contentType,
                Content = content,
            };
            var uploadResponse = await client.UploadFileAsync(uploadRequest);
            OMS.Shared.Protos.FileManagementService.Files.File file = uploadResponse.File;

            var itemImage = await _itemImageManager.UpdateAsync(
                id, itemId, description, active, displayOrder, 
                Guid.Parse(file.Id));

            return ObjectMapper.Map<ItemImage, ItemImageDto>(itemImage);
        }

        [Authorize(MdmServicePermissions.Items.Delete)]
        public virtual async Task DeleteManyAsync(List<Guid> ids)
        {
            var records = (await _itemImageRepository.GetQueryableAsync()).Where(x => ids.Contains(x.Id));
            var fileIds = records.Select(x => x.FileId.ToString()).ToList();
            DeleteFilesRequest request = new()
            {
                TenantId = _currentTenant.Id == null ? "" : _currentTenant.Id.ToString(),
            };
            request.FileIds.Add(fileIds);
            using GrpcChannel channel = GrpcChannel.ForAddress(_settingProvider["GrpcRemotes:FileManagementServiceUrl"]);
            var client = new FilesProtoAppService.FilesProtoAppServiceClient(channel);
            await client.DeleteFilesAsync(request);

            await _itemImageRepository.DeleteManyAsync(records.ToList());
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
            MemoryStream memoryStream = new MemoryStream(response.Content.ToByteArray());
            IRemoteStreamContent remoteStreamContent = new RemoteStreamContent(stream: memoryStream,
                fileName: response.File.FileName, contentType: response.File.ContentType);
            return remoteStreamContent;
        }
    }
}
