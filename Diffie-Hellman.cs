// CSE 539 Diffie Hellman Project///
#pragma warning disable
using System.Security.Cryptography;
using System.Numerics;
using System.Text;
string hexIV = args[0];
string g_e = args[1];
string g_c = args[2];
string N_e = args[3];
string N_c = args[4];
string xin = args[5];
int x = Int32.Parse(xin);
string gymodN = args[6];
string cipherBytes = args[7];
string[] IVwords = hexIV.Split(' ');
string[] cipherwords = cipherBytes.Split(' ');
string plaint = args[8];
List<byte> IVlist = new List<byte>();
List<byte> cipherlist = new List<byte>();

BigInteger bigGymodN = BigInteger.Parse(gymodN); // gymodN// 
int bigN_e = Int32.Parse(N_e); 
BigInteger bigN_c = BigInteger.Parse(N_c); 
BigInteger bigN = BigInteger.Pow(2,bigN_e);
bigN = bigN - bigN_c; // N

BigInteger Key = BigInteger.ModPow(bigGymodN,x,bigN); // Key - gxymodN//
byte[] KeyBytes = Key.ToByteArray();

foreach (var word in IVwords)
{
    byte newb = Convert.ToByte(word,16);
    byte[] result1 = new byte[] {newb};
    IVlist.AddRange(result1);
}
byte[] IVbytes = IVlist.ToArray();  // IV for encryption/decryption//

foreach (var word in cipherwords) // creating list from cipher//
{
    byte newb = Convert.ToByte(word,16);
    byte[] result1 = new byte[] {newb};
    cipherlist.AddRange(result1);
}
byte[] cipherbytes = cipherlist.ToArray(); // converting list to bytes//
string plaintext; 
plaintext = Decrypt(cipherbytes, KeyBytes, IVbytes);
byte[] encryResults;
encryResults = Encrypt(plaint, KeyBytes, IVbytes);
Console.WriteLine(plaintext+","+BitConverter.ToString(encryResults).Replace("-"," "));


static string Decrypt(byte[] cipherText,byte[] Key,byte[] IV) {
    string plaintext = null;

    using(AesManaged aes = new AesManaged()) {
        ICryptoTransform decryptor = aes.CreateDecryptor(Key, IV);
        using(MemoryStream ms = new MemoryStream(cipherText)) {
            using(CryptoStream cs = new CryptoStream(ms,decryptor,CryptoStreamMode.Read)) {
                using(StreamReader reader = new StreamReader(cs))
                plaintext = reader.ReadToEnd();
            }
        }
    }
    return plaintext;
}

static byte[] Encrypt(String plaintext, byte[] Key, byte[] IV) {
    byte [] encrypted;
    using(AesManaged aes = new AesManaged()) {
        ICryptoTransform encryptor = aes.CreateEncryptor(Key, IV);
        using(MemoryStream ms = new MemoryStream()) {
            using(CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write)) {
                using(StreamWriter sw = new StreamWriter(cs))
                sw.Write(plaintext);
                encrypted = ms.ToArray();
            }
        }
    }
    return encrypted;
}

