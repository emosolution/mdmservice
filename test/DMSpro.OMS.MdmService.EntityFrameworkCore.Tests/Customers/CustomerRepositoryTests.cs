using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.Customers;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Xunit;

namespace DMSpro.OMS.MdmService.Customers
{
    public class CustomerRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerRepositoryTests()
        {
            _customerRepository = GetRequiredService<ICustomerRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _customerRepository.GetListAsync(
                    code: "1b9b88c5bbf14ca085c3",
                    name: "bdaf7e4d09a74f2480bd9029b0c79aeb88c842d89baa4a75b93a7906e23324f8680eb89f84884a77a823ffeb2b99535535d943463de844688a0aab15cfbbf05c208fb47a90fd4ecebad2ee58fcdfec2871f2cf4ef45a4079b6bc35fdf197082a519080fc0b4a4d3c89c0eba4adacde7170155333772b4356ae4775ed3e01455",
                    phone1: "5ec763ba9ca246e3bf48f00228e841453dec5bbcca62490bbe",
                    phone2: "92ba7b1d270f4f618b79c945cf92188604d3fd2fac6d4b6082",
                    erpCode: "f2ebfe64aa974f8fbbb7",
                    license: "cfd41d68087b48138c0df120c21ed6ac90636a5078c843659c",
                    taxCode: "79ece263e1e84e0ca8ca421dd1bb5ac23a1a71c3763b445a86",
                    vatName: "2f19d75e508f45cdb8c519a7fda004c90ed80e1342c74371b27fa9d4a19388a899f537e2beb44e118c6747cd16d8336fa5748fd91ab440a8a6ef69370c504d901c2ecb69a5f245648d1dadd703242eecaf1ff35ff87846b98e94fc13252efa8387955145d84545558428c9ced61a90a29d0431fbe0644948a38d9e5c319bb4b",
                    vatAddress: "cc35eeb997c54c18b6737df1c95039648b2f9fb15c124070a834c8987e52b42ec2d647f4644b474ab43babb30e54a5af857216b3e0214c8d8245381765b4b3db7afacca34975435ab77f13ccd0352f5a4abdb83530604e10a00e4f7e96fd23b929332d3b3dfa458581f4f69e3a134639dfd21bd0ccc74fa89ff4817253d2c74df03cacf249894a94bf4d4c05990dc8e4b7b847745d644a64b32e00de525c9ffe4da2fb4957684663831d2520c22c17fdb1818875a5cf4eb08dbd6c28f1ed1ce8ec917b0b275e4945b4010089f558a664849c6fe691f54dc7859d9ce453ef148cfc429ba3c2b04e46b7eaa47bee19ba02192207756edc4090a1bafe5dbbfb6b1e7472318dcca14ddd9b4d558698f7b7bb6c27a169e8c5492a89246e3f2b3930010dcd657401654ae1a131ff144f0ac109ea0f155c1abb47f4808f9849a7eb01162aed873e34d547c8a1b096bea9b328f9039310b9aa43433bbc81e614ba5b0477bbc6569d4dc7450a896d8af073e40e2d3bc7e7b656594cb584578b78ba570aa816e34eb7ca88425d9800a5fe40acfe4dbd6e0da436fa4a4ab5980f429d82182ba55bcdc59373453ab95faddd8c43a26997637d718f7548a08daa040cc93afd21fff9ca9a9ec64a2ea643e01532809b19eae7f86176534dc99369bb818e373d0607694d9faeae4f37b39fc267534518d87a06f0b2",
                    active: true,
                    isCompany: true,
                    warehouseId: Guid.Parse("c081cb6f-95e4-4e5d-9539-bd00cf188d15"),
                    street: "2c0e7a7849f24cff89d30ee197d3d0a023f03972b34b429796a9d456c94104b5ad8db081aede4cfab4d04835e0d042d91aff442efb6648c6a4d6613be25a36926b91c7f8caa648988b9ffde67bf29b14c3a8000e2bde4cf2a9470709f3a71c03df9b642639614504ab4c9fc7e7858ca4bbc2a9aaf42148ed8ff05adac77a8a3",
                    address: "e5c59ace1e764635b3a5e45354a7378791e6cdec5e104a0a85f33e1aaf01610ab111f1781c3d4624be628fdfec70804f6f126ccf743c42528d5ce7f88f2ad253dd20ad009af340fbb9151146ad7863d95b7a6d8442a74f0393555398b7b1cd272cfa76b3c1bd4cec80455f62f4a36b4d685fffb133b4485596fdc570ce390828ce8a3b05b83e4124a75c3afafd239b480f19487b5776497c99bd1cd67dc9b95152193ac0101341fc9f2754627d6c46b2ec68c5f8305841f2b085c9d6877ec7fa188194fec6784e20a5f0d25668d6623eecf5e6eb1245475f849946e32919cd68eaf3e4c39b1f4a75ad1da7d847cc2064d133a26f75884725b9ae",
                    latitude: "750d874b6015471a8ab17743f106a461907822099c1545baac13236192e0130b8ee96e937361423fa19aaf77b9477bb059705b8cddba463995fa9114be18f4817b989653198844cea56a80b2dcb3a050c75c83eef58849979c29a3c80d5033627eddc44ea14f41d2a8525cbb5b8b259f0cceea3ab0ca4d8081602a238f5443f",
                    longitude: "5853b4b8964a4d6994575692aa8ba17a356e6f510efd4307ab6c8cd25ce777774f99dc1b816448adb501230c4d5946b4c5b38c444eac4dd4b042f22c70cbbfe3a78747c8714c4e23b05a5f06aa9a2d417881b2cb52e048feb4ecca87e5161c89e3ba994f0840488fb4ebac2fc1abd2cb56058c19b2844796b468617c44ae0d9",
                    sfaCustomerCode: "b3e56d170fc64964b837"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("03de2fdd-eb64-4eb0-bdae-cc79b5ee1a51"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _customerRepository.GetCountAsync(
                    code: "395de3e12c144ec3b331",
                    name: "d147029119774b39b1d2828eb0e2b129c232db29ce834905af1b9602a6862a0737857b2668d0420a99c943ecb7ddbafaabed728dc4de4f4ca7c58e40cadef2844dcc94990cfb43159ef926d159432f333fc4a55497a049fab5579dd63a92ec1a44285bcbc8614aefb92989876405f3eb0bea425e80184f63a2479324cd3eebb",
                    phone1: "2e6ce6a885a348d9b3f17a6be9e19e4d54835f2f9119434f89",
                    phone2: "bd34c8db0bbf459b9bf5b3b10eb87f9c8d9fce61f4384752b5",
                    erpCode: "9a96dfc7097b41c89faa",
                    license: "8e921b98d94b48388dffb0489af3e5bc98b7ba4424554023bd",
                    taxCode: "0619c088b2e841259bb18a64cb88ac4c6daa08e5f9ab40958c",
                    vatName: "4138db9ca7044b73b7c5fc73277077657e910fba68fe494888a3140cb75398c982a3145877b6487195a337512c4b60c0dcc87a16db974aa0a5eefdac999bca65628212b4c2544276a3e6e843ccd250d4a4cdf509265a4c7da1c786d9ad6aa624b3c15c34b6d74034a6133a03b5dc5fd51998519339bc4835b161cf723b40505",
                    vatAddress: "a20e368991c84252b1f52989ea0ce333a421bf3f947c4e2d8383ae70f8e97c6c5d14bfdc455d4f088cd1b56488afc1ff15f9dd913ca04772b1fc90d70a0855c43b796629a5b14357851fd3106c8c194095dd067f95d043d9b6b8b20db98e47752c420ce55e924c6b97b33bd34952d8cf46ed3ef0faa64a49b0328a1e241b5651cbad1b4465fa410ab075d132bbc5cb5ce8d9ce7fc6c847b49fdde10164d20cd343ca1d8478fe409a9037595febdebf83378d8f8455ad492393fea0276a91b9ce2c38d720a73b49fb9c3255a5e3caf23fb2a8c7994dd84d28bb9c4ecf6508076ef3aa4c1ace6c4519877b4a92a6c48b1f18f29b7779e841c9b97f3e01e5a9ad76818d54e160404d7796038b1b690111eb619616e4e9a140f5848486f685ec6f9d0f69ecfbfbba40879208334c6e7a7bddc20e5bd39b7e454f89d5388c20e45624afbb46f4c50d41d8a80bfff22b158b8b0b13ae6b96a24460a19e87687090db08723afe6bc9164d60b57f8e25ad61d5eb6814e69eab9141cdb169bb4e18abc3bb29ac83ba91514706bec47fb8882065e90c37cb082e1e4be0a705e0f417defae9f4453315899442aa9b369c6aec2544fe8565ad95734b414293ccc3ae962770775718bf9404b4470889c762cb7d7a1adfdac292aaff8e40ec91ea7a935a2623423ad336ad7cb9447eb2c5d3ab4ecfd94933e38916",
                    active: true,
                    isCompany: true,
                    warehouseId: Guid.Parse("6cd39781-a799-47a0-9652-052bb016df88"),
                    street: "51a7f41a2c3a4f78b9d60eb97578ef59220bccc4cf0d4f5485ef634bd81c566854839acdbe06405086b8951c78e6f54e98ad8c7fda3e485bb3522bc3e162982175acbd40686e443bb31f905308cace95e2a44e32b9254f8cb26f487a8c2ba29fc58fda82c72c4388bc8f3731034f192df009f48b5ea8405392548a134b2e080",
                    address: "9bc4cf9a58c84de4a00e42c50b42c6fa549383f559554b5e92f3183743be17bc6de30d8b3f7c4fb3b21165c610c95e22a90436ffe3d64cf5a1fa8887f5746b6d5bed7a7d58c8431c87a858a386490bbf96f359d423c3408b9d5ec523e34a9c72a8f86a28a19b4a2da6fae50becf113fab470677f21d04b7d910a52809e994ead7e7ed13927454553850964c934af37c7ca701404e4fc4e798edb9fee74f542a134e61ca2932a42a0ac9dbad87c865e2cce55e833220c4e1aa2270c42e54beafa76f32a058ded459b8f8a0146687f1fa753caadb359674729adbf8825ea49f61f5550a31a15074060b69d89dca139d41153349d4c8a9943eda0f5",
                    latitude: "42c0eb2eb2c048c494feee7ee2b786612eaf578785bf44a68a51d2096fc541c0bcc60581073a43e09ec2b05d10dcfa2539297c26f34647e582bf1ec0c1a001a0dd407b75b27c4db9a8e5b454fd2994f4aeb2242cc6db474fb0b4f03d184553cc9298e05ab916461da5d852822bb2020966aba3038a964820adf37b0ba41765d",
                    longitude: "cb35f059fd4f4ce9aa76ecaf6e5969ff19a6e96462af429fb22e817538b4078961ebae029070414f9e5dba438739f1b21fc2534b6d284435bb2df2d0264ec49635b1a8f684d24b5489a92439920c34d5d1f54c0dc78540d085accb0692851c5794bdcd035dbe45708d6177316417d1787af1637d6cfe408a911562246bab128",
                    sfaCustomerCode: "9421f3a8474a40c0aa04"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}