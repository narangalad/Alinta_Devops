using Alinta.Api.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerEntities = Alinta.Data.Entities.Customer;


namespace Alinta.Api.Mapping
{
        public class MappingConfiguration : Profile
        {
            /// <summary>
            /// Initialises the mapping configurations between the entity
            /// and the models
            /// </summary>
            public MappingConfiguration()
            {
                CreateMap<Customer, CustomerEntities>();
                CreateMap<CustomerEntities, Customer>();

            }
        }
    
}
