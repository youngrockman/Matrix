using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;
using MatrixProject.Exceptions;

namespace MatrixProject;

public struct Matrix
{
    private double[,] _data;
    
    public int Rows { get => _data.GetLength(0);}

    public int Columns { get => _data.GetLength(1);}

    public static Matrix operator +(Matrix matrix, Matrix matrix1) {
        if (matrix.Rows != matrix1.Rows || matrix.Columns != matrix.Columns) throw new AdditionException("Строки и столбцы не совпыдают"); 
            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Columns; j++)
                {
                    matrix._data[i, j] += matrix1._data[i, j];
                }
            }
            return matrix;
    }

    public static Matrix operator *(Matrix matrix, int scalar) {
        
        for (int i = 0; i < matrix.Rows; i++)
        {
            for (int j = 0; j < matrix.Columns; j++)
            {
                matrix._data[i, j] = matrix._data[i, j] * scalar;
            }
        }
        return matrix;
    }

    public Matrix(int rows) {

        _data = new double[rows, rows];
    }

    public Matrix(double[,] data) {

        _data = data;

    }

    public Matrix(int rows, int columns) {

        _data = new double[rows, columns];
    }

    public Matrix(int rows, int columns, int fillNumber) {
        
        _data = new double[rows,columns];
        FillMatrixByNumber(fillNumber);
    }

    private static double MultVector(double[] vectoreOne, double[] vectorTwo){
        double res = 0;
        if (vectoreOne.Length!=vectorTwo.Length) throw new MultVectorException("строки не совпадают");
        for (int i = 0; i < vectoreOne.Length; i++){
            res += vectoreOne[i] * vectorTwo[i];
        }

        return res;
    }

    public double[] GetRowByIndex(int row){
        var rows = new double[Columns];
        for(int j = 0; j < Columns; j++){
            rows[j] = _data[row, j]; 
        }

        return rows;
    }

    public double[] GetColumnByIndex(int row){ 
        var columns = new double[Rows];
        for(int j = 0; j < Columns; j++){
            columns[j] = _data[j, row]; 
        }
        return columns;
    }

    public static double[,] operator *(Matrix matrixOne, Matrix matrixTwo){
        
        if (matrixOne.Columns != matrixTwo.Rows) throw new Exception();
        double[,] result = new double[matrixOne.Rows, matrixTwo.Columns];
        for (int i = 0; i < result.GetLength(0); i++) {
            for (int j = 0; j < result.GetLength(1); j++) {
                result [i, j] = MultVector(matrixOne.GetColumnByIndex(i), matrixTwo.GetRowByIndex(j));
            }
        }
        return result;
    }

    public void ConvertToDiagonaleMatrix(Matrix matrix) {

        (int rows, int columns) = (matrix.Rows, matrix.Columns);
        for (int kIndex = 0; kIndex < rows; kIndex++) {
            for (int i = kIndex + 1; i < rows; i++) {
                double koef  = matrix[i, kIndex] / matrix[kIndex, kIndex];
                for (int j = kIndex; j < columns; j++) {
                    matrix[i, j] -= matrix[kIndex, j] * koef;
                }
            }
        }
    }

    public double GetDeterminantMatrix(Matrix matrix) {

        double res = 1;
        
        for (int i = 0; i < matrix.Rows; i++) {
            res *= matrix[i, i];
        }
        return res;
    }

    public override string ToString() 
    { 
        StringBuilder stringBuilder = new StringBuilder(); 
        for(int i = 0; i < Rows; i++){ 
            for(int j = 0; j < Columns; j++){ 
                stringBuilder.Append(_data[i, j]); 
                stringBuilder.Append(" "); 
            } 
            stringBuilder.AppendLine(""); 
        } 
        return stringBuilder.ToString(); 
    }

    public double this[int row, int column] {
        get => _data[row,column];
        set => _data[row,column] = value;
    }

    private void FillMatrixByNumber(int number) {
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Columns; j++)
            {
                _data[i, j] = number;
            }
        }
    }
}
