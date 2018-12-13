using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace Projektarbete_Filmkväll
{
    public class Program
    {
        private static List<TV> ListOfTV = new List<TV>();
        private static List<Cinema> ListOfCinema = new List<Cinema>();

        private static void Main(string[] args)
        {
            string titelRullar = "Välkommen till Filmkoll!";
            var title = "";
            while (true)
            {
                for (int i = 0; i < titelRullar.Length; i++)
                {
                    title += titelRullar[i];
                    Console.Title = title;
                    Thread.Sleep(40);
                }
                title = "";
                break;
            }

            FilmkollenIntro();


            ListOfCinema = ReadCinemaFile();
            ListOfTV = ReadTvFile();
            string[] userinput = UserInput(ListOfCinema, ListOfTV);

            if (userinput != null)
            {
                ReturnOutput(userinput, ListOfCinema, ListOfTV);
            }
        }

        private static void FilmkollenIntro()
        {
            Console.WriteLine("\n\n\n");
            Console.WriteLine("\t\t* * * * *    *   *           *         *  *       *     * *     *          *          *");
            Console.WriteLine("\t\t*            *   *           *  *   *  *  *     *     *     *   *          *          *");
            Console.WriteLine("\t\t*            *   *           *    *    *  *   *      *       *  *          *          *");
            Console.WriteLine("\t\t* * * *      *   *           *         *  * *        *       *  *          *          *");
            Console.WriteLine("\t\t*            *   *           *         *  *   *      *       *  *          *          *");
            Console.WriteLine("\t\t*            *   *           *         *  *     *     *     *   *          *           ");
            Console.WriteLine("\t\t*            *   * * * * *   *         *  *       *     * *     * * * * *  * * * * *  *");

            Console.WriteLine("\n\t\t\t\t\tTryck på Enter för att fortsätta...");
            Console.ReadKey();
            Console.Clear();
        }

        private static List<Cinema> ReadCinemaFile()
        {
            List<Cinema> listofCinema = new List<Cinema>();

            string[] lines = System.IO.File.ReadAllLines(@"textfiler\Biofilmer.txt");

            foreach (var line in lines)
            {

                var x = new Cinema();
                string[] newlines = line.Split(',');
                x.Channel = newlines[0];
                x.Time = TimeSpan.Parse(newlines[1]);
                x.Genre = newlines[2];
                x.Name = newlines[3];
                x.Age = int.Parse(newlines[4]);
                listofCinema.Add(x);
            }
            return listofCinema;
        }

        private static List<TV> ReadTvFile()
        {
            List<TV> listofTV = new List<TV>();

            string[] lines = System.IO.File.ReadAllLines(@"textfiler\Tvtablå2.txt");

            foreach (var line in lines)
            {

                var y = new TV();
                string[] newlines = line.Split(',');
                y.Channel = newlines[0];
                y.Time = TimeSpan.Parse(newlines[1]);
                y.Genre = newlines[2];
                y.Name = newlines[3];
                y.Age = int.Parse(newlines[4]);
                listofTV.Add(y);
            }
            return listofTV;
        }

        private static string[] UserInput(List<Cinema> listOfCinema, List<TV> listOfTV)
        {

            string[] userinput = new string[5];
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Hej där! Vad heter du? ");
            Console.ResetColor();
            userinput[0] = Console.ReadLine();
            

            userinput[0] = userinput[0][0].ToString().ToUpper() + userinput[0].Substring(1);

            if (string.IsNullOrWhiteSpace(userinput[0]))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Du måste ange ett namn! \n");
                Console.ResetColor();
                userinput[0] = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(userinput[0]))
                {
                    userinput[0] = "Gäst";
                }
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"\nVälkommen {userinput[0]}! Känner du för hemmakväll eller bio idag (skriv 'allt' för att visa hela tablån)?");
            Console.ResetColor();

            while (true)
            {
                userinput[1] = Console.ReadLine();

                if (!(userinput[1].ToLower() == "hemmakväll" || userinput[1].ToLower() == "bio" || userinput[1].ToLower() == "allt"))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Du måste ange hemmakväll eller bio!\n");
                    Console.ResetColor();
                    continue;
                }
                else
                {
                    break;
                }

            }
            if (userinput[1].ToLower() == "allt")
            {
                IfNothingIsApplied(listOfCinema, listOfTV);
                Console.WriteLine("Slut!");
                Console.ReadKey();
                return null;
            }

            
            userinput[1] = userinput[1][0].ToString().ToUpper() + userinput[1].Substring(1);

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"\n{userinput[1]} säger du, det kan vi lösa. Vad tycker du om för genre?");
            Console.ResetColor();

            while (true)
            {
                userinput[2] = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(userinput[2]))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Du måste ange vilken genre du vill se, kan du inte bestämma dig skriv 'alla'!");
                    Console.ResetColor();
                    continue;
                }
                else if (userinput[2] == "alla")
                {
                    break;
                }

                else if (userinput[1].ToLower() == "bio")
                {
                    if (!ListOfCinema.Any(x => x.Genre == userinput[2].ToLower()))
                    {

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Den genren har vi inte ikväll, välj en annan eller 'alla' för att se vad som finns!");
                        Console.ResetColor();
                        continue;
                    }

                    break;
                }

                else if (userinput[1].ToLower() == "hemmakväll")
                {
                    if (!ListOfTV.Any(x => x.Genre == userinput[2].ToLower()))
                    {

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Den genren har vi inte ikväll, välj en annan eller 'alla' för att se vad som finns!");
                        Console.ResetColor();
                        continue;
                    }
                    break;
                }
                else
                {
                    break;
                }
                

            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"\nPerfekt! Hur gammal är du {userinput[0]}?");
            Console.ResetColor();

            while (true)
            {
                userinput[3] = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(userinput[3]))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Du måste ange ålder!");
                    Console.ResetColor();
                    continue;
                }
                Match match = Regex.Match(userinput[3], @"^[0-9]+$");
                if (!match.Success)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Du måste ange ålder i siffror! \n");
                    Console.ResetColor();
                    continue;
                }
                else
                {
                    break;
                }
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\nFrån vilken tid har du tänkt dig?");
            Console.ResetColor();

            while (true)
            {
                userinput[4] = Console.ReadLine();
                Match match = Regex.Match(userinput[4], @"^[0-9]\d[:][0-9]\d$"); //Här vill vi validera så det blir en giltig tid.

                if (!match.Success)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Du måste skriva det i formatet XX:XX! \n");
                    Console.ResetColor();
                    continue;
                }
                else
                {
                    break;
                }
            }

            Console.WriteLine();

            return userinput;

        }


        private static void ReturnOutput(string[] userinput, List<Cinema> listOfCinema, List<TV> listOfTV)
        {
            //userinput 0 = Namn
            //userinput 1 = Bio eller hemmakväll
            //userinput 2 = Genre
            //userinput 3 = Ålder
            //userinput 4 = Tid
            int age = int.Parse(userinput[3]);
            TimeSpan tid = TimeSpan.Parse(userinput[4]);
            string genre = userinput[2].ToLower();

            Console.WriteLine();

            if (userinput[2] == "alla")
            {
                TextTonight();

                GetAllGenres(listOfTV, listOfCinema, userinput);
            }

            else if (userinput[1].ToLower() == "hemmakväll")
            {
                TextTonight();

                List<TV> newTVList = listOfTV.Where(x => x.Genre == genre && x.Age <= age && x.Time >= tid).ToList();
                List<TV> newnew = newTVList.OrderBy(x => x.Time).ToList();
                foreach (var show in newnew)
                {
                    Console.WriteLine($"{show.Name.PadRight(45)} klockan {show.Time}\t\t på {show.Channel}");
                }
            }
            else if (userinput[1].ToLower() == "bio")
            {
                TextTonight();

                List<Cinema> newCinemaList = listOfCinema.Where(x => x.Genre == genre && x.Age <= age && x.Time >= tid).ToList();
                List<Cinema> newCnew = newCinemaList.OrderBy(x => x.Time).ToList();
                foreach (var show in newCnew)
                {
                    Console.WriteLine($"{show.Name.PadRight(45)} klockan {show.Time}\t\t på {show.Channel}");
                }
            }

            Console.WriteLine();
        }


        private static void IfNothingIsApplied(List<Cinema> listOfCinema, List<TV> listOfTV)
        {
            List<Show> Shows = new List<Show>();
            foreach (var z in listOfCinema)
            {
                var y = new Show
                {
                    Channel = z.Channel,
                    Time = z.Time,
                    Genre = z.Genre,
                    Name = z.Name,
                    Age = z.Age
                };
                Shows.Add(y);
            }
            foreach (var g in listOfTV)
            {
                var k = new Show
                {
                    Channel = g.Channel,
                    Time = g.Time,
                    Genre = g.Genre,
                    Name = g.Name,
                    Age = g.Age
                };
                Shows.Add(k);
            }
            List<Show> SortedList = Shows.OrderBy(x => x.Time).ToList();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Du kunde ju inte bestämma dig, så här får du hela tablån!");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;

            foreach (var show in SortedList)
            {
                Console.WriteLine($"{show.Name.PadRight(45)} klockan {show.Time}\t\t på {show.Channel}");
            }
        }

        static void GetAllGenres(List<TV> listofTV, List<Cinema> listOfCinema, string[] userinput)
        {
            //userinput 0 = Namn
            //userinput 1 = Bio eller hemmakväll
            //userinput 2 = Genre
            //userinput 3 = Ålder
            //userinput 4 = Tid
            int age2 = int.Parse(userinput[3]);
            TimeSpan tid2 = TimeSpan.Parse(userinput[4]);
            string genre2 = userinput[2].ToLower();

            if (userinput[1].ToLower() == "bio")
            {
                List<Cinema> cinemaGenre = listOfCinema.Where(x => x.Age <= age2 && x.Time >= tid2).ToList();
                List<Cinema> newCnew = cinemaGenre.OrderBy(x => x.Time).ToList();

                foreach (var show in newCnew)
                {
                    Console.WriteLine($"{show.Name.PadRight(45)} klockan {show.Time}\t\t på {show.Channel}");
                }
            }
            else
            {
                List<TV> tVGenre = listofTV.Where(x => x.Age <= age2 && x.Time >= tid2).ToList();
                List<TV> newTnew = tVGenre.OrderBy(x => x.Time).ToList();
                
                foreach (var show in newTnew)
                {
                    Console.WriteLine($"{show.Name.PadRight(45)} klockan {show.Time}\t\t på {show.Channel}");
                }
            }
        }
        private static void TextTonight()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\n\n\n");
            Console.WriteLine("                    *   *                   ");
            Console.WriteLine("*  *   *  *       *   *     *        *      ");
            Console.WriteLine("*  *  *    *     *   * *    *        *      ");
            Console.WriteLine("*  **       *   *   *   *   *        *      ");
            Console.WriteLine("*  *  *      * *   * * * *  *        *      ");
            Console.WriteLine("*  *   *      *   *       * * * * *  * * * *");
            Console.ResetColor();
            Console.WriteLine();
        }
    }
}




// Gör textfärger, ändra typsnitt och storlek 
// Clean code (Linq)
// Gör ruta/stjärnor eller dylikt runt consolen för att spice'a upp lite :)
