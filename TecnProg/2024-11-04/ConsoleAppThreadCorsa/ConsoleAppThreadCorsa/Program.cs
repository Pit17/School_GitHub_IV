using System.Data;
using static System.Console;
//Pietro Malzone 4H 04/11/2024 ConsoleAppCorsa
namespace ConsoleAppThreadCorsa
{
    class Program
    {
        
        static int posAndrea = 0;//posizione di partenza dei tre corridori
        static int posBaldo = 0;
        static int posCarlo = 0;
        static int classifica = 0;
        
        static Object _lockConsole = new Object();//il lock funge da testimone su chi può usufruire della console e della classifica
        static Object _lockClassifica = new Object();

        static void Pronti()
        {
            ForegroundColor = ConsoleColor.Green;
            SetCursorPosition(posAndrea, 2);//Andrea
            Write("Andrea");
            SetCursorPosition(posAndrea, 3);
            Write("  []");
            SetCursorPosition(posAndrea, 4);
            Write(@" ┌░░┘");
            SetCursorPosition(posAndrea, 5);
            Write(@"  /\");
            SetCursorPosition(posAndrea, 6);
            ForegroundColor = ConsoleColor.DarkRed;                                                                                              //
            Write("▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄¦▄▄▄¦");

            ForegroundColor = ConsoleColor.Yellow;
            SetCursorPosition(posBaldo, 7);//Baldo
            Write("Baldo");
            SetCursorPosition(posBaldo, 8);
            Write("  ()");
            SetCursorPosition(posBaldo, 9);
            Write(@" ┌▓▓┘");
            SetCursorPosition(posBaldo, 10);
            Write(@"  /\");
            SetCursorPosition(posAndrea, 11);
            ForegroundColor = ConsoleColor.DarkRed;
            Write("▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄¦▄▄▄¦");

            ForegroundColor = ConsoleColor.Cyan;
            SetCursorPosition(posCarlo, 12);//Carlo
            Write("Carlo");
            SetCursorPosition(posCarlo, 13);
            Write("  {}");
            SetCursorPosition(posCarlo, 14);
            Write(@" ┌▒▒┘");
            SetCursorPosition(posCarlo, 15);
            Write(@"  /\");
            SetCursorPosition(posAndrea, 16);
            ForegroundColor = ConsoleColor.DarkRed;
            Write("▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄¦▄▄▄¦");
        }

        static void Andrea()
        {
            do//aggiorno la posizione di Andrea
            {
                posAndrea++;
                Thread.Sleep(30);
                lock (_lockConsole)
                {
                    ForegroundColor = ConsoleColor.Green;
                    SetCursorPosition(posAndrea, 5);
                    Write(@"    /\");
                }
                Thread.Sleep(30);
                lock (_lockConsole)
                {
                    ForegroundColor = ConsoleColor.Green;
                    SetCursorPosition(posAndrea, 4);
                    Write(@" ¯¯¯░░╝");
                }
                Thread.Sleep(30);
                lock (_lockConsole)
                {
                    ForegroundColor = ConsoleColor.Green;
                    SetCursorPosition(posAndrea, 3);
                    Write("    []");
                }


            } while (posAndrea < 110);
            
            lock (_lockConsole)//gestisce la console
            {
                lock (_lockClassifica)//gestisce solo la classifica
                {
                    classifica++;

                }
                ForegroundColor = ConsoleColor.Green;
                SetCursorPosition(115, 2);
                Write(classifica);
            }
        }
        static void Baldo()
        {
            do//aggiorno la posizione di Baldo
            {
                posBaldo++;
                Thread.Sleep(50);
                lock (_lockConsole)
                {
                    ForegroundColor = ConsoleColor.Yellow;
                    SetCursorPosition(posBaldo, 10);
                    Write(@"  /\");
                }
                Thread.Sleep(50);
                lock (_lockConsole)
                {
                    ForegroundColor = ConsoleColor.Yellow;
                    SetCursorPosition(posBaldo, 9);
                    Write(@" ┌▓▓┘");
                }
                
                Thread.Sleep(50);
                lock (_lockConsole)
                {
                    ForegroundColor = ConsoleColor.Yellow;
                    SetCursorPosition(posBaldo, 8);
                    Write("  ()");
                }
                

            } while (posBaldo < 112);
            
            lock (_lockConsole)//gestisce solo la console
            {
                lock (_lockClassifica)//gestisce solo la classifica
                {
                    classifica++;

                }
                ForegroundColor = ConsoleColor.Yellow;
                SetCursorPosition(115, 7);
                Write(classifica);
            }
        }
        static void Carlo()
        {
            do//aggiorno la posizione di Carlo
            {
                posCarlo++;
                Thread.Sleep(50);
                lock (_lockConsole)
                {
                    ForegroundColor = ConsoleColor.Cyan;
                    SetCursorPosition(posCarlo, 15);
                    Write(@"  /\");
                }
                
                Thread.Sleep(50);
                lock (_lockConsole)
                {
                    ForegroundColor = ConsoleColor.Cyan;
                    SetCursorPosition(posCarlo, 14);
                    Write(@" ┌▒▒┘");
                }
                Thread.Sleep(50);
                lock (_lockConsole)
                {
                    ForegroundColor = ConsoleColor.Cyan;
                    SetCursorPosition(posCarlo, 13);
                    Write("  {}");
                }

            } while (posCarlo < 112);
            
            lock (_lockConsole)//gestisce solo la console
            {
                lock (_lockClassifica)//gestisce solo la classifica
                {
                    classifica++;
                }
                ForegroundColor = ConsoleColor.Cyan;
                SetCursorPosition(115, 12);
                Write(classifica);
            }
        }
        
        static void Main(string[] args)
        {
            CursorVisible = false;//cursore invisibile
            //via alla gara
            Pronti();
            Thread thAndrea = new Thread(Andrea);
            Thread thBaldo = new Thread(Baldo);
            Thread thCarlo = new Thread(Carlo);
            
            thAndrea.Start();
            thBaldo.Start();
            thCarlo.Start();
            ReadLine();
        }
    }
    
        
    
    
}
