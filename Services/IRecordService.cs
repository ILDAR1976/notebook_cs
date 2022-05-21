using System;
using System.Collections.Generic;
 
namespace notebook.Services
{
    public interface IRecordService<IEntity> : IService<IEntity> where IEntity: class {}
}