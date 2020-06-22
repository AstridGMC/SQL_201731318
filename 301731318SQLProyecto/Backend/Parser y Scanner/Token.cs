using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Navigation;

namespace _301731318SQLProyecto.Backend.Parser_y_Scanner
{
    public class Token
    {
        private String lexema;
        private int token;
        private String tipo;
        private int columna;
        private int fila;
        private Color color;
        public String Lexema { get { return lexema; } set { lexema = value; } }
        public int Token1 { get { return token; } set { token = value; } }
        public String Tipo { get { return tipo; } set { tipo = value; } }
        public int Columna { get { return columna; } set { columna = value; } }
        public int Fila { get { return fila; } set { fila = value; } }
        public Color Color { get { return color; } set { color = value; } }

        public Token(string lexema, string tipo, int columna, int fila1)
        {
            this.lexema = lexema;
            this.tipo = tipo;
            this.columna = columna;
            this.fila = fila1;
        }

        public int NombrarToken(String estado)
        {
            if (estado == "Identificador")
            {
                return 1;
            } else if (estado == "Comentario de Una Linea")
            {
                return 2;
            } else if (estado == "Comentario Multilinea")
            {
                return 3;
            } else if (estado == "Decimal")
            {
                return 4;
            }
            else if (estado == "Entero")
            {
                return 5;
            }
            else if (estado == "ParentesisAbierto")
            {
                return 6;
            }
            else if (estado == "ParentesisCerrado")
            {
                return 7;
            }
            else if (estado == "SignoIgual")
            {
                return 8;
            }
            else if (estado == "MenorQue")
            {
                return 9;
            }
            else if (estado == "MayorQue")
            {
                return 10;
            }
            else if (estado == "MayorIgual")
            {
                return 11;
            }
            else if (estado == "MenorIgual")
            {
                return 12;
            }
            else if (estado == "NoIgual")
            {
                return 10;
            }
            else if (estado == "Coma")
            {
                return 14;
            }
            else if (estado == "PuntoYComa")
            {
                return 15;
            }
            else if (estado == "Cadena")
            {
                return 16;
            }
            else if (estado == "Fecha")
            {
                return 17;
            }
            else if (estado == "Asterisco")
            {
                return 18;
            }
            else if (estado == "Punto")
            {
                return 19;
            }
            else
            {
                return 0;
            }
        }

        public Color ColorearToken(String estado)
        {
            if (estado == "Identificador")
            {
                return Color.Brown;
            }
            else if (estado == "PalabraReservada1")
            {
                return Color.MediumPurple;
            }
            else if (estado == "Comentario de Una Linea")
            {
                return Color.LightSlateGray;
            }
            else if (estado == "Comentario Multilinea")
            {
                return Color.LightSlateGray;
            }
            else if (estado == "Decimal")
            {
                return Color.AliceBlue;
            }
            else if (estado == "Entero")
            {
                return Color.DeepSkyBlue;
            }
            else if (estado == "SignoIgual")
            {
                return Color.Red;
            }
            else if (estado == "MenorQue")
            {
                return Color.Red;
            }
            else if (estado == "MayorQue")
            {
                return Color.Red;
            }
            else if (estado == "MayorIgual")
            {
                return Color.Red;
            }
            else if (estado == "MenorIgual")
            {
                return Color.Red;
            }
            else if (estado == "NoIgual")
            {
                return Color.Red;
            }
            else if (estado == "Fecha")
            {
                return Color.Orange;
            }
            else if (estado == "Cadena")
            {

                return Color.Green;
            }

            else
            {
                return Color.Black;
            }
        }
        public bool IsPalabraReservada1(string palabra)
        {
            if (palabra.ToLower() == "insertar")
            {
                return true;
            }
            else if (palabra.ToLower() == "actualizar")
            {
                return true;
            }
            else if (palabra.ToLower() == "eliminar")
            {
                return true;
            }
            else if (palabra.ToLower() == "tabla")
            {
                return true;
            }
            else if (palabra.ToLower() == "modificar")
            {
                return true;
            }
            else if (palabra.ToLower() == "seleccionar")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public int NumerarPalabraReservada1(string palabra)
        {
            if (palabra.ToLower() == "insertar")
            {
                return 21;
            }

            else if (palabra.ToLower() == "seleccionar")
            {
                Console.WriteLine("seleccionar");
                return 22;
            }
            else if (palabra.ToLower() == "eliminar")
            {
                return 23;
            }
            else if (palabra.ToLower() == "actualizar")
            {
                return 24;
            }
            else if (palabra.ToLower() == "tabla")
            {
                return 26;
            }
            else
            {
                return -1;
            }
        }
        public bool IsPalabraReservada2(string palabra)
        {
            
            if (palabra.ToLower() == "establecer")
            {
                return true;
            }
            else if (palabra.ToLower() == "crear")
            {
                return true;
            }
            else if (palabra.ToLower() == "donde")
            {
                return true;
            }
            else if (palabra.ToLower() == "y" || palabra.ToLower() == "o")
            {
                return true;
            }
            else if (palabra.ToLower() == "en")
            {
                return true;
            }
            else if (palabra.ToLower() == "valores")
            {
                return true;
            }
            else if (palabra.ToLower() == "de")
            {
                return true;
            }
            else if (palabra.ToLower() == "como")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public int NumerarPalabraReservada2(string palabra)
        {
            if (palabra.ToLower() == "establecer")
            {
                return 28;
            }
            else if (palabra.ToLower() == "crear")
            {
                return 20;
            }
            else if (palabra.ToLower() == "donde")
            {
                return 29;
            }
            else if (palabra.ToLower() == "y")
            {
                return 30;
            }
            else if (palabra.ToLower() == "o")
            {
                return 31;
            }
            else if (palabra.ToLower() == "en")
            {
                return 31;
            }
            else if (palabra.ToLower() == "valores")
            {
                return 27;
            }
            else if (palabra.ToLower() == "de")
            {

                return 25;
            }
            else if (palabra.ToLower() == "como")
            {
                return 32;
            }
            else
            {
                return -1;
            }
        }

        public Boolean  isTipoDato(String token)
        {
            String miToken = token.ToLower();
            switch (miToken)
            {
                case "fecha":
                    return true;
                case "cadena":
                    return true;
                case "flotante":
                    return true;
                case "entero":
                    return true;
            }
            return false;
        }

        public int  NumerarTipoDato(String token)
        {
            String miToken = token.ToLower();
            switch (miToken)
            {
                case "fecha":
                    return 35;
                case "cadena":
                    return 32;
                case "flotante":
                    return 33;
                case "entero":
                    return 34;
            }
            return -1;
        }

    }
}

