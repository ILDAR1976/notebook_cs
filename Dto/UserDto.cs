using notebook.Model;

namespace notebook.Dto;


public partial class UserDto : AbstractNamedEntity
{


    public String email { get; set; } = null!;


    public String password { get; set; } = null!;


    
}
