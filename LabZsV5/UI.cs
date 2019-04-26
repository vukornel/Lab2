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
        // private DB database;
        private Stopwatch time = new Stopwatch();
        private bool lose;
        private void Init()
        {
            player = new Player(1, 1, labyrinth);
            labyrinth = new Labyrinth();
            labyrinth.GenerateWalls();
            labyrinth.DisplayWalls();
            player = new Player(1, 1, labyrinth);
            enemy1 = new Enemy(labyrinth.winX, labyrinth.winY, labyrinth);
            DisplayPlayer();
        }

        public void Start()
        {
            Console.CursorVisible = false;

            string line;

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
                        Console.Write("Pontok: " + pont.ToString() + " \nIdő: " + ido.ToString() + "\nNév: ");
                        // string nev = Console.ReadLine();
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
            bool wins = false;

            while (!wins && !lose) // gameloop
            {
                do
                {
                    KeyPressed(Console.ReadKey(true));
                    enemy1.Move();
                    DisplayPlayer();
                    DisplayEnemy(enemy1);
                    if (player.x == enemy1.x && player.y == enemy1.y) lose = true;
                } while (Console.KeyAvailable);

                wins = player.CollidesWithWinCell();
            }

            Console.SetCursorPosition(0, 27);
            if (wins)
            {
                ++playerPoints;
                Console.WriteLine("Ido: " + time.Elapsed.TotalSeconds + ", pontok: " + playerPoints.ToString());
            }
            else
            {
                Console.WriteLine("Elakaptak! Nincs plusz pont. Ido: " + time.Elapsed.TotalSeconds + ", pontok: " + playerPoints.ToString());
            }
            time.Stop();

        }


        private void KeyPressed(ConsoleKeyInfo ck)
        {
            Console.SetCursorPosition(player.y, player.x);
            Console.Write(" ");

            player.Move(ck);
        }

        private void DisplayPlayer()
        {
            Console.SetCursorPosition(player.y, player.x);
            Console.Write("X");
        }

        private void DisplayEnemy(Enemy enemy)
        {
            Console.SetCursorPosition(enemy.y, enemy.x);
            if (!enemy1.CollidesWithWinCell()) Console.Write("E");
        }
    }
}
