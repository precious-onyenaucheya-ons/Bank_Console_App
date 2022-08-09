using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrustBank.Models
{
    public class BankAccount
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string AccountNumber { get; set; }
        public decimal AccountBalance { get; set; }
        public AccountType AccountType { get; set; }
        private static int _id;

        public BankAccount(int customerId, string accountNumber, AccountType accountType)
        {
            CustomerId = customerId;
            AccountNumber = accountNumber;
            AccountType = accountType;
            Id = ++_id;
        }
    }
}
