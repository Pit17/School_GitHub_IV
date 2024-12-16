
﻿using Microsoft.Win32.SafeHandles;
using System;
using System.Text;
using System.Threading;
using static System.Console;
//Malzone Pietro 4H 09/12/2024
//Corsa Treni
namespace CorsaConTreni
{
    internal class Program
    {
        static Object _lock = new Object(); // lock 
        static Random _random = new Random();
        static Thread[] _treni = new Thread[2];

        private static void Scrivi(int sinistra, int sopra, string testo)
        {
            lock (_lock) // lock console
            {
                try
                {
                    SetCursorPosition(sinistra, sopra); //posizione del cursore

                }
                catch (Exception ex) {  }
                Write(testo); 
            }
        }

        class Treno
        {

            private int numero;
            private int velocita;
            private string[] treno =
            {
                "╔═══╗",
                "║   ║",
                "║   ║",
                "║   ║",
                "╚═══╝",
                "  |  ",
                "╔═══╗",
                "║   ║",
                "║   ║",
                "║   ║",
                "╚═══╝",
                "  |  ",
                "╔═══╗",
                "║   ║",
                "║   ║",
                "║ ☻ ║",
                "╚═══╝",
            };//Disegno treno

            public Treno(int n)//randomizzo i dati del treno
            {
                numero = n;
                velocita = _random.Next(50, 500);
            }

            public void StampaTreno()//stampo il treno lungo il suo percorso
            {
                int x = 30 * numero + (6 * (numero - 1)), y = 0, y1;
                while (y < 30)
                {
                    y1 = y;
                    foreach (var r in treno)
                    {
                        if (y1 == 30)
                            break;
                        Scrivi(x, y1++, r);
                    }
                    Thread.Sleep(velocita);
                    Scrivi(x, y++, new string(' ', 5));
                }
            }
        }

        class Persona//creo una persona
        {
            public int pos { get; private set; }//pos nello schermo
            public string name { get; private set; }//nome

            private string[] persona =
            {
                @"  () ",
                @" [██]",
                @"  /\ "
            };

            public Persona(int pos, string name)
            {
                this.pos = pos;
                this.name = name;
            }

            public void Muovi()//muovo fino al fondo
            {
                while (pos <115)
                {
                    if (pos == 25 && _treni[0].IsAlive)
                    { Thread.Sleep(50); continue; }
                    if (pos == 60 && _treni[1].IsAlive)
                    { Thread.Sleep(50); continue; }

                    for (int i = 0; i < persona.Length; i++)
                        Scrivi(pos, 10 + i, persona[i]);
                    pos++;
                    Thread.Sleep(50);
                }
                
            }
        }

