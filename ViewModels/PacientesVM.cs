using LinqToDB;
using LinqToDB.Mapping;
using Models;
using Models.Conexion;

using Models.Usuario;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using ViewModels.Library;

namespace ViewModels
{
    public class PacientesVM : Conexion
    {
        private List<TextBox> textBoxPaciente;
        private List<TextBox> textBoxPacienteVer;
        private List<TextBox> textBoxPacienteModificar;

        private List<TextBox> textBoxReporte;


        private List<Label> labelPaciente;
        //private TextBoxEvent evento;
        private string _accion = "insert";

        private PictureBox _imagePictureBox;
        private PictureBox _imagePictureBoxVer;
        private PictureBox _imagePictureBoxModificar;
        private Bitmap _imgBitmap;
        private Bitmap _imgBitmapVer;

        private ComboBox _comboBoxCarrera;
        private ComboBox _comboBoxCarreraVer;
        private ComboBox _comboBoxCarreraModificar;
        private ComboBox _comboBoxSemestre;
        private ComboBox _comboBoxSemestreVer;
        private ComboBox _comboBoxSemestreModificar;
        private ComboBox _comboBoxGenero;
        private ComboBox _comboBoxGeneroVer;
        private ComboBox _comboBoxGeneroModificar;

        private static DataGridView _dataGridView, _dataGridView2;
        private NumericUpDown _numericUpDown;
        private Paginador<TClientes> _paginadorClientes;
        private int _reg_por_pagina = 200, _num_pagina = 1;

        private Chart _ChartReportes;

        // Obtener información del Usuario
        //private static List<TUsuarios> _listUsuario;
        private static TUsuarios _dataUsuario;

        //Parametros: public PacientesVM(List<TUsuarios>listUsuario)
        public PacientesVM(TUsuarios dataUsuario)
        {
            //_listUsuario = listUsuario;
            _dataUsuario = dataUsuario;
        }

        ///////////////////////////////////////////////////
        public PacientesVM(object[] objetos, List<TextBox> textBoxPaciente, List<Label> labelPaciente, List<TextBox> textBoxPacienteVer, List<TextBox> textBoxPacienteModificar, List<TextBox> textBoxReporte)
        {
            this.textBoxPaciente = textBoxPaciente;
            this.textBoxPacienteVer = textBoxPacienteVer;
            this.textBoxPacienteModificar = textBoxPacienteModificar;

            this.textBoxReporte = textBoxReporte;

            this.labelPaciente = labelPaciente;

            _imagePictureBox = (PictureBox)objetos[0];
            _imagePictureBoxVer = (PictureBox)objetos[1];
            _imagePictureBoxModificar = (PictureBox)objetos[2];


            _comboBoxCarrera = (ComboBox)objetos[3];
            _comboBoxSemestre = (ComboBox)objetos[4];
            _comboBoxGenero = (ComboBox)objetos[5];

            _comboBoxCarreraVer = (ComboBox)objetos[6];
            _comboBoxSemestreVer = (ComboBox)objetos[7];
            _comboBoxGeneroVer = (ComboBox)objetos[8];


            _comboBoxCarreraModificar = (ComboBox)objetos[9];
            _comboBoxSemestreModificar = (ComboBox)objetos[10];
            _comboBoxGeneroModificar = (ComboBox)objetos[11];


            _imgBitmap = (Bitmap)objetos[12];


            _dataGridView = (DataGridView)objetos[13];
            _numericUpDown = (NumericUpDown)objetos[14];

            _dataGridView2 = (DataGridView)objetos[15];// Ver datos

            _ChartReportes = (Chart)objetos[16];

            restablecer();

            restblecerReport();
            //evento = new TextBoxEvent();
        }

        //REGISTRO DE CLIENTES

