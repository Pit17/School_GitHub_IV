using System.Drawing;

namespace Ereditarieta
{
    class Poligono
    {
        private double area=0.0, perimetro=0.0;
        
        public Poligono() { }
        
        public double Area{ get { return area; } set { } }
        public double Perimetro { get { return perimetro; } set { } }
        

        public virtual string Riassumiti()
        {
            return $"Area={Area}----Perimetro={Perimetro}";
        }
    }
    class Cerchio : Poligono
    {
        private double raggio;
        public Cerchio(double raggio)
            : base()
        {
            Area = Math.PI * (Math.Pow(raggio, 2));
            Perimetro = Math.PI * (2*raggio);
        }
        public double Raggio { get { return raggio; } }
        public override string Riassumiti()
        {
            return base.Riassumiti() + $"---Raggio={Raggio}";
        }
    }
    class Triangolo : Poligono
    {
        
    }
    class Quadrato : Poligono
    {
        private double lato;
        public Quadrato(double lato)
            : base()
        {
            Area = lato*lato;
            Perimetro=4*lato;
        }
        public double Lato { get { return lato; } }
        public override string Riassumiti()
        {
            return base.Riassumiti() + $"---lato={Lato}";
        }
    }
    class Rettangolo : Quadrato
    {
        public double lato2;
        public Rettangolo(double lato,double lato2)
            : base(lato)
        {
            Area =lato*lato2;
            Perimetro=(2*lato2) + (2*lato);
        }
        public override string Riassumiti()
        {
            return base.Riassumiti() + $"---lato={Lato}---lato2={lato2}";
        }
    }
    class Rombo : Quadrato
    {
        public double d1, d2;
        public Rombo(double d1,double d2,double lato)
            : base(lato)
        {
            Perimetro = 4 * lato;
            Area= (d1*d2)/ 2;
        }
        public override string Riassumiti()
        {
            return base.Riassumiti() + $"---lato={Lato}---d2={d2}---d1={d1}";
        }
    }
    class Trapezio : Poligono
    {
        public double b, b2, l, l2,h;
        public Trapezio(double b,double b2,double l,double l2,double h)
            : base()
        {
            Area = ((b + b2) * h) / (2);
            Perimetro = b + b2 + l + l2;
        }
        public override string Riassumiti()
        {
            return base.Riassumiti() + $"---lato={l}---lato2={l2}---base={b}---base2={b2}---altezza={h}";
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            
        }
    }
}
