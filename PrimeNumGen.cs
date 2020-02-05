using System;
using System.Numerics;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace PrimeGen
{
    using Extensions;
    
    /*
     * The PrimeNumGen class is used to create a number of prime numbers in parallel based on the bit number input and
     * how many prime numbers are needed, with one prime number being the default.  This class utilizes the
     * RNGCryptoServiceProvider to use a cryptographically secure sequence of bits to create the prime numbers.
     */
    public class PrimeNumGen
    {
        private static object myLock = new Object();
        private RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
        private int bits;
        private int count;
        private int counter;

        /*
         * Constructor of PrimeNumGen object
         */
        public PrimeNumGen(int bits, int count)
        {
            this.bits = bits;
            this.count = count;
        }

        /*
         * the makeAPrimeNumber method uses the bit count given to it by the PrimeNumGen object to create a potentially
         * prime number 
         */
        public BigInteger makeAPotentialPrimeNumnber(int numOfBits)
        {
            byte[] randNum = new byte[numOfBits/8];
            rng.GetBytes(randNum);
            BigInteger primeNum = new BigInteger(randNum);
            return primeNum;
        }
        
        /*
         * this run method utilizes the C# parallel library to create a number of prime numbers in parallel.
         */
        public void run()
        {
            Parallel.For(0, Int32.MaxValue, (i, state) =>
            {
                if (counter == count)
                {
                    state.Break();
                }

                BigInteger potentialPrimeNum = makeAPotentialPrimeNumnber(bits);
                
                lock (myLock)
                {
                    if (potentialPrimeNum.IsProbablyPrime())
                    {
                        BigInteger zero = BigInteger.Zero;
                            BigInteger two = new BigInteger(2);
                            int comparison = potentialPrimeNum.CompareTo(zero);
                            if (counter != count & comparison >= 0 & !BigInteger.Remainder(potentialPrimeNum, two).IsZero)
                            {
                                counter++;
                                Console.WriteLine("{0}: {1}", counter, potentialPrimeNum);
                                Console.WriteLine();   
                            }
                    } 
                }
            });
        }
    }
    
}