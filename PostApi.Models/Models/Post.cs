using System;
using System.ComponentModel.DataAnnotations;

namespace PostApi.Models.Models
{
    public class Post
    {
        public int Id { get; set; }
        [Display(Name ="Usuario"),Required(ErrorMessage ="El usuario del post es obligatorio")]
        public int IdUsuario { get; set; }
        [Display(Name ="Imagen")]
        public string Img { get; set; }
        [Display(Name ="Titulo"), Required(ErrorMessage ="El titulo es requerido")]
        public string Titulo { get; set; }

        [Display(Name ="Contenido del post"),Required(ErrorMessage ="Debe agregar un contenido al post"), MinLength(8,ErrorMessage ="El tamaño minimo del post es de 8 caracteres")] 
        public string Content { get; set; }
        public DateTime CreateAt { get; set; }
    }
}