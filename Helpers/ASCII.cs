/*
 Made by Movsisyan.
 Find me on GitHub.
 Contact me at movsisyan@protonmail.com for future endeavors.
 Գտիր ինձ ԳիթՀաբ-ում:
 Գրիր ինձ movsisyan@protonmail.com հասցեյով հետագա առաջարկների համար:
 2019
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AR5001D
{
    public static class ASCII
    {
        private static List<char> asciiCharacters = (@"	
 !" + '"' + @"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\]^_`abcdefghijklmnopqrstuvwxyz{|}~").ToList();

        /// <summary>
        /// Converts the input character into a byte encoded in ascii
        /// </summary>
        /// <param name="c">Input character that is to be converted</param>
        /// <returns>Representation of said character in byte ascii format</returns>
        public static byte ToASCII(this char c)
        {
            #region test
            //switch (c.ToString().ToUpper().ToCharArray()[0])
            //{
            //    case 'A':
            //        return 0x41;
            //    case 'B':
            //        return 0x42;
            //    case 'C':
            //        return 0x43;
            //    case 'D':
            //        return 0x44;
            //    case 'E':
            //        return 0x45;
            //    case 'F':
            //        return 0x46;
            //    case 'G':
            //        return 0x47;
            //    case 'H':
            //        return 0x48;
            //    case 'I':
            //        return 0x49;
            //    case 'J':
            //        return 0x4a;
            //    case 'K':
            //        return 0x4b;
            //    case 'L':
            //        return 0x4c;
            //    case 'M':
            //        return 0x4d;
            //    case 'N':
            //        return 0x4e;
            //    case 'O':
            //        return 0x4f;
            //    case 'P':
            //        return 0x50;
            //    case 'Q':
            //        return 0x51;
            //    case 'R':
            //        return 0x52;
            //    case 'S':
            //        return 0x53;
            //    case 'T':
            //        return 0x54;
            //    case 'U':
            //        return 0x55;
            //    case 'V':
            //        return 0x56;
            //    case 'W':
            //        return 0x57;
            //    case 'X':
            //        return 0x58;
            //    case 'Y':
            //        return 0x59;
            //    case 'Z':
            //        return 0x5a;
            //    case '0':
            //        return 0x30;
            //    case '1':
            //        return 0x31;
            //    case '2':
            //        return 0x32;
            //    case '3':
            //        return 0x33;
            //    case '4':
            //        return 0x34;
            //    case '5':
            //        return 0x35;
            //    case '6':
            //        return 0x36;
            //    case '7':
            //        return 0x37;
            //    case '8':
            //        return 0x38;
            //    case '9':
            //        return 0x39;
            //    case '.':
            //        return 0x2e;
            //    case '%':
            //        return 0x25;
            //}
            //return 0;
            #endregion test

            return (byte)asciiCharacters.FindIndex(x => x == c);
        }



        /// <summary>
        /// Converts the input byte into a character encoded in ascii
        /// </summary>
        /// <param name="bite">Input byte that is to be converted</param>
        /// <returns>Representation of said byte in character ascii format</returns>
        public static char ToASCII(this byte bite)
        {
            #region test
            //            switch (bite)
            //            {
            //                case 0x: return '';
            //                case 0x: return '';
            //                case 0x: return '';
            //                case 0x: return '';
            //                case 0x: return '';
            //                case 0x: return '';
            //                case 0x: return '';
            //                case 0x: return '';
            //                case 0x: return '';
            //                case 0x: return '';
            //                case 0x: return '';
            //                case 0x: return '';
            //                case 0x: return '';
            //                case 0x: return '';
            //                case 0x: return '';
            //                case 0x: return '';
            //                case 0x: return '';
            //                case 0x: return '';
            //                case 0x: return '';
            //                case 0x: return '';
            //                case 0x: return '';
            //                case 0x: return '';
            //                case 0x: return '';
            //                case 0x: return '';
            //                case 0x: return '';
            //                case 0x: return '';
            //                case 0x: return '';
            //                case 0x: return '';
            //                case 0x: return '';
            //                case 0x: return '';
            //                case 0x: return '';
            //                case 0x: return '';
            //                case 0x: return '';
            //                case 0x: return '';
            //                case 0x: return '';
            //                case 0x: return '';
            //                case 0x: return '';
            //                case 0x: return '';
            //                case 0x: return '';
            //                case 0x: return '';
            //                case 0x: return '';
            //                case 0x: return '';
            //                case 0x: return '';
            //                case 0x: return '';
            //                case 0x: return '';
            //                     	
            // 	

            // !"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\]^_`abcdefghijklmnopqrstuvwxyz{|}~ąƇȉʋ̍ΏБғԕؙ֗ڛܝޟ!£ĥƧȩʫ̭ίбҳԵַعڻܽ޿AÃŅǇɉˋ͍ϏёӓՕחٙۛݝߟᢢ䥥稨ꫫ�౱򳴵��򻼽�
            // !"#$%&'()*+,-./0123456789:;<=>?@[\]^_`abcdefghijklmnopqrstuvwxyz{|}~ąƇȉʋ̍ΏБғԕؙ֗ڛܝޟ!£ĥƧȩʫ̭ίбҳԵַعڻܽ޿AÃŅǇɉˋ͍ϏёӓՕחٙۛݝߟᢢ䥥稨ꫫ�౱򳴵��򻼽�
            //                case 0x41: return 'A';
            //                case 0x42: return 'B';
            //                case 0x43: return 'C';
            //                case 0x44: return 'D';
            //                case 0x45: return 'E';
            //                case 0x46: return 'F';
            //                case 0x47: return 'G';
            //                case 0x48: return 'H';
            //                case 0x49: return 'I';
            //                case 0x4a: return 'J';
            //                case 0x4b: return 'K';
            //                case 0x4c: return 'L';
            //                case 0x4d: return 'M';
            //                case 0x4f: return 'N';
            //                case 0x50: return 'O';
            //                case 0x51: return 'P';
            //                case 0x52: return 'Q';
            //                case 0x53: return 'R';
            //                case 0x54: return 'S';
            //                case 0x55: return 'T';
            //                case 0x56: return 'U';
            //                case 0x57: return 'V';
            //                case 0x58: return 'W';
            //                case 0x59: return 'X';
            //                case 0x5a: return 'Y';
            //                case 0x5b: return 'Z';
            //                case 0x30: return '0';
            //                case 0x31: return '1';
            //                case 0x32: return '2';
            //                case 0x33: return '3';
            //                case 0x34: return '4';
            //                case 0x35: return '5';
            //                case 0x36: return '6';
            //                case 0x37: return '7';
            //                case 0x38: return '8';
            //                case 0x39: return '9';
            //                case 0x2e: return '.';
            //                case 0x25: return '%';
            //            }
            #endregion test
            if (bite > 0x7f)
            {
                return '?';
            }
            return asciiCharacters[bite];
        }



        /// <summary>
        /// Converts the input string into a byte array encoded in ascii
        /// </summary>
        /// <param name="str">Input string that is to be converted</param>
        /// <returns>Representation of said string in byte[] ascii format</returns>
        public static byte[] ToASCII(this string str)
        {
            byte[] eutput = new byte[str.Length];
            int i = 0;
            foreach (char c in str)
            {
                eutput[i] = c.ToASCII();
                i++;
            }
            return eutput;
        }



        /// <summary>
        /// Converts the input byte data into a string encoded in ascii
        /// </summary>
        /// <param name="data">Input data that is to be converted</param>
        /// <returns>Representation of said data in string ascii format</returns>
        public static string ToASCII(this IEnumerable<byte> data)
        {
            StringBuilder eutput = new StringBuilder();
            foreach (byte bite in data)
            {
                eutput.Append(bite.ToASCII());
            }
            return eutput.ToString();
        }
    }
}
