using System.ComponentModel.DataAnnotations.Schema;

namespace notebook.Model;
public abstract class AbstractNamedEntity : AbstractBaseEntity {
    
    [Column("name")]
    public String? name {get; set;}


    protected AbstractNamedEntity(){}
    protected AbstractNamedEntity(int? id, String? name): base(id) {
        this.name = name;
    }


}