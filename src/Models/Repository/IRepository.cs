using System.Threading.Tasks;

namespace Phytime.Models
{
    public interface IRepository
    {
        Feed GetFeed(int id);
        User GetUser(int id);
        Task<User> GetUserAsync(string email, string password);
        void Add(User item);
        void Update(User item);
        void Add(Feed item);
        void Update(Feed item);
        Feed GetFeedByUrl(string url);
        User GetUserByEmail(string email);
        Task<User> GetUserByEmailAsync(string email);
        Feed GetFeedIncudeUsers(Feed feed);
        bool FeedContainsUser(Feed feedValue, User userValue);
        void RemoveUserFromFeed(Feed feedValue, User userValue);
        void AddUserToFeed(Feed feedValue, User userValue);
        void Save();
    }
}
