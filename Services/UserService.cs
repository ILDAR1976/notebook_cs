using System;
using System.Linq;
using System.Collections.Generic;
using notebook.Model;
using notebook.Config;
using notebook.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace notebook.Services
{
    public class UserService : IUserService<User> {

        private readonly DbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private EFGenericRepository<User> userRepository;
        private EFGenericRepository<UserRole> userRoleRepository;

        public UserService(
            DbContext context,
            IHttpContextAccessor httpContextAccessor
        ) {
             _context = context;
             _httpContextAccessor = httpContextAccessor;
             userRepository = new EFGenericRepository<User>(this._context);
             userRoleRepository = new EFGenericRepository<UserRole>(this._context);
        }

        public void Create(User item) {
            if (item.id == null) {
                item.password.Replace("{noop}","").Replace("{bcrypt}","");
                item.password = "{bcrypt}" + BCrypt.Net.BCrypt.HashPassword(item.password);
                User user = new User(null, item.name ?? String.Empty, item.email, item.password, item.roles);
                userRepository.Create(user);        
                _context.SaveChanges();
                    List<UserRole> userRoles = (List<UserRole>)user.roles;
                    userRoles.ForEach(x => {
                        x.userId = user.id;
                        userRoleRepository.Create(x);
                    });
            } else {
                int id = item.id ?? 0;
                if (id == 0) return;
                User user = userRepository.FindById(id);
                user.email = item.email;
                item.password.Replace("{noop}","").Replace("{bcrypt}","");
                item.password = "{bcrypt}" + BCrypt.Net.BCrypt.HashPassword(item.password);
                user.password = item.password;
                user.enabled = item.enabled;
                user.name = item.name;
                user.registered = item.registered;
                user.roles = item.roles;
                user.Records = item.Records;
                userRepository.Update(user);
            } 
            _context.SaveChanges();
        }
        public User FindById(int id){
            return userRepository.FindById(id);
        }
        public IEnumerable<User> Get(){
            getCurrentUser();    
            var userRoles = userRoleRepository.Get();
            var users = userRepository.Get();

            var userList = (from u in users
                        join ur in userRoles on u.id equals ur.userId 
                        into role
                        orderby u.id
                        select new User {
                            id = u.id,
                            name = u.name,
                            email = u.email,
                            password = String.Empty,
                            registered = u.registered,
                            roles = role.ToList()
                        }).ToList();
            return userList;
        }
        public void Remove(User item){
            userRepository.Remove(item);
            _context.SaveChanges();
        }

        public User getUserByEmail(string email) {
            User user = userRepository.GetWithInclude(x=>x.email.Equals(email)).ToList()[0]; 
            user.roles = userRoleRepository.Get().Where(x => x.userId == user.id).ToList();  
            return user;
        }

        public User getCurrentUser() {
            var email = _httpContextAccessor.HttpContext.User.Identity.Name;
            User user = userRepository.GetWithInclude(x=>x.email.Equals(email)).ToList()[0]; 
            user.roles = userRoleRepository.Get().Where(x => x.userId == user.id).ToList();  
            return user;
        }
    }
}