using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.PriceUpdates;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Xunit;

namespace DMSpro.OMS.MdmService.PriceUpdates
{
    public class PriceUpdateRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly IPriceUpdateRepository _priceUpdateRepository;

        public PriceUpdateRepositoryTests()
        {
            _priceUpdateRepository = GetRequiredService<IPriceUpdateRepository>();
        }
    }
}