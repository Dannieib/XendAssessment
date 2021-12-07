using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xend.Domain;

namespace Xend.API.Commands
{
    public class TransactionServiceCommand : IRequest<bool>
    {
        public Guid ClientId { get; set; }
        public Guid UserWalletAddress { get; set; }
        public decimal Amount { get; set; }
        public CurrencyTypeEnum CurrencyType { get; set; }
        public StatusEnum Status { get; set; }
    }

    public class TransactionServiceCommandHandler : IRequestHandler<TransactionServiceCommand, bool>
    {
        private readonly ILogger<TransactionServiceCommandHandler> _logger;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IBus _bus;

        public TransactionServiceCommandHandler(ILogger<TransactionServiceCommandHandler> logger, IPublishEndpoint publishEndpoint, IBus bus)
        {
            _logger = logger;
            _publishEndpoint = publishEndpoint;
            _bus = bus;
        }

        public async Task<bool> Handle(TransactionServiceCommand request, CancellationToken cancellationToken)
        {
            try
            {
                //do all neccessary checks here..

                var transaction = new TransactionModel
                {
                    Id = Guid.NewGuid(),
                    UserWalletAddress = request.UserWalletAddress,
                    Amount = request.Amount,
                    Status = StatusEnum.Pending,
                    ClientId = request.ClientId, //can also be gotten from the sessionId or bearer token.
                    CurrencyType = request.CurrencyType,
                    DateTimeInitiated = DateTime.Now
                };
                Uri uri = new Uri("amqp://localhost:15672/tnxQueue");
                var endPoint = await _bus.GetSendEndpoint(uri);
                await endPoint.Send(transaction);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message, ex);
                throw;
            }
        }
    }
}
