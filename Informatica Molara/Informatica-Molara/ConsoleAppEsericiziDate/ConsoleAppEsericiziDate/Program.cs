namespace ConsoleAppEsericiziDate
{
    internal class Program
    {
        static void Main(string[] args)
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

    }
}
