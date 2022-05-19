using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace notebook.Model;
public abstract class AbstractBaseEntity : IComparable {
    public const int START_SEQ = 100000;

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column(Order=START_SEQ, TypeName="serial")]
    public int? id {get; set;}

    protected AbstractBaseEntity(){}
    protected AbstractBaseEntity(int? id) {
        this.id = id;
    }
    public bool isNew() {
        return this.id == null;
    }

    public override bool Equals( object? obj ){
        AbstractBaseEntity? baseObj = obj as AbstractBaseEntity;
        if (baseObj == null)
            return false;
        else
            return this.id.Equals(baseObj.id);
    }

    public override int GetHashCode(){
        return this.GetHashCode() ^ this.GetHashCode();
    }
    public int CompareTo(object? obj) {
        if (this == obj) return 1;
        if (obj == null || this.GetType().BaseType != obj.GetType().BaseType) return 0;
        AbstractBaseEntity that = (AbstractBaseEntity) obj;
        return (id != null &&  this.Equals(that)) ? 1 : 0;
    }

    public override String ToString() {
        return id != null ? id.ToString() : "";
    }
}