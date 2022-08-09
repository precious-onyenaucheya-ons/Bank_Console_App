using TrustBank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrustBank.Transactions;
using TrustBank.User_Input.Transactions;
using TrustBank.BusinessLogic;

namespace TrustBank
{
    public class TransactionMenu
    {
        private static IBankAccountService _bankAccountService;
        public static IBankAccountService bankAccountService
        {
            get => _bankAccountService ??= new BankAccountService();
        }
        public static void TransactionOptions(CustomerAccount customer)
        {
            int option;
            do
            {
                
                Console.WriteLine("Select an option");
                Console.WriteLine("Select 1 to Create Bank Account");
                Console.WriteLine("Select 2 to Deposit");
                Console.WriteLine("Select 3 to Withdraw");
                Console.WriteLine("Select 4 to Transfer");
                Console.WriteLine("Select 5 to Check Balance");
                Console.WriteLine("Select 6 to Print Account Details");
                Console.WriteLine("Select 7 Print Account Statement");
                Console.WriteLine("Select 8 to Log out");
                option = int.Parse(Console.ReadLine());
                Console.Clear();    
            }
            while (option < 0 || option > 8);

            if ((!bankAccountService.CheckBankIdByCustomerId(customer.Id)) && (option > 1 && option < 7))
            {
                Console.WriteLine("You dont have a bank Account, Create one first!");
                Console.WriteLine("Select 1 to Create Account");
                option = int.Parse(Console.ReadLine()); 
            }
            Console.Clear();
            switch (option)
            {
                case 1:
                    CreateBankAccount.CreateNewAccount(customer);
                    break;

                case 2:
                    DepositTransaction.DepositMenu(customer);
                    break;

                case 3:
                    WithdrawTransaction.WithdrawMenu(customer);
                    break;

                case 4:
                    TransferTransaction.TransferMenu(customer);
                    break;

                case 5:
                    CheckBalance.CheckAccountBalance(customer);
                    break;

                case 6:
                    PrintTables.PrintAccountDetails(customer);
                    break;
                case 7:
                    PrintTables.PrintAccountStatement(customer);
                    break;
                case 8:
                    InitialMenuOptions.InitialMenu();
                    break;
            }
            Console.WriteLine("Do you want to perform another transaction? ");
            TransactionMenu.TransactionOptions(customer); ;
        }

    }
}
