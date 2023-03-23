using System;
using System.IO;
using System.Windows.Forms;

namespace ChumBucketProject
{
    class AppHandler
    {
        public GridHandler gridHandler { get; set; }
        public Printer printer;
        public Parser parser;
        public Game game;
        public void InitNewAppHandler(DataGridView dataGridView)
        {
            gridHandler = new GridHandler(dataGridView);
            printer = new Printer(gridHandler);
        }

        public void GetNewGrid(TextReader reader, int numRows, int numCols)
        {
            //gridHandler.ClearGrid()
            parser = new Parser(reader);
            if (parser.TryGetFile(numRows, numCols, out Game newGame))
            {
                game = newGame;
                printer.PrintPathGrid(game, numRows, numCols);
            }
            else
            {
                ShowMessageBoxFileFail();
            }
        }

        private void ShowMessageBoxImpossiblePath()
        {
            MessageBox.Show("This path is impossible to solve");
        }

        private void ShowMessageBoxSolved()
        {
            MessageBox.Show("This path is already solved");
        }

        public void ShowMessageBoxFileFail()
        {
            MessageBox.Show("File error! Make sure the file contains the right input");
        }

        public void ShowMessageBoxLoadError()
        {
            MessageBox.Show("Please load the correct file first");
        }
    }
}