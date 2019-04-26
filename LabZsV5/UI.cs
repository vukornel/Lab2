using System;
using System.Diagnostics;
using System.Data.SqlClient;

namespace LabZsV
{
    class UI
    {

        private Player player;
        private Enemy enemy1;
        private int playerPoints = 0;
        private Labyrinth labyrinth;
        private DB database;
        private Stopwatch time = new Stopwatch();
        private bool lose = false;
        Random rnd = new Random();

        public void Start()
        {
            Console.CursorVisible = false;

            string line;
            string nev;

            while (true)
            {
                do
                {
                    Console.SetCursorPosition(0, 26);
                    Console.WriteLine("1 Uj jatek, 2 Kilepes es mentes, 3 Pont lekerdezese\n");
                    line = Console.ReadLine();
                    Console.Clear();
                } while (line != "1" && line != "2" && line != "3");

                if (line == "1") StartGame();
                // else if (line == "3") database.DBquery(); db off
                else
                {
                    int pont = playerPoints;
                    int ido = (int)time.Elapsed.TotalSeconds;
                    if (pont != 0)
                    {
                        Console.Write("Pontok: " + pont.ToString());
                        Console.Write("\nIdő: " + ido.ToString());
                        Console.Write("\nNév: ");
                        nev = Console.ReadLine();
                        // database.DBconnect(pont, ido, nev); adatbázis off
                    }

                    System.Environment.Exit(1);
                }
            }
        }

        private void StartGame()
        {
            Init();
            time.Start();
            lose = false;

            while (!Wins(player.x, player.y) && !lose) // gameloop
            {
                do
                {
                    KeyPressed(Console.ReadKey(true));
                    DisplayPlayer(player.y, player.x);
                    MoveEnemy(enemy1);
                    DisplayEnemy(enemy1.y, enemy1.x);
                } while (Console.KeyAvailable);
            }
            if (Wins(player.x, player.y))
            {
                Console.SetCursorPosition(0, 27);
                ++playerPoints;
                Console.WriteLine("Ido: " + time.Elapsed.TotalSeconds + ", pontok: " + playerPoints.ToString());
                time.Stop();
            }
            else
            {
                Console.SetCursorPosition(0, 27);
                Console.WriteLine("Elakaptak! Nincs plusz pont. Ido: " + time.Elapsed.TotalSeconds + ", pontok: " + playerPoints.ToString());
                time.Stop();
            }
            
        }

        private void Init()
        {
            player = new Player(1, 1);
            labyrinth = new Labyrinth();
            labyrinth.GenerateWalls();
            labyrinth.DisplayWalls();
            enemy1 = new Enemy(labyrinth.winX, labyrinth.winY);
            DisplayPlayer(player.y, player.x);
        }

        private void KeyPressed(ConsoleKeyInfo ck)
        {
            Console.SetCursorPosition(player.y, player.x);
            Console.Write(" ");

            MovePlayer(ck);
        }

        private void MovePlayer(ConsoleKeyInfo ck)
        {
            switch (ck.Key)
            {
                case ConsoleKey.RightArrow:
                    player.y++;
                    if (CollidesWithWall(player.x, player.y)) player.y--;
                    break;
                case ConsoleKey.LeftArrow:
                    player.y--;
                    if (CollidesWithWall(player.x, player.y)) player.y++;
                    break;
                case ConsoleKey.DownArrow:
                    player.x++;
                    if (CollidesWithWall(player.x, player.y)) player.x--;
                    break;
                case ConsoleKey.UpArrow:
                    player.x--;
                    if (CollidesWithWall(player.x, player.y)) player.x++;
                    break;
            }
        }

        private void DisplayPlayer(int y, int x)
        {
            Console.SetCursorPosition(y, x);
            Console.Write("X");
        }

        private void MoveEnemy(Enemy enemy)
        {
            Console.SetCursorPosition(enemy.y, enemy.x); /// Nem 100%os
            if(!Wins(enemy.x, enemy.y)) Console.Write(" ");

            int rand = rnd.Next(4);
            switch (rand)
            {
                case (0):
                    enemy.x = (enemy.x < 25 - 2) ? enemy.x + 1 : enemy.x;
                    if (CollidesWithWall(enemy.x, enemy.y)) enemy.x--;
                    break;
                case (1):
                    enemy.x = (enemy.x > 1) ? enemy.x - 1 : enemy.x;
                    if (CollidesWithWall(enemy.x, enemy.y)) enemy.x++;
                    break;
                case (2):
                    enemy.y = (enemy.y < 50 - 2) ? enemy.y + 1 : enemy.y;
                    if (CollidesWithWall(enemy.x, enemy.y)) enemy.y--;
                    break;
                case (3):
                    enemy.y = (enemy.y > 1) ? enemy.y - 1 : enemy.y;
                    if (CollidesWithWall(enemy.x, enemy.y)) enemy.y++;
                    break;
            }

            if (player.x == enemy.x && player.y == enemy.y) lose = true;
        }


        private void DisplayEnemy(int y, int x)
        {
            Console.SetCursorPosition(y, x);
            if (!Wins(x, y)) Console.Write("E");
        }

        private bool CollidesWithWall(int x, int y) => labyrinth.walls[x, y] == 1;
        private bool Wins(int x, int y) => labyrinth.walls[x, y] == 2;
    }
}
