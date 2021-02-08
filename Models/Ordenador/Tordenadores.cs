using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Ordenador
{
    public class Tordenadores
    {
        [PrimaryKey, Identity]

        public int IdOrdenador { set; get; }
        public string Ordenador { set; get; }
        public bool Is_active { set; get; }
        public string Usuario { set; get; }
        public DateTime InFecha { set; get; }
        public DateTime OutFecha { set; get; }
        public int IdUsuario { set; get; }

    }
}
