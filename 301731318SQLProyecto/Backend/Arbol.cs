using System;

namespace _201731318SQLProyecto.Backend
{
    class Arbol
    {
        public void ImprimirNodos(Nodo nodo, int a)
        {
            if (nodo != null)
            {
                if (nodo.Hijos.Count > 0)
                {
                    if (nodo.Padre != null)
                    {
                        Console.WriteLine(nodo.Estado + "  ---------" + nodo.Padre.Estado + "------- id " + nodo.IdNodo);
                    }

                    for (int i = 0; i < nodo.Hijos.Count; i++)
                    {
                        ImprimirNodos(nodo.Hijos[i], i);
                    }
                }
                else
                {
                    if (nodo.Padre != null)
                    {
                        Console.WriteLine("hijo " + nodo.Estado + "  -----padre - " + nodo.Padre.Estado + "--------- " + nodo.Hijos.Count + "  id " + nodo.IdNodo);

                    }
                    else
                    {
                        Console.WriteLine("hijo " + nodo.Estado + "  -------padre - " + "   " + "-------- " + nodo.Hijos.Count + "  id " + nodo.IdNodo);
                    }
                }
            }
        }
    }
}
