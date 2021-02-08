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
using ViewModels.Library;
using System.IO.Ports;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Diagnostics;
using System.Data.SqlClient;
using Models;
using Font = System.Drawing.Font;

namespace SisPhobos
{
    public partial class Form1 : Form
    {
        

        private LoginVM login;
        private UsuariosVM usuario;
        private static TUsuarios _dataUsuario;
        public Form1(TUsuarios dataUsuario)
        {
            InitializeComponent();

            // CODIGO DE TRATAMIENTO
            getAvailablePorts(); // Puertos disponibles - TRATAMIENTO



            _dataUsuario = dataUsuario;

            //CODIGO DE PACIENTES
            pacientes = new PacientesVM(dataUsuario);

            dataGridView_Pacientes.EnableHeadersVisualStyles = false;
            dataGridView_Pacientes.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(172, 177, 245);
            dataGridView_Pacientes.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 9, FontStyle.Bold);


            // CODIGO DE USUARIO
            login = new LoginVM();
            object[] perfil =
            {
                labelNombre_Perfil,
                pictureBox_Perfil,
                Properties.Resources.usuarioSis
            };
            usuario = new UsuariosVM(dataUsuario, perfil);


            var textBoxPaciente = new List<TextBox>();
            textBoxPaciente.Add(textBoxPaciente_CI);//0
            textBoxPaciente.Add(textBoxPaciente_Nombre);//1
            textBoxPaciente.Add(textBoxPaciente_Apellido);//2
            textBoxPaciente.Add(textBoxPaciente_Edad);//3
            textBoxPaciente.Add(textBoxPaciente_FechaNac);//4
            textBoxPaciente.Add(textBoxPaciente_Fobia);//5
            textBoxPaciente.Add(textBoxPaciente_Antecedentes);//6
            textBoxPaciente.Add(textBoxPaciente_Sintomas);//7

            var textBoxPacienteVer = new List<TextBox>();
            textBoxPacienteVer.Add(textBoxPacienteVer_CI);//0
            textBoxPacienteVer.Add(textBoxPacienteVer_Nombre);//1
            textBoxPacienteVer.Add(textBoxPacienteVer_Apellido);//2
            textBoxPacienteVer.Add(textBoxPacienteVer_Edad);//3
            textBoxPacienteVer.Add(textBoxPacienteVer_FechaNac);//4
            //textBoxPacienteVer.Add(textBoxPacienteVer_Carrera);//
            //textBoxPacienteVer.Add(textBoxPacienteVer_Semestre);//
            //textBoxPacienteVer.Add(textBoxPacienteVer_Genero);//
            textBoxPacienteVer.Add(textBoxPacienteVer_Fobia);//5
            textBoxPacienteVer.Add(textBoxPacienteVer_Antecedentes);//6
            textBoxPacienteVer.Add(textBoxPacienteVer_Sintomas);//7

            //Modificar
            var textBoxPacienteModificar = new List<TextBox>();
            textBoxPacienteModificar.Add(textBoxPacienteCI_Modificar);//0
            textBoxPacienteModificar.Add(textBoxPacienteNombre_Modificar);//1
            textBoxPacienteModificar.Add(textBoxPacienteApellido_Modificar);//2
            textBoxPacienteModificar.Add(textBoxPacienteEdad_Modificar);//3
            textBoxPacienteModificar.Add(textBoxPacienteFechaNac_Modificar);//4
            textBoxPacienteModificar.Add(textBoxPacienteFobia_Modificar);//5
            textBoxPacienteModificar.Add(textBoxPacienteAntecedentes_Modificar);//6
            textBoxPacienteModificar.Add(textBoxPacienteSintomas_Modificar);//7

            //Obtener Reporte
            var textBoxReporte = new List<TextBox>();
            textBoxReporte.Add(textBoxReporte_CI);//0
            textBoxReporte.Add(textBoxReporte_Nombre);//1
            textBoxReporte.Add(textBoxReporte_Apellido);//2
            textBoxReporte.Add(textBoxReporte_Edad);//3
            textBoxReporte.Add(textBoxReporte_FechaNac);//4
            textBoxReporte.Add(textBoxReporte_Carrera);//5
            textBoxReporte.Add(textBoxReporte_Semestre);//6
            textBoxReporte.Add(textBoxReporte_Genero);//7
            textBoxReporte.Add(textBoxReporte_Fobia);//8
            textBoxReporte.Add(textBoxReporte_Antecedentes);//9
            textBoxReporte.Add(textBoxReporte_Sintomas);//10
            textBoxReporte.Add(textBoxReporte_Observaciones);//11


            var labelPaciente = new List<Label>();
            labelPaciente.Add(labelPaciente_CI);//0
            labelPaciente.Add(labelPaciente_Nombre);//1
            labelPaciente.Add(labelPaciente_Apellido);//2
            labelPaciente.Add(labelPaciente_Edad);//3
            labelPaciente.Add(labelPaciente_FechaNac);//4
            labelPaciente.Add(labelPaciente_Carrera);//5
            labelPaciente.Add(labelPaciente_Semestre);//6
            labelPaciente.Add(labelPaciente_Genero);//7
            labelPaciente.Add(labelPaciente_Fobia);//8
            labelPaciente.Add(labelPaciente_Antecedentes);//9
            labelPaciente.Add(labelPaciente_Sintomas);//10

            labelPaciente.Add(label_PaginasCliente);//11

            object[] objetos = {
                pictureBoxPaciente,
                pictureBoxPaciente_Ver,
                pictureBoxPaciente_Modificar,


                comboBoxPaciente_Carrera,
                comboBoxPaciente_Semestre,
                comboBoxPaciente_Genero,

                comboBoxPaciente_CarreraVer,
                comboBoxPaciente_SemestreVer,
                comboBoxPaciente_GeneroVer,

                comboBoxPacienteCarrera_Modificar,// M
                comboBoxPacienteSemestre_Modificar, // M
                comboBoxPacienteGenero_Modificar, // M

                Properties.Resources.usuarioSis,
                dataGridView_Pacientes, // Para visualizar clientes
                numericUpDown_PaginasCliente,
                dataGridViewCliente_Datos,// Visualiar datos

                chartPacientes_GraficarReportes // GRAFICAR REPORTES
            };

            pacientes = new PacientesVM(objetos, textBoxPaciente, labelPaciente, textBoxPacienteVer, textBoxPacienteModificar, textBoxReporte);

        }

        private bool value = true;

        //Cerrar sesion
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (value)
            {
                if (MessageBox.Show("¿Estás seguro de salir del sistema?", "Cerrar Sesión", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    value = false;
                    login.Close();
                }
                else
                {
                    value = false;
                    Application.Exit();
                }
            }

        }

