using System;
using System.Collections.Generic;

class DFS{
    private int V;
    private List<int>[] adj; 

    // Konstruktor untuk inisialisasi jumlah vertex dan adjacency
    public DFS(int v)
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

    // Implementasi DFS
    public void DFS_Traversal (int s){
        bool[] visited = new bool[V];
        DFS_rekursif(s, visited);
    }

    private void DFS_rekursif(int v, bool[] visited){
        visited[v] = true;
        Console.Write(v + "");

        // Menelusuri ke bagian dalam vertex

        foreach (int i in adj[v]){
            if (!visited[i])
                DFS_rekursif(i, visited);
        }
    }
}