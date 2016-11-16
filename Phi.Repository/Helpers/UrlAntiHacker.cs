/*
 * COPYRIGHT 2014-2016 Anton Yarkov
 * 
 * Email: anton.yarkov@gmail.com
 * Skype: optiklab
*/

namespace Phi.Repository.Helpers
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// Works with unique tokens during User registration process: generates tokens by emails and verifies it.
    /// </summary>
    public class UrlAntiHacker
    {
        #region Private fields

        /// <summary>
        /// The _key
        /// </summary>
        private static readonly Byte[] _key = { 11, 254, 45, 192, 54, 128, 25, 10 };

        #endregion

        #region Public properties

        public enum HMACResult
        {
            OK,
            Expired,
            Invalid
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Generates token.
        /// </summary>
        public static string GetExpiringToken(string parameter, DateTime expiryDate)
        {
            HMAC alg = new HMACSHA1(_key);
            try
            {
                string input = expiryDate.Ticks + parameter;
                Byte[] hashBytes = alg.ComputeHash(Encoding.UTF8.GetBytes(input));
                Byte[] result = new Byte[8 + hashBytes.Length];
                hashBytes.CopyTo(result, 8);
                BitConverter.GetBytes(expiryDate.Ticks).CopyTo(result, 0);

                return _Swap(Convert.ToBase64String(result), "+=/", "-_,");
            }
            finally
            {
                alg.Clear();
            }
        }

        /// <summary>
        /// Verifies token by e-mail and date.
        /// </summary>
        public static HMACResult Verify(string parameter, string expiringToken)
        {
            Byte[] bytes = Convert.FromBase64String(_Swap(expiringToken, "-_,", "+=/"));
            DateTime claimedExpiry = new DateTime(BitConverter.ToInt64(bytes, 0));

            if (claimedExpiry < DateTime.Now)
            {
                return HMACResult.Expired;
            }

            if (expiringToken == GetExpiringToken(parameter, claimedExpiry))
            {
                return HMACResult.OK;
            }

            return HMACResult.Expired;
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Replaces inputs to outputs in str.
        /// </summary>
        private static string _Swap(string str, string input, string output)
        {
            for (Int32 i = 0; i < input.Length; i++)
            {
                str = str.Replace(input[i], output[i]);
            }

            return str;
        }

        #endregion
    }
}
