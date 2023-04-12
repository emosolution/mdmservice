using DMSpro.OMS.MdmService.ItemGroups;
using DMSpro.OMS.MdmService.Items;
using Volo.Abp.Domain.Entities;
namespace DMSpro.OMS.MdmService.ItemGroupLists
{
	public class ItemGroupListWithDetailsDto: ItemGroupListDto, IHasConcurrencyStamp
	{
        public ItemGroupDto ItemGroup { get; set; }
        public ItemDto Item { get; set; }
        
		public ItemGroupListWithDetailsDto()
		{
		}
	}
}

