using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;
//Pietro MAlzone 4H 02/10/2024
namespace OTP_ConsoleApp
{
    class SymmetricCrypt
    {
        
        private byte[] key;
        public SymmetricCrypt(string key)
        {
            this.key = System.Text.Encoding.UTF8.GetBytes(key);
        }
        public SymmetricCrypt(byte[] data)
        {
            this.key = data;
        }
        static byte[] getBytes(string msg)
        {
            byte[] msg_v = System.Text.Encoding.UTF8.GetBytes(msg);
            return msg_v;
        }
        static string getString(byte[] msg)
        {
            string msg_v = System.Text.Encoding.UTF8.GetString(msg);
            return msg_v;
        }
        static byte[] Encript(byte[] data, byte[] key)
        {
            byte[] m = data;
            byte[] k = key;
            byte[] MSG_vect = new byte[m.Length];
            for (int i = 0; i < m.Length; i++)
            {
                 MSG_vect[i] = (byte)((int)m[i] ^ (int)key[i % key.Length]);
            }

            return MSG_vect;
        }
        public static byte[] Crypt(byte[] data, byte[] key) { return Encript(data, key); }
        
        public static string Crypt(string msg,string key) { return getString(Encript(getBytes(msg),getBytes(key))); }
        
        

        public byte[] Crypt(byte[] data) { return Encript(data, key);}
        
        public string Crypt (string msg) { return getString(Encript(getBytes(msg), key)); }
        
    }
    internal class Program
    {
       
       

        static void Main(string[] args)
        {
            string msg_str = "Il Ping of Death è un tipo di attacco Denial of Service che consiste nell'invio di un pacchetto IP malformato";
            string key_str = "Silvio Berlusconi è stato un imprenditore e politico italiano, fondatore del gruppo Fininvest e quattro volte Presidente del Consiglio dei ministri della Repubblica Italiana.";

            SymmetricCrypt.Crypt(msg_str, key_str);



        }
    }
}
