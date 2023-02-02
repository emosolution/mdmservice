using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using DMSpro.OMS.MdmService.Permissions;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using DMSpro.OMS.MdmService.Shared;

namespace DMSpro.OMS.MdmService.WorkingPositions
{

    [Authorize(MdmServicePermissions.WorkingPositions.Default)]
    public partial class WorkingPositionsAppService : ApplicationService, IWorkingPositionsAppService
    {
        private readonly IDistributedCache<WorkingPositionExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly IWorkingPositionRepository _workingPositionRepository;
        private readonly WorkingPositionManager _workingPositionManager;

        public WorkingPositionsAppService(IWorkingPositionRepository workingPositionRepository, WorkingPositionManager workingPositionManager, IDistributedCache<WorkingPositionExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _workingPositionRepository = workingPositionRepository;
            _workingPositionManager = workingPositionManager;
        }

        public virtual async Task<PagedResultDto<WorkingPositionDto>> GetListAsync(GetWorkingPositionsInput input)
        {
            var totalCount = await _workingPositionRepository.GetCountAsync(input.FilterText, input.Code, input.Name, input.Description, input.Active);
            var items = await _workingPositionRepository.GetListAsync(input.FilterText, input.Code, input.Name, input.Description, input.Active, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<WorkingPositionDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<WorkingPosition>, List<WorkingPositionDto>>(items)
            };
        }

        public virtual async Task<WorkingPositionDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<WorkingPosition, WorkingPositionDto>(await _workingPositionRepository.GetAsync(id));
        }

        [Authorize(MdmServicePermissions.WorkingPositions.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _workingPositionRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.WorkingPositions.Create)]
        public virtual async Task<WorkingPositionDto> CreateAsync(WorkingPositionCreateDto input)
        {

            var workingPosition = await _workingPositionManager.CreateAsync(
            input.Code, input.Name, input.Description, input.Active
            );

            return ObjectMapper.Map<WorkingPosition, WorkingPositionDto>(workingPosition);
        }

        [Authorize(MdmServicePermissions.WorkingPositions.Edit)]
        public virtual async Task<WorkingPositionDto> UpdateAsync(Guid id, WorkingPositionUpdateDto input)
        {

            var workingPosition = await _workingPositionManager.UpdateAsync(
            id,
            input.Code, input.Name, input.Description, input.Active, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<WorkingPosition, WorkingPositionDto>(workingPosition);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(WorkingPositionExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _workingPositionRepository.GetListAsync(input.FilterText, input.Code, input.Name, input.Description, input.Active);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<WorkingPosition>, List<WorkingPositionExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "WorkingPositions.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new WorkingPositionExcelDownloadTokenCacheItem { Token = token },
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
                });

            return new DownloadTokenResultDto
            {
                Token = token
            };
        }
    }
}