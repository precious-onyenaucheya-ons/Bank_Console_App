using TrustBank.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrustBank.repository
{
    public class repository : IRepository
    {

        private static  List<CustomerAccount> _customers = new();
        private static  List<BankAccount> _bankAccounts = new();
        private static  List<BankTransaction> _bankTransactions = new();


        public bool CreateCustomer(CustomerAccount customer)
        {
            int numberOfAccounts = _customers.Count;
            if (!_customers.Exists(x => x.EmailAddress == customer.EmailAddress))
            {
                _customers.Add(customer);
            }

            return _customers.Count > numberOfAccounts;
        }

        public bool AccountCheck(string email, string password)
        {
            return _customers.Exists(x => x.EmailAddress == email && x.Password == password);
        }

        public CustomerAccount? GetCustomerById(int id)
        {
            return _customers.Find(x => x.Id == id);
        }
        public CustomerAccount? GetCustomerByEmailAndPassword(string email, string password)
        {
            return _customers.Find(x => x.EmailAddress == email && x.Password == password);
        }

        public bool CreateBankAccount(BankAccount bankAccount)
        {
            int numberOfAccounts = _bankAccounts.Count;
            _bankAccounts.Add(bankAccount);
            return _bankAccounts.Count > numberOfAccounts;
        }

        public BankAccount? GetBankAccountByCustomerId(int id)
        {
            return _bankAccounts.Find(x => x.CustomerId == id);
        }

        public bool CheckBankIdByCustomerId(int id)
        {
            return _bankAccounts.Exists(x => x.CustomerId == id);
        }

        public bool CheckBankAccountByAccountNumber(string accountNumber)
        {
            return _bankAccounts.Exists(x => x.AccountNumber == accountNumber);
        }

        public BankAccount? GetBankAccountByAccountNumber(string accountNumber)
        {
            return _bankAccounts.Find(x => x.AccountNumber == accountNumber);
        }

        public List<BankAccount>? GetAllBankAccountsByCustomerId(int customerId)
        {
            return _bankAccounts.Where(x => x.CustomerId == customerId).ToList();
        }

        public bool CreateTransaction(BankTransaction bankTransaction)
        {
            int numberOfTransaction = _bankTransactions.Count;
            _bankTransactions.Add(bankTransaction);
            return _bankTransactions.Count > numberOfTransaction;
        }

        public BankTransaction? GetTransactionByAccountId(int id)
        {
            return _bankTransactions.Find(x => x.BankAccountId == id);
        }

        public List<BankTransaction>? GetAllTransactionByBankAccountId(int bankAccountId)
        {
            return _bankTransactions.Where(x => x.BankAccountId == bankAccountId).ToList();
        }

        public bool UpdateBankAccount(BankAccount bankAccount)
        {
            foreach (var account in _bankAccounts)
            {
                if (account.Id == bankAccount.Id)
                {
                    account.AccountBalance = bankAccount.AccountBalance;
                    return true;
                }
            }
            return false;
        }
    }
}
