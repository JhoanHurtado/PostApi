using System.ComponentModel.DataAnnotations;

namespace PostApi.Dtos.Dtos
{
    public class UsuarioPostDto
    {
        public int Id { get; set; }
        [Display(Name = "Nombre"), Required(ErrorMessage = "El nombre es obligatorio")]
        public string Name { get; set; }

        [Display(Name = "E-Mail"), Required(ErrorMessage = "El E-Mail es obligatorio")]
        public string Email { get; set; }

        [Display(Name = "Contraseña"), Required(ErrorMessage = "La contraseña es obligatoria"), MinLength(8, ErrorMessage = "La contraseña debe de tener un tamaño minimo de 8 caracteres")]
        public string Password { get; set; }
    }
}
