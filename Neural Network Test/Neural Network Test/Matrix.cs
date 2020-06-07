using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neural_Network_Test
{
    class Matrix
    {
        #region Private Members

        private int rows;
        private int columns;
        private float[,] matrixx;

        #endregion

        #region Public Accessors

        public int Rows { get => rows; set => rows = value; }
        public int Columns { get => columns; set => columns = value; }
        public float[,] matrix { get => matrixx; set => matrixx = value; }

        #endregion

        #region Constructors

        public Matrix(int mRows, int mColumns)
        {
            Rows = mRows;
            Columns = mColumns;
            matrix = new float[Rows, Columns];
        }

        public Matrix(float[,] mMatrix)
        {
            matrix = mMatrix;
            Rows = matrix.GetLength(0);
            Columns = matrix.GetLength(1);
        }

        #endregion

        #region Private Methods

        private static Matrix Add(Matrix m1, Matrix m2)
        {
            if (m2.matrix == null || m1.matrix == null)
                return null;
            //invalid vector sum
            if (!(m2.Columns == m1.Columns && m2.Rows == m1.Rows))
                return null;
            //now we have two valid vectors of the same size
            Matrix outMatrix = new Matrix(m2.Rows, m2.Columns);
            for (int i = 0; i < m2.matrix.GetLength(0); i++)
            {
                for (int j = 0; j < m2.matrix.GetLength(1); j++)
                {
                    outMatrix.matrix[i,j] = m2.matrix[i, j] + m1.matrix[i,j];
                }
            }
            return outMatrix;
        }

        private static Matrix Neg(Matrix m)
        {
            return ScalarMult(m, -1f);
        }

        private static Matrix ScalarMult(Matrix m, float s)
        {
            if (m.matrix == null)
                return null;
            Matrix outMatrix = new Matrix(m.Rows, m.Columns);
            for (int i = 0; i < m.matrix.GetLength(0); i++)
            {
                for (int j = 0; j < m.matrix.GetLength(1); j++)
                {
                    outMatrix.matrix[i, j] = m.matrix[i, j] * s;
                }
            }
            return outMatrix;
        }

        private static Matrix MatrixMult(Matrix m1, Matrix m2)
        {
            if (m2.matrix == null || m1.matrix == null)
                return null;
            if (!(m1.Columns == m2.Rows))
                return null;
            Matrix outMatrix = new Matrix(m1.Rows, m2.Columns);
            for (int i = 0; i < outMatrix.matrix.GetLength(0); i++)
            {
                for (int j = 0; j < outMatrix.matrix.GetLength(1); j++)
                {
                    float sum = 0f;
                    for (int k = 0; k < m1.Columns; k++)
                    {
                        sum += m1.matrix[i, k] * m2.matrix[k, j];
                    }
                    outMatrix.matrix[i, j] = sum; 
                }
            }
            return outMatrix;
        }

        #endregion

        #region Public Methods

        public static Matrix Identity(int length)
        {
            Matrix outMatrix = new Matrix(length, length);
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    if (i == j)
                        outMatrix.matrix[i, j] = 1;
                    else
                        outMatrix.matrix[i, j] = 0;
                }
            }
            return outMatrix;
        }

        #endregion

        #region Operators

        public static bool operator ==(Matrix m1, Matrix m2)
        {
            if (m2.matrix == null || m1.matrix == null)
                return false;

            if (!(m2.Columns == m1.Columns && m2.Rows == m1.Rows))
                return false;

            bool equal = true;
            for (int i = 0; i < m1.matrix.GetLength(0); i++)
            {
                for (int j = 0; j < m1.matrix.GetLength(1); j++)
                {
                    if (m2.matrix[i, j] != m1.matrix[i, j])
                        equal = false;
                }
            }
            return equal;
        }

        public static bool operator !=(Matrix m1, Matrix m2)
        {
            bool equal = m1 == m2;
            equal = !equal;
            return equal;
        }

        public static Matrix operator +(Matrix m1, Matrix m2)
        {
            return Add(m1, m2);
        }

        public static Matrix operator -(Matrix m1)
        {
            return Neg(m1);
        }

        public static Matrix operator -(Matrix m1, Matrix m2)
        {
            return Add(m1, -m2);
        }

        public static Matrix operator *(float s, Matrix m)
        {
            return ScalarMult(m, s);
        }

        public static Matrix operator *(Matrix m, float s)
        {
            return ScalarMult(m, s);
        }

        public static Matrix operator * (Matrix m1, Matrix m2)
        {
            return MatrixMult(m1, m2);
        }

        #endregion

        #region Constants 
        
        #endregion

        #region Overides

        public override string ToString()
        {
            string outString = "";
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    outString += matrix[i, j] + " ";
                }
                outString += "\r\n";
            }
            return outString;
        }

        #endregion
    }
}
