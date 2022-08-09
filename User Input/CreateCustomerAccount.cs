using TrustBank.BusinessLogic;
using TrustBank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrustBank
{
    public class CreateCustomerAccount
    {
        private static ICustomerService _customerService;
        public static ICustomerService customerService
        {
            get => _customerService ??= new CustomerService();
        }
        public static void RegistrationInfo()
        {
            Console.Write("First Name? ");
            string? firstname = Console.ReadLine();
            while (!Validate.IsValidName(firstname))
            {
                Console.Clear();
                Console.Write("Invalid Entry, Name must begin with capital letters! \n First Name? ");
                firstname = Console.ReadLine();
            }

            Console.Write("Last Name? ");
            string? lastname = Console.ReadLine();
            while (!Validate.IsValidName(lastname))
            {
                Console.Clear();
                Console.Write("Invalid Entry, Name must begin with capital letters! \nLast Name ? ");
                lastname = Console.ReadLine();
            }

            Console.Write("Email Address? ");
            string? emailaddress= Console.ReadLine();
            while (!Validate.IsValidMail(emailaddress))
            {
                Console.Clear();
                Console.Write("Invalid Entry! Email Address? ");
                emailaddress = Console.ReadLine();
            }

            Console.Write("Password? ");
            string? password = Console.ReadLine();
            while (!Validate.IsValidPassword(password))
            {
                Console.Clear();
                Console.Write("Password should contain minimum of 6 alphanumeric and special characters! \n Password? ");
                password = Console.ReadLine();
            }

            if(customerService.AccountCheck(emailaddress, password))
            {
                Console.WriteLine("Account already exists");
                Console.ReadLine();
                Console.Clear();
                InitialMenuOptions.InitialMenu();
            }
            else
            {
                string fullname = firstname + " " + lastname;
                CustomerAccount customer = new(fullname, emailaddress, password);
                customerService.CreateCustomer(customer);
                Console.WriteLine($"Congratulations {fullname}, Your Customer Account has been created");
                Console.ReadLine();
                Console.Clear();
                TransactionMenu.TransactionOptions(customer);
            }
            

        }

    }
}
