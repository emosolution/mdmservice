using DMSpro.OMS.MdmService.Partial;
using DMSpro.OMS.MdmService.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;

using DevExtreme.AspNet.Data.ResponseModel;
using DMSpro.OMS.Shared.Domain.Devextreme;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
namespace DMSpro.OMS.MdmService.Companies
{
    public partial interface ICompaniesAppService : IApplicationService
    {
        Task<PagedResultDto<CompanyWithNavigationPropertiesDto>> GetListAsync(GetCompaniesInput input);

        Task<CompanyWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<CompanyDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid?>>> GetCompanyLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid?>>> GetGeoMasterLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<CompanyDto> CreateAsync(CompanyCreateDto input);

        Task<CompanyDto> UpdateAsync(Guid id, CompanyUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(CompanyExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();

        Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev);
        Task<int> InsertFromExcelAsync(IFormFile file);
        Task<int> UpdateFromExcelAsync(IFormFile file);

    }
}