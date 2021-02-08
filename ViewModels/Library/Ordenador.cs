using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Library
{
    public class Ordenador
    {
        public static string Serial()
        {
            //Obtener la ID del Ordenador para identificarlo
            string HDD = Environment.CurrentDirectory.Substring(0, 1);

            //UNIDAD DE DISCO DURO
            ManagementObject disk = new ManagementObject("win32_logicaldisk.deviceid=\"" + HDD + ":\"");
            disk.Get();
            //Numero del Disco Duro
            return disk["VolumeSerialNumber"].ToString();
        }
    }
}
