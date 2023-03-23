using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.SqlServer.Server;
using System.Threading;

namespace ChumBucketProject
{
    class Solver
    {
        private int numRows = 0;
        private int numCols = 0;
        private int treasureCount = 0;
        private bool isTop;
        private bool isBottom;
        private bool isLeft;
        private bool isRight;
        private List<string[]> path;
        private List<List<int>> adj;
        private int[] startIndex;
        private int startRow = 0;
        private int startCol = 0;
        private List<List<int>> treasureIndex;
        private List<string> pathTracker = new List<string>();
        private Queue<int[]> queueToVisit = new Queue<int[]>();

        public Solver() { }
        public Solver(string method, TextReader readerSolver, DataGridView dgv)
        {
            helper(readerSolver, dgv);
            if (method == "BFS")
            {
                BFSSolver(dgv);
            }
            else
            {
                DFSSolver(dgv);
            }
        }

        public void BFSSolver(DataGridView dgv)
        {
            int[] copy = new int[2];
            copy[0] = startIndex[0];
            startRow = startIndex[0];
            copy[1] = startIndex[1];
            startCol = startIndex[1];
            queueToVisit.Enqueue(copy);
            
            dgv[startCol, startRow].Style.BackColor = GetColor(adj[startIndex[0]][startIndex[1]]);
            dgv.Refresh();
            Thread.Sleep(700);
            adj[startIndex[0]][startIndex[1]]++;
            
            int[] currentIndex;
            while (queueToVisit.Count != 0 && treasureCount != 0)
            {
                currentIndex = queueToVisit.Dequeue();
                checkPosition(currentIndex);

                if (isTop)
                {
                    if (isLeft)
                    {
                        // cek kanan
                        if (adj[currentIndex[0]][currentIndex[1]+1] == 1)
                        {
                            checkNeighbor(dgv, currentIndex[0], currentIndex[1] + 1);
                        }

                        // cek bawah
                        if (adj[currentIndex[0]+1][currentIndex[1]] == 1)
                        {
                            checkNeighbor(dgv, currentIndex[0] + 1, currentIndex[1]);
                        }
                    }
                    else if (isRight)
                    {
                        // cek bawah
                        if (adj[currentIndex[0] + 1][currentIndex[1]] == 1)
                        {
                            checkNeighbor(dgv, currentIndex[0] + 1, currentIndex[1]);
                        }

                        // cek kiri
                        if (adj[currentIndex[0]][currentIndex[1] - 1] == 1)
                        {
                            checkNeighbor(dgv, currentIndex[0], currentIndex[1] - 1);
                        }
                    }

                    else
                    {
                        // cek kanan
                        if (adj[currentIndex[0]][currentIndex[1] + 1] == 1)
                        {
                            checkNeighbor(dgv, currentIndex[0], currentIndex[1] + 1);
                        }

                        // cek bawah
                        if (adj[currentIndex[0] + 1][currentIndex[1]] == 1)
                        {
                            checkNeighbor(dgv, currentIndex[0] + 1, currentIndex[1]);
                        }

                        // cek kiri
                        if (adj[currentIndex[0]][currentIndex[1]-1] == 1)
                        {
                            checkNeighbor(dgv, currentIndex[0], currentIndex[1] - 1);
                        }
                    }
                }

                else if (isBottom)
                {
                    if (isLeft)
                    {
                        // cek kanan
                        if (adj[currentIndex[0]][currentIndex[1] + 1] == 1)
                        {
                            checkNeighbor(dgv, currentIndex[0], currentIndex[1] + 1);
                        }

                        // cek atas
                        if (adj[currentIndex[0] - 1][currentIndex[1]] == 1)
                        {
                            checkNeighbor(dgv, currentIndex[0] - 1, currentIndex[1]);
                        }
                    }
                    else if (isRight)
                    {
                        // cek kiri
                        if (adj[currentIndex[0]][currentIndex[1] - 1] == 1)
                        {
                            checkNeighbor(dgv, currentIndex[0], currentIndex[1] - 1);
                        }

                        // cek atas
                        if (adj[currentIndex[0] - 1][currentIndex[1]] == 1)
                        {
                            checkNeighbor(dgv, currentIndex[0] - 1, currentIndex[1]);
                        }

                    }

                    else
                    {
                        // cek kanan
                        if (adj[currentIndex[0]][currentIndex[1] + 1] == 1)
                        {
                            checkNeighbor(dgv, currentIndex[0], currentIndex[1] + 1);
                        }

                        // cek kiri
                        if (adj[currentIndex[0]][currentIndex[1] - 1] == 1)
                        {
                            checkNeighbor(dgv, currentIndex[0], currentIndex[1] - 1);
                        }

                        // cek atas
                        if (adj[currentIndex[0] - 1][currentIndex[1]] == 1)
                        {
                            checkNeighbor(dgv, currentIndex[0] - 1, currentIndex[1]);
                        }

                    }
                }

                else
                {
                    if (isLeft)
                    {
                        // cek kanan
                        if (adj[currentIndex[0]][currentIndex[1] + 1] == 1)
                        {
                            checkNeighbor(dgv, currentIndex[0], currentIndex[1] + 1);
                        }

                        // cek bawah
                        if (adj[currentIndex[0] + 1][currentIndex[1]] == 1)
                        {
                            checkNeighbor(dgv, currentIndex[0] + 1, currentIndex[1]);
                        }

                        // cek atas
                        if (adj[currentIndex[0] - 1][currentIndex[1]] == 1)
                        {
                            checkNeighbor(dgv, currentIndex[0] - 1, currentIndex[1]);
                        }
                    }
                    else if (isRight)
                    {
                        // cek bawah
                        if (adj[currentIndex[0] + 1][currentIndex[1]] == 1)
                        {
                            checkNeighbor(dgv, currentIndex[0] + 1, currentIndex[1]);
                        }

                        // cek kiri
                        if (adj[currentIndex[0]][currentIndex[1] - 1] == 1)
                        {
                            checkNeighbor(dgv, currentIndex[0], currentIndex[1] - 1);
                        }

                        // cek atas
                        if (adj[currentIndex[0] - 1][currentIndex[1]] == 1)
                        {
                            checkNeighbor(dgv, currentIndex[0] - 1, currentIndex[1]);
                        }
                    }

                    else
                    {
                        // cek kanan
                        if (adj[currentIndex[0]][currentIndex[1] + 1] == 1)
                        {
                            checkNeighbor(dgv, currentIndex[0], currentIndex[1] + 1);
                        }

                        // cek bawah
                        if (adj[currentIndex[0] + 1][currentIndex[1]] == 1)
                        {
                            checkNeighbor(dgv, currentIndex[0] + 1, currentIndex[1]);
                        }

                        // cek kiri
                        if (adj[currentIndex[0]][currentIndex[1] - 1] == 1)
                        {
                            checkNeighbor(dgv, currentIndex[0], currentIndex[1] - 1);
                        }

                        // cek atas
                        if (adj[currentIndex[0] - 1][currentIndex[1]] == 1)
                        {
                            checkNeighbor(dgv, currentIndex[0] - 1, currentIndex[1]);
                        }
                    }
                }
            }
        }
        public void DFSSolver(DataGridView dgv)
        {
            int[] copy = new int[2];
            copy[0] = startIndex[0];
            copy[1] = startIndex[1];
            queueToVisit.Enqueue(copy);

            dgv[copy[1], copy[0]].Style.BackColor = GetColor(adj[startIndex[0]][startIndex[1]]);
            dgv.Refresh();
            Thread.Sleep(700);
            adj[startIndex[0]][startIndex[1]]++;

            int[] currentIndex;
            while (queueToVisit.Count != 0 && treasureCount != 0)
            {
                currentIndex = queueToVisit.Dequeue();
                checkPosition(currentIndex);

                if (isTop)
                {
                    if (isLeft)
                    {
                        // cek kanan
                        if (adj[currentIndex[0]][currentIndex[1] + 1] == 1)
                        {
                            checkNeighbor(dgv, currentIndex[0], currentIndex[1] + 1);
                        }

                        // cek bawah
                        else if (adj[currentIndex[0] + 1][currentIndex[1]] == 1)
                        {
                            checkNeighbor(dgv, currentIndex[0] + 1, currentIndex[1]);
                        }
                    }
                    else if (isRight)
                    {
                        // cek bawah
                        if (adj[currentIndex[0] + 1][currentIndex[1]] == 1)
                        {
                            checkNeighbor(dgv, currentIndex[0] + 1, currentIndex[1]);
                        }

                        // cek kiri
                        else if (adj[currentIndex[0]][currentIndex[1] - 1] == 1)
                        {
                            checkNeighbor(dgv, currentIndex[0], currentIndex[1] - 1);
                        }
                    }

                    else
                    {
                        // cek kanan
                        if (adj[currentIndex[0]][currentIndex[1] + 1] == 1)
                        {
                            checkNeighbor(dgv, currentIndex[0], currentIndex[1] + 1);
                        }

                        // cek bawah
                        else if (adj[currentIndex[0] + 1][currentIndex[1]] == 1)
                        {
                            checkNeighbor(dgv, currentIndex[0] + 1, currentIndex[1]);
                        }

                        // cek kiri
                        else if (adj[currentIndex[0]][currentIndex[1] - 1] == 1)
                        {
                            checkNeighbor(dgv, currentIndex[0], currentIndex[1] - 1);
                        }
                    }
                }

                else if (isBottom)
                {
                    if (isLeft)
                    {
                        // cek kanan
                        if (adj[currentIndex[0]][currentIndex[1] + 1] == 1)
                        {
                            checkNeighbor(dgv, currentIndex[0], currentIndex[1] + 1);
                        }

                        // cek atas
                        else if (adj[currentIndex[0] - 1][currentIndex[1]] == 1)
                        {
                            checkNeighbor(dgv, currentIndex[0] - 1, currentIndex[1]);
                        }
                    }
                    else if (isRight)
                    {
                        // cek kiri
                        if (adj[currentIndex[0]][currentIndex[1] - 1] == 1)
                        {
                            checkNeighbor(dgv, currentIndex[0], currentIndex[1] - 1);
                        }

                        // cek atas
                        else if (adj[currentIndex[0] - 1][currentIndex[1]] == 1)
                        {
                            checkNeighbor(dgv, currentIndex[0] - 1, currentIndex[1]);
                        }

                    }

                    else
                    {
                        // cek kanan
                        if (adj[currentIndex[0]][currentIndex[1] + 1] == 1)
                        {
                            checkNeighbor(dgv, currentIndex[0], currentIndex[1] + 1);
                        }

                        // cek kiri
                        else if (adj[currentIndex[0]][currentIndex[1] - 1] == 1)
                        {
                            checkNeighbor(dgv, currentIndex[0], currentIndex[1] - 1);
                        }

                        // cek atas
                        else if (adj[currentIndex[0] - 1][currentIndex[1]] == 1)
                        {
                            checkNeighbor(dgv, currentIndex[0] - 1, currentIndex[1]);
                        }

                    }
                }

                else
                {
                    if (isLeft)
                    {
                        // cek kanan
                        if (adj[currentIndex[0]][currentIndex[1] + 1] == 1)
                        {
                            checkNeighbor(dgv, currentIndex[0], currentIndex[1] + 1);
                        }

                        // cek bawah
                        else if (adj[currentIndex[0] + 1][currentIndex[1]] == 1)
                        {
                            checkNeighbor(dgv, currentIndex[0] + 1, currentIndex[1]);
                        }

                        // cek atas
                        else if (adj[currentIndex[0] - 1][currentIndex[1]] == 1)
                        {
                            checkNeighbor(dgv, currentIndex[0] - 1, currentIndex[1]);
                        }
                    }
                    else if (isRight)
                    {
                        // cek bawah
                        if (adj[currentIndex[0] + 1][currentIndex[1]] == 1)
                        {
                            checkNeighbor(dgv, currentIndex[0] + 1, currentIndex[1]);
                        }

                        // cek kiri
                        else if (adj[currentIndex[0]][currentIndex[1] - 1] == 1)
                        {
                            checkNeighbor(dgv, currentIndex[0], currentIndex[1] - 1);
                        }

                        // cek atas
                        else if (adj[currentIndex[0] - 1][currentIndex[1]] == 1)
                        {
                            checkNeighbor(dgv, currentIndex[0] - 1, currentIndex[1]);
                        }
                    }

                    else
                    {
                        // cek kanan
                        if (adj[currentIndex[0]][currentIndex[1] + 1] == 1)
                        {
                            checkNeighbor(dgv, currentIndex[0], currentIndex[1] + 1);
                        }

                        // cek bawah
                        else if (adj[currentIndex[0] + 1][currentIndex[1]] == 1)
                        {
                            checkNeighbor(dgv, currentIndex[0] + 1, currentIndex[1]);
                        }

                        // cek kiri
                        else if (adj[currentIndex[0]][currentIndex[1] - 1] == 1)
                        {
                            checkNeighbor(dgv, currentIndex[0], currentIndex[1] - 1);
                        }

                        // cek atas
                        else if (adj[currentIndex[0] - 1][currentIndex[1]] == 1)
                        {
                            checkNeighbor(dgv, currentIndex[0] - 1, currentIndex[1]);
                        }
                    }
                }
            }
        }

