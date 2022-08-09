using TrustBank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrustBank.repository;

namespace TrustBank.BusinessLogic
{
    public class TransactionService: ITransactionService
    {
        private IRepository _repository;
        public IRepository repository
        {
            get => _repository ??= new repository.repository();
        }
        public bool CreateTransaction(BankTransaction bankTransaction)
        {
            return repository.CreateTransaction(bankTransaction);
        }

        public BankTransaction? GetTransactionByAccountId(int id)
        {
            return GetTransactionByAccountId(id);
        }

        public List<BankTransaction>? GetAllTransactionByBankAccountId(int bankAccountId)
        {
            return repository.GetAllTransactionByBankAccountId(bankAccountId);
        }
    }
}
