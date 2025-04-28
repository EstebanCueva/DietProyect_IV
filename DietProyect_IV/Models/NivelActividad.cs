using System.ComponentModel.DataAnnotations;

namespace DietProyect_IV.Models
{
    public class NivelActividad
    {
        [Key]
        public int NivelActividadId { get; set; }

        [MaxLength(50)]
        public string Descripcion { get; set; } 
        [Required]
        public double FactorActividad { get; set; } 
    }
}
