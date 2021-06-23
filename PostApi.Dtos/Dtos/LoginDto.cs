using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PostApi.Dtos.Dtos
{
    public class LoginDto
    {
        [Display(Name = "E-Mail"), Required(ErrorMessage = "El E-Mail es obligatorio")]
        public string Email { get; set; }

        [Display(Name = "Contraseña"), Required(ErrorMessage = "La contraseña es obligatoria"), MinLength(8, ErrorMessage = "La contraseña debe de tener un tamaño minimo de 8 caracteres")]
        public string Password { get; set; }
    }
}
