using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DMSpro.OMS.Shared.Grpc;
using DMSpro.OMS.MdmService.Permissions;
using Volo.Abp.Authorization;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Services;
using DMSpro.OMS.MdmService.Companies;
using DMSpro.OMS.Shared.GRPC.MdmService.Companies;

namespace DMSpro.OMS.MdmService;

public class GrpcMdmService : ApplicationService, IGrpcMdmService
{
    private readonly ICompanyRepository _companyRepository;
    public GrpcMdmService(ICompanyRepository companyRepository){
        _companyRepository = companyRepository;
        ObjectMapperContext = typeof(MdmServiceApplicationModule);
    }
    //public async Task<List<CompanyGrpcDto>> GetListAsync(Guid? userId, Guid? tenantId)
    public async Task<List<CompanyGRPCDto>> GetListAsync()
    {
        
        // Console.WriteLine("=====COMPANY");
        // Console.WriteLine(CurrentTenant.Id);
        // Console.WriteLine("CHange Teanat)");
        // using (CurrentTenant.Change(tenantId))
        // {
        //     var listComs = await _companyRepository.GetCountAsync("");
        //     Console.WriteLine(listComs);
        // }
        return new List<CompanyGRPCDto>
        {
            new CompanyGRPCDto { Id = Guid.NewGuid(), Name = "Company 1" },
            new CompanyGRPCDto { Id = Guid.NewGuid(), Name = "Company 2" },
        };
    }
    public async Task<GrpcPagedResultDto<CompanyGRPCDto>> GetListCompsAsync(DefaultFilterInput input)
    {
        
        Console.WriteLine("=====COMPANY");
        Console.WriteLine(CurrentTenant.Id);
        Console.WriteLine("CHange Teanat)");

        using (CurrentTenant.Change(input.TenantId))
        {
            var countComps = await _companyRepository.GetCountAsync("");
            var listComps = await _companyRepository.GetListAsync("");
            Console.WriteLine("ASSSSSSSSS");
            Console.WriteLine(listComps);
            //ObjectMapperContext = typeof(MdmServiceApplicationModule);
            return new GrpcPagedResultDto<CompanyGRPCDto>
            {
                TotalCount = countComps,
                Items = ObjectMapper.Map<List<Company>, List<CompanyGRPCDto>>(listComps)
            };
        }
        // return new List<CompanyGrpcDto>
        // {
        //     new CompanyGrpcDto { Id = Guid.NewGuid(), Name = "Company 1" },
        //     new CompanyGrpcDto { Id = Guid.NewGuid(), Name = "Company 2" },
        // };
    }

    
}