using System.Collections.Generic;
using System.Threading.Tasks;
using App1.Models;

namespace App1.DL
{
    public interface IPersonRepository
    {
        Task Add(Person person);

        Task<IEnumerable<Person>> GetAll();

        Task<IEnumerable<Person>> GetAllByAge(int Age);
    }
}