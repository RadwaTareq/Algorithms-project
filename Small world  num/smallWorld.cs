using System.Diagnostics;
using System.Text;

namespace Small_World_Phenomenon
{
    internal class smallWorld
    {
        static List<List<int>> actorsInMoviesC;
        static List<List<int>> moviesOfEachActorC;
        static List<HashSet<int>> adjList;
        static Dictionary<KeyValuePair<int, int>, List<int>> allPaths = new Dictionary<KeyValuePair<int, int>, List<int>>();
        static Dictionary<int, string> numToActor;
        static Dictionary<int, string> numToMovie;
        static Dictionary<string, int> actorToNum;
        static Queue<string> Answers; 
        static List<KeyValuePair<int, int>> Queries; 
        static int maxActors;
        static int source = 0;
        static int target = 0;
        static int[] parent;
        static bool[] visited;
        static int[] level;
        static int[] Strength;

        public static List<string> numbersToActors(List<int> path)  //O(N)
        {
            List<string> pathS = new List<string>();
            foreach (int i in path)
                pathS.Add(numToActor[i]);
            return pathS;
        }
        public static List<string> numbersToMovies(List<int> moviesint)   //O(N)
        {
            List<string> movies = new List<string>();
            foreach (var movie in moviesint)
            {
                movies.Add(numToMovie[movie]);
            }
            return movies;
        }
        static List<HashSet<int>> makeTheGraph() //O(MA^2)
        {
            List<HashSet<int>> adjList = new List<HashSet<int>>();
            for (int i = 0; i < moviesOfEachActorC.Count; i++)
                adjList.Add(new HashSet<int>());

            for (int i = 0; i < actorsInMoviesC.Count; i++)
            {
                var listOfActors = actorsInMoviesC[i];
                for (int j = 0; j < listOfActors.Count; j++)
                {
                    for (int k = j + 1; k < listOfActors.Count; k++)
                    {
                        adjList[listOfActors[j]].Add(listOfActors[k]);
                        adjList[listOfActors[k]].Add(listOfActors[j]);
                    }
                }
            }
            return adjList;
        }
        

        
       
       
        public static List<int> getBestPathFinal()   //O(ValueTask+E)
        {
            Queue<int> pqNext = new Queue<int>();
            Queue<int> pq = new Queue<int>();
            pq.Enqueue(source);
            parent[source] = -1;
            level[source] = 0;
            Strength[source] = 0;
            bool found = false;

            while (pq.Count != 0)
            {
                int node = pq.Dequeue();
                foreach (var child in adjList[node])
                {
                    if (child == target)
                        found = true;
                    if ((level[child] > level[node]))
                    {
                        level[child] = level[node] + 1;
                        int strength = CalcStrength(node, child) + Strength[node];
                        if (strength > Strength[child])
                        {
                            Strength[child] = strength;
                            parent[child] = node;
                        }
                        if (!visited[child])
                            pqNext.Enqueue(child);
                        visited[child] = true;
                    }
                }
                if (pq.Count == 0)
                {
                    if (found)
                        break;

                    pq = pqNext;
                    pqNext = new Queue<int>();

                }
            }

            List<int> path = new List<int>();
            int cur = target;
            while (cur != -1)
            {
                path.Add(cur);
                cur = parent[cur];
            }
            path.Reverse();

            return path;
        }
        private static int CalcStrength(int node, int child)     //O(NA)
        {
            int max = node, min = child;
            if (moviesOfEachActorC[max].Count > moviesOfEachActorC[min].Count)
            {
                max = child;
                min = node;
            }
            int x = 0;
            foreach (var movie in moviesOfEachActorC[min])
            {
                if (moviesOfEachActorC[max].Contains(movie))
                {
                    x++;
                }
            }
            return x;
        }
        private static List<int> MoviesBetweenTwoActors(int node, int child)   // O(NM)
        {
            int max = node, min = child;
            if (moviesOfEachActorC[child].Count > moviesOfEachActorC[min].Count)
            {
                max = child;
                min = node;
            }
            List<int> common = new List<int>();
            foreach (var movie in moviesOfEachActorC[min])
            {
                if (moviesOfEachActorC[max].Contains(movie))
                {
                    common.Add(movie);
                }
            }
            return common;
        }
        public static List<List<string>> getAllMoviesInPath(List<int> path)   //O(N^3)
        {
            List<List<string>> moviesInPath = new List<List<string>>();
            for (int i = 1; i < path.Count; i++)
            {
                List<int> moviesBetweenTwoActors = MoviesBetweenTwoActors(path[i], path[i - 1]);
                List<string> moviesBetweenTwoActorsST = numbersToMovies(moviesBetweenTwoActors);
                moviesInPath.Add(moviesBetweenTwoActorsST);
            }

            return moviesInPath;
        }
        public static string answerString(List<List<string>> moviesInPath, List<string> BestPath) //O(N^2)
        {
            int RS = 0;
            string chainOfMovies = "";
            string chainOfActors = "";
            foreach (var ListOfMovies in moviesInPath)
            {
                RS += ListOfMovies.Count;
                chainOfMovies += ListOfMovies[0] + " => ";
            }
            StringBuilder sb = new StringBuilder(chainOfMovies);
            sb.Remove(chainOfMovies.Length - 1, 1);
            chainOfMovies = sb.ToString();


            int DoS = BestPath.Count - 1;
            for (int i = 0; i < BestPath.Count - 1; i++)
            {
                chainOfActors += BestPath[i] + " -> ";

            }
            chainOfActors += BestPath[BestPath.Count - 1];
            string answer = "";
            answer += numToActor[source] + "/" + numToActor[target] + "\n" + "DoS = " + DoS + ", RS = " + RS + "\n" + "CHAIN OF ACTORS: " + chainOfActors + "\n" + "CHAIN OF MOVIES:  => " + chainOfMovies + "\n";
            return answer;


        }
        public static void solve(List<List<int>> actorsInMovies, List<KeyValuePair<int, int>> queries, List<List<int>> moviesOfEachActor, Queue<string> answers, Dictionary<int, string> NumToActor, Dictionary<int, string> NumToMovie, Dictionary<string, int> ActorToNum, int choice )
        {
            // SETUP VARIABLES TO SOLVE 
            actorsInMoviesC = actorsInMovies;
            moviesOfEachActorC = moviesOfEachActor;
            numToActor = NumToActor;
            numToMovie = NumToMovie;
            actorToNum = ActorToNum; 
            maxActors = moviesOfEachActorC.Count;
            parent = new int[maxActors];
            visited = new bool[maxActors];
            level = new int[maxActors];
            Strength = new int[maxActors];
            Queries = queries;
            Answers = answers;
            Array.Fill(level, 999);


            if (choice == 1)
                bestPathSolve();
            else if (choice == 2)
                strongestPathSolve();
            else if (choice == 3)
                freqOfDosSolve();
            else if (choice == 4)
                PrintAllPathsSolve();

        }
        // MAIN ALGORITM  
        public static void bestPathSolve()  // O(N(M^2))
        {
            List<int> path;
            List<List<string>> MoviesInPath;

            Stopwatch stopwatch = Stopwatch.StartNew();
            adjList = makeTheGraph();
            int i = 1;
            foreach (var query in Queries)
            {
                source = query.Key; target = query.Value;
                path = getBestPathFinal();
                MoviesInPath = getAllMoviesInPath(path);
                string ans = answerString(MoviesInPath, numbersToActors(path));
                //string expected = Answers.Dequeue();

                //if (ans != expected)
                //{
                //    Console.WriteLine("Test #" + i + " Passed!");
                //}
                //else
                //{

                    Console.WriteLine("Output :");
                    Console.WriteLine(ans);
                    Console.WriteLine();
                    //Console.WriteLine("Expected Output :");
                    //Console.WriteLine(expected);
                    //Console.WriteLine("");

             //   }


                parent = new int[maxActors];
                visited = new bool[maxActors];
                level = new int[maxActors];
                Strength = new int[maxActors];
                Array.Fill(level, 999);
                i++;
            }
            stopwatch.Stop();
            Console.WriteLine("Elapsed Time : " + (float)stopwatch.ElapsedMilliseconds / 1000 + " second");

        }
        // FIRST BONUS 
        public static void freqOfDosSolve()
        {
            Console.WriteLine("ACTOR NAME : ");
            string actor = Console.ReadLine();
            Stopwatch stopwatch = Stopwatch.StartNew();


            adjList = makeTheGraph();
            source = actorToNum[actor];
            List<int> ans = new List<int>();
            Queue<int> pqNext = new Queue<int>();
            Queue<int> pq = new Queue<int>();
            pq.Enqueue(source);
            ans.Add(1);
            visited[source] = true;
            while (pq.Count != 0)
            {
                int node = pq.Dequeue();
                foreach (var child in adjList[node])
                {
                    if (!visited[child])
                    {
                        visited[child] = true;
                        pqNext.Enqueue(child);
                    }
                }
                if (pq.Count == 0)
                {
                    ans.Add(pqNext.Count);
                    pq = pqNext;
                    pqNext = new Queue<int>();
                }
            }
            for (int i = 0; i < ans.Count; i++)
                Console.WriteLine(i + " " + ans[i]);

            stopwatch.Stop();
            Console.WriteLine("Elapsed Time : " + (float)stopwatch.ElapsedMilliseconds / 1000 + " second");

        }
        //SECOND BONUS  