        // Botones
        private void EnabledButton(Button button)
        {
            buttonPaciente.Enabled = true;
            buttonTratamiento.Enabled = true;
            buttonUsuario.Enabled = true;
            buttonPrincipal_Reportes.Enabled = true;
            button.Enabled = false;

        }

        /************************************
         * 
         * 
         *          CODIGO DEL PACIENTE
         *          
         ************************************/



        #region

        private PacientesVM pacientes;
        private void buttonPaciente_Click(object sender, EventArgs e)
        {

            tabControlPrincipal.SelectedIndex = 0; // Pasamos a la Tab "0" 
            EnabledButton(buttonPaciente);

        }

        //Picture Box
        private void pictureBoxPaciente_Click_1(object sender, EventArgs e)
        {
            Objects.uploadimage.CargarImagen(pictureBoxPaciente);
        }

        private void pictureBoxPaciente_Modificar_Click(object sender, EventArgs e)
        {
            Objects.uploadimage.CargarImagen(pictureBoxPaciente_Modificar);
        }

        // CI 
        private void textBoxPaciente_CI_TextChanged_1(object sender, EventArgs e)
        {
            if (textBoxPaciente_CI.Text.Equals(""))
            {
                labelPaciente_CI.ForeColor = Color.LightSlateGray;

            }
            else
            {
                labelPaciente_CI.Text = "CI*";
                labelPaciente_CI.ForeColor = Color.Green;
            }
        }

