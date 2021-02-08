using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Datos
    {
        [PrimaryKey, Identity]
        public int id { set; get; }
        public string fecha { set; get; }
        public string Hora { set; get; }
        public string Escenario { set; get; }
        public string Frecuencia { set; get; }
        public string Temperatura { set; get; }
        public string Miedo { set; get; }
        public int idC { set; get; }

    }
}
