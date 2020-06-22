using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace _301731318SQLProyecto.Backend.Parser_y_Scanner
{
    class AnalizadorSintactico
    {

        List<Tabla> tablas;
        int IDENTIFICADOR = 1;
        int FLOTANTE = 4;
        int ENTERO = 5;
        int PARABIERTO = 6;
        int PARCERRADO = 7;
        int IGUAL = 8;
        int MENORQUE = 9;
        int MAYORQUE = 10;
        int MAYORIGUAL = 11;
        int MENORIGUAL = 12;
        int NOIGUAL = 13;
        int COMA = 14;
        int PUNTOYCOMA = 15;
        int CADENA = 16;
        int FECHA = 17;
        int ASTERISCO = 18;
        int PUNTO = 19;


        int CREAR = 20;
        int INSERTAR = 21;
        int SELECCIONAR = 22;
        int ELIMINAR = 23;
        int ACTUALIZAR = 24;

        int DE = 25;
        int TABLA = 26;
        int VALORES = 27;
        int ESTABLECER = 28;
        int DONDE = 29;
        int Y = 30;
        int O = 31;
        int COMO = 32;
        int EN = 31;

        int CADENAID = 32;
        int FLOTANTEID = 33;
        int ENTEROID = 34;
        int FECHAID = 35;

        Token actual;
        List<ErrorSintactico> erroresSintacticos = new List<ErrorSintactico>();
        List<Token> tokens = new List<Token>();
        int i = 0;
        public AnalizadorSintactico(List<Token> tokens)
        {
            tablas = new List<Tabla>();
            this.tokens = tokens;
        }

        public void analizar()
        {
            actual =NextToken();
            Inicio();
            
        }

        public Token NextToken()
        {
            try
            { 
                if (i < tokens.Count)
                {
                    Token tokenN = tokens[i];
                    i++;
                    return tokenN;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
        
        public String  Match(int token, String errorMsg)
        {

            if (actual.Token1 == token)
            {
                actual = NextToken();
                if (actual != null)
                {
                    return actual.Lexema;
                }
                else
                {
                    return "";
                }
                
            }
            else
            {
                ErrorSintactico error = new ErrorSintactico(actual, errorMsg);
                Console.WriteLine("ERROR..........."+ errorMsg);
                erroresSintacticos.Add(error);
                RecuperacionErroresPanico();
                analizar();
                return null;
            }
        }
        List<Dato> columnas;
        Dato dato;
        public void Inicio()
        {
           // Console.WriteLine("INICIANDO");
            if (actual != null)
            {
                int valorToken = actual.Token1;
                //Console.WriteLine(valorToken + " " + CREAR);
                if (CREAR == valorToken)
                {
                    Tabla tabla = new Tabla();
                    columnas = new List<Dato>();
                    if(Match(CREAR, "SE ESPERABA LA ACCION CREAR") != null) { 
                        if(Match(TABLA, "SE ESPERABA LA PALABRA RESERVADA TABLA!!") !=  null)
                        {
                            String nombre = Match(IDENTIFICADOR, "SE ESPERABA UN IDENTIFICADOR");
                            if (nombre != null)
                            {
                                tabla.Nombre = nombre;
                                if(Match(PARABIERTO, "SE ESPERABA PARENTESIS ABIERTO")!= null)
                                {
                                    String nomcol = Match(IDENTIFICADOR, "SE ESPERABA UN IDENTIFICADOR PARA LA COLUMNA");
                                    String tipoCampo = TipoCampoId();
                                    if (tipoCampo != null && nomcol != null)
                                    {
                                        Dato dato = new Dato();
                                        dato.Tipo = tipoCampo;
                                        dato.Dato1 = nomcol;
                                        columnas.Add(dato);
                                        CuerpoCrear();
                                        if(Match(PARCERRADO, "SE ESPERABA PARENTESIS CERRADO") != null)
                                        {
                                            tabla.Columnas = columnas;
                                            if (Match(PUNTOYCOMA, "SE ESPERABA PUNTO Y COMA AL FINAL") != null)
                                            {
                                                tablas.Add(tabla);
                                                Console.WriteLine("ACEPTACION CREAR...........");
                                                Inicio();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else if (INSERTAR == valorToken)
                {
                    if(Match(INSERTAR, "SE ESPERABA ACCION INSERTAR") != null)
                    {
                        if(Match(EN, "SE ESPERABA PALABRA: EN")!=null)
                        {
                            if(Match(IDENTIFICADOR, "SE ESPERABA NOMBRE DE UNA TAMBLA") != null)
                            {
                                if(Match(VALORES, "SE ESPERABA PALABRA: VALORES")!=null)
                                {
                                    if(Match(PARABIERTO, "SE ESPERABA (") != null)
                                    {
                                        if (Dato()!=null)
                                        {
                                            if (CuerpoDatos())
                                            {
                                                if (Match(PARCERRADO, "SE ESPERABA )") != null)
                                                {
                                                    if (Match(PUNTOYCOMA, "SE ESPERABA PUNTO Y COMA") != null)
                                                    {
                                                        Console.WriteLine("ACEPTACION INSERTAR...........");
                                                        Inicio();
                                                    }
                                                }
                                            }
                                        }
                                        
                                    }
                                }
                               
                            }
                           
                        }
                        
                    }
                    
                }
                else if (SELECCIONAR == valorToken)
                {
                    if (Match(SELECCIONAR, "SE ESPERABA ACCION SELECCIONAR")!=null)
                    {
                        if (CuerpoSelect())
                        {
                            if (Match(PUNTOYCOMA, "SE ESPERABA PUNTO Y COMA")!=null)
                            {
                                Console.WriteLine("SELECT...........");
                                Inicio();
                                
                            }
                        }
                       
                    }
                    
                }
                else if (ELIMINAR == valorToken)
                {
                    if (Match(ELIMINAR, "SE ESPERABA ACCION ELIMINAR")!=null)
                    {
                        if (Match(DE, "SE ESPERABA PALARA: DE")!=null)
                        {
                            if (Match(IDENTIFICADOR, "SE ESPERABA IDENTIFICADOR")!=null)
                            {
                                if (Condicion())
                                {
                                    if (Match(PUNTOYCOMA, "SE ESPERABA PUNTO Y COMA")!=null)
                                    {
                                        Console.WriteLine("ELIMINAR...........");
                                        Inicio();
                                    }
                                }
                            }
                        }
                    }
                }
                else if (ACTUALIZAR == valorToken)
                {
                    if(Match(ACTUALIZAR, "SE ESPERABA ACCION ACTUALIZAR") != null)
                    {
                        if (Match(IDENTIFICADOR, "SE ESPERABA IDENTIFICADOR") != null)
                        {
                            if (Match(ESTABLECER, "SE ESPERABA PALABRA: ESTABLECER")!= null)
                            {
                                if (Match(PARABIERTO, "SE ESPERABA (") != null)
                                {
                                    if (Match(IDENTIFICADOR, "SE ESPERABA IDENTIFICADOR") != null)
                                    {
                                        if (Match(IGUAL, "SE ESPERABA SIGNO IGUAL") != null)
                                        {
                                            if (Dato() != null)
                                            {
                                                if (AActualizar())
                                                {
                                                    if (Match(PARCERRADO, "SE ESPERABA )") != null)
                                                    {
                                                        if (Condicion())
                                                        {
                                                            if (Match(PUNTOYCOMA, "SE ESPERABA PUNTO Y COMA") != null)
                                                            {
                                                                Console.WriteLine("ACTUALIZAR..............");
                                                                Inicio();
                                                            }
                                                        }
                                                        
                                                    }
                                                 }
                                            }
                                                
                                         }
                                     }
                                 }
                             }
                         }
                    }
                }
                else
                {
                    //en caso de no venir nada
                }
            }
        }

        public bool AActualizar()
        {
            if (actual.Token1 == COMA)
            {
                if(Match(COMA, "SE ESPERABA SIGNO COMA")!=null)
                {
                    if (Match(IDENTIFICADOR, "SE ESPERABA IDENTIFICADOR ")!=null)
                    {
                        if (Match(IGUAL, "SE ESPERABA SGNO IGUAL")!=null)
                        {
                            if (Dato()!=null)
                            {
                                return AActualizar();
                            }
                        }
                    }
                }
            }
            else {
                return true;
                // EN CASO DE NO VENIR NADA
            }
            return false;
        }
        //SELECCIONAR * DE Estudiantes DONDE id_estudiante>10 Y Fecha_nacimiento!= ‘21/12/2012’;  
        public bool CuerpoSelect()//A mejorar
        {
            //Console.WriteLine("CUERPO SELECT");
            if (ASTERISCO == actual.Token1)
            {
                //Console.WriteLine("CUERPO *");
                if (Match(ASTERISCO, "SE ESPERABA ASTERISCO") != null) { 
                    if (Desde() != null)
                    {
                        if (Condicion())
                        {
                            //Console.WriteLine("CUERPO  COND ACEPTADO");
                            return true;
                        }
                    }
                }
            }else if (IDENTIFICADOR == actual.Token1)
            {
                //Console.WriteLine("CUERPO ID");
                if (Match(IDENTIFICADOR, "SE ESPERABA IDENTIFICADOR") != null)
                {
                    //Console.WriteLine("CUERPO SI ID");
                    //---MasCampos();
                    if (CuerpoSelect1())
                    {
                        //Console.WriteLine("CAMPOS SELECT 1 CUERPO ");
                        if (Condicion())
                        {
                            //Console.WriteLine("CAMPOS SELECT condicion aceptada ");
                            return true;
                        }
                        
                    }

                }
            }
            else
            {
                return false;
                //metodo de error
            }
            return false;
        }
        public bool CuerpoSelect1()
        {
            if (actual != null)
            {
                if (actual.Token1 == DE)
                {
                    List<String> datosdesde = Desde();
                    if (datosdesde != null)
                    {
                        return true;
                    }
                }
                else if (actual.Token1 == PUNTO)
                {
                    //Console.WriteLine("CIERPO SELECT 1 PUNTO ");
                    if (Match(PUNTO, "SE ESPERABA SIGNO: PUNTO  ") != null)
                    {
                        if (CuerpoSelect2())
                        {
                            return true;
                        }
                    }

                }
                else
                {
                    return false;
                    //errores
                }
            }
            return false;
        }
        public bool CuerpoSelect2()
        {
            int tokenS = actual.Token1;
            if (tokenS==ASTERISCO)
            {
                if(Match(ASTERISCO, "SE ESPERABA SIGNO: *  ")!=null)
                {
                    if (MasCampos())
                    {
                        List<String> desde = Desde();
                        if (desde != null)
                        {
                            return true;
                        }
                    }
                    
                }
            }
            //else if (tokenS==MENORQUE|| tokenS==MENORIGUAL||tokenS==MAYORQUE||tokenS== MAYORIGUAL||tokenS == NOIGUAL||tokenS==IGUAL)
            else if(IDENTIFICADOR== actual.Token1)
            {
               // Console.WriteLine("CUERPO SELECT 2 IDENTIFICADOR");
                if (Match(IDENTIFICADOR, "SE ESPERABA IDENTIFICADOR  ") != null)
                {
                    String aleas =  Aleas();
                    if (aleas!=null)
                    {
                        //Console.WriteLine("CUERPO SELECT 2 ALEAS NO NULO");
                        if (MasCampos())
                        {
                            //Console.WriteLine("CUERPO SELECT 2 MAS CAMPOS SI "+ actual.Lexema);
                            List<String>desde = Desde();
                            if (desde!=null)
                            {
                                //Console.WriteLine("CUERPO SELECT 2 DESDE NO NULO ");
                                return true;
                            }
                        }

                    }
                    else
                    {
                        Console.WriteLine("CUERPO SELECT 2 ALEAS NULO");
                    }
                }
            }
            else
            {
                return false;
            }
            return false;
        }
        public String Arra()
        {
            if (actual.Token1 == COMA)
            {
                if(Match(COMA, "SE ESPERABA SIGNO: ,  ") != null)
                {
                    if (Match(IDENTIFICADOR, "SE ESPERABA IDENTIFICADOR  ") != null) { 
                        if(Match(PUNTO, "SE ESPERABA SIGNO: .  ") != null)
                        {
                            if (Match(IDENTIFICADOR, "SE ESPERABA IDENTIFICADOR")!=null)
                            {
                                String aleas = Aleas();
                                if (aleas!=null)
                                {
                                    return aleas;
                                }
                                else
                                {
                                    return "";
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                return "";
                //epsilon
            }
            return "";
        }
        public String Aleas()
        {
            if(actual.Token1== COMO)
            {
                if (Match(COMO, "SE ESPERABA PALABRA COMO ") != null)
                {
                    String aleas = Match(IDENTIFICADOR, "SE ESPERABA IDENTIDICADOR  ");
                    if (aleas!=null)
                    {
                        return aleas;
                    }
                }
            }
            else
            {
                return "";
                //EPSILON
            }
            return "";
        }


        public List<String> Desde()
        {
            if(Match(DE, "SE ESPERABA PALABRA: DE  ") != null)
            {
                Console.WriteLine("CUERPO DE");
                if (Match(IDENTIFICADOR, "SE ESPERABA IDENTIFICADOR  ") != null)
                {
                    Console.WriteLine("DESDE ID");
                    List<String> lista = Mas();
                    if (lista!=null)
                    {
                        Console.WriteLine("lista no  nula");
                        return lista;
                    }

                }
            }
            Console.WriteLine("lista nula");
            return null;
        }

        public List<String> Mas()
        {
            List<String> letras1 = new List<String>();
            if(actual.Token1== COMA)
            {
                if(Match(COMA, "SE ESPERABA SIGNO: COMA  ") != null)
                {
                    String letra = Match(IDENTIFICADOR, "SE ESPERABA IDENTIFICADOR  ");
                    if (letra  != null)
                    {
                        letras1.Add(letra);
                        List<String> add3 = Mas();
                        List<String> lista = letras1.Concat(add3).Distinct().ToList();
                        return lista;
                    }
                }
            }
            else
            {
                return letras1;
                //epsilon
            }
            return letras1;
        }
        public bool Condicion()
        {
            if (actual != null) { 
            if (actual.Token1 == DONDE)
            {
                if (Match(DONDE, "SE ESPERABA LA PALABRA RESERVADA DONDE")!=null)
                {
                   // Console.WriteLine("DONDE ");
                    if (Cond())
                    {
                        CuerpoCond();
                        return true;
                    }
                }
            }
            else
            {
                return true;
                //EPSILON
            }
            }
            else
            {
                return true;
            }
            return false;
        }

        public bool Cond()
        {
            Console.WriteLine("COND ");
            if (Match(IDENTIFICADOR, "SE ESPERABA UN IDENTIFICADOR")!=null)
            {
                //Console.WriteLine("COND ACEPTADA");
                if (Condi())
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
            return false;
        }
        public bool Condi()
        {
           // Console.WriteLine("CONDI.....m......"+actual.Lexema);
            int tokenS = actual.Token1;
            if (actual.Token1 == PUNTO)
            {
                if(Match(PUNTO, "SE ESPERABA UN IDENTIFICADOR") != null)
                {
                    if (Match(IDENTIFICADOR, "SE ESPERABA UN IDENTIFICADOR") != null)
                    {
                        if (Match(IGUAL, "SE ESPERABA UN SIGNO IGUAL") != null)
                        {
                            if (Match(IDENTIFICADOR, "SE ESPERABA UN IDENTIFICADOR") != null)
                            {
                                if (Match(PUNTO, "SE ESPERABA UN PUNTO") != null)
                                {
                                    if (Match(IDENTIFICADOR, "SE ESPERABA UN IDENTIFICADOR") != null)
                                    {
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else if (tokenS == MENORQUE || tokenS == MENORIGUAL || tokenS == MAYORQUE || tokenS == MAYORIGUAL || tokenS == NOIGUAL || tokenS == IGUAL)
            {
                //Console.WriteLine("MENOR O MAYOR ETTC");
                if (BodyCond())
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
            return false;
        }
        public bool CuerpoCond()
        {
            if (actual.Token1 == Y)
            {
                if(Match(Y, "SE ESPERABA  LETRA Y")!=null)
                {
                    Cond();
                    CuerpoCond();
                }
            }
            else if (actual.Token1 == O)
            {
                if(Match(O, "SE ESPERABA LETRA O") != null)
                {
                    Cond();
                    CuerpoCond();
                }
            }
            else
            {
                return true;
                // puede venir vacia
            }
            return false;
        }

        public bool BodyCond()
        {
            int tokenActual = actual.Token1;
            if (tokenActual == MAYORQUE)
            {
                if(Match(MAYORQUE, "SE ESPERABA signo >")!=null)
                {
                    if (Dato()!=null)
                    {
                        return true;
                    }
                }
            }
            else if (tokenActual == IGUAL) {
                if(Match(IGUAL, "SE ESPERABA signo =") != null)
                {
                    if (Dato()!=null)
                    {
                        return true;
                    }
                }
            }
            else if (tokenActual == NOIGUAL) {
                if(Match(NOIGUAL, "SE ESPERABA signo !=")!=null)
                {
                    if (Dato()!=null)
                    {
                        return true;
                    }
                }
            }
            else if (tokenActual == MENORQUE) {
                if(Match(MENORQUE, "SE ESPERABA signo <")!=null)
                {
                    if (Dato()!=null)
                    {
                        return true;
                    }
                }
            }
            else if (tokenActual == MAYORIGUAL) {
                if(Match(MAYORIGUAL, "SE ESPERABA signo >=")!=null)
                {
                    if (Dato()!=null)
                    {
                        return true;
                    }
                }
            }
            else if (tokenActual == MENORIGUAL) {
                if(Match(MENORIGUAL, "SE ESPERABA signo <=") != null)
                {
                    if (Dato()!=null)
                    {
                        return true;
                    }
                }
            }
            else
            {
                return false;
            }
            return false;
        }

        public bool MasCampos()
        {
            if (actual.Token1 == COMA)
            {
                if (Match(this.COMA, "SE ESPERABA COMA")!=null)
                {
                    if (Match(this.IDENTIFICADOR, "SE ESPERABA IDENTIFICADOR")!=null)
                    {
                        if (Match(this.PUNTO, "SE ESPERABA PUNTO")!=null)
                        {
                            if (Match(this.IDENTIFICADOR, "SE ESPERABA IDENTIFICADOR") != null)
                            {
                                String aleas = Aleas();
                                if (aleas != null)
                                {
                                    MasCampos();
                                    return true;
                                }
                            }
                        }
                    }
                }
            } else if (actual.Token1 ==1)
            {
                return true;
                // epsilon
            }
            return false;
        }
       

        public void CuerpoCrear() {
            int tokenActual = actual.Token1;
            if (tokenActual == this.COMA)
            {
                Match(this.COMA, "SE ESPERABA COMA");
                String id=Match(IDENTIFICADOR, "SE ESPERABA IDENTIFICADOR");
                String tipo=TipoCampoId();
                if (id!=null&&tipo!=null)
                {
                    Dato dato = new Dato();
                    dato.Dato1 = id;
                    dato.Tipo = tipo;
                    columnas.Add(dato);
                    CuerpoCrear();
                }
               
            }
            else
            {
                //puede venir epsilon
            }
        }

        public bool CuerpoDatos()
        {
            if (actual.Token1 == COMA)
            {
                if(Match(COMA, "SE ESPERABA COMA")!=null)
                {
                    if (Dato()!=null)
                    {
                        CuerpoDatos();
                    }
                }
            }
            else
            {
                return true;
                //puede venir epsilon
            }
            return false;
        }

        public String  TipoCampoId()
        {
            //Console.WriteLine("tipos campo");
            if (actual.Token1 == ENTEROID)
            {
                String dt = Match(ENTEROID, "SE ESPERABA UN TIPO DE DATO VALIDO ");
                if (dt != null)
                {
                    return dt;
                }
            }
            else if (actual.Token1 == CADENAID)
            {
                String dt = Match(CADENAID, "SE ESPERABA UN TIPO DE DATO VALIDO ");
                if (dt != null)
                {
                    return dt;
                }
            }
            else if (actual.Token1 == FLOTANTEID)
            {
                String dt = Match(FLOTANTEID, "SE ESPERABA UN TIPO DE DATO VALIDO ");
                if (dt != null)
                {
                    return dt;
                }
            }
            else if (actual.Token1 == FECHAID)
            {
                String dt = Match(FECHAID, "SE ESPERABA UN TIPO DE DATO VALIDO ");
                if (dt != null)
                {
                    return dt;
                }
            }
            else
            {
                return null;
                //ERROR SINTACTICO
            }
            return null;
        }

        public String Dato()
        {
            if (actual.Token1 == FECHA)
            {
                Match(FECHA, "SE ESPERABA UN DATO VALIDO ");
                    return "fecha";
            }
            else if (actual.Token1 == ENTERO)
            {
                Match(ENTERO, "SE ESPERABA UN DATO VALIDO ");
                    return "entero";
            }
            else if (actual.Token1 == CADENA)
            {
                Match(CADENA, "SE ESPERABA UN DATO VALIDO ");
                return "cadena";
            }
            else if (actual.Token1 == FLOTANTE)
            {
                Match(FLOTANTE, "SE ESPERABA UN DATO VALIDO ");
                return "flotante";
            }
            else
            {
                return "null";
            }
        }

        public void RecuperacionErroresPanico()
        {
            Console.WriteLine(i);
            for (int j = i; j < tokens.Count; j++)
            {
                if (tokens[j].Lexema == ";")
                {
                    Console.WriteLine(j+ tokens[j].Lexema);
                    i = j+1;
                    break;
                }
                else
                {

                }
            }
            Console.WriteLine(i);
            //actual = tokens[i+1];
            //Inicio();
        }
    }
}

