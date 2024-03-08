using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DD.DBL
{
    public class Helpers
    {
        //public static bool ValidBase64String(string base64String)
        //{
        //    if (string.IsNullOrEmpty(base64String))
        //    {
        //        return false;
        //    }
        //    Uri outUri;
        //    if (Uri.TryCreate(base64String, UriKind.Absolute, out outUri)
        //       && (outUri.Scheme == Uri.UriSchemeHttp || outUri.Scheme == Uri.UriSchemeHttps))
        //    {
        //        return true;
        //    }
        //    if (base64String.Split(";").Count() != 2)
        //    {
        //        return false;
        //    }
        //    if (base64String.Split(",").Count() != 2)
        //    {
        //        return false;
        //    }
        //    return true;
        //}
        public static string GetReferenceNumber()
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(Guid.NewGuid().ToString()));
        }

        public static bool IsValidName(string name)
        {
            return Regex.IsMatch(name, @"^[a-zA-Z@$!%*?&#()=+_'""{}[;:<>/?|., ^-]+$", RegexOptions.IgnoreCase);
        }

        public static bool IsValidEmail(string emailId)
        {
            return Regex.IsMatch(emailId, @"^(?=[a-zA-Z0-9])(?!.*[\.+\-_]{2})([a-zA-Z0-9_+.-]+)@([a-zA-Z0-9_.]+)\.([a-zA-Z]{2,5})(?!\.)$", RegexOptions.IgnoreCase);
        }
        public static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }
        //public static bool IsValidFont(string fontName)
        //{
        //    try
        //    {
        //        // Create a FontFamily object using the font name
        //        FontFamily fontFamily = new FontFamily(fontName);

        //        // Check if the font is installed
        //        bool isInstalled = fontFamily.IsStyleAvailable(FontStyle.Regular);

        //        return isInstalled;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}
        public static bool IsValidUsPhoneNumber(string phoneNumber)
        {
            return Regex.IsMatch(phoneNumber, @"^(1\s?)?((\([0-9]{3}\))|[0-9]{3})[\s\-]?[\0-9]{3}[\s\-]?[0-9]{4}$");
        }

        public static bool IsAlphNumericStartswithChar(string text, int min = 0, int max = 0)
        {
            if (!Char.IsLetter(text[0]))
            {
                return false;
            }
            else if (text.Length < min && min != 0)
            {
                return false;
            }
            else if (text.Length > max && max != 0)
            {
                return false;
            }
            return Regex.IsMatch(text, @"^(?=.*[0-9])(?=.*[a-zA-Z])([a-zA-Z0-9]+)$");
        }

        public static bool IsAlphaOrNumericStartswithChar(string text, int min = 0, int max = 0)
        {
            if (!Char.IsLetter(text[0]))
            {
                return false;
            }
            else if (text.Length < min && min != 0)
            {
                return false;
            }
            else if (text.Length > max && max != 0)
            {
                return false;
            }
            return Regex.IsMatch(text, @"^[\d \w \s]+$");
        }
        public static bool IsNumeric(string text)
        {
            return Regex.IsMatch(text, @"^(?=.*[0-9])([0-9]+)$");
        }

        public static bool IsValidPassword(string password)
        {
            return Regex.IsMatch(password, @"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[@$!%*?&#()=+_{}[;:<>/?|.,^-]).{8,}$");
        }

        public static bool IsCharacterRepeating(string password)
        {
            return Regex.IsMatch(password, "([a-zA-Z0-9@$!%*?&#=+_{};:<>/?|[.,^-])\\1{" + (2) + "}");
        }

        public static bool IsSequentialNumber(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return false;
            }

            if (str.Length < 2)
            {
                return false;
            }

            for (int i = 0; i < str.Length - 2; i++)
            {
                if (str[i] >= '0' && str[i] <= '9' &&
                    str[i + 1] >= '0' && str[i + 1] <= '9' &&
                    str[i + 2] >= '0' && str[i + 2] <= '9' &&
                    ((str[i + 1] == str[i] + 1 && str[i + 2] == str[i] + 2) || (str[i + 1] == str[i] - 1 && str[i + 2] == str[i] - 2)))
                {
                    return true;
                }
            }

            return false;
        }

        //public static bool ValidateStartAndEndDate(DateTime? startDatetime, DateTime? endDatetime)
        //{
        //    bool flag = true;
        //    if (endDatetime.ToHermesDate() < startDatetime.ToHermesDate())
        //    {
        //        flag = false;
        //    }

        //    return flag;
        //}

        public static string GetCurrentDate()
        {
            return DateTime.UtcNow.Year + "-" + DateTime.UtcNow.Month + "-" + DateTime.UtcNow.Day + " " + DateTime.UtcNow.Hour + ":" + DateTime.UtcNow.Minute + ":" + DateTime.UtcNow.Second;
        }

        public static string GetDate()
        {
            return DateTime.UtcNow.ToString("MM/dd/yyyy HH:mm:ss.fff tt");
        }

        public static string GetDateFormat(string date)
        {
            if (string.IsNullOrWhiteSpace(date))
            {
                DateTime dt = DateTime.MinValue;
                date = dt.Year + "-" + dt.Month + "-" + dt.Day + " " + dt.Hour + ":" + dt.Minute + ":" + dt.Second;
            }

            return date;
        }

        public static DateTime GetDateTimeByTimeZone(string timeZone, DateTime dateTime)
        {
            if (string.IsNullOrEmpty(timeZone))
            {
                timeZone = Constants.EasternTimeZone;
            }

            var timeToConvert = dateTime;
            TimeZoneInfo timeZoneToConvert = TimeZoneInfo.FindSystemTimeZoneById(timeZone);
            DateTime convertedDateTime = TimeZoneInfo.ConvertTimeFromUtc(timeToConvert, timeZoneToConvert);
            return convertedDateTime;
        }

        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        public static string GeneratePassword(string rawPassword)
        {
            string cipherText = Encrypt(rawPassword);
            return cipherText;
        }
        public static string EncryptString(string key, string plainText)
        {
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(plainText);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array);
        }

        public static string DecryptString(string key, string cipherText)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(cipherText);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }
        //public static string EncryptStringAES(string plainText)
        //{
        //    byte[] key = new byte[] { 0x01, 0x23, 0x45, 0x67, 0x89, 0xAB, 0xCD, 0xEF, 0xFE, 0xDC, 0xBA, 0x98, 0x76, 0x54, 0x32, 0x10 };
        //    byte[] iv = new byte[] { 0xFE, 0xDC, 0xBA, 0x98, 0x76, 0x54, 0x32, 0x10, 0x01, 0x23, 0x45, 0x67, 0x89, 0xAB, 0xCD, 0xEF };

        //    // Check arguments.
        //    if (plainText == null || plainText.Length <= 0)
        //        throw new ArgumentNullException("plainText");
        //    if (key == null || key.Length <= 0)
        //        throw new ArgumentNullException("key");
        //    if (iv == null || iv.Length <= 0)
        //        throw new ArgumentNullException("iv");

        //    byte[] encrypted;

        //    // Create an AES object with the specified key and IV.
        //    using (Aes aesAlg = Aes.Create())
        //    {
        //        aesAlg.Key = key;
        //        aesAlg.IV = iv;

        //        // Create an encryptor to perform the stream transform.
        //        ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

        //        // Create the streams used for encryption.
        //        using (MemoryStream msEncrypt = new MemoryStream())
        //        {
        //            using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
        //            {
        //                using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
        //                {
        //                    // Write all data to the stream.
        //                    swEncrypt.Write(plainText);
        //                }
        //                encrypted = msEncrypt.ToArray();
        //            }
        //        }
        //    }

        //    // Convert the encrypted data to a base-10 string.
        //    string base10String = Convert.ToBase64String(encrypted).Replace("/", "").Replace("+", "");
        //    string numericString = "";
        //    for (int i = 0; i < 10; i++)
        //    {
        //        int index = i * 2;
        //        string twoDigitString = base10String.Substring(index, 2);
        //        int value = int.Parse(twoDigitString, System.Globalization.NumberStyles.HexNumber);
        //        numericString += value.ToString();
        //    }

        //    // Return the numeric string.
        //    return numericString;
        //}
        //public static string DecryptStringAES(string cipherText)
        //{
        //    byte[] key = new byte[] { 0x01, 0x23, 0x45, 0x67, 0x89, 0xAB, 0xCD, 0xEF, 0xFE, 0xDC, 0xBA, 0x98, 0x76, 0x54, 0x32, 0x10 };
        //    byte[] iv = new byte[] { 0xFE, 0xDC, 0xBA, 0x98, 0x76, 0x54, 0x32, 0x10, 0x01, 0x23, 0x45, 0x67, 0x89, 0xAB, 0xCD, 0xEF };

        //    // Check arguments.
        //    if (cipherText == null || cipherText.Length <= 0)
        //        throw new ArgumentNullException("cipherText");
        //    if (key == null || key.Length <= 0)
        //        throw new ArgumentNullException("key");
        //    if (iv == null || iv.Length <= 0)
        //        throw new ArgumentNullException("iv");

        //    byte[] encrypted = new byte[20];
        //    for (int i = 0; i < 10; i++)
        //    {
        //        int index = i * 2;
        //        string twoDigitString = cipherText.Substring(index, 2);
        //        int value = int.Parse(twoDigitString);
        //        encrypted[i] = (byte)value;
        //    }

        //    // Convert the encrypted string to bytes.
        //    string base10String = Convert.ToBase64String(encrypted);
        //    base10String = base10String.PadRight(base10String.Length + (4 - base10String.Length % 4) % 4, '=');
        //    byte[] cipherBytes = Convert.FromBase64String(base10String);

        //    // Create an AES object with the specified key and IV.
        //    using (Aes aesAlg = Aes.Create())
        //    {
        //        aesAlg.Key = key;
        //        aesAlg.IV = iv;

        //        // Create a decryptor to perform the stream transform.
        //        ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

        //        // Create the streams used for decryption.
        //        using (MemoryStream msDecrypt = new MemoryStream(cipherBytes))
        //        {
        //            using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
        //            {
        //                using (StreamReader srDecrypt = new StreamReader(csDecrypt))
        //                {
        //                    // Read the decrypted bytes from the decrypting stream
        //                    // and place them in a string.
        //                    string plaintext = srDecrypt.ReadToEnd();
        //                    return plaintext;
        //                }
        //            }
        //        }
        //    }
        //}
        public static string Encrypt(string plainText)
        {
            string key = GetKey();
            string cipherText;

            var aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(key);
            aes.Mode = CipherMode.ECB;
            aes.BlockSize = 128;
            aes.Padding = PaddingMode.Zeros;

            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, null);

            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    using (var streamWriter = new StreamWriter(cryptoStream))
                    {
                        streamWriter.Write(plainText);
                        streamWriter.Flush();
                    }
                }
                cipherText = Convert.ToBase64String(memoryStream.ToArray());
            }

            return cipherText;
        }

        public static string GetKey()
        {
            return BitConverter.ToString(MD5.Create().ComputeHash(Encoding.ASCII.GetBytes("IKr0Qt1iimPvsOoHW9IRi14rM9p97Tj8nT7QsjnItHOxmJmRqKHfqvJdFyHocic"))).Replace("-", string.Empty).ToLower();
        }

        public static string GenerateActivationCode(int numberOfDigit)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNPQRSTUVWXYZ123456789";
            return new string(Enumerable.Repeat(chars, numberOfDigit)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static string GenerateActivationCodeNumbers(int numberOfDigit)
        {
            Random random = new Random();
            const string chars = "1234567890";
            return new string(Enumerable.Repeat(chars, numberOfDigit)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static string GenerateTemporaryPassword(int legnth)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, legnth)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static decimal? DecimalDataCompare(decimal? requestData, decimal? dbData)
        {
            if (requestData != dbData)
            {
                return requestData;
            }
            else
            {
                return dbData;
            }
        }

        public static int? IntDataCompare(int? requestData, int? dbData)
        {
            if (requestData != dbData)
            {
                return requestData;
            }
            else
            {
                return dbData;
            }
        }

        public static bool BoolDataCompare(bool requestData, bool dbData)
        {
            if (requestData != dbData)
            {
                return requestData;
            }
            else
            {
                return dbData;
            }
        }

        public static bool? BoolDataCompare(bool? requestData, bool? dbData)
        {
            if (requestData != dbData)
            {
                return requestData;
            }
            else
            {
                return dbData;
            }
        }

        public static string StringDataCompare(string requestData, string dbData)
        {
            if (!string.Equals(requestData, dbData))
            {
                return requestData;
            }
            else
            {
                return dbData;
            }
        }

        public static int GetTotalWeeksBetweenTwoDates(DateTime startDate, DateTime endDate)
        {
            return Convert.ToInt32((endDate - startDate).TotalDays / 7);
        }

        public static string GenerateHashCode(string fileName)
        {
            string[] words = fileName.Split('.');
            Guid g = Guid.NewGuid();
            string name = g.ToString().Replace("-", string.Empty);

            // int unixTimestamp = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            using (MD5 md5Hash = MD5.Create())
            {
                string hash = GetMd5Hash(md5Hash, name);
                string hashCode = hash + "." + words[words.Length - 1];
                return hashCode;
            }
        }

        public static string GetMd5Hash(MD5 md5Hash, string input)
        {
            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        //private static DateTime ToHermesDate(this DateTime? dateTime)
        //{
        //    dateTime = dateTime == null ? DateTime.Now : dateTime;
        //    DateTime today = dateTime.Value.ToUniversalTime();
        //    return dateTime.Value.ToUniversalTime();
        //}

        public static double ConvertToUnixTimestamp(DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan diff = date - origin;
            return Math.Floor(diff.TotalSeconds);
        }

        public static DateTime ConvertUnixTimestampToDate(double time)
        {
            DateTimeOffset dateTimeOffSet = DateTimeOffset.FromUnixTimeSeconds((long)time);
            DateTime datTime = dateTimeOffSet.DateTime;
            return datTime;
        }

        public static DateTime ConvertUnixTimestampToDateTime(long time)
        {
            DateTimeOffset dateTimeOffSet = DateTimeOffset.FromUnixTimeMilliseconds(time);
            DateTime datTime = dateTimeOffSet.DateTime;
            return datTime;
        }
    }
}
