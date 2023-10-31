using System.Text;

namespace SimpleCiphers
{
    public static class Encryption
    {
        private static readonly string alpha = "abcdefghijklmnopqrstuvwxyz";
        private static readonly string ALPHA = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private static readonly Dictionary<char, string> morseAlphaSym = new() { { 'a', ".-" }, { 'b', "-..." }, { 'c', "-.-." }, { 'd', "-.." }, { 'e', "." }, { 'f', "..-." }, { 'g', "--." }, { 'h', "...." }, { 'i', ".." }, { 'j', ".---" }, { 'k', "-.-" }, { 'l', ".-.." }, { 'm', "--" }, { 'n', "-." }, { 'o', "---" }, { 'p', ".--." }, { 'q', "--.-" }, { 'r', ".-." }, { 's', "..." }, { 't', "-" }, { 'u', "..-" }, { 'v', "...-" }, { 'w', ".--" }, { 'x', "-..-" }, { 'y', "-.--" }, { 'z', "--.." }, { '.', ".-.-.-" }, { ',', "--..--" }, { '\'', ".----." }, { '?', "..--.." }, { '!', "-.-.--" }, { '/', "-..-." }, { '(', "-.--." }, { ')', "-.--.-" }, { '&', ".-..." }, { ':', "---..." }, { ';', "-.-.-." }, { '=', "-...-" }, { '+', ".-.-." }, { '-', "-....-" }, { '_', "..--.-" }, { '"', ".-..-." }, { '$', "...-..-" }, { '@', ".--.-." } };
        private static readonly Dictionary<int, string> morseNum = new() { { 0, "-----" }, { 1, ".----" }, { 2, "..---" }, { 3, "...--" }, { 4, "....-" }, { 5, "....." }, { 6, "-...." }, { 7, "--..." }, { 8, "---.." }, { 9, "----." } };

        /// <summary>
        /// Encrypts plain text via swapping letters to their opposite positions.
        /// </summary>
        /// <param name="text"></param>
        /// <returns>string in the Atbash cipher.</returns>
        public static string Atbash(string text)
        {
            StringBuilder finalText = new();

            foreach (char letter in text)
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
        /// Encrypts plain text via a letter shift.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="shift"></param>
        /// <returns>string in a Caesar cipher.</returns>
        public static string Caesar(string text, uint shift)
        {
            StringBuilder finalText = new();
            int shiftInt = (int)shift;

            foreach (char letter in text)
            {
                if (alpha.IndexOf(letter) > -1) // lower alphabet
                {
                    if (alpha.IndexOf(letter) + shiftInt <= 25)
                    {
                        finalText.Append(alpha[alpha.IndexOf(letter) + shiftInt]);
                    }
                    else
                    {
                        finalText.Append(alpha[alpha.IndexOf(letter) + shiftInt - 26]);
                    }
                }
                else if (ALPHA.IndexOf(letter) > -1) // upper alphabet
                {
                    if (ALPHA.IndexOf(letter) + shiftInt <= 25)
                    {
                        finalText.Append(ALPHA[ALPHA.IndexOf(letter) + shiftInt]);
                    }
                    else
                    {
                        finalText.Append(ALPHA[ALPHA.IndexOf(letter) + shiftInt - 26]);
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
        /// Encrypts plain text via a numerical value correpsonding to the position in the alphabet.
        /// </summary>
        /// <param name="text"></param>
        /// <returns>string in A1Z26.</returns>
        public static string A1Z26(string text)
        {
            StringBuilder finalText = new();

            int place = 0;
            foreach (char letter in text)
            {
                if (alpha.IndexOf(letter) > -1 || ALPHA.IndexOf(letter) > -1) // lower or upper alphabet
                {
                    int letterVal = (alpha.IndexOf(letter) > -1) ? alpha.IndexOf(letter) + 1 : ALPHA.IndexOf(letter) + 1;
                    finalText.Append(letterVal);
                    if (text.Length > place + 1)
                    {
                        if (alpha.IndexOf(text[place + 1]) > -1 || ALPHA.IndexOf(text[place + 1]) > -1)
                        {
                            finalText.Append('-');
                        }
                    }
                    place += 1;
                }
                else // other
                {
                    place += 1;
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
            string lowerText = text.ToLower();
            StringBuilder finalText = new();

            foreach (char character in lowerText)
            {
                if (morseAlphaSym.TryGetValue(character, out string value)) // alphabet (lower or upper) or symbol
                {
                    finalText.Append(' ' + value);
                }
                else if (int.TryParse(character.ToString(), out int n) && morseNum.TryGetValue(n, out string num)) // numbers
                {
                    finalText.Append(' ' + num);
                }
                else if (character == ' ') // spaces
                {
                    finalText.Append(" / ");
                }
                else // other
                {
                    finalText.Append(' ' + character);
                }
            }

            return finalText.ToString();
        }
    }
}
