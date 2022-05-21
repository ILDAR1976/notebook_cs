using System;
using System.Collections.Generic;
 
namespace notebook.Services
{
    public interface IService<IEntity> where IEntity: class
    {
        void Create(IEntity item);
        IEntity FindById(int id);
        IEnumerable<IEntity> Get();
        void Remove(IEntity item);
        
    }
}