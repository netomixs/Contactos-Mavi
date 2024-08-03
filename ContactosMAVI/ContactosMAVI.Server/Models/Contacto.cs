 
namespace ContactosMAVI.Server.Models
{
    [Serializable]
    public class Contacto
    {
        public int  Id { get; set; }
        public string  Nombre { get; set; }
        public string  Tipo_Contacto { get; set; }
        public string? Telefono_Fijo { get; set; }
        public string  Telefono_Movil { get; set; }
       
        public DateTime  Nacimiento { get; set; }
        public string  Sexo { get; set; }
        public string  Estado_Civil { get; set; }

    }
}
