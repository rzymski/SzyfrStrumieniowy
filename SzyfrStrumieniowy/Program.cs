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
        public static string CreateKey(int dlugoscKlucza, string seed, string wielomian)
        {
            string klucz = "";
            LfsrGenerator generatorKlucza = new LfsrGenerator(seed, wielomian);
            for (int i = 0; i < dlugoscKlucza; i++)
                klucz += generatorKlucza.GetNextBit();
            return klucz;
        }

        public static string SzyfrStrumieniowyEncode(string ciagBitow, string seed, string wielomian)
        {
            Console.WriteLine($"\nSzyfrowanie:\nciagBitow: {ciagBitow}  ziarno: {seed}  wielomian: {wielomian} ");
            string klucz = CreateKey(ciagBitow.Length, seed, wielomian);
            Console.WriteLine("Klucz = "+klucz);
            string result = "";
            for (int i = 0; i < ciagBitow.Length; i++)
                result += ciagBitow[i] ^ klucz[i];
            Console.WriteLine("Wynik = "+result);
            return result;
        }

        public static string SzyfrStrumieniowyDecode(string text, string seed, string wielomian)
        {
            //Console.WriteLine($"\nOdszyfrowanie:\nciagBitow: {text}  ziarno: {seed}  wielomian: {wielomian} ");
            string klucz = CreateKey(text.Length, seed, wielomian);
            //Console.WriteLine("Klucz = " + klucz);
            string result = "";
            for (int i = 0; i < text.Length; i++)
                result += text[i] ^ klucz[i];
            Console.WriteLine("Odszyfrowanie:\nWynik = " + result);
            return result;
        }

        static void Main()
        {
            string e1 = SzyfrStrumieniowyEncode("111010011100", "0010", "1001");
            string d1 = SzyfrStrumieniowyDecode(e1, "0010", "1001");
            string e2 = SzyfrStrumieniowyEncode("0011001100", "0010", "1001");
            string d2 = SzyfrStrumieniowyDecode(e2, "0010", "1001");
            string e3 = SzyfrStrumieniowyEncode("1110100111001100110011", "11111", "11011");
            string d3 = SzyfrStrumieniowyDecode(e3, "11111", "11011");
        }
    }
}