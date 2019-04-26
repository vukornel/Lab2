using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabZsV
{
    class Entity
    {
        public int x { get; set; }
        public int y { get; set; }
        public Labyrinth labyrinth { get; set; }

        public Entity(int x, int y, Labyrinth inLabi)
        {
            labyrinth = inLabi;
            this.x = x;
            this.y = y;
        }


        public bool CollidesWithWall() => labyrinth.walls[x, y] == 1;
        public bool CollidesWithWinCell() => labyrinth.walls[x, y] == 2;
    }
}
