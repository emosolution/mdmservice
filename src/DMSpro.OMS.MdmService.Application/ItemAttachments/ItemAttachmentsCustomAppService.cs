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
using System.Text.Json;

namespace DMSpro.OMS.MdmService.ItemAttachments
{
    public partial class ItemAttachmentsAppService
    {
        [Authorize(MdmServicePermissions.Items.Create)]
        public virtual async Task<ItemAttachmentDto> CreateAsync(ItemAttachmentCreateDto input)
        {
            if (input.ItemId == default)
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
                DirectoryId = _fileManagementInfoAppService.ItemAttachmentDirectoryId.ToString(),
                Name = input.File.FileName,
                ContentType = contentType,
                Content = content,
            };
            var client = new FilesProtoAppService.FilesProtoAppServiceClient(channel);
            var response = client.UploadFile(request);
            OMS.Shared.Protos.FileManagementService.Files.File file = response.File;

            string url = "N/A";
            var itemAttachment = await _itemAttachmentManager.CreateAsync(input.ItemId, 
                input.Description, url, input.Active, Guid.Parse(file.Id));

            return ObjectMapper.Map<ItemAttachment, ItemAttachmentDto>(itemAttachment);
        }

        [Authorize(MdmServicePermissions.Items.Edit)]
        public virtual async Task<ItemAttachmentDto> UpdateAsync(Guid id, ItemAttachmentUpdateDto input)
        {
            if (input.ItemId == default)
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

            var record = await _itemAttachmentRepository.GetAsync(id);
            string tenantId = _currentTenant.Id == null ? "" : _currentTenant.Id.ToString();
            using GrpcChannel channel = GrpcChannel.ForAddress(_settingProvider["GrpcRemotes:FileManagementServiceUrl"]);
            DeleteFilesRequest deleteRequest = new()
            {
                TenantId = tenantId,
            };
            deleteRequest.FileIds.Add(record.FileId.ToString());
            var client = new FilesProtoAppService.FilesProtoAppServiceClient(channel);
            client.DeleteFiles(deleteRequest);

            var stream = new MemoryStream();
            await input.File.GetStream().CopyToAsync(stream);
            var content = ByteString.CopyFrom(stream.ToArray());
            UploadFileRequest uploadRequest = new()
            {
                TenantId = tenantId,
                DirectoryId = _fileManagementInfoAppService.ItemAttachmentDirectoryId.ToString(),
                Name = input.File.FileName,
                ContentType = contentType,
                Content = content,
            };
            var uploadResponse = client.UploadFile(uploadRequest);
            OMS.Shared.Protos.FileManagementService.Files.File file = uploadResponse.File;

            string url = "N/A";
            var itemAttachment = await _itemAttachmentManager.UpdateAsync(
                id, input.ItemId, input.Description, url, input.Active, Guid.Parse(file.Id), 
                input.ConcurrencyStamp);

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
            client.DeleteFiles(request);

            await _itemAttachmentRepository.DeleteManyAsync(records.ToList());
        }
    }
}
