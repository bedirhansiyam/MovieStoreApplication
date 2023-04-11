using WebApi.DBOperations;
using WebApi.Entities;

namespace TestSetup;

public static class Customers
{
    public static void AddCustomers(this MovieStoreDbContext context)
    {
        context.Customers.AddRange(
            new Customer
            {
                Name = "Baltasar", 
                Surname = "Robert", 
                Email = "balt@mail.com",
                Password = "123456789",
            },
            new Customer
            {
                Name = "Jennifer", 
                Surname = "Brian", 
                Email = "jenn@mail.com",
                Password = "123456789",
            },
            new Customer
            {
                Name = "Isabeau", 
                Surname = "Oluf", 
                Email = "oluf@mail.com",
                Password = "123456789",
            });
    }
}