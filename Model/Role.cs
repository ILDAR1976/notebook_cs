using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace notebook.Model {

    [Flags]
    public enum Role
    {
        ROLE_USER = 100000,
        ROLE_ADMIN = 100001
    }

    [Table("user_roles")]
    public partial class UserRole {
        [Column("user_id")]
        public int? userId {get; set;}
    
        [Column("role")]
        public Role role {get; set;}

        public User user {get; set;}
    } 
}