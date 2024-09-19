namespace ConsoleAppEsericiziDate
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("+-------------------------------------------------------------+");
                Console.WriteLine("|                                                             |");
                Console.WriteLine("|  Benvenuto!                                                 |");
                Console.WriteLine("|  Inserisci il numero dell'esercizio che vuoi eseguire       |");
                Console.WriteLine("|  1) Calcola la differenza tra due date                      |");
                Console.WriteLine("|  2) Verifica se un'ora è compresa tra due orari             |");
                Console.WriteLine("|  3) Trova la coppia di date con la minima differenza        |");
                Console.WriteLine("|  4) Esci                                                    |");
                Console.WriteLine("|                                                             |");
                Console.WriteLine("+-------------------------------------------------------------+");
                int scelta = int.Parse(Console.ReadLine());
                switch (scelta)
                {
                    case 1:
                        Ex1(args);
                        break;
                    case 2:
                        Ex2(args);
                        break;
                    case 3:
                        Ex3(args);
                        break;
                    case 4:
                        return;
                    default:
                        Console.WriteLine("Scelta non valida");
                        break;
                }
            }
            
        }
        static void Ex1(string[] args)
        {
            DateOnly data1;
            DateOnly data2;
            while (true)
            {
                Console.WriteLine("Inserisca la prima data");
                if(DateOnly.TryParse(Console.ReadLine(), out data1))break;
            }
            while (true)
            {
                Console.WriteLine("Inserisca la seconda data");
                if (DateOnly.TryParse(Console.ReadLine(), out data2)) break;
            }
           
            
            int days = Math.Abs(data1.DayNumber - data2.DayNumber);
            Console.WriteLine($"Ci sono {days} giorni di differenza");
        }

        static void Ex2(string[] args)
        {
            TimeOnly ora;
            while (true)
            {
                Console.WriteLine("Inserisca l'ora(HH:MM)");
                if (TimeOnly.TryParse(Console.ReadLine(), out ora))
                {
                    break;
                }
                else Console.WriteLine("Riprova!");
            }

            TimeOnly Start = new TimeOnly(16,00);
            TimeOnly End = new TimeOnly(22, 00);

            if (Start <= ora && ora <= End) Console.WriteLine("INTERNO");
            else Console.WriteLine("ESTERNO");
        }
        static void Ex3(string[] args){
            DateTime[] dates = {
            new DateTime(2014, 6, 14, 6, 32, 0),
            new DateTime(2014, 7, 10, 23, 49, 0),
            new DateTime(2015, 1, 10, 1, 16, 0),
            new DateTime(2014, 12, 20, 21, 45, 0),
            new DateTime(2014, 6, 2, 15, 14, 0)
        };

        
        DateTime min1 = dates[0];
        DateTime min2 = dates[1];
        double differenzaMin = Math.Abs((dates[0] - dates[1]).TotalSeconds);

        
        for (int i = 0; i < dates.Length; i++)
        {
            for (int j = i + 1; j < dates.Length; j++)
            {
                
                double differenza = Math.Abs((dates[i] - dates[j]).TotalSeconds);

                
                if (differenza < differenzaMin)
                {
                    differenzaMin = differenza;
                    min1 = dates[i];
                    min2 = dates[j];
                }
            }
        }

        
        Console.WriteLine("La coppia di date con la minima distanza è:");
        Console.WriteLine($"{min1} e {min2}");
        Console.WriteLine($"Differenza: {TimeSpan.FromSeconds(differenzaMin)} giorni");
        }
    }
    
}
