using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace SubstitutionCipher.Resources
{
    class CipherCracker
    {
        public string Code { get; private set; }
        public string Cleartext { get; private set; }

        private Dictionary<string, ulong> grams;
        private int gramLength;
        private ulong totalGrams;

        public CipherCracker (string code)
        {
            Code = code;
            grams = new Dictionary<string, ulong>();
            string[] trigrams = Properties.Resources.Trigrams_English.Split('\n');
            foreach (string line in trigrams)
            {
                string[] tokens = line.Split("\t");
                grams.Add(tokens[0].ToLower(), ulong.Parse(tokens[1]));
                gramLength = tokens[0].ToLower().Length;
                totalGrams += ulong.Parse(tokens[1]);
            }
        }
        
        public Tuple<string, string, double> Decipher (string code = "")
        {
            if (code != "")
            {
                Code = code;
            }

            string key = CipherGenerator.Alpha;
            string testKey = key;

            double currentFitness = CalcFitness(Code);

            string betterKey = key;
            int first = 0, sec = 0;
            bool betterFitness;

            do
            {
                betterFitness = false;
                key = betterKey;
                for (first = 0; first < key.Length - 1; first++)
                {
                    for (sec = first + 1; sec < key.Length; sec++)
                    {
                        testKey = Swap(key, first, sec);
                        Cipher c = new Cipher(testKey);
                        Cleartext = c.Decode(Code);
                        double tempFitness = CalcFitness(Cleartext);
                        if (tempFitness > currentFitness)
                        {
                            currentFitness = tempFitness;
                            betterKey = testKey;
                            betterFitness = true;
                        }
                    }
                }
            }
            while (betterFitness);

            key = betterKey;

            //Random r = new Random();
            //while (first < 200000)
            //{
            //    testKey = Swap(key, r.Next(key.Length), r.Next(key.Length));
            //    Cipher c = new Cipher(CipherGenerator.FromKey(testKey));
            //    Cleartext = c.Decode(Code);
            //    double tempFitness = CalcFitness(Cleartext);
            //    if (tempFitness > currentFitness)
            //    {
            //        currentFitness = tempFitness;
            //        key = testKey;
            //        first = 0;
            //    }
            //    else
            //    {
            //        first++;
            //    }
            //}

            Cipher cipher = new Cipher(key);
            Cleartext = cipher.Decode(Code);

            return Tuple.Create(Cleartext, key, currentFitness);
        }

        private string Swap (string s, int i, int j)
        {
            string first = s[i].ToString();
            string second = s[j].ToString();
            return Regex.Replace(s, $"{first}|{second}", (m) => m.Value == first.ToString() ? second : first);
        }

        private double CalcFitness(string text)
        {
            text = text.ToLower();
            StringBuilder sb = new StringBuilder();

            foreach (char c in text)
            {
                if (CipherGenerator.Alpha.Contains(c))
                {
                    sb.Append(c);
                }
            }

            text = sb.ToString();

            double fitness = 0;

            for (int i = 0; i < text.Length - gramLength; i++)
            {
                string g = text.Substring(i, gramLength);
                if (grams.ContainsKey(g))
                {
                    fitness += Math.Log(grams[g] / (double)totalGrams);
                }
            }

            return fitness;
        }
    }
}
