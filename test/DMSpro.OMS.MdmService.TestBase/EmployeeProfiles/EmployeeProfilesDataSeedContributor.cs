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

        public EmployeeProfilesDataSeedContributor(IEmployeeProfileRepository employeeProfileRepository, IUnitOfWorkManager unitOfWorkManager, WorkingPositionsDataSeedContributor workingPositionsDataSeedContributor)
        {
            _employeeProfileRepository = employeeProfileRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _workingPositionsDataSeedContributor = workingPositionsDataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _workingPositionsDataSeedContributor.SeedAsync(context);

            await _employeeProfileRepository.InsertAsync(new EmployeeProfile
            (
                id: Guid.Parse("1fb0435c-53b9-4183-a98c-c656337727cf"),
                code: "b6857091adda49a2adf5",
                erpCode: "eb2d11e8b38f42e8bc44",
                firstName: "7cdbbf096dea4dceb7645fd4ab002366185de5c80d184ba3bdd162386e665be02c804225f4a34cb5a60584840ad4137cc5c9634b93e444beb0ac762f96311a77e756b1825d0f4b7aa85a96c046bc379bdc43a0054efe447d841d86c2b2b8fe4d1db720ca2dca40fbb41b14e45325284b3f82da0e4b75413e8443fe78bb09c87",
                lastName: "aee634de295d4606892089c56d0235990154894502bb436ca736d44af24b0ba2b4ebeea10af54ab7a51b69882f248d8ca2980132d9434202bbf9a74bc18e5ed88f58909f267b4122a02c747581cd696223bf5d4f0943413ca8b6b53e1c457c6210bc9b0bff7846ef888e0a793ad3c59a1f8f5573ce15439fb3b6b5013310330",
                dateOfBirth: new DateTime(2016, 1, 12),
                idCardNumber: "91a90408cca54bd6ad40d7e31738afb2d8686ceffb28478ca8252a125fd952b6ca9a4be4363b45be87409b748f1db23ac3edae92fc064ac5abbfb2d33c374df8f7c79b8d66214ccd95acf20d3e0ec4a526ed157f259846358219a2ec5edaab7cd8fb75bef7bc43988d1dc09b61018fd59615d7634f5a45949d53727cc622a59",
                email: "d89aa3c09d30403b823c717d5b668c922a593078c472410cb6d8851aa46a6d4003738d387e0749feab8a0e1f47f5c591ddeb637224304fb5b1d10dea1f38@c810319d5a464e81bc471f02793bb34400fe605d49ca4dd98d4667a6f8da9774e89abbc357ac42ab9923cf35207581e6a5982fafaa7b40b581f337f727f9.com",
                phone: "f1c6c519d23847faa76a7cfcc0f29775df81fb0d76614e12b925758f3a6cfa09e151d34c8d5748ff8096c6817f87ae9a20244969e0d6482eb189f2006e85b5517a4bc2ab51614c81bca91cfe2cd682b19b1e04fe33ac480ab4b0471b3f79f4605d61d814cca340369ebce4a263c30766918133799450407baf3df764c4973fd",
                address: "d1c46517be384958b2152abc0ef327c7fc8a0c9c9b4e442d8de0aef318409f105ddd05eee0194a758fadcc505ed93be94f9514a3bd09413eb4b059aca6166012401ff138be7247c4bbbe119ee9fc82c678386a67f818469b944cfea81bc0537f92aa070fd46341e4b469dfb39f382a241e94fb0688524139827c5ce65e2d704c012460dd0b274d33a2374fd243270a5c7062b60eeadf4b14b5fb3c678662c7cfee3e508d1fe84e50b297ea6eb5d6ac9823bb5216155a4396967ac8f59addb4df6f0efb21d65b4932afa33cd0dd6aba8120040016e0964ea99a26d086226722bf43dfa812b6114d3088f88e9b3c1d545cfe98edda70e840c489c5",
                active: true,
                effectiveDate: new DateTime(2021, 2, 22),
                endDate: new DateTime(2021, 8, 10),
                identityUserId: Guid.Parse("0cb9bdfe-7c8d-466a-a663-f9b7c0e17ecc"),
                employeeType: default,
                workingPositionId: null
            ));

            await _employeeProfileRepository.InsertAsync(new EmployeeProfile
            (
                id: Guid.Parse("32df1798-92d6-4ae8-a915-d71d6f15197e"),
                code: "883d2c60753e40a49f2a",
                erpCode: "cda3b8fa11bc46529f6e",
                firstName: "051ce15bf9554ffb88fa3dbb8861c328b524ce1f155a4da78a4ba516bffb278aa2c110edc94942d3955ee49756a41fa8f1d5dd4fb1b34f7883a44751226851e9ae8b5ec87b95483b9f6573be95dd00fa5d376bb339c94132b8d0033e3a61fa3891cbe8748d4a4849a5030b42bed2a6049be4861d79ce4c0597f83f77121c469",
                lastName: "5583b062798e4a878b62bde301e3a8719e69adb83cb34933923ec6a197f14d07183826581f9642888406334a4cdd0ecc346af3b66ac948fa9d895ecc1c2e3e75414a54a1b1cc48b282353d89989df396301a1a31da6d41b498cc053381d2fc975df132aac45c4cbeba910c6b40b9cae5f1337ece18484952b64fbdbf8f9906e",
                dateOfBirth: new DateTime(2013, 2, 1),
                idCardNumber: "e86dc8df491a4bf9876d13a40cd7fde1daaa3fe897c248aab7c56efd715d6c9751c6f8d983ce4fbc8d8194d0f2f86c04b46595e4f81146c99acd57d5fcb871ab3ee71df7603248ba9c7d17fd129de4fd52485c2fdc344cdcb806af4898998aa9dfb1d56c579f4cdc9bd28adfad0a48c8c7e643d53e76455f975fbbf90ecfb9f",
                email: "b9712a50c72740fe9d2af6a8af3a2326e25b2e03b2d04da98e0288d9bcd43ef7508602a4d2d8455fb34ab2e19930ec84aff22d0ed53146a4bb31784c1fed@30f1f860950e4176b4b223d1490fb1f23ce07e9cc0f04fbb84807dc5b864669f725d2097f03045c491c3d4c6cbc5f872804e11e84a7a4155896021c36ae3.com",
                phone: "7acc804f43234bd1b6cb7740bd75c7095ae7a04fe0aa44aebe91de366c4e117820c472c0f3854e359b90d44dd7d30e45dac762bf16684fc79195c756a4d84cdd755c53e30a604f2fb83548412ac74c700b6c1bda8a6240f0849f5d4b5de79a9c8196ecb96bc04d068c446db195a50b862ca49688d03942929e9124376d6e902",
                address: "ca0d88d05703460db24624ab3f1c478f504673b1a44d405fb1cba055fcd918c98f426acef88e4cf4bbd9f20186f703754d1ea88722fb4a7db0cb0892314a2fb57cb4d339d5434cfd97cbaccdca7ead873cdfeee855ec4b488ba29a9f5b4294dcdedadd4927b64f0388d32c2a08090d56239350505e604dfd85b9b078424654112c71654af890443a9715371c240fe002953d30ed6eeb4ea589c474b8f8d2660d45fa0fa7630746f79ff05479640a7e5d6224a6fe7aed47a086f97bf120be5a84536081cf25744593a5d2dedd379ca4e4bb17630e1ddb43ba8622637ad0335479bec795541c5345c8b7a703acf1a04db8513d71b7bd864cd7820a",
                active: true,
                effectiveDate: new DateTime(2009, 1, 24),
                endDate: new DateTime(2006, 2, 20),
                identityUserId: Guid.Parse("3a8c3f43-488d-4581-9b43-44f716029458"),
                employeeType: default,
                workingPositionId: null
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}