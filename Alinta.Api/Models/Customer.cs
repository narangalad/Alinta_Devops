using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alinta.Api.Models
{
    public class Customer
    {
        public Customer(long id, string firstName, string lastName, DateTime dateOfBirth)
        {
            ID = id;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
        }

        public long ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
    }

}
