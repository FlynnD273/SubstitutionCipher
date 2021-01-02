using System;
using System.Collections.Generic;
using System.Text;

//This is where the cleartext is converted to ciphertext using a given key

namespace SubstitutionCipher
{
    class CipherGenerator
    {
        public const string Alpha = "abcdefghijklmnopqrstuvwxyz";

        //huffle the characters around to create a random key
        public static string CreateRandomKey()
        {
            StringBuilder lettersLeft = new StringBuilder(Alpha);
            StringBuilder temp = new StringBuilder();
            Random r = new Random();
            for (int i = 0; i < Alpha.Length; i++)
            {
                int index = r.Next(lettersLeft.Length);
                temp.Append(lettersLeft[index]);
                lettersLeft.Remove(index, 1);
            }
            string key = temp.ToString();
            return key;
        }

        //Create a key from two potentially shuffled alphabets
        public static string CreateKey (string original, string next)
        {
            //Pair up the original and new key letters, so we can sort into the regular alphabet by the first key
            List<Tuple<char, char>> sortedLetters = new List<Tuple<char, char>>();
            for (int i = 0; i < next.Length; i++)
            {
                sortedLetters.Add(Tuple.Create(original[i], next[i]));
            }

            //Alphabetize the first key, making a standard key from the second
            sortedLetters.Sort((one, two) => one.Item1.CompareTo(two.Item1));

            StringBuilder sb = new StringBuilder();
            foreach (Tuple<char, char> t in sortedLetters)
            {
                sb.Append(t.Item2);
            }

            return sb.ToString();
        }

        //Create a standard Caesar cipher key
        //Frozen is the letters that won't be affected and will stay in the same spot
        //while the rest of the alphabet rotates around
        public static string Caesar (int Offset, string Frozen)
        {
            //This is our key
            SortedDictionary<char, char> encodeChars = new SortedDictionary<char, char>();

            //We want the frozen letter to stay the same
            for (int i = 0; i < Frozen.Length; i++)
            {
                if (!encodeChars.ContainsKey(Frozen[i]) && Alpha.Contains(Frozen[i]))
                {
                    encodeChars.Add(Frozen[i], Frozen[i]);
                }
            }

            //Rotate the rest of the letters
            for (int c = 0; c < Alpha.Length; c++)
            {
                if (!Frozen.Contains(Alpha[c]))
                {
                    int swap, o = 0;
                    for (swap = c; swap <= Offset + c; swap++)
                    {
                        //Skip past the frozen characters
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
