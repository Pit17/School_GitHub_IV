namespace ConsoleAppBanca
{
    internal class Program
    {
        class Banca
        {
            private class Cliente
            {
                string nome;
                string cognome;
                string codice_fiscale;
                double stipendio;

                Cliente(string nome, string cognome, string codice_fiscale, double stipendio)
                {
                    this.nome = nome;
                    this.cognome = cognome;
                    this.codice_fiscale = codice_fiscale;
                    this.stipendio = stipendio;
                }

                private void StampaCliente()
                {
                    Console.WriteLine($"Nome : {nome} \n Cognome : {cognome} \n Codice-Fiscale : {codice_fiscale} \n Stipendio : {stipendio}");
                }
                private class PrestitoSemplice
                {
                    double capitale;
                    double interesse;
                    DateOnly data_i;
                    DateOnly data_f;
                    
                    
                    PrestitoSemplice(double capitale, double interesse, DateOnly data_i, DateOnly data_f)
                    {
                        this.capitale = capitale;
                        this.interesse = interesse;
                        this.data_i = data_i;
                        this.data_f = data_f;
                    }

                    private double rata()
                    {
                        return (Montante() / Durata()) / 12;
                    }
                    private double Montante()
                    {
                       return capitale + (capitale * (Durata() * interesse));
                    }
                    private double Durata()
                    {
                       return (data_f.DayNumber - data_i.DayNumber)/365;
                    }
                    
                    private void StampaPrestito()
                    {
                        Console.WriteLine($"Durata : {Durata()} \n Rata mensile : {rata}$");
                    }


                }

                class PrestitoComposto : PrestitoSemplice
                {
                    public override Montante()
                    {
                        
                    }
                }
            
            }


            private void AddCliente(Cliente cliente) { }

            private void RemoveCliente(string codice_fiscale) { }

            private Cliente SearchCliente(string codice_fiscale) { return Cliente}

            private void AddPrestito (PrestitoSemplice Prestito) { }

            private List<> SearchPrestiti(string codice_fiscale) { }

            private double TotalePrestiti(string codice_fiscale) { }
        
        
        }

    }
}
