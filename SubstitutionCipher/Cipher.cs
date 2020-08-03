using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SubstitutionCipher
{
    class Cipher
    {
        private Dictionary<char, char> encodeChars;
        private Dictionary<char, char> decodeChars;
        private string encode;
        private string decode;

        public Cipher (string key)
        {
            encode = CipherGenerator.Alpha;
            decode = key;
        }

        public string Encode (string cleartext)
        {
            StringBuilder code = new StringBuilder();

            for (int i = 0; i < cleartext.Length; i++)
            {
                if (decode.Contains(cleartext[i].ToString().ToLower()[0]))
                {
                    if (cleartext[i].ToString().ToLower() == cleartext[i].ToString())
                    {
                        code.Append(encode[decode.IndexOf(cleartext[i])]);
                    }
                    else
                    {
                        code.Append(encode[decode.IndexOf(cleartext[i].ToString().ToLower()[0])].ToString().ToUpper());
                    }
                }
                else
                {
                    code.Append(cleartext[i]);
                }
            }

            return code.ToString();
        }

        public string Decode(string code)
        {
            StringBuilder cleartext = new StringBuilder();

            for (int i = 0; i < code.Length; i++)
            {
                if (decode.Contains(code[i].ToString().ToLower()[0]))
                {
                    if (code[i].ToString().ToLower() == code[i].ToString())
                    {
                        cleartext.Append(decode[encode.IndexOf(code[i])]);
                    }
                    else
                    {
                        cleartext.Append(decode[encode.IndexOf(code[i].ToString().ToLower()[0])].ToString().ToUpper());
                    }
                }
                else
                {
                    cleartext.Append(code[i]);
                }
            }

            return cleartext.ToString();
        }
    }
}
