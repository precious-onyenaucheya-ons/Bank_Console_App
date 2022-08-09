using TrustBank.Models;

namespace TrustBank.BusinessLogic
{
    public interface IBankAccountService
    {
        bool CreateBankAccount(BankAccount bankAccount);


        BankAccount? GetBankAccountByCustomerId(int id);

        bool CheckBankIdByCustomerId(int id);

        bool CheckBankAccountByAccountNumber(string accountNumber);

        List<BankAccount>? GetAllAccountsByCustomerId(int customerId);

        BankAccount? GetBankAccountsByAccountNumber(string accountNumber);

        void CheckBalance(string accountNumber);

        bool Deposit(decimal amount, BankAccount bankAccount, string note);

        bool Withdraw(decimal amount, BankAccount bankAccount, string note);

        bool Transfer(decimal amount, BankAccount bankAccount, string note, string DestinationAccount);

        void PrintAccountDetails(int customerId);

        void PrintAccountStatement(BankAccount bankAccount);
        
    }
}