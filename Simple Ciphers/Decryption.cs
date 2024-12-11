using System.Text;

namespace SimpleCiphers
{
    public static class Decryption
    {
        private static string alpha = "abcdefghijklmnopqrstuvwxyz";
        private static readonly string ALPHA = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private static readonly Dictionary<string, char> morseAlphaSym = new()
        {
            { ".-", 'a' }, { "-...", 'b' }, { "-.-.", 'c' }, { "-..", 'd' }, { ".", 'e' }, { "..-.", 'f' },
            { "--.", 'g' }, { "....", 'h' }, { "..", 'i' }, { ".---", 'j' }, { "-.-", 'k' }, { ".-..", 'l' },
            { "--", 'm' }, { "-.", 'n' }, { "---", 'o' }, { ".--.", 'p' }, { "--.-", 'q' }, { ".-.", 'r' },
            { "...", 's' }, { "-", 't' }, { "..-", 'u' }, { "...-", 'v' }, { ".--", 'w' }, { "-..-", 'x' },
            { "-.--", 'y' }, { "--..", 'z' }, { ".-.-.-", '.' }, { "--..--", ',' }, { ".----.", '\'' }, { "..--..", '?' },
            { "-.-.--", '!' }, { "-..-.", '/' }, { "-.--.", '(' }, { "-.--.-", ')' }, { ".-...", '&' }, { "---...", ':' },
            { "-.-.-.", ';' }, { "-...-", '=' }, { ".-.-.", '+' }, { "-....-", '-' }, { "..--.-", '_' }, { ".-..-.", '"' },
            { "...-..-", '$' }, { ".--.-.", '@' }
        };
        private static readonly Dictionary<string, int> morseNum = new()
        {
            { "-----", 0 }, { ".----", 1 }, { "..---", 2 }, { "...--", 3 }, { "....-", 4 }, { ".....", 5 },
            { "-....", 6 }, { "--...", 7 }, { "---..", 8 }, { "----.", 9 }
        };

        /// <summary>
        /// Decrypts text ciphered with the Atbash cipher.
        /// </summary>
        /// <param name="cipherText"></param>
        /// <returns>a string in plain text.</returns>
        public static string Atbash(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText))
            {
                return string.Empty;
            }

            StringBuilder finalText = new();

            foreach (char letter in cipherText)
            {
                int indexLower = alpha.IndexOf(letter);
                int indexUpper = ALPHA.IndexOf(letter);

                if (indexLower >= 0)
                {
                    finalText.Append(alpha[25 - indexLower]);
                }
                else if (indexUpper >= 0)
                {
                    finalText.Append(ALPHA[25 - indexUpper]);
                }
                else
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
            if (string.IsNullOrEmpty(cipherText))
            {
                return string.Empty;
            }

            if (string.IsNullOrEmpty(key) || !key.All(char.IsLetter))
            {
                throw new ArgumentException("Key must contain only alphabetic characters.");
            }

            cipherText = cipherText.ToLower();
            key = key.ToLower();

            StringBuilder finalText = new();
            int keyShift = 0;

            foreach (char c in cipherText)
            {
                if (!alpha.Contains(c))
                {
                    // Preserve non-alphabetic characters in the ciphertext.
                    finalText.Append(c);
                }
                else
                {
                    // Compute the shifted position for decryption.
                    int letterShift = alpha.IndexOf(c) - alpha.IndexOf(key[keyShift]);

                    if (letterShift < 0)
                    {
                        letterShift += 26;
                    }

                    finalText.Append(alpha[letterShift]);

                    // Move to the next character in the key.
                    keyShift = (keyShift + 1) % key.Length;
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
            if (string.IsNullOrEmpty(cipherText))
            {
                return string.Empty;
            }

            StringBuilder finalText = new();
            int shiftInt = (int)shift % 26;

            foreach (char letter in cipherText)
            {
                int indexLower = alpha.IndexOf(letter);
                int indexUpper = ALPHA.IndexOf(letter);

                if (indexLower > -1)
                {
                    int newIndex = (indexLower - shiftInt + 26) % 26;
                    finalText.Append(alpha[newIndex]);
                }
                else if (indexUpper > -1)
                {
                    int newIndex = (indexUpper - shiftInt + 26) % 26;
                    finalText.Append(ALPHA[newIndex]);
                }
                else
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
            if (string.IsNullOrEmpty(cipherText))
            {
                return string.Empty;
            }

            StringBuilder finalText = new();
            StringBuilder currentNumber = new();

            foreach (char character in cipherText)
            {
                if (char.IsDigit(character))
                {
                    currentNumber.Append(character);
                }
                else
                {
                    if (currentNumber.Length > 0)
                    {
                        if (int.TryParse(currentNumber.ToString(), out int number) && number >= 1 && number <= 26)
                        {
                            char letter = (char)('a' + number - 1);
                            finalText.Append(letter);
                        }
                        else
                        {
                            finalText.Append(currentNumber);
                        }

                        currentNumber.Clear();
                    }

                    finalText.Append(character);
                }
            }

            if (currentNumber.Length > 0)
            {
                if (int.TryParse(currentNumber.ToString(), out int number) && number >= 1 && number <= 26)
                {
                    char letter = (char)('a' + number - 1);
                    finalText.Append(letter);
                }
                else
                {
                    finalText.Append(currentNumber);
                }
            }

            finalText.Replace("-", "");

            return finalText.ToString();
        }

        /// <summary>
        /// Decrypts ciphered text with Morse code.
        /// </summary>
        /// <param name="cipherText"></param>
        /// <returns>a string in plain text.</returns>
        public static string Morse(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText))
            {
                return string.Empty;
            }

            StringBuilder finalText = new();
            string[] words = cipherText.Trim().Split(" / ");

            foreach (string word in words)
            {
                foreach (string letter in word.Split(' '))
                {
                    if (morseAlphaSym.TryGetValue(letter, out char value))
                    {
                        finalText.Append(value);
                    }
                    else if (morseNum.TryGetValue(letter, out int num))
                    {
                        finalText.Append(num);
                    }
                    else
                    {
                        finalText.Append(letter);
                    }
                }
                finalText.Append(' ');
            }

            return finalText.ToString().Trim();
        }

        /// <summary>
        /// Converts binary to plain text.
        /// </summary>
        /// <param name="cipherText">Binary-encoded string with words separated by spaces.</param>
        /// <returns>Decoded plain text string.</returns>
        public static string Binary(string cipherText)
        {
            if (string.IsNullOrWhiteSpace(cipherText))
            {
                return string.Empty;
            }

            StringBuilder finalText = new();
            string[] binaryWords = cipherText.Split(' ');

            foreach (string binary in binaryWords)
            {
                if (binary.Length == 8 && IsBinaryString(binary))
                {
                    try
                    {
                        finalText.Append((char)Convert.ToInt32(binary, 2));
                    }
                    catch (FormatException)
                    {
                        // Log or handle the exception if needed.
                        continue;
                    }
                }
                else
                {
                    // Optionally log or handle invalid binary words.
                    continue;
                }
            }

            return finalText.ToString();
        }

        /// <summary>
        /// Checks if a string contains only binary digits (0 or 1).
        /// </summary>
        /// <param name="input">Input string.</param>
        /// <returns>True if the string is binary; otherwise, false.</returns>
        private static bool IsBinaryString(string input)
        {
            foreach (char c in input)
            {
                if (c != '0' && c != '1')
                {
                    return false;
                }
            }
            return true;
        }
    }
}