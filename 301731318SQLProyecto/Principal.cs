using _201731318SQLProyecto.Backend;
using _201731318SQLProyecto.Backend.Parser_y_Scanner;
using _301731318SQLProyecto;
using _301731318SQLProyecto.Backend;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace _201731318SQLProyecto
{
    public partial class Principal : Form
    {

        String textoAnalizar;
        String PathTextoConsultas;
        List<Token> tokens;
        List<Token> tokensAnalizar;
        List<Token> errores;
        List<ErrorSintactico> erroresSintacticos;
        List<Tabla> tablas = new List<Tabla>();
        Graficador graficador = new Graficador();
        public Principal()
        {
            InitializeComponent();
        }

        private void mOSTRARTOKENToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GeneradorHtml generador = new GeneradorHtml();
            generador.GenerarHtmlTablas(tokensAnalizar, errores);
        }

        private void aCERCADEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string text = "Vercion 1.0 \n  \n Nombre: Astrid Gabriela Martinez Castillo \n Carnet: 201731318";
            MessageBox.Show(text);
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
            if (escritorsql != null)
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
            if (abreArchivo.ShowDialog() == DialogResult.OK)
            {
                string path = abreArchivo.FileName;
                TextReader lector = new StreamReader(abreArchivo.FileName);
                PathTextoConsultas = abreArchivo.FileName;
                textoAnalizar = lector.ReadToEnd();
                escritorsql = new Escritor(textoAnalizar);
                AgregarPanel(escritorsql);
                lector.Close();
            }
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
            if (tablas != null)
            {
                if (tablas.Count > 0)
                {
                    GeneradorHtml generador = new GeneradorHtml();
                    String tablashtml = generador.GenerarReporteTablas(tablas);
                    generador.generarHTML(tablashtml, "tablas.html");
                }
                else
                {
                    MessageBox.Show("No Cuenta Con Tablas Cargadas en este momento");
                }
            }
        }

        private void gUARDARCOMOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "sql| *.sqle |agmc | *.agmc";
            saveFileDialog1.Title = "GUARDAR COMO";
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName.Length > 0)
            {

                TextWriter escribir = new StreamWriter(saveFileDialog1.FileName);
                Console.WriteLine(escritorsql.TEXTO() + "liiiiii");
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

                if (escritorsql.TEXTO() != null)
                {
                    String miText = escritorsql.TEXTO();
                    if (escritorsql.TEXTOSELECCIONADO().Length > 0)
                    {
                        miText = escritorsql.TEXTOSELECCIONADO();
                    }
                    AnalizadorLexico lexico = new AnalizadorLexico();
                    //AnalizadorSintactico sintaxis = new AnalizadorSintactico();
                    tokens = lexico.ObtenerTokens(miText);
                    tokensAnalizar = lexico.tokensAnalizar;
                    errores = lexico.Errores;
                    Console.WriteLine("\n tama;o de tokens analizar " + tokensAnalizar.Count);
                    if (errores.Count() > 0)
                    {
                        vERTABLASToolStripMenuItem.Enabled = false;
                    }
                    else
                    {
                        vERTABLASToolStripMenuItem.Enabled = true;
                    }

                    for (int i = 0; i < tokens.Count(); i++)
                    {
                        Token miToken = tokens[i];
                        Console.WriteLine("Lexema: " + miToken.Lexema + " Token: " + miToken.Token1 + " Tipo: " + miToken.Tipo + "  Color" + miToken.Color + "  Fila" + miToken.Fila + "  Columna" + miToken.Columna);
                    }
                    Console.WriteLine("\n ========iniciando analizador sintactico=============");
                    AnalizadorSintactico sintactico = new AnalizadorSintactico(tokensAnalizar);
                    sintactico.Tablas = tablas;
                    sintactico.analizar();
                    erroresSintacticos = sintactico.Errores;
                    escritorsql.InsertarErrores(erroresSintacticos);
                    Arbol arbol = new Arbol();
                    arbol.ImprimirNodos(sintactico.Principal, 1);

                    graficador.Raiz = sintactico.Principal;
                    // tokensAnalizar.Clear();
                    escritorsql.InsertarTexto(lexico.TokensArchivo);
                    if (erroresSintacticos.Count > 0 || errores.Count > 0)
                    {
                        vERTABLASToolStripMenuItem.Enabled = false;
                        bntArbol.Enabled = false;
                    }
                    else
                    {
                        vERTABLASToolStripMenuItem.Enabled = true;
                        bntArbol.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Debe Cargar un archivo primero");
                Console.WriteLine(ex);
            }


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
                Console.WriteLine(ex);
            }


        }

        private void mOSTRARERRORESToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (erroresSintacticos != null && errores != null)
            {
                GeneradorHtml generador = new GeneradorHtml();
                String erroreshtml = generador.EscribirErroresSintacticosHtml(errores, erroresSintacticos);
                generador.generarTablasErroresHTML(erroreshtml);
            }
        }

        private void mANUALTECNICOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String manualtec = "/ManualTecnicoSQL.pdf";
            GeneradorHtml generador = new GeneradorHtml();
            generador.abirPDF(manualtec);
        }

        private void eJECUTARToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                if (escritorsql.TEXTO() != null)
                {
                    String miText = escritorsql.TEXTO();
                    if (escritorsql.TEXTOSELECCIONADO().Length > 0)
                    {
                        miText = escritorsql.TEXTOSELECCIONADO();
                    }
                    AnalizadorLexico lexico = new AnalizadorLexico();
                    //AnalizadorSintactico sintaxis = new AnalizadorSintactico();
                    tokens = lexico.ObtenerTokens(miText);
                    tokensAnalizar = lexico.tokensAnalizar;
                    errores = lexico.Errores;
                    Console.WriteLine("\n tama;o de tokens analizar " + tokensAnalizar.Count);
                    if (errores.Count() > 0)
                    {
                        vERTABLASToolStripMenuItem.Enabled = false;
                    }
                    else
                    {
                        vERTABLASToolStripMenuItem.Enabled = true;
                    }

                    for (int i = 0; i < tokens.Count(); i++)
                    {
                        Token miToken = tokens[i];
                        Console.WriteLine("Lexema: " + miToken.Lexema + " Token: " + miToken.Token1 + " Tipo: " + miToken.Tipo + "  Color" + miToken.Color + "  Fila" + miToken.Fila + "  Columna" + miToken.Columna);
                    }
                    Console.WriteLine("\n ========iniciando analizador sintactico=============");
                    AnalizadorSintactico sintactico = new AnalizadorSintactico(tokensAnalizar);
                    sintactico.Tablas = tablas;
                    sintactico.analizar();
                    erroresSintacticos = sintactico.Errores;
                    escritorsql.InsertarErrores(erroresSintacticos);
                    Arbol arbol = new Arbol();
                    arbol.ImprimirNodos(sintactico.Principal, 1);
                    // tokensAnalizar.Clear();
                    escritorsql.InsertarTexto(lexico.TokensArchivo);
                    graficador.Raiz = sintactico.Principal;
                    if (erroresSintacticos.Count > 0 || errores.Count > 0)
                    {
                        vERTABLASToolStripMenuItem.Enabled = false;
                        bntArbol.Enabled = false;
                    }
                    else
                    {
                        vERTABLASToolStripMenuItem.Enabled = true;
                        bntArbol.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Debe Cargar un archivo primero");
                Console.WriteLine(ex);
            }

        }

        private void bntArbol_Click(object sender, EventArgs e)
        {
            graficador.GenerarGrafica(graficador.Raiz);
        }
    }
}


