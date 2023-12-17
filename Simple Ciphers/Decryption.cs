using System;
using System.ComponentModel.Design;
using System.Reflection;
using System.Text;

namespace SimpleCiphers
{
    public static class Decryption
    {
        private static readonly string alpha = "abcdefghijklmnopqrstuvwxyz";
        private static readonly string ALPHA = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private static readonly Dictionary<string, char> morseAlphaSym = new() { { ".-", 'a' }, { "-...", 'b' }, { "-.-.", 'c' }, { "-..", 'd' }, { ".", 'e' }, { "..-.", 'f' }, { "--.", 'g' }, { "....", 'h' }, { "..", 'i' }, { ".---", 'j' }, { "-.-" , 'k' }, { ".-.." , 'l' }, { "--", 'm' }, { "-.", 'n' }, { "---", 'o' }, { ".--.", 'p' }, { "--.-", 'q' }, { ".-.", 'r' }, { "...", 's' }, { "-", 't' }, { "..-", 'u' }, { "...-", 'v' }, { ".--", 'w' }, { "-..-", 'x' }, { "-.--", 'y' }, { "--..", 'z' }, { ".-.-.-", '.' }, { "--..--", ',' }, { ".----.", '\'' }, { "..--..", '?' }, { "-.-.--", '!' }, { "-..-.", '/' }, { "-.--.", '(' }, { "-.--.-", ')' }, { ".-...", '&' }, { "---...", ':' }, { "-.-.-.", ';' }, { "-...-", '=' }, { ".-.-.", '+' }, { "-....-", '-' }, { "..--.-", '_' }, { ".-..-.", '"' }, { "...-..-", '$' }, { ".--.-.", '@' } };
        private static readonly Dictionary<string, int> morseNum = new() { { "-----", 0 }, { ".----", 1 }, { "..---", 2 }, { "...--", 3 }, { "....-", 4 }, { ".....", 5 }, { "-....", 6 }, { "--...", 7 }, { "---..", 8 }, { "----.", 9 } };

        /// <summary>
        /// Decrypts text ciphered with the Atbash cipher.
        /// </summary>
        /// <param name="cipherText"></param>
        /// <returns>a string in plain text.</returns>
        public static string Atbash(string cipherText)
		{
            StringBuilder finalText = new();

            foreach (char letter in cipherText)
            {
                if (alpha.IndexOf(letter) > -1) // lower alphabet
                {
                    int finalVal = 25 - alpha.IndexOf(letter);
                    finalText.Append(alpha[finalVal]);
                }
                else if (ALPHA.IndexOf(letter) > -1) // upper alphabet
                {
                    int finalVal = 25 - ALPHA.IndexOf(letter);
                    finalText.Append(ALPHA[finalVal]);
                }
                else // other
                {
                    finalText.Append(letter);
                }
            }

            return finalText.ToString();
        }

        /// <summary>
        /// Decrypts text ciphered with a Vigenere cipher.
        /// </summary>
        /// <param name="cipherText"></param>
        /// <param name="key"></param>
        /// <returns>a string in plain text.</returns>
        public static string Vigenere(string cipherText, string key)
        {
            cipherText = cipherText.ToLower();

            StringBuilder finalText = new();
            int keyShift = 0;

            foreach(char c in cipherText)
            {
                if (!alpha.Contains(c)) // symbols or spaces
                {
                    finalText.Append(c);
                }
                else // letters
                {
                    int letterShift = alpha.IndexOf(c) - alpha.IndexOf(key[keyShift]);

                    if (letterShift >= 0)
                    {
                        finalText.Append(alpha[letterShift]);
                    }
                    else if(letterShift < 0)
                    {
                        letterShift += 26;
                        finalText.Append(alpha[letterShift]);
                    }

                    keyShift++;
                    if (letterShift == key.Length)
                    {
                        keyShift = 0;
                    }
                }
            }

            return finalText.ToString();
        }

        /// <summary>
        /// Decrypts text ciphered with a Caesar cipher.
        /// </summary>
        /// <param name="cipherText"></param>
        /// <param name="shift"></param>
        /// <returns>a string in plain text.</returns>
        public static string Caesar(string cipherText, uint shift)
        {
            StringBuilder finalText = new();
            int shiftInt = (int)shift;

            foreach (char letter in cipherText)
            {
                if (alpha.IndexOf(letter) > -1) // lower alphabet
                {
                    if (alpha.IndexOf(letter) - shiftInt >= 0)
                    {
                        finalText.Append(alpha[alpha.IndexOf(letter) - shiftInt]);
                    }
                    else
                    {
                        finalText.Append(alpha[alpha.IndexOf(letter) - shiftInt + 26]);
                    }
                }
                else if (ALPHA.IndexOf(letter) > -1) // upper alphabet
                {
                    if (ALPHA.IndexOf(letter) - shiftInt >= 0)
                    {
                        finalText.Append(ALPHA[ALPHA.IndexOf(letter) - shiftInt]);
                    }
                    else
                    {
                        finalText.Append(ALPHA[ALPHA.IndexOf(letter) - shiftInt + 26]);
                    }
                }
                else // other
                {
                    finalText.Append(letter);
                }
            }

            return finalText.ToString();
        }

        /// <summary>
        /// Decrypts text ciphered with the A1Z26 cipher.
        /// </summary>
        /// <param name="cipherText"></param>
        /// <returns>a string in plain text.</returns>
        public static string A1Z26(string cipherText)
        {
            StringBuilder finalText = new();
            string[] words = cipherText.Split(separator: " ");
            foreach(string word in words)
            {
                foreach (string letter in word.Split("-"))
                {
                    if (int.TryParse(letter, out int n)) // alphabet (lower or upper)
                    {
                        if (int.Parse(letter) <= 26)
                        {
                            finalText.Append(alpha[n - 1]);
                        }
                    }
                    else // other
                    {
                        finalText.Append(letter);
                    }
                }
                finalText.Append(' ');
            }

            return finalText.ToString();
        }

        /// <summary>
        /// Decrypts ciphered text with Morse code.
        /// </summary>
        /// <param name="cipherText"></param>
        /// <returns>a string in plain text.</returns>
        public static string Morse(string cipherText)
        {
            StringBuilder finalText = new();
            string[] words = cipherText.Split(separator: " / ");
            foreach (string word in words)
            {
                foreach (string letter in word.Split(" "))
                {
                    if (morseAlphaSym.TryGetValue(letter, out char value)) // alphabet (lower or upper) or symbol
                    {
                        finalText.Append(value);
                    }
                    else if (morseNum.TryGetValue(letter, out int num)) // numbers
                    {
                        finalText.Append(num);
                    }
                    else // other
                    {
                        finalText.Append(letter);
                    }
                }
                finalText.Append(' ');
            }

            return finalText.ToString();
        }
	}
}