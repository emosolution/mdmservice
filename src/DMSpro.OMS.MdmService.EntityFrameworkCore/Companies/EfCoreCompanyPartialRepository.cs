using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Microsoft.EntityFrameworkCore;

namespace DMSpro.OMS.MdmService.Companies
{
	public partial class EfCoreCompanyRepository
	{
		public virtual async Task<Guid?> GetIdByCodeAsync(string code)
        {
            var item = (await GetDbSetAsync()).Where(x => x.Code == code).FirstOrDefault();
            return item?.Id;
        }
		
		public virtual async Task<Dictionary<string, Guid>> GetListIdByCodeAsync(List<string> codes)
        {
            var items = (await GetDbSetAsync()).Where(x => codes.Contains(x.Code));
            Dictionary<string, Guid> result = new();
            if (items.Count() < 1)
            {
                return result;
            }
            foreach (var item in items)
            {
                if (result.ContainsKey(item.Code))
                {
                    throw new BusinessException(message: "Error:ImportHandler:570", code: "1");
                }
                Guid id = item.Id;
                string code = item.Code;    
                result.Add(code, id);
            }
            return result;
        }

        public virtual async Task<int> GetCountByCodeAsync(List<string> codes)
        {
            var items = (await GetDbSetAsync()).Where(x => codes.Contains(x.Code));
            return items.Count();
        }

        public virtual async Task<bool> CheckUniqueCodeForUpdate(List<string> codes, List<Guid> ids)
        {
            var items = await (await GetDbSetAsync()).
                Where(x => codes.Contains(x.Code) && !ids.Contains(x.Id)).ToListAsync();
            return items.Count() <= 0 ? true : false;
        }

        public virtual async Task<List<Company>> GetByIdAsync(List<Guid> ids)
        {
            var items = (await GetDbSetAsync()).Where(x => ids.Contains(x.Id));
            return await items.ToListAsync();
        }

        public virtual async Task<IQueryable<CompanyWithDetails>> GetQueryWithDetailsAsync()
        {
            return from company in (await GetDbSetAsync())
                   join company1 in (await GetDbContextAsync()).Companies on company.ParentId equals company1.Id into companies1
                   from company1 in companies1.DefaultIfEmpty()
                   join geoMaster in (await GetDbContextAsync()).GeoMasters on company.GeoLevel0Id equals geoMaster.Id into geoMasters
                   from geoMaster in geoMasters.DefaultIfEmpty()
                   join geoMaster1 in (await GetDbContextAsync()).GeoMasters on company.GeoLevel1Id equals geoMaster1.Id into geoMasters1
                   from geoMaster1 in geoMasters1.DefaultIfEmpty()
                   join geoMaster2 in (await GetDbContextAsync()).GeoMasters on company.GeoLevel2Id equals geoMaster2.Id into geoMasters2
                   from geoMaster2 in geoMasters2.DefaultIfEmpty()
                   join geoMaster3 in (await GetDbContextAsync()).GeoMasters on company.GeoLevel3Id equals geoMaster3.Id into geoMasters3
                   from geoMaster3 in geoMasters3.DefaultIfEmpty()
                   join geoMaster4 in (await GetDbContextAsync()).GeoMasters on company.GeoLevel4Id equals geoMaster4.Id into geoMasters4
                   from geoMaster4 in geoMasters4.DefaultIfEmpty()

                       //    select new CompanyWithDetails {
                       //     com = company,
                       //     parentCode = company1.Code,
                       //     parentName = company1.Name,
                       //     geoLevel0Name = geoMaster.Name,
                       //     geoLevel1Name = geoMaster1.Name,
                       //     geoLevel2Name = geoMaster2.Name,
                       //     geoLevel3Name = geoMaster3.Name,
                       //     geoLevel4Name = geoMaster4.Name
                       //    };
                       // select new CompanyWithDetails{
                       //     Name = company.Name,
                       //     Code = company.Code,
                       //     Street = company.Street,
                       //     Address = company.Address,
                       //     ParentCode = company1.Code,
                       //     ParentName = company1.Name,
                       //     GeoLevel0Name = geoMaster.Name,
                       //     GeoLevel1Name = geoMaster1.Name,
                       //     GeoLevel2Name = geoMaster2.Name,
                       //     GeoLevel3Name = geoMaster3.Name,
                       //     GeoLevel4Name =geoMaster4.Name
                       // };
                   select new CompanyWithDetails
                   {
                       Company = company,
                       Company1 = company1,
                       GeoMaster = geoMaster,
                       GeoMaster1 = geoMaster1,
                       GeoMaster2 = geoMaster2,
                       GeoMaster3 = geoMaster3,
                       GeoMaster4 = geoMaster4
                   };


        }
    }
}