using System;
using System.IO;

namespace SemesterProject1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("A szabályzat a CIMD2L_FF/Uzenetek/bin/Debug mappában, az UZENET.BE szövegfájl első sorában módosítható. A végét '#' jelöli.");

            Console.WriteLine();

            Emberek[] szabalyzatNevek = Peldanyosit(Beolvas()); //Példányosítom a beolvasottakat.

            //for (int i = 0; i < szabalyzatNevek.Length; i++)
            //{
            //    if (szabalyzatNevek[i] != null)
            //    {
            //        Console.WriteLine(szabalyzatNevek[i].Nev);
            //        Console.Write("Kinek küldi tovább: ");

            //        if (szabalyzatNevek[i].KiknekKuldi == null)
            //        {
            //            Console.WriteLine("Senkinek.");
            //        }

            //        else
            //        {
            //            for (int j = 0; j < szabalyzatNevek[i].KiknekKuldi.Length; j++)
            //            {
            //                Console.Write(szabalyzatNevek[i].KiknekKuldi[j] + " ");
            //            }
            //            Console.WriteLine();
            //        }

            //        Console.WriteLine();
            //    }
            //}


            //for (int i = 0; i < szabalyzatNevek.Length; i++)
            //{
            //    Console.WriteLine(szabalyzatNevek[i].Nev);
            //}


            Kiiratas.SzovegFajlbaIrat(MaxKozvetlenul(szabalyzatNevek), UzenetUt(szabalyzatNevek), NemKell(szabalyzatNevek)); //A, B, C feladat kiíratása fájlba.

            Console.WriteLine("Az eredmények az UZENET.KI szövegfájlban találhatók (első sor: A feladat, második sor: B feladat, harmadik sor: C feladat).");

            Console.ReadLine();
        }

        static Emberek[] Peldanyosit(string elsoSor) //Példányosítás, az Emberek létrehozása.
        {
            Emberek[] emberek = new Emberek[1000]; //Mivel max 1000 ember neve lehet a szabályzatban.
            int emberekIdx = 0; //Kell egy indexelő példányosításnál.

            string[] szabalyzat = elsoSor.Split('#'); //Mivel a # zárja le a szabályzatot, ezért az kell, ami tőle balra van. Pl: Miklos(Csaba,Bela)#Geza(Timi) - A Geza(Timi) nem része a szabályzatnak, ezért azt nem kell példányosítnai. 

            string nev = ""; // segdéváltozó

            for (int i = 0; i < szabalyzat[0].Length; i++) //Bejárja a szabályzatot karakterenként.
            {
                if (szabalyzat[0][i] != '(' && szabalyzat[0][i] != ')' && szabalyzat[0][i] != ',') //Ha betű, akkor bemásolja a segédbe. A végén egy nevet fogunk kapni.
                {

                    if (szabalyzat[0][i] != ' ') //Ha véletlen valaki üt egy spacet a szabályzatba, az ne kerüljön be.
                    {
                        nev += szabalyzat[0][i]; //Hozzáfűzi a betűket.
                    }
                }

                else // Ha talál egy nem betűt,...
                {
                    if (nev.Length > 1) // ...és az addig felépített segédváltozónk hossza nagyobb egynél, (tehát biztosan név),...
                    {
                        emberek[emberekIdx++] = new Emberek(nev); // ...akkor példányosítja és az új példány neve az addig elmentett segédváltozó lesz.
                        nev = ""; // A segédváltozót visszaállítja.
                    }
                }
            }


            int nyitokDb = 0; // Segédváltozók, számolják a zárójeleket.
            int zarokDb = 0;

            for (int i = 0; i < szabalyzat[0].Length; i++) // Bejárjuk újra a szabályzatot karakterenként.
            {
                if (szabalyzat[0][i] == '(') // Az ELSE ág ismeretében érthetőbb.
                {
                    nyitokDb++;

                    int j = i + 1; //Azért kell, mert a '(' -től kezdve vizsgáljuk a karaktereket.
                    string kiknek = ""; //Segédváltozó.

                    while (nyitokDb != zarokDb) //Ha egyenlő akkor végig ért azon a zárójeles részen, amibe belépett, hiszen minden nyitóhoz tartozik egy záró. Pl: Miklos(Gabor(Balazs,Denes),Barbi) - Látható, ha a Miklos utáni zárójeles részt vizsgáljuk akkor ér végig, ha a nyitók száma megegyezik a zárók számával. Gabornál ugyan ez.
                    {
                        if (szabalyzat[0][j] != '(' && szabalyzat[0][j] != ')' && (nyitokDb - zarokDb < 2)) //Amíg betű vagy vessző(később splitteljük) ÉS a nyitójelek és zárójelek különbsége 1 (hogy csak 1 zárójelezést nézzen) addig hozzáfűzi a karaktereket a segédhez.
                        {
                            kiknek += szabalyzat[0][j]; // Zárójelkülönbség magyarázat: Most csak a már megtalált névhez tartozó KÖZVETLEN neveket keressük tehát, ha útközben van egy zárójel azt figyelmen kívül szeretnénk hagyni. Pl: Miklos(Gabor(Balazs,Denes),Barbi) - Miklos után találunk egy nyitójelet (nyitokSzama = 1, nyitokSzama - zarokSzama = 1), folytatódik a hozzáfűzés,
                        }                                 // ...majd ha Miklos(Gabor(Balazs,Denes),Barbi) - Gabor beolvasása megtörtént következik egy nyitójel. Igen ám, de ami benne van az nekünk nem kell. (nyitokSzama = 2, nyitokSzama - zarokSzama = 2 ---> Tehát nem kell!) 

                        else if (szabalyzat[0][j] == '(') //Ha talál egy nyitójelet növeli a countert.
                        {
                            nyitokDb++;
                        }

                        else if (szabalyzat[0][j] == ')') // Egyértélmű
                        {
                            zarokDb++;
                        }

                        j++; //bejárás miatt.
                    }

                    for (int k = 0; k < emberek.Length; k++) //Ha vége a zárójeles résznek bejárjuk példányokat tartalmazó Emberek típusú tömböt.
                    {
                        if (emberek[k] != null) //Ha nem üres
                        {
                            if (emberek[k].Nev == nev) // és a k-adik elem neve megegyezik az ELSE ágban megtalált névvel (tehát  az ehhez tartozó neveket kell néznünk), ...
                            {
                                emberek[k].KiknekKuldiTovabb(kiknek); // ...akkor átadjuk a megtalált neveket.
                            }
                        }
                    }

                    nev = ""; // Ha megtaláltuk a nevhez közvetlenül tartozó embereket, üresezzük ki, mert a következő nevet ehhez fűzné hozzá és nem találna Pl: MiklosGabor nevű példányt.
                }

                else //Amíg nem találkozik ilyennel: '('
                {
                    if (szabalyzat[0][i] != ' ' && szabalyzat[0][i] != ')') // és az nem is space, és nem is ilyen: ')'
                    {
                        if (szabalyzat[0][i] == ',') // és nem is vessző(hiszen akkor egy új emberről beszélnénk, így ha odaértünk, reseteljük a változót, hogy új nevet kaphassunk majd) 
                        {
                            nev = ""; // Azért  másabb, mint az előző üresezés, mert ha vessző van a név után, nem pedi '(', akkor ehhez a névhez nincs hozzárendelhető személy, nem küld üzenetet senkinek.
                        }

                        else // tehát betű, azt hozzáfűzi a nev változóhoz. Az IF ágban történik meg az ehhez a névhez KÖZVETLENÜL tartozó emberek nevének hozzárendelése
                        {
                            nev += szabalyzat[0][i];
                        }
                    }
                }
            }

            return VeglegesSzabalyzat(emberek); //Csak a neveket tartalmazó tömböt adja vissza.
        }

        static string Beolvas() //Fájl beolvasása.
        {
            StreamReader be = new StreamReader("UZENET.BE.txt");
            string szabalyzat = be.ReadLine(); // Csak az első sort olvassa be.
            be.Close();

            return szabalyzat;
        }

        static Emberek[] VeglegesSzabalyzat(Emberek[] szabalyzatNevek) // Az 1000 nagyságú tömbből lemetszi az üres helyeket.
        {
            int db = 0;

            while (szabalyzatNevek[db] != null) // Megszámolja, mekkora a tömb nem üres része.
            {
                db++;
            }

            Emberek[] vegleges = new Emberek[db];

            for (int i = 0; i < vegleges.Length; i++)
            {
                vegleges[i] = szabalyzatNevek[i];
            }

            return vegleges;
        }

        static int MaxKozvetlenul(Emberek[] szabalyzatNevek) //A feladat.
        {
            int max = 0;
            int maxErtek = 0;

            for (int i = 0; i < szabalyzatNevek.Length; i++)
            {
                if (szabalyzatNevek[i].KiknekKuldi != null) //Kiszűri azokat a tagokat akiknek nincs KinekKuldi tulajdonsága.
                {
                    if (szabalyzatNevek[i].KiknekKuldi.Length > szabalyzatNevek[max].KiknekKuldi.Length) // Ez egy maximumkiválasztás tétel.
                    {
                        max = i;
                        maxErtek = szabalyzatNevek[i].KiknekKuldi.Length;
                    }
                }
            }

            return maxErtek;
        }

        static int UzenetUt(Emberek[] szabalyzatNevek) //B feladat.
        {
            int tovabbIdx = 0; // Segédindex.
            int maxUt = 0; // A végleges eredmény.
            int segedUt = 0; // Segédváltozó.

            for (int i = 0; i < szabalyzatNevek.Length; i++) // Végigmegyünk az összes néven.
            {

                if (szabalyzatNevek[i].KiknekKuldi != null) // Ha továbbküldi a levelet, akkor...
                {
                    tovabbIdx = i; //... mentse el ezt az indexet, és...
                    segedUt++; //... adjon a változóhoz 1-et.
                }

                else // Ha nem küldi tovább, akkor kivonunk a változóból egyed, de csak akkor, ha kilépett a zárójelből, így nem minden névné végezzük el a műveletet, csak kilépéskor.
                {
                    if (tovabbIdx != 0) // Ha történt segedUt++, és nem nulláztuk le a változót, tehát ez az első olyan név, ahol kilépés történt (lásd lentebb).
                    {
                        // Megvizsgáljuk, hogy az adott név benne van-e még a zárójeében, vagy nincs.
                        Emberek[] emberekTomb = new Emberek[szabalyzatNevek[tovabbIdx].KiknekKuldi.Length]; // Ehhez el kell a KiknekKuldi neveit egy tömbbe tárolni, mivel az a véltozó egy string tömb.

                        for (int k = 0; k < emberekTomb.Length; k++)
                        {
                            // Tehát áttesszük egy új Emberek tömbbe a neveket...
                            Emberek segedEmberek = new Emberek(szabalyzatNevek[tovabbIdx].KiknekKuldi[k]);
                            emberekTomb[k] = segedEmberek;
                        }

                        bool kilepett = false; // Igaz, ha kiléptünk a zárójelből.

                        // ...és megnézzük, ezek között van-e az általunk vizsgált név.
                        for (int j = 0; j < emberekTomb.Length; j++)
                        {
                            if (szabalyzatNevek[i] != emberekTomb[j])
                            {
                                // Ha igen, kilépést jelzünk.
                                kilepett = true;
                            }
                        }

                        if (kilepett == true) // Ha kiléptünk a zárójelből, vonjon ki egyet a változóból és nullázza le a segédindexet, hogy a továbbiakban ne vonjunk ki feleslegesen.
                        {
                            segedUt--;
                            tovabbIdx = 0;
                        }

                    }
                }

                if (segedUt > maxUt) // Mivel a maximális út a kérdés.
                {
                    maxUt = segedUt;
                }
            }
            return maxUt;
        }

        static int NemKell(Emberek[] szabalyzatNevek) //C feladat.
        {
            int nemKell = 0;

            for (int i = 0; i < szabalyzatNevek.Length; i++)
            {
                if (szabalyzatNevek[i].KiknekKuldi == null) //Megszámolja azokat a példányokat amelyeknek nincs KinekKuldi tulajdonságuk.
                {
                    nemKell++;
                }
            }

            return nemKell;
        }
    }
}