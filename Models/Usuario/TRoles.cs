using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Usuario
{
    public class TRoles
    {
        [PrimaryKey, Identity]

        public int IdRol { set; get; }
        public string Rol { set; get; }

    }
}
