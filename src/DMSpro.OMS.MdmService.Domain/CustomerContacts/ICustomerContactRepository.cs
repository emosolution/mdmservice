using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.CustomerContacts
{
    public interface ICustomerContactRepository : IRepository<CustomerContact, Guid>
    {
        Task<CustomerContactWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);

        Task<List<CustomerContactWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            Title? title = null,
            string firstName = null,
            string lastName = null,
            Gender? gender = null,
            DateTime? dateOfBirthMin = null,
            DateTime? dateOfBirthMax = null,
            string phone = null,
            string email = null,
            string address = null,
            string identityNumber = null,
            string bankName = null,
            string bankAccName = null,
            string bankAccNumber = null,
            Guid? customerId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<CustomerContact>> GetListAsync(
                    string filterText = null,
                    Title? title = null,
                    string firstName = null,
                    string lastName = null,
                    Gender? gender = null,
                    DateTime? dateOfBirthMin = null,
                    DateTime? dateOfBirthMax = null,
                    string phone = null,
                    string email = null,
                    string address = null,
                    string identityNumber = null,
                    string bankName = null,
                    string bankAccName = null,
                    string bankAccNumber = null,
                    string sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string filterText = null,
            Title? title = null,
            string firstName = null,
            string lastName = null,
            Gender? gender = null,
            DateTime? dateOfBirthMin = null,
            DateTime? dateOfBirthMax = null,
            string phone = null,
            string email = null,
            string address = null,
            string identityNumber = null,
            string bankName = null,
            string bankAccName = null,
            string bankAccNumber = null,
            Guid? customerId = null,
            CancellationToken cancellationToken = default);
    }
}