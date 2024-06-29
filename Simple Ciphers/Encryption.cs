using System.Text;

namespace SimpleCiphers
{
    public static class Encryption
    {
        private static readonly string alpha = "abcdefghijklmnopqrstuvwxyz";
        private static readonly string ALPHA = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private static readonly Dictionary<char, string> morseAlphaSym = new()
        {
            { 'a', ".-" }, { 'b', "-..." }, { 'c', "-.-." }, { 'd', "-.." }, { 'e', "." }, { 'f', "..-." }, { 'g', "--." },
            { 'h', "...." }, { 'i', ".." }, { 'j', ".---" }, { 'k', "-.-" }, { 'l', ".-.." }, { 'm', "--" }, { 'n', "-." },
            { 'o', "---" }, { 'p', ".--." }, { 'q', "--.-" }, { 'r', ".-." }, { 's', "..." }, { 't', "-" }, { 'u', "..-" },
            { 'v', "...-" }, { 'w', ".--" }, { 'x', "-..-" }, { 'y', "-.--" }, { 'z', "--.." }, { '.', ".-.-.-" }, { ',', "--..--" },
            { '\'', ".----." }, { '?', "..--.." }, { '!', "-.-.--" }, { '/', "-..-." }, { '(', "-.--." }, { ')', "-.--.-" },
            { '&', ".-..." }, { ':', "---..." }, { ';', "-.-.-." }, { '=', "-...-" }, { '+', ".-.-." }, { '-', "-....-" },
            { '_', "..--.-" }, { '"', ".-..-." }, { '$', "...-..-" }, { '@', ".--.-." }
        };

        private static readonly Dictionary<int, string> morseNum = new()
        {
            { 0, "-----" }, { 1, ".----" }, { 2, "..---" }, { 3, "...--" }, { 4, "....-" }, { 5, "....." },
            { 6, "-...." }, { 7, "--..." }, { 8, "---.." }, { 9, "----." }
        };

        /// <summary>
        /// Encrypts plain text via swapping letters to their opposite positions.
        /// </summary>
        /// <param name="text"></param>
        /// <returns>string in the Atbash cipher.</returns>
        public static string Atbash(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }

            StringBuilder finalText = new();

            foreach (char letter in text)
            {
                int indexLower = alpha.IndexOf(letter);
                int indexUpper = ALPHA.IndexOf(letter);

                if (indexLower > -1)
                {
                    int finalVal = 25 - indexLower;
                    finalText.Append(alpha[finalVal]);
                }
                else if (indexUpper > -1)
                {
                    int finalVal = 25 - indexUpper;
                    finalText.Append(ALPHA[finalVal]);
                }
                else
                {
                    finalText.Append(letter);
                }
            }

            return finalText.ToString();
        }

        /// <summary>
        /// Encrypts plain text via a letter shift.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="shift"></param>
        /// <returns>string in a Caesar cipher.</returns>
        public static string Caesar(string text, uint shift)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }

            StringBuilder finalText = new();
            int shiftInt = (int)shift % 26;

            foreach (char letter in text)
            {
                int indexLower = alpha.IndexOf(letter);
                int indexUpper = ALPHA.IndexOf(letter);

                if (indexLower > -1)
                {
                    finalText.Append(alpha[(indexLower + shiftInt) % 26]);
                }
                else if (indexUpper > -1)
                {
                    finalText.Append(ALPHA[(indexUpper + shiftInt) % 26]);
                }
                else
                {
                    finalText.Append(letter);
                }
            }

            return finalText.ToString();
        }

        /// <summary>
        /// Encrypts plain text via a key.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="key"></param>
        /// <returns>string in a Vigenere cipher.</returns>
        public static string Vigenere(string text, string key)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }

            text = text.ToLower();
            key = key.ToLower();

            StringBuilder finalText = new();
            int keyShift = 0;

            foreach (char c in text)
            {
                if (!alpha.Contains(c))
                {
                    finalText.Append(c);
                }
                else
                {
                    int letterShift = alpha.IndexOf(key[keyShift]) + alpha.IndexOf(c);

                    if (letterShift >= 26)
                    {
                        letterShift -= 26;
                    }

                    finalText.Append(alpha[letterShift]);
                    keyShift = (keyShift + 1) % key.Length;
                }
            }

            return finalText.ToString();
        }

        /// <summary>
        /// Encrypts plain text via a numerical value correpsonding to the position in the alphabet.
        /// </summary>
        /// <param name="text"></param>
        /// <returns>string in A1Z26.</returns>
        public static string A1Z26(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }

            StringBuilder finalText = new();

            for (int i = 0; i < text.Length; i++)
            {
                char letter = text[i];
                int indexLower = alpha.IndexOf(letter);
                int indexUpper = ALPHA.IndexOf(letter);

                if (indexLower > -1 || indexUpper > -1)
                {
                    int letterVal = (indexLower > -1) ? indexLower + 1 : indexUpper + 1;
                    finalText.Append(letterVal);

                    if (i < text.Length - 1 && (alpha.Contains(text[i + 1]) || ALPHA.Contains(text[i + 1])))
                    {
                        finalText.Append('-');
                    }
                }
                else
                {
                    finalText.Append(letter);
                }
            }

            return finalText.ToString();
        }

        /// <summary>
        /// Converts plain text to Morse code.
        /// </summary>
        /// <param name="text"></param>
        /// <returns>string in Morse code.</returns>
        public static string Morse(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }

            StringBuilder finalText = new();

            foreach (char character in text.ToLower())
            {
                if (morseAlphaSym.TryGetValue(character, out string? value))
                {
                    if (value != null)
                    {
                        finalText.Append(value + " ");
                    }
                }
                else if (char.IsDigit(character) && morseNum.TryGetValue(character - '0', out string? num))
                {
                    if (num != null)
                    {
                        finalText.Append(num + " ");
                    }
                }
                else if (character == ' ')
                {
                    finalText.Append("/ ");
                }
                else
                {
                    finalText.Append(character + " ");
                }
            }

            return finalText.ToString().Trim();
        }
    }
}
