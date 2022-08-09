using TrustBank.Models;

namespace TrustBank.BusinessLogic
{
    public interface ITransactionService
    {
        bool CreateTransaction(BankTransaction bankTransaction);


        BankTransaction? GetTransactionByAccountId(int id);


        List<BankTransaction>? GetAllTransactionByBankAccountId(int bankAccountId);
        
    }
}