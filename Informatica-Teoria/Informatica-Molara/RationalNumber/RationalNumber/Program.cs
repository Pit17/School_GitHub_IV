using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.IO.IsolatedStorage;

namespace RationalNumber
{
    class RationalNumber
    {
        public static int Euclid(int a, int b)
        {
            Debug.Assert(a > 0 && b > 0);
            while (b > 0)
            {
                int r = a % b;
                a = b;
                b = r;
            }
            return a;
        }

        private int num, den;  // dovrà sempre essere (den > 0)

        public RationalNumber(int num, int den)
        {
            if (num == 0)
                throw new Exception("Invalid denominator value");

            this.num = num;
            this.den = den;

            Normalize();
        }
        public RationalNumber(int number) : this(number, 1) { }
        public RationalNumber() : this(0, 1) { }
        private void Normalize()
        {
            if (den < 0)
            {
                den = -den;
                num = -num;
            }

            int MCD = Euclid(Math.Abs(num), den);
            Debug.Assert(num % MCD == 0 && den % MCD == 0);
            num /= MCD;
            den /= MCD;
        }

        public static RationalNumber operator +(RationalNumber a, RationalNumber b)
        {
            return new RationalNumber(a.num * b.den + b.num * a.den, a.den * b.den);
        }
        public static RationalNumber operator -(RationalNumber a, RationalNumber b)
        {
            return new RationalNumber(a.num * b.den - b.num * a.den, a.den * b.den);
        }
        public static RationalNumber operator *(RationalNumber a, RationalNumber b)
        {
            return new RationalNumber(a.num * b.num, a.den * b.den);


        }
        public static RationalNumber operator /(RationalNumber a, RationalNumber b)
        {
            return new RationalNumber(a.num * b.den, a.den * b.num);
        }
        public static bool operator <(RationalNumber a, RationalNumber b)
        {
            return (a.num * b.den) < (b.num * a.den);
        }
        public static bool operator <=(RationalNumber a, RationalNumber b)
        {
            return (a.num * b.den) <= (b.num * a.den);
        }
        
        public static bool operator >(RationalNumber a, RationalNumber b)
        {
            return (a.num * b.den) > (b.num * a.den);
        }

        
        public static bool operator >=(RationalNumber a, RationalNumber b)
        {
            return (a.num * b.den) >= (b.num * a.den);
        }
        public static bool operator ==(RationalNumber a, RationalNumber b)
        {
            return (a.num * b.den) == (b.num * a.den);
        }
        public static bool operator !=(RationalNumber a, RationalNumber b)
        {
            return !(a == b);
        }


        public static RationalNumber operator +(RationalNumber a)
        {
            return a;
        }

        
        public static RationalNumber operator -(RationalNumber a)
        {
            return new RationalNumber(-a.num, a.den);
        }


        public override string ToString()
        {
            if (den == 1)
                return $"{num}";
            if (num < 0)
                return $"({num}/{den})";
            return $"{num}/{den}";
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            RationalNumber rA = new RationalNumber(3, 10);
            RationalNumber rB = new RationalNumber(7, -15);

            RationalNumber rSum = rA + rB;
            Console.WriteLine($"{rA} + {rB} = {rSum}");
            RationalNumber rSott = rA - rB;
            Console.WriteLine($"{rA} - {rB} = {rSott}");
            RationalNumber rMol = rA * rB;
            Console.WriteLine($"{rA} * {rB} = {rMol}");
            RationalNumber rDiv = rA / rB;
            Console.WriteLine($"{rA} / {rB} = {rDiv}");
            RationalNumber rPlus = +rA;
            Console.WriteLine($"{rA} = {rPlus}");
            RationalNumber rMin = -rA;
            Console.WriteLine($"{rA} = {rMin}");
        }
    }
}