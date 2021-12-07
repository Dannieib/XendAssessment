using System;
using Xend.Domain;

namespace Xend.Domain
{
    public class TransactionModel
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public Guid UserWalletAddress { get; set; }
        public decimal Amount { get; set; }
        public CurrencyTypeEnum CurrencyType { get; set; }
        public string TxnReference { get; set; }
        public StatusEnum Status { get; set; }
        public DateTime DateTimeInitiated { get; set; }
        public DateTime? DateTimeCompleted { get; set; }
    }
}
