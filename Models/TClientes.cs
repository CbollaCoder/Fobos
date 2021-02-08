using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class TClientes
    {
        [PrimaryKey, Identity]
        public int IdCliente { set; get; }
        public string CI { set; get; }
        public string Nombre { set; get; }
        public string Apellido { set; get; }
        public string Edad { set; get; }
        public string FechaNac { set; get; }
        public string Carrera { set; get; }
        public string Semestre { set; get; }
        public string Genero { set; get; }
        public string Fobia { set; get; }
        public string Antecedentes { set; get; }
        public string Sintomas { set; get; }
        public string Observaciones { set; get; }
        public string Fecha { set; get; }
        public byte[] Imagen { set; get; }

    }
}
