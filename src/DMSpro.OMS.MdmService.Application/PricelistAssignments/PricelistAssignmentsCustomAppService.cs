using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using DMSpro.OMS.MdmService.Permissions;
using System.Collections.Generic;
using DMSpro.OMS.MdmService.Customers;
using System.Linq;

namespace DMSpro.OMS.MdmService.PricelistAssignments
{

    public partial class PricelistAssignmentsAppService
    {
        [Authorize(MdmServicePermissions.PriceListAssignments.Release)]
        public virtual async Task<PricelistAssignmentDto> ReleaseAsync(Guid id)
        {
            DateTime now = DateTime.Now;
            var record = await _pricelistAssignmentRepository.GetAsync(x => x.Id == id && 
                x.IsReleased == false && x.ReleasedDate == null); 
            record.ReleasedDate = now;
            record.IsReleased = true;
            await _pricelistAssignmentRepository.UpdateAsync(record);

            var customerGroup = await _customerGroupRepository.GetAsync(x => x.Id == record.CustomerGroupId && x.Status == CustomerGroups.Status.RELEASED);
            var listCustomer = new List<Customer>();
            switch (customerGroup.GroupBy)
            {
                case CustomerGroups.Type.ATTRIBUTE:
                    var cusGroupAttr = await _customerGroupAttributeRepository.GetListAsync(x => x.CustomerGroupId == customerGroup.Id);
                    foreach (var item in cusGroupAttr)
                    {
                        listCustomer.AddRange(await _customerRepository.GetListAsync(x =>
                            (item.Attr0Id == null || x.Attr0Id == item.Attr0Id) &&
                            (item.Attr1Id == null || x.Attr1Id == item.Attr1Id) &&
                            (item.Attr2Id == null || x.Attr2Id == item.Attr2Id) &&
                            (item.Attr3Id == null || x.Attr3Id == item.Attr3Id) &&
                            (item.Attr4Id == null || x.Attr4Id == item.Attr4Id) &&
                            (item.Attr5Id == null || x.Attr5Id == item.Attr5Id) &&
                            (item.Attr6Id == null || x.Attr6Id == item.Attr6Id) &&
                            (item.Attr7Id == null || x.Attr7Id == item.Attr7Id) &&
                            (item.Attr8Id == null || x.Attr8Id == item.Attr8Id) &&
                            (item.Attr9Id == null || x.Attr9Id == item.Attr9Id) &&
                            (item.Attr10Id == null || x.Attr10Id == item.Attr10Id) &&
                            (item.Attr11Id == null || x.Attr11Id == item.Attr11Id) &&
                            (item.Attr12Id == null || x.Attr12Id == item.Attr12Id) &&
                            (item.Attr13Id == null || x.Attr13Id == item.Attr13Id) &&
                            (item.Attr14Id == null || x.Attr14Id == item.Attr14Id) &&
                            (item.Attr15Id == null || x.Attr15Id == item.Attr15Id) &&
                            (item.Attr16Id == null || x.Attr16Id == item.Attr16Id) &&
                            (item.Attr17Id == null || x.Attr17Id == item.Attr17Id) &&
                            (item.Attr18Id == null || x.Attr18Id == item.Attr18Id) &&
                            (item.Attr19Id == null || x.Attr19Id == item.Attr19Id)
                        ));
                    }
                    break;
                case CustomerGroups.Type.LIST:
                    var cusGroupIdList = _customerGroupListRepository.GetListAsync(x => x.CustomerGroupId == customerGroup.Id).Result.Select(x => x.CustomerId);
                    listCustomer.AddRange(await _customerRepository.GetListAsync(x => cusGroupIdList.Contains(x.Id)));
                    break;
                case CustomerGroups.Type.GEO:
                    var cusGroupGeo = await _customerGroupGeoRepository.GetListAsync(x => x.CustomerGroupId == customerGroup.Id);
                    foreach (var item in cusGroupGeo)
                    {
                        listCustomer.AddRange(await _customerRepository.GetListAsync(x =>
                            (x.GeoMaster0Id == item.GeoMaster0Id) &&
                            (x.GeoMaster1Id == null || x.GeoMaster1Id == item.GeoMaster1Id) &&
                            (x.GeoMaster2Id == null || x.GeoMaster2Id == item.GeoMaster2Id) &&
                            (x.GeoMaster3Id == null || x.GeoMaster3Id == item.GeoMaster3Id) &&
                            (x.GeoMaster4Id == null || x.GeoMaster4Id == item.GeoMaster4Id)
                        ));
                    }
                    break;
                default:
                    break;
            }

            listCustomer.ForEach(x => x.PriceListId = record.PriceListId);
            await _customerRepository.UpdateManyAsync(listCustomer);

            return ObjectMapper.Map<PricelistAssignment, PricelistAssignmentDto>(record);
        }
    }
}