using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Small_World_Phenomenon
{
    class Program
    {
        static string moviesPath = "", queriesPath = "", solutionPath = "", sl;
        static Queue<string> answers = new Queue<string>();

        /// 
        public static void parseSolution() //O(N)
        {
            StreamReader reader = new StreamReader(solutionPath);
            string answer = "";
            while (true)
            {
                string line = reader.ReadLine();
                if (line == null)
                    break;
                if (line == "")
                {
                    answers.Enqueue(answer);
                    answer = "";
                }
                else
                {
                    answer += line + "\n";
                }

            }
            Console.WriteLine("Solution Parsing Done, {0} answers", answers.Count());
        }
        static void menu()  //O(1)
        {
            Console.WriteLine(" ");
            Console.WriteLine("           SMALL   ");

            Console.WriteLine(" [1] 139 Movies 110 Queries");
            Console.WriteLine(" [2] 187 Movies 50 Queries");
            Console.WriteLine(" ");
            Console.WriteLine("           MEDIUM    ");
            Console.WriteLine(" [3] 967 Movies 85 Queries");
            Console.WriteLine(" [4] 967 Movies 4000 Queries");
            Console.WriteLine(" [5] 4736 Movies 110 Queries");
            Console.WriteLine(" [6] 4736 Movies 2000 Queries");
            Console.WriteLine(" ");
            Console.WriteLine("            LARGE    ");
            Console.WriteLine(" [7] 14129 Movies 26 Queries");
            Console.WriteLine(" [8] 14129 Movies 600 Queries");
            Console.WriteLine(" ");
            Console.WriteLine("           EXTREME ");
            Console.WriteLine(" [9] 122806 Movies 22 Queries");
            Console.WriteLine(" [10] 122806 Movies 200 Queries");
            Console.WriteLine("");
            Console.WriteLine("           SAMPLE ");
            Console.WriteLine("[11] sample Movies sample Queries");

            Console.Write(" CHOICE : ");



            int choice = 0;
            choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    moviesPath = @"Complete\small\Case1\Movies193.txt";
                    queriesPath = @"Complete\small\Case1\queries110.txt";
                    sl = @"Complete\small\Case1\Solutions\queries110 - Solution.txt";
                    break;

                case 2:
                    moviesPath = @"Complete\small\Case2\Movies187.txt";
                    queriesPath = @"Complete\small\Case2\queries50.txt";
                    sl = @"Complete\small\Case2\Solutions\queries50 - Solution.txt";
                    break;

                case 3:
                    moviesPath = @"Complete\medium\Case1\Movies967.txt";
                    queriesPath = @"Complete\medium\Case1\queries85.txt";
                    sl = @"Complete\medium\Case1\Solutions\queries85 - Solution.txt";
                    break;

                case 4:
                    moviesPath = @"Complete\medium\Case1\Movies967.txt";
                    queriesPath = @"Complete\medium\Case1\queries4000.txt";
                    sl = @"Complete\medium\Case1\Solutions\queries4000 - Solution.txt";
                    break;

                case 5:
                    moviesPath = @"Complete\medium\Case2\Movies4736.txt";
                    queriesPath = @"Complete\medium\Case2\queries110.txt";
                    sl = @"Complete\medium\Case2\Solutions\queries110 - Solution.txt";
                    break;
                case 6:
                    moviesPath = @"Complete\medium\Case2\Movies4736.txt";
                    queriesPath = @"Complete\medium\Case2\queries2000.txt";
                    sl = @"Complete\medium\Case2\Solutions\queries2000 - Solution.txt";
                    break;

                case 7:
                    moviesPath = @"Complete\large\Movies14129.txt";
                    queriesPath = @"Complete\large\queries26.txt";
                    sl = @"Complete\large\Solutions\queries26 - Solution.txt";
                    break;
                case 8:
                    moviesPath = @"Complete\large\Movies14129.txt";
                    queriesPath = @"Complete\large\queries600.txt";
                    sl = @"Complete\large\Solutions\queries600 - Solution.txt";
                    break;
                case 9:
                    moviesPath = @"Complete\extreme\Movies122806.txt";
                    queriesPath = @"Complete\extreme\queries22.txt";
                    sl = @"Complete\extreme\Solutions\queries22 - Solution.txt";


                    break;
                case 10:
                    moviesPath = @"Complete\extreme\Movies122806.txt";
                    queriesPath = @"Complete\extreme\queries200.txt";
                    sl = @"Complete\extreme\Solutions\queries200 - Solution.txt";
                    break;

                case 11:
                    moviesPath = @"Sample\Movies1.txt";
                    queriesPath = @"Sample\queries1.txt";
                    sl = @"Sample\queries1 - Solution.txt";
                    break;
            }
        }
        static void menu2()
        {
            Console.WriteLine("       CHOOSE GRAPH       ");
            Console.WriteLine("[1] SMALL");
            Console.WriteLine("[2] MEDIUM ");
            Console.WriteLine("[3] LARGE ");
            Console.WriteLine("[4] EXTREME ");
            Console.WriteLine("[5] SAMPLE ");

            int choice = 0;
            choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    moviesPath = @"Complete\small\Case1\Movies193.txt";
                    queriesPath = @"Complete\small\Case1\queries110.txt";
                    sl = @"Complete\small\Case1\Solutions\queries110 - Solution.txt";
                    break;



                case 2:
                    moviesPath = @"Complete\medium\Case1\Movies967.txt";
                    queriesPath = @"Complete\medium\Case1\queries85.txt";
                    sl = @"Complete\medium\Case1\Solutions\queries85 - Solution.txt";
                    break;



                case 3:
                    moviesPath = @"Complete\large\Movies14129.txt";
                    queriesPath = @"Complete\large\queries26.txt";
                    sl = @"Complete\large\Solutions\queries26 - Solution.txt";
                    break;


                case 4:
                    moviesPath = @"Complete\extreme\Movies122806.txt";
                    queriesPath = @"Complete\extreme\queries200.txt";
                    sl = @"Complete\extreme\Solutions\queries200 - Solution.txt";
                    break;


                case 5:
                    moviesPath = @"Sample\Movies1.txt";
                    queriesPath = @"Sample\queries1.txt";
                    sl = @"Sample\queries1 - Solution.txt";
                    break;

            }
        }
        static void MainMenu() //O(N^3)
        {
            Console.WriteLine("[1] LOWEST DEG OF SEPERATION AND HIGHEST RELATION STRENGTH");
            Console.WriteLine("[2] STRONGEST PATH ");
            Console.WriteLine("[3] DISTRIBUTION OF DOS BETWEEN ACTOR AND ALL ACTORS ");
            Console.WriteLine("[4] PRINT ALL PATHS BETWEEN 2 NODES "); 
        }
        static void Main(string[] args)
        {
            MainMenu();
            int choice = 0;
            choice = Convert.ToInt32(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    menu();
                    break;
                case 2:
                    menu();
                    break;
                case 3:
                    menu2();
                    break;
                case 4:
                    menu();
                    break; 
            }
            solutionPath = @"C:\Users\Hagar.Elshafei\Downloads\Small world Final b1\Small world Final b1\Testcases\" + sl;
            if (choice == 1)
            parseSolution();

            string[] lines = File.ReadAllLines(@"C:\Users\Hagar.Elshafei\Downloads\Small world Final b1\Small world Final b1\Testcases\" + moviesPath, Encoding.UTF8);
            List<List<int>> ActorsInMovies = new List<List<int>>();
            List<List<int>> moviesOfEachActor = new List<List<int>>();
            Dictionary<string, int> ActorToNum = new Dictionary<string, int>();
            Dictionary<string, int> MovieToNum = new Dictionary<string, int>();
            Dictionary<int, string> NumToActor = new Dictionary<int, string>();
            Dictionary<int, string> NumToMovie = new Dictionary<int, string>();
            int x = 0;
            foreach (string line in lines)
            {
                var Line = line.Split("/");
                MovieToNum[Line[0]] = MovieToNum.Count ;
                NumToMovie[NumToMovie.Count] = Line[0]; 
                ActorsInMovies.Add(new List<int>());
                for (int i = 1; i < Line.Length; i++)
                {
                    if (!ActorToNum.ContainsKey(Line[i]))
                    {
                        moviesOfEachActor.Add(new List<int>());
                        ActorToNum[Line[i]] = ActorToNum.Count();
                        NumToActor[NumToActor.Count] = Line[i];
                    }
                    ActorsInMovies[x].Add(ActorToNum[Line[i]]);
                    moviesOfEachActor[ActorToNum[Line[i]]].Add(MovieToNum[Line[0]]);
                }
                x++;
            }
            string[] Qlines = File.ReadAllLines(@"C:\Users\Hagar.Elshafei\Downloads\Small world Final b1\Small world Final b1\Testcases\" + queriesPath, Encoding.UTF8);
            var queries = new List<KeyValuePair<int, int>>();
            foreach (var line in Qlines)  //O(N^2)
            {
                string[] lineParts = line.Split('/');
                queries.Add(new KeyValuePair<int, int>(ActorToNum[lineParts[0]], ActorToNum[lineParts[1]]));
            }

                smallWorld.solve(ActorsInMovies, queries, moviesOfEachActor, answers, NumToActor, NumToMovie , ActorToNum, choice);


        }
    }
}