# ChumBucket
Tugas Besar 2 Strategi Algoritma - Pengaplikasian Algoritma BFS dan DFS dalam Menyelesaikan Persoalan Maze Treasure Hunt

## Daftar Isi
* [Deskripsi Singkat Program](#deskripsi-singkat-program)
* [Struktur Program](#struktur-program)
* [Requirement Program](#requirement-program)
* [Cara Kompilasi Program](#cara-kompilasi-program)
* [Cara Menjalankan Program](#cara-menjalankan-program)
* [Authors](#authors)

## Deskripsi Singkat Program

Dalam tugas besar ini, Anda akan diminta untuk membangun sebuah aplikasi dengan GUI sederhana
yang dapat mengimplementasikan BFS dan DFS untuk mendapatkan rute memperoleh seluruh
treasure atau harta karun yang ada. Program dapat menerima dan membaca input sebuah file txt
yang berisi maze yang akan ditemukan solusi rute mendapatkan treasure-nya. Untuk mempermudah,
batasan dari input maze cukup berbentuk segi-empat dengan spesifikasi simbol sebagai berikut :
● K : Krusty Krab (Titik awal)
● T : Treasure
● R : Grid yang mungkin diakses / sebuah lintasan
● X : Grid halangan yang tidak dapat diakses

Dengan memanfaatkan algoritma Breadth First Search (BFS) dan Depth First Search (DFS), anda dapat menelusuri grid (simpul) yang mungkin dikunjungi hingga ditemukan rute solusi, baik secara melebar ataupun mendalam bergantung alternatif algoritma yang dipilih. Rute solusi adalah rute yang memperoleh seluruh treasure pada maze. Perhatikan bahwa rute yang diperoleh dengan algoritma BFS dan DFS dapat berbeda, dan banyak langkah yang dibutuhkan pun menjadi berbeda. Prioritas arah simpul yang dibangkitkan dibebaskan asalkan ditulis di laporan ataupun readme, semisal LRUD (left right up down). Tidak ada pergerakan secara diagonal. Anda juga diminta untuk
memvisualisasikan input txt tersebut menjadi suatu grid maze serta hasil pencarian rute solusinya. Cara visualisasi grid dibebaskan, sebagai contoh dalam bentuk matriks yang ditampilkan dalam GUI dengan keterangan berupa teks atau warna. Pemilihan warna dan maknanya dibebaskan ke masing - masing kelompok, asalkan dijelaskan di readme / laporan.

Daftar input maze akan dikemas dalam sebuah folder yang dinamakan test dan terkandung dalam repository program. Folder tersebut akan setara kedudukannya dengan folder src dan doc
(struktur folder repository akan dijelaskan lebih lanjut di bagian bawah spesifikasi tubes). Cara input maze boleh langsung input file atau dengan textfield sehingga pengguna dapat mengetik
nama maze yang diinginkan. Apabila dengan textfield, harus menghandle kasus apabila tidak
ditemukan dengan nama file tersebut.

Setelah program melakukan pembacaan input, program akan memvisualisasikan gridnya terlebih dahulu tanpa pemberian rute solusi. Hal tersebut dilakukan agar pengguna dapat mengerjakan terlebih dahulu treasure hunt secara manual jika diinginkan. Kemudian, program menyediakan tombol solve untuk mengeksekusi algoritma DFS dan BFS. Setelah tombol diklik,
program akan melakukan pemberian warna pada rute solusi.

## Struktur Program

    .
    │   README.md
    │
    ├───bin
    │       ChumBucketProject.exe 
    │       ChumBucketProject.exe.config
    │       ChumBucketProject.pdb
    │
    ├───doc
    │       Tubes2_K1_13521161_ChumBucket.pdf
    │
    └───src
        │   BFS.cs
        │   DFS.cs
        │
        └───ChumBucketProject
            │   App.config
            │   AppHandler.cs
            │   BFS.cs
            │   Cell.cs
            │   ChumBucketProject.csproj
            │   ChumBucketProject.sln
            │   Form1.cs
            │   
            ├───Properties
            │   AssemblyInfo.cs
            │   Resources.Designer.cs
            │   Resources.resx
            │   Settings.Designer.cs
            │   Settings.settings
            ├───bin 
            │   
            
## Requirement Program
    - Windows Operating System
    - .NET
    - Visual Studio 2022
    - MSAGL
    - WPF

## Cara Kompilasi Program
1. Lakukan git clone terhadap repositori ini
2. Buka Solution `WinFormsApp1.sln` dari repositori ini
3. Run program dengan menggunakan tombol Run pada Visual Studio 2022

## Cara Menjalankan Program
1. Buka folder `bin/Release/net6.0-windows` pada folder repositori
2. Jalankan file `ChumBucketProject.exe`

## Authors

| NIM      | NAMA                        |
|----------|-----------------------------|
| 13521161 | Ferindya Aulia Berlianty    |
| 13521164 | Akhmad Setiawan             |
| 13521167 | Irgiansyah Mondo            |
