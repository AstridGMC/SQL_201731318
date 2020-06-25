using _301731318SQLProyecto;
using _301731318SQLProyecto.Backend;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace _201731318SQLProyecto.Backend.Parser_y_Scanner
{
    class AnalizadorSintactico
    {



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
        int EN = 33;

        int CADENAID = 34;
        int FLOTANTEID = 35;
        int ENTEROID = 36;
        int FECHAID = 37;

        Nodo principal;
        Token actual;

        List<ErrorSintactico> erroresSintacticos = new List<ErrorSintactico>();
        List<Token> tokens = new List<Token>();
        List<Tabla> tablas;
        Instruccion instruccion;
        int i = 0;
        int idNodo = 0;
        public Nodo Principal { get { return principal; } set { principal = value; } }
        public List<ErrorSintactico> Errores { get { return erroresSintacticos; } set { erroresSintacticos = value; } }
        public List<Tabla> Tablas { get { return tablas; } set { tablas = value; } }
        public AnalizadorSintactico(List<Token> tokens)
        {
            this.tokens = tokens;
        }

        public void analizar()
        {
            Nodo.id = 1;
            principal = new Nodo(null, null, "S");
            actual = NextToken();
            Nodo inicio = Inicio();
            if (inicio != null)
            {
                inicio.Padre = principal;
                principal.Hijos.Add(inicio);
            }
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

        public Token Match(int token, String errorMsg)
        {
            Token act = actual;
            if (actual.Token1 == token)
            {
                actual = NextToken();
                if (actual != null)
                {
                    return act;
                }
                else
                {
                    Console.WriteLine("nadaaa....");
                    return act;
                }

            }
            else
            {
                ErrorSintactico error = new ErrorSintactico(actual, errorMsg);
                Console.WriteLine("ERROR..........." + errorMsg);
                error.Descartados = RecuperacionErroresPanico();
                erroresSintacticos.Add(error);
                analizar();
                return null;
            }
        }
        List<Dato> columnas;
        Dato dato;
        List<Dato> datos;
        List<String> nombresTablas;
        List<String> columnasS;
        public Nodo Inicio()
        {
            nombresTablas = new List<string>();
            instruccion = new Instruccion(tablas);
            Nodo inicio = new Nodo(null, null, "INICIO");
            // Console.WriteLine("INICIANDO");
            if (actual != null)
            {
                int valorToken = actual.Token1;

                if (CREAR == valorToken)
                {
                    Tabla tabla = new Tabla();
                    columnas = new List<Dato>();

                    Token uno = Match(CREAR, "SE ESPERABA LA ACCION CREAR");
                    if (uno != null)
                    {
                        Nodo nodo1 = new Nodo(inicio, uno, uno.Lexema);
                        Token dos = Match(TABLA, "SE ESPERABA LA PALABRA RESERVADA TABLA!!");
                        if (dos != null)
                        {
                            Nodo nodo2 = new Nodo(inicio, dos, dos.Lexema);
                            Token tres = Match(IDENTIFICADOR, "SE ESPERABA UN IDENTIFICADOR");

                            if (tres.Lexema != null)
                            {
                                Nodo nodo3 = new Nodo(inicio, tres, tres.Lexema);
                                tabla.Nombre = tres.Lexema;
                                Token cuatro = Match(PARABIERTO, "SE ESPERABA PARENTESIS ABIERTO");
                                if (cuatro != null)
                                {
                                    Nodo nodo4 = new Nodo(inicio, cuatro, cuatro.Lexema);
                                    Token cinco = Match(IDENTIFICADOR, "SE ESPERABA UN IDENTIFICADOR PARA LA COLUMNA");
                                    String nomcol = cinco.Lexema;
                                    Nodo tipoCampo = TipoCampoId();
                                    if (tipoCampo != null && nomcol != null)
                                    {
                                        Nodo nodo5 = new Nodo(inicio, cinco, cinco.Lexema);
                                        tipoCampo.Padre = inicio;
                                        Dato dato = new Dato();
                                        dato.Tipo = tipoCampo.Hijos[0].TokenN.Lexema;
                                        dato.Dato1 = nomcol;
                                        columnas.Add(dato);

                                        Nodo nodo9 = CuerpoCrear();
                                        if (nodo9 != null)
                                        {
                                            nodo9.Padre = inicio;
                                            Token token7 = Match(PARCERRADO, "SE ESPERABA PARENTESIS CERRADO");
                                            if (token7 != null)
                                            {
                                                Nodo nodo7 = new Nodo(inicio, token7, token7.Lexema);
                                                tabla.Columnas = columnas;
                                                Token token8 = Match(PUNTOYCOMA, "SE ESPERABA PUNTO Y COMA AL FINAL");
                                                if (token8 != null)
                                                {
                                                    if (!instruccion.BuscarExistencia(tabla.Nombre))
                                                    {
                                                        tablas.Add(tabla);
                                                    }
                                                    else
                                                    {
                                                        MessageBox.Show("lA TABLA existe en la base de datos");
                                                    }


                                                    Console.WriteLine(tabla.Columnas.Count + "num col..................");
                                                    Nodo nodo8 = new Nodo(inicio, token8, token8.Lexema);
                                                    inicio.Hijos.Add(nodo1);
                                                    inicio.Hijos.Add(nodo2);
                                                    inicio.Hijos.Add(nodo3);
                                                    inicio.Hijos.Add(nodo4);
                                                    inicio.Hijos.Add(nodo5);
                                                    inicio.Hijos.Add(tipoCampo);
                                                    inicio.Hijos.Add(nodo9);
                                                    inicio.Hijos.Add(nodo7);
                                                    inicio.Hijos.Add(nodo8);

                                                    Nodo inicio1 = Inicio();
                                                    if (inicio1 != null)
                                                    {
                                                        inicio1.Padre = inicio;
                                                        inicio.Hijos.Add(inicio1);
                                                    }

                                                    Console.WriteLine("ACEPTACION CREAR..........." + " " + inicio.Hijos.Count);
                                                    return inicio;
                                                }
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
                    // Console.WriteLine("Insettar");
                    Token token1 = Match(INSERTAR, "SE ESPERABA ACCION INSERTAR");
                    if (token1 != null)
                    {
                        datos = new List<Dato>();
                        Nodo uno = new Nodo(inicio, token1, token1.Lexema);
                        Token token6 = Match(EN, "SE ESPERABA PALABRA: EN");
                        if (token6 != null)
                        {
                            Nodo seis = new Nodo(inicio, token6, token6.Lexema);
                            Token token2 = Match(IDENTIFICADOR, "SE ESPERABA NOMBRE DE UNA TABLA");
                            if (token2 != null)
                            {
                                String nombreTabla = token2.Lexema;
                                Nodo dos = new Nodo(inicio, token2, token2.Lexema);
                                Token token3 = Match(VALORES, "SE ESPERABA PALABRA: VALORES");
                                if (token3 != null)
                                {
                                    Nodo tres = new Nodo(inicio, token3, token3.Lexema);
                                    Token token4 = Match(PARABIERTO, "SE ESPERABA (");
                                    if (token4 != null)
                                    {
                                        Nodo cuatro = new Nodo(inicio, token4, token4.Lexema);
                                        Nodo dato = Dato();
                                        if (dato != null)
                                        {
                                            Dato dat = new Dato();
                                            dat.Dato1 = dato.Hijos[0].TokenN.Lexema;
                                            datos.Add(dat);
                                            //Nodo cinco = new Nodo(dato, token5, token5.Lexema);
                                            //dato.Hijos.Add(cinco);
                                            Nodo siete = CuerpoDatos();
                                            if (siete != null)
                                            {
                                                //Console.WriteLine("cuerpo dato");
                                                Token token8 = Match(PARCERRADO, "SE ESPERABA )");
                                                if (token8 != null)
                                                {
                                                    //Console.WriteLine("parcerrado"+ actual.Token1);
                                                    Nodo ocho = new Nodo(inicio, token8, token8.Lexema);
                                                    Token token9 = Match(PUNTOYCOMA, "SE ESPERABA PUNTO Y COMA");
                                                    if (token9 != null)
                                                    {
                                                        instruccion.Insertar(nombreTabla, datos);

                                                        tablas = instruccion.Tablas1;
                                                        siete.Padre = inicio;
                                                        dato.Padre = inicio;
                                                        Nodo nueve = new Nodo(inicio, token9, token9.Lexema);
                                                        inicio.Hijos.Add(uno);
                                                        inicio.Hijos.Add(dos);
                                                        inicio.Hijos.Add(tres);
                                                        inicio.Hijos.Add(cuatro);
                                                        inicio.Hijos.Add(dato);
                                                        inicio.Hijos.Add(seis);
                                                        inicio.Hijos.Add(siete);
                                                        inicio.Hijos.Add(ocho);
                                                        Nodo inicio1 = Inicio();
                                                        if (inicio1 != null)
                                                        {
                                                            inicio1.Padre = inicio;
                                                            inicio.Hijos.Add(inicio1);
                                                        }
                                                        Console.WriteLine("ACEPTACION INSERTAR........... " + inicio.Hijos.Count);
                                                        return inicio;
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

                    columnasS = new List<String>();
                    datos = new List<Dato>();
                    Token token1 = Match(SELECCIONAR, "SE ESPERABA ACCION SELECCIONAR");
                    if (token1 != null)
                    {
                        Nodo uno = new Nodo(inicio, token1, token1.Lexema);
                        Nodo cuerposelect = CuerpoSelect();
                        if (cuerposelect != null)
                        {
                            Token token2 = Match(PUNTOYCOMA, "SE ESPERABA PUNTO Y COMA");
                            if (token2 != null)
                            {
                                cuerposelect.Padre = inicio;
                                Nodo dos = new Nodo(inicio, token2, token2.Lexema);
                                inicio.Hijos.Add(uno);
                                inicio.Hijos.Add(cuerposelect);
                                inicio.Hijos.Add(dos);
                                Console.WriteLine("SELECT...........");
                                Nodo inicioNodo = Inicio();
                                if (inicioNodo != null)
                                {

                                    inicioNodo.Padre = inicio;
                                    inicio.Hijos.Add(inicioNodo);
                                }
                                return inicio;
                            }
                        }

                    }

                }
                else if (ELIMINAR == valorToken)
                {
                    String nombreTabla;
                    Token token1 = Match(ELIMINAR, "SE ESPERABA ACCION ELIMINAR");
                    if (token1 != null)
                    {
                        Nodo uno = new Nodo(inicio, token1, token1.Lexema);
                        Token token2 = Match(DE, "SE ESPERABA PALARA: DE");
                        if (token2 != null)
                        {
                            Nodo dos = new Nodo(inicio, token2, token2.Lexema);
                            Token token3 = Match(IDENTIFICADOR, "SE ESPERABA IDENTIFICADOR");
                            if (token3 != null)
                            {
                                nombreTabla = token3.Lexema;
                                Nodo tres = new Nodo(inicio, token3, token3.Lexema);
                                Nodo condicion = Condicion();
                                if (condicion != null)
                                {
                                    if (condicion.Hijos[0].Estado != "epsilon")
                                    {

                                    }
                                    else
                                    {
                                        if (!instruccion.Eliminar(nombreTabla, null))
                                        {
                                            Console.WriteLine("error eliminando tabla");

                                        }
                                        else
                                        {
                                            tablas = instruccion.Tablas1;
                                        }
                                    }
                                    condicion.Padre = inicio;
                                    Token token5 = Match(PUNTOYCOMA, "SE ESPERABA PUNTO Y COMA");
                                    if (token5 != null)
                                    {
                                        Nodo cinco = new Nodo(inicio, token5, token5.Lexema);
                                        Console.WriteLine("ELIMINAR...........");
                                        inicio.Hijos.Add(uno);
                                        inicio.Hijos.Add(dos);
                                        inicio.Hijos.Add(tres);
                                        inicio.Hijos.Add(condicion);
                                        inicio.Hijos.Add(cinco);
                                        Nodo init = Inicio();
                                        if (Inicio() != null)
                                        {
                                            init.Padre = inicio;
                                            inicio.Hijos.Add(init);
                                        }
                                        principal.Hijos.Add(inicio);
                                        return inicio;
                                    }
                                }
                            }
                        }
                    }
                }
                else if (ACTUALIZAR == valorToken)
                {
                    Token t1 = Match(ACTUALIZAR, "SE ESPERABA ACCION ACTUALIZAR");
                    if (t1 != null)
                    {
                        datos = new List<Dato>();
                        Nodo uno = new Nodo(inicio, t1, t1.Lexema);
                        Token t2 = Match(IDENTIFICADOR, "SE ESPERABA IDENTIFICADOR");
                        if (t2 != null)
                        {
                            String nombreTabla = t2.Lexema;
                            Nodo dos = new Nodo(inicio, t2, t2.Lexema);
                            Token t3 = Match(ESTABLECER, "SE ESPERABA PALABRA: ESTABLECER");
                            if (t3 != null)
                            {
                                Nodo tres = new Nodo(inicio, t3, t3.Lexema);
                                Token t4 = Match(PARABIERTO, "SE ESPERABA (");
                                if (t4 != null)
                                {
                                    Nodo cuatro = new Nodo(inicio, t4, t4.Lexema);
                                    Token t5 = Match(IDENTIFICADOR, "SE ESPERABA IDENTIFICADOR");
                                    if (t5 != null)
                                    {
                                        String columna = t5.Lexema;
                                        Nodo cinco = new Nodo(inicio, t5, t5.Lexema);
                                        Token t6 = Match(IGUAL, "SE ESPERABA SIGNO IGUAL");
                                        if (t6 != null)
                                        {
                                            Nodo seis = new Nodo(inicio, t6, t6.Lexema);
                                            Nodo dato = Dato();
                                            if (dato != null)
                                            {
                                                String datoActualizado = dato.Hijos[0].TokenN.Lexema;
                                                Dato datoAC = new Dato();
                                                datoAC.Dato1 = datoActualizado;
                                                datoAC.Columna = columna;
                                                datos.Add(datoAC);
                                                dato.Padre = inicio;
                                                Nodo aactualizar = AActualizar();
                                                if (aactualizar != null)
                                                {
                                                    aactualizar.Padre = inicio;
                                                    Token t7 = Match(PARCERRADO, "SE ESPERABA )");
                                                    if (t7 != null)
                                                    {
                                                        Nodo siete = new Nodo(inicio, t7, t7.Lexema);
                                                        Nodo condicion = Condicion();
                                                        if (condicion != null)
                                                        {
                                                            condicion.Padre = inicio;
                                                            Token t8 = Match(PUNTOYCOMA, "SE ESPERABA PUNTO Y COMA");
                                                            if (t8 != null)
                                                            {
                                                                if (condicion.Hijos[0].Estado != "epsilon")
                                                                {

                                                                }
                                                                else
                                                                {
                                                                    instruccion.Actualizar(nombreTabla, datos, null);
                                                                    tablas = instruccion.Tablas1;
                                                                }
                                                                Nodo ocho = new Nodo(inicio, t8, t8.Lexema);
                                                                inicio.Hijos.Add(uno);
                                                                inicio.Hijos.Add(dos);
                                                                inicio.Hijos.Add(tres);
                                                                inicio.Hijos.Add(cuatro);
                                                                inicio.Hijos.Add(cinco);
                                                                inicio.Hijos.Add(seis);
                                                                inicio.Hijos.Add(dato);
                                                                inicio.Hijos.Add(aactualizar);
                                                                inicio.Hijos.Add(siete);
                                                                inicio.Hijos.Add(ocho);
                                                                Console.WriteLine("ACTUALIZAR..............");
                                                                Nodo inicial = Inicio();
                                                                if (inicial != null)
                                                                {
                                                                    inicial.Padre = inicio;
                                                                    inicio.Hijos.Add(inicial);
                                                                }
                                                                return inicio;
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
                    if (actual.Lexema.Length > 0)
                    {
                        ErrorSintactico error = new ErrorSintactico(actual, "inicio de intruccion no reconocida  " + actual.Lexema);
                        Console.WriteLine("ERROR..........." + "inicio de instruccion no reconocida");
                        error.Descartados = RecuperacionErroresPanico();
                        erroresSintacticos.Add(error);
                        analizar();
                        return null;
                        //en caso de no venir nada
                    }
                }
            }
            return null;
        }

        public Nodo AActualizar()
        {
            Nodo aactualizar = new Nodo(null, null, "AACTUALIZAR");
            if (actual.Token1 == COMA)
            {
                Token token1 = Match(COMA, "SE ESPERABA SIGNO COMA");
                if (token1 != null)
                {
                    Nodo uno = new Nodo(aactualizar, token1, token1.Lexema);
                    Token token2 = Match(IDENTIFICADOR, "SE ESPERABA IDENTIFICADOR ");
                    if (token2 != null)
                    {
                        string columna = token2.Lexema;
                        Nodo dos = new Nodo(aactualizar, token2, token2.Lexema);
                        Token token3 = Match(IGUAL, "SE ESPERABA SGNO IGUAL");
                        if (token3 != null)
                        {
                            Nodo dato = Dato();
                            if (dato != null)
                            {
                                String dat = dato.Hijos[0].TokenN.Lexema;
                                dato.Padre = aactualizar;
                                Dato datoAC = new Dato();
                                datoAC.Columna = columna;
                                datoAC.Dato1 = dat;
                                datos.Add(datoAC);
                                aactualizar.Hijos.Add(uno);
                                aactualizar.Hijos.Add(dos);
                                aactualizar.Hijos.Add(dato);
                                Nodo aactual = AActualizar();
                                if (aactual != null)
                                {
                                    aactual.Padre = aactualizar;
                                    aactualizar.Hijos.Add(aactual);
                                }
                                return aactualizar;
                            }
                        }
                    }
                }
            }
            else
            {
                //epsilon
                Nodo epsilon = new Nodo(aactualizar, null, "epsilon");
                aactualizar.Hijos.Add(epsilon);
                return aactualizar;
            }
            return null;
        }
        //============================================ ARREGLAR EL RETURN 
        public Nodo CuerpoSelect()//A mejorar
        {
            Nodo cuerposelect = new Nodo(null, null, "CUERPOSELECT");
            //Console.WriteLine("CUERPO SELECT");
            if (ASTERISCO == actual.Token1)
            {
                Token t1 = Match(ASTERISCO, "SE ESPERABA ASTERISCO");
                if (t1 != null)
                {
                    Nodo uno = new Nodo(cuerposelect, t1, t1.Lexema);
                    Nodo desde = Desde();
                    if (desde != null)
                    {
                        desde.Padre = cuerposelect;
                        Nodo condicion = Condicion();
                        if (condicion != null)
                        {
                            if (condicion.Hijos[0].Estado != "epsilon")
                            {

                            }
                            else
                            {
                                if (nombresTablas.Count == 1)
                                {
                                    GeneradorHtml generador = new GeneradorHtml();
                                    String table = generador.GenerarConsultaHTML(tablas[instruccion.BuscarIndiceTabla(nombresTablas[0])]);
                                    generador.generarHTML(table, "/consulta.html");
                                }
                            }
                            condicion.Padre = cuerposelect;
                            condicion.Hijos.Add(uno);
                            condicion.Hijos.Add(desde);
                            condicion.Hijos.Add(condicion);
                            //Console.WriteLine("CUERPO  COND ACEPTADO");
                            return cuerposelect;
                        }
                    }
                }
            }
            else if (IDENTIFICADOR == actual.Token1)
            {
                Token t1 = Match(IDENTIFICADOR, "SE ESPERABA IDENTIFICADOR");
                //Console.WriteLine("CUERPO ID");
                if (t1 != null)
                {
                    String campo = t1.Lexema;
                    columnasS.Add(campo);
                    //Console.WriteLine("CUERPO SI ID");
                    Nodo uno = new Nodo(cuerposelect, t1, t1.Lexema);
                    Nodo mas = Mas('C');
                    if (mas != null)
                    {

                        Nodo cuerpoS = CuerpoSelect1();
                        if (cuerpoS != null)
                        {
                            cuerpoS.Padre = cuerposelect;
                            //Console.WriteLine("CAMPOS SELECT 1 CUERPO ");
                            Nodo nodo2 = Condicion();
                            if (nodo2 != null)
                            {
                                if (nodo2.Hijos[0].Estado != "epsilon")
                                {

                                }
                                else
                                {
                                    if (nombresTablas.Count == 1)
                                    {
                                        instruccion.seleccionarDe1Tabla(nombresTablas[0], columnasS, null);
                                    }
                                }
                                nodo2.Padre = cuerposelect;
                                cuerposelect.Hijos.Add(uno);
                                cuerposelect.Hijos.Add(cuerpoS);
                                cuerposelect.Hijos.Add(nodo2);
                                //Console.WriteLine("CAMPOS SELECT condicion aceptada ");
                                return cuerposelect;
                            }

                        }
                    }


                }
            }
            else
            {

                //metodo de error
            }
            return null;
        }

        public Nodo CuerpoSelect1()

        {
            Nodo campoSelect = new Nodo(null, null, "CUERPOSELECT1");
            if (actual != null)
            {
                if (actual.Token1 == DE)
                {
                    Nodo datosdesde = Desde();
                    if (datosdesde != null)
                    {
                        datosdesde.Padre = campoSelect;
                        campoSelect.Hijos.Add(datosdesde);
                        return campoSelect;
                    }

                }
                else if (actual.Token1 == PUNTO)
                {
                    //Console.WriteLine("CIERPO SELECT 1 PUNTO ");
                    Token uno = Match(PUNTO, "SE ESPERABA SIGNO: PUNTO  ");
                    if (uno != null)
                    {
                        Nodo uno1 = new Nodo(campoSelect, uno, uno.Lexema);
                        Nodo cuerpo = CuerpoSelect2();
                        if (cuerpo != null)
                        {
                            cuerpo.Padre = campoSelect;
                            campoSelect.Hijos.Add(uno1);
                            campoSelect.Hijos.Add(cuerpo);

                            return campoSelect;
                        }
                    }

                }
                else
                {
                    return null;
                    //errores
                }
            }
            return null;
        }
        public Nodo CuerpoSelect2()
        {
            Nodo cuerpoSelect = new Nodo(null, null, "CUERPOSELECT");
            int tokenS = actual.Token1;
            if (tokenS == ASTERISCO)
            {
                Token t1 = Match(ASTERISCO, "SE ESPERABA SIGNO: *  ");
                if (t1 != null)
                {
                    Nodo uno = new Nodo(cuerpoSelect, t1, t1.Lexema);
                    Nodo nodo = MasCampos();
                    if (MasCampos() != null)
                    {
                        nodo.Padre = cuerpoSelect;
                        Nodo desde = Desde();
                        if (desde != null)
                        {
                            desde.Padre = cuerpoSelect;
                            cuerpoSelect.Hijos.Add(uno);
                            cuerpoSelect.Hijos.Add(nodo);
                            cuerpoSelect.Hijos.Add(desde);
                            return cuerpoSelect;
                        }
                    }

                }
            }
            //else if (tokenS==MENORQUE|| tokenS==MENORIGUAL||tokenS==MAYORQUE||tokenS== MAYORIGUAL||tokenS == NOIGUAL||tokenS==IGUAL)
            else if (IDENTIFICADOR == actual.Token1)
            {
                Token token1 = Match(IDENTIFICADOR, "SE ESPERABA IDENTIFICADOR  ");
                // Console.WriteLine("CUERPO SELECT 2 IDENTIFICADOR");
                if (token1 != null)
                {
                    Nodo uno = new Nodo(cuerpoSelect, token1, token1.Lexema);
                    Nodo aleas = Aleas();
                    if (aleas != null)
                    {
                        aleas.Padre = cuerpoSelect;
                        //Console.WriteLine("CUERPO SELECT 2 ALEAS NO NULO");
                        Nodo mascampos = MasCampos();
                        if (mascampos != null)
                        {
                            mascampos.Padre = cuerpoSelect;
                            Nodo desde = Desde();
                            if (desde != null)
                            {
                                desde.Padre = cuerpoSelect;
                                cuerpoSelect.Hijos.Add(uno);
                                cuerpoSelect.Hijos.Add(aleas);
                                cuerpoSelect.Hijos.Add(mascampos);
                                cuerpoSelect.Hijos.Add(desde);
                                //Console.WriteLine("CUERPO SELECT 2 DESDE NO NULO ");
                                return cuerpoSelect;
                            }
                        }

                    }
                    else
                    {

                        Console.WriteLine("CUERPO SELECT 2 ALEAS NULO");
                        return null;
                    }
                }
            }
            else
            {
                return null;
            }
            return null;
        }
        public Nodo Arra()
        {
            Nodo ARRA = new Nodo(null, null, "ARRA");
            if (actual.Token1 == COMA)
            {
                Token token1 = Match(COMA, "SE ESPERABA SIGNO: ,  ");
                if (token1 != null)
                {
                    Nodo uno = new Nodo(ARRA, token1, token1.Lexema);
                    Token token2 = Match(IDENTIFICADOR, "SE ESPERABA IDENTIFICADOR  ");
                    if (token2 != null)
                    {
                        Nodo dos = new Nodo(ARRA, token2, token2.Lexema);
                        Token token3 = Match(PUNTO, "SE ESPERABA SIGNO: .  ");
                        if (token3 != null)
                        {
                            Nodo tres = new Nodo(ARRA, token3, token3.Lexema);
                            Token token4 = Match(IDENTIFICADOR, "SE ESPERABA IDENTIFICADOR");
                            if (token4 != null)
                            {
                                Nodo cuatro = new Nodo(ARRA, token4, token4.Lexema);
                                Nodo aleas = Aleas();
                                if (aleas != null)
                                {
                                    aleas.Padre = ARRA;
                                    ARRA.Hijos.Add(uno);
                                    ARRA.Hijos.Add(dos);
                                    ARRA.Hijos.Add(tres);
                                    ARRA.Hijos.Add(cuatro);
                                    ARRA.Hijos.Add(aleas);
                                    return aleas;
                                }
                                else
                                {
                                    return null;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                return ARRA;
                //epsilon
            }
            return null;
        }
        public Nodo Aleas()
        {
            Nodo aleas = new Nodo(null, null, "ALEAS");
            if (actual.Token1 == COMO)
            {
                Token token = Match(COMO, "SE ESPERABA PALABRA COMO ");
                if (token != null)
                {
                    Nodo uno = new Nodo(aleas, token, token.Lexema);
                    Token token2 = Match(IDENTIFICADOR, "SE ESPERABA IDENTIDICADOR  ");
                    if (token2 != null)
                    {
                        Nodo dosAleas = new Nodo(aleas, token2, token2.Lexema);
                        aleas.Hijos.Add(uno);
                        aleas.Hijos.Add(dosAleas);
                        return aleas;
                    }
                }
            }
            else
            {
                Nodo epsilon = new Nodo(aleas, null, "epsilon");
                aleas.Hijos.Add(epsilon);
                return aleas;
                //EPSILON
            }
            return null;
        }

        public Nodo Desde()
        {
            Nodo desde = new Nodo(null, null, "DESDE");
            Token token1 = Match(DE, "SE ESPERABA PALABRA: DE  ");
            if (token1 != null)
            {
                Nodo uno = new Nodo(desde, token1, token1.Lexema);
                Token token2 = Match(IDENTIFICADOR, "SE ESPERABA IDENTIFICADOR  ");

                Console.WriteLine("CUERPO DE");
                if (token2 != null)
                {
                    string nombreTabla = token2.Lexema;
                    nombresTablas.Add(nombreTabla);
                    Nodo dos = new Nodo(desde, token2, token2.Lexema);
                    Nodo lista = Mas('T');
                    if (lista != null)
                    {
                        lista.Padre = desde;
                        desde.Hijos.Add(uno);
                        desde.Hijos.Add(dos);
                        desde.Hijos.Add(lista);
                        Console.WriteLine("lista no  nula Desde");
                        return desde;
                    }
                    else
                    {
                        lista.Padre = desde;
                        desde.Hijos.Add(uno);
                        desde.Hijos.Add(dos);
                        Console.WriteLine("lista no  nula Desde");
                        return desde;
                    }

                }
            }
            Console.WriteLine("lista nula");
            return null;
        }

        public Nodo Mas(char tipo)
        {
            Nodo mas = new Nodo(null, null, "LIST");
            if (actual.Token1 == COMA)
            {
                Token token1 = Match(COMA, "SE ESPERABA SIGNO: COMA  ");
                if (token1 != null)
                {
                    Nodo nodo = new Nodo(mas, token1, token1.Lexema);
                    Token letra = Match(IDENTIFICADOR, "SE ESPERABA IDENTIFICADOR  ");
                    if (letra != null)
                    {
                        if (tipo == 'C')
                        {
                            columnasS.Add(letra.Lexema);
                        }
                        else
                        {
                            nombresTablas.Add(letra.Lexema);
                        }


                        Nodo let = new Nodo(mas, letra, letra.Lexema);
                        mas.Hijos.Add(nodo);
                        mas.Hijos.Add(let);
                        // letras1.Add(letra);
                        Nodo m = Mas(tipo);
                        if (m != null)
                        {
                            m.Padre = mas;
                            mas.Hijos.Add(m);
                        }

                        return mas;
                    }
                }
            }
            else
            {
                Nodo epsilon = new Nodo(mas, null, "epsilon");
                mas.Hijos.Add(epsilon);
                return mas;
                //epsilon
            }
            return null;
        }
        public Nodo Condicion()
        {
            Nodo condicion = new Nodo(null, null, "CONDICION");
            if (actual != null)
            {
                if (actual.Token1 == DONDE)
                {
                    Token token1 = Match(DONDE, "SE ESPERABA LA PALABRA RESERVADA DONDE");
                    if (token1 != null)
                    {
                        Nodo uno = new Nodo(condicion, token1, token1.Lexema);
                        // Console.WriteLine("DONDE ");
                        Nodo nod = Cond();
                        if (nod != null)
                        {
                            nod.Padre = condicion;
                            Nodo nodcond = CuerpoCond();
                            if (nodcond != null)
                            {
                                nodcond.Padre = condicion;
                                condicion.Hijos.Add(uno);
                                condicion.Hijos.Add(nod);
                                condicion.Hijos.Add(nodcond);
                                return condicion;
                            }

                        }
                    }
                }
                else
                {
                    Nodo epsilon = new Nodo(condicion, null, "epsilon");
                    condicion.Hijos.Add(epsilon);
                    return condicion;
                }
            }

            return null;
        }
        List<Condicion> condiciones;
        Condicion conActual;
        public Nodo Cond()
        {
            conActual = new Condicion();
            Nodo condicion = new Nodo(null, null, "COND");
            Console.WriteLine("COND ");
            Token token1 = Match(IDENTIFICADOR, "SE ESPERABA UN IDENTIFICADOR");
            if (token1 != null)
            {
                conActual.Tabla = token1.Lexema;
                Nodo uno = new Nodo(condicion, token1, token1.Lexema);
                Nodo dos = Condi();

                //Console.WriteLine("COND ACEPTADA");
                if (dos != null)
                {
                    condiciones.Add(conActual);
                    dos.Padre = condicion;
                    condicion.Hijos.Add(uno);
                    condicion.Hijos.Add(dos);
                    return condicion;
                }
            }
            else
            {
                return null;
            }
            return null;
        }
        public Nodo Condi()
        {
            Nodo condi = new Nodo(null, null, "CONDI");
            // Console.WriteLine("CONDI.....m......"+actual.Lexema);
            int tokenS = actual.Token1;
            if (actual.Token1 == PUNTO)
            {

                Token token1 = Match(PUNTO, "SE ESPERABA UN IDENTIFICADOR");
                if (token1 != null)
                {
                    Nodo uno = new Nodo(condi, token1, token1.Lexema);
                    Token token2 = Match(IDENTIFICADOR, "SE ESPERABA UN IDENTIFICADOR");
                    if (token2 != null)
                    {
                        Nodo dos = new Nodo(condi, token2, token2.Lexema);
                        Token token3 = Match(IGUAL, "SE ESPERABA UN SIGNO IGUAL");
                        if (token3 != null)
                        {
                            Nodo tres = new Nodo(condi, token3, token3.Lexema);
                            Token token4 = Match(IDENTIFICADOR, "SE ESPERABA UN IDENTIFICADOR");
                            if (token4 != null)
                            {
                                Nodo cuatro = new Nodo(condi, token4, token4.Lexema);
                                Token token5 = Match(PUNTO, "SE ESPERABA UN PUNTO");
                                if (token5 != null)
                                {
                                    Nodo cinco = new Nodo(condi, token5, token5.Lexema);
                                    Token token6 = Match(IDENTIFICADOR, "SE ESPERABA UN IDENTIFICADOR");
                                    if (token6 != null)
                                    {
                                        Nodo seis = new Nodo(condi, token6, token6.Lexema);
                                        condi.Hijos.Add(uno);
                                        condi.Hijos.Add(dos);
                                        condi.Hijos.Add(tres);
                                        condi.Hijos.Add(cuatro);
                                        condi.Hijos.Add(cinco);
                                        condi.Hijos.Add(seis);
                                        return condi;
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
                Nodo bodycondi = BodyCond();
                if (bodycondi != null)
                {
                    return bodycondi;
                }
            }
            else
            {
                return null;
            }
            return null;
        }

        public Nodo CuerpoCond()
        {
            Nodo cuperpocond = new Nodo(null, null, "CUERPOCONDI");
            if (actual.Token1 == Y)
            {
                Token uno = Match(Y, "SE ESPERABA  LETRA Y");
                if (uno != null)
                {
                    Nodo uno1 = new Nodo(cuperpocond, uno, uno.Lexema);
                    Nodo condi = Cond();
                    if (condi != null)
                    {
                        condi.Padre = cuperpocond;
                        cuperpocond.Hijos.Add(uno1);
                        cuperpocond.Hijos.Add(condi);
                        Nodo este = CuerpoCond();
                        if (este != null)
                        {
                            este.Padre = cuperpocond;
                            cuperpocond.Hijos.Add(este);
                        }
                        return cuperpocond;
                    }

                }
            }
            else if (actual.Token1 == O)
            {
                Token uno = Match(O, "SE ESPERABA LETRA O");
                if (uno != null)
                {
                    Nodo uno1 = new Nodo(cuperpocond, uno, uno.Lexema);
                    Nodo condi = Cond();
                    if (condi != null)
                    {
                        condi.Padre = cuperpocond;
                        cuperpocond.Hijos.Add(uno1);
                        cuperpocond.Hijos.Add(condi);
                        Nodo este = CuerpoCond();
                        if (este != null)
                        {
                            este.Padre = cuperpocond;
                            cuperpocond.Hijos.Add(este);
                        }
                        return cuperpocond;
                    }
                }
            }
            else
            {
                Nodo epsilon = new Nodo(cuperpocond, null, "epsilon");
                cuperpocond.Hijos.Add(epsilon);
                return cuperpocond;
                // puede venir vacia
            }
            return null;
        }

        public Nodo BodyCond()
        {
            Nodo bodyCond = new Nodo(null, null, "BODYCOND");
            int tokenActual = actual.Token1;

            if (tokenActual == MAYORQUE)
            {
                Token t1 = Match(MAYORQUE, "SE ESPERABA signo >");
                conActual.Signo = t1.Lexema;
                if (t1 != null)
                {
                    Nodo uno = new Nodo(bodyCond, t1, t1.Lexema);
                    Nodo dato = Dato();
                    if (dato != null)
                    {
                        dato.Padre = bodyCond;
                        bodyCond.Hijos.Add(uno);
                        bodyCond.Hijos.Add(dato);
                        return bodyCond;
                    }
                }
            }
            else if (tokenActual == IGUAL)
            {
                Token t2 = Match(IGUAL, "SE ESPERABA signo =");
                if (t2 != null)
                {
                    conActual.Signo = t2.Lexema;
                    Nodo uno = new Nodo(bodyCond, t2, t2.Lexema);
                    Nodo dato = Dato();
                    if (dato != null)
                    {
                        dato.Padre = bodyCond;
                        bodyCond.Hijos.Add(uno);
                        bodyCond.Hijos.Add(dato);
                        return bodyCond;
                    }
                }
            }
            else if (tokenActual == NOIGUAL)
            {
                Token t2 = Match(NOIGUAL, "SE ESPERABA signo !=");
                if (t2 != null)
                {
                    conActual.Signo = t2.Lexema;
                    Nodo uno = new Nodo(bodyCond, t2, t2.Lexema);
                    Nodo dato = Dato();
                    if (dato != null)
                    {
                        dato.Padre = bodyCond;
                        bodyCond.Hijos.Add(uno);
                        bodyCond.Hijos.Add(dato);
                        return bodyCond;
                    }
                }
            }
            else if (tokenActual == MENORQUE)
            {
                Token t2 = Match(MENORQUE, "SE ESPERABA signo <");
                if (t2 != null)
                {
                    conActual.Signo = t2.Lexema;
                    Nodo uno = new Nodo(bodyCond, t2, t2.Lexema);
                    Nodo dato = Dato();
                    if (dato != null)
                    {
                        dato.Padre = bodyCond;
                        bodyCond.Hijos.Add(uno);
                        bodyCond.Hijos.Add(dato);
                        return bodyCond;
                    }
                }
            }
            else if (tokenActual == MAYORIGUAL)
            {
                Token t2 = Match(MAYORIGUAL, "SE ESPERABA signo >=");
                if (t2 != null)
                {
                    conActual.Signo = t2.Lexema;
                    Nodo uno = new Nodo(bodyCond, t2, t2.Lexema);
                    Nodo dato = Dato();
                    if (dato != null)
                    {
                        dato.Padre = bodyCond;
                        bodyCond.Hijos.Add(uno);
                        bodyCond.Hijos.Add(dato);
                        return bodyCond;
                    }
                }
            }
            else if (tokenActual == MENORIGUAL)
            {
                Token t2 = Match(MENORIGUAL, "SE ESPERABA signo <=");
                if (t2 != null)
                {
                    conActual.Signo = t2.Lexema;
                    Nodo uno = new Nodo(bodyCond, t2, t2.Lexema);
                    Nodo dato = Dato();
                    if (dato != null)
                    {
                        conActual.Dato = dato.TokenN.Lexema;
                        dato.Padre = bodyCond;
                        bodyCond.Hijos.Add(uno);
                        bodyCond.Hijos.Add(dato);
                        return bodyCond;
                    }
                }
            }
            else
            {
                return null;
            }
            return null;
        }

        public Nodo MasCampos()
        {
            Nodo masCampos = new Nodo(null, null, "MASCAMPOS");
            if (actual.Token1 == COMA)
            {

                Token t1 = Match(this.COMA, "SE ESPERABA COMA");
                if (t1 != null)
                {
                    Nodo uno = new Nodo(masCampos, t1, t1.Lexema);

                    Token t2 = Match(this.IDENTIFICADOR, "SE ESPERABA IDENTIFICADOR");
                    if (t2 != null)
                    {
                        Nodo dos = new Nodo(masCampos, t2, t2.Lexema);
                        Token t3 = Match(this.PUNTO, "SE ESPERABA PUNTO");
                        if (t3 != null)
                        {
                            Nodo tres = new Nodo(masCampos, t3, t3.Lexema);
                            Token t4 = Match(this.IDENTIFICADOR, "SE ESPERABA IDENTIFICADOR");
                            if (t4 != null)
                            {
                                Nodo cuatro = new Nodo(masCampos, t4, t4.Lexema);
                                Nodo aleas = Aleas();

                                if (aleas != null)
                                {
                                    aleas.Padre = masCampos;
                                    Nodo mas = MasCampos();
                                    if (mas != null)
                                    {
                                        mas.Padre = masCampos;
                                        masCampos.Hijos.Add(uno);
                                        masCampos.Hijos.Add(dos);
                                        masCampos.Hijos.Add(tres);
                                        masCampos.Hijos.Add(cuatro);
                                        masCampos.Hijos.Add(aleas);
                                        masCampos.Hijos.Add(mas);

                                    }
                                    return masCampos;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                Nodo epsilon = new Nodo(masCampos, null, "epsilon");
                masCampos.Hijos.Add(epsilon);
                return masCampos;
                // epsilon
            }
            return null;
        }


        public Nodo CuerpoCrear()
        {
            int tokenActual = actual.Token1;
            Nodo cuerpoCrear = new Nodo(null, null, "CUERPOCREAR");
            if (tokenActual == this.COMA)
            {
                Token token = Match(this.COMA, "SE ESPERABA COMA");
                if (token != null)
                {
                    Nodo coma = new Nodo(cuerpoCrear, token, token.Lexema);
                    Token id = Match(IDENTIFICADOR, "SE ESPERABA IDENTIFICADOR");
                    Nodo tipo = TipoCampoId();
                    if (id != null && tipo != null)
                    {
                        tipo.Padre = cuerpoCrear;
                        dato = new Dato();
                        Nodo uno = new Nodo(cuerpoCrear, id, id.Lexema);
                        dato.Dato1 = id.Lexema;
                        dato.Tipo = tipo.Hijos[0].TokenN.Lexema;
                        columnas.Add(dato);
                        cuerpoCrear.Hijos.Add(coma);
                        cuerpoCrear.Hijos.Add(uno);
                        cuerpoCrear.Hijos.Add(tipo);
                        Nodo cuerpo = CuerpoCrear();
                        if (cuerpo != null)
                        {
                            cuerpo.Padre = cuerpoCrear;
                            cuerpoCrear.Hijos.Add(cuerpo);

                        }
                        return cuerpoCrear;
                    }
                }
            }
            else
            {
                Nodo epsilon = new Nodo(cuerpoCrear, null, "epsilon");
                cuerpoCrear.Hijos.Add(epsilon);
                return cuerpoCrear;
            }

            return null;
        }

        public Nodo CuerpoDatos()
        {
            Nodo cuerpoDatos = new Nodo(null, null, "CuerpoDatos");
            if (actual.Token1 == COMA)
            {
                // Console.WriteLine("CUEPRO DATOS"+ actual.Token1);

                Token token1 = Match(COMA, "SE ESPERABA COMA");
                if (token1 != null)
                {
                    Nodo uno = new Nodo(cuerpoDatos, token1, token1.Lexema);
                    Nodo dato = Dato();
                    if (dato != null)
                    {
                        Dato dt = new Dato();
                        dt.Dato1 = dato.Hijos[0].TokenN.Lexema;
                        datos.Add(dt);
                        dato.Padre = cuerpoDatos;
                        Nodo cDatos = CuerpoDatos();
                        cuerpoDatos.Hijos.Add(uno);
                        cuerpoDatos.Hijos.Add(dato);
                        if (cDatos != null)
                        {
                            cDatos.Padre = cuerpoDatos;
                            cuerpoDatos.Hijos.Add(cDatos);
                        }
                        return cuerpoDatos;
                    }
                }
            }
            else
            {
                Nodo epsilon = new Nodo(cuerpoDatos, null, "epsilon");
                cuerpoDatos.Hijos.Add(epsilon);
                return cuerpoDatos;
                //puede venir epsilon
            }
            return null;
        }

        public Nodo TipoCampoId()
        {
            Nodo tipodeCaMPO = new Nodo(null, null, "TIPOCAMPOID");
            //Console.WriteLine("tipos campo");
            if (actual.Token1 == ENTEROID)
            {
                Token dt = Match(ENTEROID, "SE ESPERABA UN TIPO DE DATO VALIDO ");
                if (dt != null)
                {
                    Nodo uno = new Nodo(tipodeCaMPO, dt, dt.Lexema);
                    tipodeCaMPO.Hijos.Add(uno);
                    return tipodeCaMPO;
                }
            }
            else if (actual.Token1 == CADENAID)
            {
                Token dt = Match(CADENAID, "SE ESPERABA UN TIPO DE DATO VALIDO ");
                if (dt != null)
                {
                    Nodo uno = new Nodo(tipodeCaMPO, dt, dt.Lexema);
                    tipodeCaMPO.Hijos.Add(uno);
                    return tipodeCaMPO;
                }
            }
            else if (actual.Token1 == FLOTANTEID)
            {
                Token dt = Match(FLOTANTEID, "SE ESPERABA UN TIPO DE DATO VALIDO ");
                if (dt != null)
                {
                    Nodo uno = new Nodo(tipodeCaMPO, dt, dt.Lexema);
                    tipodeCaMPO.Hijos.Add(uno);
                    return tipodeCaMPO;
                }
            }
            else if (actual.Token1 == FECHAID)
            {
                Token dt = Match(FECHAID, "SE ESPERABA UN TIPO DE DATO VALIDO ");
                if (dt != null)
                {
                    Nodo uno = new Nodo(tipodeCaMPO, dt, dt.Lexema);
                    tipodeCaMPO.Hijos.Add(uno);
                    return tipodeCaMPO;
                }
            }
            else
            {
                return null;
                //ERROR SINTACTICO
            }
            return null;
        }

        public Nodo Dato()
        {
            Nodo dato = new Nodo(null, null, "DATO");
            if (actual.Token1 == FECHA)
            {
                Token T = Match(FECHA, "SE ESPERABA UN DATO VALIDO ");
                Nodo nd = new Nodo(dato, T, T.Lexema);
                dato.Hijos.Add(nd);
                return dato;
            }
            else if (actual.Token1 == ENTERO)
            {
                Token T = Match(ENTERO, "SE ESPERABA UN DATO VALIDO ");
                Nodo nd = new Nodo(dato, T, T.Lexema);
                dato.Hijos.Add(nd);
                return dato;
            }
            else if (actual.Token1 == CADENA)
            {
                Token t = Match(CADENA, "SE ESPERABA UN DATO VALIDO ");
                Nodo nd = new Nodo(dato, t, t.Lexema);
                dato.Hijos.Add(nd);
                return dato;
            }
            else if (actual.Token1 == FLOTANTE)
            {
                Token t = Match(FLOTANTE, "SE ESPERABA UN DATO VALIDO ");
                Nodo nd = new Nodo(dato, t, t.Lexema);
                dato.Hijos.Add(nd);
                return dato;
            }
            else
            {
                return null;
            }
        }

        public List<Token> RecuperacionErroresPanico()
        {
            List<Token> descartados = new List<Token>();
            Console.WriteLine(i);
            for (int j = i; j < tokens.Count; j++)
            {
                if (tokens[j].Lexema == ";")
                {
                    Console.WriteLine(j + tokens[j].Lexema);
                    descartados.Add(tokens[j]);
                    i = j + 1;
                    break;
                }
                else
                {
                    descartados.Add(tokens[j]);
                }

            }
            Console.WriteLine(i);
            return descartados;
            //actual = tokens[i+1];
            //Inicio();
        }
    }
}

