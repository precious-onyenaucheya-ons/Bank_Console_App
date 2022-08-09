using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrustBank.Models;

namespace TrustBank.repository
{
    public interface IRepository
    {
        bool CreateCustomer(CustomerAccount customer); 
        bool AccountCheck(string email, string password);

        CustomerAccount? GetCustomerById(int id);
        CustomerAccount? GetCustomerByEmailAndPassword(string email, string password);

        bool CreateBankAccount(BankAccount bankAccount);

        BankAccount? GetBankAccountByCustomerId(int id);

        bool CheckBankIdByCustomerId(int id);

        BankAccount? GetBankAccountByAccountNumber(string accountNumber);

        List<BankAccount>? GetAllBankAccountsByCustomerId(int customerId);

        bool CreateTransaction(BankTransaction bankTransaction);

        BankTransaction? GetTransactionByAccountId(int id);

         List<BankTransaction>? GetAllTransactionByBankAccountId(int bankAccountId);

        bool UpdateBankAccount(BankAccount bankAccount);
        bool CheckBankAccountByAccountNumber(string accountNumber);
    }
}
