using System.Security.Cryptography;
using System.Text;

namespace WebApi.Services
{
    public class Hash
    {
        public static string Criar(string value)
        {

            string key = "Joao" + value + "Maria";
            MD5 md5Hash = MD5.Create();
            byte[] valorCriptografado = md5Hash.ComputeHash(Encoding.Default.GetBytes(key));
            // Cria um StringBuilder para passarmos os bytes gerados para ele
            StringBuilder strBuilder = new StringBuilder();

            // Converte cada byte em um valor hexadecimal e adiciona ao
            // string builder
            // and format each one as a hexadecimal string.
            for (int i = 0; i < valorCriptografado.Length; i++)
            {
                strBuilder.Append(valorCriptografado[i].ToString("x2"));
            }

            // retorna o valor criptografado como string

            return strBuilder.ToString();
        }




    }
}