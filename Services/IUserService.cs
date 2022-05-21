using System;
using System.Collections.Generic;
 
namespace notebook.Services
{
    public interface IUserService<IEntity> : IService<IEntity> where IEntity: class {
        IEntity getUserByEmail(string email);
        IEntity getCurrentUser();
    }
}