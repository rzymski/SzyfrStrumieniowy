using System;

namespace SzyfrStrumieniowy
{
    class LfsrGenerator
    {
        private readonly int dlugoscWielomianu;
        private readonly int wielomian;
        private string aktualneWartosci;

        public LfsrGenerator(string startoweWartosci, string wielomian)
        {
            dlugoscWielomianu = startoweWartosci.Length;
            this.wielomian = int.Parse(wielomian);
            this.aktualneWartosci = startoweWartosci;
        }

        public char GetNextBit()
        {
            //zapisanie wyniku
            int wynikXor = 0;
            string wNapis = wielomian.ToString();
            for (int i = 0; i < wNapis.Length; i++)
                if (wNapis[i] == '1')
                    wynikXor ^= (aktualneWartosci[i] - '0');
            //przesuniecie wartosci w prawo
            int akInt = Convert.ToInt32(aktualneWartosci, 2);
            akInt >>= 1;
            aktualneWartosci = Convert.ToString(akInt, toBase: 2).PadLeft(dlugoscWielomianu, '0');
            if (wynikXor == 1)
                aktualneWartosci = "1" + aktualneWartosci.Substring(1);
            return Convert.ToChar(wynikXor + '0');
        }
    }

    public class Program
    {
        public static string SzyfrStrumieniowy(string ciagBitow, string seed, string wielomian)
        {
            Console.WriteLine($"\nciagBitow: {ciagBitow}  wielomina: {wielomian}  ziarno: {seed}");
            string klucz = "";
            LfsrGenerator generatorKlucza = new LfsrGenerator(seed, wielomian);
            for (int i = 0; i < ciagBitow.Length; i++)
                klucz += generatorKlucza.GetNextBit();
            Console.WriteLine("Klucz = "+klucz);
            string result = "";
            for (int i = 0; i < ciagBitow.Length; i++)
                result += ciagBitow[i] ^ klucz[i];
            Console.WriteLine("Wynik = "+result);
            return result;
        }


        static void Main()
        {
            string w1 = SzyfrStrumieniowy("111010011100", "0010", "1001");
            string w2 = SzyfrStrumieniowy("0011001100", "0010", "1001");
            string w3 = SzyfrStrumieniowy("1110100111001100110011", "11111", "11011");

        }
    }
}


/*using System;

namespace SzyfrStrumieniowy
{
    class LfsrGenerator
    {
        private readonly int dlugoscWielomianu;
        private readonly int wielomian;
        private string aktualneWartosci;

        public LfsrGenerator(string startoweWartosci, string wielomian)
        {
            dlugoscWielomianu = startoweWartosci.Length;
            this.wielomian = int.Parse(wielomian);
            this.aktualneWartosci = startoweWartosci;
        }

        public char GetNextBit()
        {
            //zapisanie wyniku
            int wynikXor = 0;
            string wNapis = wielomian.ToString();
            for (int i = 0; i < wNapis.Length; i++)
                if (wNapis[i] == '1')
                    wynikXor ^= (aktualneWartosci[i] - '0');
            //przesuniecie wartosci w prawo
            int akInt = Convert.ToInt32(aktualneWartosci, 2);
            akInt >>= 1;
            aktualneWartosci = Convert.ToString(akInt, toBase: 2).PadLeft(dlugoscWielomianu, '0');
            if (wynikXor == 1)
                aktualneWartosci = "1" + aktualneWartosci.Substring(1);
            return Convert.ToChar(wynikXor+'0');
        }
    }

    public class Program
    {
        public static string SzyfrStrumieniowy(string klucz, string ciagBitow)
        {
            string result = "";
            for (int i = 0; i < ciagBitow.Length; i++)
                result += ciagBitow[i] ^ klucz[i];
            return result;
        }

        public static string SzyfrStrumieniowy2(string klucz, string ciagBitow)
        {
            string result = "";
            for (int i = 0; i < ciagBitow.Length; i++)
            {
                int keyBit = int.Parse(klucz[i].ToString());
                int messageBit = int.Parse(ciagBitow[i].ToString());
                int xorResult = keyBit ^ messageBit;
                result += xorResult.ToString();
            }
            return result;
        }


        static void Main()
        {

            string seed = "0010";
            string wielomian = "1001";
            string ciagBitow = "0011001100";
            string klucz = "";
            LfsrGenerator generatorKlucza = new LfsrGenerator(seed, wielomian);
            Console.WriteLine($"ciagBitow: {ciagBitow}  wielomina: {wielomian}  ziarno: {seed}");
            Console.Write($"KLUCZ = ");
            for (int i = 0; i < ciagBitow.Length; i++)
            {
                klucz += generatorKlucza.GetNextBit();
                Console.Write($"{klucz[i]}");
            }

            Console.Write("\nWynik = ");
            Console.Write(SzyfrStrumieniowy(klucz, ciagBitow));
            Console.Write("\nWynik = ");
            Console.WriteLine(SzyfrStrumieniowy2(klucz, ciagBitow));
        }
    }
}
*/