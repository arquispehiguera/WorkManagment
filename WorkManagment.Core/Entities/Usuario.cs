using System;

namespace WorkManagment.Core.Entities
{
    public   class Usuario
    {
        public int Dni { get; set; }
        public int IdArea { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
