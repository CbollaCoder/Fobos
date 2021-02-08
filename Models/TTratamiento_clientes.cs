using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class TTratamiento_clientes
    {
        [PrimaryKey, Identity]
        public int IdSesion { set; get; }
        public string CI { set; get; }
        public string Nombre { set; get; }
        public string Apellido { set; get; }
        public string Observaciones { set; get; }
        public string Fecha { set; get; }
        public int IdCliente { set; get; }

        /*[PrimaryKey, Identity]
        public int IdReport { set; get; }
        public Decimal UltimoPago { set; get; }
        public string FechaPago { set; get; }
        public Decimal DeudaActual { set; get; }
        public string FechaDeuda { set; get; }
        public string Ticket { set; get; }
        public string FechaLimite { set; get; }
        public int IdCliente { set; get; }*/

    }
}
