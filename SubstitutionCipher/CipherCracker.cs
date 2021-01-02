using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

//Cracker any substitution cipher by randomly mutating a key

namespace SubstitutionCipher.Resources
{
    class CipherCracker
    {
        public string Code { get; private set; }
        public string Cleartext { get; private set; }

        private Dictionary<string, double> grams;
        private HashSet<string> dict;
        private int gramLength;
        private ulong totalGrams;
        private double norm;

        public CipherCracker (string code)
        {
            norm = 0;
            ulong[] top = new ulong[5];
            Code = code;
            grams = new Dictionary<string, double>();

            //load trigrams from memory

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

            //Attempt to normalize values
            for (int i = 0; i < top.Length; i++)
            {
                norm += Math.Log(top[i]);
            }
            norm /= top.Length;


            dict = new HashSet<string>();
            Assembly assembly = Assembly.GetExecutingAssembly();
            string resourceName = assembly.GetManifestResourceNames().Single(str => str.EndsWith("english3.txt"));
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader sr = new StreamReader(stream))
            {
                while (!sr.EndOfStream)
                {
                    dict.Add(sr.ReadLine());
                }
            }
        }
        
        public Tuple<string, string, double> Decipher (string code = "")
        {
            Stopwatch s = new Stopwatch();
            s.Start();
            if (code != "")
            {
                Code = code;
            }
            string key = CipherGenerator.CreateRandomKey();
            string betterKey = key;
            string bestKey = key;

            double betterFitness = CalcFitness(Code);
            double bestFitness = betterFitness;
            double words;

            int first = 0, sec = 0, tries = 0;
            bool hasBetterFitness;

            //Check a bunch of similar keys, changing a little each time
            do
            {
                do
                {
                    hasBetterFitness = false;
                    key = betterKey;
                    for (first = 0; first < key.Length - 1; first++)
                    {
                        for (sec = first; sec < key.Length; sec++)
                        {
                            //Swap two letters
                            string testKey = sec == first ? key : Swap(key, first, sec);
                            Cipher c = new Cipher(testKey);
                            Cleartext = c.Decode(Code);
                            //Check if it's English
                            double fitness = CalcFitness(Cleartext);
                            if (fitness < betterFitness)
                            {
                                //Use the better key as a base key
                                betterFitness = fitness;
                                betterKey = testKey;
                                hasBetterFitness = true;
                            }
                        }
                    }
                }
                while (hasBetterFitness);

                if (betterFitness < bestFitness)
                {
                    bestFitness = betterFitness;
                    bestKey = betterKey;
                }
                else
                {
                    tries++;
                }


                betterKey = CipherGenerator.CreateRandomKey();
                key = bestKey;
                Cipher cipher = new Cipher(key);
                Cleartext = cipher.Decode(Code);

                //Check agaainst a dictionary to avoid local maxima
                words = CountGoodWords(KeepWords(Cleartext, " "));
                betterFitness = CalcFitness(Cleartext);

                if (tries > 10)
                {
                    bestFitness = 10;
                    tries = 0;
                }
            }
            while (words > 0.12);

            s.Stop();
            var time = s.Elapsed;

            return Tuple.Create(Cleartext, key, bestFitness);
        }

        private string Swap (string s, int i, int j)
        {
            string first = s[i].ToString();
            string second = s[j].ToString();
            return Regex.Replace(s, $"{first}|{second}", (m) => m.Value == first.ToString() ? second : first);
        }

        //Fitness is a combination of words from a dictionary and trigrams
        private double CalcFitness(string text)
        {
            double f = CalcFitnessFromWords(KeepWords(text));
            return CountGoodWords(KeepWords(text, " ")) * 0.2 + f;
        }

        //Check against a dictionary
        private double CountGoodWords(string text)
        {
            string[] words = text.Split(' ');
            int count = words.Length;

            foreach (string s in words)
            {
                if (dict.Contains(s))
                {
                    count--;
                }
            }

            double r = (double)count / words.Length;
            return r;
        }

        //Strip all invalid characters from the string, this is just for the rigram calculations
        private static string KeepWords(string text, string keep = "")
        {
            text = text.ToLower();
            StringBuilder sb = new StringBuilder();

            foreach (char c in text)
            {
                if (CipherGenerator.Alpha.Contains(c) || keep.Contains(c))
                {
                    sb.Append(c);
                }
            }

            text = sb.ToString();
            return text;
        }

        //Calculate fitness using trigrams
        private double CalcFitnessFromWords(string text)
        {
            if (text.Length >= gramLength)
            {
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
            return -1;
        }
    }
}
