using _201731318SQLProyecto.Backend;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;

namespace _301731318SQLProyecto.Backend
{
    public class Graficador
    {
        String declaracionNodos = "";
        Nodo raiz;
        public Nodo Raiz { get { return raiz; } set { raiz = value; } }
        List<Nodo> nodos = new List<Nodo>();
        public void GenerarGrafica(Nodo raiz)
        {

            try
            {
                String path = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + "/imagen.dot";
                TextWriter escribir = new StreamWriter(path);
                escribir.Write(generarArchivoDot());
                escribir.Close();
                Process cmd = new Process();
                cmd.StartInfo.FileName = "cmd.exe";
                cmd.StartInfo.RedirectStandardInput = true;
                cmd.StartInfo.RedirectStandardOutput = true;
                cmd.StartInfo.CreateNoWindow = true;
                cmd.StartInfo.UseShellExecute = false;
                cmd.Start();

                cmd.StandardInput.WriteLine("dot " + path + " -o ejemplo1.png -Tpng");
                cmd.StandardInput.Flush();
                cmd.StandardInput.Close();
                cmd.WaitForExit();

                Process.Start(System.IO.Directory.GetCurrentDirectory() + "/ejemplo1.png");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            //generarImagen(generarArchivoDot());

        }
        public String generarArbol()
        {
            return "";
        }
        int id = 0;
        public void listarNodos(Nodo nodo, Nodo padre)
        {
            if (nodo != null)
            {
                if (nodo.Hijos.Count > 0)
                {
                    if (nodo.Padre != null)
                    {
                        nodos.Add(nodo);
                        //Console.WriteLine(nodo.Estado + "  ---------" + nodo.Padre.Estado + "------- id padre" + nodo.Padre.IdNodo );
                    }
                    else
                    {
                        nodos.Add(nodo);
                        id++;
                    }

                    for (int i = 0; i < nodo.Hijos.Count; i++)
                    {
                        listarNodos(nodo.Hijos[i], nodo);
                    }
                }
                else
                {
                    if (nodo.Padre != null)
                    {
                        nodos.Add(nodo);
                        nodo.Padre = padre;
                        // Console.WriteLine("hijo " + nodo.Estado + "  -------x----padre - " + nodo.Padre.Estado + "---------- " + nodo.Hijos.Count);

                    }
                    else
                    {
                        nodos.Add(nodo);
                        nodo.Padre = padre;
                        // Console.WriteLine("hijo " + nodo.Estado + "  ------- ----padre - " + "   " + "-------- " + nodo.Hijos.Count);
                    }
                }
            }
        }

        public void identificarNodos()
        {
            for (int i = 0; i < nodos.Count; i++)
            {
                Nodo nodo = nodos[i];
                declaracionNodos = declaracionNodos + "\n node" + nodo.IdNodo + "  [label= \"" + quitarComilas(nodo.Estado) + "\" ];  \n ";
            }
        }
        string flecha = "->";
        public String enlazarNodos()
        {
            String enlazados = "";
            for (int i = 0; i < nodos.Count; i++)
            {
                Nodo nodo = nodos[i];
                if (nodo.Padre != null)
                {
                    // enlazados = enlazados + "" + nodo.Padre.Estado+ " " + flecha + " " + nodo.IdNodo + "\"; \n ";

                    enlazados = enlazados + "node" + nodo.Padre.IdNodo + " " + flecha + " node" + nodo.IdNodo + "; \n ";
                }
                else
                {

                }
            }
            return enlazados;
        }

        public String quitarComilas(String palabra)
        {
            if (palabra.Contains('"'))
            {
                palabra.Replace('"', ' ');
            }
            else if (palabra.Contains('”'))
            {
                palabra.Replace('”', ' ');
            }
            else if (palabra.Contains('\''))
            {
                palabra.Replace('\'', ' ');
            }
            else
            {
                return palabra;
            }
            return palabra.Replace('"', ' ').Replace('\'', ' ');
        }

        public String generarArchivoDot()
        {
            Console.WriteLine("generando dotrrr");
            String texto = "digraph {\n  edge[dir = none];\n graph[ordering = \"out\"]; \n" +
                "node [shape = circle, style=\"filled\",fillcolor=\"lightskyblue\", fontcolor= \"black\"];";
            listarNodos(raiz, null);
            identificarNodos();
            texto = texto + declaracionNodos;
            texto = texto + enlazarNodos() + "   rankdir = UD; \n}  ";
            return texto;
        }

        public void generarImagen(string dot)
        {
            string executable = @".\external\dot.exe";
            string output = @".\external\tempgraph";
            File.WriteAllText(output, dot);

            System.Diagnostics.Process process = new System.Diagnostics.Process();

            // Stop the process from opening a new window
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;

            // Setup executable and parameters
            process.StartInfo.FileName = executable;
            process.StartInfo.Arguments = string.Format(@"{0} -Tjpg -O", output);

            // Go
            process.Start();
            // and wait dot.exe to complete and exit
            process.WaitForExit();
            Bitmap bitmap = null; ;
            using (Stream bmpStream = System.IO.File.Open(output + ".jpg", System.IO.FileMode.Open))
            {
                Image image = Image.FromStream(bmpStream);
                bitmap = new Bitmap(image);
            }
            File.Delete(output);
            File.Delete(output + ".jpg");
        }
    }
}
