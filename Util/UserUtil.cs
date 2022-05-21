using System;
using Microsoft.AspNetCore.Authorization;

using notebook.Dto;
using notebook.Model;

namespace notebook.Util {

    public class UserUtil {

        public static User getUserFromUserDto(UserDto userDto) {
            UserRole userRole = new UserRole { 
                userId = userDto.id,
                role = Role.ROLE_USER
            };
            
            List<UserRole> userRoles = new List<UserRole> {
                userRole
            }; 
            
            User user = new User();

            user.id = userDto.id;
            user.name = userDto.name;
            user.email = userDto.email;
            user.password = userDto.password;
            user.enabled = true;
            user.registered = DateTime.Now;
            user.roles = userRoles;
            
            return user;
        }

        public static User getUserFromUserDtoAndRepositoryUser(UserDto userDto,User repositoryUser) {
                  
            User user = new User();

            user.id = userDto.id;
            user.name = userDto.name;
            user.email = userDto.email;
            user.password = userDto.password;
            user.enabled = repositoryUser.enabled;
            user.registered = repositoryUser.registered;
            user.roles = repositoryUser.roles;
            
            return user;
        }

    }


}