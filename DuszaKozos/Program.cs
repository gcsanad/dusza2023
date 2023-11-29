//Index out of range csapat

using System.Threading.Tasks.Dataflow;

namespace DuszaKozos
{
    internal class Program
    {
        static List<string> listaAlanyok = new List<string>();
        static List<string> listaEsemenyek = new List<string>();
        static List<string[]> listafogadoFelek = new List<string[]>();
        static List<Jatek> listaJatekok = new List<Jatek>();

        static void Main(string[] args)
        {
            Menu();
        }
        public static void Menu()
        {
            Console.WriteLine("1-\tJáték létrehozása" +
                "\n2-\tFogadás leadása" +
                "\n3-\tJáték lezárása" +
                "\n4-\tLekérdezés" +
                "\n5-\tKilépés");


            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.D1:
                    JatekLetrehozas();
                    break;
                case ConsoleKey.D2:
                    FogadasLeadasa();
                    break;
                case ConsoleKey.D3:
                    JatekLezarasa();
                    break;
                case ConsoleKey.D4:
                    Lekerdezes();
                    break;
                case ConsoleKey.D5:
                    break;
                default:
                    break;
            }
        }

        public static void Lekerdezes()
        {
            Console.Clear();
            Console.WriteLine("1-\tRanglista" +
                "\n2-\tJáték statisztika" +
                "\n3-\tFogadási statisztika");
            var rendezettListaFogadok = listafogadoFelek.OrderByDescending(x => x[1]).ToList();
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.D1:
                    Console.WriteLine("Ranglista:");
                    rendezettListaFogadok.ForEach(x => Console.WriteLine($"\t{x[0]};{x[1]:f2}"));
                    break;
                case ConsoleKey.D2:
                    Console.WriteLine("Játék statisztika");
                    listaJatekok.ForEach(x => Console.WriteLine($"Pontok:\t{x.FogadasokSzama};{x.OsszPontszam}"));
                    break;
                case ConsoleKey.D3:
                    Console.WriteLine("Fogadási statisztika");
                    Console.WriteLine($"Összpontok:\t{listaJatekok.Sum(x => x.OsszPontszam)}");
                    break;
                default:
                    break;
            }
            Console.ReadKey();
            Console.Clear();
            Menu();
        }

        public static void JatekLezarasa()
        {
            Console.Clear();
            Console.Write("Adja meg a szervező nevét: ");
            string szervezoNeve = Console.ReadLine();
            Console.Write("Adja meg a játék megnevezését: ");
            string jatekMegnevezese = Console.ReadLine();
            string fileNeve = "eredmenyek.txt";
            int szam = 4;
            if (File.Exists(fileNeve))
            {
                double szorzo;
                if (listafogadoFelek.Count > 0)
                {
                    szorzo = Math.Round(1 + 5 / Math.Pow(2, listafogadoFelek.Count - 1), 1);

                }
                else
                {
                    szorzo = 0;
                }
                foreach (var elem in listaJatekok)
                {
                    if (szervezoNeve == elem.Szervezo)
                    {
                        File.AppendAllText(fileNeve, $"\n{elem.JatekNeve}");

                        for (int alanyIndex = 0; alanyIndex < elem.Alanyok.Length; alanyIndex++)
                        {
                            for (int esemenyIndex = 0; esemenyIndex < elem.Esemenyek.Length; esemenyIndex++)
                            {
                                File.AppendAllText(fileNeve, $"\n{elem.Alanyok[alanyIndex]};{elem.Esemenyek[esemenyIndex]};{szam};{szorzo}");
                            }
                        }

                    }

                    elem.Lezart = true;
                }

            }
            else
            {
                double szorzo;
                if (listafogadoFelek.Count > 0)
                {
                    szorzo = Math.Round(1 + 5 / Math.Pow(2, listafogadoFelek.Count - 1), 1);

                }
                else
                {
                    szorzo = 0;
                }
                foreach (var elem in listaJatekok)
                {
                    if (szervezoNeve == elem.Szervezo)
                    {
                        File.WriteAllText(fileNeve, elem.JatekNeve);

                        for (int alanyIndex = 0; alanyIndex < elem.Alanyok.Length; alanyIndex++)
                        {
                            for (int esemenyIndex = 0; esemenyIndex < elem.Esemenyek.Length; esemenyIndex++)
                            {
                                File.AppendAllText(fileNeve, $"\n{elem.Alanyok[alanyIndex]};{elem.Esemenyek[esemenyIndex]};{szam};{szorzo}");
                            }
                        }
                    }

                    elem.Lezart = true;
                }
            }

            Console.Clear();
            Menu();
        }

        public static void FogadasLeadasa()
        {
            Console.Clear();
            Console.Write("Adja meg a fogadó fél nevét: ");

            int esemenySzamFogad = 0;
            string fogadoFel = Console.ReadLine();
            int pontok = 100;
            string[] jatekos = new string[3];
            string eredmenyBeKer = "";
            jatekos[0] = fogadoFel;
            jatekos[1] = pontok.ToString();
            jatekos[2] = 0.ToString();

            listafogadoFelek.Add(jatekos);
            listaJatekok.ForEach(x => Console.WriteLine(x.ToString()));
            int index = listafogadoFelek.FindIndex(x => x[0] == fogadoFel);
            Console.WriteLine($"Pontok: {listafogadoFelek[index][1]}");

            Console.Write($"Adja meg melyik játékra szeretne fogadni (1-{listaJatekok.Count}):");
            int fogadasIndex = -1;
            while (fogadasIndex < 0)
            {
                try
                {
                    fogadasIndex = int.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {

                    Console.WriteLine("Helytelen játéknév megadás!");
                }
            }
            Console.Clear();
            int aktPont = int.Parse(listafogadoFelek[index][1]);
            Console.WriteLine($"Pontok: {aktPont}");
            listaJatekok[fogadasIndex-1].FogadasokSzama++;
            Console.WriteLine(listaJatekok[fogadasIndex-1].ToString());

            if (listaJatekok[fogadasIndex-1].Esemenyek.Length > 1)
            {
                Console.WriteLine("Melyik eseményre szeretne fogdni (Számokkal pl.: első(1),második(2),stb.):");
                Console.WriteLine(string.Join(";",listaJatekok[fogadasIndex - 1].Esemenyek));
                esemenySzamFogad = Convert.ToInt32(Console.ReadLine());
                while (esemenySzamFogad == 0)
                {
                    try
                    {
                        esemenySzamFogad = int.Parse(Console.ReadLine());
                    }
                    catch (FormatException)
                    {

                        Console.WriteLine("Helytelen esemény megadás!");
                    }
                }

            }
            Console.Write("Adja meg a fogadás tétjét: ");
            int fogadas = 0;
            while (fogadas == 0)
            {
                try
                {
                    fogadas = int.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {

                    Console.WriteLine("Helytelen a tét megadása!");
                }
            }

            while (fogadas > aktPont || fogadas <= 0 || listaJatekok[fogadasIndex-1].Lezart == true)
            {
                Console.WriteLine("Helytelen pontszám megadás vagy a játék már lezárult!");
                while (fogadas == 0)
                {
                    try
                    {
                        fogadas = int.Parse(Console.ReadLine());
                    }
                    catch (FormatException)
                    {

                        Console.WriteLine("Helytelen pontszám megadás!");
                    }
                }
                fogadas = int.Parse(Console.ReadLine());
            }
            listafogadoFelek[index][1] = $"{int.Parse(listafogadoFelek[index][1]) - fogadas}";
            listafogadoFelek[index][2] = fogadas.ToString();
            int ujPont = aktPont - fogadas;
            listaJatekok[fogadasIndex - 1].FeltettPontok += fogadas;
            listafogadoFelek[index][1] = ujPont.ToString();
            Console.WriteLine($"Pontok: {ujPont}");
            Console.WriteLine("Adja meg hogy szerinte mi lesz az eredmény: ");
            eredmenyBeKer = Console.ReadLine();

            string fileNeve = "fogadasok.txt";

            if (File.Exists(fileNeve))
            {
                File.AppendAllText(fileNeve, $"\n{fogadoFel};{listaJatekok[fogadasIndex - 1].JatekNeve};{listaJatekok[fogadasIndex - 1].Esemenyek[esemenySzamFogad]};{fogadas};{eredmenyBeKer}");

            }
            else
            {
                File.WriteAllText(fileNeve, $"\n{fogadoFel};{listaJatekok[fogadasIndex - 1].JatekNeve};{listaJatekok[fogadasIndex - 1].Esemenyek[esemenySzamFogad]},{fogadas};{eredmenyBeKer}");
            }
            Console.WriteLine("A folytatáshoz nyomjon ENTERT");
            Console.ReadKey();
            Console.Clear();
            Menu();

        }

        public static void JatekLetrehozas()
        {
            Console.Clear();
            listaAlanyok.Clear();
            listaEsemenyek.Clear();
            Console.Write("Adja meg a szervező nevét: ");
            string szervezoNeve = Console.ReadLine();

            Console.Write("Adja meg a játék megnevezését: ");
            string jatekMegnevezese = Console.ReadLine();

            Console.Write("Adja meg az alany nevét (A befejezéshez nyomojn ENTERT): ");
            string alany = Console.ReadLine();
            while (alany != "")
            {
                listaAlanyok.Add(alany);
                Console.Write("Adja meg az alany nevét: ");
                alany = Console.ReadLine();
            }

            Console.Write("Adja meg az esemény nevét (A befejezéshez nyomojn ENTERT): ");
            string esemeny = Console.ReadLine();
            while (esemeny != "")
            {
                listaEsemenyek.Add(esemeny);
                Console.Write("Adja meg az esemény nevét: ");
                esemeny = Console.ReadLine();
            }


            Jatek jatekLetrehoz = new Jatek(szervezoNeve,jatekMegnevezese,string.Join(";",listaAlanyok),string.Join(";",listaEsemenyek));
            listaJatekok.Add(jatekLetrehoz);

            string fileNeve = "jatekok.txt";

            if (File.Exists(fileNeve))
            {
                File.AppendAllText(fileNeve, $"\n{szervezoNeve};{jatekMegnevezese};{listaAlanyok.Count};{listaEsemenyek.Count}");
                for (int i = 0; i < listaAlanyok.Count; i++)
                {
                    File.AppendAllText(fileNeve, $"\n{listaAlanyok[i]}");
                }
                for (int i = 0; i < listaEsemenyek.Count; i++)
                {
                    File.AppendAllText(fileNeve, $"\n{listaEsemenyek[i]}");
                }
            }
            else
            {
                File.WriteAllText(fileNeve, $"{szervezoNeve};{jatekMegnevezese};{listaAlanyok.Count};{listaEsemenyek.Count}");
                for (int i = 0; i < listaAlanyok.Count; i++)
                {
                    File.AppendAllText(fileNeve, $"\n{listaAlanyok[i]}");
                }
                for (int i = 0; i < listaEsemenyek.Count; i++)
                {
                    File.AppendAllText(fileNeve, $"\n{listaEsemenyek[i]}");
                }
            }
            Console.Clear();
            Menu();
        }
    }
}
