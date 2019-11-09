using AdventureWorks.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace AdventureWorks.Domain
{
    public class CustomerManager
    {
        public IList<Customer> SearchCustomers(string keyword)
        {
            keyword = keyword.ToUpper();

            List<Customer> customers = new List<Customer>();
            using (var dbContext = new Entities.AdventureWorks2017Context())
            {
                IEnumerable<Entities.Customer> entities = dbContext.Customer
                    .Include(c => c.Person)
                    .Include(c => c.Person.EmailAddress)
                    .Where(c => c.Person.FirstName.ToUpper().Contains(keyword)
                                ||
                                c.Person.LastName.ToUpper().Contains(keyword));
                foreach (var entity in entities)
                {
                    customers.Add(new Customer()
                    {
                        Id = entity.CustomerId,
                        FirstName = entity.Person.FirstName,
                        LastName = entity.Person.LastName,
                        Email = entity.Person.EmailAddress
                            .Select(ea => ea.EmailAddress1)
                            .FirstOrDefault()
                    });
                }
            }
            return customers;
        }

        public Customer GetCustomer(int id)
        {
            using (var dbContext = new Entities.AdventureWorks2017Context())
            {
                Entities.Customer customer = dbContext.Customer
                    .Include(c => c.Person)
                    .Include(c => c.Person.EmailAddress)
                    .Where(c => c.CustomerId == id && c.Person != null)
                    .FirstOrDefault();
                if (customer != null)
                {
                    return new Customer()
                    {
                        Id = customer.CustomerId,
                        FirstName = customer.Person.FirstName,
                        LastName = customer.Person.LastName,
                        Email = customer.Person.EmailAddress
                            .Select(ea => ea.EmailAddress1)
                            .FirstOrDefault()
                    };
                }

            }
            return null;
        }
    }
}
