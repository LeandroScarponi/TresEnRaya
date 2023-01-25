using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace TresEnRaya
{
    internal class Program
    {
        static void Main(string[] args)
        {
            char[,] tablero = new char[3, 3];
            string jugador1 = "";
            string jugador2 = "";
            string coordenada = "";
            char letra1 = 'X';
            char letra2 = 'O';

            Console.WriteLine("BIENVENIDO AL TRES EN RAYA!");
            Console.WriteLine("Primero, necesitaremos los nombres de los jugadores.");
            Console.WriteLine();
            PedirJugadores(ref jugador1, ref jugador2, letra1, letra2);
            InicializarTablero(ref tablero);
            Console.WriteLine(DibujarTablero(tablero));
            ComenzarJuego(ref tablero, jugador1, jugador2, coordenada, letra1, letra2);
            
            Console.ReadKey();
        }

        static void PedirJugadores(ref string jugador1, ref string jugador2, char letra1, char letra2)
        {
            while (jugador1 == "")
            {
                Console.WriteLine("Ingrese el nombre del jugador 1");
                jugador1 = Console.ReadLine().ToUpper();
            }
            Console.Clear();
            Console.WriteLine($"Bienvenido/a {jugador1}. Tú serás {letra1}.");
            Console.ReadKey();
            Console.Clear();

            while (jugador2 == "")
            {
                Console.WriteLine("Ingrese el nombre del jugador 2.");
                jugador2 = Console.ReadLine().ToUpper();
            }
            Console.Clear();
            Console.WriteLine($"Bienvenido/a {jugador2}. Tú serás {letra2}.");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("COMENCEMOS!");
            Console.ReadKey();
            Console.Clear();

        }

        static void ComenzarJuego(ref char[,] tablero, string jugador1, string jugador2, string coordenada, char letra1, char letra2)
        {
            while (!HayGanador(ref tablero))
            {
                Console.WriteLine($"Es el turno de {jugador1} = {letra1}");
                PonerCoordenadas(ref tablero, coordenada, letra1);
                Console.Clear();
                Console.WriteLine(DibujarTablero(tablero));


                if (!HayGanador(ref tablero))
                {
                    Console.WriteLine($"Es el turno de {jugador2} = {letra2}");
                    PonerCoordenadas(ref tablero, coordenada, letra2);
                    Console.Clear();
                    Console.WriteLine(DibujarTablero(tablero));

                    if (HayGanador(ref tablero))
                    {
                        Console.WriteLine($"HA GANADO {jugador2}");
                        Console.WriteLine();
                        Console.WriteLine("Gracias por jugar! Presione una tecla para salir.");
                    }
                }
                else
                {
                    Console.WriteLine($"HA GANADO {jugador1}");
                    Console.WriteLine();
                    Console.WriteLine("Gracias por jugar! Presione una tecla para salir.");
                }
            }
        }

        static void InicializarTablero(ref char[,] tablero)
        {
            //ESCRIBE UN ESPACIO EN BLANCO EN EL CENTRO DE CADA CUADRO
            for (int i = 0; i < tablero.GetLength(0); i++)
            {
                for (int j = 0; j < tablero.GetLength(0); j++)
                {
                    tablero[i, j] = ' ';
                }
            }
        }

        static string DibujarTablero(char[,] tablero) 
        {
            //DIBUJO EL TABLERO E INCLUYO EN CADA CUADRO LAS COORDENADAS
            string tableroVisual;
            tableroVisual = "     A   B   C" + Environment.NewLine;
            tableroVisual += "   ┌───┬───┬───┐" + Environment.NewLine;
            tableroVisual += $"1  │ {tablero[0,0]} │ {tablero[0,1]} │ {tablero[0,2]} │" + Environment.NewLine;
            tableroVisual += "   ├───┼───┼───┤" + Environment.NewLine;
            tableroVisual += $"2  │ {tablero[1, 0]} │ {tablero[1, 1]} │ {tablero[1, 2]} │" + Environment.NewLine;
            tableroVisual += "   ├───┼───┼───┤" + Environment.NewLine;
            tableroVisual += $"3  │ {tablero[2, 0]} │ {tablero[2, 1]} │ {tablero[2, 2]} │" + Environment.NewLine;
            tableroVisual += "   └───┴───┴───┘" + Environment.NewLine;

            return tableroVisual;

        }

        static char Ganador(char[,] tablero)
        {
            //HORIZONTALES
            if (tablero[0,0] == tablero[0,1] && tablero[0,1] == tablero[0,2] && tablero[0,0] != ' ')
            {
                return tablero[0,0];
            }
            if (tablero[1,0] == tablero[1,1] && tablero[1,1] == tablero[1,2] && tablero[1,0] != ' ')
            {
                return tablero [1,0];
            }
            if (tablero[2,0] == tablero[2,1] && tablero[2,1] == tablero[2,2] && tablero[2,0] != ' ')
            {
                return tablero[2, 0];
            }

            //VERTICALES
            if (tablero[0, 0] == tablero[1, 0] && tablero[1, 0] == tablero[2, 0] && tablero[0, 0] != ' ')
            {
                return tablero[0, 0];
            }
            if (tablero[0, 1] == tablero[1, 1] && tablero[1, 1] == tablero[2, 1] && tablero[0, 1] != ' ')
            {
                return tablero[0, 1];
            }
            if (tablero[0, 2] == tablero[1, 2] && tablero[1, 2] == tablero[2, 2] && tablero[0, 2] != ' ')
            {
                return tablero[0, 2];
            }

            //DIAGONALES
            if (tablero[0, 0] == tablero[1, 1] && tablero[1, 1] == tablero[2, 2] && tablero[0, 0] != ' ')
            {
                return tablero[0, 0];
            }
            if (tablero[0, 2] == tablero[1, 1] && tablero[1, 1] == tablero[2, 0] && tablero[0, 2] != ' ')
            {
                return tablero[0, 2];
            }

            return ' ';

        }

        static bool HayGanador(ref char[,] tablero)
        {
            return Ganador(tablero) != ' ';
        }

        static void PonerCoordenadas(ref char[,] tablero, string coordenada, char letra)
        {
            coordenada = Console.ReadLine();
            coordenada = coordenada.ToUpper();

            switch (coordenada)
            {
                case "A1":
                    if (tablero[0, 0] == ' ')
                    {
                        tablero[0, 0] = letra;
                    }
                    else
                    {
                        Console.WriteLine("CASILLA OCUPADA");
                        PonerCoordenadas(ref tablero, coordenada, letra);
                    }
                    return;
                case "A2":
                    if (tablero[1, 0] == ' ')
                    {
                        tablero[1, 0] = letra;
                    }
                    else
                    {
                        Console.WriteLine("CASILLA OCUPADA");
                        PonerCoordenadas(ref tablero, coordenada, letra);
                    }
                    return;
                case "A3":
                    if (tablero[2, 0] == ' ')
                    {
                        tablero[2, 0] = letra;
                    }
                    else
                    {
                        Console.WriteLine("CASILLA OCUPADA");
                        PonerCoordenadas(ref tablero, coordenada, letra);
                    }
                    return;
                case "B1":
                    if (tablero[0, 1] == ' ')
                    {
                        tablero[0, 1] = letra;
                    }
                    else
                    {
                        Console.WriteLine("CASILLA OCUPADA");
                        PonerCoordenadas(ref tablero, coordenada, letra);
                    }
                    return;
                case "B2":
                    if (tablero[1, 1] == ' ')
                    {
                        tablero[1, 1] = letra;
                    }
                    else
                    {
                        Console.WriteLine("CASILLA OCUPADA");
                        PonerCoordenadas(ref tablero, coordenada, letra);
                    }
                    return;
                case "B3":
                    if (tablero[2, 1] == ' ')
                    {
                        tablero[2, 1] = letra;
                    }
                    else
                    {
                        Console.WriteLine("CASILLA OCUPADA");
                        PonerCoordenadas(ref tablero, coordenada, letra);
                    }
                    return;
                case "C1":
                    if (tablero[0, 2] == ' ')
                    {
                        tablero[0, 2] = letra;
                    }
                    else
                    {
                        Console.WriteLine("CASILLA OCUPADA");
                        PonerCoordenadas(ref tablero, coordenada, letra);
                    }
                    return;
                case "C2":
                    if (tablero[1, 2] == ' ')
                    {
                        tablero[1, 2] = letra;
                    }
                    else
                    {
                        Console.WriteLine("CASILLA OCUPADA");
                        PonerCoordenadas(ref tablero, coordenada, letra);
                    }
                    return;
                case "C3":
                    if (tablero[2, 2] == ' ')
                    {
                        tablero[2, 2] = letra;
                    }
                    else
                    {
                        Console.WriteLine("CASILLA OCUPADA");
                        PonerCoordenadas(ref tablero, coordenada, letra);
                    }
                    return;
                default:
                    Console.WriteLine("ERROR. Ingrese un campo válido.");
                    PonerCoordenadas(ref tablero, coordenada, letra);
                    break;
                    

            }
        }

    }
}
