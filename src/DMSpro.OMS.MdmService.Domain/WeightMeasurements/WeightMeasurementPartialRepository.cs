using System;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.WeightMeasurements
{
	public partial interface IWeightMeasurementRepository
	{
		Task<Guid?> GetIdByCodeAsync(string code);
	}
}