        static private void StampaFerrovia()//disegno base
        {
            WriteLine("                             |" + new string(' ', 5) + "|Stato treno 1                |" + new string(' ', 5) + "|Stato treno 2");
            WriteLine("   ╔═════════════════╗       |" + new string(' ', 5) + "|                             |" + new string(' ', 5) + "|");
            WriteLine("   ║    STAZIONE     ║       |" + new string(' ', 5) + "|                             |" + new string(' ', 5) + "|");
            WriteLine("   ║       DI        ║       |" + new string(' ', 5) + "|Is alive =                   |" + new string(' ', 5) + "|Is alive = ");
            WriteLine("   ║     CESENA      ║       |" + new string(' ', 5) + "|                             |" + new string(' ', 5) + "|");
            WriteLine("   ╚═════════════════╝       |" + new string(' ', 5) + "|                             |" + new string(' ', 5) + "|");
            WriteLine("                             |" + new string(' ', 5) + "|                             |" + new string(' ', 5) + "|");
            WriteLine("                             |" + new string(' ', 5) + "|                             |" + new string(' ', 5) + "|");
            WriteLine("                             |" + new string(' ', 5) + "|                             |" + new string(' ', 5) + "|");
            WriteLine("░░ ░░ ░░ ░░ ░░ ░░ ░░ ░░ ░░ ░░■" + new string(' ', 5) + "■░░ ░░ ░░ ░░ ░░ ░░ ░░ ░░ ░░ ░░■" + new string(' ', 5) + "■░░ ░░ ░░ ░░ ░░ ░░ ░░ ░░ ░░ ░░ ░░ ░░ ░░ ░░ ░░ ░░");
            WriteLine("                              " + new string(' ', 5) +  "                              " + new string(' ', 5));          
            WriteLine("                              " + new string(' ', 5) +  "                              " + new string(' ', 5));
            WriteLine("                              " + new string(' ', 5) +  "                              " + new string(' ', 5));
            WriteLine("                              " + new string(' ', 5) +  "                              " + new string(' ', 5));
            WriteLine("░░ ░░ ░░ ░░ ░░ ░░ ░░ ░░ ░░ ░░■" + new string(' ', 5) + "■░░ ░░ ░░ ░░ ░░ ░░ ░░ ░░ ░░ ░░■" + new string(' ', 5) + "■░░ ░░ ░░ ░░ ░░ ░░ ░░ ░░ ░░ ░░ ░░ ░░ ░░ ░░ ░░ ░░");
            WriteLine("                             |" + new string(' ', 5) + "|                             |" + new string(' ', 5) + "|");
            WriteLine("                             |" + new string(' ', 5) + "|                             |" + new string(' ', 5) + "|");
            WriteLine("                             |" + new string(' ', 5) + "|                             |" + new string(' ', 5) + "|");
            WriteLine("                             |" + new string(' ', 5) + "|                             |" + new string(' ', 5) + "|");
            WriteLine("   ╔═════════════════╗       |" + new string(' ', 5) + "|                             |" + new string(' ', 5) + "|");
            WriteLine("   ║ Malzone Pietro  ║       |" + new string(' ', 5) + "|                             |" + new string(' ', 5) + "|");
            WriteLine("   ║       4°H       ║       |" + new string(' ', 5) + "|                             |" + new string(' ', 5) + "|");
            WriteLine("   ║    16/12/2024   ║       |" + new string(' ', 5) + "|                             |" + new string(' ', 5) + "|");
            WriteLine("   ╚═════════════════╝       |" + new string(' ', 5) + "|                             |" + new string(' ', 5) + "|");
            WriteLine("                             |" + new string(' ', 5) + "|                             |" + new string(' ', 5) + "|");
            WriteLine("                             |" + new string(' ', 5) + "|                             |" + new string(' ', 5) + "|");
            WriteLine("                             |" + new string(' ', 5) + "|                             |" + new string(' ', 5) + "|");
            WriteLine("                             |" + new string(' ', 5) + "|                             |" + new string(' ', 5) + "|");
            WriteLine("                             |" + new string(' ', 5) + "|                             |" + new string(' ', 5) + "|");
            WriteLine("                             |" + new string(' ', 5) + "|                             |" + new string(' ', 5) + "|");
        }

        static void AggiornaStato()//aggiorno la pos dei treni
        {
            bool treno0, treno1;
            do
            {
                treno0 = _treni[0].IsAlive;//se treno 0 si muove
                treno1 = _treni[1].IsAlive; //se treno 1 si muove
                Scrivi(47, 2, treno0.ToString());
                Scrivi(83, 2, treno1.ToString());
                Thread.Sleep(20);
            } while (treno0 || treno1);
        }

        static void Main(string[] args)
        {
            OutputEncoding = Encoding.Unicode;
            ForegroundColor = ConsoleColor.Yellow;//giallo
            CursorVisible = false;

            Treno treno1 = new Treno(1), treno2 = new Treno(2);//crea due treni
            _treni[0] = new Thread(treno1.StampaTreno);
            _treni[1] = new Thread(treno2.StampaTreno);
            StampaFerrovia();

            _treni[0].Start();
            _treni[1].Start();

            Thread stato = new Thread(AggiornaStato);//lo stato dei treni
            stato.Start();

            Persona pietro = new Persona(0, "Pietro");//persona 1
            Thread persona = new Thread(pietro.Muovi);
            persona.Start();

            while (persona.IsAlive)//fino a quando si muove la persona aspetto prima di terminare
                Thread.Sleep(10);

            SetCursorPosition(0, 29);
            Write("Premere per terminare");//termino
            ReadKey();

        }
        
        
    }
}