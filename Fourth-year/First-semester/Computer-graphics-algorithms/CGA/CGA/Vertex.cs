using System;

namespace CGA
{
    class Vertex
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public double W { get; set; }



        public double VertexTextureU { get; set; }
        public double VertexTextureV { get; set; }
        public double VertexTextureW { get; set; }



        public double VertexNormalI { get; set; }
        public double VertexNormalJ { get; set; }
        public double VertexNormalK { get; set; }



        //
        public Vertex Normal { get; set; }
        public bool Visible { get; set; }

        public Vertex()
        {
            W = 1;
        }

        public Vertex(double X, double Y, double Z, double W)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
            this.W = W;
        }

        public Vertex(double X, double Y, double Z, double W, Vertex Normal)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
            this.W = W;
            this.Normal = Normal;
        }

        public Vertex(double X, double Y, double Z, double W, double VertexNormalI, double VertexNormalJ, double VertexNormalK)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
            this.W = W;
            this.VertexNormalI = VertexNormalI;
            this.VertexNormalJ = VertexNormalJ;
            this.VertexNormalK = VertexNormalK;
        }

        public Vertex(double X, double Y, double Z)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
            this.W = 1;
        }

        public Vertex(double X, double Y, double Z, Vertex Normal)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
            this.W = 1;
            this.Normal = Normal;
        }

        public Vertex NormalizeNormal()
        {
            double invLength = 1 / Math.Sqrt(VertexNormalI * VertexNormalI + VertexNormalJ * VertexNormalJ + VertexNormalK * VertexNormalK);
            VertexNormalI = VertexNormalI * invLength;
            VertexNormalJ = VertexNormalJ * invLength;
            VertexNormalK = VertexNormalK * invLength;
            return new Vertex(X * invLength, Y * invLength, Z * invLength, W);
        }

        public Vertex Clone()
        {
            //return new Vertex(X, Y, Z, W, VertexNormalI, VertexNormalJ, VertexNormalK);
            return new Vertex(X, Y, Z, W, Normal);
        }

        public static Vertex Reflect(Vertex v1, Vertex v2) //v1 относительно v2
        {
            double scalarMlt = v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
            return new Vertex(v1.X - 2 * v2.X * scalarMlt, v1.Y - 2 * v2.Y * scalarMlt, v1.Z - 2 * v2.Z * scalarMlt);
        }



        public Vertex Normalize()
        {
            double invLength = 1 / Math.Sqrt(X * X + Y * Y + Z * Z);
            return new Vertex(X * invLength, Y * invLength, Z * invLength, W);
        }


        public Vertex Subtraction(Vertex CurrentSubtrahend)
        {
            return new Vertex(X - CurrentSubtrahend.X, Y - CurrentSubtrahend.Y, Z - CurrentSubtrahend.Z, W - CurrentSubtrahend.W);
        }

        public Vertex Addition(Vertex CurrentTerm)
        {
            return new Vertex(X + CurrentTerm.X, Y + CurrentTerm.Y, Z + CurrentTerm.Z, W + CurrentTerm.W);
        }

        public Vertex VectorMultiplication(Vertex CurrentMult)
        {
            return new Vertex(Y * CurrentMult.Z - Z * CurrentMult.Y, -(X * CurrentMult.Z - Z * CurrentMult.X), X * CurrentMult.Y - Y * CurrentMult.X, W);
        }

        public double ScalarMultiplication(Vertex CurrentMult)
        {
            return X * CurrentMult.X + Y * CurrentMult.Y + Z * CurrentMult.Z;
        }
    }
}
