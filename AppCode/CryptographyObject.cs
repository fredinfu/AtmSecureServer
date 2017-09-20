using System; 
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FtpServerUI.AppCode
{
    public class CryptographyObject
    {
        private string unlockKey;
        private const string APP_KEY = "25062017";
        public CryptographyObject(string unlockKey)
        {
            this.unlockKey = unlockKey;
        }

        public string Md5Gen()
        {
            var md5 = new MD5CryptoServiceProvider();
            unlockKey += APP_KEY;
            var md5String = string.Empty;
            var bytes = Encoding.GetEncoding("Windows-1252").GetBytes(unlockKey);
            bytes = md5.ComputeHash(bytes);
            foreach (var byteChar in bytes)
            {
                md5String += byteChar.ToString("X2").ToUpper();
            }

            return md5String;
        }

        public string stringToMd5(string word)
        {
            var md5 = new MD5CryptoServiceProvider();

            var md5String = string.Empty;
            var bytes = Encoding.GetEncoding("Windows-1252").GetBytes(word);
            bytes = md5.ComputeHash(bytes);
            foreach (var byteChar in bytes)
            {
                md5String += byteChar.ToString("X2").ToUpper();
            }

            return md5String;
        }

        public string Encriptar(string texto)
        {
            try
            {
                string key = "fredinFunez"; //llave para encriptar datos
                byte[] keyArray;
                byte[] Arreglo_a_Cifrar = UTF8Encoding.UTF8.GetBytes(texto);
                //Se utilizan las clases de encriptación MD5
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                hashmd5.Clear();
                //Algoritmo TripleDES
                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
                tdes.Key = keyArray;
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;
                ICryptoTransform cTransform = tdes.CreateEncryptor();
                byte[] ArrayResultado = cTransform.TransformFinalBlock(Arreglo_a_Cifrar, 0, Arreglo_a_Cifrar.Length);
                tdes.Clear();
                //se regresa el resultado en forma de una cadena
                texto = Convert.ToBase64String(ArrayResultado, 0, ArrayResultado.Length);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return texto;
        }
        public string Desencriptar(string textoEncriptado)
        {
            try
            {
                string key = "fredinFunez";
                byte[] keyArray;
                byte[] Array_a_Descifrar = Convert.FromBase64String(textoEncriptado);
                //algoritmo MD5
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                hashmd5.Clear();
                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
                tdes.Key = keyArray;
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;
                ICryptoTransform cTransform = tdes.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(Array_a_Descifrar, 0, Array_a_Descifrar.Length);
                tdes.Clear();
                textoEncriptado = UTF8Encoding.UTF8.GetString(resultArray);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return textoEncriptado;
        }
    }
}
