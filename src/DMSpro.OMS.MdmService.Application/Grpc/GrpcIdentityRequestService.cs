using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using DMSpro.OMS.MdmService.Permissions;
using DMSpro.OMS.MdmService.CustomerAssignments;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using DMSpro.OMS.MdmService.Shared;
using Grpc.Net.Client;
using ProtoBuf.Grpc.Client;
using DMSpro.OMS.Shared.Grpc;
// using Microsoft.Extensions.Configuration;

namespace DMSpro.OMS.MdmService.Grpc;
public class GrpcIdentityRequestService : ApplicationService{
    
    private readonly Microsoft.Extensions.Configuration.IConfiguration _settingProvider;
    protected GrpcChannel _channel;
    public GrpcIdentityRequestService(Microsoft.Extensions.Configuration.IConfiguration settingProvider)
    {
        
        _settingProvider = settingProvider;
        string mdmGrpcUrl = _settingProvider["GrpcRemotes:identiyUrl"];
        _channel = GrpcChannel.ForAddress(mdmGrpcUrl);
    }


    public virtual async Task<GrpcPagedResultDto<IdentityUserGrpcDto>> GrpcRequestListUser()
    {
        
        Console.WriteLine("-======== CALL IDENTITY SERVICE GRPC");
        Console.WriteLine(CurrentUser.Id);
        Console.WriteLine(_channel);
        Console.WriteLine(_channel);
        
        using (_channel)
        {
            var identityAppService = _channel.CreateGrpcService<IGrpcIdentityService>();
            //var productDtos = await productAppService.GetListAsync(CurrentUser.Id,CurrentUser.TenantId);
            var usersDtos = await identityAppService.GetListUsersAsync(new DefaultFilterInput(CurrentUser.Id,CurrentUser.TenantId));
            foreach (var userDto in usersDtos.Items)
            {
                Console.WriteLine($"[User] Id = {userDto.Id}, Name = {userDto.Name}  , Email = {userDto.Email}");
            }
            return usersDtos;
        }
        

        
    }

}