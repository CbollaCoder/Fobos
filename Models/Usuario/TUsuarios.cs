using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Usuario
{
    public class TUsuarios
    {
        [PrimaryKey, Identity]
        public int IdUsuario { set; get; }
        public string Nid { set; get; }
        public string Nombre { set; get; }
        public string Apellido { set; get; }
        public string Telefono { set; get; }
        public string Direccion { set; get; }
        public string Email { set; get; }
        public string Usuario { set; get; }
        public string Password { set; get; }
        public string Rol { set; get; }
        public byte[] Imagen { set; get; }
        public bool Is_active { set; get; }
        public bool Estado { set; get; }

        public DateTime Fecha { set; get; }
    }

}
