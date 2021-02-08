using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ViewModels.Library
{
    public class TextBoxEvent
    {
        public void textKeyPress(KeyPressEventArgs e)
        {
            //Condicion que solo permite ingresar datos de tipo texto
            if (char.IsLetter(e.KeyChar)) { e.Handled = false; }
            //Condicion que permite no dar salto de linea cuando se prome Enter
            else if (e.KeyChar == Convert.ToChar(Keys.Enter)) { e.Handled = true; }
            //Condicion que nos permite utilizr la tecla backspace
            else if (char.IsControl(e.KeyChar)) { e.Handled = false; }
            //Condicion que nos permite utilizar la tecla espacio
            else if (char.IsSeparator(e.KeyChar)) { e.Handled = false; }
            else { e.Handled = true; }
        }

        public void numberKeyPress(KeyPressEventArgs e)
        {
            //Condicion que solo permite ingresar datos de tipo texto
            if (char.IsDigit(e.KeyChar)) { e.Handled = false; }
            //Condicion que permite no dar salto de linea cuando se oprime Enter
            else if (e.KeyChar == Convert.ToChar(Keys.Enter)) { e.Handled = true; }
            //Condicion que no permite ingresar datos de tipo texto
            else if (char.IsLetter(e.KeyChar)) { e.Handled = true; }
            //Condicion que nos permite utilizr la tecla backspace
            else if (char.IsControl(e.KeyChar)) { e.Handled = false; }
            //Condicion que nos permite utilizar la tecla espacio
            else if (char.IsSeparator(e.KeyChar)) { e.Handled = false; }
            else { e.Handled = true; }
        }

        /*public bool IsValidDateTimeTest(string dateTime)
        {
            string[] formats = { "dd/MM/yyyy" };
            DateTime parsedDateTime;
            return DateTime.TryParseExact(dateTime, formats, new CultureInfo("en-US"), DateTimeStyles.None, out parsedDateTime);
        }*/

        public bool comprobarFormatoEmail(string email)
        {
            return new EmailAddressAttribute().IsValid(email);
        }


    }
}
