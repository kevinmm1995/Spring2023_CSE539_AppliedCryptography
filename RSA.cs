//CSE 539 RSA Project //
#pragma warning disable
using System;
using System.Security.Cryptography;
using System.Numerics;
using System.Text;
string p_e = args[0];
string p_c = args[1];
string q_e = args[2];
string q_c = args[3];
string cipherText = args[4];
string plainText = args[5];

//int cipherT = Int32.Parse(cipherText);  
BigInteger cipher_ = BigInteger.Parse(cipherText); 

//int plainT = Int32.Parse(plainText);  
BigInteger plain_ = BigInteger.Parse(plainText); 

int bigP_e = Int32.Parse(p_e);  
BigInteger bigP_c = BigInteger.Parse(p_c); 
BigInteger bigP = BigInteger.Pow(2,bigP_e);
bigP = bigP - bigP_c;  // p

int bigq_e = Int32.Parse(q_e); 
BigInteger bigq_c = BigInteger.Parse(q_c); 
BigInteger bigq = BigInteger.Pow(2,bigq_e);
bigq = bigq - bigq_c;  // q

BigInteger e = 65537;
BigInteger N = bigP * bigq;
BigInteger phiN = (bigP - 1) * (bigq - 1);



BigInteger d;
BigInteger d2;
BigInteger FinalD;
FinalD = 0;
for (int i = 0;i<1000000;i++)
{   d = ( (phiN * i) + 1) / e ;
    d2 = (e * d) % phiN;
    if (d2 == 1)
    {
        FinalD = d;
    }
}

BigInteger Decrypted;
//Decrypted =( (cipher_ ^ FinalD) ) % N;
Decrypted = BigInteger.ModPow(cipher_, FinalD,N);


BigInteger Encrypted;
Encrypted = BigInteger.ModPow(plain_, e,N);
Console.WriteLine(Decrypted+","+Encrypted);
// BigInteger Encrpyted;
// Encrypted = (plain_ ^ e) % N;