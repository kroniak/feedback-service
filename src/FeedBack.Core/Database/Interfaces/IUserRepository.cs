using System.Threading.Tasks;
using FeedBack.Core.Models;

namespace FeedBack.Core.Database.Interfaces
{
    /// <summary>
    /// User Mongodb repository
    /// </summary>
    public interface IUserRepository
    {
        Task<User> GetSecureUserAsync(string userName);
    }
}