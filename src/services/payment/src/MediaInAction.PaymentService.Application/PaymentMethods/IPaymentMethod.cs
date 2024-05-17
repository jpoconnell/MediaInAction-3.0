using System.Threading.Tasks;
using MediaInAction.PaymentService.PaymentRequests;
using Volo.Abp.DependencyInjection;

namespace MediaInAction.PaymentService.PaymentMethods;

public interface IPaymentMethod : ITransientDependency
{
    string Name { get; }

    public Task<PaymentRequestStartResultDto> StartAsync(PaymentRequest paymentRequest, PaymentRequestStartDto input);

    public Task<PaymentRequest> CompleteAsync(IPaymentRequestRepository paymentRequestRepository, string token);

    public Task HandleWebhookAsync(string payload);
}