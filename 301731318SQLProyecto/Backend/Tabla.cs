using System;
using System.Collections.Generic;
namespace _201731318SQLProyecto
{
    public class Tabla
    {
        List<Fila> filas;
        List<Dato> columnas;
        String nombre;

        public Tabla()
        {
            filas = new List<Fila>();
            columnas = new List<Dato>();
        }

        public List<Fila> Filas
        {
            get { return filas; }
            set { filas = value; }
        }
        public List<Dato> Columnas
        {
            get { return columnas; }
            set { columnas = value; }
        }
        public String Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
    }
}
