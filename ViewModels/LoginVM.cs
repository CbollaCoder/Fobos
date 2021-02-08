using LinqToDB;
using Models.Conexion;
using Models.Usuario;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ViewModels.Library;

namespace ViewModels
{
    public class LoginVM : Conexion
    {
        private List<TextBox> _textBox;
        private List<Label> _label;
        List<TUsuarios> listUsuarios;

        public LoginVM() { }

        public LoginVM(List<TextBox> textBox, List<Label> label)
        {
            _textBox = textBox;
            _label = label;
        }
        public object[] Login()
        {
            listUsuarios = new List<TUsuarios>();
            if (_textBox[0].Text.Equals(""))
            {
                _label[0].Text = "Correo Electrónico: Este campo es requerido";
                _label[0].ForeColor = Color.Red;
                _textBox[0].Focus();
            }
            else if (_textBox[1].Text.Equals(""))
            {
                _label[1].Text = "Contraseña: Este campo es requerido";
                _label[1].ForeColor = Color.Red;
                _textBox[1].Focus();
            }
            else
            {
                
                listUsuarios = TUsuarios.Where(u => u.Email.Equals(_textBox[0].Text)).ToList(); // Verificar que el email ya está registrado
                if (!listUsuarios.Count.Equals(0))
                {
                    // Verificar que el usuario está activo
                    if (listUsuarios[0].Estado)
                    {

                        var encriptar = new Encriptar();
                        String password = encriptar.DecryptData(listUsuarios[0].Password, _textBox[0].Text);
                        //pass.Equals(_textBox[1].Text)
                        //listUsuarios[0].Password.Equals(_textBox[1].Text)
                        if (password.Equals(_textBox[1].Text))
                        {
                            BeginTransactionAsync();
                            try
                            {
                                var hdd = Ordenador.Serial();
                                TUsuarios.Where(u => u.IdUsuario.Equals(listUsuarios[0].IdUsuario))
                                    .Set(u => u.Is_active, true)
                                    .Update();

                                var dataOrdenador = Tordenadores.Where(u => u.Ordenador.Equals(hdd)).ToList();
                                if (dataOrdenador.Count.Equals(0))
                                {
                                    Tordenadores.Value(u => u.Ordenador, hdd)
                                        .Value(u => u.Is_active, true)
                                        .Value(u => u.Usuario, listUsuarios[0].Email)
                                        .Value(u => u.InFecha, DateTime.Now)
                                        .Value(u => u.IdUsuario, listUsuarios[0].IdUsuario)
                                        .Insert();
                                }
                                else
                                {
                                    Tordenadores.Where(u => u.IdOrdenador.Equals(dataOrdenador[0].IdOrdenador))
                                        .Set(u => u.Is_active, true)
                                        .Set(u => u.Usuario, listUsuarios[0].Email)
                                        .Set(u => u.InFecha, DateTime.Now)
                                        .Update();

                                }
                                CommitTransaction();
                            }
                            catch (Exception ex)
                            {
                                RollbackTransaction();
                                MessageBox.Show(ex.Message);

                            }

                        }
                        else
                        {
                            _label[1].Text = "Contraseña incorrecta";
                            _label[1].ForeColor = Color.Red;
                            _textBox[1].Focus();
                            listUsuarios.Clear();
                        }
    
                    }
                    else
                    {
                        listUsuarios.Clear();
                        MessageBox.Show("El usuario no está disponible", "Estado",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    _label[0].Text = "El email no está registrado";
                    _label[0].ForeColor = Color.Red;
                    _textBox[0].Focus();
                    listUsuarios.Clear();
                }
         
            }

            object[] objects = { listUsuarios };
            return objects;
        }

        public object[] Verificar()
        {
            listUsuarios = new List<TUsuarios>();
            var hdd = Ordenador.Serial();
            var dataOrdenador = Tordenadores.Where(u => u.Ordenador.Equals(hdd) && u.Is_active == true).ToList();
            if (!dataOrdenador.Count.Equals(0))
            {
                //u => u.Email.Equals(dataOrdenador[0].Usuario)
                //u => u.Email.Equals(dataOrdenador[0].Usuario) && u.Estado == true
                listUsuarios = TUsuarios.Where(u => u.Email.Equals(dataOrdenador[0].Usuario)).ToList();
            }
            object[] objects = { listUsuarios };
            return objects;
        }

        public void Close()
        {
            listUsuarios = new List<TUsuarios>();
            BeginTransactionAsync();
            try
            {
                var hdd = Ordenador.Serial();
                var dataOrdenador = Tordenadores.Where(u => u.Ordenador.Equals(hdd)).ToList();
                listUsuarios = TUsuarios.Where(u => u.Email.Equals(dataOrdenador[0].Usuario)).ToList();

                TUsuarios.Where(u => u.IdUsuario.Equals(listUsuarios[0].IdUsuario))
                    .Set(u => u.Is_active, false)
                    .Update();

                Tordenadores.Where(u => u.IdOrdenador.Equals(dataOrdenador[0].IdOrdenador))
                    .Set(u => u.Is_active, false)
                    .Set(u => u.OutFecha, DateTime.Now)
                    .Update();

                CommitTransaction();
                Application.Exit();
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                MessageBox.Show(ex.Message);
            }
        }
    }
}
