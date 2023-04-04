using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.ItemAttributeValues;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Xunit;

namespace DMSpro.OMS.MdmService.ItemAttributeValues
{
    public class ItemAttributeValueRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly IItemAttributeValueRepository _itemAttributeValueRepository;

        public ItemAttributeValueRepositoryTests()
        {
            _itemAttributeValueRepository = GetRequiredService<IItemAttributeValueRepository>();
        }
    }
}