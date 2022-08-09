using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrustBank.BusinessLogic;
using TrustBank.Models;

namespace TrustBank.User_Input.Transactions
{
    public class PrintTables
    {
        private static IBankAccountService _bankAccountService;
        public static IBankAccountService bankAccountService
        {
            get => _bankAccountService ??= new BankAccountService();
        }
        public static void PrintAccountDetails(CustomerAccount customer)
        {
            bankAccountService.PrintAccountDetails(customer.Id);

            Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Do you want to perform another transaction? ");
            TransactionMenu.TransactionOptions(customer);
        }

        public static void PrintAccountStatement(CustomerAccount customer)
        {

            Console.WriteLine("Write your 10-digit Account Number? ");
            string? accNo = Console.ReadLine();

            while (!bankAccountService.CheckBankAccountByAccountNumber(accNo))
            {
                Console.Clear();
                Console.WriteLine("Account number doesnt exist");
                Console.WriteLine("Do you want to perform another transaction?");
                TransactionMenu.TransactionOptions(customer);
            }
            BankAccount? bankacc = bankAccountService.GetBankAccountsByAccountNumber(accNo);
            while (bankacc == null)
            {
                Console.Clear();
                Console.WriteLine("Account number doesnt exist, please input a valid account number");
                accNo = Console.ReadLine();
                bankacc = bankAccountService.GetBankAccountsByAccountNumber(accNo);
            }
            while (bankacc.CustomerId != customer.Id)
            {
                Console.Clear();
                Console.WriteLine("This account number belongs to another customer");
                TransactionMenu.TransactionOptions(customer);
            }
            bankAccountService.PrintAccountStatement(bankacc);

            Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Do you want to perform another transaction? ");
            TransactionMenu.TransactionOptions(customer);
        }
    }
}