        public void checkNeighbor(DataGridView dgv, int row, int col)
        {
            dgv[col, row].Style.BackColor = GetColor(adj[row][col]);
            dgv.Refresh();
            Thread.Sleep(700);
            adj[row][col]++;
            queueToVisit.Enqueue(visitNeighbor(row, col));
            checkTreasure(row, col);
        }

        public int[] visitNeighbor(int row, int col)
        {
            int[] neighborIndex = new int[2];
            neighborIndex[0] = row;
            neighborIndex[1] = col;
            return neighborIndex;
        }

        public void checkTreasure(int row, int col)
        {
            if (path[row][col] == "T")
            {
                treasureCount--;
            }
        }

        public Color GetColor(int visitedTimes)
        {
            Color color = Color.FromArgb(0, 255 - ((visitedTimes - 1) * 60), 255 - ((visitedTimes - 1) * 60));
            return color;
        }

        public void checkPosition(int[] currentIndex)
        {
            isTop = false;
            isBottom = false;
            isLeft = false;
            isRight = false;
            if (currentIndex[0] == 0)
            {
                isTop = true;
            }
            else if (currentIndex[0] == numRows - 1)
            {
                isBottom = true;
            }
            if (currentIndex[1] == 0)
            {
                isLeft = true;
            }
            else if (currentIndex[1] == numCols - 1)
            {
                isRight = true;
            }
        }


