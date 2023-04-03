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
                id: Guid.Parse("3fc2a4ad-9179-4e93-a189-3ea230977844"),
                code: "6fcfb9a17810436f86e9",
                erpCode: "5076de5771cb4fdb96f5",
                firstName: "51e6dc36268146adb4e132652632250d9551e1104182491db8537914c876a294a90e6a292cc04b5b8fdf35bba3e1ad8803402c54cfcf42b0aab5603e306c5cc87898aa819165425ca222937ab703a0a3e10f49e84eac43dc979cb18e8246c7a5948a3ba3f8b74b5f8ad02b1bfda37b1a131523a308dc4cca98d068ba4299bfb",
                lastName: "771b48b985334a6ea7052eaab186d5a5d99cbdfc78cf4919a1ef092f8c5aa5ae942cf5a2cbfa4c4ab7c324ee98ed1a8e6bcd6471d4404dacbad46a9907bf4adcff0c46953e7e40f39e7f715d2abc2c7753bf2a0d1cbc4feb85f3b7abdfc3a8a6c9ac0561a1a047cd9c91bcd200e6f115fa16ecd7b75446a98fe775a8ab8f82f",
                dateOfBirth: new DateTime(2017, 7, 1),
                idCardNumber: "d65c49d50a9c4e5e8609a8d681688625a842971918b34855b9fd3bffb9016c2b5d58bf541a64422e8a15226df507c46d9e1f5b2f42c64d208f3ac650e90860dc052e1ee2a20642dea9cbaa2e2204deeba25fd33281a349d98ae927a644f1d0ef1f6634e352d24e0e9d811038c1470c33824c3e5ca7ae43218d5858697ccb94a",
                email: "0fa89b8a61dc4f038c150b203af998e028cb2e8cc63341d095966e7cf5de7206ff20fa05054746719c37497c1b1aa86bd1d6e9029eae455ba524af1a2720@fbc2b20337e041118044a9317c01b487ecf72088d5af47ac91c7e9a5fb7968dea5cf98044bbc476197b6190610d215210148028da63e45238ea50eaa2928.com",
                phone: "42e0a6d320c74ac09650fe522f9897094c21e1e112e341d0b0e08372e7e8a7bbb2168195861d4b27a6e99125ddbf0bfcd4ca7301cf644b6aa96f9fc7ef3aaa50e412821885884b2b845fa2c81345756cb92558f6d3a34387a930afb95ee82a2fe405626956d04568a8dbbbbc41fbb7594669f1bad8d34304a4cfb8ff4abe6b8",
                address: "725fb08d8d504d618c2b19f6f118069f85b859e98a4544e381530efe665179091c71692bdea84dacab2bf20d39a3d7228604fbeb89d040979d8fef6b6a3036af8f3cd59cae984449a6823b21a2dae5fbf2f6b46ab1f9426db8be008dd9a957077dfc016943594fe6a5281e098358aa37d2dfe9f73f3240c881ba2485c4e85cf3e09204454a724c13819436c1423769198287ed2f525740c585ec2d91f84863c37686d886c0834d9cbf65d4a83b8a48b86d32e2d4e3c2426a8406d754c3a508af28dfcb5f6d744f398b63835d8c0220311484fc70521e4db987b2e945d89f380866969b20e2db4ce396b00648fb28e25616f424676ea64859a642",
                active: true,
                effectiveDate: new DateTime(2013, 10, 12),
                endDate: new DateTime(2008, 10, 12),
                identityUserId: Guid.Parse("bb669a19-b38a-4979-918b-8dcaec855cf4"),
                workingPositionId: null,
                employeeTypeId: null
            ));

            await _employeeProfileRepository.InsertAsync(new EmployeeProfile
            (
                id: Guid.Parse("1006f6e9-95fe-4b64-933a-f0eaaee0df16"),
                code: "b76edc3615014b14aab5",
                erpCode: "4d56d63d4a554feaa0b7",
                firstName: "ca036d17d04346e087fd6ffd91e93ffddd8688293675402b9ad280918e409a4797a37134c51548418f44094e14fd1f8228d07d8ca34e405fb011a264b116f2438b1f7b7dccb54300997a2ddeb43056e5f04ca20c245d47508436d4601e9a66082fe6e7662455404aac3fd019a8bcbb7bea1787cffb894fe9a24aef01c44f5b5",
                lastName: "3a381efa8bf54868ba5a947ff553d16e997e746a3b4c4020b44ead49cd5091145a0e718152784a999f55f83e0b9f08ca047be86f46474846860dffa7a9e109df268ca5a973e4493d9d375ebc4e4a2942c4c8682e45f748309e30be3aad42ac1725b91120e15a4702a9c1d81e6e5353b4dfddcb0308d1480e8ab3f6e7cafa4c7",
                dateOfBirth: new DateTime(2010, 7, 27),
                idCardNumber: "66de414712a14ed0a9c399a6a1ace4d652a2561cb66943c198810e4a7b9cecdf35b76207b4414c98bf30ab76bde3dd8f6a3275f168ae44fa90450f5b2e174d4c26a5d9986fc149b7be1025ae3a99c62b59ccaf2e09a94ff39bc5ea65f5ae099ef5a475cb672f451fa5dda2729913e23d189728422341447bb2a8b8ca488eb90",
                email: "7c44c1b64819428f91c88c7c955cf36e3b153adf80c44ad5bd62afdd6bf188f1bb9f1f184ec849ccb09e40f9c1d686d04a64997d1a5a43b8af51919e07cd@fae5fc32080c46c7a052c1c449b62c152b626c2434c54866a56315cb7e3108d6fa9b5858030c407daa139b52c0bed48c99e3f691b1524a2ab39fa0ed2f39.com",
                phone: "2ff508d64537493d835564bc8e23b69d7424b7f646554090b4da6d8491706db41221d9d7bc19422988d3ba3d361ecc4dc2d95374ff884e63bfed463fd97996ae64e2acc730684c1d9cf21843d6cebdc636b55a2812fb4393bd94393b8cc016289b342bdb8fdb491f9e9a04a8fe96b8c47d4ec090dcf047c6a2625270663f69d",
                address: "5fe60d2a79e240758595e882a03ec29f7d6dd420e5844d0380a48e642a4b5279d5797e0ff43447f4bc3a30310e4d18f8290b2904970a41289e8402b0573ef951df158a7644974661b6548ad950617d4c530c629a30214f77b8f46bea70269e432d63b9ea8e0d40c19b8cf530fc78892cfb6ae5c704e3428096f85895ea6182601479d5d260c64a099a6b6aa509b6b635a9ce989ee8f04376a527b034150d80b8a4d76e8656eb4549ab2f035989d50c3eb14375699ff249199f09ad30854444f1ca4f8282f5bd4c2385267178e8d5d011ed3e3f8d4fa6406c9f5fbc075bc1ed39c645d0f1d95a4bd2b857934ab5a12211c2f96ede7b27468993ca",
                active: true,
                effectiveDate: new DateTime(2016, 11, 24),
                endDate: new DateTime(2011, 7, 26),
                identityUserId: Guid.Parse("94a644c4-862b-42fe-a0b6-fb2c26bdab85"),
                workingPositionId: null,
                employeeTypeId: null
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}