using System.ComponentModel.DataAnnotations.Schema;
using notebook.Model;


namespace notebook.Dto;


public partial class RecordDto : AbstractBaseEntity
{
 
    public string description { get; set; } = null!;
    public DateTime dateTime {get; set;}
 
    public RecordDto(): base(null) {
    }
    
}
