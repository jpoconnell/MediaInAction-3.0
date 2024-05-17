using MediaInAction.EmbyService.EntityFrameworkCore;
using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MediaInAction.EmbyService.EmbyRequests
{
    public class EmbyRequestRepository_Tests : EmbyServiceEntityFrameworkCoreTestBase
    {
        private readonly IEmbyRequestRepository _paymentRequestRepository;
        public EmbyRequestRepository_Tests()
        {
            _paymentRequestRepository = GetRequiredService<IEmbyRequestRepository>();
        }

        [Fact]
        public async Task Should_Insert_Emby_Request()
        {
            var id = Guid.NewGuid();
            var paymentRequest = new EmbyRequest(id, Guid.NewGuid().ToString(), 456, "USD");

            await _paymentRequestRepository.InsertAsync(paymentRequest, autoSave: true);

            var inserted = await _paymentRequestRepository.GetAsync(id);

            inserted.Id.ShouldNotBe(Guid.Empty);
        }
    }
}
