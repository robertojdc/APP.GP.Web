using System.Security.Cryptography;
using System.Text;

namespace APP.GP.Web.Helper
{
    public class Encriptacion
    {
        private static string KeyEncriptacion()
        {
            return "LlaveMD5Gpu*&l=e";
        }

        internal static string Encriptar(string texto)
        {
            string txtEncriptado = string.Empty;
            try
            {


                byte[] keyArray;

                byte[] arreglo_a_Cifrar = UTF8Encoding.UTF8.GetBytes(texto);


                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();

                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(KeyEncriptacion()));

                hashmd5.Clear();

                //Algoritmo TripleDES
                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider()
                {
                    Key = keyArray,
                    Mode = CipherMode.ECB,
                    Padding = PaddingMode.PKCS7
                };
                ICryptoTransform cTransform = tdes.CreateEncryptor();

                byte[] ArrayResultado = cTransform.TransformFinalBlock(arreglo_a_Cifrar, 0, arreglo_a_Cifrar.Length);

                tdes.Clear();

                //se regresa el resultado en forma de una cadena
                txtEncriptado = Convert.ToBase64String(ArrayResultado, 0, ArrayResultado.Length);


            }
            catch (Exception)
            {

            }
            return txtEncriptado;
        }

        internal static string Desencriptar(string textoEncriptado)
        {
            string txtTransform = string.Empty;
            try
            {
                byte[] keyArray;
                byte[] array_a_Descifrar = Convert.FromBase64String(textoEncriptado);

                //algoritmo MD5
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();

                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(KeyEncriptacion()));

                hashmd5.Clear();

                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider()
                {
                    Key = keyArray,
                    Mode = CipherMode.ECB,
                    Padding = PaddingMode.PKCS7
                };

                ICryptoTransform cTransform = tdes.CreateDecryptor();

                byte[] resultArray = cTransform.TransformFinalBlock(array_a_Descifrar, 0, array_a_Descifrar.Length);

                tdes.Clear();
                txtTransform = UTF8Encoding.UTF8.GetString(resultArray);

                if (string.IsNullOrEmpty(txtTransform))
                    throw new ArgumentException("La cadena encriptada no es correcta.");
            }
            catch (Exception)
            {
                throw new ArgumentException("La cadena encriptada no es correcta.");
            }
            return txtTransform;
        }
    }
}
