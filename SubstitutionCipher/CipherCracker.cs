using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.RegularExpressions;

namespace SubstitutionCipher.Resources
{
    class CipherCracker
    {
        public string Code { get; private set; }
        public string Cleartext { get; private set; }

        private Dictionary<string, double> grams;
        private int gramLength;
        private ulong totalGrams;
        private double norm;

        public CipherCracker (string code)
        {
            norm = 0;
            ulong[] top = new ulong[5];
            Code = code;
            grams = new Dictionary<string, double>();
            string[] trigrams = Properties.Resources.Trigrams_English.Split('\n');
            for (int i = 0; i < trigrams.Length; i++)
            {
                string line = trigrams[i];
                string[] tokens = line.Split("\t");
                grams.Add(tokens[0].ToLower(), ulong.Parse(tokens[1]));
                gramLength = tokens[0].ToLower().Length;
                totalGrams += ulong.Parse(tokens[1]);
                if (i < top.Length)
                {
                    top[i] = ulong.Parse(tokens[1]);
                }
            }

            List<string> keys = new List<string>(grams.Keys);
            foreach (string s in keys)
            {
                grams[s] /= totalGrams;
            }

            for (int i = 0; i < top.Length; i++)
            {
                norm += Math.Log(top[i]);// / (double)totalGrams);
            }
            norm /= top.Length;
        }
        
        public Tuple<string, string, double> Decipher (string code = "")
        {
            if (code != "")
            {
                Code = code;
            }
            string key = CipherGenerator.CreateRandomKey();
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
                        if (tempFitness < currentFitness)
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
            int l = 0;
            for (int i = 0; i < text.Length - gramLength; i++)
            {
                string g = text.Substring(i, gramLength);
                if (grams.ContainsKey(g))
                {
                    fitness += Math.Log(grams[g]);
                    l++;
                }
            }
            fitness /= l;
            fitness = Math.Abs(fitness - norm) / norm;

            return fitness;
        }
    }
}
