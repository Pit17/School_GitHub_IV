using System.Net.Http.Headers;
//Pietro MAlzone 4H 09/10/2024
namespace BitArrays
{
    class BitArray
    {
        private const int BITS_PER_VALUE = 32;  // ogni (unit) ha 32 bit
        private ulong[] bits;
        private long n_bits;

        public BitArray(long n_bits, bool initial_value)
        {
            long n_uints = n_bits / BITS_PER_VALUE;
            if (n_bits % BITS_PER_VALUE != 0)
                ++n_uints;  // serve un (uint) in più per i bits residui

            if (n_uints > int.MaxValue - 60)  // il valore massimo è stato trovato per tentativi
                throw new OverflowException("Too many bits");

            this.bits = new ulong[n_uints];
            this.n_bits = n_bits;
            SetAllBits(initial_value);
        }

        public void SetAllBits(bool value)  // imposta tutti i bit a true o false
        {
            uint _value = 0;
            if (value)
                _value = ~_value;

            for (int i = 0; i < bits.Length; ++i)
                bits[i] = _value;
        }
        public bool GetBit(long bit_index)
        {
            long indice = bit_index / BITS_PER_VALUE;
            long posizione = bit_index % BITS_PER_VALUE;
            ulong bitmask = (ulong)(1 << (int)posizione);
            
            return (bits[indice] & bitmask) != 0;

        }
        public void SetBit(long bit_index, bool value) 
        {
            long indice = bit_index / BITS_PER_VALUE;
            long posizione = bit_index % BITS_PER_VALUE;
            ulong bitmask = (ulong)(1 << (int)posizione);

            if (value)
            {
                bits[indice] |= bitmask;
            }
            else 
            {
                bitmask = ~bitmask;
                bits[indice] &= bitmask;
            }

        }

        public bool this[long bit_index]
        {
            get { return GetBit(bit_index); }
            set { SetBit(bit_index, value); }
        }
    
    }
    internal class Program
    {
        static List<long> EratosthenesSieve(long max_value)
        {
            List<long> primes = new List<long>();

            BitArray sieve = new BitArray(max_value + 1, true);
            for (long n = 2; n <= max_value; ++n)
            {
                if (sieve[n])  // se true, allora n è primo
                {
                    primes.Add(n);
                    for (long mult_n = (n << 1); mult_n <= max_value; mult_n += n)  // genera tutti i multipli di n, che vanno marcati come non-primi
                    {
                        sieve[mult_n] = false;
                    }
                }
            }

            return primes;
        }

        static void Main(string[] args)
        {
            List<long> primes = EratosthenesSieve(120_000_000);

            foreach (long n in primes)
                Console.Write($"{n}, ");
            Console.WriteLine();
        }
    }
}