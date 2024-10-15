using MatrixProject.Exceptions;

namespace MatrixProject.Test;

public class UnitTest1
{
    [InlineData(10)]
    [InlineData(5)]
    [InlineData(2)]
    [InlineData(3)]
    [Theory]
    public void CheckRowsBeforeInitMatrix(int size)
    {
        Matrix matrix = new Matrix(size);

        Assert.Equal(size, matrix.Rows);
        Assert.Equal(size, matrix.Columns);

    }
    [InlineData(10, 10)]
    [InlineData(5, 5)]
    [InlineData(2, 2)]
    [InlineData(3, 3)]
    [Theory]
    public void CheckColumnsBeforeInitMatrix(int rows, int columns)
    {
        Matrix matrix = new Matrix(rows, columns);

        Assert.Equal(columns, matrix.Columns);
    }
    [Theory]
    [MemberData(nameof(testTwoDimArray))]

    public void CheckFillMatrixInDefaultConstructor(int rows, int columns, int fillNumber, double[,] testArray) {
        
        Matrix matrix = new Matrix(rows, columns, fillNumber);

        Assert.True(EqualsTwoDimArray(testArray, matrix));
    }

    public static TheoryData<int, int,int, double[,]> testTwoDimArray => new () {

         {2, 2, 3, new double[,] { { 3, 3 }, { 3, 3 } } }
    };

    private bool EqualsTwoDimArray(double[,] testMatrix, Matrix matrix) {
        if (testMatrix.GetLength(0) != matrix.Rows || testMatrix.GetLength(1) != matrix.Columns) return false;

        for (int i = 0; i < matrix.Rows; i++)
        {
            for (int j = 0; j < matrix.Columns; j++)
            {
                if (testMatrix[i, j] != matrix[i, j]) return false;
            }
        }
        return true;
    }
    [Theory]
    [MemberData(nameof(testMultiplyDigit))]
    public void CheackMultiplyMatrixByDigit(int row, int column, int fillNumber, int scalar, double[,] testArray) {

        Matrix matrix = new Matrix(row, column, fillNumber);
        matrix *= scalar;
        Assert.True(EqualsTwoDimArray(testArray, matrix));

    }
    public static TheoryData<int, int, int, int, double[,]> testMultiplyDigit => new () {

        { 2, 2, 2, 2, new double[,] { { 4, 4 }, { 4, 4 } } }
    };
    [Theory]
    [MemberData(nameof(testSumException))]
    public void CheckSumWithException(Matrix matrix, Matrix matrix1) {

        Assert.Throws<AdditionException>(() => matrix += matrix1);

    }
    public static TheoryData<Matrix, Matrix> testSumException => new() {
        {new Matrix(4, 3), new Matrix(3, 4)}
    };
    [Theory]
    [MemberData(nameof(testSum))]
    public void CheckSum(Matrix matrix, Matrix matrix1, double[,] testArray) {

        matrix += matrix1;
        Assert.True(EqualsTwoDimArray(testArray, matrix));
    }
    public static TheoryData<Matrix, Matrix, double[,]> testSum => new() {

        { new Matrix(2, 2, 3), new Matrix(2, 2, 2), new double[,] {{5, 5}, {5, 5}}}
    };
    [Theory]
    [MemberData(nameof(testGetRow))]
    public void CheckGetRowByIndex(Matrix matrix, int row, double[] resultRow) {

        Assert.Equal(matrix.GetRowByIndex(row), resultRow);
    }
    public static TheoryData<Matrix, int, double[]> testGetRow => new()
    {
        { new Matrix(2, 2, 2), 0, new double[] {2, 2}}
    };
    [Theory]
    [MemberData(nameof(testGetColumn))]
    public void CheckGetColumnByIndex(Matrix matrix, int column, double[] resultColumn) {

        Assert.Equal(matrix.GetColumnByIndex(column), resultColumn);

    }
    public static TheoryData<Matrix, int, double[]> testGetColumn => new()
    {
        { new Matrix(2, 2, 2), 0, new double[] {2, 2}}
    };
    [Theory]
    [MemberData(nameof(testMultiplyTwoMatrix))]
    public void CheckMultiplyTwoMatrix(Matrix matrixOne, Matrix matrixTwo, double[,] testMultiply) {
        
        double[,] result = matrixOne * matrixTwo;

        Assert.Equal(result, testMultiply);

    }
    public static TheoryData<Matrix, Matrix, double[,]> testMultiplyTwoMatrix => new()
    {
        { new Matrix(2, 2, 2), new Matrix(2, 2, 3), new double[,] {{12, 12}, {12, 12}}}
    };
    [Theory]
    [MemberData(nameof(testDiagonale))]
    public void CheckDiagonaleMatrix(Matrix matrix, double[,] answer) {

        matrix.ConvertToDiagonaleMatrix(matrix);
        Assert.True(EqualsTwoDimArray(answer, matrix));

    }
    public static TheoryData<Matrix, double[,]> testDiagonale => new()
    {
        { new Matrix( new double[,]{{20, 30, 30}, {10, 20, 30}, {10, 40, 20}}), new double[,] { {20, 30, 30}, {0, 5, 15}, {0, 0, -70}}}
    };
    [Theory]
    [MemberData(nameof(testDeterminant))]
    public void CheckDeterminantMatrix(Matrix matrix, double answer) {

        matrix.ConvertToDiagonaleMatrix(matrix);
        double res = matrix.GetDeterminantMatrix(matrix);

        Assert.Equal(answer, res);

    }
    public static TheoryData<Matrix, double> testDeterminant => new()
    {
        { new Matrix( new double[,]{{20, 30, 30}, {10, 20, 30}, {10, 40, 20}}), -7000}
    };
    [Theory]
    [MemberData(nameof(testToString))]
    public void CheckToStringMatrix(Matrix matrix, string answer) {

        string res = matrix.ToString();
        Assert.Equal(answer, res);

    }
    public static TheoryData<Matrix, string> testToString => new()
    {
        { new Matrix(3, 3, 2), "2 2 2 \n2 2 2 \n2 2 2 \n"}
    };
}
