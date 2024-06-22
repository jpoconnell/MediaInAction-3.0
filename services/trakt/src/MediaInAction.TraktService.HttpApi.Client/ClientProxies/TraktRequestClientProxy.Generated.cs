// This file is automatically generated by ABP framework to use MVC Controllers from CSharp
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Http.Client;
using Volo.Abp.Http.Modeling;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Http.Client.ClientProxying;
using MediaInAction.TraktService.TraktRequests;

// ReSharper disable once CheckNamespace
namespace MediaInAction.TraktService.Controllers.ClientProxies;

[Dependency(ReplaceServices = true)]
[ExposeServices(typeof(ITraktRequestAppService), typeof(TraktRequestClientProxy))]
public partial class TraktRequestClientProxy : ClientProxyBase<ITraktRequestAppService>, ITraktRequestAppService
{
    public virtual async Task<TraktRequestDto> CompleteAsync(string paymentMethod, TraktRequestCompleteInputDto input)
        {
            return await RequestAsync<TraktRequestDto>(nameof(CompleteAsync), new ClientProxyRequestTypeValue
            {
                { typeof(string), paymentMethod },
                { typeof(TraktRequestCompleteInputDto), input }
            });
        }

        public virtual async Task<TraktRequestDto> CreateAsync(TraktRequestCreationDto input)
        {
            return await RequestAsync<TraktRequestDto>(nameof(CreateAsync), new ClientProxyRequestTypeValue
            {
                { typeof(TraktRequestCreationDto), input }
            });
        }

        public virtual async Task<bool> HandleWebhookAsync(string paymentMethod, string payload)
        {
            return await RequestAsync<bool>(nameof(HandleWebhookAsync), new ClientProxyRequestTypeValue
            {
                { typeof(string), paymentMethod },
                { typeof(string), payload }
            });
        }

        public virtual async Task<TraktRequestStartResultDto> StartAsync(string paymentMethod, TraktRequestStartDto input)
        {
            return await RequestAsync<TraktRequestStartResultDto>(nameof(StartAsync), new ClientProxyRequestTypeValue
            {
                { typeof(string), paymentMethod },
                { typeof(TraktRequestStartDto), input }
            });
        }
}