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

namespace AR5001D
{
    /// <summary>
    /// Provides helper methods for the AR5001D
    /// </summary>
    public static class Helpers
    {
        #region Methods

        /// <summary>
        /// Cuts the collection to the specified size
        /// </summary>
        /// <typeparam name="T">Type of objects in the collecting</typeparam>
        /// <param name="collection">The collection which is going to be cut</param>
        /// <param name="start">Starting index of cut</param>
        /// <param name="end">Exclusive end of cut</param>
        /// <returns></returns>
        public static IEnumerable<T> Cut<T>(this IEnumerable<T> collection, int start, int end)
        {
            int i = 0;
            List<T> holder = new List<T>();
            foreach (T item in collection)
            {
                if (i == end) return holder;
                if (i >= start)
                {
                    holder.Add(item);
                }
                i++;
            }
            return holder.ToArray();
        }


        /// <summary>
        /// Prints a collection
        /// </summary>
        /// <typeparam name="T">Type of item stored in the collection</typeparam>
        /// <param name="collection">The collection that is to be printed</param>
        /// <returns>A pretty representation of said collection in string format. (csv)</returns>
        public static string PrettyPrint<T>(this IEnumerable<T> collection)
        {
            StringBuilder strBldr = new StringBuilder();
            int i = collection.Count();
            foreach (var item in collection)
            {
                i--;
                strBldr.Append(item.ToString());
                if (i > 0) strBldr.Append(", ");
            }
            return strBldr.ToString();
        }


        /// <summary>
        /// Checks to see if the device responded with "Valid Command"
        /// </summary>
        /// <param name="data">Responsum</param>
        /// <returns>True if valid command, false otherwise</returns>
        internal static bool isOk(this byte[] data)
        {
            if (data[data.Length - 3] == 32) return true;
            return false;
        }



        public static double Direction(byte[] data)
        {
            short x = (short)(parseX(data) - 2048);
            short y = (short)(parseY(data) - 2048);
            return (Math.Atan2(y, x) * 180) % 360;
        }

        private static short parseX(byte[] data)
        {
            return short.Parse(data.Cut(1, 5).ToASCII().TrimStart('0'));
        }
        private static short parseY(byte[] data)
        {
            return short.Parse(data.Cut(5, 9).ToASCII().TrimStart('0'));
        }
        #endregion Methods
    }
}
