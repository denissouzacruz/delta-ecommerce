using Microsoft.AspNetCore.Identity;

namespace Delta.AppMvc.ViewModel
{
    public class UsuarioViewModel: IdentityUser
    {
        public string Nome { get; set; }
    }
}
