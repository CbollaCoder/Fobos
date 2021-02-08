using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ViewModels.Library
{
   public class ExportData
    {
        private static SaveFileDialog fichero;
        private static Application aplicacion;//

        //private static Workbook libros_trabajo;
        //private static Worksheet hoja_trabajo;

        //public static void exportarDataGridViewPDF(DataGridView grd, String[] title, int[] colum, string labelFechaAct, string fechaActual, string labelEncargado, string Encargado, string labelSesion, string Sesion, string labelSesionesAsistidas, string SesionesAsistidas, string CI, string st1, string Nombre, string st2, string Apellido, string st3, string Edad, string st4, string FechaNac, string st5, string Carrera, string st6, string Semestre, string st7, string Genero, string st8, string Fobia, string st9, string Antecedentes, string st10, string Sintomas, string st11, string st12, Chart chart1)

        public static void exportarDataGridViewPDF (DataGridView grd, String[] title, int[] colum, string labelFechaAct, string fechaActual, string labelEncargado, string Encargado, string labelSesion, string Sesion, string labelSesionesAsistidas, string SesionesAsistidas ,string CI, string st1, string Nombre, string st2, string Apellido, string st3, string Edad, string st4, string FechaNac, string st5, string Carrera, string st6, string Semestre, string st7, string Genero, string st8, string Fobia, string st9, string Antecedentes, string st10, string Sintomas, string st11, string st12)
        {
            //var valor;
            
            int i;
            int j;
            fichero = new SaveFileDialog();
            fichero.Filter = "PDF (*.pdf)|*.pdf";
            if(fichero.ShowDialog() == DialogResult.OK)
            {
                //Crear el documento de tamano tradicional
                Document doc = new Document(PageSize.LETTER);

                //Indicamos donde vamos a guardar el documento.
                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(fichero.FileName, FileMode.Create));
                //Colocamos titulo y autor, no sera visible en el documento
                doc.AddTitle("Información de las sesiones");
                doc.AddCreator("Gabinete Psicológico - EMI UALP");
                //Abrimos el archivo
                doc.Open();

                // Creamos la imagen y le ajustamos el tamaño
                iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance("E:\\header.jpg");
                //iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance("E:\\41SisPhobos - Version con COMBOBOXES HORA GRAFICO\\SisPhobos\\Resources\\header.jpg");
                imagen.BorderWidth = 0;
                imagen.Alignment = Element.ALIGN_CENTER;
                float percentage = 0.0f;
                percentage = 550 / imagen.Width;
                imagen.ScalePercent(percentage * 100);
                // Insertamos la imagen en el documento
                doc.Add(imagen); 

                // Tipo de FONT que vamos a utilizar
                Font _standardFont = new Font(Font.FontFamily.HELVETICA, 8, Font.BOLD, BaseColor.BLACK);

                //Encabezado
                /*doc.Add(new Paragraph("Información de las Sesiones"));
                doc.Add(Chunk.NEWLINE);*/

                doc.Add(new Phrase(labelFechaAct.Trim())); // FechaActual
                doc.Add(new Phrase(fechaActual.Trim())); // FechaActual
                doc.Add(Chunk.NEWLINE);
                doc.Add(new Phrase(labelEncargado.Trim())); // FechaActual
                doc.Add(new Phrase(Encargado.Trim())); //Encargado
                doc.Add(Chunk.NEWLINE);
                doc.Add(new Phrase(labelSesion.Trim())); //Sesión
                doc.Add(new Phrase(Sesion.Trim())); //Sesión
                doc.Add(Chunk.NEWLINE);
                doc.Add(new Phrase(labelSesionesAsistidas.Trim())); //Sesiones Asistidas
                doc.Add(new Phrase(SesionesAsistidas.Trim()));

                doc.Add(Chunk.NEWLINE);
                iTextSharp.text.Image immg = iTextSharp.text.Image.GetInstance("E:\\InfPaciente.png");
                //iTextSharp.text.Image immg = iTextSharp.text.Image.GetInstance("E:\\41SisPhobos - Version con COMBOBOXES HORA GRAFICO\\SisPhobos\\Resources\\InfPaciente.png");
                immg.BorderWidth = 0;
                immg.Alignment = Element.ALIGN_LEFT;
                immg.ScaleAbsolute(200f, 40f);
                // Insertamos la imagen en el documento
                doc.Add(immg);

                
                doc.Add(new Phrase(CI.Trim())); // CI
                doc.Add(new Phrase(st1.Trim())); // CI
                doc.Add(Chunk.NEWLINE);
                doc.Add(new Phrase(Nombre.Trim())); // CI
                doc.Add(new Phrase(st2.Trim())); //Nombre
                doc.Add(Chunk.NEWLINE);
                doc.Add(new Phrase(Apellido.Trim())); //Apellido
                doc.Add(new Phrase(st3.Trim())); //Apellido
                doc.Add(Chunk.NEWLINE);
                doc.Add(new Phrase(Edad.Trim())); //Edad
                doc.Add(new Phrase(st4.Trim())); //Edad
                doc.Add(Chunk.NEWLINE);
                doc.Add(new Phrase(FechaNac.Trim())); //FechaNac
                doc.Add(new Phrase(st5.Trim())); //FechaNac
                doc.Add(Chunk.NEWLINE);
                doc.Add(new Phrase(Carrera.Trim())); //Carrera
                doc.Add(new Phrase(st6.Trim())); //Carrera
                doc.Add(Chunk.NEWLINE);
                doc.Add(new Phrase(Semestre.Trim())); //Semestre
                doc.Add(new Phrase(st7.Trim())); //Semestre
                doc.Add(Chunk.NEWLINE);
                doc.Add(new Phrase(Genero.Trim())); //Genero
                doc.Add(new Phrase(st8.Trim())); //Genero
                doc.Add(Chunk.NEWLINE);
                doc.Add(new Phrase(Fobia.Trim())); //Fobia
                doc.Add(new Phrase(st9.Trim())); //Fobia
                doc.Add(Chunk.NEWLINE);
                doc.Add(new Phrase(Antecedentes.Trim())); //Antecedentes
                doc.Add(new Phrase(st10.Trim())); //Antecedentes
                doc.Add(Chunk.NEWLINE);
                doc.Add(new Phrase(Sintomas.Trim())); //Sintomas
                doc.Add(new Phrase(st11.Trim())); //Sintomas
                doc.Add(Chunk.NEWLINE);

                iTextSharp.text.Image ig = iTextSharp.text.Image.GetInstance("E:\\DatosSesion.png");
                //iTextSharp.text.Image ig = iTextSharp.text.Image.GetInstance("E:\\41SisPhobos - Version con COMBOBOXES HORA GRAFICO\\SisPhobos\\Resources\\DatosSesion.png");
                ig.BorderWidth = 0;
                ig.Alignment = Element.ALIGN_LEFT;
                ig.ScaleAbsolute(140f, 40f);
                // Insertamos la imagen en el documento
                doc.Add(ig);


                //Creamos una tabla que contendrá la información deseada
                PdfPTable tblProductos = new PdfPTable(title.Length);
                tblProductos.WidthPercentage = 100;
                //Configuramos el titulo de las columnas de la tabla
                for( i = 0; i < title.Length; i++)
                {
                    PdfPCell columnas = new PdfPCell(new Phrase(title[i], _standardFont));
                    columnas.BorderWidth = 0;
                    columnas.BorderWidthBottom = 0.75f;
                    //Añadimos las celdas a la tabla
                    tblProductos.AddCell(columnas);
                }

                for( i = 0; i< grd.Rows.Count; i++)
                {
                    for( j = 0; j < grd.Columns.Count; j++)
                    {
                        var valor = true;
                        for (int a = 0; a < colum.Length; a++)
                        {
                            if(j == colum[a])
                            {
                                valor = false;
                            }
                        }
                        if (valor)
                        {
                            PdfPCell producto = new PdfPCell(new Phrase(grd.Rows[i].Cells[j].Value.ToString(), _standardFont));
                            producto.BorderWidth = 0;
                            //Añadimos las celdas a la table
                            tblProductos.AddCell(producto);
                        }
                    }
                   
                }

                doc.Add(tblProductos);


                doc.Add(Chunk.NEWLINE);


                doc.Add(Chunk.NEWLINE);

                iTextSharp.text.Image Obse = iTextSharp.text.Image.GetInstance("E:\\Observaciones.png");
                //iTextSharp.text.Image Obse = iTextSharp.text.Image.GetInstance("E:\\41SisPhobos - Version con COMBOBOXES HORA GRAFICO\\SisPhobos\\Resources\\Observaciones.png");
                Obse.BorderWidth = 0;
                Obse.Alignment = Element.ALIGN_LEFT;
                Obse.ScaleAbsolute(130f, 40f);
                // Insertamos la imagen en el documento
                doc.Add(Obse);

                doc.Add(new Phrase(st12.Trim())); //Observaciones

                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);

                iTextSharp.text.Image Firma = iTextSharp.text.Image.GetInstance("E:\\Firma.png");
                //iTextSharp.text.Image Firma = iTextSharp.text.Image.GetInstance("E:\\41SisPhobos - Version con COMBOBOXES HORA GRAFICO\\SisPhobos\\Resources\\Firma.png");
                Firma.BorderWidth = 0;
                Firma.Alignment = Element.ALIGN_CENTER;
                Firma.ScaleAbsolute(200f, 120f);
                // Insertamos la imagen en el documento
                doc.Add(Firma);

                doc.Close();
                writer.Close();

            }

        }





    }
}
