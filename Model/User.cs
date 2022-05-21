using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace notebook.Model;

[Table("users")]
public partial class User : AbstractNamedEntity
{

    [Column("email")]
    public String email { get; set; } = null!;

    [Column("password")]
    public String password { get; set; } = null!;

    [Column("enabled")]
    public bool? enabled {get; set;}

    [Column("registered")]
    public DateTime? registered {get; set;}
    
    [NotMapped]
    public ICollection<UserRole> roles {get; set;}       
    
    public virtual ICollection<Record> Records { get; set; }

    
    public User(): base(null, null){}

    public User(int? id, String? name):base(id, name) {}

    public User(User u) : this(u.id, u.name, u.email, u.password, u.enabled, u.registered, u.roles) {}
 
    public User(int? id, String name, String email, String password, ICollection<UserRole> roles) :
        this(id, name, email, password, true, DateTime.Now, roles) {}
 
    public User(int? id, String? name, String? email, String? password, bool? enabled, DateTime? registered, ICollection<UserRole> roles) 
        :base(id, name)
    {
        this.Records = new HashSet<Record>();
        this.email = email;
        this.password = password;
        this.enabled = enabled;
        this.registered = registered;
        this.roles = roles;
    }

}
