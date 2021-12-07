using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xend.AppService.MemoryService;
using Xend.Domain;
using Xend.Providers.Interfaces;

namespace Xend.AppService
{
    public class TransactionProcessor : ITransactionProcessor
    {
        private readonly ITransactionService _transactionService;
        private readonly ILogger<TransactionService> _logger;

        public TransactionProcessor(
            ILogger<TransactionService> logger, ITransactionService transactionService)
        {
            _logger = logger;
            _transactionService = transactionService;
        }

        public async Task<bool> HandleNewTransactions(TransactionModel model)
        {
            try
            {
                //check if txn aleady with same status, currencyType and walletId..
                var checkForExistence = await _transactionService.TransactionByReference(model.TxnReference);

                if (checkForExistence is not null &&
                    checkForExistence.TxnReference == model.TxnReference &&
                    checkForExistence.UserWalletAddress == model.UserWalletAddress &&
                    checkForExistence.CurrencyType == model.CurrencyType)
                    throw new ArgumentException("Sorry, Transaction already exist with these reference");

                var insert = await _transactionService.Insert(model);
                return (insert is not null) ? true : false;
            }
            catch (Exception e)
            {
                _logger.LogCritical(e.Message, e);
                throw;
            }
        }

        public async Task<IEnumerable<TransactionModel>> GetAllTransaction(string search = null)
        {
            try
            {
                return await _transactionService.GetAll(search);
            }
            catch (Exception e)
            {
                _logger.LogCritical(e.Message, e);
                throw;
            }
        }

        public async Task<TransactionModel> GetAllTransactionByReference(string reference)
        {
            try
            {
                var get = await _transactionService.TransactionByReference(reference);
                if (get is not null) return get;
                return null;
            }
            catch (Exception e)
            {
                _logger.LogCritical(e.Message, e);
                throw;
            }
        }

    }
}