        static bool isNotVisited(int x, List<int> path)
        {
            int size = path.Count;
            for (int i = 0; i < size; i++)
                if (path[i] == x)
                    return false;

            return true;
        }
        public static List<List<int>> findpaths()
        {

            // Create a queue which stores
            // the paths
            Queue<List<int>> queue = new Queue<List<int>>();
            List<List<int>> paths = new List<List<int>>();

            // Path vector to store the current path
            List<int> path = new List<int>();
            path.Add(source);
            queue.Enqueue(path);

            while (queue.Count != 0)
            {
                path = queue.Dequeue();
                int last = path[path.Count - 1];

                // If last vertex is the desired destination
                // then print the path
                if (last == target)
                {
                    paths.Add(path);
                }

                // Traverse to all the nodes connected to
                // current vertex and push new path to queue
                List<int> lastNode = adjList[last].ToList();
                for (int i = 0; i < lastNode.Count; i++)
                {
                    if (isNotVisited(lastNode[i], path))
                    {
                        List<int> newpath = new List<int>(path);
                        newpath.Add(lastNode[i]);
                        queue.Enqueue(newpath);
                    }
                }
            }

            return paths;

        }
        public static List<int> getStrongestPath(List<List<int>> p)
        {
           List<int> answer = new List<int>();
            int max = 0;
           foreach (var path in p)
            {
                int str = 0;
                for ( int i=1; i < path.Count; i++)
                { 
                   str += CalcStrength(path[i], path[i -1]);
                } 
                if(str > max)
                {
                    max = str;
                    answer = path; 
                }  

            } 
           return answer; 
        }
        public static void strongestPathSolve()
        {
            List<int> path;
            List<List<string>> MoviesInPath;

            Stopwatch stopwatch = Stopwatch.StartNew();

            adjList = makeTheGraph();

            int i = 1;
            foreach (var query in Queries)
            {
                source = query.Key; target = query.Value;
               List<List<int>> PATHS = findpaths();
                

                //CHANGE THIS FUNCTION
               path = getStrongestPath(PATHS); 
                MoviesInPath = getAllMoviesInPath(path);
                string ans = answerString(MoviesInPath, numbersToActors(path));


                Console.WriteLine("OUTPUT : ");
                Console.WriteLine(ans);
                Console.WriteLine();


                parent = new int[maxActors];
                visited = new bool[maxActors];
                level = new int[maxActors];
                Strength = new int[maxActors];
                Array.Fill(level, 999);
                i++;
            }

        }
        // THIRD BONUS  
        public static void PrintAllPathsSolve()
        {
            
           

            Stopwatch stopwatch = Stopwatch.StartNew();

            adjList = makeTheGraph();

            int i = 1;
            foreach (var query in Queries)
            {
                source = query.Key; target = query.Value;
                List<List<int>> PATHS = findpaths();


               Console.WriteLine("source: " + numToActor[source] + " " + "Destination: " + numToActor[target]);
                foreach(var path in PATHS)
                {
                    foreach(var actor in path)
                    {
                        Console.Write(numToActor[actor] + " -> ");
                    }  
                    Console.WriteLine();
                } 
                


                parent = new int[maxActors];
                visited = new bool[maxActors];
                level = new int[maxActors];
                Strength = new int[maxActors];
                Array.Fill(level, 999);
                i++;
            }
        }
        }
    }
