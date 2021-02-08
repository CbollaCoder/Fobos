using Models.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ViewModels;

namespace SisPhobos
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var login = new LoginVM();
            object[] objects = login.Verificar();
            var listUsuario = (List<TUsuarios>)objects[0];
            if(0 < listUsuario.Count)
            {
                //Application.Run(new Form1(listUsuario));
                Application.Run(new Form1(listUsuario.ToList().Last()));
            }
            else
            {
                Application.Run(new Login());
            }
            
        }
    }
}
