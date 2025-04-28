using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DietProyect_IV.Models
{
    public class ObjetivoCalorico
    {
        [Key]
        public int ObjetivoCaloricoId { get; set; }

        [MaxLength(50)]
        [Required]
        public string TipoObjetivo { get; set; } 

        [Required]
        public int AjusteCalorias { get; set; } 

        [Required]
        public int CalculoCaloriasId { get; set; }

        [ForeignKey("CalculoCaloriasId")]
        public CalculoCalorias CalculoCalorias { get; set; }
    }
}
