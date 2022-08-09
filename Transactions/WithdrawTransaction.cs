using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrustBank.BusinessLogic;
using TrustBank.Models;

namespace TrustBank.User_Input.Transactions
{
    public class WithdrawTransaction
    {
        private static IBankAccountService _bankAccountService;
        public static IBankAccountService bankAccountService
        {
            get => _bankAccountService ??= new BankAccountService();
        }
        public static void WithdrawMenu(CustomerAccount customer)
        {
            Console.WriteLine("Write your 10-digit Account Number? ");
            string? accountNo = Console.ReadLine();
            while (!bankAccountService.CheckBankAccountByAccountNumber(accountNo))
            {
                Console.Clear();
                Console.WriteLine("Account number doesnt exist");
                Console.WriteLine("Do you want to perform another transaction?");
                TransactionMenu.TransactionOptions(customer);
            }
            BankAccount? bankAct = bankAccountService.GetBankAccountsByAccountNumber(accountNo);
            while (bankAct == null)
            {
                Console.Clear();
                Console.WriteLine("Account number doesnt exist, please input a valid account number");
                accountNo = Console.ReadLine();
                bankAct = bankAccountService.GetBankAccountsByAccountNumber(accountNo);
            }
            while (bankAct.CustomerId != customer.Id)
            {
                Console.Clear();
                Console.WriteLine("This account number belongs to another customer");
            }


            Console.WriteLine("How much do you wish to withdraw? ");
            decimal amountToWithdraw = decimal.Parse(Console.ReadLine());
            while (amountToWithdraw <= 0)
            {
                Console.Clear();
                Console.WriteLine("Amount must be positive!, How much do you wish to withdraw? ");
                amountToWithdraw = decimal.Parse(Console.ReadLine());
            }
            if (bankAct.AccountType == AccountType.CurrentAccount)
            {
                while (bankAct.AccountBalance < amountToWithdraw)
                {
                    Console.Clear();
                    Console.WriteLine("Insufficient Balance");
                    TransactionMenu.TransactionOptions(customer);
                }

            }
            else
            {
                while (bankAct.AccountBalance - amountToWithdraw < 1000)
                {
                    Console.Clear();
                    Console.WriteLine("Insufficient Balance");
                    TransactionMenu.TransactionOptions(customer);
                }
            }

            Console.WriteLine("Purpose of Withdrawal? ");
            string description = Console.ReadLine();


            bankAccountService.Withdraw(amountToWithdraw, bankAct, description);
            Console.WriteLine("Transaction Completed. Withdrawal Successful!");

            Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Do you want to perform another transaction? ");
            TransactionMenu.TransactionOptions(customer);
        }

    }
}
