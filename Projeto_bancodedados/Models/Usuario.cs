using System.ComponentModel.DataAnnotations;

namespace Projeto_bancodedados.Models
{
    public class Usuario
    {
        [Display(Name = "Código")]
        public int IdUsu { get; set; }
        [Display(Name = "Nome Completo")]
        [Required(ErrorMessage ="O campo nome é obrigatorio")]
        public string nomeUsu { get; set; }
        [Display(Name = "Cargo")]
        [Required(ErrorMessage = "O campo cargo é obrigatorio")]
        public string cargo { get; set; }
        [Display(Name = "Data de Nascimento")]
        [Required(ErrorMessage = "O campo data de nascimento é obrigatorio")]
        public DateTime dataNasc {  get; set; }
    }
}
