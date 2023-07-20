using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Security.Cryptography;

namespace PHEDServe.Models
{
   public class PaymentHash
    {
       

        #region Hash Hex Functions
        public static string HashHMACHex(string keyHex, string message)
        {
            byte[] hash = HashHMAC(HexDecode(keyHex), StringEncode(message));
            return HashEncode(hash);
        }

        private static string HashSHAHex(string innerKeyHex, string outerKeyHex, string message)
        {
            byte[] hash = HashSHA(HexDecode(innerKeyHex),HexDecode(outerKeyHex), StringEncode(message));
            return HashEncode(hash);
        }
        #endregion

        #region Hash Functions
        private static byte[] HashHMAC(byte[] key, byte[] message)
        {
            var hash = new HMACSHA256(key);
            return hash.ComputeHash(message);
        }

        private static byte[] HashSHA(byte[] innerKey, byte[] outerKey, byte[] message)
        {
            var hash = new SHA256Managed();

            // Compute the hash for the inner data first
            byte[] innerData = new byte[innerKey.Length + message.Length];
            Buffer.BlockCopy(innerKey, 0, innerData, 0, innerKey.Length);
            Buffer.BlockCopy(message, 0, innerData, innerKey.Length, message.Length);
            byte[] innerHash = hash.ComputeHash(innerData);

            // Compute the entire hash
            byte[] data = new byte[outerKey.Length + innerHash.Length];
            Buffer.BlockCopy(outerKey, 0, data, 0, outerKey.Length);
            Buffer.BlockCopy(innerHash, 0, data, outerKey.Length, innerHash.Length);
            byte[] result = hash.ComputeHash(data);

            return result;
        }
        #endregion

        #region Encoding Helpers
        public static byte[] StringEncode(string text)
        {
            var encoding = new ASCIIEncoding();
            return encoding.GetBytes(text);
        }

        private static string HashEncode(byte[] hash)
        {
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }

