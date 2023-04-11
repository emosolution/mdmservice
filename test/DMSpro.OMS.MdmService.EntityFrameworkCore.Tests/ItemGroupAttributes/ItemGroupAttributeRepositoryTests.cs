using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.ItemGroupAttributes;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Xunit;

namespace DMSpro.OMS.MdmService.ItemGroupAttributes
{
    public class ItemGroupAttributeRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly IItemGroupAttributeRepository _itemGroupAttributeRepository;

        public ItemGroupAttributeRepositoryTests()
        {
            _itemGroupAttributeRepository = GetRequiredService<IItemGroupAttributeRepository>();
        }
    }
}