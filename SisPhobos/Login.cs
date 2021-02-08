using Models.Usuario;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ViewModels;

namespace SisPhobos
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private LoginVM login;

        private void buttonIniciar_Click(object sender, EventArgs e)
        {
            var textBox = new List<TextBox>
            {
                textBoxUsuario,
                textBoxPassword
            };
            var label = new List<Label>
            {
                labelUsuario,
                labelPassword
            };

            login = new LoginVM(textBox, label);
            object[] objects = login.Login();
            var listUsuario = (List<TUsuarios>)objects[0];
            if( 0 < listUsuario.Count)
            {
                //Form1 form1 = new Form1(listUsuario);
                Form1 form1 = new Form1(listUsuario.ToList().Last());
                form1.Show();
                Visible = false;
            }

        }

        private void textBoxUsuario_TextChanged(object sender, EventArgs e)
        {
            if (textBoxUsuario.Text.Equals(""))
            {
                labelUsuario.ForeColor = Color.LightSlateGray;
            }
            else
            {
                labelUsuario.Text = "Correo Electrónico";
                labelUsuario.ForeColor = Color.Green;
            }
        }

        private void textBoxPassword_TextChanged(object sender, EventArgs e)
        {
            if (textBoxPassword.Text.Equals(""))
            {
                labelPassword.ForeColor = Color.LightSlateGray;
            }
            else
            {
                labelPassword.Text = "Contraseña";
                labelPassword.ForeColor = Color.Green;
            }
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
