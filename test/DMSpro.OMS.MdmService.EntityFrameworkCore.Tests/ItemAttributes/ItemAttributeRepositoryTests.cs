using DMSpro.OMS.MdmService.EntityFrameworkCore;

namespace DMSpro.OMS.MdmService.ItemAttributes
{
    public class ItemAttributeRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly IItemAttributeRepository _itemAttributeRepository;

        public ItemAttributeRepositoryTests()
        {
            _itemAttributeRepository = GetRequiredService<IItemAttributeRepository>();
        }
    }
}