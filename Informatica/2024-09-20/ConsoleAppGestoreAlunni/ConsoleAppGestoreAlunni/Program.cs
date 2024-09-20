//Pietro Malzone 4H Console App per gestire dati degli studenti 20/09/2024
using System.ComponentModel;

namespace ConsoleAppGestoreAlunni
{
    class Alunno//classe che gestisce i dati di ogni studente
    {
        public string Nome;
        public string Cognome;
        public char Sesso;
        public DateOnly data_nascita;
        public string Classe;
        public string indirizzo_scolastico;

        public Alunno(string nome, string cognome, char sesso, DateOnly data, string classe, string indirizzo)//costruttore
        {
            Nome = nome;
            Cognome = cognome;
            Sesso = sesso;
            data_nascita = data;
            Classe = classe;
            indirizzo_scolastico = indirizzo;
        }

        public override string ToString()//per stampare i dati
        {
            return $"{Nome}\t{Cognome}\t{Sesso}\t{data_nascita}\t{Classe}\t{indirizzo_scolastico}";
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
            foreach (Alunno alunno in gestore)//ristampo come da file tutti i dati
            {
                Console.WriteLine(alunno.ToString());
            }


        }

        static void Main()//richiamo la funzione
        {
            Console.WriteLine("Pietro Malzone 4H Console App per gestire dati degli studenti 20/09/2024");
            Console.WriteLine();
            CreazioneGestore(@"../../../elenco-alunni-classi.txt");
            
        }
    }

}
