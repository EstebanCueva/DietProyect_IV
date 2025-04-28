using System.ComponentModel.DataAnnotations;

namespace DietProyect_IV.Models
{
    public class Usuario
    {
        [Key]
        public int UsuarioId { get; set; }

        [MaxLength(100)]
        public string Nombre { get; set; }

        [Required]
        public int Edad { get; set; }

        [Required]
        public double PesoKg { get; set; }  // Peso en kilogramos

        [Required]
        public double AlturaCm { get; set; } // Altura en centímetros

        [Required]
        [MaxLength(10)]
        public string Sexo { get; set; } // "Masculino" o "Femenino"
    }
}
