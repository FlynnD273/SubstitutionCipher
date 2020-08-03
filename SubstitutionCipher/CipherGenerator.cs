using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SubstitutionCipher
{
    class CipherGenerator
    {
        public const string Alpha = "abcdefghijklmnopqrstuvwxyz";

        public static string CreateRandomKey()
        {
            StringBuilder lettersLeft = new StringBuilder(CipherGenerator.Alpha);
            StringBuilder temp = new StringBuilder();
            Random r = new Random();
            for (int i = 0; i < CipherGenerator.Alpha.Length; i++)
            {
                int index = r.Next(lettersLeft.Length);
                temp.Append(lettersLeft[index]);
                lettersLeft.Remove(index, 1);
            }
            string key = temp.ToString();
            return key;
        }
        public static string CreateKey (string original, string next)
        {
            List<Tuple<char, char>> sortedLetters = new List<Tuple<char, char>>();
            for (int i = 0; i < next.Length; i++)
            {
                sortedLetters.Add(Tuple.Create(original[i], next[i]));
            }
            sortedLetters.Sort((one, two) => one.Item1.CompareTo(two.Item1));

            StringBuilder sb = new StringBuilder();
            foreach (Tuple<char, char> t in sortedLetters)
            {
                sb.Append(t.Item2);
            }

            return sb.ToString();
        }

        //public static Dictionary<char, char> FromKey(string key)
        //{
        //    key = key.ToLower();
        //    key = string.Concat(key.Distinct());

        //    Dictionary<char, char> encodeChars = new Dictionary<char, char>();

        //    for (int i = 0; i < 26; i++)
        //    {
        //        encodeChars.Add(Alpha[i], key[i]);
        //    }

        //    return encodeChars;
        //}

        public static string Caesar (int Offset, string Frozen)
        {
            SortedDictionary<char, char> encodeChars = new SortedDictionary<char, char>();

            for (int i = 0; i < Frozen.Length; i++)
            {
                if (!encodeChars.ContainsKey(Frozen[i]) && Alpha.Contains(Frozen[i]))
                {
                    encodeChars.Add(Frozen[i], Frozen[i]);
                }
            }

            for (int c = 0; c < Alpha.Length; c++)
            {
                if (!Frozen.Contains(Alpha[c]))
                {
                    int swap, o = 0;
                    for (swap = c; swap <= Offset + c; swap++)
                    {
                        while (Frozen.Contains(Alpha[(swap + o) % Alpha.Length]))
                        {
                            o++;
                        }
                    }
                    swap--;

                    encodeChars.Add(Alpha[c], Alpha[(swap + o) % Alpha.Length]);
                }
            }
            StringBuilder sb = new StringBuilder();

            foreach (KeyValuePair<char, char> p in encodeChars)
            {
                sb.Append(p.Value);
            }


            return sb.ToString();
        }
    }
}