        #region
        public void guardarPaciente()
        {
            if (this.textBoxPaciente[0].Text.Equals(""))
            {
                this.labelPaciente[0].Text = "Requerido";
                this.labelPaciente[0].ForeColor = Color.Red;
                this.textBoxPaciente[0].Focus();

            }
            else
            {
                if (this.textBoxPaciente[1].Text.Equals(""))
                {
                    this.labelPaciente[1].Text = "Requerido";
                    this.labelPaciente[1].ForeColor = Color.Red;
                    this.textBoxPaciente[1].Focus();

                }
                else
                {
                    if (this.textBoxPaciente[2].Text.Equals(""))
                    {
                        this.labelPaciente[2].Text = "Requerido";
                        this.labelPaciente[2].ForeColor = Color.Red;
                        this.textBoxPaciente[2].Focus();

                    }
                    else
                    {
                        if (this.textBoxPaciente[3].Text.Equals(""))
                        {
                            this.labelPaciente[3].Text = "Requerido";
                            this.labelPaciente[3].ForeColor = Color.Red;
                            this.textBoxPaciente[3].Focus();

                        }
                        else
                        {
                            if (this.textBoxPaciente[4].Text.Equals(""))
                            {
                                this.labelPaciente[4].Text = "Requerido";
                                this.labelPaciente[4].ForeColor = Color.Red;
                                this.textBoxPaciente[4].Focus();

                            }
                            else
                            {
                                if (_comboBoxCarrera.Text.Equals(""))
                                {
                                    this.labelPaciente[5].Text = "Requerido";
                                    this.labelPaciente[5].ForeColor = Color.Red;
                                    _comboBoxCarrera.Focus();

                                }
                                else
                                {
                                    if (_comboBoxSemestre.Text.Equals(""))
                                    {
                                        this.labelPaciente[6].Text = "Requerido";
                                        this.labelPaciente[6].ForeColor = Color.Red;
                                        _comboBoxSemestre.Focus();

                                    }
                                    else
                                    {
                                        if (_comboBoxGenero.Text.Equals(""))
                                        {
                                            this.labelPaciente[7].Text = "Requerido";
                                            this.labelPaciente[7].ForeColor = Color.Red;
                                            _comboBoxGenero.Focus();

                                        }
                                        else
                                        {
                                            if (this.textBoxPaciente[5].Text.Equals(""))
                                            {
                                                this.labelPaciente[5].Text = "Requerido";
                                                this.labelPaciente[5].ForeColor = Color.Red;
                                                this.textBoxPaciente[5].Focus();

                                            }
                                            else
                                            {
                                                if (this.textBoxPaciente[6].Text.Equals(""))
                                                {
                                                    this.labelPaciente[6].Text = "Requerido";
                                                    this.labelPaciente[6].ForeColor = Color.Red;
                                                    this.textBoxPaciente[6].Focus();

                                                }
                                                else
                                                {
                                                    if (this.textBoxPaciente[7].Text.Equals(""))
                                                    {
                                                        this.labelPaciente[7].Text = "Requerido";
                                                        this.labelPaciente[7].ForeColor = Color.Red;
                                                        this.textBoxPaciente[7].Focus();

                                                    }
                                                    else
                                                    {
                                                        var cliente1 = TClientes.Where(p => p.CI.Equals(this.textBoxPaciente[0].Text)).ToList();
                                                        var cliente2 = TClientes.Where(p => p.Apellido.Equals(this.textBoxPaciente[2].Text)).ToList();
                                                        var list = cliente1.Union(cliente2).ToList();
                                                        switch (_accion)
                                                        {
                                                            case "insert":
                                                                if (list.Count.Equals(0))
                                                                {
                                                                    SaveData();
                                                                }
                                                                else
                                                                {
                                                                    if (0 < cliente1.Count)
                                                                    {
                                                                        this.labelPaciente[0].Text = "El CI ya esta registrado";
                                                                        this.labelPaciente[0].ForeColor = Color.Red;
                                                                        this.textBoxPaciente[0].Focus();
                                                                    }
                                                                    if (0 < cliente2.Count)
                                                                    {
                                                                        this.labelPaciente[2].Text = "El Apellido ya esta registrado";
                                                                        this.labelPaciente[2].ForeColor = Color.Red;
                                                                        this.labelPaciente[2].Focus();
                                                                    }
                                                                }
                                                                break;

                                                            case "update":
                                                                if (list.Count.Equals(2))
                                                                {
                                                                    if (cliente1[0].IdCliente.Equals(_idCliente) &&
                                                                        cliente2[0].IdCliente.Equals(_idCliente))
                                                                    {
                                                                        SaveDataModificar();
                                                                    }
                                                                    else
                                                                    {
                                                                        if (cliente1[0].IdCliente != _idCliente)
                                                                        {
                                                                            this.labelPaciente[0].Text = "El CI ya está registrado";
                                                                            this.labelPaciente[0].ForeColor = Color.Red;
                                                                            this.textBoxPaciente[0].Focus();
                                                                        }

                                                                        /*if(cliente2[0].IdCliente != _idCliente)
                                                                        {
                                                                            this.labelPaciente[2].Text = "El Apellido ya está registrado";
                                                                            this.labelPaciente[2].ForeColor = Color.Red;
                                                                            this.textBoxPaciente[2].Focus();
                                                                        }*/
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    if (list.Count.Equals(0))
                                                                    {
                                                                        SaveDataModificar();
                                                                    }
                                                                    else
                                                                    {
                                                                        if (0 != cliente1.Count)
                                                                        {
                                                                            if (cliente1[0].IdCliente.Equals(_idCliente))
                                                                            {
                                                                                SaveDataModificar();
                                                                            }
                                                                            else
                                                                            {
                                                                                if (cliente1[0].IdCliente != _idCliente)
                                                                                {
                                                                                    this.labelPaciente[0].Text = "El CI ya está registrado";
                                                                                    this.labelPaciente[0].ForeColor = Color.Red;
                                                                                    this.textBoxPaciente[0].Focus();
                                                                                }
                                                                            }
                                                                        }

                                                                        if (0 != cliente2.Count)
                                                                        {

                                                                            if (cliente2[0].IdCliente.Equals(_idCliente))
                                                                            {
                                                                                SaveDataModificar();
                                                                            }
                                                                            else
                                                                            {
                                                                                /*if (cliente2[0].IdCliente != _idCliente)
                                                                                {
                                                                                    this.labelPaciente[2].Text = "El Apellido ya está registrado";
                                                                                    this.labelPaciente[2].ForeColor = Color.Red;
                                                                                    this.textBoxPaciente[2].Focus();
                                                                                }*/
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
                                    }//7
                                }//6

                            }//5
                        }

                    }
                }
            }
        }

        /*private void getId()
        {
            switch (_accion)
            {
                case "insert":

                    /*Datos.Value(u => u.idCliente, cliente.IdCliente)
                        .Insert();
                    break;
            }

        }*/

        private void SaveData()
        {
            BeginTransactionAsync();
            try
            {
                var srcImage = Objects.uploadimage.ResizeImage(_imagePictureBox.Image, 165, 100);
                var image = Objects.uploadimage.ImageToByte(srcImage);
                switch (_accion)
                {
                    case "insert":
                        TClientes.Value(c => c.CI, textBoxPaciente[0].Text)//Insertar CI en la BD
                            .Value(u => u.Nombre, textBoxPaciente[1].Text)//Insertar Nombre en la BD
                            .Value(u => u.Apellido, textBoxPaciente[2].Text)//Insertar Apellido en la BD
                            .Value(u => u.Edad, textBoxPaciente[3].Text)//Insertar Edad en la BD
                            .Value(u => u.FechaNac, textBoxPaciente[4].Text)//Insertar Fecha de Nacimiento en la BD
                            .Value(u => u.Carrera, _comboBoxCarrera.Text)//Insertar Carrera en la BD
                            .Value(u => u.Semestre, _comboBoxSemestre.Text)//Insertar Semestre en la BD
                            .Value(u => u.Genero, _comboBoxGenero.Text)//Insertar Genero en la BD
                            .Value(u => u.Fobia, textBoxPaciente[5].Text)//Insertar Fobia en la BD
                            .Value(u => u.Antecedentes, textBoxPaciente[6].Text)//Insertar Antecedentes en la BD
                            .Value(u => u.Sintomas, textBoxPaciente[7].Text)//Insertar Sintomas en la BD
                            .Value(u => u.Fecha, DateTime.Now.ToString("dd/MMM/yyy"))//Insertar fecha de registro en la BD
                            .Value(u => u.Imagen, image)//Insertar imagen en la BD
                            .Insert();// Consulta de inserción en la BD

                        var cliente = TClientes.ToList().Last();

                        /*Datos.Value(u => u.idCliente, cliente.IdCliente)
                            .Insert();*/

                        /* TReportes_clientes.Value(u => u.UltimoPago, 0)
                             .Value(u => u.FechaPago, "_ _/_ _/_ _")
                             .Value(u => u.DeudaActual, 0)
                             .Value(u => u.FechaDeuda, "_ _/_ _/_ _")
                             .Value(u => u.Ticket, "0000000000")
                             .Value(u => u.FechaLimite, "_ _/_ _/_ _")
                             .Value(u => u.IdCliente, cliente.IdCliente) 
                             .Insert();*/

                        break;

                    case "update":
                        TClientes.Where(u => u.IdCliente.Equals(_idCliente))
                            .Set(u => u.CI, this.textBoxPaciente[0].Text)
                            .Set(u => u.Nombre, this.textBoxPaciente[1].Text)
                            .Set(u => u.Apellido, this.textBoxPaciente[2].Text)
                            .Set(u => u.Edad, this.textBoxPaciente[3].Text)
                            .Set(u => u.FechaNac, this.textBoxPaciente[4].Text)
                            .Set(u => u.Carrera, _comboBoxCarrera.Text)
                            .Set(u => u.Semestre, _comboBoxSemestre.Text)
                            .Set(u => u.Genero, _comboBoxGenero.Text)
                            .Set(u => u.Fobia, this.textBoxPaciente[5].Text)
                            .Set(u => u.Antecedentes, this.textBoxPaciente[6].Text)
                            .Set(u => u.Sintomas, this.textBoxPaciente[7].Text)
                            .Set(u => u.Imagen, image)//Insertar Imagen en la BD
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

        //Guardar Modificar
        public void SaveDataModificar()
        {
            BeginTransactionAsync();
            try
            {
                var srcImage = Objects.uploadimage.ResizeImage(_imagePictureBoxModificar.Image, 165, 100);
                var image = Objects.uploadimage.ImageToByte(srcImage);
                switch (_accion)
                {
                    case "insert":
                        TClientes.Value(c => c.CI, textBoxPacienteModificar[0].Text)//Insertar CI en la BD
                            .Value(u => u.Nombre, textBoxPacienteModificar[1].Text)//Insertar Nombre en la BD
                            .Value(u => u.Apellido, textBoxPacienteModificar[2].Text)//Insertar Apellido en la BD
                            .Value(u => u.Edad, textBoxPacienteModificar[3].Text)//Insertar Edad en la BD
                            .Value(u => u.FechaNac, textBoxPacienteModificar[4].Text)//Insertar Fecha de Nacimiento en la BD
                            .Value(u => u.Carrera, _comboBoxCarreraModificar.Text)//Insertar Carrera en la BD
                            .Value(u => u.Semestre, _comboBoxSemestreModificar.Text)//Insertar Semestre en la BD
                            .Value(u => u.Genero, _comboBoxGeneroModificar.Text)//Insertar Genero en la BD
                            .Value(u => u.Fobia, textBoxPacienteModificar[5].Text)//Insertar Fobia en la BD
                            .Value(u => u.Antecedentes, textBoxPacienteModificar[6].Text)//Insertar Antecedentes en la BD
                            .Value(u => u.Sintomas, textBoxPacienteModificar[7].Text)//Insertar Sintomas en la BD
                            .Value(u => u.Fecha, DateTime.Now.ToString("dd/MMM/yyy"))//Insertar fecha de registro en la BD
                            .Value(u => u.Imagen, image)//Insertar imagen en la BD
                            .Insert();// Consulta de inserción en la BD

                        var cliente = TClientes.ToList().Last();

                        /*Datos.Value(u => u.idCliente, cliente.IdCliente)
                            .Insert();*/

                        /* TReportes_clientes.Value(u => u.UltimoPago, 0)
                             .Value(u => u.FechaPago, "_ _/_ _/_ _")
                             .Value(u => u.DeudaActual, 0)
                             .Value(u => u.FechaDeuda, "_ _/_ _/_ _")
                             .Value(u => u.Ticket, "0000000000")
                             .Value(u => u.FechaLimite, "_ _/_ _/_ _")
                             .Value(u => u.IdCliente, cliente.IdCliente) 
                             .Insert();*/

                        break;

                    case "update":
                        TClientes.Where(u => u.IdCliente.Equals(_idCliente))
                            .Set(u => u.CI, this.textBoxPacienteModificar[0].Text)
                            .Set(u => u.Nombre, this.textBoxPacienteModificar[1].Text)
                            .Set(u => u.Apellido, this.textBoxPacienteModificar[2].Text)
                            .Set(u => u.Edad, this.textBoxPacienteModificar[3].Text)
                            .Set(u => u.FechaNac, this.textBoxPacienteModificar[4].Text)
                            .Set(u => u.Carrera, _comboBoxCarreraModificar.Text)
                            .Set(u => u.Semestre, _comboBoxSemestreModificar.Text)
                            .Set(u => u.Genero, _comboBoxGeneroModificar.Text)
                            .Set(u => u.Fobia, this.textBoxPacienteModificar[5].Text)
                            .Set(u => u.Antecedentes, this.textBoxPacienteModificar[6].Text)
                            .Set(u => u.Sintomas, this.textBoxPacienteModificar[7].Text)
                            .Set(u => u.Imagen, image)//Insertar Imagen en la BD
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

        //Metodo para buscar
        public void SearchClientes(string campo)
        {
            List<TClientes> query = new List<TClientes>();
            int inicio = (_num_pagina - 1) * _reg_por_pagina;
            if (campo.Equals(""))
            {
                query = TClientes.ToList();
            }
            else
            {
                query = TClientes.Where(c => c.CI.StartsWith(campo) || c.Nombre.StartsWith(campo)
                || c.Apellido.StartsWith(campo)).ToList();
            }

            if (0 < query.Count)
            {
                _dataGridView.DataSource = query.Skip(inicio).Take(_reg_por_pagina).ToList();
                _dataGridView.Columns[0].Visible = false;
                _dataGridView.Columns[12].Visible = false;
                _dataGridView.Columns[13].Visible = false;
                _dataGridView.Columns[14].Visible = false;

                _dataGridView.Columns[1].DefaultCellStyle.BackColor = Color.WhiteSmoke;
                _dataGridView.Columns[2].DefaultCellStyle.BackColor = Color.WhiteSmoke;
                _dataGridView.Columns[3].DefaultCellStyle.BackColor = Color.WhiteSmoke;
                _dataGridView.Columns[4].DefaultCellStyle.BackColor = Color.WhiteSmoke;
                _dataGridView.Columns[5].DefaultCellStyle.BackColor = Color.WhiteSmoke;
                _dataGridView.Columns[6].DefaultCellStyle.BackColor = Color.WhiteSmoke;
                _dataGridView.Columns[7].DefaultCellStyle.BackColor = Color.WhiteSmoke;
                _dataGridView.Columns[8].DefaultCellStyle.BackColor = Color.WhiteSmoke;
                _dataGridView.Columns[9].DefaultCellStyle.BackColor = Color.WhiteSmoke;
                _dataGridView.Columns[10].DefaultCellStyle.BackColor = Color.WhiteSmoke;
                _dataGridView.Columns[11].DefaultCellStyle.BackColor = Color.WhiteSmoke;
                //_dataGridView.Columns[12].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            }
            else
            {
                _dataGridView.DataSource = query;
            }

        }

        private int _idCliente = 0;
        public void GetCliente()
        {
            _accion = "update";
            _idCliente = Convert.ToInt16(_dataGridView.CurrentRow.Cells[0].Value);
            this.textBoxPacienteModificar[0].Text = Convert.ToString(_dataGridView.CurrentRow.Cells[1].Value);
            this.textBoxPacienteModificar[1].Text = Convert.ToString(_dataGridView.CurrentRow.Cells[2].Value);
            this.textBoxPacienteModificar[2].Text = Convert.ToString(_dataGridView.CurrentRow.Cells[3].Value);
            this.textBoxPacienteModificar[3].Text = Convert.ToString(_dataGridView.CurrentRow.Cells[4].Value);
            this.textBoxPacienteModificar[4].Text = Convert.ToString(_dataGridView.CurrentRow.Cells[5].Value);
            _comboBoxCarreraModificar.Text = Convert.ToString(_dataGridView.CurrentRow.Cells[6].Value);
            _comboBoxSemestreModificar.Text = Convert.ToString(_dataGridView.CurrentRow.Cells[7].Value);
            _comboBoxGeneroModificar.Text = Convert.ToString(_dataGridView.CurrentRow.Cells[8].Value);
            this.textBoxPacienteModificar[5].Text = Convert.ToString(_dataGridView.CurrentRow.Cells[9].Value);
            this.textBoxPacienteModificar[6].Text = Convert.ToString(_dataGridView.CurrentRow.Cells[10].Value);
            this.textBoxPacienteModificar[7].Text = Convert.ToString(_dataGridView.CurrentRow.Cells[11].Value);

            //this.textBoxPaciente[0].Enabled = false;

            try
            {
                byte[] arrayImage = (byte[])_dataGridView.CurrentRow.Cells[14].Value;
                _imagePictureBox.Image = Objects.uploadimage.byteArrayToImage(arrayImage);
            }
            catch (Exception)
            {
                _imagePictureBox.Image = _imgBitmap;
            }
        }

        //VER PACIENTE
        public void GetClienteVer()
        {
            _accion = "update";
            _idCliente = Convert.ToInt16(_dataGridView.CurrentRow.Cells[0].Value);
            this.textBoxPacienteVer[0].Text = Convert.ToString(_dataGridView.CurrentRow.Cells[1].Value);
            this.textBoxPacienteVer[1].Text = Convert.ToString(_dataGridView.CurrentRow.Cells[2].Value);
            this.textBoxPacienteVer[2].Text = Convert.ToString(_dataGridView.CurrentRow.Cells[3].Value);
            this.textBoxPacienteVer[3].Text = Convert.ToString(_dataGridView.CurrentRow.Cells[4].Value);
            this.textBoxPacienteVer[4].Text = Convert.ToString(_dataGridView.CurrentRow.Cells[5].Value);
            _comboBoxCarreraVer.Text = Convert.ToString(_dataGridView.CurrentRow.Cells[6].Value);
            _comboBoxSemestreVer.Text = Convert.ToString(_dataGridView.CurrentRow.Cells[7].Value);
            _comboBoxGeneroVer.Text = Convert.ToString(_dataGridView.CurrentRow.Cells[8].Value);
            this.textBoxPacienteVer[5].Text = Convert.ToString(_dataGridView.CurrentRow.Cells[9].Value);
            this.textBoxPacienteVer[6].Text = Convert.ToString(_dataGridView.CurrentRow.Cells[10].Value);
            this.textBoxPacienteVer[7].Text = Convert.ToString(_dataGridView.CurrentRow.Cells[11].Value);

            this.textBoxPacienteVer[0].Enabled = false;
            this.textBoxPacienteVer[1].Enabled = false;
            this.textBoxPacienteVer[2].Enabled = false;
            this.textBoxPacienteVer[3].Enabled = false;
            this.textBoxPacienteVer[4].Enabled = false;
            _comboBoxCarreraVer.Enabled = false;
            _comboBoxSemestreVer.Enabled = false;
            _comboBoxGeneroVer.Enabled = false;
            this.textBoxPacienteVer[5].Enabled = false;
            this.textBoxPacienteVer[6].Enabled = false;
            this.textBoxPacienteVer[7].Enabled = false;

            try
            {
                byte[] arrayImage = (byte[])_dataGridView.CurrentRow.Cells[14].Value;
                _imagePictureBoxVer.Image = Objects.uploadimage.byteArrayToImage(arrayImage);
            }
            catch (Exception)
            {
                _imagePictureBoxVer.Image = _imgBitmap;
            }
        }

        //Get Cliente Modificar
        public void GetClienteModificar()
        {
            _accion = "update";
            _idCliente = Convert.ToInt16(_dataGridView.CurrentRow.Cells[0].Value);
            this.textBoxPacienteModificar[0].Text = Convert.ToString(_dataGridView.CurrentRow.Cells[1].Value);
            this.textBoxPacienteModificar[1].Text = Convert.ToString(_dataGridView.CurrentRow.Cells[2].Value);
            this.textBoxPacienteModificar[2].Text = Convert.ToString(_dataGridView.CurrentRow.Cells[3].Value);
            this.textBoxPacienteModificar[3].Text = Convert.ToString(_dataGridView.CurrentRow.Cells[4].Value);
            this.textBoxPacienteModificar[4].Text = Convert.ToString(_dataGridView.CurrentRow.Cells[5].Value);
            _comboBoxCarreraModificar.Text = Convert.ToString(_dataGridView.CurrentRow.Cells[6].Value);
            _comboBoxSemestreModificar.Text = Convert.ToString(_dataGridView.CurrentRow.Cells[7].Value);
            _comboBoxGeneroModificar.Text = Convert.ToString(_dataGridView.CurrentRow.Cells[8].Value);
            this.textBoxPacienteModificar[5].Text = Convert.ToString(_dataGridView.CurrentRow.Cells[9].Value);
            this.textBoxPacienteModificar[6].Text = Convert.ToString(_dataGridView.CurrentRow.Cells[10].Value);
            this.textBoxPacienteModificar[7].Text = Convert.ToString(_dataGridView.CurrentRow.Cells[11].Value);

            this.textBoxPacienteModificar[0].Enabled = false;
            //this.textBoxPacienteModificar[1].Enabled = false;
            //this.textBoxPacienteModificar[2].Enabled = false;
            //this.textBoxPacienteModificar[5].Enabled = false;


            try
            {
                byte[] arrayImage = (byte[])_dataGridView.CurrentRow.Cells[14].Value;
                _imagePictureBoxModificar.Image = Objects.uploadimage.byteArrayToImage(arrayImage);
            }
            catch (Exception)
            {
                _imagePictureBox.Image = _imgBitmap;
            }
        }

        //Restablecer la pagina
        public void restablecer()
        {
            //_accion = "insert";
            _num_pagina = 1;
            _imagePictureBox.Image = _imgBitmap;
            this.textBoxPaciente[0].Text = "";
            this.textBoxPaciente[1].Text = "";
            this.textBoxPaciente[2].Text = "";
            this.textBoxPaciente[3].Text = "";
            this.textBoxPaciente[4].Text = "";
            _comboBoxCarrera.Text = "";
            _comboBoxSemestre.Text = "";
            _comboBoxGenero.Text = "";
            //this.textBoxPaciente[5].Text = "";
            //this.textBoxPaciente[6].Text = "";
            //this.textBoxPaciente[7].Text = "";
            this.textBoxPaciente[5].Text = "";
            this.textBoxPaciente[6].Text = "";
            this.textBoxPaciente[7].Text = "";
            this.labelPaciente[0].Text = "CI*";
            this.labelPaciente[0].ForeColor = Color.LightSlateGray;
            this.labelPaciente[1].Text = "Nombre*";
            this.labelPaciente[1].ForeColor = Color.LightSlateGray;
            this.labelPaciente[2].Text = "Apellido*";
            this.labelPaciente[2].ForeColor = Color.LightSlateGray;
            this.labelPaciente[3].Text = "Edad*";
            this.labelPaciente[3].ForeColor = Color.LightSlateGray;
            this.labelPaciente[4].Text = "Fecha de Nacimiento*";
            this.labelPaciente[4].ForeColor = Color.LightSlateGray;
            this.labelPaciente[5].Text = "Carrera*";
            this.labelPaciente[5].ForeColor = Color.LightSlateGray;
            this.labelPaciente[6].Text = "Semestre*";
            this.labelPaciente[6].ForeColor = Color.LightSlateGray;
            this.labelPaciente[7].Text = "Género*";
            this.labelPaciente[7].ForeColor = Color.LightSlateGray;
            this.labelPaciente[8].Text = "Fobia*";
            this.labelPaciente[8].ForeColor = Color.LightSlateGray;
            this.labelPaciente[9].Text = "Antecedentes*";
            this.labelPaciente[9].ForeColor = Color.LightSlateGray;
            this.labelPaciente[10].Text = "Síntomas*";
            this.labelPaciente[10].ForeColor = Color.LightSlateGray;
            SearchClientes("");
        }

        public void restablecerPacientes_Modificar()
        {
            // _accion = "insert";
            _num_pagina = 1;
            _imagePictureBoxModificar.Image = _imgBitmap;
            this.textBoxPacienteModificar[3].Text = "";
            this.textBoxPacienteModificar[4].Text = "";
            _comboBoxCarreraModificar.Text = "";
            _comboBoxSemestreModificar.Text = "";
            _comboBoxGeneroModificar.Text = "";

            this.textBoxPacienteModificar[6].Text = "";
            this.textBoxPacienteModificar[7].Text = "";

            SearchClientes("");
        }

        #endregion

        //REPORTE DE CLIENTES
        #region

        // GET CLIENTE REPORTE
        public void GetClienteReporte(string ci)
        {

            /* TClientes.Where(u => u.CI.Equals(ci))
                      .Set(u => u.CI, this.textBoxReporte[0].Text)
                      .Set(u => u.Nombre, this.textBoxReporte[1].Text)
                      .Set(u => u.Apellido, this.textBoxReporte[2].Text)
                      .Set(u => u.Edad, this.textBoxReporte[3].Text)
                      .Set(u => u.FechaNac, this.textBoxReporte[4].Text)
                      .Set(u => u.Carrera, this.textBoxReporte[5].Text)
                      .Set(u => u.Semestre, this.textBoxReporte[6].Text)
                      .Set(u => u.Genero, this.textBoxReporte[7].Text)
                      .Set(u => u.Fobia, this.textBoxReporte[8].Text)
                      .Set(u => u.Antecedentes, this.textBoxReporte[9].Text)
                      .Set(u => u.Sintomas, this.textBoxReporte[10].Text).ToString();*/

            var CI = (from i in TClientes
                      where i.CI == ci
                      select new
                      {
                          i.CI
                      }).FirstOrDefault();

            this.textBoxReporte[0].Text = Convert.ToString(CI.CI);

            var Nombre = (from i in TClientes
                          where i.CI == ci
                          select new
                          {
                              i.Nombre
                          }).FirstOrDefault();

            this.textBoxReporte[1].Text = Convert.ToString(Nombre.Nombre);

            var Apellido = (from i in TClientes
                            where i.CI == ci
                            select new
                            {
                                i.Apellido
                            }).FirstOrDefault();

            this.textBoxReporte[2].Text = Convert.ToString(Apellido.Apellido);

            var Edad = (from i in TClientes
                        where i.CI == ci
                        select new
                        {
                            i.Edad
                        }).FirstOrDefault();

            this.textBoxReporte[3].Text = Convert.ToString(Edad.Edad);

            var FechaNac = (from i in TClientes
                            where i.CI == ci
                            select new
                            {
                                i.FechaNac
                            }).FirstOrDefault();

            this.textBoxReporte[4].Text = Convert.ToString(FechaNac.FechaNac);

            var Carrera = (from i in TClientes
                           where i.CI == ci
                           select new
                           {
                               i.Carrera
                           }).FirstOrDefault();

            this.textBoxReporte[5].Text = Convert.ToString(Carrera.Carrera);

            var Semestre = (from i in TClientes
                            where i.CI == ci
                            select new
                            {
                                i.Semestre
                            }).FirstOrDefault();

            this.textBoxReporte[6].Text = Convert.ToString(Semestre.Semestre);

            var Genero = (from i in TClientes
                          where i.CI == ci
                          select new
                          {
                              i.Genero
                          }).FirstOrDefault();

            this.textBoxReporte[7].Text = Convert.ToString(Genero.Genero);

            var Fobia = (from i in TClientes
                         where i.CI == ci
                         select new
                         {
                             i.Fobia
                         }).FirstOrDefault();

            this.textBoxReporte[8].Text = Convert.ToString(Fobia.Fobia);

            var Antecedentes = (from i in TClientes
                                where i.CI == ci
                                select new
                                {
                                    i.Antecedentes
                                }).FirstOrDefault();

            this.textBoxReporte[9].Text = Convert.ToString(Antecedentes.Antecedentes);

            var Sintomas = (from i in TClientes
                            where i.CI == ci
                            select new
                            {
                                i.Sintomas
                            }).FirstOrDefault();

            this.textBoxReporte[10].Text = Convert.ToString(Sintomas.Sintomas);


            var Observaciones = (from i in TClientes
                                 where i.CI == ci
                                 select new
                                 {
                                     i.Observaciones
                                 }).FirstOrDefault();

            this.textBoxReporte[11].Text = Convert.ToString(Observaciones.Observaciones);



            this.textBoxReporte[0].Enabled = false;
            this.textBoxReporte[1].Enabled = false;
            this.textBoxReporte[2].Enabled = false;
            this.textBoxReporte[3].Enabled = false;
            this.textBoxReporte[4].Enabled = false;
            this.textBoxReporte[5].Enabled = false;
            this.textBoxReporte[6].Enabled = false;
            this.textBoxReporte[7].Enabled = false;
            this.textBoxReporte[8].Enabled = false;
            this.textBoxReporte[9].Enabled = false;
            this.textBoxReporte[10].Enabled = false;
            this.textBoxReporte[11].Enabled = false;

        }


        //String valor
        public void GetReportes()
        {
            //int inicio = (_num_pagina - 1) * _reg_por_pagina;

            //if (valor.Equals(""))
            //{
            var query = TClientes.Join(Datos,
                c => c.IdCliente,
                t => t.idC,
                (c, t) => new {
                    c.IdCliente,
                    c.CI,
                    c.Nombre,
                    c.Apellido,
                    t.fecha,
                    t.Hora,
                    t.Escenario,
                    t.Frecuencia,
                    t.Temperatura,
                    t.Miedo
                }).ToList();
            //_dataGridView2.DataSource = query.Skip(inicio).Take(_reg_por_pagina).ToList();
            _dataGridView2.DataSource = query.ToList();
            // }
            /* else
             {
                 var query = TClientes.Join(Datos,
                     c => c.IdCliente,
                     t => t.idC,
                     (c, t) => new
                     {
                         c.IdCliente,
                         c.CI,
                         c.Nombre,
                         c.Apellido,
                         t.fecha,
                         t.frec,
                         t.temp,
                         t.miedo
                     }).Where(c => c.Nombre.StartsWith(valor) || c.Apellido.StartsWith(valor)
                     || c.CI.StartsWith(valor)).ToList();
                 //_dataGridView2.DataSource = query.Skip(inicio).Take(_reg_por_pagina).ToList();
                 _dataGridView2.DataSource = query.ToList();
             }*/

            _dataGridView2.Columns[0].Visible = false;

            _dataGridView2.Columns[1].DefaultCellStyle.BackColor = Color.WhiteSmoke; //CI
            _dataGridView2.Columns[2].DefaultCellStyle.BackColor = Color.WhiteSmoke; // Nombre
            _dataGridView2.Columns[3].DefaultCellStyle.BackColor = Color.WhiteSmoke; //Apellido
            _dataGridView2.Columns[4].DefaultCellStyle.BackColor = Color.WhiteSmoke; //fecha
            _dataGridView2.Columns[5].DefaultCellStyle.BackColor = Color.WhiteSmoke; //hora
            _dataGridView2.Columns[6].DefaultCellStyle.BackColor = Color.WhiteSmoke; //escenario
            _dataGridView2.Columns[7].DefaultCellStyle.BackColor = Color.WhiteSmoke; //frec
            _dataGridView2.Columns[8].DefaultCellStyle.BackColor = Color.WhiteSmoke; //temp
            _dataGridView2.Columns[9].DefaultCellStyle.BackColor = Color.WhiteSmoke; //miedo
        }

        public void restblecerReport()
        {
            GetReportes();
        }


        //BUSCAR EL REPORTE GENERAL DEL PACIENTE 
        public async Task SearchReporteAsync(string campo)
        {
            var query = TClientes.Join(Datos,
                   c => c.IdCliente,
                   t => t.idC,
                   (c, t) => new {
                       c.IdCliente,
                       c.CI,
                       c.Nombre,
                       c.Apellido,
                       t.fecha,
                       t.Hora,
                       t.Escenario,
                       t.Frecuencia,
                       t.Temperatura,
                       t.Miedo
                   }).ToList();

            int inicio = (_num_pagina - 1) * _reg_por_pagina;

            if (campo.Equals(""))
            {
                query = await TClientes.Join(Datos,
                    c => c.IdCliente,
                    t => t.idC,
                    (c, t) => new {
                        c.IdCliente,
                        c.CI,
                        c.Nombre,
                        c.Apellido,
                        t.fecha,
                        t.Hora,
                        t.Escenario,
                        t.Frecuencia,
                        t.Temperatura,
                        t.Miedo
                    }).ToListAsync();
            }
            else
            {
                query = await TClientes.Join(Datos,
                   c => c.IdCliente,
                   t => t.idC,
                   (c, t) => new
                   {
                       c.IdCliente,
                       c.CI,
                       c.Nombre,
                       c.Apellido,
                       t.fecha,
                       t.Hora,
                       t.Escenario,
                       t.Frecuencia,
                       t.Temperatura,
                       t.Miedo
                   }).Where(c => c.Nombre.StartsWith(campo) || c.Apellido.StartsWith(campo)
                   || c.CI.StartsWith(campo)).ToListAsync();
            }

            if (0 < query.Count)
            {
                _dataGridView2.DataSource = query.Skip(inicio).Take(_reg_por_pagina).ToList();

                _dataGridView2.Columns[0].Visible = false;

                _dataGridView2.Columns[1].DefaultCellStyle.BackColor = Color.WhiteSmoke; //CI
                _dataGridView2.Columns[2].DefaultCellStyle.BackColor = Color.WhiteSmoke; // Nombre
                _dataGridView2.Columns[3].DefaultCellStyle.BackColor = Color.WhiteSmoke; //Apellido
                _dataGridView2.Columns[4].DefaultCellStyle.BackColor = Color.WhiteSmoke; //fecha
                _dataGridView2.Columns[5].DefaultCellStyle.BackColor = Color.WhiteSmoke; //hora
                _dataGridView2.Columns[6].DefaultCellStyle.BackColor = Color.WhiteSmoke; //escenario
                _dataGridView2.Columns[7].DefaultCellStyle.BackColor = Color.WhiteSmoke; //frec
                _dataGridView2.Columns[8].DefaultCellStyle.BackColor = Color.WhiteSmoke; //temp
                _dataGridView2.Columns[9].DefaultCellStyle.BackColor = Color.WhiteSmoke; //miedo
            }
            else
            {
                _dataGridView2.DataSource = query;
            }
        }

        // BUSCAR REPORTE POR SESIÓN 
        public async Task SearchReporteSesionAsync(string ci, string fecha)
        {
            var query = TClientes.Join(Datos,
                   c => c.IdCliente,
                   t => t.idC,
                   (c, t) => new {
                       c.IdCliente,
                       c.CI,
                       c.Nombre,
                       c.Apellido,
                       t.fecha,
                       t.Hora,
                       t.Escenario,
                       t.Frecuencia,
                       t.Temperatura,
                       t.Miedo
                   }).ToList();

            int inicio = (_num_pagina - 1) * _reg_por_pagina;

            if (ci.Equals(""))
            {
                query = await TClientes.Join(Datos,
                    c => c.IdCliente,
                    t => t.idC,
                    (c, t) => new {
                        c.IdCliente,
                        c.CI,
                        c.Nombre,
                        c.Apellido,
                        t.fecha,
                        t.Hora,
                        t.Escenario,
                        t.Frecuencia,
                        t.Temperatura,
                        t.Miedo
                    }).ToListAsync();
            }
            else
            {
                query = await TClientes.Join(Datos,
                   c => c.IdCliente,
                   t => t.idC,
                   (c, t) => new
                   {
                       c.IdCliente,
                       c.CI,
                       c.Nombre,
                       c.Apellido,
                       t.fecha,
                       t.Hora,
                       t.Escenario,
                       t.Frecuencia,
                       t.Temperatura,
                       t.Miedo
                   }).Where(c => c.CI.Equals(ci) && c.fecha.Equals(fecha)).ToListAsync();
            }

            if (0 < query.Count)
            {
                _dataGridView2.DataSource = query.Skip(inicio).Take(_reg_por_pagina).ToList();

                _dataGridView2.Columns[0].Visible = false;
                _dataGridView2.Columns[1].Visible = false;
                _dataGridView2.Columns[2].Visible = false;
                _dataGridView2.Columns[3].Visible = false;
                _dataGridView2.Columns[4].Visible = false;
                _dataGridView2.Columns[5].Visible = false;

                _dataGridView2.Columns[1].DefaultCellStyle.BackColor = Color.WhiteSmoke; //CI
                _dataGridView2.Columns[2].DefaultCellStyle.BackColor = Color.WhiteSmoke; // Nombre
                _dataGridView2.Columns[3].DefaultCellStyle.BackColor = Color.WhiteSmoke; //Apellido
                _dataGridView2.Columns[4].DefaultCellStyle.BackColor = Color.WhiteSmoke; //fecha
                _dataGridView2.Columns[5].DefaultCellStyle.BackColor = Color.WhiteSmoke; //hora
                _dataGridView2.Columns[6].DefaultCellStyle.BackColor = Color.WhiteSmoke; //escenario
                _dataGridView2.Columns[7].DefaultCellStyle.BackColor = Color.WhiteSmoke; //frec
                _dataGridView2.Columns[8].DefaultCellStyle.BackColor = Color.WhiteSmoke; //temp
                _dataGridView2.Columns[9].DefaultCellStyle.BackColor = Color.WhiteSmoke; //miedo
            }
            else
            {
                _dataGridView2.DataSource = query;
            }

        }

        //GET PACIENTES - COMBOBOX
        public List<TClientes> getPacientes()
        {
            return TClientes.ToList();
        }

        public object getFecha(int ci) //object
        {
 
            var query = Datos.Where(c => c.idC.Equals(ci))
                                .GroupBy(c => c.fecha)
                                .Where(c => c.Count() > 0)
                                .Select(c => c.Key);

            return query.ToList();
        }


        #endregion

        // LISTADO DE CLIENTES

        #region

        private List<TClientes> listCliente;
        public void Paginador(string metodo)
        {
            switch (metodo)
            {
                case "Primero":
                    if (0 < listCliente.Count)
                        _num_pagina = _paginadorClientes.primero();
                    break;

                case "Anterior":
                    if (0 < listCliente.Count)
                        _num_pagina = _paginadorClientes.anterior();
                    break;

                case "Siguiente":
                    if (0 < listCliente.Count)
                        _num_pagina = _paginadorClientes.siguiente();
                    break;

                case "Ultimo":
                    if (0 < listCliente.Count)
                        _num_pagina = _paginadorClientes.ultimo();
                    break;
            }

            SearchClientes("");
        }

        public void Registro_Paginas()
        {
            _num_pagina = 1;
            _reg_por_pagina = (int)_numericUpDown.Value;
            listCliente = TClientes.ToList();
            if (0 < listCliente.Count)
            {
                _paginadorClientes = new Paginador<TClientes>(listCliente, this.labelPaciente[11], _reg_por_pagina);
                SearchClientes("");
            }
        }

        //Eliminar permanentemente
        public void DeletePaciente(int IdPaciente)
        {
            TClientes.Where(d => d.IdCliente.Equals(_idCliente)).Delete();
        }

        #endregion

    }
}
