using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrustBank.BusinessLogic;
using TrustBank.Models;

namespace TrustBank.User_Input.Transactions
{
    public class DepositTransaction
    {
        private static IBankAccountService _bankAccountService;
        public static IBankAccountService bankAccountService
        {
            get => _bankAccountService ??= new BankAccountService();
        }
        public static void DepositMenu(CustomerAccount customer)
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

            Console.WriteLine("How much do you wish to deposit? ");
            decimal amountToDeposit = decimal.Parse(Console.ReadLine());
            if (amountToDeposit <= 0)
            {  
                Console.WriteLine("Amount must be positive!.How much do you wish to deposit? ");
                amountToDeposit = decimal.Parse(Console.ReadLine());
            }

            Console.WriteLine("Purpose of Deposit? ");
            string? note = Console.ReadLine();

            if (bankAccount.CustomerId == customer.Id)
            {
                bankAccountService.Deposit(amountToDeposit, bankAccount, note);
                Console.WriteLine("Transaction Completed. Deposit Successful!");
            }
            else
            {
                Console.WriteLine("This account number belongs to another customer");
                TransactionMenu.TransactionOptions(customer);
            }

            Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Do you want to perform another transaction? ");
            TransactionMenu.TransactionOptions(customer);
        }
    }
}
