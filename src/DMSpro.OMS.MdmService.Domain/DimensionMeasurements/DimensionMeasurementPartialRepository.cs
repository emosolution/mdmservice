using System;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.DimensionMeasurements
{
	public partial interface IDimensionMeasurementRepository
	{
		Task<Guid?> GetIdByCodeAsync(string code);
	}
}