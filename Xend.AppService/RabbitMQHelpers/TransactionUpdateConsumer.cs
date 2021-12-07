using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xend.Domain;

namespace Xend.AppService.RabbitMQHelpers
{
    public class TransactionUpdateConsumer : IConsumer<TransactionModel>
    {
        private readonly ITransactionProcessor _transactionProcessor;

        public TransactionUpdateConsumer(ITransactionProcessor transactionProcessor)
        {
            _transactionProcessor = transactionProcessor;
        }

        public async Task Consume(ConsumeContext<TransactionModel> ctx)
        {
            TransactionModel tranx = ctx.Message;
            Console.WriteLine($"A new transaction from {tranx.ClientId} received.");

            await ctx.Publish<TransactionModel>(new
            {
                TransacsactionId = tranx.Id,
                UserId = tranx.ClientId,
                UserWalletId = tranx.UserWalletAddress,
                TotalAmountDebited = tranx.Amount,
                CurrencyType = tranx.CurrencyType,
                DateTimeInitiated = tranx.DateTimeInitiated
            });
            await _transactionProcessor.HandleNewTransactions(tranx);
            //return tranx;
        }
    }
}
