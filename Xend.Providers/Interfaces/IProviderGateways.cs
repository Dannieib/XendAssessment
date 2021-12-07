using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xend.Providers.Interfaces
{
    public interface IProviderGateways
    {
        Task<object> SendCrypto<TRequest>(TRequest payload);
        Task<object> ReceiveCrypto<TRequest>(TRequest payload);
        Task<object> GetTransactionByReference(string reference);
    }
}
