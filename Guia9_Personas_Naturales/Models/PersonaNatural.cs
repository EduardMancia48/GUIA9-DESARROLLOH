namespace Guia9_Personas_Naturales.Models
{
    public class PersonaNatural
    {

        public int Id { get; set; }
        //  [Required]
        public string PrimerNombre { get; set; }

        public string? SegundoNombre { get; set; }
        //  [Required]
        public string PrimerApellido { get; set; }
        public string? SegundoApellido { get; set; }
        public string Dui { get; set; }
        public DateTime FechaNacimiento { get; set; }


    }
}
