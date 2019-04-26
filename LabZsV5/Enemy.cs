using System;

namespace LabZsV
{
    class Enemy : Entity
    {
        Random rnd = new Random();

        public Enemy(int x, int y, Labyrinth inLabi) : base(x, y, inLabi) {}

        public void Move()
        {
            Console.SetCursorPosition(y, x);
            if (!CollidesWithWinCell()) Console.Write(" ");

            switch (rnd.Next(4))
            {
                case (0):
                    x = (x < 25 - 2) ? x + 1 : x;
                    if (CollidesWithWall()) x--;
                    break;
                case (1):
                    x = (x > 1) ? x - 1 : x;
                    if (CollidesWithWall()) x++;
                    break;
                case (2):
                    y = (y < 50 - 2) ? y + 1 : y;
                    if (CollidesWithWall()) y--;
                    break;
                case (3):
                    y = (y > 1) ? y - 1 : y;
                    if (CollidesWithWall()) y++;
                    break;
            }
        }

    }
}
