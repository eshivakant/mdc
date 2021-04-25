using System.Collections.Concurrent;
using System.Collections.Generic;
using MDC.AuthService.Domain;

namespace MDC.AuthService.DataAccess
{
    public class BusinessUsersRepository : IBusinessUsersRepo
    {
        private readonly IDictionary<string, BusinessUser> _db = new ConcurrentDictionary<string, BusinessUser>();
        public BusinessUsersRepository()
        {
            Add(new BusinessUser("user1", "pwd"));
            Add(new BusinessUser("user2", "pwd"));
            Add(new BusinessUser("admin", "admin"));
        }
        public void Add(BusinessUser user)
        {
            _db[user.Login] = user;
        }

        public BusinessUser FindByLogin(string login)
        {
            return _db.TryGetValue(login, out BusinessUser bu) ? bu : null;
        }
    }
}
