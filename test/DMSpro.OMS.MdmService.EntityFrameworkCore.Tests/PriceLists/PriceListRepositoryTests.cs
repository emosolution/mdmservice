using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.PriceLists;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Xunit;

namespace DMSpro.OMS.MdmService.PriceLists
{
    public class PriceListRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly IPriceListRepository _priceListRepository;

        public PriceListRepositoryTests()
        {
            _priceListRepository = GetRequiredService<IPriceListRepository>();
        }
    }
}