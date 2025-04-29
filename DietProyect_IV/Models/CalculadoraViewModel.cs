using System.ComponentModel.DataAnnotations;

namespace DietProyect_IV.Models
{
    public class CalculadoraViewModel
    {
        [Display(Name = "Nombre (opcional)")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El peso es obligatorio")]
        [Display(Name = "Peso actual (kg)")]
        [Range(20, 300, ErrorMessage = "El peso debe estar entre 20 y 300 kg")]
        public double PesoKg { get; set; }

        [Required(ErrorMessage = "La altura es obligatoria")]
        [Display(Name = "Altura (cm)")]
        [Range(100, 250, ErrorMessage = "La altura debe estar entre 100 y 250 cm")]
        public double AlturaCm { get; set; }

        [Required(ErrorMessage = "La edad es obligatoria")]
        [Display(Name = "Edad (años)")]
        [Range(12, 100, ErrorMessage = "La edad debe estar entre 12 y 100 años")]
        public int Edad { get; set; }

        [Required(ErrorMessage = "El género es obligatorio")]
        [Display(Name = "Género")]
        public string Sexo { get; set; }

        [Required(ErrorMessage = "El nivel de actividad es obligatorio")]
        [Display(Name = "Nivel de actividad")]
        public int NivelActividadId { get; set; }

        [Required(ErrorMessage = "El objetivo es obligatorio")]
        [Display(Name = "Tu objetivo")]
        public string Objetivo { get; set; }
    }
}
