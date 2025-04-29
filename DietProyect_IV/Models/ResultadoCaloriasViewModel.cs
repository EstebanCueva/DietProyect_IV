namespace DietProyect_IV.Models
{
    public class ResultadoCaloriasViewModel
    {
        public string Nombre { get; set; }
        public double PesoKg { get; set; }
        public double AlturaCm { get; set; }
        public int Edad { get; set; }
        public string Sexo { get; set; }
        public string NivelActividad { get; set; }
        public string Objetivo { get; set; }


        public double TMB { get; set; }  
        public double CaloriasDiarias { get; set; }  
        public double CaloriasObjetivo { get; set; }  
        public int AjusteCalorias { get; set; }  

        public CalculadoraViewModel CalculadoraModel { get; set; }
    }
}
