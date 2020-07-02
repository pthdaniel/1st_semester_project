using System.IO;

namespace SemesterProject1
{
    static class Kiiratas
    {
        public static void SzovegFajlbaIrat(int A, int B, int C) //Fájlba kiírás, a három feladat egy-egy eredményét.
        {
            StreamWriter ki = new StreamWriter("UZENET.KI.txt");

            ki.WriteLine(A); //A feladat
            ki.WriteLine(B); //B feladat
            ki.WriteLine(C); //C feladat

            ki.Close();
        }
    }
}
