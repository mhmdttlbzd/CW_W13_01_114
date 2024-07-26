namespace CW_W13_01_114.Data
{
    public class UserRepository
    {
        public User Create(User user) { 
            user.Id = Guid.NewGuid();
            AppContext.Users.Add(user);
            return user;
        }
        public List<User> GetAll() { 
            return AppContext.Users;
        }
        public User? GetById(Guid id) { 
            return AppContext.Users.FirstOrDefault(u => u.Id == id);
        }
        public void Update(User user,Guid id) {
            if (id != null && id != Guid.Empty)
            {
                var oldUser = AppContext.Users.FirstOrDefault(u => u.Id == id);
                if (oldUser != null)
                {
                    oldUser.Name = user.Name;
                    oldUser.Email = user.Email;
                    oldUser.Password = user.Password;
                }
            }
        }
        public void Delete(Guid id) {
            var user= AppContext.Users.FirstOrDefault(x => x.Id == id);
            if (user != null) 
            AppContext.Users.Remove(user);
        }

    }
}
