using System;

namespace LabZsV
{
    class Player : Entity
	{
        public Player(int x, int y, Labyrinth inLabi) : base(x, y, inLabi) {}

        public void Move(ConsoleKeyInfo ck)
        {
            switch (ck.Key)
            {
                case ConsoleKey.RightArrow:
                    y++;
                    if (CollidesWithWall()) y--;
                    break;
                case ConsoleKey.LeftArrow:
                    y--;
                    if (CollidesWithWall()) y++;
                    break;
                case ConsoleKey.DownArrow:
                    x++;
                    if (CollidesWithWall()) x--;
                    break;
                case ConsoleKey.UpArrow:
                    x--;
                    if (CollidesWithWall()) x++;
                    break;
            }
        }

    }
}
