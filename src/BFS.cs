using System;
using System.Collections.Generic;

class BFS
{
    private int V; 
    private List<int>[] adj; 

    // Konstruktor untuk inisialisasi jumlah vertex dan adjacency
    public BFS(int v)
    {
        V = v;
        adj = new List<int>[v];
        for (int i = 0; i < v; ++i)
            adj[i] = new List<int>();
    }

    // Fungsi untuk menambahkan edge ke graph
    public void AddEdge(int v, int w)
    {
        adj[v].Add(w);
    }

    // Fungsi BFS traversal dari source
    public void BFS_Traversal(int s)
    {
        bool[] visited = new bool[V]; 
        Queue<int> queue = new Queue<int>(); 

        visited[s] = true; 
        queue.Enqueue(s); 

        while (queue.Count != 0) 
        {
            s = queue.Dequeue();

            Console.Write(s + " "); 

            // Kunjungi semua vertex yang bertetangga dengan vertex s
            foreach (int i in adj[s])
            {
                if (!visited[i])
                {
                    visited[i] = true; 
                    queue.Enqueue(i); 
                }
            }
        }
    }
}