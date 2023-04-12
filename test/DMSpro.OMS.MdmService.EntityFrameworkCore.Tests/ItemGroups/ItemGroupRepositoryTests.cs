using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.ItemGroups;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Xunit;

namespace DMSpro.OMS.MdmService.ItemGroups
{
    public class ItemGroupRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly IItemGroupRepository _itemGroupRepository;

        public ItemGroupRepositoryTests()
        {
            _itemGroupRepository = GetRequiredService<IItemGroupRepository>();
        }
    }
}