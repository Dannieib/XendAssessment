using System.Collections.Generic;
using System.Threading.Tasks;
using Xend.Domain;

namespace Xend.AppService
{
    public interface ITransactionService
    {
        Task<List<TransactionModel>> GetAll(string search = null);
        Task<TransactionModel> Insert(TransactionModel model);
        Task<TransactionModel> TransactionByReference(string search);
        Task<TransactionModel> UpdateTransaction(TransactionModel updateModel);
    }
}