using _201731318SQLProyecto;
using _201731318SQLProyecto.Backend.Parser_y_Scanner;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace _301731318SQLProyecto
{
    class GeneradorHtml

    {

        public void abirPDF(string ruta)
        {
            Console.WriteLine(System.IO.Directory.GetCurrentDirectory());
            String dir = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + ruta;
            Process.Start(dir);
        }

        public void GenerarHtmlTablas(List<Token> tokens, List<Token> errores)
        {

            Console.WriteLine("Prueba");
            string path = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + "/Tokens.html";
            TextWriter escribir = new StreamWriter(path);
            escribir.Write(GenerarHTMLtokensTXT(tokens, errores));
            escribir.Close();
            Console.WriteLine(path);
            System.Diagnostics.Process.Start("Chrome.exe", path);

        }

        public void generarTablasErroresHTML(String tablas)
        {
            Console.WriteLine("Prueba");
            string path = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + "/errores.html";
            TextWriter escribir = new StreamWriter(path);
            escribir.Write(tablas);
            escribir.Close();
            Console.WriteLine(path);
            System.Diagnostics.Process.Start("Chrome.exe", path);
        }

        public void generarHTML(String tablas, String nombre)
        {
            string path = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + nombre;
            TextWriter escribir = new StreamWriter(path);
            escribir.Write(tablas);
            escribir.Close();
            Console.WriteLine(path);
            System.Diagnostics.Process.Start("Chrome.exe", path);
        }


        public String GenerarHTMLtokensTXT(List<Token> tokens, List<Token> errores)
        {
            String cuerpo = "<!DOCTYPE HTML> <html > <head > <title > TOKENS Y ERRORES</title>" +
                "<meta charset = \"utf-8\" >" +
                "<meta name = \"viewport\" content = \"width=device-width, initial-scale=1\" >" +
            "<link rel = \"stylesheet\" href = \"https://maxcdn.bootstrapcdn.com/bootstrap/4.5.0/css/bootstrap.min.css\">" +
            " <script src = \"https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js\" ></script >" +
            "<script src = \"https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.16.0/umd/popper.min.js\" ></script >" +
            "<script src = \"https://maxcdn.bootstrapcdn.com/bootstrap/4.5.0/js/bootstrap.min.js\" ></script >" +
            "</tr </head >\n <body>" +
            "</tr></tr > \n<h1>TOKENS </h1> </tr > \n " +
            "<table class=\"table table-bordered\">\n" +
                "<thead class=\"thead - dark\">" +
                    "<tr > \n" +
                        "<th scope = \"col\" >Token</th>\n" +
                        "<th scope = \"col\" > Lexema </ th > \n" +
                        "<th scope = \"col\" > tipo </ th >\n" +
                        " <th scope = \"col\" > Columna </ th >\n" +
                        " <th scope = \"col\" > Fila </ th >\n" +
                    "</tr >\n" +
                "</thead >\n" +
            " <tbody>";

            for (int i = 0; i < tokens.Count; i++)
            {
                Token token = tokens[i];
                cuerpo = cuerpo +
                    "<tr> " +
                        "<td> " + token.Token1 + " </td > \n" +
                        "<td > " + token.Lexema + " </td >\n" +
                        "<td > " + token.Tipo + " </td >\n" +
                        "<td > " + token.Columna + " </td>\n" +
                        "<td >" + token.Fila + " </td>\n" +
                    "</ tr > ";
            }

            cuerpo = cuerpo +
                "</tbody>\n" +
                "</table>\n " +
                "</tr ></ tr >\n " +
                "<h1>Error</h1> </tr >\n" +
                " <table class=\"table table-bordered\">\n " +
                "<thead class=\"thead - dark\"> \n" +
                "<tr > "
                + "<th scope = \"col\" > tipo </th >  \n" +
                "<th scope = \"col\" > Lexema </th >  \n" +
                "<th scope = \"col\" > Fila </th >  \n" +
                "<th scope = \"col\" > Columna </th > \n " +
                "</tr > \n" +
                "</ thead > \n" +
                " <tbody>  \n";
            for (int i = 0; i < errores.Count; i++)
            {
                Token token = tokens[i];
                cuerpo = cuerpo +
                    "  <tr > " +
                        "<td > " + token.Lexema + " </td >  \n" +
                        "<td > error Lexico </td >\n" +
                        "<td > " + token.Columna + " </td > \n" +
                        "<td > " + token.Fila + " </td > \n" +
                    "</tr > ";
            }
            cuerpo = cuerpo +
                "</tbody>" +
                "</table> " +
                "</tr ></tr >" +
                "</body>" +
                "</html>";
            return cuerpo;
        }


        public String EscribirErroresSintacticosHtml(List<Token> erroresLexicos, List<ErrorSintactico> erroesSintacticos)
        {

            String cuerpo = "<!DOCTYPE HTML> <html > <head > <title > TOKENS Y ERRORES</title>" +
                "<meta charset = \"utf-8\" >" +
                "<meta name = \"viewport\" content = \"width=device-width, initial-scale=1\" >" +
            "<link rel = \"stylesheet\" href = \"https://maxcdn.bootstrapcdn.com/bootstrap/4.5.0/css/bootstrap.min.css\">" +
            " <script src = \"https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js\" ></script >" +
            "<script src = \"https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.16.0/umd/popper.min.js\" ></script >" +
            "<script src = \"https://maxcdn.bootstrapcdn.com/bootstrap/4.5.0/js/bootstrap.min.js\" ></script >" +
            "</tr </head >\n <body>" +
            "</tr></tr > \n<h1>ERRORES LEXICOS </h1> </tr > \n " +
            "<table class=\"table table-bordered\">\n" +
                "<thead class=\"thead-dark\">" +
                    "<tr > \n" +
                        "<th scope = \"col\" > tipo </th >  \n" +
                        "<th scope = \"col\" > Lexema </th >  \n" +
                        "<th scope = \"col\" > Fila </th >  \n" +
                        "<th scope = \"col\" > Columna </th > \n " +
                    "</tr >\n" +
                "</thead >\n" +
            " <tbody>";
            for (int i = 0; i < erroresLexicos.Count; i++)
            {
                Token token = erroresLexicos[i];
                cuerpo = cuerpo +
                    "  <tr > " +
                        "<td > error Lexico </td >\n" +
                        "<td > " + token.Lexema + " </td >  \n" +
                        "<td > " + token.Columna + " </td > \n" +
                        "<td > " + token.Fila + " </td > \n" +
                    "</tr > ";
            }

            cuerpo = cuerpo +
                "</tbody>\n" +
                "</tr ></ tr >\n " +
                "</table>\n " +
                "</tr ></ tr >\n " +
                "<h1>ERRORES SINTACTICOS</h1> </tr >\n" +
                " <table class=\"table table-bordered\">\n " +
                "<thead class=\"thead-dark\"> \n" +
                "<tr> "
                + "<th scope = \"col\" > tipo </th >  \n" +
                "<th scope = \"col\" > lexema </th >  \n" +
                "<th scope = \"col\" > Fila </th >  \n" +
                "<th scope = \"col\" > Columna </th > \n " +
                "<th scope = \"col\" > Mensaje </th > \n " +
                "<th scope = \"col\" > Descartados </th > \n " +
                "</tr > \n" +
                "</ thead > \n" +
                " <tbody>  \n";
            for (int i = 0; i < erroesSintacticos.Count; i++)
            {
                ErrorSintactico error = erroesSintacticos[i];
                cuerpo = cuerpo +
                    "  <tr > " +
                        "<td > " + "Sintactico" + " </td >  \n" +
                        "<td > " + error.Token2.Lexema + " </td >\n" +
                        "<td > " + error.Token2.Columna + " </td > \n" +
                        "<td > " + error.Token2.Fila + " </td > \n" +
                        "<td > " + error.Mensaje + " </td > \n" +
                        "<td > ";
                for (int j = 0; j < error.Descartados.Count; j++)
                {
                    cuerpo = cuerpo + " <li>" + error.Descartados[j].Lexema + " Fila " + error.Descartados[j].Fila + " Columna " + error.Descartados[j].Columna + "</li>";
                }
                cuerpo = cuerpo + " </td > \n" +
                    "</tr > ";
            }
            cuerpo = cuerpo +
               "</tbody>" +
               "</table> " +
               "</tr ></tr >" +
               "</body>" +
               "</html>";
            return cuerpo;
        }

        public String GenerarReporteTablas(List<Tabla> tablas)
        {
            int columnas;
            String cuerpo = "<!DOCTYPE HTML> <html > <head > <title > Tablas </title>" +
               "<meta charset = \"utf-8\" >" +
               "<meta name = \"viewport\" content = \"width=device-width, initial-scale=1\" >" +
           "<link rel = \"stylesheet\" href = \"https://maxcdn.bootstrapcdn.com/bootstrap/4.5.0/css/bootstrap.min.css\">" +
           " <script src = \"https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js\" ></script >" +
           "<script src = \"https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.16.0/umd/popper.min.js\" ></script >" +
           "<script src = \"https://maxcdn.bootstrapcdn.com/bootstrap/4.5.0/js/bootstrap.min.js\" ></script >" +
           "</tr </head >\n <body>";
            for (int i = 0; i < tablas.Count; i++)
            {
                Tabla tabla = tablas[i];
                cuerpo = cuerpo + "</tr></tr > \n<h1>" + tabla.Nombre + " </h1> </tr > \n " +
               "<table class=\"table table-bordered\">\n" +
                   "<thead class=\"thead - dark\">" +
                       "<tr > \n";
                columnas = tabla.Columnas.Count;
                for (int j = 0; j < tabla.Columnas.Count; j++)
                {
                    Dato dato = tabla.Columnas[j];
                    cuerpo = cuerpo + "<td > " + dato.Dato1 + " (" + dato.Tipo + ") " + " </td >\n";
                }
                cuerpo = cuerpo + "</tr > \n" +
                "</ thead > \n" +
                " <tbody>  \n";
                for (int j = 0; j < tabla.Filas.Count; j++)
                {
                    Fila fila = tabla.Filas[j];
                    cuerpo = cuerpo + "  <tr > ";
                    for (int k = 0; k < columnas; k++)
                    {
                        if (k < fila.Fila1.Count)
                        {
                            Dato dato = fila.Fila1[k];
                            if (dato != null)
                            {
                                cuerpo = cuerpo + "<td > " + dato.Dato1 + " </td >\n";
                            }
                            else
                            {
                                cuerpo = cuerpo + "<td > " + "     " + " </td >\n";
                            }
                        }
                        else
                        {
                            cuerpo = cuerpo + "<td > " + "     " + " </td >\n";
                        }

                    }
                    cuerpo = cuerpo + "  </tr > ";
                }

                cuerpo = cuerpo +
               "</tbody>\n" +
               "</tr ></ tr >\n " +
               "</table>\n " +
               "</tr ></ tr >\n ";

            }

            return cuerpo;
        }

        public String GenerarConsultaHTML(Tabla tabla)
        {
            int columnas;
            String cuerpo = "<!DOCTYPE HTML> <html > <head > <title > Tablas </title>" +
               "<meta charset = \"utf-8\" >" +
               "<meta name = \"viewport\" content = \"width=device-width, initial-scale=1\" >" +
           "<link rel = \"stylesheet\" href = \"https://maxcdn.bootstrapcdn.com/bootstrap/4.5.0/css/bootstrap.min.css\">" +
           " <script src = \"https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js\" ></script >" +
           "<script src = \"https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.16.0/umd/popper.min.js\" ></script >" +
           "<script src = \"https://maxcdn.bootstrapcdn.com/bootstrap/4.5.0/js/bootstrap.min.js\" ></script >" +
           "</tr </head >\n <body>";

            cuerpo = cuerpo + "</tr></tr > \n<h1>" + tabla.Nombre + " </h1> </tr > \n " +
               "<table class=\"table table-bordered\">\n" +
                   "<thead class=\"thead - dark\">" +
                       "<tr > \n";
            columnas = tabla.Columnas.Count;
            for (int j = 0; j < tabla.Columnas.Count; j++)
            {
                Dato dato = tabla.Columnas[j];
                cuerpo = cuerpo + "<td > " + dato.Dato1 + " (" + dato.Tipo + ") " + " </td >\n";
            }
            cuerpo = cuerpo + "</tr > \n" +
            "</ thead > \n" +
            " <tbody>  \n";
            for (int j = 0; j < tabla.Filas.Count; j++)
            {
                Fila fila = tabla.Filas[j];
                cuerpo = cuerpo + "  <tr > ";
                for (int k = 0; k < columnas; k++)
                {
                    if (k < fila.Fila1.Count)
                    {
                        Dato dato = fila.Fila1[k];
                        if (dato != null)
                        {
                            cuerpo = cuerpo + "<td > " + dato.Dato1 + " </td >\n";
                        }
                        else
                        {
                            cuerpo = cuerpo + "<td > " + "     " + " </td >\n";
                        }
                    }
                    else
                    {
                        cuerpo = cuerpo + "<td > " + "     " + " </td >\n";
                    }

                }
                cuerpo = cuerpo + "  </tr > ";
            }

            cuerpo = cuerpo +
               "</tbody>\n" +
               "</tr ></ tr >\n " +
               "</table>\n " +
               "</tr ></ tr >\n ";



            return cuerpo;
        }
    }
}
