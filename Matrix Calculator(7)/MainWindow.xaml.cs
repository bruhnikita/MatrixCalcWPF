using System;
using System.Windows;
using System.Windows.Controls;
using System.Data;

namespace MatrixCalculator
{
    public partial class MainWindow : Window
    {
        private int rows, cols;
        private DataTable matrix1, matrix2;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void CreateMatrices_Click(object sender, RoutedEventArgs e)
        {
            
            if (int.TryParse(RowsTextBox.Text, out rows) && int.TryParse(ColumnsTextBox.Text, out cols))
            {
 
                matrix1 = CreateEmptyMatrix(rows, cols);
                matrix2 = CreateEmptyMatrix(rows, cols);


                Matrix1DataGrid.ItemsSource = matrix1.DefaultView;
                Matrix2DataGrid.ItemsSource = matrix2.DefaultView;
            }
            else
            {
                MessageBox.Show("Некорректный ввод размеров матрицы!");
            }
        }


        private DataTable CreateEmptyMatrix(int rows, int cols)
        {
            DataTable table = new DataTable();
            for (int i = 0; i < cols; i++)
            {
                table.Columns.Add(i.ToString(), typeof(double));
            }
            for (int i = 0; i < rows; i++)
            {
                table.Rows.Add(table.NewRow());
            }
            return table;
        }


        private double[,] DataTableToArray(DataTable table)
        {
            int rows = table.Rows.Count;
            int cols = table.Columns.Count;
            double[,] array = new double[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    array[i, j] = Convert.ToDouble(table.Rows[i][j]);
                }
            }

            return array;
        }


        private DataTable ArrayToDataTable(double[,] array)
        {
            int rows = array.GetLength(0);
            int cols = array.GetLength(1);
            DataTable table = new DataTable();

            for (int i = 0; i < cols; i++)
            {
                table.Columns.Add(i.ToString(), typeof(double));
            }
            for (int i = 0; i < rows; i++)
            {
                DataRow row = table.NewRow();
                for (int j = 0; j < cols; j++)
                {
                    row[j] = array[i, j];
                }
                table.Rows.Add(row);
            }

            return table;
        }

  
        private void AddMatrices_Click(object sender, RoutedEventArgs e)
        {
            double[,] matrix1Array = DataTableToArray(matrix1);
            double[,] matrix2Array = DataTableToArray(matrix2);

            if (matrix1Array.GetLength(0) == matrix2Array.GetLength(0) && matrix1Array.GetLength(1) == matrix2Array.GetLength(1))
            {
                double[,] result = new double[rows, cols];

                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        result[i, j] = matrix1Array[i, j] + matrix2Array[i, j];
                    }
                }

                ResultDataGrid.ItemsSource = ArrayToDataTable(result).DefaultView;
            }
            else
            {
                MessageBox.Show("Матрицы должны иметь одинаковые размеры для сложения.");
            }
        }

       
        private void MultiplyMatrices_Click(object sender, RoutedEventArgs e)
        {
            double[,] matrix1Array = DataTableToArray(matrix1);
            double[,] matrix2Array = DataTableToArray(matrix2);

            if (matrix1Array.GetLength(1) == matrix2Array.GetLength(0))
            {
                double[,] result = new double[rows, matrix2Array.GetLength(1)];

                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < matrix2Array.GetLength(1); j++)
                    {
                        result[i, j] = 0;
                        for (int k = 0; k < cols; k++)
                        {
                            result[i, j] += matrix1Array[i, k] * matrix2Array[k, j];
                        }
                    }
                }

                ResultDataGrid.ItemsSource = ArrayToDataTable(result).DefaultView;
            }
            else
            {
                MessageBox.Show("Количество столбцов в Матрице 1 должно быть равно количеству строк в Матрице 2 для умножения.");
            }
        }

     
        private void TransposeMatrix1_Click(object sender, RoutedEventArgs e)
        {
            double[,] matrix1Array = DataTableToArray(matrix1);
            double[,] result = new double[cols, rows];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    result[j, i] = matrix1Array[i, j];
                }
            }

            ResultDataGrid.ItemsSource = ArrayToDataTable(result).DefaultView;
        }
    }
}
