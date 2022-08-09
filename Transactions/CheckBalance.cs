using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrustBank.BusinessLogic;
using TrustBank.Models;

namespace TrustBank.User_Input.Transactions
{
    public class CheckBalance
    {
        private static IBankAccountService _bankAccountService;
        public static IBankAccountService bankAccountService
        {
            get => _bankAccountService ??= new BankAccountService();
        }
        public static void CheckAccountBalance(CustomerAccount customer)
        {
            Console.WriteLine("Write your 10-digit Account Number? ");
            string? accountNumber = Console.ReadLine();

            while (!bankAccountService.CheckBankAccountByAccountNumber(accountNumber))
            {
                Console.Clear();
                Console.WriteLine("Account number doesnt exist");
                Console.WriteLine("Do you want to perform another transaction?");
                TransactionMenu.TransactionOptions(customer);
            }
            BankAccount? bankAccount = bankAccountService.GetBankAccountsByAccountNumber(accountNumber);
            if (bankAccount == null)
            {
                Console.Clear();
                Console.WriteLine("Account number doesnt exist");
                Console.WriteLine("Do you want to perform another transaction?");
                TransactionMenu.TransactionOptions(customer);
            }
            
            bankAccountService.CheckBalance(accountNumber);
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Do you want to perform another transaction?");
            TransactionMenu.TransactionOptions(customer);

        }
    }
}
