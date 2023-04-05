using System.Linq;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.ItemAttributes
{
    public partial class EfCoreItemAttributeRepository
    {
        public virtual async Task<int?> GetLastHierarchyLevel()
        {
            var hierarchicalAttributes =
                (await GetListAsync(x => x.HierarchyLevel != null)).OrderByDescending(x => x.HierarchyLevel);
            var lastHierarchicalAttribute = hierarchicalAttributes.FirstOrDefault();
            if (lastHierarchicalAttribute == null)
            {
                return null;
            }
            return lastHierarchicalAttribute.HierarchyLevel;
        }
    }
}