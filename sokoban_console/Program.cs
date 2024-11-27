using System;
using System.Diagnostics.Metrics;


namespace sokoban_console
{
    class Program
    {
        private static int heroX = 10;
        private static int heroY = 3;

        private static int level = 0;

        private static Dictionary<string, string> skin = new Dictionary<string, string>()
        {
            { "T", "🌳" }, { "R", "🐔" }, { "C", "🐷" }, { "O", "🍽" }, { "Ⓒ", "🥩" }, { "Ⓡ", "🍖" }, { " ", " " }
        };

        private static string[,,] map =
        {
            {
                { "T", "T", "T", "T", "T", "T", "T", "T", "T", "T", "T", "T", "T", "T", "T", "T", "T", "T", "T", "T" },
                { "T", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", "T" },
                { "T", "T", "T", "T", "T", "T", " ", " ", " ", " ", " ", " ", " ", "R", " ", " ", " ", " ", " ", "T" },
                { "T", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", "O", " ", " ", " ", "T" },
                { "T", " ", " ", " ", " ", " ", " ", "T", " ", " ", " ", " ", " ", " ", "T", "T", "T", "T", "T", "T" },
                { "T", " ", " ", " ", "O", " ", " ", "T", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", "T" },
                { "T", " ", " ", " ", " ", " ", " ", "T", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", "T" },
                { "T", " ", " ", " ", "T", " ", " ", "T", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", "T" },
                { "T", "T", "T", "T", "T", "T", "T", "T", "T", "T", "T", "T", "T", "T", "T", "T", "T", "T", "T", "T" }
            },
            {
                { "T", "T", "T", "T", "T", "T", "T", "T", "T", "T", "T", "T", "T", "T", "T", "T", "T", "T", "T", "T" },
                { "T", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", "T" },
                { "T", "T", "T", "T", "T", "T", " ", " ", " ", " ", " ", " ", " ", "R", " ", " ", " ", " ", " ", "T" },
                { "T", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", "O", " ", " ", " ", "T" },
                { "T", " ", " ", " ", "R", " ", " ", " ", " ", "R", " ", " ", " ", " ", " ", " ", "T", " ", "T", "T" },
                { "T", " ", " ", " ", "O", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", "T" },
                { "T", " ", " ", " ", " ", " ", " ", "O", " ", " ", " ", "T", " ", " ", "O", " ", " ", " ", " ", "T" },
                { "T", " ", " ", " ", "T", " ", " ", " ", " ", " ", " ", "T", " ", " ", " ", " ", " ", " ", " ", "T" },
                { "T", "T", "T", "T", "T", "T", "T", "T", "T", "T", "T", "T", "T", "T", "T", "T", "T", "T", "T", "T" }
            },
            {
                { "T", "T", "T", "T", "T", "T", "T", "T", "T", "T", "T", "T", "T", "T", "T", "T", "T", "T", "T", "T" },
                { "T", " ", " ", " ", " ", " ", " ", " ", " ", " ", "T", " ", "O", " ", " ", " ", " ", " ", " ", "T" },
                { "T", "T", " ", "R", " ", " ", " ", " ", " ", " ", "T", " ", "T", "T", " ", " ", " ", " ", " ", "T" },
                { "T", "O", " ", " ", " ", " ", " ", " ", " ", " ", "T", " ", " ", " ", " ", " ", " ", " ", " ", "T" },
                { "T", "T", "T", "T", " ", " ", " ", " ", " ", " ", "T", " ", "R", " ", " ", " ", " ", " ", "T", "T" },
                { "T", " ", " ", " ", " ", " ", " ", "T", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", "T" },
                { "T", " ", " ", " ", " ", " ", "T", "O", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", "T" },
                { "T", " ", " ", " ", "T", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", "T" },
                { "T", "T", "T", "T", "T", "T", "T", "T", "T", "T", "T", "T", "T", "T", "T", "T", "T", "T", "T", "T" }
            }
        };

        public static void DrawMap()
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                for (int x = 0; x < map.GetLength(2); x++)
                {
                    DrawTile(y, x);
                }
            }
        }

        public static void DrawTile(int y, int x)
        {
            Console.CursorLeft = x * 2;
            Console.CursorTop = y;
            string tile = map[level, y, x];
            switch (tile)
            {
                case "T": Console.ForegroundColor = ConsoleColor.Green; break;
                case "R": Console.ForegroundColor = ConsoleColor.Gray; break;
                case "O": Console.ForegroundColor = ConsoleColor.Yellow; break;
                case "Ⓡ": Console.ForegroundColor = ConsoleColor.Yellow; break;
            }

            Console.WriteLine(skin[tile]);
        }

        public static void DrawHero() //отрисовка персонажа
        {
            Console.CursorLeft = heroX * 2;
            Console.CursorTop = heroY;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(map[level, heroY, heroX] == "O" ? skin["Ⓒ"] : skin["C"]);
        }

        public static bool CanMoveHero(int x, int y) //проверка, может ли персонаж шагнуть
        {
            if (map[level, y, x] != "T")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool MoveStone(int x, int y, int dirX, int dirY)
        {
            if (map[level, y, x] == "R")
            {
                if (map[level, y + dirY, x + dirX] == " ")
                {
                    map[level, y, x] = " ";
                    map[level, y + dirY, x + dirX] = "R";
                    return true;
                }
                else if (map[level, y + dirY, x + dirX] == "O")
                {
                    map[level, y, x] = " ";
                    map[level, y + dirY, x + dirX] = "Ⓡ";
                    return true;
                }

                return false;
            }

            if (map[level, y, x] == "Ⓡ") //движение камень из корзины
            {
                if (map[level, y + dirY, x + dirX] == " ")
                {
                    map[level, y, x] = "O";
                    map[level, y + dirY, x + dirX] = "R";
                    return true;
                }

                return false;
            }

            return true;
        }

        public static Tuple<int, int> CountPoints()
        {
            int total = 0;
            int current = 0;
            for (int y = 0; y < map.GetLength(1); y++)
            {
                for (int x = 0; x < map.GetLength(2); x++)
                {
                    if (map[level, y, x] == "O" || map[level, y, x] == "Ⓡ")
                    {
                        total++;
                    }

                    if (map[level, y, x] == "Ⓡ")
                    {
                        current++;
                    }
                }
            }

            if (map[level, heroY, heroX] == "O")
            {
                current++;
            }

            return Tuple.Create(total, current);
        }

        public static void showInfo(Tuple<int, int> points)
        {
            Console.SetCursorPosition(50, 0);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(points.Item2 + "/" + points.Item1);

            Console.SetCursorPosition(50, 1);
            Console.WriteLine("level " + (level + 1));
            
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.SetCursorPosition(50, 3);
            Console.WriteLine("COOK ALL ANIMALS");
            
        }

        public static void Init()
        {
            heroX = 10;
            heroY = 3;
            Console.Clear();
            Console.CursorVisible = false;
            DrawMap();
            DrawHero();

            Tuple<int, int> points = CountPoints();
            showInfo(points);
        }

        static void Main(string[] args)
        {
            
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Init();
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey();

                int dirX = 0;
                int dirY = 0;
                
                if (key.Key == ConsoleKey.UpArrow)
                {
                    dirY = -1;
                }
                if (key.Key == ConsoleKey.DownArrow)
                {
                    dirY = 1;
                }
                if (key.Key == ConsoleKey.LeftArrow)
                {
                    dirX = -1;
                }
                if (key.Key == ConsoleKey.RightArrow)
                {
                    dirX = 1;
                }

                if (CanMoveHero(heroX + dirX, heroY + dirY))
                {
                    if (MoveStone(heroX + dirX, heroY + dirY, dirX, dirY))
                    {
                        heroX += dirX;
                        heroY += dirY;
                        DrawTile(heroY - dirY, heroX - dirX);
                        DrawHero();
                        DrawTile(heroY + dirY, heroX + dirX);
                    }
                }

                if (key.Key == ConsoleKey.Escape)
                {
                    break;
                }

                Tuple<int, int> points = CountPoints();
                showInfo(points);
                if (points.Item1 == points.Item2)
                {
                    if (level == map.GetLength(0) - 1)
                    {
                        Console.SetCursorPosition(5, 5);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("YOU WON BON APPETIT");
                        Console.ReadKey();
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    }
                    else
                    {
                        Console.SetCursorPosition(5, 2);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("PRESS TO START NEXT LEVEL");
                        Console.ReadKey();
                        level++;
                    }

                    Init();
                }
            }
        }
    }
}