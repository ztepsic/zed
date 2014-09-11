using Zed.Domain;

namespace Zed.Tests.Domain.ValueObjects {
    public class Money : ValueObject {

        [NotValueMember]
        public string NotValueMember1 { get; set; }

        [NotValueMember]
        public string NotValueMember2 { get; set; }

        private readonly decimal amount;

        public decimal Amount { get { return amount; } }

        private readonly string currency;
        public string Currency { get { return currency; } }

        public Money(decimal amount, string currency) {
            this.amount = amount;
            this.currency = currency;
        }
    }
}