        private static byte[] HexDecode(string hex)
        {
            var bytes = new byte[hex.Length / 2];
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = byte.Parse(hex.Substring(i * 2, 2),NumberStyles.HexNumber);
            }
            return bytes;
        }
        #endregion
    }

   public class RandomPassword
   {
       // Define default min and max password lengths.
       private static int DEFAULT_MIN_PASSWORD_LENGTH = 8;
       private static int DEFAULT_MAX_PASSWORD_LENGTH = 10;

       // Define supported password characters divided into groups.
       // You can add (or remove) characters to (from) these groups.

       private static string PASSWORD_CHARS_LCASE = "1029394875643528374889890876754325678";
       private static string PASSWORD_CHARS_UCASE = "1312423530460457506867097808973019886";
       private static string PASSWORD_CHARS_NUMERIC = "23404560789989820103040560687904857";
       private static string PASSWORD_CHARS_SPECIAL = "12345067890495856040303847589302498";

       /// <summary>
       /// Generates a random password.
       /// </summary>
       /// <returns>
       /// Randomly generated password.
       /// </returns>
       /// <remarks>
       /// The length of the generated password will be determined at
       /// random. It will be no shorter than the minimum default and
       /// no longer than maximum default.
       /// </remarks>
       public static string Generate()
       {
           return Generate(DEFAULT_MIN_PASSWORD_LENGTH,
                           DEFAULT_MAX_PASSWORD_LENGTH);
       }

       /// <summary>
       /// Generates a random password of the exact length.
       /// </summary>
       /// <param name="length">
       /// Exact password length.
       /// </param>
       /// <returns>
       /// Randomly generated password.
       /// </returns>
       public static string Generate(int length)
       {
           return Generate(length, length);
       }

       /// <summary>
       /// Generates a random password.
       /// </summary>
       /// <param name="minLength">
       /// Minimum password length.
       /// </param>
       /// <param name="maxLength">
       /// Maximum password length.
       /// </param>
       /// <returns>
       /// Randomly generated password.
       /// </returns>
       /// <remarks>
       /// The length of the generated password will be determined at
       /// random and it will fall with the range determined by the
       /// function parameters.
       /// </remarks>
       public static string Generate(int minLength, int maxLength)
       {
           // Make sure that input parameters are valid.
           if (minLength <= 0 || maxLength <= 0 || minLength > maxLength)
               return null;

           // Create a local array containing supported password characters
           // grouped by types. You can remove character groups from this
           // array, but doing so will weaken the password strength.
           char[][] charGroups = new char[][]
        {
            PASSWORD_CHARS_LCASE.ToCharArray(),
            PASSWORD_CHARS_UCASE.ToCharArray(),
            PASSWORD_CHARS_NUMERIC.ToCharArray(),
            PASSWORD_CHARS_SPECIAL.ToCharArray()
        };

           // Use this array to track the number of unused characters in each
           // character group.
           int[] charsLeftInGroup = new int[charGroups.Length];

           // Initially, all characters in each group are not used.
           for (int i = 0; i < charsLeftInGroup.Length; i++)
               charsLeftInGroup[i] = charGroups[i].Length;

           // Use this array to track (iterate through) unused character groups.
           int[] leftGroupsOrder = new int[charGroups.Length];

           // Initially, all character groups are not used.
           for (int i = 0; i < leftGroupsOrder.Length; i++)
               leftGroupsOrder[i] = i;

           // Because we cannot use the default randomizer, which is based on the
           // current time (it will produce the same "random" number within a
           // second), we will use a random number generator to seed the
           // randomizer.

           // Use a 4-byte array to fill it with random bytes and convert it then
           // to an integer value.
           byte[] randomBytes = new byte[4];

           // Generate 4 random bytes.
           RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
           rng.GetBytes(randomBytes);

           // Convert 4 bytes into a 32-bit integer value.
           int seed = (randomBytes[0] & 0x7f) << 24 |
                       randomBytes[1] << 16 |
                       randomBytes[2] << 8 |
                       randomBytes[3];

           // Now, this is real randomization.
           Random random = new Random(seed);

           // This array will hold password characters.
           char[] password = null;

           // Allocate appropriate memory for the password.
           if (minLength < maxLength)
               password = new char[random.Next(minLength, maxLength + 1)];
           else
               password = new char[minLength];

           // Index of the next character to be added to password.
           int nextCharIdx;

           // Index of the next character group to be processed.
           int nextGroupIdx;

           // Index which will be used to track not processed character groups.
           int nextLeftGroupsOrderIdx;

           // Index of the last non-processed character in a group.
           int lastCharIdx;

           // Index of the last non-processed group.
           int lastLeftGroupsOrderIdx = leftGroupsOrder.Length - 1;

           // Generate password characters one at a time.
           for (int i = 0; i < password.Length; i++)
           {
               // If only one character group remained unprocessed, process it;
               // otherwise, pick a random character group from the unprocessed
               // group list. To allow a special character to appear in the
               // first position, increment the second parameter of the Next
               // function call by one, i.e. lastLeftGroupsOrderIdx + 1.
               if (lastLeftGroupsOrderIdx == 0)
                   nextLeftGroupsOrderIdx = 0;
               else
                   nextLeftGroupsOrderIdx = random.Next(0,
                                                        lastLeftGroupsOrderIdx);

               // Get the actual index of the character group, from which we will
               // pick the next character.
               nextGroupIdx = leftGroupsOrder[nextLeftGroupsOrderIdx];

               // Get the index of the last unprocessed characters in this group.
               lastCharIdx = charsLeftInGroup[nextGroupIdx] - 1;

               // If only one unprocessed character is left, pick it; otherwise,
               // get a random character from the unused character list.
               if (lastCharIdx == 0)
                   nextCharIdx = 0;
               else
                   nextCharIdx = random.Next(0, lastCharIdx + 1);

               // Add this character to the password.
               password[i] = charGroups[nextGroupIdx][nextCharIdx];

               // If we processed the last character in this group, start over.
               if (lastCharIdx == 0)
                   charsLeftInGroup[nextGroupIdx] =
                                             charGroups[nextGroupIdx].Length;
               // There are more unprocessed characters left.
               else
               {
                   // Swap processed character with the last unprocessed character
                   // so that we don't pick it until we process all characters in
                   // this group.
                   if (lastCharIdx != nextCharIdx)
                   {
                       char temp = charGroups[nextGroupIdx][lastCharIdx];
                       charGroups[nextGroupIdx][lastCharIdx] =
                                   charGroups[nextGroupIdx][nextCharIdx];
                       charGroups[nextGroupIdx][nextCharIdx] = temp;
                   }
                   // Decrement the number of unprocessed characters in
                   // this group.
                   charsLeftInGroup[nextGroupIdx]--;
               }

               // If we processed the last group, start all over.
               if (lastLeftGroupsOrderIdx == 0)
                   lastLeftGroupsOrderIdx = leftGroupsOrder.Length - 1;
               // There are more unprocessed groups left.
               else
               {
                   // Swap processed group with the last unprocessed group
                   // so that we don't pick it until we process all groups.
                   if (lastLeftGroupsOrderIdx != nextLeftGroupsOrderIdx)
                   {
                       int temp = leftGroupsOrder[lastLeftGroupsOrderIdx];
                       leftGroupsOrder[lastLeftGroupsOrderIdx] =
                                   leftGroupsOrder[nextLeftGroupsOrderIdx];
                       leftGroupsOrder[nextLeftGroupsOrderIdx] = temp;
                   }
                   // Decrement the number of unprocessed groups.
                   lastLeftGroupsOrderIdx--;
               }
           }

           // Convert password characters into a string and return the result.
           return new string(password);
       }


   }
}
 









