using Volo.Abp.Application.Services;

namespace DMSpro.OMS.MdmService.Grpc;
public class GrpcIdentityRequestService : ApplicationService{
    
    //private readonly Microsoft.Extensions.Configuration.IConfiguration _settingProvider;
    //protected GrpcChannel _channel;
    //public GrpcIdentityRequestService(Microsoft.Extensions.Configuration.IConfiguration settingProvider)
    //{
        
    //    _settingProvider = settingProvider;
    //    string mdmGrpcUrl = _settingProvider["GrpcRemotes:identiyUrl"];
    //    _channel = GrpcChannel.ForAddress(mdmGrpcUrl);
    //}


    //public virtual async Task<GrpcPagedResultDto<IdentityUserGrpcDto>> GrpcRequestListUser()
    //{
        
    //    Console.WriteLine("-======== CALL IDENTITY SERVICE GRPC");
    //    Console.WriteLine(CurrentUser.Id);
    //    Console.WriteLine(_channel);
    //    Console.WriteLine(_channel);
        
    //    using (_channel)
    //    {
    //        var identityAppService = _channel.CreateGrpcService<IGrpcIdentityService>();
    //        //var productDtos = await productAppService.GetListAsync(CurrentUser.Id,CurrentUser.TenantId);
    //        var usersDtos = await identityAppService.GetListUsersAsync(new DefaultFilterInput(CurrentUser.Id,CurrentUser.TenantId));
    //        foreach (var userDto in usersDtos.Items)
    //        {
    //            Console.WriteLine($"[User] Id = {userDto.Id}, Name = {userDto.Name}  , Email = {userDto.Email}");
    //        }
    //        return usersDtos;
    //    }
    //}

}