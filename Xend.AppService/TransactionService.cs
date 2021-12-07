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
    public class TransactionService : ITransactionService
    {
        private readonly IMemoryCacheService _memoryCacheService;
        private readonly ILogger<TransactionService> _logger;

        public TransactionService(IMemoryCacheService memoryCacheService,
            ILogger<TransactionService> logger)
        {
            _memoryCacheService = memoryCacheService;
            _logger = logger;
        }

        public async Task<TransactionModel> Insert(TransactionModel model)
        {
            if (model.Status is not StatusEnum.Pending
                && model.UserWalletAddress == Guid.Empty
                || model is null)
                throw new ArgumentNullException("Unprocessed payload passed.");

            var resp = await _memoryCacheService.AddNewItem<TransactionModel>(model, "txn");
            return resp;
        }

        public async Task<TransactionModel> UpdateTransaction(TransactionModel updateModel)
        {
            var update = await _memoryCacheService.UpdateItem<TransactionModel>(updateModel, "txn");
            return update;
        }

        public async Task<TransactionModel> TransactionByReference(string search)
        {
            if (string.IsNullOrEmpty(search))
                throw new ArgumentNullException("Parameter cannot be empty..");

            var get = await _memoryCacheService.Get<TransactionModel>("txn", search);
            return get;
        }

        public async Task<List<TransactionModel>> GetAll(string search = null)
        {
            if (string.IsNullOrEmpty(search))
                throw new ArgumentNullException("Parameter cannot be empty..");

            var get = await _memoryCacheService.Get<List<TransactionModel>>("txn", search);
            return get;
        }
    }
}
