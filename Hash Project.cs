// CSE 566 Project 2 MD5 Birthday Attack ///
#pragma warning disable 
using System.Text;
using System.Linq;

string inp = args[0];  // 1 byte salt//
byte inSalt = Convert.ToByte(inp,16);
byte[] inSaltb = new byte[] {inSalt}; //// Salt in byte array form//
int i; 
string randomstring1;
string resultbytes1;

IDictionary<string,string> valuesHash = new Dictionary<string,string>();
for (i = 0; i <=1000000; i++) // i - number of times to iterate to find collisions//
{
    randomstring1 = Randomstring(10);
    byte[] newb1 = Encoding.UTF8.GetBytes(randomstring1);
    List<byte> list = new List<byte>();
    list.AddRange(newb1);
    list.AddRange(inSaltb);
    byte[] myarrayb;
    myarrayb = list.ToArray();
    resultbytes1 = CreateMD5(myarrayb);
    string rb1 = resultbytes1.Substring(0,2);
    valuesHash.Add(randomstring1, rb1);
}

var lookup = valuesHash.GroupBy(x => x.Value).Where(x => x.Count() > 1);
string[] RESULT;
i = 0;
foreach(var item in lookup)
{   
    if (i == 0)
    {
        var keys = item.Aggregate("", (s, v)=> s+","+v);
        var message = keys;
        RESULT = message.Split(",");
        string[] message1 = RESULT.ToArray();
        string res1 = message1[1];
        res1 = res1.Remove(0,1);
        string res2 = message1[3];
        res2 = res2.Remove(0,1);
        Console.WriteLine(res1+","+res2);
    }
    i++;
}

static string Randomstring(int length)
{
    Random random = new Random();
    const string pool = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
    var chars = Enumerable.Range(0, length).Select(x => pool[random.Next(0, pool.Length)]);
    return new string(chars.ToArray());
    
}

static string CreateMD5(byte[] inputBytes)
{
    using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
    {
        byte[] hashBytes = md5.ComputeHash(inputBytes);
        return Convert.ToHexString(hashBytes);
    }
}

