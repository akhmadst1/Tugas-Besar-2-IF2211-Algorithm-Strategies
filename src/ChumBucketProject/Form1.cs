using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChumBucketProject
{
    public partial class Form1 : Form
    {
        private AppHandler appHandler;
        private TextReader readerSolver;
        private int seconds;
        public bool isValid = false;
        public bool isSolvedBFS = false;
        public bool isSolvedDFS = false;
        public int numOfTreasure = 0;

        public Form1()
        {
            InitializeComponent();
            appHandler = new AppHandler();
            appHandler.InitNewAppHandler(dataGridView1);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void InitializeGrid(int numRows, int numCols, List<string> charMap)
        {
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.ScrollBars = ScrollBars.None;
            dataGridView1.ColumnHeadersVisible = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

            //dataGridView1.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.DarkBlue;

            //resctricting user to change the grid except the content
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToOrderColumns = false;
            dataGridView1.AllowUserToResizeColumns = false;

            //setting grid size
            dataGridView1.ColumnCount = numCols;
            dataGridView1.Rows.Add(numRows);

            for (int i = 0; i < numRows; i++)
            {
                dataGridView1.Rows[i].Height = dataGridView1.Height / numRows;
                dataGridView1.Rows[i].Frozen = true;
            }
            for (int j = 0; j < numCols; j++)
            {
                dataGridView1.Columns[j].Width = dataGridView1.Width / numCols;
                dataGridView1.Columns[j].Frozen = true;
            }
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.GridColor = Color.Black;
            dataGridView1.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold,
                System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridView1[0, 0].Selected = false;

            int factor = 0;
            isValid = true;
            for (int row = 0; row < numRows; row++)
            {
                for (int col = 0; col < numCols; col++)
                {
                    if (charMap[factor + col] == "K" || charMap[factor + col] == "R" || charMap[factor + col] == "T" || charMap[factor + col] == "X")
                    {
                        PrintGrid(row, col, charMap[factor + col]);
                    }
                    else
                    {
                        ClearGrid();
                        appHandler.ShowMessageBoxFileFail();
                        isValid = false;
                        break;
                    }
                }
                factor += numCols;
                if (!isValid)
                {
                    break;
                }
            }
        }

        public void PrintGrid(int row, int column, string map)
        {
            if (map == "K")
            {
                dataGridView1[column, row].Value = "S";
                dataGridView1[column, row].Style.BackColor = Color.White;
            }
            else if (map == "R")
            {
                dataGridView1[column, row].Value = " ";
                dataGridView1[column, row].Style.BackColor = Color.White;
            }
            else if (map == "T")
            {
                dataGridView1[column, row].Value = "T";
                dataGridView1[column, row].Style.BackColor = Color.White;
                numOfTreasure++;
            }
            else if (map == "X")
            {
                dataGridView1[column, row].Value = "";
                dataGridView1[column, row].Style.BackColor = Color.Black;
            }
        }

        public void ClearGrid()
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    dataGridView1[j, i].Value = " ";
                    dataGridView1[j, i].Style.BackColor = Color.White;
                }
            }
        }

        public void assignReader(string filepath)
        {
            readerSolver = new StreamReader(filepath);
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            int numRows = 0;
            int numCols = 0;
            List<string> charMap = new List<string>();
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                string filePath = openFileDialog1.FileName;
                assignReader(filePath);
                TextReader reader = new StreamReader(filePath);
                for (string line = reader.ReadLine(); line != null; line = reader.ReadLine())
                {
                    string[] lineElements = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (numRows == 0)
                    {
                        numCols = lineElements.Length;
                    }
                    charMap.AddRange(lineElements);
                    numRows++;
                }
                InitializeGrid(numRows, numCols, charMap);
            }
            isSolvedBFS = false;
            isSolvedDFS = false;
        }
        public void clearBackColor()
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                {
                    dataGridView1[j, i].Style.BackColor = Color.White;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (isValid)
            {
                string method = "BFS";
                if (radioButton2.Checked)
                {
                    method = "DFS";
                }

                if (method == "BFS" && isSolvedBFS)
                {
                    appHandler.ShowMessageBoxSolved();
                }
                else if (method == "BFS" && !isSolvedBFS)
                {
                    if (isSolvedDFS)
                    {
                        clearBackColor();
                    }
                    isSolvedBFS = true;
                    isSolvedDFS = false;
                    appHandler.solveMap(method, readerSolver, dataGridView1);
                }

                else if (method == "DFS" && isSolvedDFS)
                {
                    appHandler.ShowMessageBoxSolved();
                }
                else if (method == "DFS" && !isSolvedDFS)
                {
                    if (isSolvedBFS)
                    {
                        clearBackColor();
                    }
                    isSolvedBFS = false;
                    isSolvedDFS = true;
                    appHandler.solveMap(method, readerSolver, dataGridView1);
                }
            }
            else
            {
                appHandler.ShowMessageBoxLoadError();
            }
        }
    }
}