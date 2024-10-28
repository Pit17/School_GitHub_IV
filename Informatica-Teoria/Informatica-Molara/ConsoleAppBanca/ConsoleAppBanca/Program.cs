using System;
using System.Collections.Generic;

namespace ConsoleAppBanca
{
    internal class Program
    {
        class Banca
        {
            private List<Cliente> clienti = new List<Cliente>();

            public class Cliente
            {
                public string nome;
                public string cognome;
                public string codice_fiscale;
                public double stipendio;
                public List<PrestitoSemplice> prestiti = new List<PrestitoSemplice>();

                public Cliente(string nome, string cognome, string codice_fiscale, double stipendio)
                {
                    this.nome = nome;
                    this.cognome = cognome;
                    this.codice_fiscale = codice_fiscale;
                    this.stipendio = stipendio;
                }

                public void StampaCliente()
                {
                    Console.WriteLine($"Nome : {nome}\nCognome : {cognome}\nCodice-Fiscale : {codice_fiscale}\nStipendio : {stipendio} euro");
                }
            }

            public class PrestitoSemplice
            {
                protected double capitale;
                protected double interesse;
                protected DateOnly data_i;
                protected DateOnly data_f;

                public PrestitoSemplice(double capitale, double interesse, DateOnly data_i, DateOnly data_f)
                {
                    this.capitale = capitale;
                    this.interesse = interesse;
                    this.data_i = data_i;
                    this.data_f = data_f;
                }

                public double Rata()
                {
                    return Math.Ceiling((Montante() / Durata()) / 12 * 100) / 100; 
                }

                public virtual double Montante()
                {
                    return Math.Ceiling((capitale + (capitale * (Durata() * interesse))) * 100) / 100; 
                }

                public double Durata()
                {
                    return Math.Ceiling(((data_f.DayNumber - data_i.DayNumber) / 365.0) * 100) / 100; 
                }

                public void StampaPrestito()
                {
                    Console.WriteLine($"Durata: {Durata()} anni\nRata mensile: {Rata()} euro");
                }
            }

            public class PrestitoComposto : PrestitoSemplice
            {
                public PrestitoComposto(double capitale, double interesse, DateOnly data_i, DateOnly data_f)
                    : base(capitale, interesse, data_i, data_f) { }

                public override double Montante()
                {
                    int anni = (int)Durata();
                    return Math.Ceiling((capitale * Math.Pow(1 + interesse, anni)) * 100) / 100; 
                }
            }

            public void AddCliente(Cliente cliente)
            {
                clienti.Add(cliente);
            }

            public void RemoveCliente(string codice_fiscale)
            {
                for (int i = 0; i < clienti.Count; i++)
                {
                    if (clienti[i].codice_fiscale == codice_fiscale)
                    {
                        clienti.RemoveAt(i);
                        break;
                    }
                }
            }

            public Cliente SearchCliente(string codice_fiscale)
            {
                foreach (Cliente cliente in clienti)
                {
                    if (cliente.codice_fiscale == codice_fiscale)
                    {
                        return cliente;
                    }
                }
                return null;
            }

            public void AddPrestito(string codice_fiscale, PrestitoSemplice prestito)
            {
                Cliente cliente = SearchCliente(codice_fiscale);
                if (cliente != null)
                {
                    cliente.prestiti.Add(prestito);
                }
            }

            public List<PrestitoSemplice> SearchPrestiti(string codice_fiscale)
            {
                Cliente cliente = SearchCliente(codice_fiscale);
                if (cliente != null)
                {
                    return cliente.prestiti;
                }
                return new List<PrestitoSemplice>();
            }

            public double TotalePrestiti(string codice_fiscale)
            {
                Cliente cliente = SearchCliente(codice_fiscale);
                double totale = 0;
                if (cliente != null)
                {
                    foreach (PrestitoSemplice prestito in cliente.prestiti)
                    {
                        totale += prestito.Montante();
                    }
                }
                return Math.Ceiling(totale * 100) / 100; 
            }
        }

        static void Main(string[] args)
        {
            Banca banca = new Banca();

            Banca.Cliente cliente1 = new Banca.Cliente("Mario", "Rossi", "MRORSS80A01H501Z", 2500.0);
            Banca.Cliente cliente2 = new Banca.Cliente("Anna", "Bianchi", "BNCHAN95P41H501Q", 3000.0);

            
            banca.AddCliente(cliente1);
            banca.AddCliente(cliente2);

            
            Console.WriteLine("Clienti registrati in banca:");
            cliente1.StampaCliente();
            cliente2.StampaCliente();

            
            Banca.PrestitoSemplice prestito1 = new Banca.PrestitoSemplice(10000.0, 0.05, new DateOnly(2024, 1, 1), new DateOnly(2026, 1, 1));
            Banca.PrestitoSemplice prestito2 = new Banca.PrestitoComposto(15000.0, 0.04, new DateOnly(2023, 1, 1), new DateOnly(2026, 1, 1));

            
            banca.AddPrestito("MRORSS80A01H501Z", prestito1);
            banca.AddPrestito("BNCHAN95P41H501Q", prestito2);

            
            Console.WriteLine("\nDettagli prestiti di Mario Rossi:");
            foreach (var prestito in banca.SearchPrestiti("MRORSS80A01H501Z"))
            {
                prestito.StampaPrestito();
            }

            Console.WriteLine("\nDettagli prestiti di Anna Bianchi:");
            foreach (var prestito in banca.SearchPrestiti("BNCHAN95P41H501Q"))
            {
                prestito.StampaPrestito();
            }

            
            Console.WriteLine($"\nTotale prestiti di Mario Rossi: {banca.TotalePrestiti("MRORSS80A01H501Z")} euro");
            Console.WriteLine($"Totale prestiti di Anna Bianchi: {banca.TotalePrestiti("BNCHAN95P41H501Q")} euro");

            
            Console.WriteLine("\nRimozione del cliente Mario Rossi dalla banca.");
            banca.RemoveCliente("MRORSS80A01H501Z");

            
            var clienteRimosso = banca.SearchCliente("MRORSS80A01H501Z");
            Console.WriteLine(clienteRimosso == null ? "Mario Rossi è stato rimosso correttamente." : "Errore nella rimozione di Mario Rossi.");
        }
    }
}
