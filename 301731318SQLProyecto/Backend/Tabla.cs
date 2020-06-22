using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _301731318SQLProyecto
{
    class Tabla
    {
        List<Fila> filas;
        List<Dato> columnas;
        String nombre;
        public List<Fila> Filas { 
            get { return filas; }
            set { filas = value; }
        }
        public List<Dato> Columnas {
            get { return columnas; }
            set { columnas = value; }
        }
        public String Nombre {
            get { return nombre; }
            set { nombre = value; } 
        }


    }
}
