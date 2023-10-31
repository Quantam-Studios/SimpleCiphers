using System.Text;

namespace SimpleCiphers
{
    public static class Encryption
    {
        private static readonly string alpha = "abcdefghijklmnopqrstuvwxyz";
        private static readonly string ALPHA = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private static readonly string num = "0123456789";
        private static readonly (string, string)[] morseAlpha = { ("a", ".-"), ("b", "-..."), ("c", "-.-."), ("d", "-.."), ("e", "."), ("f", "..-."), ("g", "--."), ("h", "...."), ("i", ".."), ("j", ".---"), ("k", "-.-"), ("l", ".-.."), ("m", "--"), ("n", "-."), ("o", "---"), ("p", ".--."), ("q", "--.-"), ("r", ".-."), ("s", "..."), ("t", "-"), ("u", "..-"), ("v", "...-"), ("w", ".--"), ("x", "-..-"), ("y", "-.--"), ("z", "--..") };
        private static readonly (int, string)[] morseNum = { (0, "-----"), (1, ".----"), (2, "..---"), (3, "...--"), (4, "....-"), (5, "....."), (6, "-...."), (7, "--..."), (8, "---.."), (9, "----.") };
        private static readonly (char, string)[] morseSym = { ('.', ".-.-.-"), (',', "--..--"), ('\'', ".----."), ('?', "..--.."), ('!', "-.-.--"), ('/', "-..-."), ('(', "-.--."), (')', "-.--.-"), ('&', ".-..."), (':', "---..."), (';', "-.-.-."), ('=', "-...-"), ('+', ".-.-."), ('-', "-....-"), ('_', "..--.-"), ('"', ".-..-."), ('$', "...-..-"), ('@', ".--.-.") };
        private static readonly string morseAcceptedSym = ".,'?!/()&:;=+-_\"$@";

        /// <summary>
        /// Encrypts plain text via swapping letters to their opposite positions.
        /// </summary>
        /// <param name="text"></param>
        /// <returns>string</returns>
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
        /// <returns>string</returns>
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
        /// <returns>string</returns>
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
        /// <returns>string</returns>
        public static string Morse(string text)
        {
            string lowerText = text.ToLower();
            StringBuilder finalText = new();

            foreach (char character in lowerText)
            {
                if (alpha.IndexOf(character) > -1) // lower alphabet (lower or upper)
                {
                    for (int i = 0; i < morseAlpha.Length; i++)
                    {
                        if (character.ToString() == morseAlpha[i].Item1)
                        {
                            finalText.Append(" " + morseAlpha[i].Item2);
                        }
                    }
                }
                else if (num.IndexOf(character) > -1) // numbers
                {
                    for (int i = 0; i < morseNum.Length; i++)
                    {
                        if (character == morseNum[i].Item1)
                        {
                            finalText.Append(" " + morseNum[i].Item2);
                        }
                    }
                }
                else if (morseAcceptedSym.IndexOf(character) > -1) // symbols
                {
                    for (int i = 0; i < morseSym.Length; i++)
                    {
                        if (character == morseSym[i].Item1)
                        {
                            finalText.Append(" " + morseSym[i].Item2);
                        }
                    }
                }
                else if (character == ' ') // spaces
                {
                    finalText.Append(" / ");
                }
                else // other
                {
                    finalText.Append(" " + character);
                }
            }

            return finalText.ToString();
        }
    }
}
