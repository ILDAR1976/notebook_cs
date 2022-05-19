using System.ComponentModel.DataAnnotations.Schema;

namespace notebook.Model;


[Table("records")]
public partial class Record : AbstractBaseEntity
{
 
    [Column("description")]
    public string description { get; set; } = null!;
    
    [Column("date_time")]
    public DateTime dateTime {get; set;}

    [Column("user_id")]
    public int userId {get; set;}
    
    
    public virtual User User { get; set; } = null!;

 
    public Record(): base(null) {
    }
    public Record(int? id): base(id) {} 
}
