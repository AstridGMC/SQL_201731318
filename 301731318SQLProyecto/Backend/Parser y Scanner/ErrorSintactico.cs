using System;
using System.Collections.Generic;

namespace _201731318SQLProyecto.Backend.Parser_y_Scanner
{
    public class ErrorSintactico
    {

        Token token;
        String mensaje;
        List<Token> descartados;
        public Token Token2
        {
            get { return token; }
            set { token = value; }
        }
        public String Mensaje
        {
            get { return mensaje; }
            set { mensaje = value; }
        }

        public List<Token> Descartados
        {
            get { return descartados; }
            set { descartados = value; }
        }

        public ErrorSintactico(Token token, string mensaje)
        {
            this.token = token;
            this.mensaje = mensaje;
        }
    }
}
