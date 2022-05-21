using System;
using System.Linq;
using System.Collections.Generic;
using notebook.Model;
using notebook.Config;
using notebook.Repositories;
using Microsoft.EntityFrameworkCore;

namespace notebook.Services
{
    public class RecordService : IRecordService<Record> {

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly DbContext _context;
        private EFGenericRepository<Record> recordRepository;
        private EFGenericRepository<User> userRepository;

        public RecordService(
            DbContext context,
            IHttpContextAccessor httpContextAccessor
        ) {
             _context = context;
             _httpContextAccessor = httpContextAccessor;
             recordRepository = new EFGenericRepository<Record>(this._context);
             userRepository = new EFGenericRepository<User>(this._context);
        }

        public void Create(Record item) {
            
            if (item.id == null) {
                Record record = new Record(null, item.description, DateTime.UtcNow, getCurrentUserId());
                recordRepository.Create(record);        
            } else {
                recordRepository.Update(item);
            } 
            _context.SaveChanges();
        }
        public Record FindById(int id){
            return recordRepository.FindById(id);
        }
        public IEnumerable<Record> Get(){
            return recordRepository.Get().Where<Record>(x => x.userId == getCurrentUserId());
        }
        public void Remove(Record item){
            recordRepository.Remove(item);
            _context.SaveChanges();
        }


        private int getCurrentUserId() {
            var username = _httpContextAccessor.HttpContext.User.Identity.Name;
            User user = userRepository.GetWithInclude(x=>x.email.Equals(username)).ToList()[0];   
            return (int)user.id;
        }


    }
}