        public void helper(TextReader readerSolver, DataGridView dgv)
        {
            path = new List<string[]>();
            for (string line = readerSolver.ReadLine(); line != null; line = readerSolver.ReadLine())
            {
                string[] lineElements = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (numRows == 0)
                {
                    numCols = lineElements.Length;
                }
                path.Add(lineElements);
                numRows++;
            }

            adj = new List<List<int>>();
            for (int i = 0; i < numRows; i++)
            {
                List<int> newList = new List<int>();
                for (int j = 0; j < numCols; j++)
                {
                    if (path[i][j] == "X")
                    {
                        newList.Add(0);
                    }
                    else
                    {
                        newList.Add(1);
                        if (path[i][j] == "T")
                        {
                            treasureCount++;
                        }
                    }
                }
                adj.Add(newList);
            }
            startIndex = getStartIndex();
            treasureIndex = getTreasureIndex();
        }

        public int[] getStartIndex()
        {
            int[] index = new int[2];
            for (int i = 0; i < path.Count; i++)
            {
                for (int j = 0; j < path[i].Length; j++)
                {
                    if (path[i][j] == "K")
                    {
                        index[0] = i;
                        index[1] = j;
                        break;
                    }
                }
            }
            return index;
        }

        public List<List<int>> getTreasureIndex()
        {
            List<List<int>> indices = new List<List<int>>();
            for (int i = 0; i < path.Count; i++)
            {
                List<int> index = new List<int>();
                for (int j = 0; j < path[i].Length; j++)
                {
                    if (path[i][j] == "T")
                    {
                        index.Add(i);
                        index.Add(j);
                    }
                }
                indices.Add(index);
            }
            return indices;
        }
    }
}