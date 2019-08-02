using FeedBack.Core.Models;

namespace FeedBack.Core.Database.Interfaces
{
    /// <summary>
    /// User Mongodb repository
    /// </summary>
    public interface IUserRepository
    {
        User GetSecureUser(string userName);
    }
}