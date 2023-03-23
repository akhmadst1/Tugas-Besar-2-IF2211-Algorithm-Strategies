using System.Drawing;
using System.Windows.Forms;

namespace ChumBucketProject
{
    class GridHandler
    {
        public DataGridView Grid;

        public GridHandler(DataGridView dataGridView)
        {
            Grid = dataGridView;
        }

        public void PrintGrid(int row, int column, char map)
        {
            if (map == 'K')
            {
                Grid[column, row].Value = "Start";
                Grid[column, row].Style.BackColor = Color.White;
            }
            else if (map == 'R')
            {
                Grid[column, row].Value = "";
                Grid[column, row].Style.BackColor = Color.White;
            }
            else if (map == 'T')
            {
                Grid[column, row].Value = "Treasure";
                Grid[column, row].Style.BackColor = Color.White;
            }
            else if (map == 'X')
            {
                Grid[column, row].Value = "";
                Grid[column, row].Style.BackColor = Color.Black;
            }
        }

        public void PrintStep()
        {

        }

        public void ClearGrid()
        {
            for (int i = 0; i < Grid.Rows.Count; i++)
            {
                for (int j = 0; j < Grid.Columns.Count; j++)
                {
                    Grid[i, j].Value = "";
                    Grid[i, j].Style.BackColor = Color.White;
                }
            }
        }
    }
}