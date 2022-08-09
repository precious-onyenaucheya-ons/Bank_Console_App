using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrustBank.BusinessLogic;
using TrustBank.Models;

namespace TrustBank.User_Input.Transactions
{
    public class TransferTransaction
    {
        private static IBankAccountService _bankAccountService;
        public static IBankAccountService bankAccountService
        {
            get => _bankAccountService ??= new BankAccountService();
        }
        public static void TransferMenu(CustomerAccount customer)
        {
            Console.WriteLine("Write your 10-digit Account Number? ");
            string? acctNo = Console.ReadLine();
            while (!bankAccountService.CheckBankAccountByAccountNumber(acctNo))
            {
                Console.Clear();
                Console.WriteLine("Account number doesnt exist");
                Console.WriteLine("Do you want to perform another transaction?");
                TransactionMenu.TransactionOptions(customer);
            }
            BankAccount? BankAcc = bankAccountService.GetBankAccountsByAccountNumber(acctNo);
            while (BankAcc == null)
            {
                Console.Clear();
                Console.WriteLine("Account number doesnt exist, please input a valid account number");
                acctNo = Console.ReadLine();
                BankAcc = bankAccountService.GetBankAccountsByAccountNumber(acctNo);
            }
            while (BankAcc.CustomerId != customer.Id)
            {
                Console.Clear();
                Console.WriteLine("This account number belongs to another customer");
            }

            Console.WriteLine("How much do you wish to transfer? ");
            decimal amountToTransfer = decimal.Parse(Console.ReadLine());
            while (amountToTransfer <= 0)
            {
                Console.Clear();
                Console.WriteLine("Amount must be positive!, How much do you wish to transfer? ");
                amountToTransfer = decimal.Parse(Console.ReadLine());
            }

            if (BankAcc.AccountType == AccountType.CurrentAccount)
            {
                while (BankAcc.AccountBalance - amountToTransfer < 0)
                {
                    Console.Clear();
                    Console.WriteLine("Insufficient Balance");
                    TransactionMenu.TransactionOptions(customer);
                }

            }
            else
            {
                while (BankAcc.AccountBalance - amountToTransfer - 1000 < 0)
                {
                    Console.Clear();
                    Console.WriteLine("Insufficient Balance");
                    TransactionMenu.TransactionOptions(customer);
                }
            }

            Console.WriteLine("Purpose of transfer? ");
            string TransferInfo = Console.ReadLine();




            // Prompt user for destination account
            Console.WriteLine("Destination Account Number? ");
            string? DestinationAccount = Console.ReadLine();
            // check if user is in Database
            BankAccount? DestinationBankAcc = bankAccountService.GetBankAccountsByAccountNumber(DestinationAccount);
            while (DestinationBankAcc == null)
            {
                Console.Clear();
                Console.WriteLine("Account number doesnt exist, please input a valid destination account number? ");
                DestinationAccount = Console.ReadLine();
                DestinationBankAcc = bankAccountService.GetBankAccountsByAccountNumber(DestinationAccount);
            }


            bankAccountService.Transfer(amountToTransfer, BankAcc, TransferInfo, DestinationAccount);
            Console.WriteLine("Transaction Completed, Transfer Successful!");
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Do you want to perform another transaction? ");
            TransactionMenu.TransactionOptions(customer);
        }
    }
}
