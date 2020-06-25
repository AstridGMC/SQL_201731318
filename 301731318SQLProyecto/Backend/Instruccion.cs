using _201731318SQLProyecto;
using System;
using System.Collections.Generic;
using MessageBox = System.Windows.Forms.MessageBox;

namespace _301731318SQLProyecto.Backend
{
    public class Instruccion
    {
        List<Tabla> tablas;

        public Instruccion(List<Tabla> tablas)
        {
            this.tablas = tablas;
        }

        public List<Tabla> Tablas1 { get { return tablas; } set { tablas = value; } }

        public bool Eliminar(String nombre, List<Condicion> condicion)
        {
            int i = BuscarIndiceTabla(nombre);
            if (i >= 0)
            {
                if (condicion != null)
                {
                    return true;
                }
                else
                {
                    tablas[i].Filas.Clear();
                    return true;
                }
            }
            else
            {
                MessageBox.Show("No existe la tabla en la base de datos");
            }
            return false;
        }

        public void Insertar(String nombreTabla, List<Dato> datos)
        {
            List<Dato> filaD = datos;
            List<Dato> filaO = new List<Dato>();
            int indice = BuscarIndiceTabla(nombreTabla);
            if (indice >= 0)
            {
                for (int i = 0; i < tablas[indice].Columnas.Count; i++)
                {
                    if (i < filaD.Count)
                    {
                        filaO.Add(filaD[i]);
                    }
                    else
                    {
                        MessageBox.Show("EL NUMERO DE COLUMNAS NO COINCIDE ");
                        Dato dato = new Dato();
                        dato.Dato1 = "";
                    }
                }
                Fila filaN = new Fila();
                filaN.Fila1 = filaO;
                tablas[BuscarIndiceTabla(nombreTabla)].Filas.Add(filaN);
            }
            else
            {
                MessageBox.Show("No existe la tabla en la base de datos");
            }
        }

        public void Actualizar(String nombreTabla, List<Dato> datosActualizar, List<Condicion> condiciones)
        {
            int indiceTabla = BuscarIndiceTabla(nombreTabla);
            if (indiceTabla >= 0)
            {
                Tabla tabla = tablas[indiceTabla];
                for (int k = 0; k < datosActualizar.Count; k++)
                {
                    String columna = datosActualizar[k].Columna;
                    string datoActualizar = datosActualizar[k].Dato1;
                    int columnaIndice = BuscarIndiceColumna(columna, tabla);
                    if (columnaIndice >= 0)
                    {
                        if (condiciones != null)
                        {

                        }
                        else
                        {
                            for (int i = 0; i < tabla.Filas.Count; i++)
                            {
                                tablas[indiceTabla].Filas[i].Fila1[columnaIndice].Dato1 = datoActualizar;
                            }
                        }
                    }

                }
            }
            else
            {
                MessageBox.Show("La tabla " + nombreTabla + "  No existe la tabla en la base de datos");
            }
        }

        public void seleccionarDe1Tabla(string nombreT, List<String> columnas, List<Condicion> condiciones)
        {
            int indiceTabla = BuscarIndiceTabla(nombreT);
            if (indiceTabla >= 0)
            {
                Tabla tabla = tablas[indiceTabla];
                Tabla nuevaTabla = new Tabla();
                List<Dato> columnasTabla = new List<Dato>();

                for (int i = 0; i < tabla.Filas.Count; i++)
                {
                    Fila fila1 = new Fila();
                    if (condiciones == null)
                    {
                        List<Dato> misDatos = new List<Dato>();
                        for (int j = 0; j < columnas.Count; j++)
                        {
                            int indiceColumnas = BuscarIndiceColumna(columnas[j], tabla);
                            if (indiceColumnas >= 0)
                            {
                                if (i == 0)
                                {
                                    columnasTabla.Add(tabla.Columnas[indiceColumnas]);
                                }
                                misDatos.Add(tabla.Filas[i].Fila1[indiceColumnas]);
                            }
                            else
                            {
                                if (i == 0)
                                {
                                    MessageBox.Show("La columna " + columnas[i] + "No existe la tabla en la base de datos");
                                }

                            }
                        }
                        fila1.Fila1 = misDatos;
                        nuevaTabla.Filas.Add(fila1);
                        nuevaTabla.Nombre = nombreT;
                    }
                }
                nuevaTabla.Columnas = columnasTabla;
                GeneradorHtml generador = new GeneradorHtml();
                String txtReporte = generador.GenerarConsultaHTML(nuevaTabla);
                generador.generarHTML(txtReporte, "/SELECCIONAR.html");
            }
            else
            {
                MessageBox.Show("la tabla " + nombreT + " No existe la tabla en la base de datos");
            }
        }


        public int BuscarIndiceTabla(String nombre)
        {
            for (int i = 0; i < tablas.Count; i++)
            {
                if (tablas[0].Nombre.ToLower() == nombre.ToLower())
                {
                    return i;
                }
            }
            return -1;
        }
        public int BuscarIndiceFila(String nombre)
        {
            for (int i = 0; i < tablas.Count; i++)
            {
                if (tablas[i].Nombre.ToLower() == nombre.ToLower())
                {
                    return i;
                }
            }
            return -1;
        }

        public int BuscarIndiceColumna(String nombre, Tabla tabla)
        {
            for (int i = 0; i < tabla.Columnas.Count; i++)
            {
                if (tabla.Columnas[i].Dato1.ToLower() == nombre.ToLower())
                {
                    return i;
                }
            }
            return -1;
        }

        public bool BuscarExistencia(String nombre)
        {
            for (int i = 0; i < tablas.Count; i++)
            {

                if (tablas[i].Nombre.ToLower() == nombre.ToLower())
                {
                    return true;
                }
            }
            return false;
        }
    }
}
