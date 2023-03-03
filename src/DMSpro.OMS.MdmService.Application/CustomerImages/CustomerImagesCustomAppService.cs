using DMSpro.OMS.MdmService.EmployeeImages;
using DMSpro.OMS.MdmService.EmployeeProfiles;
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

namespace DMSpro.OMS.MdmService.CustomerImages
{
    public partial class CustomerImagesAppService
    {
        [Authorize(MdmServicePermissions.Customers.Create)]
        public virtual async Task<CustomerImageDto> CreateAsync(Guid customerId,
             IRemoteStreamContent inputFile,
             string description, bool active, bool isPOSM,
             Guid? itemPOSMId)
        {
            if (isPOSM != true)
            {
                itemPOSMId = null;
            }
            else
            {
                if (itemPOSMId == null)
                {
                    throw new BusinessException(message: L["Error:CustomerImage:550"], code: "1");
                }
            }
            return await CreateImageAsync(customerId, inputFile,
                description, active, false, isPOSM, itemPOSMId);
        }

        [Authorize(MdmServicePermissions.Customers.Create)]
        public virtual async Task<CustomerImageDto> CreateAvatarAsync(Guid customerId,
            IRemoteStreamContent inputFile, string description)
        {
            return await CreateImageAsync(customerId, inputFile,
                description, true, true, false, null);
        }

        [Authorize(MdmServicePermissions.Customers.Edit)]
        public virtual async Task<CustomerImageDto> UpdateAsync(Guid id, Guid customerId,
            IRemoteStreamContent inputFile,
            string description, bool active, bool isPOSM,
            Guid? itemPOSMId)
        {
            if (isPOSM != true)
            {
                itemPOSMId = null;
            }
            else
            {
                if (itemPOSMId == null)
                {
                    throw new BusinessException(message: L["Error:CustomerImage:550"], code: "1");
                }
            }
            return await UpdateImageAsync(id, customerId, inputFile,
                description, active, false, isPOSM, itemPOSMId);
        }

        [Authorize(MdmServicePermissions.Customers.Edit)]
        public virtual async Task<CustomerImageDto> UpdateAvatarAsync(Guid customerId,
            IRemoteStreamContent inputFile, string description)
        {
            var customer = await _customerRepository.GetAsync(customerId);
            var image = await _customerImageRepository.GetAsync(x => x.CustomerId == customer.Id &&
                x.IsAvatar == true);
            return await UpdateImageAsync(image.Id, customerId, inputFile,
                description, true, true, false, null);
        }

        private async Task<CustomerImageDto> CreateImageAsync(Guid customerId,
            IRemoteStreamContent inputFile,
            string description, bool active, bool isAvatarImage,
            bool isPOSM, Guid? itemPOSMId)
        {
            string contentType = inputFile.ContentType;
            if (!_fileManagementInfoAppService.AcceptedImageContentTypes.Contains(contentType))
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
                DirectoryId = _fileManagementInfoAppService.EmployeeImageDirectoryId.ToString(),
                Name = inputFile.FileName,
                ContentType = contentType,
                Content = content,
            };
            var client = new FilesProtoAppService.FilesProtoAppServiceClient(channel);
            var response = await client.UploadFileAsync(request);
            OMS.Shared.Protos.FileManagementService.Files.File file = response.File;

            if (isAvatarImage)
            {
                await SetAllIsAvatarFalse();
            }
            var customerImage = await _customerImageManager.CreateAsync(customerId,
                itemPOSMId, description, active, isAvatarImage, isPOSM, Guid.Parse(file.Id));

            return ObjectMapper.Map<CustomerImage, CustomerImageDto>(customerImage);
        }

        private async Task<CustomerImageDto> UpdateImageAsync(Guid id, Guid customerId,
            IRemoteStreamContent inputFile,
            string description, bool active, bool isAvatarImage,
            bool isPOSM, Guid? itemPOSMId)
        {
            string contentType = inputFile.ContentType;
            if (!_fileManagementInfoAppService.AcceptedImageContentTypes.Contains(contentType))
            {
                var detailDict = new Dictionary<string, string> { ["contentType"] = contentType };
                string detailString = JsonSerializer.Serialize(detailDict).ToString();
                throw new UserFriendlyException(message: L["Error:FileManagement:551"], code: "0", details: detailString);
            }

            var record = await _customerImageRepository.GetAsync(id);
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
                DirectoryId = _fileManagementInfoAppService.EmployeeImageDirectoryId.ToString(),
                Name = inputFile.FileName,
                ContentType = contentType,
                Content = content,
            };
            var uploadResponse = await client.UploadFileAsync(uploadRequest);
            OMS.Shared.Protos.FileManagementService.Files.File file = uploadResponse.File;

            if (isAvatarImage)
            {
                await SetAllIsAvatarFalse();
            }
            var customerImage = await _customerImageManager.UpdateAsync(
                id, customerId, itemPOSMId, description, active, isAvatarImage,
                isPOSM, Guid.Parse(file.Id));

            return ObjectMapper.Map<CustomerImage, CustomerImageDto>(customerImage);
        }

        [Authorize(MdmServicePermissions.Customers.Delete)]
        public virtual async Task DeleteManyAsync(List<Guid> ids)
        {
            var records = (await _customerImageRepository.GetQueryableAsync()).Where(x => ids.Contains(x.Id));
            var fileIds = records.Select(x => x.FileId.ToString()).ToList();
            DeleteFilesRequest request = new()
            {
                TenantId = _currentTenant.Id == null ? "" : _currentTenant.Id.ToString(),
            };
            request.FileIds.Add(fileIds);
            using GrpcChannel channel = GrpcChannel.ForAddress(_settingProvider["GrpcRemotes:FileManagementServiceUrl"]);
            var client = new FilesProtoAppService.FilesProtoAppServiceClient(channel);
            await client.DeleteFilesAsync(request);

            await _customerImageRepository.DeleteManyAsync(records.ToList());
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

        private async Task SetAllIsAvatarFalse()
        {
            var images = (await _customerImageRepository.GetQueryableAsync()).ToList();
            foreach (var image in images)
            {
                image.IsAvatar = false;
            }
            await _customerImageRepository.UpdateManyAsync(images);
        }
    }
}
