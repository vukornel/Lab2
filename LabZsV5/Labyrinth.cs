using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;


namespace LabZsV
{

    class Labyrinth
    {
        static readonly int height = 25;
        static readonly int width = 50;
        public int[,] walls { get; }
        Random rnd = new Random();
        private int x;
        private int y;
        public int winX { get; set; }
        public int winY { get; set; }

        int[,] visited; // egyenlőre felesleges de később hátha jólesz

        public Labyrinth() => walls = new int[height, width]; //0 a nemfal, 1 a fal, 2 a győzelmi hely

        private void PreGenerateWalls()
        {
            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                    walls[j, i] = 1; // feltöltés falakkal

            x = 1;
            y = 1;
            walls[x, y] = 0;
            visited = walls; //0 visited 1 nem
        }

        public void GenerateWalls()
        {
            int dbWallLeft = (width - 2) * (height - 2) / 2;
            int direction = rnd.Next(4); // alapból 4

            PreGenerateWalls();
            do
            {
                do
                {
                    if (rnd.Next(4) == 0) direction = rnd.Next(4); // alapból 4
                    NextWall(direction);
                } while (visited[x, y] == 0);

                walls[x, y] = 0;
                visited[x, y] = 0;
                dbWallLeft--;
            } while (dbWallLeft != 0);

            walls[x, y] = 2;
            winX = x;
            winY = y;
        }

        private void NextWall(int direction)
        {
            switch (direction)
            {
                case (0):
                    x = (x < height - 2) ? x + 1 : x;
                    break;
                case (1):
                    x = (x > 1) ? x - 1 : x;
                    break;
                case (2):
                    y = (y < width - 2) ? y + 1 : y;
                    break;
                case (3):
                    y = (y > 1) ? y - 1 : y;
                    break;
            }
        }

        public void DisplayWalls()
        {
            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                {
                    Console.SetCursorPosition(j, i);
                    if (walls[i, j] == 1)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                    }
                    else if (walls[i, j] == 2)
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.Write("O");
                    }
                    Console.Write(" ");
                    Console.BackgroundColor = ConsoleColor.Black;
                }
        }
    }
}
