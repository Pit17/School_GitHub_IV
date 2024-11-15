using System.Runtime.InteropServices;
//Pietro Malzone 15/11/2024 ConsoleApp Dijkstra e Grafi
namespace ConsoleAProvaGrafoDirettoppDijkstra
{
    public class GrafoDiretto
    {
        public struct Arco
        {
            public int nodo_partenza;
            public int nodo_arrivo;
            public int costo;
        }
        List<string> nodi;
        List<Arco> archi;
        public GrafoDiretto(int numero_max_nodi = 1000)//costruttore
        {
            this.archi = new List<Arco>();//lista di archi
            this.nodi = new List<string>();//lista di nodi
                                                                  
        }
    public int NumeroNodi; // proprietà che torna il numero attuale di nodi inseriti
        public void AggiungiNodo(string nome) { nodi.Add(nome); NumeroNodi++; } // aggiunge un nodo

        
        public virtual void AggiungiArco(string nome_nodo_partenza, string nome_nodo_arrivo, int costo)
        {  
            Arco arco = new Arco();//aggiungo l'arco con tutti i dettagli
            arco.costo = costo;
            arco.nodo_partenza = this[nome_nodo_partenza];  
            arco.nodo_arrivo = this[nome_nodo_arrivo];
            archi.Add(arco);

        }// aggiunge un arco fra i due nodi dati, con il costo indicato
        public virtual void AggiungiArco(Arco arco) { archi.Add(arco); } // aggiunge un arco fra i due nodi dati, con il costo indicato
        public string this[int nodo] { get { return nodi[nodo]; } }  // indicizzatore per i nodi
        public int this[string nome_nodo] { 
            get{
                for (int i = 0; i < nodi.Count; i++)//indicizzatore
                {
                    if (nome_nodo == nodi[i]) return i;
                     
                }
                return -1;
            }
        }
        public bool ControllaIndiceNodo(int nodo)//verifico l'esistenza di un nodo
        {
            for (int i = 0; i < nodi.Count; i++)
            {
                if (i == nodo) return true;

            }
            return false;
        }
        public IEnumerable<Arco> ArchiUscenti(int nodo) // enumeratore per gli archi uscenti dal nodo dato
        { 
            foreach(var arco in archi)
            {
                if(arco.nodo_partenza == nodo)
                    yield return arco;
            }
        
        } 
        public IEnumerable<Arco> ArchiEntranti(int nodo)// enumeratore per gli archi entranti nell nodo dato 
        {
            foreach (var arco in archi)
            {
                if (arco.nodo_arrivo == nodo)
                    yield return arco;
            }

        } 
    }



public class GrafoNonDiretto : GrafoDiretto  // specializzazione di GrafoDiretto per grafi "non diretti" (se si può andare da A a B con costo 10, allora si va acnhe da B ad A con lo stesso costo)
{
    public GrafoNonDiretto(int numero_max_nodi = 1000) {  }
        public override void AggiungiArco(string nome_nodo_partenza, string nome_nodo_arrivo, int costo)
        {
            Arco arco = new Arco();
            arco.costo = costo;
            arco.nodo_partenza = this[nome_nodo_partenza];
            arco.nodo_arrivo = this[nome_nodo_arrivo];
            base.AggiungiArco(arco);
            Arco arco2 = new Arco();//creo l'arco inverso essendo un grafo non diretto
            arco2.costo = costo;
            arco2.nodo_partenza = this[nome_nodo_arrivo];
            arco2.nodo_arrivo = this[nome_nodo_partenza];
            base.AggiungiArco(arco2);

        } // aggiunge un arco fra i due nodi dati, con il costo indicato
        public override void AggiungiArco(Arco arco) 
        {
            base.AggiungiArco(arco);
            Arco arco2 = new Arco();//creo l'arco inverso essendo un grafo non diretto
            arco2.nodo_arrivo = arco.nodo_partenza;
            arco2.nodo_partenza = arco.nodo_arrivo;
            arco2.costo = arco.costo;
            base.AggiungiArco (arco2);



        } // aggiunge un arco fra i due nodi dati, con il costo indicato
    }
}
