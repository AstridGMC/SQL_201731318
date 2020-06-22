using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace _301731318SQLProyecto.Backend.Parser_y_Scanner
{
    class AnalizadorLexico
    {
        public List<Token> tokens;
        public List<Token> errores;
        public List<Token> tokensAnalizar;
        public List<Token> tokensArchivo;
        String texto="";
        public AnalizadorLexico()
        {
        }

        public List<Token> TokensArchivo
        {
            get { return tokensArchivo; }
            set { tokensArchivo = value; }
        }
        public List<Token> TokensAnalizar
        {
            get { return TokensAnalizar; }
            set { TokensAnalizar = value; }
        }
        public List<Token> ObtenerTokens(String texto)
        {
            errores = new List<Token>();
            tokens = new List<Token>();
            tokensAnalizar = new List<Token>();
            tokensArchivo = new List<Token>();
            String estadoActual = "s0";
            String palabra="";
            Token token;
            int columna = 0;
            int fila = 0;
            for (int i=0;i<texto.Length; i++ )
            {
               
                char letra = texto[i];
                //Console.WriteLine("\n.ii.." + letra + ".. " + estadoActual);
                palabra = palabra + letra;
                estadoActual = ObtenerEstado(estadoActual, letra);
                //Console.WriteLine("estado " + estadoActual + "..con " + letra);
                if (i == texto.Length - 1)
                {
                    token = new Token(palabra.Trim(), NombrarEstados(estadoActual), columna, fila);
                    token.Token1 = token.NombrarToken(NombrarEstados(estadoActual));
                   
                    if (token.IsPalabraReservada1(palabra.Trim()))
                    {
                        token.Tipo = "PalabraReservada1";
                        token.Token1 = 0;
                    }
                    else if (token.IsPalabraReservada2(palabra.Trim()))
                    {
                        token.Tipo = "palabraReservada2";
                        token.Token1 = 0;
                    }
                    if ("error".Equals(estadoActual) || (" Error".Equals(estadoActual) && letra != '\n'))
                    {
                        errores.Add(token);
                    }
                    else
                    {
                        token.Color = token.ColorearToken(token.Tipo);
                        tokensArchivo.Add(token);
                        tokens.Add(token);
                        tokensAnalizar.Add(token);
                    }
                    estadoActual = "s0";
                }
                else
                {
                    if (estadoActual == "s1" &&( !Char.IsLetter(texto[i+1])&& texto[i + 1] !='_'))
                    {
                        letra = texto[i + 1];
                        //Console.WriteLine("cambiando char " + letra);
                        //i++;
                        token = new Token(palabra.Trim(), NombrarEstados(estadoActual), columna, fila);
                        token.Token1 = token.NombrarToken(NombrarEstados(estadoActual));
                        
                        if (token.IsPalabraReservada1(palabra.Trim())){
                            token.Tipo = "PalabraReservada1";
                            token.Token1 = token.NumerarPalabraReservada1(token.Lexema);
                        }
                        else if (token.IsPalabraReservada2(palabra.Trim()))
                        {
                            token.Tipo = "palabraReservada2";
                            token.Token1 = token.NumerarPalabraReservada2(token.Lexema);
                        }else if (token.isTipoDato(palabra.Trim()))
                        {
                            token.Tipo = "palabraReservada3";
                            token.Token1 = token.NumerarTipoDato(token.Lexema);
                        }
                        token.Color = token.ColorearToken(token.Tipo);
                        tokensArchivo.Add(token);
                        tokens.Add(token);
                        tokensAnalizar.Add(token);
                        estadoActual = "s0";
                        palabra = "";
                        columna++;
                        //Console.WriteLine("agrego estado s1......."+ estadoActual);
                        
                    }
                    else if (estadoActual=="s13" && letra == '\n') { 
                        token = new Token(palabra.Trim().Replace(palabra.Substring(palabra.Length), ""), NombrarEstados(estadoActual), columna, fila);
                        token.Token1 = token.NombrarToken(NombrarEstados(estadoActual));
                        tokens.Add(token);
                        tokensArchivo.Add(token);
                        estadoActual = "s0";
                        palabra = "";
                        columna++;
                    
                    }
                    else if (estadoActual == "s3" && !Char.IsDigit(texto[i+1]))
                    {
                        token = new Token(palabra.Trim() , NombrarEstados(estadoActual), columna, fila);
                        token.Token1 = token.NombrarToken(NombrarEstados(estadoActual));
                        token.Color = token.ColorearToken(token.Tipo);
                        tokens.Add(token);
                        tokensArchivo.Add(token);
                        tokensAnalizar.Add(token);
                        estadoActual = "s0";
                        palabra = "";
                        columna++;
                    }
                    else if (estadoActual=="s21"&& (letra == ' '|| letra == '\n'))
                    {
                        token = new Token(palabra.Trim() , NombrarEstados(estadoActual), columna, fila);
                        token.Token1 = token.NombrarToken(NombrarEstados(estadoActual));
                        token.Color = token.ColorearToken(token.Tipo);
                        tokens.Add(token);
                        tokensArchivo.Add(token);
                        tokensAnalizar.Add(token);
                        estadoActual = "s0";
                        palabra = "";
                        columna++;
                    }
                    else if (estadoActual == "s17")
                    {
                        token = new Token(palabra.Trim(), NombrarEstados(estadoActual), columna, fila);
                        token.Token1 = token.NombrarToken(NombrarEstados(estadoActual));
                        token.Color = token.ColorearToken(token.Tipo);
                        tokens.Add(token);
                        tokensArchivo.Add(token);
                        estadoActual = "s0";
                        palabra = "";
                        columna++;
                    }
                    else if (estadoActual == "s5")
                    {
                        token = new Token(palabra.Trim(), NombrarEstados(estadoActual), columna, fila);
                        token.Token1 = token.NombrarToken(NombrarEstados(estadoActual));
                        token.Color = token.ColorearToken(token.Tipo);
                        tokens.Add(token);
                        tokensArchivo.Add(token);
                        tokensAnalizar.Add(token);
                        estadoActual = "s0";
                        palabra = "";
                        columna++;
                        //Console.WriteLine("entrando s5....... " + estadoActual);
                    }
                    else if (estadoActual == "s6")
                    {
                        token = new Token(palabra.Trim(), NombrarEstados(estadoActual), columna, fila);
                        token.Token1 = token.NombrarToken(NombrarEstados(estadoActual));
                        token.Color = token.ColorearToken(token.Tipo);
                        tokens.Add(token);
                        tokensArchivo.Add(token);
                        tokensAnalizar.Add(token);
                        estadoActual = "s0";
                        palabra = "";
                        columna++;
                    }
                    else if (estadoActual == "s7")
                    {
                        token = new Token(palabra.Trim(), NombrarEstados(estadoActual), columna, fila);
                        token.Token1 = token.NombrarToken(NombrarEstados(estadoActual));
                        token.Color = token.ColorearToken(token.Tipo);
                        tokens.Add(token);
                        tokensArchivo.Add(token);
                        tokensAnalizar.Add(token);
                        estadoActual = "s0";
                        palabra = "";
                        columna++;
                    }
                    else if (estadoActual == "s8" && texto[i+1]!='=')
                    {
                        Console.WriteLine("entrando s9 ....." + texto[i + 1]  + "....");
                        token = new Token(palabra.Trim(), NombrarEstados(estadoActual), columna, fila);
                        token.Token1 = token.NombrarToken(NombrarEstados(estadoActual));
                        token.Color = token.ColorearToken(token.Tipo);
                        tokens.Add(token);
                        tokensArchivo.Add(token);
                        tokensAnalizar.Add(token);
                        estadoActual = "s0";
                        palabra = "";
                        columna++;
                    }
                    else if (estadoActual == "s9" && texto[i + 1] != '=')
                    {
                        Console.WriteLine("entrando s9 ....."+ texto[i + 1] +"....");
                        token = new Token(palabra.Trim(), NombrarEstados(estadoActual), columna, fila);
                        token.Token1 = token.NombrarToken(NombrarEstados(estadoActual));
                        token.Color = token.ColorearToken(token.Tipo);
                        tokens.Add(token);
                        tokensArchivo.Add(token);
                        tokensAnalizar.Add(token);
                        estadoActual = "s0";
                        palabra = "";
                        columna++;
                    }
                    else if (estadoActual == "s18")
                    {
                        token = new Token(palabra.Trim(), NombrarEstados(estadoActual), columna, fila);
                        token.Token1 = 12;
                        token.Color = token.ColorearToken(token.Tipo);
                        tokens.Add(token);
                        tokensArchivo.Add(token);
                        tokensAnalizar.Add(token);
                        estadoActual = "s0";
                        palabra = "";
                        columna++;
                    }
                    else if (estadoActual == "s19")
                    {
                        token = new Token(palabra.Trim(), NombrarEstados(estadoActual), columna, fila);
                        token.Token1 = token.NombrarToken(NombrarEstados(estadoActual));
                        token.Color = token.ColorearToken(token.Tipo);
                        tokensArchivo.Add(token);
                        tokensAnalizar.Add(token);
                        tokens.Add(token);
                        estadoActual = "s0";
                        palabra = "";
                        columna++;
                    }
                    else if (estadoActual == "s20")
                    {
                        token = new Token(palabra.Trim(), NombrarEstados(estadoActual), columna, fila);
                        token.Token1 = token.NombrarToken(NombrarEstados(estadoActual));
                        token.Color = token.ColorearToken(token.Tipo);
                        tokens.Add(token);
                        tokensAnalizar.Add(token);
                        tokensArchivo.Add(token);
                        estadoActual = "s0";
                        palabra = "";
                        columna++;
                    }
                    else if (estadoActual == "s11")
                    {
                        token = new Token(palabra.Trim(), NombrarEstados(estadoActual), columna, fila);
                        token.Token1 = token.NombrarToken(NombrarEstados(estadoActual));
                        token.Color = token.ColorearToken(token.Tipo);
                        tokens.Add(token);
                        tokensAnalizar.Add(token);
                        tokensArchivo.Add(token);
                        estadoActual = "s0";
                        palabra = "";
                        columna++;
                    }
                    else if (estadoActual == "s12"|| estadoActual == "s36")
                    {
                        token = new Token(palabra.Trim(), NombrarEstados(estadoActual), columna, fila);
                        token.Token1 = token.NombrarToken(NombrarEstados(estadoActual));
                        token.Color = token.ColorearToken(token.Tipo);
                        tokens.Add(token);
                        tokensAnalizar.Add(token);
                        tokensArchivo.Add(token);
                        estadoActual = "s0";
                        palabra = "";
                        columna++;
                    }
                    else if (estadoActual == "s21"&& !Char.IsDigit(texto[i+1]))
                    {
                        token = new Token(palabra.Trim(), NombrarEstados(estadoActual), columna, fila);
                        token.Token1 = token.NombrarToken(NombrarEstados(estadoActual));
                        token.Color = token.ColorearToken(token.Tipo);
                        tokens.Add(token);
                        tokensAnalizar.Add(token);
                        tokensArchivo.Add(token);
                        estadoActual = "s0";
                        palabra = "";
                        columna++;
                    }
                    else if (estadoActual == "s35"|| estadoActual == "s36" || estadoActual == "s37" || estadoActual == "s38")
                    {
                        token = new Token(palabra.Trim(), NombrarEstados(estadoActual), columna, fila);
                        token.Token1 = token.NombrarToken(NombrarEstados(estadoActual));
                        token.Color = token.ColorearToken(token.Tipo);
                        tokens.Add(token);
                        tokensArchivo.Add(token);
                        tokensAnalizar.Add(token);
                        estadoActual = "s0";
                        palabra = "";
                        columna++;
                    }
                    else if (estadoActual == "error"&&texto[i+1]==' ')
                    {
                        token = new Token(palabra.Trim(), NombrarEstados(estadoActual), columna, fila);
                        token.Token1 = token.NombrarToken(NombrarEstados(estadoActual));
                        token.Color = token.ColorearToken(token.Tipo);
                        errores.Add(token);
                        tokensArchivo.Add(token);
                        estadoActual = "s0";
                        palabra = "";
                        columna++;
                    }
                }
                
                if (letra == '\n')
                {
                    token = new Token("\n", null, 0, 0);
                    token.Color = Color.Black;
                    tokensArchivo.Add(token);
                    columna = 0;
                    fila++;
                }
                else if(letra == ' ')
                {
                    token = new Token(" ", null, 0, 0);
                    token.Color = Color.Black;
                    tokensArchivo.Add(token);
                }

            }
            Console.Out.WriteLine("tam de arreglo arch 1" + tokens.Count );
            Console.Out.WriteLine("tam de errores lexicos " + errores.Count);
            return tokens;
        }

        public String ObtenerEstado(String estado, char charA)
        {
            switch (estado)
            {
                case "s0":
                    if (Char.IsLetter(charA))
                    {
                        //Console.WriteLine(".......pasandos1 ...."+charA+"........");
                        return "s1";
                    }
                    else if(Char.IsDigit(charA))
                    {
                        return "s3";
                    }
                    else if (charA=='-')
                    {
                        return "s2";
                    }
                    else if (charA=='/')
                    {
                        return "s4";
                    }
                    else if (charA=='(')
                    {
                        return "s5";
                    }
                    else if (charA==')')
                    {
                        return "s6";
                    }
                   /* else if (charA=='=')
                    {
                        return "s7";
                    }*/
                    else if (charA=='=')
                    {
                        return "s7";
                    }
                    else if (charA=='<')
                    {
                        return "s8";
                    }
                    else if (charA=='>')
                    {
                        return "s9";
                    }
                    else if (charA=='!')
                    {
                        return "s10";
                    }
                    else if (charA == ',')
                    {
                        return "s11";
                    }
                    else if (charA == ';')
                    {
                        return "s12";
                    }
                    else if (charA.ToString().Equals("'")|| charA.ToString().Equals("‘"))
                    {
                        return "s23";
                    }
                    else if (charA == '"'|| charA == '”')
                    {
                        return "s22";
                    }
                    else if (charA == "'"[0])
                    {
                        Console.WriteLine("..............." + "'"[0]);
                        return "s23";
                    }
                    else if (charA == '*')
                    {
                        return "s38";
                    }
                    else if (charA == '.')
                    {
                        return "s37";
                    }
                    else if (charA == '\n')
                    {
                        return estado;
                    }
                    else if (charA == '\t')
                    {
                        return estado;
                    }
                    else if (charA == ' ')
                    {
                        return "s0";
                    }
                    else
                    {
                        return "error";
                    }

                case "s1":
                    if (Char.IsDigit(charA))
                    {
                        return "s1";
                    }else if (Char.IsLetter(charA))
                    {
                        return "s1";
                    }
                    else if (charA=='_')
                    {
                        return "s1";
                    }
                    else if (charA == ' ')
                    {
                        return "s1";
                    }
                    else
                    {
                        return "error";
                    }
                case "s2":
                    if (charA == '-')
                    {
                        return "s13";
                    } else {
                        return "error";
                    }
                    
                case "s3":
                    if (Char.IsDigit(charA))
                    {
                        return "s3";
                    }else if (charA == '.')
                    {
                        return "s14";
                    }else if (charA==' '|| charA == '\n')
                    {
                        return "s3";
                    }
                    else
                    {
                        return "error";
                    }
                case "s4":
                    if (charA == '*')
                    {
                        return "s15";
                    }else
                    {
                        return "error";
                    }
                case "s5":
                    break;
                case "s6":
                    break;
                case "s7":
                    break;
                case "s8":
                    if (charA == '=')
                    {
                        return "s18";
                    }
                    else if (charA == ' ')
                    {
                        return "s8";
                    }
                    else
                    {
                        return "error";
                    }
                case "s9":
                    if (charA == '=')
                    {
                        return "s19";
                    }
                    else if (charA == ' ')
                    {
                        return "s9";
                    }
                    else
                    {
                        return "error";
                    }
                case "s10":
                    if (charA == '=')
                    {
                        return "s20";
                    }
                    else
                    {
                        return "error";
                    }
                case "s11":
                    break;
                case "s12":
                    break;
                case "s13":
                    return "s13";
                case "s14":
                    if (Char.IsDigit(charA))
                    {
                        return "s21";
                    }
                    else
                    {
                        return "error";
                    }
                case "s15":
                    if (charA == '*')
                    {
                        return "s16";
                    }
                    else
                    {
                        return "s15";
                    }
                case "s16":
                    if (charA == '/')
                    {
                        return "s17";
                    }
                    else
                    {
                        return "error";
                    }
                case "s17":
                    break;
                case "s18":
                    break;
                case "s19":
                    break;
                case "s20":
                    break;
                case "s21":
                    if (Char.IsDigit(charA))
                    {
                        return "s21";
                    }
                    else
                    {
                        return "error";
                    }
                case "s22":
                    if (Char.IsLetter(charA))
                    {
                        return "s24";
                    }
                    else if (charA == ' ')
                    {
                        return "s24";
                    }
                    else if (charA == '\n')
                    {
                        return "s22";
                    }
                    else
                    {
                        return "error";
                    }
                case "s23":
                    if (Char.IsDigit(charA))
                    {
                        return "s25";
                    }
                    else if (charA == ' ')
                    {
                        return "s23";
                    }
                    else
                    {
                        return "error";
                    }
                case "s24":
                    if (Char.IsLetter(charA))
                    {
                        return "s24";
                    }
                    else if (charA == ' ')
                    {
                        return "s24";
                    }
                    else if (charA == '"' || charA == '”')
                    {
                        return "s36";
                    }
                    else
                    {
                        return "error";
                    }
                case "s25":
                    if (Char.IsDigit(charA))
                    {
                        return "s26";
                    }
                    else
                    {
                        return "error";
                    }
                case "s26":
                    if (charA=='/')
                    {
                        return "s27";
                    }
                    else if (charA == ' ')
                    {
                        return "s26";
                    }
                    else
                    {
                        return "error";
                    }
                case "s27":
                    if (Char.IsDigit(charA))
                    {
                        return "s28";
                    }
                    else if (charA == ' ')
                    {
                        return "s27";
                    }
                    else
                    {
                        return "error";
                    }
                case "s28":
                    if (Char.IsDigit(charA))
                    {
                        return "s29";
                    }
                    else
                    {
                        return "error";
                    }
                case "s29":
                    if (charA=='/')
                    {
                        return "s30";
                    }
                    else if (charA == ' ')
                    {
                        return "s29";
                    }
                    else
                    {
                        return "error";
                    }
                case "s30":
                    if (Char.IsDigit(charA))
                    {
                        return "s31";
                    }
                    else if (charA == ' ')
                    {
                        return "s30";
                    }
                    else
                    {
                        return "error";
                    }
                case "s31":
                    if (Char.IsDigit(charA))
                    {
                        return "s32";
                    }
                    else
                    {
                        return "error";
                    }
                case "s32":
                    if (Char.IsDigit(charA))
                    {
                        return "s33";
                    }
                    else
                    {
                        return "error";
                    }
                case "s33":
                    if (Char.IsDigit(charA))
                    {
                        return "s34";
                    }
                    else
                    {
                        return "error";
                    }
                case "s34":
                    if (charA.ToString().Equals("'") || charA.ToString().Equals("‘"))
                    {
                        return "s35";
                    }
                    else if (charA == ' ')
                    {
                        return "s34";
                    }
                    else
                    {
                        return "error";
                    }
                case "35":
                    break;
            }
            return estado;
        }

        public String NombrarEstados(String actual)
        {
            if (actual == "s1")
            {
                return "Identificador";
            }
            else if (actual == "s13")
            {
                return "Comentario de Una Linea";
            }
            else if (actual == "s3")
            {
                return "Entero";
            }
            else if (actual == "s21")
            {
                return "Decimal";
            }
            else if (actual == "s17")
            {
                return "Comentario Multilinea";
            }
            else if ("s5".Equals(actual))
            {
                return "ParentesisAbierto";
            }
            else if (actual == "s6")
            {
                return "ParentesisCerrado";
            }
            else if (actual == "s7")
            {
                return "SignoIgual";
            }
            else if (actual == "s8")
            {
                return "MenorQue";
            }
            else if (actual == "s9")
            {
                return "MayorQue";
            }
            else if (actual == "s18")
            {
                return "MenorIgual";
            }
            else if (actual == "s19")
            {
                return "MayorIgual" ;
            }
            else if (actual == "s20")
            {
                return "NoIgual";
            }
            else if (actual == "s11")
            {
                return "Coma";
            }
            else if (actual == "s12")
            {
                return "PuntoYComa";
            }
            else if (actual == "s36")
            {
                return "Cadena";
            }
            else if (actual == "s35")
            {
                return "Fecha";
            }
            else if (actual == "s38")
            {
                return "Asterisco";
            }
            else if (actual == "s37")
            {
                return "Punto";
            }
            else
            {
                return " Error";
            }
        }
    }
   
}
