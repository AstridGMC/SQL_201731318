using _201731318SQLProyecto.Backend.Parser_y_Scanner;
using System;
using System.Collections.Generic;

namespace _201731318SQLProyecto.Backend
{
    public class Nodo
    {
        public static int id;
        Nodo padre;
        List<Nodo> hijos;
        Token token;
        String estado;
        String idNodo;

        public Nodo()
        {
            idNodo = id.ToString();
            id++;
            hijos = new List<Nodo>();
        }

        public Nodo(Nodo padre, Token token, string estado)
        {
            hijos = new List<Nodo>();
            this.padre = padre;
            this.token = token;
            this.estado = estado;
            idNodo = id.ToString();
            id++;
        }

        public Nodo Padre { get { return padre; } set { padre = value; } }

        public List<Nodo> Hijos { get { return hijos; } set { hijos = value; } }

        public Token TokenN { get { return token; } set { token = value; } }

        public String Estado { get { return estado; } set { estado = value; } }

        public String IdNodo { get { return idNodo; } set { idNodo = value; } }
    }
}
