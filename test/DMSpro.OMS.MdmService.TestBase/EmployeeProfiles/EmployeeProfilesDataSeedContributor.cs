using DMSpro.OMS.MdmService.SystemDatas;
using DMSpro.OMS.MdmService.WorkingPositions;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using DMSpro.OMS.MdmService.EmployeeProfiles;

namespace DMSpro.OMS.MdmService.EmployeeProfiles
{
    public class EmployeeProfilesDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IEmployeeProfileRepository _employeeProfileRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly WorkingPositionsDataSeedContributor _workingPositionsDataSeedContributor;

        private readonly SystemDatasDataSeedContributor _systemDatasDataSeedContributor;

        public EmployeeProfilesDataSeedContributor(IEmployeeProfileRepository employeeProfileRepository, IUnitOfWorkManager unitOfWorkManager, WorkingPositionsDataSeedContributor workingPositionsDataSeedContributor, SystemDatasDataSeedContributor systemDatasDataSeedContributor)
        {
            _employeeProfileRepository = employeeProfileRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _workingPositionsDataSeedContributor = workingPositionsDataSeedContributor; _systemDatasDataSeedContributor = systemDatasDataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _workingPositionsDataSeedContributor.SeedAsync(context);
            await _systemDatasDataSeedContributor.SeedAsync(context);

            await _employeeProfileRepository.InsertAsync(new EmployeeProfile
            (
                id: Guid.Parse("b582d913-b271-48f8-ae8b-93fc32c81072"),
                code: "d476420e4be54fee9ce0",
                erpCode: "e6fe7eb6c0a1494895126c84bdfac163f643cef3193640d9b51fdcdcd1dcf6",
                firstName: "c4a40cc5441e45069cfb0f913c808650b5447e08d46e4f0a917f6597b02c2dd4fc06c80688d34ae79341ccd34947a4d77b8678db22ef455195eecb98cda2e796f3c68352bbad41759201c3e69e9de645f047a79dc5c548a78ef5775f5f39b1108622a9e0def944f78d443bcd8709aed069622c4c58624afc80ee6e0403ac2df",
                lastName: "fe2f00d796b34f93b2f6489ecaf8952d2a7c53751815405ca286c09cc6515ae9b1a2b2247f24484994d",
                dateOfBirth: new DateTime(2007, 6, 25),
                idCardNumber: "b1f181fd18fd47ceb7dca3f11482e77d21",
                email: "f7e2caed8ff04f3da34f4e741b67b30beb2059adc6e74f3ca775ade6078",
                phone: "b631ca41575848a483b5a09",
                address: "8a97351474564e6c99b8dd906304f9404a822fe75749490f912b9713d46120e8f7a46a6c8d2a46c9b5",
                active: true,
                effectiveDate: new DateTime(2005, 1, 17),
                endDate: new DateTime(2011, 3, 15),
                identityUserId: Guid.Parse("220a1018-c670-4821-853d-cdc6d69caad4"),
                workingPositionId: null,
                employeeTypeId: null
            ));

            await _employeeProfileRepository.InsertAsync(new EmployeeProfile
            (
                id: Guid.Parse("914b814c-0616-470d-a337-dcfa64e6f06b"),
                code: "33dcdd5f135f4e81bac3",
                erpCode: "8b983d209bdd4bcb840ca9db",
                firstName: "a1b76843579e453b82eb71d170fccf44f7ad5eddb71a41708ae3808d9d78c5cad9fc98be87fc4d92b4a1461c782b1d829e77349ac5ad46159d0936027024f88140f09f65183144f8b28890af259fb1545dec460ab2584fea90803050cac4b6db64fc75d83e554149842875afa394373670404a25b049455eafbaf1bd490824c",
                lastName: "53307772fe1b4c4e8cb6e4",
                dateOfBirth: new DateTime(2010, 2, 1),
                idCardNumber: "701010128285",
                email: "a02b922e9fb542799f936b1a92f73dfd0c5",
                phone: "6e652cc8eba64b509a7309bbc477eea5aa61d060",
                address: "4b429e7535834396bc07fb364ca3e05c8f6a1ef6b0cd4652882f755ee2de00d27c2f592ceb284d2aaa0402a2909bd263933",
                active: true,
                effectiveDate: new DateTime(2006, 7, 21),
                endDate: new DateTime(2021, 4, 8),
                identityUserId: Guid.Parse("13ba454b-9c13-453d-93d9-b89371fbac3a"),
                workingPositionId: null,
                employeeTypeId: null
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}