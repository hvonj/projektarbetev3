using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Projektarbete_Filmkväll
{
    class Program
    {
        private static List<TV> ListOfTV = new List<TV>();
        private static List<Cinema> ListOfCinema = new List<Cinema>();

        private static void Main(string[] args)
        {

            ListOfCinema = ReadCinemaFile();
            ListOfTV = ReadTvFile();
            string[] userinput = UserInput(ListOfCinema, ListOfTV);

            if (userinput != null)
                ReturnOutput(userinput, ListOfCinema, ListOfTV);

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
            Console.WriteLine("Hej där! Vad heter du? ");
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
            Console.WriteLine();
            Console.WriteLine($"Välkommen {userinput[0]}! Känner du för hemmakväll eller bio idag (skriv 'allt' för att visa hela tablån)?");
            


            while (true)
            {
                userinput[1] = Console.ReadLine();

                if (!(userinput[1].ToLower() == "hemmakväll" || userinput[1].ToLower() == "bio" || userinput[1].ToLower() == "allt"))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Du måste ange hemmakväll eller bio!) \n");
                    Console.ResetColor();
                    continue;
                }
                else
                    break;

            }
            if (userinput[1].ToLower() == "allt")
            {
                IfNothingIsApplied(listOfCinema, listOfTV);
                Console.WriteLine("Slut!");
                Console.ReadKey();
                return null;
            }

            Console.WriteLine();
            userinput[1] = userinput[1][0].ToString().ToUpper() + userinput[1].Substring(1);

            Console.WriteLine($"{userinput[1]} säger du, det kan vi lösa. Vad tycker du om för genre?");

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

                    else if (userinput[1].ToLower() == "hemmakväll")
                    {
                        if (!ListOfTV.Any(x => x.Genre == userinput[2].ToLower()))
                        {

                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Den genren har vi inte ikväll, välj en annan eller 'alla' för att se vad som finns!");
                            Console.ResetColor();
                            continue;
                        }

                    }
                }
                else
                    break;
            }


            Console.WriteLine($"Perfekt! Men hur gammal är du {userinput[0]}?"); //tillåt endast siffror

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
                Match match = Regex.Match(userinput[3], @"^[0-9]\d$");
                if (!match.Success)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Du måste ange ålder i siffror! \n");
                    Console.ResetColor();
                    continue;
                }
                else
                    break;
            }

            Console.WriteLine("Från vilken tid har du tänkt dig?");

            while (true)
            {
                userinput[4] = Console.ReadLine();
                Match match = Regex.Match(userinput[4], @"^[0-9]\d[:][0-9]\d$");
                if (!match.Success)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Du måste skriva det i formatet XX:XX! \n");
                    Console.ResetColor();
                    continue;
                }
                else
                    break;
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

            if (userinput[1].ToLower() == "hemmakväll")
            {
                Console.WriteLine($"Dessa filmer rekommenderar vi för ikväll!)");
                Console.WriteLine();
                List<TV> newTVList = listOfTV.Where(x => x.Genre == genre && x.Age <= age && x.Time >= tid).ToList();
                List<TV> newnew = newTVList.OrderBy(x => x.Time).ToList();
                foreach (var show in newnew)
                {
                    Console.WriteLine($"{show.Name} klockan {show.Time} på {show.Channel}");
                }
            }
            else if (userinput[1].ToLower() == "bio")
            {
                Console.WriteLine($"Dessa filmer rekommenderar vi för ikväll!)");
                Console.WriteLine();

                List<Cinema> newCinemaList = listOfCinema.Where(x => x.Genre == genre && x.Age <= age && x.Time >= tid).ToList();
                List<Cinema> newCnew = newCinemaList.OrderBy(x => x.Time).ToList();
                foreach (var show in newCnew)
                {
                    Console.WriteLine($"{show.Name} klockan {show.Time} på {show.Channel}");
                }
            }

            else
            {
            }


            //Göra argument exeptions på all input. Gärna med lösningar så programmet kan fortsätta


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
                Console.WriteLine($"{show.Name} klockan {show.Time} på {show.Channel}");
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

            if (userinput[1] == "bio")
                {

            }
        }
    }
}


// Exception på genre (lägg till så att användaren inte kan söka på genre som inte finns... "Den genre du angav finns inte tillgänglig ikväll"
// Gör textfärger, ändra typsnitt och storlek 
// Clean code (Linq)
// Gör ruta/stjärnor eller dylikt runt consolen för att spice'a upp lite :)
