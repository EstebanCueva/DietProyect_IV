using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DietProyect_IV.Models
{
    public class CalculoCalorias
    {
        [Key]
        public int CalculoCaloriasId { get; set; }

        [Required]
        public double TasaMetabolicaBasal { get; set; } 

        [Required]
        public double CaloriasDiarias { get; set; } 

        [Required]
        public int UsuarioId { get; set; }

        [ForeignKey("UsuarioId")]
        public Usuario Usuario { get; set; }

        [Required]
        public int NivelActividadId { get; set; }

        [ForeignKey("NivelActividadId")]
        public NivelActividad NivelActividad { get; set; }
    }
}
