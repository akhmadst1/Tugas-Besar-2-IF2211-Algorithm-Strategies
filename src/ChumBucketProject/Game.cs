using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChumBucketProject
{
    class Game
    {
        public Cell[,] Grid;
        public Game()
        {

        }
        public Game(int numRows, int numCols)
        {
            Grid = new Cell[numRows,numCols];
        }

    }
}