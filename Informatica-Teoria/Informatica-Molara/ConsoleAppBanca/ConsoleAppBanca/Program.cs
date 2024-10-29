namespace ConsoleAppBanca
{
    internal class Program
    {
        class Banca
        {
            private List<Cliente> clienti = new List<Cliente>();

            public void AddCliente(Cliente cliente)
            {
                clienti.Add(cliente);
            }

            public void RemoveCliente(string codice_fiscale)
            {
                for (int i = 0; i < clienti.Count; i++)
                {
                    if (clienti[i].CodiceFiscale == codice_fiscale)
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
                    if (cliente.CodiceFiscale == codice_fiscale)
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
                    cliente.AddPrestito(prestito);
                }
            }

            public List<PrestitoSemplice> SearchPrestiti(string codice_fiscale)
            {
                Cliente cliente = SearchCliente(codice_fiscale);
                if (cliente != null)
                {
                    return cliente.GetPrestiti();
                }
                return new List<PrestitoSemplice>();
            }

            public double TotalePrestiti(string codice_fiscale)
            {
                Cliente cliente = SearchCliente(codice_fiscale);
                if (cliente != null)
                {
                    return cliente.TotalePrestiti();
                }
                return 0;
            }
        }

        class PrestitoSemplice
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

            protected double Rata()
            {
                return Math.Ceiling((Montante() / Durata()) / 12 * 100) / 100;
            }

            public virtual double Montante()
            {
                return Math.Ceiling((capitale + (capitale * (Durata() * interesse))) * 100) / 100;
            }

            protected double Durata()
            {
                return Math.Ceiling(((data_f.DayNumber - data_i.DayNumber) / 365.0) * 100) / 100;
            }

            public void StampaPrestito()
            {
                Console.WriteLine($"Durata: {Durata()} anni\nRata mensile: {Rata()} euro");
            }
        }

        class PrestitoComposto : PrestitoSemplice
        {
            public PrestitoComposto(double capitale, double interesse, DateOnly data_i, DateOnly data_f)
                : base(capitale, interesse, data_i, data_f) { }

            public override double Montante()
            {
                int anni = (int)Durata();
                return Math.Ceiling((capitale * Math.Pow(1 + interesse, anni)) * 100) / 100;
            }
        }

        class Cliente
        {
            private string nome;
            private string cognome;
            public string CodiceFiscale { get; private set; }
            private double stipendio;
            private List<PrestitoSemplice> prestiti = new List<PrestitoSemplice>();

            public Cliente(string nome, string cognome, string codice_fiscale, double stipendio)
            {
                this.nome = nome;
                this.cognome = cognome;
                this.CodiceFiscale = codice_fiscale;
                this.stipendio = stipendio;
            }

            public void AddPrestito(PrestitoSemplice prestito)
            {
                prestiti.Add(prestito);
            }

            public List<PrestitoSemplice> GetPrestiti()
            {
                return prestiti;
            }

            public double TotalePrestiti()
            {
                double totale = 0;
                foreach (PrestitoSemplice prestito in prestiti)
                {
                    totale += prestito.Montante();
                }
                return Math.Ceiling(totale * 100) / 100;
            }

            public void StampaCliente()
            {
                Console.WriteLine($"Nome : {nome}\nCognome : {cognome}\nCodice-Fiscale : {CodiceFiscale}\nStipendio : {stipendio} euro");
            }
        }

        static void Main(string[] args)
        {
            Banca banca = new Banca();

            Cliente cliente1 = new Cliente("Mario", "Rossi", "MRORSS80A01H501Z", 2500.0);
            Cliente cliente2 = new Cliente("Anna", "Bianchi", "BNCHAN95P41H501Q", 3000.0);
            Cliente cliente3 = new Cliente("Marco", "Balducci", "BLDMRC07B04H294J", 2500.0);

            banca.AddCliente(cliente1);
            banca.AddCliente(cliente2);
            banca.AddCliente(cliente3);

            Console.WriteLine("Clienti registrati in banca:");
            cliente1.StampaCliente();
            cliente2.StampaCliente();
            cliente3.StampaCliente();

            PrestitoSemplice prestito1 = new PrestitoSemplice(10000.0, 0.05, new DateOnly(2024, 1, 1), new DateOnly(2026, 1, 1));
            PrestitoSemplice prestito2 = new PrestitoComposto(15000.0, 0.04, new DateOnly(2023, 1, 1), new DateOnly(2026, 1, 1));
            PrestitoSemplice prestito3 = new PrestitoComposto(200000.0, 0.048, new DateOnly(2024, 10, 29), new DateOnly(2034, 10, 29));

            banca.AddPrestito("MRORSS80A01H501Z", prestito1);
            banca.AddPrestito("BNCHAN95P41H501Q", prestito2);
            banca.AddPrestito("BLDMRC07B04H294J", prestito3);

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

            Console.WriteLine("\nDettagli prestiti di Marco Balducci:");
            foreach (var prestito in banca.SearchPrestiti("BLDMRC07B04H294J"))
            {
                prestito.StampaPrestito();
            }


            Console.WriteLine($"\nTotale prestiti di Mario Rossi: {banca.TotalePrestiti("MRORSS80A01H501Z")} euro");
            Console.WriteLine($"Totale prestiti di Anna Bianchi: {banca.TotalePrestiti("BNCHAN95P41H501Q")} euro");
            Console.WriteLine($"Totale prestiti di Marco Balducci: {banca.TotalePrestiti("BLDMRC07B04H294J")} euro");

            Console.WriteLine("\nRimozione del cliente Mario Rossi dalla banca.");
            banca.RemoveCliente("MRORSS80A01H501Z");

            var clienteRimosso = banca.SearchCliente("MRORSS80A01H501Z");
            if (clienteRimosso == null) Console.WriteLine("Mario Rossi è stato rimosso correttamente.");
            else Console.WriteLine("Errore nella rimozione di Mario Rossi.");
        }
    }
}