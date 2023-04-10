using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.Items;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Xunit;

namespace DMSpro.OMS.MdmService.Items
{
    public class ItemRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly IItemRepository _itemRepository;

        public ItemRepositoryTests()
        {
            _itemRepository = GetRequiredService<IItemRepository>();
        }
    }
}