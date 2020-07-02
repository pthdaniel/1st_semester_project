namespace SemesterProject1
{
    class Emberek
    {
        string nev; //Adattag a névhez.
        string[] kiknekKuldi; //Adattag a továbbküldött nevekről.

        public string Nev //Tulajdonság a névhez.
        {
            get { return nev; }
        }

        public string[] KiknekKuldi //Tulajdonság a továbbításhoz.
        {
            get { return kiknekKuldi; }
        }

        public Emberek(string nev) //Konstruktor.
        {
            this.nev = nev;
        }

        public void KiknekKuldiTovabb(string kiknek) //Metódus a példányhoz tartozó nevek Splitteléséhez és elmentéséhez.
        {
            string[] seged = kiknek.Split(',');

            kiknekKuldi = new string[seged.Length];
            kiknekKuldi = seged;
        }
    }
}
