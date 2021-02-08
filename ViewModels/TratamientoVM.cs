using LinqToDB;
using Models;
using Models.Conexion;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ViewModels
{
    public class TratamientoVM : Conexion
    {
        private List<TextBox> _textBoxTratamiento;
        private List<Label> _labelTratamiento;
        private string _accion = "insert";
        private static DataGridView _datagridView;

        private static DataGridView _datagridViewTratamientoSesion;
        private static Chart _chartTratamientoSesion;


        private int _reg_por_pagina = 200, _num_pagina = 1;


        public TratamientoVM( object[] objetos, List<TextBox> textBoxTratamiento, List<Label> labelTratamiento)
        {
            _textBoxTratamiento = textBoxTratamiento;
            _labelTratamiento = labelTratamiento;
            _datagridView = (DataGridView)objetos[0];

            _datagridViewTratamientoSesion = (DataGridView)objetos[1];
            _chartTratamientoSesion = (Chart)objetos[2];

            restablecer();
        }

        public void guardarTratamiento()
        {
            if (_textBoxTratamiento[3].Text.Equals(""))
            {
                _labelTratamiento[3].Text = "Requerido: Observación de la sesión";
                _labelTratamiento[3].ForeColor = Color.Red;
                _textBoxTratamiento[3].Focus();
            }
            else
            {
                switch (_accion)
                {
                    case "insert":
                        SaveData();
                        break;

                    case "update":
                        SaveData();
                        break;


                }
            }

        }

        public void SaveData()
        {
            BeginTransactionAsync();
            try
            {
                switch(_accion)
                {
                    case "insert":

                        TClientes.Value(u => u.Observaciones, _textBoxTratamiento[3].Text)//Inserción de las observaciones en la BD
                            .Insert();//Consulta de inserción

                        var cli = TClientes.ToList().Last();

                        break;

                    case "update":

                        TClientes.Where(u => u.IdCliente.Equals(_idCliente))
                            .Set(u => u.Observaciones, _textBoxTratamiento[3].Text)
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

        // BUSCAR
        public async Task SearchTratamientoAsync(string campo)
        {
            List<TClientes> query;
            int inicio = (_num_pagina - 1) * _reg_por_pagina;
            if (campo.Equals(""))
            {
                query = await TClientes.ToListAsync();
            }
            else
            {
                query = await TClientes.Where(c => c.CI.StartsWith(campo) || c.Nombre.StartsWith(campo) 
                || c.Apellido.StartsWith(campo)).ToListAsync();
            }

            _datagridView.DataSource = query.Skip(inicio).Take(_reg_por_pagina).ToList();
            _datagridView.Columns[0].Visible = false;
            _datagridView.Columns[4].Visible = false;
            _datagridView.Columns[5].Visible = false;
            _datagridView.Columns[6].Visible = false;
            _datagridView.Columns[7].Visible = false;
            _datagridView.Columns[8].Visible = false;
            _datagridView.Columns[10].Visible = false;
            _datagridView.Columns[11].Visible = false;
            _datagridView.Columns[12].Visible = false;//OBSERVACIONES
            _datagridView.Columns[13].Visible = false;
            _datagridView.Columns[14].Visible = false;

            _datagridView.Columns[1].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            _datagridView.Columns[2].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            _datagridView.Columns[3].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            _datagridView.Columns[9].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            //_datagridView.Columns[12].DefaultCellStyle.BackColor = Color.WhiteSmoke;

        }

        //METODO PARA OBTENER LA INFORMACION DE LOS LABEL DE FRECUENCIA, TEMPERATURA Y MIEDO
        private int _idCliente1 = 0;
        public void GetId(string f, string t, string m, string e)
        {
             _accion = "update";
            _idCliente1 = Convert.ToInt16(_datagridView.CurrentRow.Cells[0].Value);

            Datos.Value(u => u.idC, _idCliente1)
                .Value(u => u.fecha, DateTime.Now.ToString("dd/MMM/yyy"))
                .Value(u => u.Hora,  DateTime.Now.ToString("h:mm:ss tt"))
                .Value(u => u.Escenario, e)
                .Value(u => u.Frecuencia, f)
                .Value(u => u.Temperatura, t)
                .Value(u => u.Miedo, m)
                .Insert();
        }


        private int _idCliente = 0;
        public void GetTratamiento()
        {
            try
            {
                _accion = "update";
                _textBoxTratamiento[0].Enabled = false;
                _textBoxTratamiento[1].Enabled = false;
                _textBoxTratamiento[2].Enabled = false;
                _textBoxTratamiento[3].Enabled = false;
                _textBoxTratamiento[4].Enabled = false;
                _textBoxTratamiento[5].Enabled = false;
                _textBoxTratamiento[6].Enabled = false;
                _textBoxTratamiento[7].Enabled = false;
                _textBoxTratamiento[8].Enabled = false;



                _idCliente = Convert.ToInt16(_datagridView.CurrentRow.Cells[0].Value);

                /*Datos.Value(u => u.idCliente, _idCliente)
                    .Insert();*/

                _textBoxTratamiento[0].Text = Convert.ToString(_datagridView.CurrentRow.Cells[1].Value);
                _textBoxTratamiento[1].Text = Convert.ToString(_datagridView.CurrentRow.Cells[2].Value);
                _textBoxTratamiento[2].Text = Convert.ToString(_datagridView.CurrentRow.Cells[3].Value);
                _textBoxTratamiento[3].Text = Convert.ToString(_datagridView.CurrentRow.Cells[4].Value);
                _textBoxTratamiento[4].Text = Convert.ToString(_datagridView.CurrentRow.Cells[6].Value);
                _textBoxTratamiento[5].Text = Convert.ToString(_datagridView.CurrentRow.Cells[7].Value);
                _textBoxTratamiento[6].Text = Convert.ToString(_datagridView.CurrentRow.Cells[9].Value);
                _textBoxTratamiento[7].Text = Convert.ToString(_datagridView.CurrentRow.Cells[10].Value);
                _textBoxTratamiento[8].Text = Convert.ToString(_datagridView.CurrentRow.Cells[11].Value);


                _textBoxTratamiento[9].Text = Convert.ToString(_datagridView.CurrentRow.Cells[12].Value); // Observaciones


            }
            catch (Exception ex)
            {
                RollbackTransaction();
                MessageBox.Show(ex.Message);
            }
          
        }

        public void restablecer()
        {
            _accion = "insert";
            _num_pagina = 1;
            _textBoxTratamiento[0].Text = "";
            _textBoxTratamiento[1].Text = "";
            _textBoxTratamiento[2].Text = "";
            _textBoxTratamiento[3].Text = "";

            _labelTratamiento[3].Text = "Observaciones de la sesión";
            _labelTratamiento[3].ForeColor = Color.LightSlateGray;
            _ = SearchTratamientoAsync("");
        }

        //DEL COMBOBOX
        public object getFechaSesionesTratamiento() //object
        {
            _idCliente = Convert.ToInt16(_datagridView.CurrentRow.Cells[0].Value);
            var query = Datos.Where(c => c.idC.Equals(_idCliente))
                                .GroupBy(c => c.fecha)
                                .Where(c => c.Count() > 0)
                                .Select(c => c.Key);

            return query.ToList();
        }

        //VER SESIONES EN DATAGRIDVIEW
        public async Task MostrarResultadosSesionAsync(string fecha)
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

            _idCliente = Convert.ToInt16(_datagridView.CurrentRow.Cells[0].Value);

            int inicio = (_num_pagina - 1) * _reg_por_pagina;

            if (_idCliente.Equals(""))
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
                   }).Where(c => c.IdCliente.Equals(_idCliente) && c.fecha.Equals(fecha)).ToListAsync();
            }

            if (0 < query.Count)
            {
                _datagridViewTratamientoSesion.DataSource = query.Skip(inicio).Take(_reg_por_pagina).ToList();

                _datagridViewTratamientoSesion.Columns[0].Visible = false;
                _datagridViewTratamientoSesion.Columns[1].Visible = false;
                _datagridViewTratamientoSesion.Columns[2].Visible = false;
                _datagridViewTratamientoSesion.Columns[3].Visible = false;
                _datagridViewTratamientoSesion.Columns[4].Visible = false;
                _datagridViewTratamientoSesion.Columns[5].Visible = false;

                _datagridViewTratamientoSesion.Columns[1].DefaultCellStyle.BackColor = Color.WhiteSmoke; //CI
                _datagridViewTratamientoSesion.Columns[2].DefaultCellStyle.BackColor = Color.WhiteSmoke; // Nombre
                _datagridViewTratamientoSesion.Columns[3].DefaultCellStyle.BackColor = Color.WhiteSmoke; //Apellido
                _datagridViewTratamientoSesion.Columns[4].DefaultCellStyle.BackColor = Color.WhiteSmoke; //fecha
                _datagridViewTratamientoSesion.Columns[5].DefaultCellStyle.BackColor = Color.WhiteSmoke; //hora
                _datagridViewTratamientoSesion.Columns[6].DefaultCellStyle.BackColor = Color.WhiteSmoke; //escenario
                _datagridViewTratamientoSesion.Columns[7].DefaultCellStyle.BackColor = Color.WhiteSmoke; //frec
                _datagridViewTratamientoSesion.Columns[8].DefaultCellStyle.BackColor = Color.WhiteSmoke; //temp
                _datagridViewTratamientoSesion.Columns[9].DefaultCellStyle.BackColor = Color.WhiteSmoke; //miedo
            }
            else
            {
                _datagridViewTratamientoSesion.DataSource = query;
            }

        }


    }
}
