//Pietro Malzone 4H Console App per gestire dati degli studenti 24/09/2024
using System.ComponentModel;

namespace ConsoleAppGestoreAlunni
{
    class Alunno//classe che gestisce i dati di ogni studente
    {
        public string Nome { get; private set; }
        public string Cognome { get; private set; }
        public char Sesso { get; private set; }
        public DateOnly data_nascita { get; private set; }
        public string Classe { get; private set; }
        public int Anno { get; private set; }
        public char Sezione { get; private set; }
        public string indirizzo_scolastico { get; private set; }

        private double eta;

        public Alunno(string nome, string cognome, char sesso, DateOnly data, string classe, string indirizzo)//costruttore
        {
            Nome = nome;
            Cognome = cognome;
            Sesso = sesso;
            data_nascita = data;
            Classe = classe;
            Anno = int.Parse(Classe[0].ToString());
            Sezione= Classe[1];
            indirizzo_scolastico = indirizzo;
        }
        public double Calcolatore() 
        { 
            DateOnly oggi = DateOnly.FromDateTime(DateTime.Now);
            return (oggi.DayNumber - this.data_nascita.DayNumber) / 365.25;
        
        }



        public override string ToString()//per stampare i dati
        {
            return $"{Nome}\t{Cognome}\t{Sesso}\t{data_nascita}\tANNO:{Anno}\tSEZIONE:{Sezione}\t{indirizzo_scolastico}";
        }

    }

    internal class Program
    {

        static void CreazioneGestore(string nome_file)//crea il gestore per inserire i dati in una lista di alunni
        {
            List<Alunno> gestore = new();//lista di alunni
            using (StreamReader sr = new StreamReader(nome_file))
            {
                while (!sr.EndOfStream) // la proprietà .EndOfStream è bool e diventa
                                        // true quando il file finisce
                {
                    string[] linea = sr.ReadLine().Split('\t'); // legge la prossima riga del file rimuovendo i tab
                    if (linea.Length != 6)
                    {

                        Console.WriteLine("Inserimento alunno fallito!");// in caso non siano presenti tutti i dati di un alunno

                    }
                    else
                    {
                        gestore.Add(new Alunno(linea[0], linea[1], linea[2][0], DateOnly.Parse(linea[3]), linea[4], linea[5]));//altrimenti creo l'alunno
                    }

                }
                sr.Close();
            }



            using (StreamWriter sw = new StreamWriter(@"../../../elenco-alunni-classi-elaborato.txt"))
            {
                foreach (Alunno alunno in gestore)//ristampo come da file tutti i dati
                {
                    sw.WriteLine(alunno.ToString());
                    
                }
                sw.Close();
                Console.WriteLine("Elenco riscritto con successo e chiamato 'elenco-alunni-classi-elaborato.txt'");
            }

        }
        
        static void Main()//richiamo la funzione
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Pietro Malzone 4H Console App per gestire dati degli studenti 20/09/2024");
            Console.WriteLine();
            Console.ForegroundColor= ConsoleColor.Green;
            CreazioneGestore(@"../../../elenco-alunni-classi.txt");
            Console.ForegroundColor=ConsoleColor.White;
            
        }
    }

}
