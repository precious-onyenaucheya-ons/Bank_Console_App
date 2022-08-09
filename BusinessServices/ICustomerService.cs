using TrustBank.Models;

namespace TrustBank.BusinessLogic
{
    public interface ICustomerService
    {
        bool CreateCustomer(CustomerAccount customer);

        CustomerAccount? GetCustomerByEmailAndPassword(string email, string password);


        bool AccountCheck(string email, string password);


        CustomerAccount? GetCustomerById(int id);
        
    }
}