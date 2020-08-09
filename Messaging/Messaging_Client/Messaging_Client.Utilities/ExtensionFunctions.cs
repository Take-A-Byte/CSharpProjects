namespace Messaging_Client.Utilities
{
    using System;

    public static class ExtensionFunctions
    {
        #region Public Methods

        /// <summary>
        /// returns a sub array of the array
        /// </summary>
        /// <typeparam name="T">type of data saved in array</typeparam>
        /// <param name="data">current array</param>
        /// <param name="index">starting index from which copying begins</param>
        /// <param name="length">length of array to copy. If nothing is supplied, array is copied till the end.</param>
        /// <returns></returns>
        public static T[] SubArray<T>(this T[] data, int index, int length = -1)
        {
            if (length == -1)
            {
                length = data.Length - index;
            }

            T[] result = new T[length];
            Array.Copy(data, index, result, 0, length);
            return result;
        }

        #endregion Public Methods
    }
}