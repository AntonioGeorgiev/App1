using App1.Models;
using System.Threading.Tasks;

namespace App1.BL.Interfaces
{
    public interface IRabbitMqService
    {
        Task PublishPersonAsync(Person person);
    }
}