        private void textBoxPaciente_CI_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            Objects.eventos.numberKeyPress(e);
        }

        //Nombre
        private void textBoxPaciente_Nombre_TextChanged_1(object sender, EventArgs e)
        {
            if (textBoxPaciente_Nombre.Text.Equals(""))
            {
                labelPaciente_Nombre.ForeColor = Color.LightSlateGray;

            }
            else
            {
                labelPaciente_Nombre.Text = "Nombre*";
                labelPaciente_Nombre.ForeColor = Color.Green;
            }
        }

        private void textBoxPaciente_Nombre_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            Objects.eventos.textKeyPress(e);
        }

        //Apellido
        private void textBoxPaciente_Apellido_TextChanged_1(object sender, EventArgs e)
        {
            if (textBoxPaciente_Apellido.Text.Equals(""))
            {
                labelPaciente_Apellido.ForeColor = Color.LightSlateGray;

            }
            else
            {
                labelPaciente_Apellido.Text = "Apellido*";
                labelPaciente_Apellido.ForeColor = Color.Green;
            }
        }

        private void textBoxPaciente_Apellido_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            Objects.eventos.textKeyPress(e);
        }

        //Edad
        private void textBoxPaciente_Edad_TextChanged_1(object sender, EventArgs e)
        {
            if (textBoxPaciente_Edad.Text.Equals(""))
            {
                labelPaciente_Edad.ForeColor = Color.LightSlateGray;

            }
            else
            {
                labelPaciente_Edad.Text = "Edad*";
                labelPaciente_Edad.ForeColor = Color.Green;
            }
        }

        private void textBoxPaciente_Edad_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            Objects.eventos.numberKeyPress(e);
        }


        //Fecha de Nacimiento
        private void textBoxPaciente_FechaNac_TextChanged_1(object sender, EventArgs e)
        {
            if (textBoxPaciente_FechaNac.Text.Equals(""))
            {
                labelPaciente_FechaNac.ForeColor = Color.LightSlateGray;

            }
            else
            {
                labelPaciente_FechaNac.Text = "Fecha de Nacimiento*";
                labelPaciente_FechaNac.ForeColor = Color.Green;
            }
        }

        private void comboBoxPaciente_Carrera_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxPaciente_Carrera.Text.Equals(""))
            {
                labelPaciente_Carrera.ForeColor = Color.LightSlateGray;

            }
            else
            {
                labelPaciente_Carrera.Text = "Carrera*";
                labelPaciente_Carrera.ForeColor = Color.Green;
            }
        }

        private void comboBoxPaciente_Semestre_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxPaciente_Semestre.Text.Equals(""))
            {
                labelPaciente_Semestre.ForeColor = Color.LightSlateGray;

            }
            else
            {
                labelPaciente_Semestre.Text = "Semestre*";
                labelPaciente_Semestre.ForeColor = Color.Green;
            }
        }

        private void comboBoxPaciente_Genero_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxPaciente_Genero.Text.Equals(""))
            {
                labelPaciente_Genero.ForeColor = Color.LightSlateGray;

            }
            else
            {
                labelPaciente_Genero.Text = "Género*";
                labelPaciente_Genero.ForeColor = Color.Green;
            }
        }

        //Carrera
        /*private void textBoxPaciente_Carrera_TextChanged_1(object sender, EventArgs e)
        {
            if (textBoxPaciente_Carrera.Text.Equals(""))
            {
                labelPaciente_Carrera.ForeColor = Color.LightSlateGray;

            }
            else
            {
                labelPaciente_Carrera.Text = "Carrera*";
                labelPaciente_Carrera.ForeColor = Color.Green;
            }
        }

        private void textBoxPaciente_Carrera_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            Objects.eventos.textKeyPress(e);
        }

        // Semestre
        private void textBoxPaciente_Semestre_TextChanged_1(object sender, EventArgs e)
        {
            if (textBoxPaciente_Semestre.Text.Equals(""))
            {
                labelPaciente_Semestre.ForeColor = Color.LightSlateGray;

            }
            else
            {
                labelPaciente_Semestre.Text = "Semestre*";
                labelPaciente_Semestre.ForeColor = Color.Green;
            }
        }

        private void textBoxPaciente_Semestre_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            Objects.eventos.textKeyPress(e);
        }

        //Genero
        private void textBoxPaciente_Genero_TextChanged_1(object sender, EventArgs e)
        {
            if (textBoxPaciente_Genero.Text.Equals(""))
            {
                labelPaciente_Genero.ForeColor = Color.LightSlateGray;

            }
            else
            {
                labelPaciente_Genero.Text = "Género*";
                labelPaciente_Genero.ForeColor = Color.Green;
            }
        }

        private void textBoxPaciente_Genero_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            Objects.eventos.textKeyPress(e);
        }*/

        // Fobia
        private void textBoxPaciente_Fobia_TextChanged_1(object sender, EventArgs e)
        {
            if (textBoxPaciente_Fobia.Text.Equals(""))
            {
                labelPaciente_Fobia.ForeColor = Color.LightSlateGray;

            }
            else
            {
                labelPaciente_Fobia.Text = "Fobia*";
                labelPaciente_Fobia.ForeColor = Color.Green;
            }
        }

        // Antecedentes
        private void textBoxPaciente_Antecedentes_TextChanged(object sender, EventArgs e)
        {
            if (textBoxPaciente_Antecedentes.Text.Equals(""))
            {
                labelPaciente_Antecedentes.ForeColor = Color.LightSlateGray;

            }
            else
            {
                labelPaciente_Antecedentes.Text = "Antecedentes*";
                labelPaciente_Antecedentes.ForeColor = Color.Green;
            }
        }

        // Síntomas
        private void textBoxPaciente_Sintomas_TextChanged_1(object sender, EventArgs e)
        {
            if (textBoxPaciente_Sintomas.Text.Equals(""))
            {
                labelPaciente_Sintomas.ForeColor = Color.LightSlateGray;

            }
            else
            {
                labelPaciente_Sintomas.Text = "Síntomas*";
                labelPaciente_Sintomas.ForeColor = Color.Green;
            }
        }

        //Botón Agregar
        private void buttonPaciente_Agregar_Click_1(object sender, EventArgs e)
        {
            pacientes.guardarPaciente();
            tabControlSecundario_Pacientes.SelectedIndex = 0;

            labelPacientes_RegistroNuevo.Text = "";
            labelPaciente_VerRegistro.Text = "";
            labelPaciente_ModificarRegistro.Text = "";
        }

        //Botón Agregar - Modificar
        private void buttonPacienteGuardar_Modificar_Click(object sender, EventArgs e)
        {
            pacientes.SaveDataModificar();
            tabControlSecundario_Pacientes.SelectedIndex = 0;

            labelPacientes_RegistroNuevo.Text = "";
            labelPaciente_VerRegistro.Text = "";
            labelPaciente_ModificarRegistro.Text = "";
        }

        //Botón Restablecer - Nuevo Registro
        private void buttonPaciente_Cancelar_Click_1(object sender, EventArgs e)
        {
            pacientes.restablecer();
            //dataGridView_Pacientes.ClearSelection();
            //tabControlSecundario_Pacientes.SelectedIndex = 0;
        }

        //Botón Restablecer - Modificar Registro
        private void buttonPacienteRestablecer_Modificar_Click(object sender, EventArgs e)
        {
            pacientes.restablecerPacientes_Modificar();
            //dataGridView_Pacientes.ClearSelection();
            //tabControlSecundario_Pacientes.SelectedIndex = 0;
        }

        //DataGridView
        private void dataGridView_Pacientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView_Pacientes.Rows.Count != 0)
                pacientes.GetCliente();
                pacientes.GetClienteVer();
                pacientes.GetClienteModificar();
        }

        private void dataGridView_Pacientes_KeyUp(object sender, KeyEventArgs e)
        {
            if (dataGridView_Pacientes.Rows.Count != 0)
                pacientes.GetCliente();
        }

        private void buttonCliente_Pagina1_Click(object sender, EventArgs e)
        {
            pacientes.Paginador("Primero");
        }

        private void buttonCliente_Pagina2_Click(object sender, EventArgs e)
        {
            pacientes.Paginador("Anterior");
        }

        private void buttonCliente_Pagina3_Click(object sender, EventArgs e)
        {
            pacientes.Paginador("Siguiente");
        }

        private void buttonClientePagina_4_Click(object sender, EventArgs e)
        {
            pacientes.Paginador("Último");
        }

        private void numericUpDown_PaginasCliente_ValueChanged(object sender, EventArgs e)
        {
            pacientes.Registro_Paginas();
        }

        private void textBox1Paciente_Buscar_TextChanged(object sender, EventArgs e)
        {
            pacientes.SearchClientes(textBox1Paciente_Buscar.Text);
        }


        private void tabControlCliente1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControlCliente1.SelectedIndex)
            {
                case 0:
                    tabControlCliente2.SelectedIndex = 0;
                    break;
                case 1:
                    tabControlCliente2.SelectedIndex = 1;

                    break;
            }
        }

        private void tabControlCliente2_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControlCliente2.SelectedIndex)
            {
                case 0:
                    tabControlCliente1.SelectedIndex = 0;
                    break;
                case 1:
                    tabControlCliente1.SelectedIndex = 1;
                    break;
            }

        }

        /*private void buttonPacienteCancelar_Click(object sender, EventArgs e)
        {
            /*if (MessageBox.Show("¿Desea eliminar de forma permanente?", "Eliminar Paciente", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                pacientes.DeletePaciente(pacientes.ID);
                pacientes.restablecer();
                MessageBox.Show("Paciente eliminado");
            }
            tabControlSecundario_Pacientes.SelectedIndex = 0;
        }*/

        //Botón para volver al menú principal
        private void buttonPacienteCancelar_Click_1(object sender, EventArgs e)
        {
            tabControlSecundario_Pacientes.SelectedIndex = 0;

            labelPacientes_RegistroNuevo.Text = "";
            labelPaciente_VerRegistro.Text = "";
            labelPaciente_ModificarRegistro.Text = "";
        }

        //Crear nuevo paciente - MENU PRINCIPAL
        private void buttonRegistrar_Paciente_Click(object sender, EventArgs e)
        {
            pacientes.restablecer();
            tabControlSecundario_Pacientes.SelectedIndex = 1;

            labelPacientes_RegistroNuevo.Text = "Registrar Nuevo Paciente";
        }

        //Modificar paciente seleccionado - MENU PRINCIPAL
        private void buttonModificar_Paciente_Click(object sender, EventArgs e)
        {
            tabControlSecundario_Pacientes.SelectedIndex = 2;
            labelPaciente_ModificarRegistro.Text = "Modificar Registro de Paciente Seleccionado";
        }

        //Volver al Menú principal VER
        private void buttonMenuPrincipalPacientes_Ver_Click(object sender, EventArgs e)
        {
            tabControlSecundario_Pacientes.SelectedIndex = 0;

            labelPacientes_RegistroNuevo.Text = "";
            labelPaciente_VerRegistro.Text = "";
            labelPaciente_ModificarRegistro.Text = "";

        }

        //Volver al Menú Principal MODIFICAR
        private void buttonPaciente_ModificarVolver_Click(object sender, EventArgs e)
        {
            tabControlSecundario_Pacientes.SelectedIndex = 0;

            labelPacientes_RegistroNuevo.Text = "";
            labelPaciente_VerRegistro.Text = "";
            labelPaciente_ModificarRegistro.Text = "";
        }

        private void buttonPacientes_Ver_Click(object sender, EventArgs e)
        {
            tabControlSecundario_Pacientes.SelectedIndex = 3;
            labelPaciente_VerRegistro.Text = "Ver Registro de Paciente Seleccionado";
        }

        #endregion


        /************************************
         * 
         * 
         *          CODIGO DEL TRATAMIENTO
         *          
         ************************************/


        #region

        private TratamientoVM tratamiento;

        // Botón Principal Tratamiento
        private void buttonTratamiento_Click(object sender, EventArgs e)
        {
            tabControlPrincipal.SelectedIndex = 1;
            EnabledButton(buttonTratamiento);

            dataGridView_Tratamiento.EnableHeadersVisualStyles = false;
            dataGridView_Tratamiento.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(172, 177, 245);
            dataGridView_Tratamiento.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 10, FontStyle.Bold);

            dataGridViewTratamiento_ResultadosSesion.EnableHeadersVisualStyles = false;
            dataGridViewTratamiento_ResultadosSesion.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(172, 177, 245);
            dataGridViewTratamiento_ResultadosSesion.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 9, FontStyle.Bold);

            var textBoxTratamiento = new List<TextBox>();
            textBoxTratamiento.Add(textBoxTratamiento_CI);
            textBoxTratamiento.Add(textBoxTratamiento_Nombre);
            textBoxTratamiento.Add(textBoxTratamiento_Apellido);
            textBoxTratamiento.Add(textBoxTratamiento_Edad);
            textBoxTratamiento.Add(textBoxTratamiento_Carrera);
            textBoxTratamiento.Add(textBoxTratamiento_Semestre);
            textBoxTratamiento.Add(textBoxTratamiento_Fobia);
            textBoxTratamiento.Add(textBoxTratamiento_Antecedentes);
            textBoxTratamiento.Add(textBoxTratamiento_Sintomas);

            textBoxTratamiento.Add(textBoxTratamiento_Observaciones);

            var labelTratamiento = new List<Label>();
            labelTratamiento.Add(labelTratamiento_CI);
            labelTratamiento.Add(labelTratamiento_Nombre);
            labelTratamiento.Add(labelTratamiento_Apellido);
            labelTratamiento.Add(labelTratamiento_Observaciones);

            object[] objetos =
            {

                dataGridView_Tratamiento, //0
                 dataGridViewTratamiento_ResultadosSesion, //1
                chartTratamiento_GraficaSesion //2

            };

            tratamiento = new TratamientoVM(objetos, textBoxTratamiento, labelTratamiento);

        }

        /////////////////////////////// SESION
        private void textBoxTratamiento_Observaciones_TextChanged_1(object sender, EventArgs e)
        {
            if (textBoxTratamiento_Observaciones.Text.Equals(""))
            {
                labelTratamiento_Observaciones.ForeColor = Color.LightSlateGray;

            }
            else
            {
                labelTratamiento_Observaciones.Text = "Observaciones de la sesión";
                labelTratamiento_Observaciones.ForeColor = Color.Green;
            }
        }


        //Actualizar
        private void buttonTratamiento_Agregar_Click_1(object sender, EventArgs e)
        {
            textBoxTratamiento_CI.Enabled = false;
            textBoxTratamiento_Nombre.Enabled = false;
            textBoxTratamiento_Apellido.Enabled = false;
            tratamiento.guardarTratamiento();
            MessageBox.Show("Sesión guardada correctamente.");
            tabControlTratamiento_Secundario.SelectedIndex = 0;
        }

        /* private void buttonTratamiento_Cancelar_Click(object sender, EventArgs e)
         {
             tratamiento.restablecer();
         }*/

        //DATAGRIDVIEW
        private void dataGridView_Tratamiento_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView_Tratamiento.Rows.Count != 0)
            {
                tratamiento.GetTratamiento();
            }
        }

        private void dataGridView_Tratamiento_KeyUp(object sender, KeyEventArgs e)
        {
            if (dataGridView_Tratamiento.Rows.Count != 0)
            {
                tratamiento.GetTratamiento();
            }
        }

        private void textBoxTratamiento_Buscar_TextChanged(object sender, EventArgs e)
        {
            tratamiento.SearchTratamientoAsync(textBoxTratamiento_Buscar.Text);
        }

        //COMBOBOX
       /* private void comboBoxTratamiento_Sesiones_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxTratamiento_Sesiones.Text = "Seleccionar fecha";
            comboBoxTratamiento_Sesiones.DataSource = tratamiento.getFechaSesionesTratamiento();
            comboBoxTratamiento_Sesiones.DisplayMember = "fecha";

        }*/

        private void comboBoxTratamiento_Sesiones_KeyPress(object sender, KeyPressEventArgs e)
        {
            comboBoxTratamiento_Sesiones.Text = "Seleccionar fecha";
            comboBoxTratamiento_Sesiones.DataSource = tratamiento.getFechaSesionesTratamiento();
            comboBoxTratamiento_Sesiones.DisplayMember = "fecha";
        }

        private void buttonTratamiento_VerSesionSeleccionada_Click(object sender, EventArgs e)
        {
            tratamiento.MostrarResultadosSesionAsync(comboBoxTratamiento_Sesiones.Text);
            textBoxTratamiento_FechaSesion.Text = comboBoxTratamiento_Sesiones.Text;

            

            if(comboBoxTratamiento_Sesiones.Text == "" || comboBoxTratamiento_Sesiones.Text == "Seleccionar fecha") 
            {
                MessageBox.Show("Seleccione una fecha!");
                tabControlTratamiento_Secundario.SelectedIndex = 1;
            }
            else
            {
                tabControlTratamiento_Secundario.SelectedIndex = 3;
                labelTratamiento_RegistroAtencion.Text = "Resultados de la Sesión Seleccionada";
            }

        }


        ////////////////////////////// DATOS FISIOLOGICOS

        public void getAvailablePorts()
        {
            string[] ports = SerialPort.GetPortNames();
            comboBox_Puerto.Items.AddRange(ports);
        }

        private void button_Play_Click(object sender, EventArgs e)
        {


            try
            {
                if (comboBox_Puerto.Text.Equals("") || comboBox_Velocidad_Transmision.Text.Equals(""))
                {
                    label_Puerto.Text = "Seleccionar el puerto";
                    label_Puerto.ForeColor = Color.Red;

                    label_Velocidad_Transmision.Text = "Seleccionar la velocidad";
                    label_Velocidad_Transmision.ForeColor = Color.Red;
                }
                else
                {
                    timer1.Start(); // Inicializar el timer
                    serialPort1.PortName = comboBox_Puerto.Text; //Seleccionar el puerto de entrada por ComboBox
                    serialPort1.BaudRate = Convert.ToInt32(comboBox_Velocidad_Transmision.Text); // Seleccionar la velocidad de transmisión por ComboBox
                    serialPort1.Open(); //Abrir el puerto serial

                    progressBar_Estado.Value = 100;

                    if (progressBar_Estado.Value == 100)
                    {
                        label_Estado.Text = "Listo para utilizar";
                        label_Estado.ForeColor = Color.Red;
                    }

                    button_Play.Enabled = false;
                    button_stop.Enabled = true;



                }
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Acceso denegado.");
            }
        }

        private void button_stop_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            serialPort1.Close();
            //chart1.Series.Clear();
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();

            progressBar_Estado.Value = 0;
            label_Estado.Text = "Estado";
            label_Estado.ForeColor = Color.Black;

            button_Play.Enabled = true;
            button_stop.Enabled = false;

            label_dato.Text = "";
            label_cadenaF.Text = "";
            label_cadenaT.Text = "";
            label_cadenaM.Text = "";
            label_cadenaE.Text = "";


            //Close();
        }

        string dato;
        string oficial;
        string oficial1;
        string oficial2;
        string cd1 = "", cd2 = "", cd3 = "";
        int x = 0;

        string[] todo = new string[1000];

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int espacios = 0;

            dato = serialPort1.ReadLine(); //Lectura de datos por el puerto serial

            tratamiento.GetId(label_cadenaF.Text, label_cadenaT.Text, label_cadenaM.Text, label_cadenaE.Text); // Método para almacenar los datos de los labels en la BD


            string cad = "";


            if (dato != cad)
            {
                ThreadHelperClass.SetText(this, label_dato, dato);

                for (int i = 0; i < dato.Length; i++)
                {
                    if (dato[i] == ' ') espacios++;
                    if (espacios == 0 && espacios != ' ') cd1 = cd1 + dato[i]; //Separar la cadena correspondiente a "Frecuencia cardiaca"

                    if (espacios == 1 && espacios != ' ') cd2 = cd2 + dato[i]; //Separar la cadena correspondiente a "Temperatura de la piel"

                    if (espacios == 2 && espacios != ' ') cd3 = cd3 + dato[i]; //Separar la cadena correspondiente a "Nivel del miedo"

                }

                ThreadHelperClass.SetText(this, label_cadenaF, cd1);
                ThreadHelperClass.SetText(this, label_cadenaT, cd2);
                ThreadHelperClass.SetText(this, label_cadenaM, cd3);


                oficial = cd1;
                oficial1 = cd2;

                cd1 = "";
                cd2 = "";
                cd3 = "";
            }

        }


        //Graficar
        private void timer1_Tick(object sender, EventArgs e)
        {

            try
            {
                //serialPort1.Write(this.label_dato.Text);
                //dato = serialPort1.ReadChar();
                x++; // Inicializar un contador

                chart1.Series[0].Points.AddXY(x, oficial); //Graficar la frecuencia cardiaca
                chart1.Series[1].Points.AddXY(x, oficial1); //Graficar la temperatura de la piel

                if (x == 60) //Cuando el contador llega a 60, se reinicia la gráfica
                {
                    x = 0;
                    chart1.Series[0].Points.Clear();
                    chart1.Series[1].Points.Clear();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR AL ENVIAR");
            }

        }

        //ESCENARIOS

        //Aplicación Remota
        private void buttonControl_Claustro_Click(object sender, EventArgs e)
        {
            //string ruta = "D:\\AirDroid\\AirDroid.exe";
            //ruta: Direccion donde se encuetra la aplicacion.
            //"": Parametro en caso de que tu aplicacion este esperando un valor
            //ProcessStartInfo info = new ProcessStartInfo(ruta, "");

            try
            {
                //Abrir Aplicación de Escritorio
                //Process.Start(info);

                //Abrir URL
                System.Diagnostics.Process.Start("https://web.airdroid.com/");
                MessageBox.Show("¡Abierto exitosamente!");
            }
            catch (Exception)
            {
                MessageBox.Show("Problema de Conexión");
            }
        }

        //Seleccionar escenarios
        private void buttonEscenario1_Click(object sender, EventArgs e)
        {
            label_cadenaE.Text = "Escenario 1";
        }

        private void buttonEscenario2_Click(object sender, EventArgs e)
        {
            label_cadenaE.Text = "Escenario 2";
        }

        private void buttonEscenario3_Click(object sender, EventArgs e)
        {
            label_cadenaE.Text = "Escenario 3";
        }

        private void buttonEscenario4_Click(object sender, EventArgs e)
        {
            label_cadenaE.Text = "Escenario 4";
        }

        private void buttonEscenario5_Click(object sender, EventArgs e)
        {
            label_cadenaE.Text = "Escenario 5";
        }

        /// BOTONES
        //Botón para "NUEVA SESIÓN"
        private void buttonTratamiento_NuevaSesion_Click(object sender, EventArgs e)
        {
            //dataGridView_Tratamiento_CellClick
            if (dataGridView_Tratamiento.SelectedRows.Count != 0 || textBoxTratamiento_Buscar.Text != "")
            {
                tabControlTratamiento_Secundario.SelectedIndex = 1;

                labelTratamiento_RegistroSesion.Text = "Registro de la Sesión";

                //MOSTRAR FECHAS DE SESIONES DEL PACIENTE
                //comboBoxTratamiento_Sesiones.Text = "";
                comboBoxTratamiento_Sesiones.DataSource = tratamiento.getFechaSesionesTratamiento();
                comboBoxTratamiento_Sesiones.DisplayMember = "fecha";

            }
            else
            {
                MessageBox.Show("Selecciona un paciente!");
            }

        }

        //BOTON MENU PRINCIPAL
        private void buttonTratamientoSesiones_MenuPrincipal_Click_1(object sender, EventArgs e)
        {
            tabControlTratamiento_Secundario.SelectedIndex = 1;
            labelTratamiento_RegistroAtencion.Text = "Registro de la Sesión";
        }

        //Botón para Volver al Menú de Selección del Paciente (1)
        private void buttonTratamiento_VolverMenu1_Click(object sender, EventArgs e)
        {
            tabControlTratamiento_Secundario.SelectedIndex = 0;
            textBoxTratamiento_Buscar.Text = "";

            labelTratamiento_RegistroAtencion.Text = "";
            labelTratamiento_RegistroSesion.Text = "";

            dataGridView_Tratamiento.ClearSelection();
        }

        //Botón para LANZAR LA TOMA DE DATOS
        private void buttonTratamiento_LanzarTDatos_Click(object sender, EventArgs e)
        {
            textBoxFecha_Actual.Text = DateTime.Now.ToString("dd/MMM/yyy");
            textBoxFecha_Actual.Enabled = false;
            tabControlTratamiento_Secundario.SelectedIndex = 2;

            labelTratamiento_RegistroAtencion.Text = "Nueva Sesión";
            labelTratamiento_RegistroSesion.Text = "";

        }

        /// TAB DE "TOMA DE DATOS"
        /// 

        //Botón para Volver al Menú de Selección del Paciente (2)
        private void buttonTratamiento_VolverMenuPrincipal2_Click(object sender, EventArgs e)
        {
            tabControlTratamiento_Secundario.SelectedIndex = 0;
            textBoxTratamiento_Buscar.Text = "";
            dataGridView_Tratamiento.ClearSelection();

            labelTratamiento_RegistroAtencion.Text = "";
            labelTratamiento_RegistroSesion.Text = "";

        }


        #endregion

        /************************************
         * 
         * 
         *          CODIGO DEL USUARIO
         *          
         ************************************/

        #region


        private void buttonUsuario_Click(object sender, EventArgs e)
        {

            dataGridView_Usuarios.EnableHeadersVisualStyles = false;
            dataGridView_Usuarios.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(172, 177, 245);
            dataGridView_Usuarios.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 9, FontStyle.Bold);

            var textBoxUsuarioModficar = new List<TextBox>
            {
                textBoxUsuarioCI_Modificar,
                textBoxUsuarioNombre_Modificar,
                textBoxUsuarioApellido_Modificar,
                textBoxUsuarioEmail_Modificar,
                textBoxUsuarioTelefono_Modificar,
                textBoxUsuarioDireccion_Modificar,
                textBoxUsuarioUsuario_Modificar,
                textBoxUsuarioContrasena_Modificar

            };

            var textBoxUsuarioVer = new List<TextBox>
            {
                textBoxUsuarioVer_CI,
                textBoxUsuarioVer_Nombre,
                textBoxUsuarioVer_Apellido,
                textBoxUsuarioVer_Email,
                textBoxUsuarioVer_Telefono,
                textBoxUsuarioVer_Direccion,
                textBoxUsuarioVer_Usuario,
                textBoxUsuarioVer_Password

            };


            var textBoxUsuario = new List<TextBox>
            {
                textBoxUsuario_Nid,
                textBoxUsuario_Nombre,
                textBoxUsuario_Apellido,
                textBoxUsuario_Email,
                textBoxUsuario_Telefono,
                textBoxUsuario_Direccion,
                textBoxUsuario_Usuario,
                textBoxUsuario_Password

            };

            var labelUsuario = new List<Label>
            {
                labelUsuario_Nid,
                labelUsuario_Nombre,
                labelUsuario_Apellido,
                labelUsuario_Email,
                labelUsuario_Telefono,
                labelUsuario_Direccion,
                labelUsuario_Usuario,
                labelUsuario_Password,

                //Para paginas
                labelPaginas_Usuario

            };

            object[] objetos =
            {
                pictureBoxUsuario,
                pictureBoxUsuario_Ver,
                pictureBoxUsuario_Modificar,

                checkBoxUsuario_Estado,
                checkBoxUsuarioVer_Estado,
                checkBoxUsuarioEstado_Modificar,

                comboBoxUsuario_Roles,
                comboBoxUsuarioVer_Roles,
                comboBoxUsuarioRol_Modificar,

                //Para ver
                dataGridView_Usuarios,
                numeric_PaginasUsuario
            };

            usuario = new UsuariosVM(objetos, textBoxUsuario, labelUsuario, textBoxUsuarioVer, textBoxUsuarioModficar);

            if (_dataUsuario.Rol.Equals("Admin")) //Se compara el usuario con "Administrador"
            {
                tabControlPrincipal.SelectedIndex = 2; //Acceso a la interfaz "Usuarios"
                EnabledButton(buttonUsuario); // Botón habilitado
            }
            else
            {
                MessageBox.Show("No cuenta con el permiso requerido para ejecutar esta acción."); //Si no es "administrador", no accede a la interfaz "Usuarios"
            }

        }

        //Picture Box
        private void pictureBoxUsuario_Click_1(object sender, EventArgs e)
        {
            Objects.uploadimage.CargarImagen(pictureBoxUsuario);
        }

        // Picture Box para Modificar
        private void pictureBoxUsuario_Modificar_Click(object sender, EventArgs e)
        {
            Objects.uploadimage.CargarImagen(pictureBoxUsuario_Modificar);
        }


        //CI
        private void textBoxUsuario_Nid_TextChanged_1(object sender, EventArgs e)
        {
            if (textBoxUsuario_Nid.Text.Equals(""))
            {
                labelUsuario_Nid.ForeColor = Color.LightSlateGray;
            }
            else
            {
                labelUsuario_Nid.Text = "CI";
                labelUsuario_Nid.ForeColor = Color.Green;
            }
        }

        // Nombre
        private void textBoxUsuario_Nombre_TextChanged_1(object sender, EventArgs e)
        {
            if (textBoxUsuario_Nombre.Text.Equals(""))
            {
                labelUsuario_Nombre.ForeColor = Color.LightSlateGray;
            }
            else
            {
                labelUsuario_Nombre.Text = "Nombre";
                labelUsuario_Nombre.ForeColor = Color.Green;
            }
        }

        private void textBoxUsuario_Nombre_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            Objects.eventos.textKeyPress(e);
        }

        // Apellido
        private void textBoxUsuario_Apellido_TextChanged_1(object sender, EventArgs e)
        {
            if (textBoxUsuario_Apellido.Text.Equals(""))
            {
                labelUsuario_Apellido.ForeColor = Color.LightSlateGray;
            }
            else
            {
                labelUsuario_Apellido.Text = "Apellido";
                labelUsuario_Apellido.ForeColor = Color.Green;
            }
        }

        private void textBoxUsuario_Apellido_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            Objects.eventos.textKeyPress(e);
        }


        // Email
        private void textBoxUsuario_Email_TextChanged_1(object sender, EventArgs e)
        {
            if (textBoxUsuario_Email.Text.Equals(""))
            {
                labelUsuario_Email.ForeColor = Color.LightSlateGray;
            }
            else
            {
                labelUsuario_Email.Text = "Email";
                labelUsuario_Email.ForeColor = Color.Green;
            }
        }

        // Telefono
        private void textBoxUsuario_Telefono_TextChanged_1(object sender, EventArgs e)
        {
            if (textBoxUsuario_Telefono.Text.Equals(""))
            {
                labelUsuario_Telefono.ForeColor = Color.LightSlateGray;
            }
            else
            {
                labelUsuario_Telefono.Text = "Teléfono";
                labelUsuario_Telefono.ForeColor = Color.Green;
            }
        }

        private void textBoxUsuario_Telefono_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            Objects.eventos.numberKeyPress(e);
        }

        // Direccion
        private void textBoxUsuario_Direccion_TextChanged_1(object sender, EventArgs e)
        {
            if (textBoxUsuario_Direccion.Text.Equals(""))
            {
                labelUsuario_Direccion.ForeColor = Color.LightSlateGray;
            }
            else
            {
                labelUsuario_Direccion.Text = "Dirección";
                labelUsuario_Direccion.ForeColor = Color.Green;
            }
        }

        // Usuario
        private void textBoxUsuario_Usuario_TextChanged_1(object sender, EventArgs e)
        {
            if (textBoxUsuario_Usuario.Text.Equals(""))
            {
                labelUsuario_Usuario.ForeColor = Color.LightSlateGray;
            }
            else
            {
                labelUsuario_Usuario.Text = "Usuario";
                labelUsuario_Usuario.ForeColor = Color.Green;
            }
        }

        //Contraseña
        private void textBoxUsuario_Password_TextChanged_1(object sender, EventArgs e)
        {
            if (textBoxUsuario_Password.Text.Equals(""))
            {
                labelUsuario_Password.ForeColor = Color.LightSlateGray;
            }
            else
            {
                labelUsuario_Password.Text = "Contraseña";
                labelUsuario_Password.ForeColor = Color.Green;
            }
        }

        //Botón Guardar - Nuevo
        private void buttonUsuarios_Agregar_Click_1(object sender, EventArgs e)
        {
            usuario.guardarUsuario();
            tabControlUsuario_Secundario.SelectedIndex = 0;

            labelUsuario_RegistroNuevo.Text = "";
            labelUsuario_RegistroModificado.Text = "";
            labelUsuario_RegistroVer.Text = "";
        }

        //Botón Guardar - Modificar
        private void buttonUsuarioGuardar_Modificar_Click(object sender, EventArgs e)
        {
            usuario.guardarUsuario();
            tabControlUsuario_Secundario.SelectedIndex = 0;

            labelUsuario_RegistroNuevo.Text = "";
            labelUsuario_RegistroModificado.Text = "";
            labelUsuario_RegistroVer.Text = "";
        }


        //Restablecer - Nuevo Usuario
        private void buttonUsuario_BorrarTodo_Click(object sender, EventArgs e)
        {
            usuario.restablecer();
        }

        // Restablecer - Modificar Usuario
        private void buttonUsuarioRestablecer_Modificar_Click(object sender, EventArgs e)
        {
            usuario.restablecerUsuarios_Modificar();
        }

        private void buttonUsuario_Pagina1_Click(object sender, EventArgs e)
        {
            usuario.Paginador("Primero");
        }

        private void buttonUsuario_Pagina2_Click(object sender, EventArgs e)
        {
            usuario.Paginador("Anterior");
        }

        private void buttonUsuario_Pagina3_Click(object sender, EventArgs e)
        {
            usuario.Paginador("Siguiente");
        }

        private void buttonUsuario_Pagina4_Click(object sender, EventArgs e)
        {
            usuario.Paginador("Último");
        }

        private void numeric_PaginasUsuario_ValueChanged(object sender, EventArgs e)
        {
            usuario.Registro_Paginas();
        }

        private void dataGridView_Usuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView_Usuarios.Rows.Count != 0)
                usuario.GetUsuario();
            usuario.GetVerUsuario();
            usuario.GetUsuarioModificar();
        }


        private void dataGridView_Usuarios_KeyUp(object sender, KeyEventArgs e)
        {
            if (dataGridView_Usuarios.Rows.Count != 0)
                usuario.GetUsuario();
            usuario.GetVerUsuario();
            usuario.GetUsuarioModificar();
        }


        //Buscar
        private void textBoxUsuarios_Buscar_TextChanged(object sender, EventArgs e)
        {
            usuario.SearchUsuarios(textBoxUsuarios_Buscar.Text);
        }



        //Eliminar
        /*private void buttonUsuario_Eliminar_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("¿Desea eliminar de forma permanente?","Eliminar Usuario",MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                usuario.DeleteUsuario(usuario.ID);
                usuario.restablecer();
                MessageBox.Show("Usuario eliminado");
            }
        }*/

        //Volver a menú principal
        private void buttonUsuario_Eliminar_Click(object sender, EventArgs e)
        {
            tabControlUsuario_Secundario.SelectedIndex = 0;

            labelUsuario_RegistroNuevo.Text = "";
            labelUsuario_RegistroModificado.Text = "";
            labelUsuario_RegistroVer.Text = "";
        }

        //Registrar Nuevo Usuario - MENU PRINCIPAL
        private void buttonUsuario_RegistrarNuevo_Click(object sender, EventArgs e)
        {
            usuario.restablecer();
            tabControlUsuario_Secundario.SelectedIndex = 1;

            labelUsuario_RegistroNuevo.Text = "Registrar Nuevo Usuario";
        }

        //Volver al Menú Principal - desde Modificar
        private void buttonMenuPUsuarios_Modificar_Click(object sender, EventArgs e)
        {
            tabControlUsuario_Secundario.SelectedIndex = 0;

            labelUsuario_RegistroNuevo.Text = "";
            labelUsuario_RegistroModificado.Text = "";
            labelUsuario_RegistroVer.Text = "";
        }

        //Modificar Nuevo Usuario - MENU PRINCIPAL
        private void buttonUsuario_Modificar_Click(object sender, EventArgs e)
        {
            tabControlUsuario_Secundario.SelectedIndex = 2;

            labelUsuario_RegistroModificado.Text = "Modificar Registro de Usuario Seleccionado";
        }

        //VOLVER AL MENU PRINCIPAL DESDE "VER"
        private void buttonMenuPrincipalUsuariosVer_Click(object sender, EventArgs e)
        {
            tabControlUsuario_Secundario.SelectedIndex = 0;

            labelUsuario_RegistroNuevo.Text = "";
            labelUsuario_RegistroModificado.Text = "";
            labelUsuario_RegistroVer.Text = "";

        }

        //VER AL USUARIO
        private void buttonUsuario_Ver_Click(object sender, EventArgs e)
        {
            tabControlUsuario_Secundario.SelectedIndex = 3;

            labelUsuario_RegistroVer.Text = "Ver Registro de Usuario Seleccionado";
        }


        #endregion


        /************************************
        * 
        * 
        *         CODIGO DEL REPORTE
        *          
        ************************************/

        #region


        // BOTON PRINCIPAL PARA OBTENER REPORTES
        private void buttonPrincipal_Reportes_Click(object sender, EventArgs e)
        {
            dataGridViewCliente_Datos.EnableHeadersVisualStyles = false;
            dataGridViewCliente_Datos.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(172, 177, 245);
            dataGridViewCliente_Datos.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 9, FontStyle.Bold);

            textBoxReporte_Fecha.Text = DateTime.Now.ToString("dd/MMM/yyy");
            textBoxReporte_Usuario.Text = labelNombre_Perfil.Text;

            textBoxReporte_Fecha.Enabled = false;
            textBoxReporte_Usuario.Enabled = false;

            tabControlPrincipal.SelectedIndex = 3;
            EnabledButton(buttonPrincipal_Reportes);
            pacientes.GetReportes();

            //Cargar las opciones del ComboBOx
            comboBoxReportes_Pacientes.DataSource = pacientes.getPacientes();
            comboBoxReportes_Pacientes.ValueMember = "IdCliente";
            comboBoxReportes_Pacientes.DisplayMember = "CI";

        }

        //REPORTE POR SESION - COMBOBOXES
        private void comboBoxReportes_Pacientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            TClientes pac = (TClientes)comboBoxReportes_Pacientes.SelectedItem;
            comboBoxReportes_Fechas.Text = "Seleccionar fecha";
            comboBoxReportes_Fechas.DataSource = pacientes.getFecha(pac.IdCliente);
            comboBoxReportes_Fechas.DisplayMember = "fecha";
        }

        private void comboBoxReportes_Pacientes_KeyPress(object sender, KeyPressEventArgs e)
        {
            TClientes pac = (TClientes)comboBoxReportes_Pacientes.SelectedItem;
            comboBoxReportes_Fechas.Text = "Seleccionar fecha";
            comboBoxReportes_Fechas.DataSource = pacientes.getFecha(pac.IdCliente);
            comboBoxReportes_Fechas.DisplayMember = "fecha";
        }

        // BOTON PARA OBTENER REPORTE POR SESION
        private void buttonReporte_Sesion_Click(object sender, EventArgs e)
        {
                pacientes.SearchReporteSesionAsync(comboBoxReportes_Pacientes.Text, comboBoxReportes_Fechas.Text);

                // TextBox para Indicar la Sesión
                textBoxReporteFechaSesion.Text = comboBoxReportes_Fechas.Text;
                textBoxReporteFechaSesion.Enabled = false;

                TClientes pac = (TClientes)comboBoxReportes_Pacientes.SelectedItem;
                comboBoxReportes_Fechas.Text = "";
                //comboBoxReportes_Fechas.DataSource = pacientes.getFecha(pac.IdCliente);
                //pacientes.graficarReporte(pac.IdCliente);
                tabControlReportes.SelectedIndex = 1;
                labelReporteGraficaGeneral.Text = "Datos Personales del Paciente";

                pacientes.GetClienteReporte(comboBoxReportes_Pacientes.Text);

                int n = 0;
                //IMPRIMIR TODAS LAS SESIONES
                foreach (var item in comboBoxReportes_Fechas.Items) 
                {
                    Sesiones.Items.Add(item.ToString());
                    n = n + 1;
                }

                textBoxNumero.Text = n.ToString();
                Sesiones.Enabled = false;
                textBoxNumero.Enabled = false;
        }

        //SIGUIENTE PÁGINA DATOS PERSONALES
        private void buttonReportes_Siguiente1_Click(object sender, EventArgs e)
        {
            tabControlReportes.SelectedIndex = 2;
            labelReporteGraficaGeneral.Text = "Datos de la Sesión";
        }

        // SIGUIENTE PAGINA DATOS DE LA SESION
        private void buttonSesionSiguiente_Click(object sender, EventArgs e)
        {
            tabControlReportes.SelectedIndex = 4;
            labelReporteGraficaGeneral.Text = "Observaciones";
        }


        //ANTERIOR PAGINA
        private void buttonSesionAnterior1_Click(object sender, EventArgs e)
        {
            tabControlReportes.SelectedIndex = 1;
            labelReporteGraficaGeneral.Text = "Datos Personales del Paciente";
        }

        //ANTERIOR PÁGINA
        private void buttonSesionAnterior_Click(object sender, EventArgs e)
        {
            tabControlReportes.SelectedIndex = 2;
            labelReporteGraficaGeneral.Text = "Datos de la Sesión";
        }

        private void buttonReporteAnterior3_Click(object sender, EventArgs e)
        {
            tabControlReportes.SelectedIndex = 3;
            labelReporteGraficaGeneral.Text = "Gráfica de la Sesión";
        }


        private void buttonReporteGrafica_Volver_Click_1(object sender, EventArgs e)
        {
            tabControlReportes.SelectedIndex = 0;
            //pacientes.restablecerGrafico();

            labelReporteGraficaGeneral.Text = "";

            comboBoxReportes_Fechas.Text = "";
            comboBoxReportes_Pacientes.Text = "";
        }


        //PDF
        private void buttonReporte_PDF_Click_1(object sender, EventArgs e)
        {
            /*String[] title = { "CI", "Nombre", "Apellido","Fecha","Hora","Frecuencia", "Temperatura", "Miedo" }; //Se definen los títulos de las columnas
            int[] colum = { 0 }; // Las columnas que no serán impresas
            ExportData.exportarDataGridViewPDF(dataGridViewCliente_Datos, title, colum); //Clase que exporta el documento a PDF
            MessageBox.Show("¡PDF guardado exitosamente!"); // Mensaje de guardado exitoso*/

           
                String[] title = { "Escenarios","Frecuencia", "Temperatura", "Miedo" }; //Se definen los títulos de las columnas
                int[] colum = { 0, 1, 2, 3, 4, 5 }; // Las columnas que no serán impresas
                ExportData.exportarDataGridViewPDF(dataGridViewCliente_Datos, title, colum, labelreporteFechaActual.Text, this.textBoxReporte_Fecha.Text, labelReporteEncargado.Text, this.textBoxReporte_Usuario.Text, labelReporteSesion.Text, this.textBoxReporteFechaSesion.Text, this.labelNSesionesA.Text, this.textBoxNumero.Text, this.labelReporte_CI.Text, this.textBoxReporte_CI.Text, this.labelReporte_Nombre.Text, this.textBoxReporte_Nombre.Text, this.labelReporte_Apellido.Text, this.textBoxReporte_Apellido.Text, this.labelReporte_Edad.Text, this.textBoxReporte_Edad.Text, this.labelReporte_FechaNac.Text, this.textBoxReporte_FechaNac.Text, this.labelReporte_Carrera.Text, this.textBoxReporte_Carrera.Text, this.labelReporte_Semestre.Text, this.textBoxReporte_Semestre.Text, this.labelReporte_Genero.Text, this.textBoxReporte_Genero.Text, this.labelReporte_Fobia.Text, this.textBoxReporte_Fobia.Text, this.labelReporte_Antecedentes.Text, this.textBoxReporte_Antecedentes.Text, this.labelReporte_Sintomas.Text, this.textBoxReporte_Sintomas.Text, this.textBoxReporte_Observaciones.Text); //Clase que exporta el documento a PDF
                MessageBox.Show("¡PDF guardado exitosamente!"); // Mensaje de guardado exitoso

        }


        }

    #endregion

}
