using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _301731318SQLProyecto.Backend.Parser_y_Scanner
{
    class ErrorSintactico
    {

        Token token;
        String mensaje;

        public Token Token2{
            get { return token; }
            set { token = value; }
        }

        public ErrorSintactico(Token token, string mensaje)
        {
            this.token = token;
            this.mensaje = mensaje;
        }
    }
}
