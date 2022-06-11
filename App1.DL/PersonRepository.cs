using App1.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App1.DL
{
    public class PersonRepository : IPersonRepository
    {
        public PersonRepository()
        {

        }
        public Task Add(Person person)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Person>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Person>> GetAllByAge(int Year)
        {
            throw new NotImplementedException();
        }
    }
}