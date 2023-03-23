using System;

namespace ChumBucketProject
{
    class Printer
    {
        public GridHandler gh;
        public Printer(GridHandler grid)
        {
            gh = grid;
        }

        public void PrintPathGrid(Game game, int numRows, int numCols)
        {
            for (int i = 0; i < numRows; i++)
            {
                for (int j = 0; j < numCols; j++)
                {
                    gh.PrintGrid(i, j, game.Grid[i, j].Symbol);
                }
            }
        }
    }
}