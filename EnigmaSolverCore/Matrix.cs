using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnigmaSolverCore
{
    class Matrix
    {
        private int[,] data;

        public Matrix(int rows, int cols)
        {
            data = new int[rows, cols];
        }

        public int this[int row, int col]
        {
            get { return data[row, col]; }
            set { data[row, col] = value; }
        }

        public int Rows => data.GetLength(0);
        public int Cols => data.GetLength(1);

        public void Display()
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    Console.Write(data[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        // Set a specific row
        public void SetRow(int rowIndex, int[] newRow)
        {
            if (newRow.Length != Cols)
            {
                throw new ArgumentException("The new row must have the same number of columns as the matrix.");
            }

            if (rowIndex < 0 || rowIndex >= Rows)
            {
                throw new IndexOutOfRangeException("Row index is out of range.");
            }

            for (int j = 0; j < Cols; j++)
            {
                data[rowIndex, j] = newRow[j];
            }
        }

        // Set a specific column
        public void SetColumn(int colIndex, int[] newColumn)
        {
            if (newColumn.Length != Rows)
            {
                throw new ArgumentException("The new column must have the same number of rows as the matrix.");
            }

            if (colIndex < 0 || colIndex >= Cols)
            {
                throw new IndexOutOfRangeException("Column index is out of range.");
            }

            for (int i = 0; i < Rows; i++)
            {
                data[i, colIndex] = newColumn[i];
            }
        }
    }
}
