using LinqToDB;
using LinqToDB.Data;
using Models.Ordenador;
using Models.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Conexion
{
    public class Conexion : DataConnection
    {
        public Conexion() : base("PDHN") {}
        public ITable<TClientes> TClientes { get { return GetTable<TClientes>(); } }
        public ITable<TTratamiento_clientes> TTratamiento_clientes { get { return GetTable<TTratamiento_clientes>(); } }
        public ITable<TUsuarios> TUsuarios => GetTable<TUsuarios>();
        public ITable<Tordenadores> Tordenadores => GetTable<Tordenadores>();

        // Trabaja con ComboBox
        public ITable<TRoles> TRoles => GetTable<TRoles>();
        public ITable<Datos> Datos => GetTable<Datos>();


    }
}
