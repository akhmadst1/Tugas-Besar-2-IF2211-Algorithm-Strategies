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
        private int nodeVisited = 0;
        private int treasureCount = 0;
        private bool isTop;
        private bool isBottom;
        private bool isLeft;
        private bool isRight;
        private List<string[]> path;
        private List<List<int>> adj;
        private int[] startIndex;
        private List<List<int>> treasureIndex;
        private List<string> trackPath = new List<string>();
        private Queue<int[]> queueToVisit = new Queue<int[]>();

        public Solver() { }
        public Solver(string method, TextReader readerSolver, DataGridView dgv, List<string> pathTracker)
        {
            helper(readerSolver, dgv);
            if (method == "BFS")
            {
                BFSSolver(dgv, pathTracker);
            }
            else
            {
                DFSSolver(dgv, pathTracker);
            }
        }

        // -------------------------[BFS ALGORITHM]----------------------------------
        public void BFSSolver(DataGridView dgv, List<string> pathTracker)
        {
            queueToVisit.Enqueue(startIndex);
            dgv[startIndex[1], startIndex[0]].Style.BackColor = GetColor(adj[startIndex[0]][startIndex[1]]);
            dgv.Refresh();
            Thread.Sleep(700);
            adj[startIndex[0]][startIndex[1]]++;
            findPathBFS(startIndex, dgv, pathTracker);
        }

        public void findPathBFS(int[] currentIndex, DataGridView dgv, List<string> pathTracker)
        {
            if (treasureCount != 0)
            {
                trackBFS(visitNeighbor(currentIndex[0], currentIndex[1]), dgv, pathTracker);
            }
            foreach (string s in trackPath)
            {
                pathTracker.Add(s);
            }
        }

        public void trackBFS(int[] currentIndex, DataGridView dgv, List<string> pathTracker)
        {
            while (queueToVisit.Count != 0 && treasureCount != 0)
            {
                currentIndex = queueToVisit.Dequeue();
                checkPosition(currentIndex);

                if (isTop)
                {
                    if (isLeft)
                    {
                        checkRight(currentIndex, dgv);
                        checkDown(currentIndex, dgv);
                    }
                    else if (isRight)
                    {
                        checkDown(currentIndex, dgv);
                        checkLeft(currentIndex, dgv);
                    }
                    else
                    {
                        checkRight(currentIndex, dgv);
                        checkDown(currentIndex, dgv);
                        checkLeft(currentIndex, dgv);
                    }
                }

                else if (isBottom)
                {
                    if (isLeft)
                    {
                        checkRight(currentIndex, dgv);
                        checkUp(currentIndex, dgv);
                    }
                    else if (isRight)
                    {
                        checkLeft(currentIndex, dgv);
                        checkUp(currentIndex, dgv);
                    }
                    else
                    {
                        checkRight(currentIndex, dgv);
                        checkLeft(currentIndex, dgv);
                        checkUp(currentIndex, dgv);
                    }
                }

                else
                {
                    if (isLeft)
                    {
                        checkRight(currentIndex, dgv);
                        checkDown(currentIndex, dgv);
                        checkUp(currentIndex, dgv);
                    }
                    else if (isRight)
                    {
                        checkDown(currentIndex, dgv);
                        checkLeft(currentIndex, dgv);
                        checkUp(currentIndex, dgv);
                    }
                    else
                    {
                        checkRight(currentIndex, dgv);
                        checkDown(currentIndex, dgv);
                        checkLeft(currentIndex, dgv);
                        checkUp(currentIndex, dgv);
                    }
                }
            }
        }

        // --------------------------------[DFS ALGORITHM]--------------------------------------
        public void DFSSolver(DataGridView dgv, List<string> pathTracker)
        {
            queueToVisit.Enqueue(startIndex);
            dgv[startIndex[1], startIndex[0]].Style.BackColor = GetColor(adj[startIndex[0]][startIndex[1]]);
            dgv.Refresh();
            Thread.Sleep(700);
            adj[startIndex[0]][startIndex[1]]++;
            Stack<int[]> visitedIndex = new Stack<int[]>();
            int[] currentIndex;
            while (queueToVisit.Count != 0 && treasureCount != 0)
            {
                currentIndex = queueToVisit.Dequeue();
                visitedIndex.Push(currentIndex);
                findPathDFS(currentIndex, dgv, pathTracker, visitedIndex);
            }
            while (queueToVisit.Count == 0 && treasureCount != 0)
            {
                //backtrack
                currentIndex = visitedIndex.Pop();
                dgv[visitedIndex.Peek()[1], visitedIndex.Peek()[0]].Style.BackColor = GetColor(adj[visitedIndex.Peek()[0]][visitedIndex.Peek()[1]]);
                dgv.Refresh();
                Thread.Sleep(700);
                backtrackDFSHandler(currentIndex, visitedIndex);
                currentIndex = visitedIndex.Peek();
                findPathDFS(currentIndex, dgv, pathTracker, visitedIndex);
            }
            if (treasureCount == 0)
            {
                foreach (string s in trackPath)
                {
                    pathTracker.Add(s);
                }
            }
        }

        public void findPathDFS(int[] currentIndex, DataGridView dgv, List<string> pathTracker, Stack<int[]> visitedIndex)
        {
            checkPosition(currentIndex);

            if (isTop)
            {
                if (isLeft)
                {
                    checkTopLeft(currentIndex, dgv);
                }
                else if (isRight)
                {
                    checkTopRight(currentIndex, dgv);
                }
                else
                {
                    checkTopMiddle(currentIndex, dgv);
                }
            }

            else if (isBottom)
            {
                if (isLeft)
                {
                    checkBottomLeft(currentIndex, dgv);
                }
                else if (isRight)
                {
                    checkBottomRight(currentIndex, dgv);
                }
                else
                {
                    checkBottomMiddle(currentIndex, dgv);
                }
            }
            else
            {
                if (isLeft)
                {
                    checkMiddleLeft(currentIndex, dgv);
                }
                else if (isRight)
                {
                    checkMiddleRight(currentIndex, dgv);
                }
                else
                {
                    checkMiddle(currentIndex, dgv);
                }
            }
        }

        public void backtrackDFSHandler(int[] currentIndex, Stack<int[]> visitedIndex)
        {
            if (currentIndex[1] > visitedIndex.Peek()[1])
            {
                trackPath.Add("Left");
                nodeVisited++;
            }
            else if (currentIndex[1] < visitedIndex.Peek()[1])
            {
                trackPath.Add("Right");
                nodeVisited++;
            }
            else if (currentIndex[0] > visitedIndex.Peek()[0])
            {
                trackPath.Add("Up");
                nodeVisited++;
            }
            else if (currentIndex[0] < visitedIndex.Peek()[0])
            {
                trackPath.Add("Down");
                nodeVisited++;
            }
        }

        // ---------------------------[MATRIX CHECKER]---------------------------
        // BFS Checker
        public void checkRight(int[] currentIndex, DataGridView dgv)
        {
            if (adj[currentIndex[0]][currentIndex[1] + 1] == 1)
            {
                trackPath.Add("Right");
                checkNeighbor(dgv, currentIndex[0], currentIndex[1] + 1);
                nodeVisited++;
            }
        }

        public void checkDown(int[] currentIndex, DataGridView dgv)
        {
            if (adj[currentIndex[0]+1][currentIndex[1]] == 1)
            {
                trackPath.Add("Down");
                checkNeighbor(dgv, currentIndex[0]+1, currentIndex[1]);
                nodeVisited++;
            }
        }

        public void checkLeft(int[] currentIndex, DataGridView dgv)
        {
            if (adj[currentIndex[0]][currentIndex[1]-1] == 1)
            {
                trackPath.Add("Left");
                checkNeighbor(dgv, currentIndex[0], currentIndex[1]-1);
                nodeVisited++;
            }
        }

        public void checkUp(int[] currentIndex, DataGridView dgv)
        {
            if (adj[currentIndex[0]-1][currentIndex[1]] == 1)
            {
                trackPath.Add("Up");
                checkNeighbor(dgv, currentIndex[0]-1, currentIndex[1]);
                nodeVisited++;
            }
        }

        // DFS Checker
        public void checkTopLeft(int[] currentIndex, DataGridView dgv)
        {
            if (adj[currentIndex[0]][currentIndex[1] + 1] == 1)
            {
                trackPath.Add("Right");
                checkNeighbor(dgv, currentIndex[0], currentIndex[1] + 1);
                nodeVisited++;
            }
            else if (adj[currentIndex[0] + 1][currentIndex[1]] == 1)
            {
                trackPath.Add("Down");
                checkNeighbor(dgv, currentIndex[0] + 1, currentIndex[1]);
                nodeVisited++;
            }
        }

        public void checkTopRight(int[] currentIndex, DataGridView dgv)
        {
            if (adj[currentIndex[0] + 1][currentIndex[1]] == 1)
            {
                trackPath.Add("Down");
                checkNeighbor(dgv, currentIndex[0] + 1, currentIndex[1]);
                nodeVisited++;
            }
            else if (adj[currentIndex[0]][currentIndex[1] - 1] == 1)
            {
                trackPath.Add("Left");
                checkNeighbor(dgv, currentIndex[0], currentIndex[1] - 1);
                nodeVisited++;
            }
        }

        public void checkTopMiddle(int[] currentIndex, DataGridView dgv)
        {
            if (adj[currentIndex[0]][currentIndex[1] + 1] == 1)
            {
                trackPath.Add("Right");
                checkNeighbor(dgv, currentIndex[0], currentIndex[1] + 1);
                nodeVisited++;
            }
            else if (adj[currentIndex[0] + 1][currentIndex[1]] == 1)
            {
                trackPath.Add("Down");
                checkNeighbor(dgv, currentIndex[0] + 1, currentIndex[1]);
                nodeVisited++;
            }
            else if (adj[currentIndex[0]][currentIndex[1] - 1] == 1)
            {
                trackPath.Add("Left");
                checkNeighbor(dgv, currentIndex[0], currentIndex[1] - 1);
                nodeVisited++;
            }
        }

        public void checkBottomLeft(int[] currentIndex, DataGridView dgv)
        {
            if (adj[currentIndex[0]][currentIndex[1] + 1] == 1)
            {
                trackPath.Add("Right");
                checkNeighbor(dgv, currentIndex[0], currentIndex[1] + 1);
                nodeVisited++;
            }
            else if (adj[currentIndex[0] - 1][currentIndex[1]] == 1)
            {
                trackPath.Add("Up");
                checkNeighbor(dgv, currentIndex[0] - 1, currentIndex[1]);
                nodeVisited++;
            }
        }

        public void checkBottomRight(int[] currentIndex, DataGridView dgv)
        {
            if (adj[currentIndex[0]][currentIndex[1] - 1] == 1)
            {
                trackPath.Add("Left");
                checkNeighbor(dgv, currentIndex[0], currentIndex[1] - 1);
                nodeVisited++;
            }
            else if (adj[currentIndex[0] - 1][currentIndex[1]] == 1)
            {
                trackPath.Add("Up");
                checkNeighbor(dgv, currentIndex[0] - 1, currentIndex[1]);
                nodeVisited++;
            }
        }

        public void checkBottomMiddle(int[] currentIndex, DataGridView dgv)
        {
            if (adj[currentIndex[0]][currentIndex[1] + 1] == 1)
            {
                trackPath.Add("Right");
                checkNeighbor(dgv, currentIndex[0], currentIndex[1] + 1);
                nodeVisited++;
            }
            else if (adj[currentIndex[0]][currentIndex[1] - 1] == 1)
            {
                trackPath.Add("Left");
                checkNeighbor(dgv, currentIndex[0], currentIndex[1]-1);
                nodeVisited++;
            }
            else if (adj[currentIndex[0] - 1][currentIndex[1]] == 1)
            {
                trackPath.Add("Up");
                checkNeighbor(dgv, currentIndex[0] - 1, currentIndex[1]);
                nodeVisited++;
            }
        }

        public void checkMiddleLeft(int[] currentIndex, DataGridView dgv)
        {
            if (adj[currentIndex[0]][currentIndex[1] + 1] == 1)
            {
                trackPath.Add("Right");
                checkNeighbor(dgv, currentIndex[0], currentIndex[1] + 1);
                nodeVisited++;
            }
            else if (adj[currentIndex[0] + 1][currentIndex[1]] == 1)
            {
                trackPath.Add("Down");
                checkNeighbor(dgv, currentIndex[0] + 1, currentIndex[1]);
                nodeVisited++;
            }
            else if (adj[currentIndex[0] - 1][currentIndex[1]] == 1)
            {
                trackPath.Add("Up");
                checkNeighbor(dgv, currentIndex[0] - 1, currentIndex[1]);
                nodeVisited++;
            }
        }

        public void checkMiddleRight(int[] currentIndex, DataGridView dgv)
        {
            if (adj[currentIndex[0] + 1][currentIndex[1]] == 1)
            {
                trackPath.Add("Down");
                checkNeighbor(dgv, currentIndex[0] + 1, currentIndex[1]);
                nodeVisited++;
            }
            else if (adj[currentIndex[0]][currentIndex[1] - 1] == 1)
            {
                trackPath.Add("Left");
                checkNeighbor(dgv, currentIndex[0], currentIndex[1] - 1);
                nodeVisited++;
            }
            else if (adj[currentIndex[0] - 1][currentIndex[1]] == 1)
            {
                trackPath.Add("Up");
                checkNeighbor(dgv, currentIndex[0] - 1, currentIndex[1]);
                nodeVisited++;
            }
        }

        public void checkMiddle(int[] currentIndex, DataGridView dgv)
        {
            if (adj[currentIndex[0]][currentIndex[1] + 1] == 1)
            {
                trackPath.Add("Right");
                checkNeighbor(dgv, currentIndex[0], currentIndex[1] + 1);
                nodeVisited++;
            }
            else if (adj[currentIndex[0] + 1][currentIndex[1]] == 1)
            {
                trackPath.Add("Down");
                checkNeighbor(dgv, currentIndex[0] + 1, currentIndex[1]);
                nodeVisited++;
            }
            else if (adj[currentIndex[0]][currentIndex[1] - 1] == 1)
            {
                trackPath.Add("Left");
                checkNeighbor(dgv, currentIndex[0], currentIndex[1] - 1);
                nodeVisited++;
            }
            else if (adj[currentIndex[0] - 1][currentIndex[1]] == 1)
            {
                trackPath.Add("Up");
                checkNeighbor(dgv, currentIndex[0] - 1, currentIndex[1]);
                nodeVisited++;
            }
        }


        // ---------------------------[NEIGHBORHOOD]---------------------------
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

        // ---------------------------[UTILITIES]---------------------------
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