using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrustBank.Models
{
    public class BankTransaction
    {
        public int Id { get; set; }
        public int BankAccountId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Note { get; set; }
        private static int _id;
        public decimal Balance { get; set; }

        public BankTransaction(int bankAccountId, decimal amount, DateTime date, string note, decimal balance)
        {
            Id = ++_id;
            BankAccountId = bankAccountId;
            Amount = amount;
            Date = date;
            Note = note;
            Balance = balance;
        }


    }
}
