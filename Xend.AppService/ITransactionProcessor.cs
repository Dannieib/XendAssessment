using System.Collections.Generic;
using System.Threading.Tasks;
using Xend.Domain;

namespace Xend.AppService
{
    public interface ITransactionProcessor
    {
        Task<IEnumerable<TransactionModel>> GetAllTransaction(string search = null);
        Task<TransactionModel> GetAllTransactionByReference(string reference);
        Task<bool> HandleNewTransactions(TransactionModel model);
    }
}