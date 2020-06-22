using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using _301731318SQLProyecto.Backend.Parser_y_Scanner;

namespace _301731318SQLProyecto
{
    public partial class Principal : Form
    {

        String textoAnalizar;
        String PathTextoConsultas;
        List<Token> tokens;
        List<Token>  tokensAnalizar;
        public Principal()
        {
            InitializeComponent();
        }

        private void mOSTRARTOKENToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void aCERCADEToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void mANUALDEUSUARIOToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void aYUDAToolStripMenuItem_Click_1(object sender, EventArgs e)
        {

        }

        private void mmToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_AutoSizeChanged(object sender, EventArgs e)
        {

        }

        private void sALIRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        public void AgregarPanel(Object panel)
        {
            if (this.panelPrincipal.Controls.Count > 0)
            {
                this.panelPrincipal.Controls.RemoveAt(0);
            }
            Form myFormulario = panel as Form;
            myFormulario.TopLevel = false;
            myFormulario.Dock = DockStyle.Fill;
            this.panelPrincipal.Controls.Add(myFormulario);
            this.panelPrincipal.Tag = myFormulario;
            myFormulario.Show();
        }

        public void LimpiarPanel(Object panel)
        {
            if (this.panelPrincipal.Controls.Count > 0)
            {
                this.panelPrincipal.Controls.RemoveAt(0);
            }
        }
        Escritor escritorsql;
        private void nUEVOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (escritorsql !=null)
            {
                DialogResult opcion = MessageBox.Show("desea guardar los cambios realizados en el archivo?", "ALERTA", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (opcion == DialogResult.Yes)
                {
                    TextWriter escribir = new StreamWriter(PathTextoConsultas);
                    Console.WriteLine(escritorsql.TEXTO() + "liiiiii");
                    escribir.Write(escritorsql.TEXTO());
                    escritorsql = new Escritor();
                    AgregarPanel(escritorsql);
                    escribir.Close();
                }
                else
                {
                    escritorsql = new Escritor();
                    AgregarPanel(escritorsql);
                }
            }
            else
            {
                escritorsql = new Escritor();
                AgregarPanel(escritorsql);
            }
            
            
            
        }

        private void cARGARTABLASToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog abreArchivo = new OpenFileDialog();
            abreArchivo.Filter = "text|*.sqle";
            string path = abreArchivo.FileName;

        }

        private void aBRIRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog abreArchivo = new OpenFileDialog();
            abreArchivo.Filter = "text|*.agmc";
            if (abreArchivo.ShowDialog() == DialogResult.OK)
            {
                TextReader lector = new StreamReader(abreArchivo.FileName);
                MessageBox.Show("EL ARCCHIVO SE HA LEIDO CON EXITO");
                PathTextoConsultas = abreArchivo.FileName;
                textoAnalizar = lector.ReadToEnd();
                escritorsql = new Escritor(textoAnalizar);
                AgregarPanel(escritorsql);
                lector.Close();
            }
        }

        private void vERTABLASToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void gUARDARCOMOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "sql| *.sqle |agmc | *.agmc";
            saveFileDialog1.Title = "GUARDAR COMO";
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName.Length>0)
            {
               
                TextWriter escribir = new StreamWriter(saveFileDialog1.FileName);
                Console.WriteLine(escritorsql.TEXTO() + "liiiiii" );
                escribir.Write(escritorsql.TEXTO());
                escritorsql = new Escritor();
                AgregarPanel(escritorsql);
                escribir.Close();
            }
        }

        private void hERRAMIENTASToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void aNALIZARToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (escritorsql.TEXTO()!=null)
                {
                    AnalizadorLexico lexico = new AnalizadorLexico();
                    //AnalizadorSintactico sintaxis = new AnalizadorSintactico();
                    tokens = lexico.ObtenerTokens(escritorsql.TEXTO());
                    tokensAnalizar = lexico.tokensAnalizar;
                    Console.WriteLine("\n tama;o de tokens analizar "+ tokensAnalizar.Count);
                   
                    
                    for (int i = 0; i < tokens.Count(); i++)
                    {
                        Token miToken = tokens[i];
                        Console.WriteLine("Lexema: " + miToken.Lexema +" Token: "+miToken.Token1+ " Tipo: " + miToken.Tipo + "  Color" + miToken.Color + "  Fila" + miToken.Fila + "  Columna" + miToken.Columna);
                    }
                    Console.WriteLine("\n ========iniciando analizador sintactico=============" );
                   
                    escritorsql.InsertarTexto(lexico.TokensArchivo);
                   // tokensAnalizar.Clear();
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Debe Cargar un archivo primero");
            }
            AnalizadorSintactico sintactico = new AnalizadorSintactico(tokensAnalizar);
            sintactico.analizar();

        }

        private void gUARDARToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog abreArchivo = new OpenFileDialog();
                TextWriter escribir = new StreamWriter(PathTextoConsultas);
                Console.WriteLine(escritorsql.TEXTO() + "liiiiii");
                escribir.Write(escritorsql.TEXTO());
                escribir.Close();
            }
            catch (Exception ex)
            {

            }
           
            
        }

        
    }
}
