using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Alinta.Data.Entities
{
    [Table("Customer")]
    public class Customer
    {
        public Customer()
        {

        }


        public Customer(long id, string firstName, string lastName, DateTime dateOfBirth)
        {
            ID = id;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
        }

        public long ID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Updates The Properties of the Entity
        /// </summary>
        /// <param ID="Id"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="dateOfBirth"></param>
        public void UpdateProperties(long id, string? firstName, string? lastName, DateTime? dateOfBirth)
        {
            ID = id;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
        }
    }
}
