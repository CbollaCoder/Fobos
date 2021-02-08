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
    //Hereda de la clase Conexion
    public class UsuariosVM : Conexion
    {
        //private List<TUsuarios> _listUsuario;
        private static TUsuarios _dataUsuario;
        private static Label _nombrePerfil;

        private static PictureBox _pictureBoxPerfil;

        private static Bitmap _imgBitmap;
        private static Bitmap _imgBitmapVer;

        private PictureBox _imagePictureBox;
        private PictureBox _imagePictureBoxVer;
        private PictureBox _imagePictureBoxModificar;

        private CheckBox _checkBoxState;
        private CheckBox _checkBoxStateVer;
        private CheckBox _checkBoxStateModificar;

        private List<TextBox> _textBoxUsuario;
        private List<TextBox> _textBoxUsuarioVer;
        private List<TextBox> _textBoxUsuarioModificar;

        private List<Label> _labelUsuario;

        private TextBoxEvent evento;

        //Variable para evaluar la accion que se desea hacer: Agregar, eliminar, actualizar, mostrar
        private string _accion = "insert";

        private ComboBox _comboxRoles;
        private ComboBox _comboxRolesVer;
        private ComboBox _comboBoxRolesModificar;

        private NumericUpDown _numericUpDown;
        private static DataGridView _dataGridView1;
        private int _reg_por_pagina = 20, _num_pagina = 1;
        private Paginador<TUsuarios> _paginadorUsuarios;

        //Funciona con las paginas
        public int _seccion { get; set; } = 1;

        // parámetro: List<TUsuarios> listUsuario,object[] perfil
        public UsuariosVM(TUsuarios dataUsuario, object[] perfil)
        {
            //_listUsuario = listUsuario;
            _dataUsuario = dataUsuario;

            _nombrePerfil = (Label)perfil[0];
            _pictureBoxPerfil = (PictureBox)perfil[1];
            _imgBitmap = (Bitmap)perfil[2];
            Perfil();

        }

        public UsuariosVM(object[] objetos, List<TextBox> textBoxUsuario, List<Label> labelUsuario, List<TextBox> textBoxUsuarioVer, List<TextBox> textBoxUsuarioModificar)
        {
            _textBoxUsuario = textBoxUsuario;
            _textBoxUsuarioVer = textBoxUsuarioVer;
            _textBoxUsuarioModificar = textBoxUsuarioModificar;

            _labelUsuario = labelUsuario;

            _imagePictureBox = (PictureBox)objetos[0];
            _imagePictureBoxVer = (PictureBox)objetos[1];
            _imagePictureBoxModificar = (PictureBox)objetos[2];

            _checkBoxState = (CheckBox)objetos[3];
            _checkBoxStateVer = (CheckBox)objetos[4];
            _checkBoxStateModificar = (CheckBox)objetos[5];

            _comboxRoles = (ComboBox)objetos[6];
            _comboxRolesVer = (ComboBox)objetos[7];
            _comboBoxRolesModificar = (ComboBox)objetos[8];


            _dataGridView1 = (DataGridView)objetos[9]; // Grid para ver
            _numericUpDown = (NumericUpDown)objetos[10];
            evento = new TextBoxEvent();
            restablecer(); // Funciona junto con el ComboBox
            restablecerUsuarios_Modificar();

        }

        private void Perfil()
        {
            //_nombrePerfil.Text = $"{_listUsuario[0].Nombre}";
            _nombrePerfil.Text = $"{_dataUsuario.Nombre}";
            try
            {
                _pictureBoxPerfil.Image = Objects.uploadimage.byteArrayToImage(_dataUsuario.Imagen);
                // _pictureBoxPerfil.Image = Objects.uploadimage.byteArrayToImage(_listUsuario[0].Imagen);
            }
            catch (Exception ex)
            {
                _pictureBoxPerfil.Image = _imgBitmap;
            }
        }

        public void guardarUsuario()
        {
            if (_textBoxUsuario[0].Text.Equals(""))
            {
                _labelUsuario[0].Text = "CI: Requerido";
                _labelUsuario[0].ForeColor = Color.Red;
                _textBoxUsuario[0].Focus();
            }
            else
            {
                if (_textBoxUsuario[1].Text.Equals(""))
                {
                    _labelUsuario[1].Text = "Nombre: Requerido";
                    _labelUsuario[1].ForeColor = Color.Red;
                    _textBoxUsuario[1].Focus();
                }
                else
                {
                    if (_textBoxUsuario[2].Text.Equals(""))
                    {
                        _labelUsuario[2].Text = "Apellido: Requerido";
                        _labelUsuario[2].ForeColor = Color.Red;
                        _textBoxUsuario[2].Focus();
                    }
                    else
                    {
                        if (_textBoxUsuario[3].Text.Equals(""))
                        {
                            _labelUsuario[3].Text = "Email: Requerido";
                            _labelUsuario[3].ForeColor = Color.Red;
                            _textBoxUsuario[3].Focus();
                        }
                        else
                        {
                            if (evento.comprobarFormatoEmail(_textBoxUsuario[3].Text))
                            {
                                if (_textBoxUsuario[4].Text.Equals(""))
                                {
                                    _labelUsuario[4].Text = "Teléfono: Requerido";
                                    _labelUsuario[4].ForeColor = Color.Red;
                                    _textBoxUsuario[4].Focus();
                                }
                                else
                                {
                                    if (_textBoxUsuario[5].Text.Equals(""))
                                    {
                                        _labelUsuario[5].Text = "Dirección: Requerido";
                                        _labelUsuario[5].ForeColor = Color.Red;
                                        _textBoxUsuario[5].Focus();
                                    }
                                    else
                                    {
                                        if (_textBoxUsuario[6].Text.Equals(""))
                                        {
                                            _labelUsuario[6].Text = "Usuario: Requerido";
                                            _labelUsuario[6].ForeColor = Color.Red;
                                            _textBoxUsuario[6].Focus();
                                        }
                                        else
                                        {
                                            if (_textBoxUsuario[7].Text.Equals(""))
                                            {
                                                _labelUsuario[7].Text = "Contraseña: Requerido";
                                                _labelUsuario[7].ForeColor = Color.Red;
                                                _textBoxUsuario[7].Focus();
                                            }
                                            else
                                            {
                                                var usuario1 = TUsuarios.Where(p => p.Nid.Equals(_textBoxUsuario[0].Text)).ToList();
                                                var usuario2 = TUsuarios.Where(p => p.Email.Equals(_textBoxUsuario[3].Text)).ToList();
                                                var list = usuario1.Union(usuario2).ToList();

                                                // switch que evalua qué hacer
                                                switch (_accion)
                                                {
                                                    case "insert":
                                                        if (list.Count.Equals(0))
                                                        {
                                                            SaveData();
                                                        }
                                                        else
                                                        {
                                                            if (0 < usuario1.Count)
                                                            {
                                                                _labelUsuario[0].Text = "El CI ya está registrado";
                                                                _labelUsuario[0].ForeColor = Color.Red;
                                                                _textBoxUsuario[0].Focus();
                                                            }
                                                            if (0 < usuario2.Count)
                                                            {
                                                                _labelUsuario[3].Text = "El email ya está registrado";
                                                                _labelUsuario[3].ForeColor = Color.Red;
                                                                _textBoxUsuario[3].Focus();
                                                            }
                                                        }
                                                        break;

                                                    case "update":
                                                        if (list.Count.Equals(2))
                                                        {
                                                            if(usuario1[0].IdUsuario.Equals(_idUsuario) &&
                                                                usuario2[0].IdUsuario.Equals(_idUsuario))
                                                            {
                                                                SaveDataModificarUsuarios();
                                                            }
                                                            else
                                                            {
                                                                if(usuario1[0].IdUsuario != _idUsuario)
                                                                {
                                                                    _labelUsuario[0].Text = "El CI ya está registrado";
                                                                    _labelUsuario[0].ForeColor = Color.Red;
                                                                    _textBoxUsuario[0].Focus();
                                                                }
                                                                if(usuario2[0].IdUsuario != _idUsuario)
                                                                {
                                                                    _labelUsuario[3].Text = "El Email ya está registrado";
                                                                    _labelUsuario[3].ForeColor = Color.Red;
                                                                    _textBoxUsuario[3].Focus();
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (list.Count.Equals(0))
                                                            {
                                                                SaveDataModificarUsuarios();
                                                            }
                                                            else
                                                            {
                                                                if(0 != usuario1.Count)
                                                                {
                                                                    if (usuario1[0].IdUsuario.Equals(_idUsuario))
                                                                    {
                                                                        SaveDataModificarUsuarios();
                                                                    }
                                                                    else
                                                                    {
                                                                        if(usuario1[0].IdUsuario != _idUsuario)
                                                                        {
                                                                            _labelUsuario[0].Text = "El CI ya está registrado";
                                                                            _labelUsuario[0].ForeColor = Color.Red;
                                                                            _textBoxUsuario[0].Focus();
                                                                        }
                                                                    }
                                                                }

                                                                if(0 != usuario2.Count)
                                                                {
                                                                    if (usuario2[0].IdUsuario.Equals(_idUsuario))
                                                                    {
                                                                        SaveDataModificarUsuarios();
                                                                    }
                                                                    else
                                                                    {
                                                                        if (usuario2[0].IdUsuario != _idUsuario)
                                                                        {
                                                                            _labelUsuario[3].Text = "El Email ya está registrado";
                                                                            _labelUsuario[3].ForeColor = Color.Red;
                                                                            _textBoxUsuario[3].Focus();
                                                                        }

                                                                    }
                                                                }
                                                            }
                                                        }

                                                        break;


                                                }


                                            }
                                        }
                                    }
                                }

                            }
                            else
                            {
                                _labelUsuario[3].Text = "Email inválido";
                                _labelUsuario[3].ForeColor = Color.Red;
                                _textBoxUsuario[3].Focus();
                            }

                        }

                    }
                }
            }
        }

        // Método para insertar datos
        private void SaveData()
        {
            BeginTransactionAsync();
            try
            {
                var encriptar = new Encriptar();
                var srcImage = Objects.uploadimage.ResizeImage(_imagePictureBox.Image, 165, 100);
                var image = Objects.uploadimage.ImageToByte(srcImage);
                switch (_accion)
                {
                    case "insert":
                        TUsuarios.Value(c => c.Nid, _textBoxUsuario[0].Text) //Insertar el CI en la BD
                          .Value(u => u.Nombre, _textBoxUsuario[1].Text)//Insertar el nombre en la BD
                          .Value(u => u.Apellido, _textBoxUsuario[2].Text)//Insertar el apellido en la BD
                          .Value(u => u.Email, _textBoxUsuario[3].Text)//Insertar el email en la BD
                          .Value(u => u.Telefono, _textBoxUsuario[4].Text)//Insertar el teléfono en la BD
                          .Value(u => u.Direccion, _textBoxUsuario[5].Text)//Insertar la dirección en la BD
                          .Value(u => u.Usuario, _textBoxUsuario[6].Text)//Insertar el usuario en la BD
                          .Value(u => u.Password, encriptar.EncryptData(_textBoxUsuario[7].Text, _textBoxUsuario[3].Text))//Insertar la contrseña en la BD
                          .Value(u => u.Estado, _checkBoxState.Checked)//Insertar el estado en la BD
                          .Value(u => u.Rol, _comboxRoles.Text)//Insertar el rol en la BD
                          .Value(u => u.Is_active, true)//Insertar la actividad en la BD
                          .Value(u => u.Fecha, DateTime.Now)//Insertar la fecha de registro en la BD
                          .Value(u => u.Imagen, image)//Insertar la imagen en la BD
                          .Insert();//Consulta de "Inserción" en la BD

                        break;

                    case "update":
                        TUsuarios.Where(u => u.IdUsuario.Equals(_idUsuario))
                          .Set(u => u.Nid, _textBoxUsuario[0].Text)
                          .Set(u => u.Nombre, _textBoxUsuario[1].Text)
                          .Set(u => u.Apellido, _textBoxUsuario[2].Text)
                          .Set(u => u.Email, _textBoxUsuario[3].Text)
                          .Set(u => u.Telefono, _textBoxUsuario[4].Text)
                          .Set(u => u.Direccion, _textBoxUsuario[5].Text)
                          .Set(u => u.Usuario, _textBoxUsuario[6].Text)
                          .Set(u => u.Estado, _checkBoxState.Checked)
                          .Set(u => u.Rol, _comboxRoles.Text)
                          .Set(u => u.Imagen, image)
                          .Update();

                        break;
                }
                CommitTransaction();
                restablecer();
            }
            catch (Exception ex)
            {

                RollbackTransaction();
                MessageBox.Show(ex.Message);
            }
        }

        // Método para insertar datos - MODIFICAR
        private void SaveDataModificarUsuarios()
        {
            BeginTransactionAsync();
            try
            {
                var encriptar = new Encriptar();
                var srcImage = Objects.uploadimage.ResizeImage(_imagePictureBoxModificar.Image, 165, 100);
                var image = Objects.uploadimage.ImageToByte(srcImage);
                switch (_accion)
                {
                    case "insert":
                        TUsuarios.Value(c => c.Nid, _textBoxUsuarioModificar[0].Text) //Insertar el CI en la BD
                          .Value(u => u.Nombre, _textBoxUsuarioModificar[1].Text)//Insertar el nombre en la BD
                          .Value(u => u.Apellido, _textBoxUsuarioModificar[2].Text)//Insertar el apellido en la BD
                          .Value(u => u.Email, _textBoxUsuarioModificar[3].Text)//Insertar el email en la BD
                          .Value(u => u.Telefono, _textBoxUsuarioModificar[4].Text)//Insertar el teléfono en la BD
                          .Value(u => u.Direccion, _textBoxUsuarioModificar[5].Text)//Insertar la dirección en la BD
                          .Value(u => u.Usuario, _textBoxUsuarioModificar[6].Text)//Insertar el usuario en la BD
                          .Value(u => u.Password, encriptar.EncryptData(_textBoxUsuarioModificar[7].Text, _textBoxUsuarioModificar[3].Text))//Insertar la contrseña en la BD
                          .Value(u => u.Estado, _checkBoxStateModificar.Checked)//Insertar el estado en la BD
                          .Value(u => u.Rol, _comboBoxRolesModificar.Text)//Insertar el rol en la BD
                          .Value(u => u.Is_active, true)//Insertar la actividad en la BD
                          .Value(u => u.Fecha, DateTime.Now)//Insertar la fecha de registro en la BD
                          .Value(u => u.Imagen, image)//Insertar la imagen en la BD
                          .Insert();//Consulta de "Inserción" en la BD

                        break;

                    case "update":
                        TUsuarios.Where(u => u.IdUsuario.Equals(_idUsuario))
                          .Set(u => u.Nid, _textBoxUsuarioModificar[0].Text)
                          .Set(u => u.Nombre, _textBoxUsuarioModificar[1].Text)
                          .Set(u => u.Apellido, _textBoxUsuarioModificar[2].Text)
                          .Set(u => u.Email, _textBoxUsuarioModificar[3].Text)
                          .Set(u => u.Telefono, _textBoxUsuarioModificar[4].Text)
                          .Set(u => u.Direccion, _textBoxUsuarioModificar[5].Text)
                          .Set(u => u.Usuario, _textBoxUsuarioModificar[6].Text)
                          .Set(u => u.Estado, _checkBoxStateModificar.Checked)
                          .Set(u => u.Rol, _comboBoxRolesModificar.Text)
                          .Set(u => u.Imagen, image)
                          .Update();

                        break;
                }
                CommitTransaction();
                restablecer();
            }
            catch (Exception ex)
            {

                RollbackTransaction();
                MessageBox.Show(ex.Message);
            }
        }

        //Enlistar Usuarios
        public void SearchUsuarios(string campo)
        {
            List<TUsuarios> query = new List<TUsuarios>();
            int inicio = (_num_pagina - 1) * _reg_por_pagina;
            if (campo.Equals(""))
            {
                query = TUsuarios.ToList();
            }
            else
            {
                query = TUsuarios.Where(c => c.Nid.StartsWith(campo) || c.Nombre.StartsWith(campo) || c.Apellido.StartsWith(campo)).ToList();
            }
            if (0 < query.Count)
            {
                _dataGridView1.DataSource = query.Skip(inicio).Take(_reg_por_pagina).ToList();
                _dataGridView1.Columns[0].Visible = false;
                _dataGridView1.Columns[8].Visible = false;
                _dataGridView1.Columns[10].Visible = false;
                _dataGridView1.Columns[11].Visible = false;
                _dataGridView1.Columns[13].Visible = false;
                _dataGridView1.Columns[1].DefaultCellStyle.BackColor = Color.WhiteSmoke;
                _dataGridView1.Columns[3].DefaultCellStyle.BackColor = Color.WhiteSmoke;
                _dataGridView1.Columns[5].DefaultCellStyle.BackColor = Color.WhiteSmoke;

            }
            else
            {
                _dataGridView1.DataSource = query.Select(c => new
                {
                    c.Nid,
                    c.Nombre,
                    c.Apellido,
                    c.Telefono,
                    c.Direccion,
                    c.Email,
                    c.Usuario,
                    c.Estado
                }).ToList();
            }
        }

        private int _idUsuario = 0;

        public void GetUsuario()
        {
            _accion = "update";
            _idUsuario = Convert.ToInt16(_dataGridView1.CurrentRow.Cells[0].Value);
            _textBoxUsuario[0].Text = Convert.ToString(_dataGridView1.CurrentRow.Cells[1].Value);
            _textBoxUsuario[1].Text = Convert.ToString(_dataGridView1.CurrentRow.Cells[2].Value);
            _textBoxUsuario[2].Text = Convert.ToString(_dataGridView1.CurrentRow.Cells[3].Value);
            _textBoxUsuario[3].Text = Convert.ToString(_dataGridView1.CurrentRow.Cells[6].Value);
            _textBoxUsuario[4].Text = Convert.ToString(_dataGridView1.CurrentRow.Cells[4].Value);
            _textBoxUsuario[5].Text = Convert.ToString(_dataGridView1.CurrentRow.Cells[5].Value);
            _textBoxUsuario[6].Text = Convert.ToString(_dataGridView1.CurrentRow.Cells[7].Value);
            _textBoxUsuario[7].Text = "*********";
            //_textBoxUsuario[7].Enabled = false;

            //Para los campos especiales
            var role = Convert.ToString(_dataGridView1.CurrentRow.Cells[9].Value);
            var roles = new List<TRoles>();
            roles.Add(new TRoles
            {
                Rol = role
            });
            foreach (var item in TRoles.ToList())
            {
                if (!role.Equals(item.Rol))
                {
                    roles.Add(new TRoles
                    {
                        Rol = item.Rol
                    });
                }
            }
            _comboxRoles.DataSource = roles;
            _comboxRoles.ValueMember = "IdRol";
            _comboxRoles.DisplayMember = "Rol";
            try
            {
                byte[] arrayImageV = (byte[])_dataGridView1.CurrentRow.Cells[10].Value;
                _imagePictureBoxVer.Image = Objects.uploadimage.byteArrayToImage(arrayImageV);
            }
            catch (Exception)
            {

                _imagePictureBoxVer.Image = _imgBitmapVer;
            }
            _checkBoxState.Checked = Convert.ToBoolean(_dataGridView1.CurrentRow.Cells[12].Value);
            _checkBoxState.ForeColor = _checkBoxState.Checked ? Color.Green : Color.Red;
        }

        //VER USUARIOS
        public void GetVerUsuario()
        {
            _accion = "update";
            _idUsuario = Convert.ToInt16(_dataGridView1.CurrentRow.Cells[0].Value);
            _textBoxUsuarioVer[0].Text = Convert.ToString(_dataGridView1.CurrentRow.Cells[1].Value);
            _textBoxUsuarioVer[1].Text = Convert.ToString(_dataGridView1.CurrentRow.Cells[2].Value);
            _textBoxUsuarioVer[2].Text = Convert.ToString(_dataGridView1.CurrentRow.Cells[3].Value);
            _textBoxUsuarioVer[3].Text = Convert.ToString(_dataGridView1.CurrentRow.Cells[6].Value);
            _textBoxUsuarioVer[4].Text = Convert.ToString(_dataGridView1.CurrentRow.Cells[4].Value);
            _textBoxUsuarioVer[5].Text = Convert.ToString(_dataGridView1.CurrentRow.Cells[5].Value);
            _textBoxUsuarioVer[6].Text = Convert.ToString(_dataGridView1.CurrentRow.Cells[7].Value);
            _textBoxUsuarioVer[7].Text = "*********";

            //Para los campos especiales
            var roleV = Convert.ToString(_dataGridView1.CurrentRow.Cells[9].Value);
            var rolesV = new List<TRoles>();
            rolesV.Add(new TRoles
            {
                Rol = roleV
            });
            foreach (var item in TRoles.ToList())
            {
                if (!roleV.Equals(item.Rol))
                {
                    rolesV.Add(new TRoles
                    {
                        Rol = item.Rol
                    });
                }
            }
            _comboxRolesVer.DataSource = rolesV;
            _comboxRolesVer.ValueMember = "IdRol";
            _comboxRolesVer.DisplayMember = "Rol";
            try
            {
                byte[] arrayImage = (byte[])_dataGridView1.CurrentRow.Cells[10].Value;
                _imagePictureBoxVer.Image = Objects.uploadimage.byteArrayToImage(arrayImage);
            }
            catch (Exception)
            {

                _imagePictureBox.Image = _imgBitmap;
            }
            _checkBoxStateVer.Checked = Convert.ToBoolean(_dataGridView1.CurrentRow.Cells[12].Value);
            _checkBoxStateVer.ForeColor = _checkBoxState.Checked ? Color.Green : Color.Red;


            _textBoxUsuarioVer[0].Enabled = false;
            _textBoxUsuarioVer[1].Enabled = false;
            _textBoxUsuarioVer[2].Enabled = false;
            _textBoxUsuarioVer[3].Enabled = false;
            _textBoxUsuarioVer[4].Enabled = false;
            _textBoxUsuarioVer[5].Enabled = false;
            _textBoxUsuarioVer[6].Enabled = false;
            _textBoxUsuarioVer[7].Enabled = false;
            _comboxRolesVer.Enabled = false;
            _imagePictureBoxVer.Enabled = false;
            _checkBoxStateVer.Enabled = false;
        }

        // GET USUARIO PARA MODIFICA USUARIOS
        public void GetUsuarioModificar()
        {
            _accion = "update";
            _idUsuario = Convert.ToInt16(_dataGridView1.CurrentRow.Cells[0].Value);
            _textBoxUsuarioModificar[0].Text = Convert.ToString(_dataGridView1.CurrentRow.Cells[1].Value);
            _textBoxUsuarioModificar[1].Text = Convert.ToString(_dataGridView1.CurrentRow.Cells[2].Value);
            _textBoxUsuarioModificar[2].Text = Convert.ToString(_dataGridView1.CurrentRow.Cells[3].Value);
            _textBoxUsuarioModificar[3].Text = Convert.ToString(_dataGridView1.CurrentRow.Cells[6].Value);
            _textBoxUsuarioModificar[4].Text = Convert.ToString(_dataGridView1.CurrentRow.Cells[4].Value);
            _textBoxUsuarioModificar[5].Text = Convert.ToString(_dataGridView1.CurrentRow.Cells[5].Value);
            _textBoxUsuarioModificar[6].Text = Convert.ToString(_dataGridView1.CurrentRow.Cells[7].Value);
            _textBoxUsuarioModificar[7].Text = "*********";
            
            _textBoxUsuarioModificar[0].Enabled = false;
           // _textBoxUsuarioModificar[1].Enabled = false;
           // _textBoxUsuarioModificar[2].Enabled = false;
            
            //Para los campos especiales
            var role = Convert.ToString(_dataGridView1.CurrentRow.Cells[9].Value);
            var roles = new List<TRoles>();
            roles.Add(new TRoles
            {
                Rol = role
            });
            foreach (var item in TRoles.ToList())
            {
                if (!role.Equals(item.Rol))
                {
                    roles.Add(new TRoles
                    {
                        Rol = item.Rol
                    });
                }
            }
            _comboBoxRolesModificar.DataSource = roles;
            _comboBoxRolesModificar.ValueMember = "IdRol";
            _comboBoxRolesModificar.DisplayMember = "Rol";

            try
            {
                byte[] arrayImageV = (byte[])_dataGridView1.CurrentRow.Cells[10].Value;
                _imagePictureBoxModificar.Image = Objects.uploadimage.byteArrayToImage(arrayImageV);
            }
            catch (Exception)
            {

                _imagePictureBoxModificar.Image = _imgBitmapVer;
            }
            _checkBoxStateModificar.Checked = Convert.ToBoolean(_dataGridView1.CurrentRow.Cells[12].Value);
            _checkBoxStateModificar.ForeColor = _checkBoxState.Checked ? Color.Green : Color.Red;
        }


        private List<TUsuarios> listUsuarios;
        // Método para restablecer - Nuevo Registro de Usuario
        public void restablecer()
        {
            _idUsuario = 0;
            //_accion = "insert";
            _num_pagina = 1;
            _textBoxUsuario[0].Text = "";
            _textBoxUsuario[1].Text = "";
            _textBoxUsuario[2].Text = "";
            _textBoxUsuario[3].Text = "";
            _textBoxUsuario[4].Text = "";
            _textBoxUsuario[5].Text = "";
            _textBoxUsuario[6].Text = "";
            _textBoxUsuario[7].Text = "";
            _labelUsuario[0].Text = "Nid";
            _labelUsuario[0].ForeColor = Color.LightSlateGray;
            _labelUsuario[1].Text = "Nombre";
            _labelUsuario[1].ForeColor = Color.LightSlateGray;
            _labelUsuario[2].Text = "Apellido";
            _labelUsuario[2].ForeColor = Color.LightSlateGray;
            _labelUsuario[3].Text = "Email";
            _labelUsuario[3].ForeColor = Color.LightSlateGray;
            _labelUsuario[4].Text = "Teléfono";
            _labelUsuario[4].ForeColor = Color.LightSlateGray;
            _labelUsuario[5].Text = "Dirección";
            _labelUsuario[5].ForeColor = Color.LightSlateGray;
            _labelUsuario[6].Text = "Usuario";
            _labelUsuario[6].ForeColor = Color.LightSlateGray;
            _labelUsuario[7].Text = "Contraseña";
            _labelUsuario[7].ForeColor = Color.LightSlateGray;
            _checkBoxState.Checked = false;
            _imagePictureBox.Image = _imgBitmap;

            _comboxRoles.DataSource = TRoles.ToList(); // Obtener toda la lista de datos
            _comboxRoles.ValueMember = "IdRol"; // Almacenar la ID del Registro
            _comboxRoles.DisplayMember = "Rol"; // Mostrar los roles (nombre de la columna)
            SearchUsuarios("");
            listUsuarios = TUsuarios.ToList();
            if (0 < listUsuarios.Count)
            {
                _paginadorUsuarios = new Paginador<TUsuarios>(listUsuarios, _labelUsuario[8], _reg_por_pagina);
            }
        }

        //Método para restablecer - Modificar Registro
        public void restablecerUsuarios_Modificar()
        {
            _idUsuario = 0;
            //_accion = "insert";
            _num_pagina = 1;

            _textBoxUsuarioModificar[3].Text = "";
            _textBoxUsuarioModificar[4].Text = "";
            _textBoxUsuarioModificar[5].Text = "";
            _textBoxUsuarioModificar[6].Text = "";
            _textBoxUsuarioModificar[7].Text = "";
            
            _checkBoxStateModificar.Checked = false;
            _imagePictureBoxModificar.Image = _imgBitmap;

            _comboBoxRolesModificar.DataSource = TRoles.ToList(); // Obtener toda la lista de datos
            _comboBoxRolesModificar.ValueMember = "IdRol"; // Almacenar la ID del Registro
            _comboBoxRolesModificar.DisplayMember = "Rol"; // Mostrar los roles (nombre de la columna)
            SearchUsuarios("");
        }

        public void Paginador(string metodo)
        {
            switch (metodo)
            {
                case "Primero":
                    switch (_seccion)
                    {
                        case 1:
                            if (0 < listUsuarios.Count)
                                _num_pagina = _paginadorUsuarios.primero();
                            break;
                    }
                    break;
                case "Anterior":
                    switch (_seccion)
                    {
                        case 1:
                            if (0 < listUsuarios.Count)
                                _num_pagina = _paginadorUsuarios.anterior();
                            break;
                    }
                    break;
                case "Siguiente":
                    switch (_seccion)
                    {
                        case 1:
                            if (0 < listUsuarios.Count)
                                _num_pagina = _paginadorUsuarios.siguiente();
                            break;
                    }
                    break;
                case "Último":
                    switch (_seccion)
                    {
                        case 1:
                            if (0 < listUsuarios.Count)
                                _num_pagina = _paginadorUsuarios.ultimo();
                            break;
                    }
                    break;
            }
            switch (_seccion)
            {
                case 1:
                    SearchUsuarios("");
                    break;
            }

        }

        public void Registro_Paginas()
        {
            _num_pagina = 1;
            _reg_por_pagina = (int)_numericUpDown.Value;
            switch (_seccion)
            {
                case 1:
                    listUsuarios = TUsuarios.ToList();
                    if(0 < listUsuarios.Count)
                    {
                        _paginadorUsuarios = new Paginador<TUsuarios>(listUsuarios, _labelUsuario[8], _reg_por_pagina);
                        SearchUsuarios("");
                    }
                    break;
            }
        }


        public void DeleteUsuario(int IdUsuario)
        {
            TUsuarios.Where(d => d.IdUsuario.Equals(_idUsuario)).Delete();
        }
    }
}
