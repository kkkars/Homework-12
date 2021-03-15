using DepsWebApp.Models;
using System.Threading.Tasks;

namespace DepsWebApp.Services
{
    public interface IAccountService
    {
        Task<bool> Add(Account newAccount);

        Task<bool> Find(Account newAccount);
    }
}
