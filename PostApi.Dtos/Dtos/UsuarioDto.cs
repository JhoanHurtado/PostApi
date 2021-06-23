using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PostApi.Dtos.Dtos
{
    public class UsuarioDto
    {
        public int Id { get; set; }
        [Display(Name = "Nombre"), Required(ErrorMessage = "El nombre es obligatorio")]
        public string Name { get; set; }

        [Display(Name = "E-Mail"), Required(ErrorMessage = "El E-Mail es obligatorio")]
        public string Email { get; set; }

        public string token { get; set; }
        public virtual ICollection<PostDto> GetPosts { get; set; }
    }
}
