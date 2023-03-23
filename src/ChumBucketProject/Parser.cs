using System;
using System.IO;
using System.Windows.Forms;

namespace ChumBucketProject
{
    class Parser
    {
        public TextReader r;

        public Parser(TextReader reader)
        {
            this.r = reader;
        }

        public bool TryGetFile(int numRows, int numCols, out Game newGame)
        {
            newGame = new Game(numRows, numCols);
            string[] tokens;
            string line;
            int rows = 0;

            for (line = r.ReadLine(); line != null; line = r.ReadLine())
            {
                tokens = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                for (int j = 0; j < numCols; j++)
                {
                    newGame.Grid[rows, j] = new Cell(Convert.ToChar(tokens[j]));
                }
                rows++;
            }
            return true;
        }
    }
}