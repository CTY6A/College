using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CGA
{
    public partial class Form1 : Form
    {
        LogicalObject CurrentLogicalObject;

        Matrix TranslationMatrix;
        Matrix ScaleMatrix;
        Matrix RotationMatrixAxisX;
        Matrix RotationMatrixAxisY;
        Matrix RotationMatrixAxisZ;

        Vertex eye;
        Vertex target;
        Vertex up;
        Vertex ZAxis;
        Vertex XAxis;
        Vertex YAxis;

        Matrix ViewerMatrix;

        int width;
        int height;
        double zNear = 500;
        double zFar = 1000;

        Matrix ProjectionMatrix;
        Matrix ScreenMatrix;


        Graphics CurrentGraphics;
        Bitmap CurrentBitmap;

        ////*
        double angleX = 0;
        double angleY = 0;
        double angleZ = 0;

        Vertex view;
        Vertex light = new Vertex(1, 0, 0);
        int AMBIENT_RED = 0;
        int AMBIENT_GREEN = 0;
        int AMBIENT_BLUE = 255;
        double AMBIENT_FACTOR = 0.01;
        int DIFFUSE_RED = 255;
        int DIFFUSE_GREEN = 255;
        int DIFFUSE_BLUE = 255;
        double DIFFUSE_FACTOR = 0.7;
        int SPECULAR_RED = 255;
        int SPECULAR_GREEN = 0;
        int SPECULAR_BLUE = 0;
        double SPECULAR_FACTOR = 0.4;
        double SHINE_FACTOR = 4;
        Point po1 = new Point();
        //Point po2 = new Point();
        List<Vertex> PointsBeforeProjaction = new List<Vertex>();
        List<Vertex> PointsBeforeDrawing = new List<Vertex>();
        double[,] zBuff;    
        ///*/
        public Form1()
        {
            InitializeComponent();

            width = pbCanvas.Width;
            height = pbCanvas.Height;

            CurrentBitmap = new Bitmap(width, height);
            CurrentGraphics = Graphics.FromImage(CurrentBitmap);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            TranslationMatrix   = new Matrix(new double[4, 4]{ { 1, 0, 0, 0},
                                                               { 0, 1, 0, 0},
                                                               { 0, 0, 1, 0},
                                                               { 0, 0, 0, 1} });

            ScaleMatrix         = new Matrix(new double[4, 4]{ { 100, 0, 0, 0},
                                                               { 0, 100, 0, 0},
                                                               { 0, 0, 100, 0},
                                                               { 0, 0, 0, 1} });

            RotationMatrixAxisX = new Matrix(new double[4, 4]{ { 1, 0, 0, 0},
                                                               { 0, 1, 0, 0},
                                                               { 0, 0, 1, 0},
                                                               { 0, 0, 0, 1} });

            RotationMatrixAxisY = new Matrix(new double[4, 4]{ { 1, 0, 0, 0},
                                                               { 0, 1, 0, 0},
                                                               { 0, 0, 1, 0},
                                                               { 0, 0, 0, 1} });

            RotationMatrixAxisZ = new Matrix(new double[4, 4]{ { 1, 0, 0, 0},
                                                               { 0, 1, 0, 0},
                                                               { 0, 0, 1, 0},
                                                               { 0, 0, 0, 1} });

            ScreenMatrix        = new Matrix(new double[4, 4]{ { width/2, 0, 0, width/2},
                                                               { 0, -height/2, 0, height/2},
                                                               { 0, 0, 1, 0},
                                                               { 0, 0, 0, 1} });


            eye = new Vertex(0, 0, 250);
            target = new Vertex(0, 0, 0);
            up = new Vertex(0, 1, 0);
            FillViewerMatrix();
            FillProjectionMatrix();

            CurrentLogicalObject = new LogicalObject();
            CurrentLogicalObject.ReadFile("Model.obj");

            ////*
            view = target.Subtraction(eye);
            view = view.Normalize();
            light = light.Normalize();
            AMBIENT_RED = (int)(AMBIENT_FACTOR * AMBIENT_RED);
            AMBIENT_GREEN = (int)(AMBIENT_FACTOR * AMBIENT_GREEN);
            AMBIENT_BLUE = (int)(AMBIENT_FACTOR * AMBIENT_BLUE);
            DIFFUSE_RED = (int)(DIFFUSE_FACTOR * DIFFUSE_RED);
            DIFFUSE_GREEN = (int)(DIFFUSE_FACTOR * DIFFUSE_GREEN);
            DIFFUSE_BLUE = (int)(DIFFUSE_FACTOR * DIFFUSE_BLUE);
            SPECULAR_RED = (int)(SPECULAR_FACTOR * SPECULAR_RED);
            SPECULAR_GREEN = (int)(SPECULAR_FACTOR * SPECULAR_GREEN);
            SPECULAR_BLUE = (int)(SPECULAR_FACTOR * SPECULAR_BLUE);

            zBuff = new double[height, width];
            ///*/

            DrawModel();
        }

        

        private void DrawModel()
        {
            ////*
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    zBuff[i, j] = Double.MaxValue;
                }
            }
            ///*/

            PointsBeforeDrawing = new List<Vertex>();
            PointsBeforeProjaction = new List<Vertex>();

            CurrentGraphics.Clear(Color.White);
            for (int i = 0; i < CurrentLogicalObject.Points.Count; i++)
            {
                Vertex CurrentVertex = CurrentLogicalObject.Points[i].Clone();
                
                CurrentVertex = ScaleMatrix.Multiply(CurrentVertex);
                CurrentVertex = RotationMatrixAxisX.Multiply(CurrentVertex);
                CurrentVertex = RotationMatrixAxisY.Multiply(CurrentVertex);
                CurrentVertex = RotationMatrixAxisZ.Multiply(CurrentVertex);
                CurrentVertex = TranslationMatrix.Multiply(CurrentVertex);

                /*CurrentVertex.Normal = ScaleMatrix.Multiply(CurrentVertex.Normal);
                CurrentVertex.Normal = RotationMatrixAxisX.Multiply(CurrentVertex.Normal);
                CurrentVertex.Normal = RotationMatrixAxisY.Multiply(CurrentVertex.Normal);
                CurrentVertex.Normal = RotationMatrixAxisZ.Multiply(CurrentVertex.Normal);
                CurrentVertex.Normal = TranslationMatrix.Multiply(CurrentVertex.Normal);
                CurrentVertex.VertexNormalI = CurrentVertex.Normal.X;
                CurrentVertex.VertexNormalJ = CurrentVertex.Normal.Y;
                CurrentVertex.VertexNormalK = CurrentVertex.Normal.Z;
                CurrentVertex.NormalizeNormal();*/

                PointsBeforeProjaction.Add(CurrentVertex);
                CurrentVertex = CurrentVertex.Clone();


                CurrentVertex = ViewerMatrix.Multiply(CurrentVertex);

                CurrentVertex = ProjectionMatrix.Multiply(CurrentVertex);
                if (CurrentVertex.W != 0)
                {
                    CurrentVertex.X /= CurrentVertex.W;
                    CurrentVertex.Y /= CurrentVertex.W;
                    CurrentVertex.Z /= CurrentVertex.W;
                    CurrentVertex.W /= CurrentVertex.W;
                }

                CurrentVertex = ScreenMatrix.Multiply(CurrentVertex);
                /*
                CurrentVertex.VertexNormalI = CurrentVertex.Normal.X;
                CurrentVertex.VertexNormalJ = CurrentVertex.Normal.Y;
                CurrentVertex.VertexNormalK = CurrentVertex.Normal.Z;
                CurrentVertex.NormalizeNormal();*/

                PointsBeforeDrawing.Add(CurrentVertex);

                if (CurrentVertex.X > 0 && CurrentVertex.X < width && CurrentVertex.Y > 0 && CurrentVertex.Y < height)// && zBuff[CurrentVertex.Y, CurrentVertex.X] > CurrentVertex.Z)
                {
                    CurrentVertex.Visible = true;
                }
                else
                {
                    CurrentVertex.Visible = false;
                }
            }

            foreach (Polygon CurrentPolygon in CurrentLogicalObject.Polygons)
            {
                CurrentPolygon.Vertex1 = PointsBeforeDrawing[CurrentPolygon.Vertex1Id - 1];
                CurrentPolygon.Vertex2 = PointsBeforeDrawing[CurrentPolygon.Vertex2Id - 1];
                CurrentPolygon.Vertex3 = PointsBeforeDrawing[CurrentPolygon.Vertex3Id - 1];
                CurrentPolygon.Vertex1BeforeProjection = PointsBeforeProjaction[CurrentPolygon.Vertex1Id - 1];
                CurrentPolygon.Vertex2BeforeProjection = PointsBeforeProjaction[CurrentPolygon.Vertex2Id - 1];
                CurrentPolygon.Vertex3BeforeProjection = PointsBeforeProjaction[CurrentPolygon.Vertex3Id - 1];
                if (CheckPolygon(CurrentPolygon))
                {
                    Rasterize(CurrentPolygon);

                    //DrawLine(CurrentPolygon.Vertex1, CurrentPolygon.Vertex2);
                    //DrawLine(CurrentPolygon.Vertex2, CurrentPolygon.Vertex3);
                    //DrawLine(CurrentPolygon.Vertex3, CurrentPolygon.Vertex1);
                }
            }
            pbCanvas.Image = CurrentBitmap;
        }

        /**private void drawLine(Vertex v1, Vertex v2)
        {
            double L = Math.Max(Math.Abs(v2.X - v1.X), Math.Abs(v2.Y - v1.Y));
            for (int i = 0; i < L; i++)
            {
                po1.X = Convert.ToInt32(v1.X + (v2.X - v1.X) * i / L);
                po1.Y = Convert.ToInt32(v1.Y + (v2.Y - v1.Y) * i / L);
                if (po1.X > 0 && po1.X < width && po1.Y > 0 && po1.Y < height)
                {
                    CurrentBitmap.SetPixel(po1.X, po1.Y, Color.Black);
                }
            }
        }**/

        private bool CheckPolygon(Polygon CurrentPolygon)
        {
            Vertex v1 = CurrentPolygon.Vertex1;
            Vertex v2 = CurrentPolygon.Vertex2;
            Vertex v3 = CurrentPolygon.Vertex3;
            CurrentPolygon.CalculateNormal();
            if (v1.Visible && v2.Visible && v3.Visible)
            {
                if (CurrentPolygon.Normal.X * view.X + CurrentPolygon.Normal.Y * view.Y + CurrentPolygon.Normal.Z * view.Z > 0)
                {
                    return true;
                }
            }
            return false;
        }

        private void Rasterize(Polygon CurrentPolygon)
        {
            Vertex v1, v2, v3;
            Vertex v1BP, v2BP, v3BP;
            /*CurrentPolygon.CalculateNormalBeforeProjection(PointsBeforeProjaction);
            int lightIntensity = (int)((CurrentPolygon.Normal.X * light.X + CurrentPolygon.Normal.Y * light.Y + CurrentPolygon.Normal.Z * light.Z) * 255);
            if (lightIntensity > 255)
            {
                lightIntensity = 255;
            }
            if (lightIntensity < 0)
            {
                lightIntensity = 0;
            }
            Color color = Color.FromArgb(lightIntensity, lightIntensity, lightIntensity);*/
            int y1, x1, y2, x2, y3, x3;
            double z1, z2, z3;
            double x1BP, y1BP, z1BP, x2BP, y2BP, z2BP, x3BP, y3BP, z3BP;
            //double xBP, yBP, zBP;
            //double dxBP, dyBP, dzBP;
            double NormalX1, NormalY1, NormalZ1;
            double NormalX2, NormalY2, NormalZ2;
            double NormalX3, NormalY3, NormalZ3;

            double lNormalX, lNormalY, lNormalZ;
            double rNormalX, rNormalY, rNormalZ;

            double dlNormalX, dlNormalY, dlNormalZ;
            double drNormalX, drNormalY, drNormalZ;
            if (CurrentPolygon.Vertex1.Y < CurrentPolygon.Vertex2.Y) //выстраиваем точки по возрастанию Y
            {
                v1 = CurrentPolygon.Vertex1;
                v2 = CurrentPolygon.Vertex2;
                v1BP = CurrentPolygon.Vertex1BeforeProjection;
                v2BP = CurrentPolygon.Vertex2BeforeProjection;
            }
            else
            {
                v1 = CurrentPolygon.Vertex2;
                v2 = CurrentPolygon.Vertex1;
                v1BP = CurrentPolygon.Vertex2BeforeProjection;
                v2BP = CurrentPolygon.Vertex1BeforeProjection;
            }
            if (CurrentPolygon.Vertex3.Y > v2.Y)
            {
                v3 = CurrentPolygon.Vertex3;
                v3BP = CurrentPolygon.Vertex3BeforeProjection;
            }
            else
            {
                v3 = v2;
                v3BP = v2BP;
                if (CurrentPolygon.Vertex3.Y > v1.Y)
                {
                    v2 = CurrentPolygon.Vertex3;
                    v2BP = CurrentPolygon.Vertex3BeforeProjection;
                }
                else
                {
                    v2 = v1;
                    v1 = CurrentPolygon.Vertex3;
                    v2BP = v1BP;
                    v1BP = CurrentPolygon.Vertex3BeforeProjection;
                }
            }

            x1 = (int)v1.X;
            y1 = (int)v1.Y;
            z1 = v1.Z;
            x2 = (int)v2.X;
            y2 = (int)v2.Y;
            z2 = v2.Z;
            x3 = (int)v3.X;
            y3 = (int)v3.Y;
            z3 = v3.Z;

            x1BP = v1BP.X;
            y1BP = v1BP.Y;
            z1BP = v1BP.Z;
            x2BP = v2BP.X;
            y2BP = v2BP.Y;
            z2BP = v2BP.Z;
            x3BP = v3BP.X;
            y3BP = v3BP.Y;
            z3BP = v3BP.Z;
            //double z = (v1.Z + v2.Z + v3.Z) / 3;
            double z;

            int y = y1;
            int lx = x1;
            int rx = x1;
            int lError = 0, rError = 0;
            int lSign = (x1 < x2) ? 1 : -1;
            int rSign = (x1 < x3) ? 1 : -1;
            int sign = (x2 < x3) ? 1 : -1;
            int lLx = Math.Abs(x1 - x2);
            int rLx = Math.Abs(x1 - x3);
            int lLy = Math.Abs(y1 - y2);
            int rLy = Math.Abs(y1 - y3);
            double lz = z1;
            double rz = z1;
            double ldz = (z2 - z1) / lLy;
            double rdz = (z3 - z1) / rLy;
            double VertexNormalI, VertexNormalJ, VertexNormalK;//, avarageL, l1, l2, l3;

            NormalX1 = v1BP.Normal.X;
            NormalY1 = v1BP.Normal.Y;
            NormalZ1 = v1BP.Normal.Z;
            NormalX2 = v2BP.Normal.X;
            NormalY2 = v2BP.Normal.Y;
            NormalZ2 = v2BP.Normal.Z;
            NormalX3 = v3BP.Normal.X;
            NormalY3 = v3BP.Normal.Y;
            NormalZ3 = v3BP.Normal.Z;

            lNormalX = NormalX1;
            rNormalX = NormalX1;
            lNormalY = NormalY1;
            rNormalY = NormalY1;
            lNormalZ = NormalZ1;
            rNormalZ = NormalZ1;

            dlNormalX = (NormalX2 - NormalX1) / lLy;
            dlNormalY = (NormalY2 - NormalY1) / lLy;
            dlNormalZ = (NormalZ2 - NormalZ1) / lLy;
            drNormalX = (NormalX3 - NormalX1) / rLy;
            drNormalY = (NormalY3 - NormalY1) / rLy;
            drNormalZ = (NormalZ3 - NormalZ1) / rLy;


            while (y < y2) 
            {
                int l = Math.Abs(rx - lx);
                sign = (lx < rx) ? 1 : -1;
                po1.Y = y;
                for (int i = 0; i <= l; i++)
                {
                    double dzInCurrentLine = (rz - lz) / l;
                    double dNormalXInCurrentLine = (rNormalX - lNormalX) / l;
                    double dNormalYInCurrentLine = (rNormalY - lNormalY) / l;
                    double dNormalZInCurrentLine = (rNormalZ - lNormalZ) / l;
                    z = lz + dzInCurrentLine * i;
                    po1.X = lx + sign*i;
                    if (po1.X > 0 && po1.X < width && po1.Y > 0 && po1.Y < height&& z <= zBuff[po1.Y, po1.X])
                    {
                        VertexNormalI = lNormalX + dNormalXInCurrentLine * i;
                        VertexNormalJ = lNormalY + dNormalYInCurrentLine * i;
                        VertexNormalK = lNormalZ + dNormalZInCurrentLine * i;

                        NormalizeVector(ref VertexNormalI, ref VertexNormalJ, ref VertexNormalK);
                        double lightIntensity = VertexNormalI * light.X + VertexNormalJ * light.Y + VertexNormalK * light.Z;
                        lightIntensity = (lightIntensity > 1) ? 1 : lightIntensity;
                        lightIntensity = (lightIntensity < 0) ? 0 : lightIntensity;
                        Vertex R = Vertex.Reflect(light, new Vertex(VertexNormalI, VertexNormalJ, VertexNormalK)).Normalize();

                        int red = (int)(AMBIENT_RED + lightIntensity * DIFFUSE_RED);
                        double shining = R.ScalarMultiplication(view);
                        red = (shining > 0) ? red + (int)(Math.Pow(shining, SHINE_FACTOR) * SPECULAR_RED) : red;
                        red = (red > 255) ? 255 : red;
                        red = (red < 0) ? 0 : red;

                        int green = (int)(AMBIENT_GREEN + lightIntensity * DIFFUSE_GREEN);
                        shining = R.ScalarMultiplication(view);
                        green = (shining > 0) ? green + (int)(Math.Pow(shining, SHINE_FACTOR) * SPECULAR_GREEN) : green;
                        green = (green > 255) ? 255 : green;
                        green = (green < 0) ? 0 : green;

                        int blue = (int)(AMBIENT_BLUE + lightIntensity * DIFFUSE_BLUE);
                        shining = R.ScalarMultiplication(view);
                        blue = (shining > 0) ? blue + (int)(Math.Pow(shining, SHINE_FACTOR) * SPECULAR_BLUE) : blue;
                        blue = (blue > 255) ? 255 : blue;
                        blue = (blue < 0) ? 0 : blue;

                        Color color = Color.FromArgb(red, green, blue);
                        CurrentBitmap.SetPixel(po1.X, po1.Y, color);
                        zBuff[po1.Y, po1.X] = z;
                    }
                }

                lError += lLx;
                while (2 * lError > lLy)
                {
                    lx += lSign;
                    lError -= lLy;
                }

                rError += rLx;
                while (2 * rError > rLy)
                {
                    rx += rSign;
                    rError -= rLy;
                }

                lz += ldz;
                rz += rdz;

                lNormalX += dlNormalX;
                lNormalY += dlNormalY;
                lNormalZ += dlNormalZ;
                rNormalX += drNormalX;
                rNormalY += drNormalY;
                rNormalZ += drNormalZ;

                y++;
            }

            lError = 0;
            lSign = (x2 < x3) ? 1 : -1;
            lLx = Math.Abs(x2 - x3);
            lLy = Math.Abs(y2 - y3);
            lx = x2;

            lz = z2;
            ldz = (z3 - z2) / lLy;

            lNormalX = NormalX2;
            lNormalY = NormalY2;
            lNormalZ = NormalZ2;

            dlNormalX = (NormalX3 - NormalX2) / lLy;
            dlNormalY = (NormalY3 - NormalY2) / lLy;
            dlNormalZ = (NormalZ3 - NormalZ2) / lLy;

            while (y < y3)
            {
                int l = Math.Abs(rx - lx);
                sign = (lx < rx) ? 1 : -1;
                po1.Y = y;
                for (int i = 0; i <= l; i++)
                {
                    double dzInCurrentLine = (rz - lz) / l;
                    double dNormalXInCurrentLine = (rNormalX - lNormalX) / l;
                    double dNormalYInCurrentLine = (rNormalY - lNormalY) / l;
                    double dNormalZInCurrentLine = (rNormalZ - lNormalZ) / l;
                    z = lz + dzInCurrentLine * i;
                    po1.X = lx + sign * i;
                    if (po1.X > 0 && po1.X < width && po1.Y > 0 && po1.Y < height && z <= zBuff[po1.Y, po1.X])
                    {
                        /*l1 = Math.Sqrt((x1 - po1.X) * (x1 - po1.X) + (y1 - po1.Y) * (y1 - po1.Y));
                        l2 = Math.Sqrt((x2 - po1.X) * (x2 - po1.X) + (y2 - po1.Y) * (y2 - po1.Y));
                        l3 = Math.Sqrt((x3 - po1.X) * (x3 - po1.X) + (y3 - po1.Y) * (y3 - po1.Y));
                        l1 = (l1 == 0) ? 0.001 : l1;
                        l2 = (l2 == 0) ? 0.001 : l2;
                        l3 = (l3 == 0) ? 0.001 : l3;
                        avarageL = (l1 + l2 + l3) / 3;
                        VertexNormalI = (NormalX1 / l1 + NormalX2 / l2 + NormalX3 / l3) / 3 * avarageL;
                        VertexNormalJ = (NormalY1 / l1 + NormalY2 / l2 + NormalY3 / l3) / 3 * avarageL;
                        VertexNormalK = (NormalZ1 / l1 + NormalZ2 / l2 + NormalZ3 / l3) / 3 * avarageL;*/
                        VertexNormalI = lNormalX + dNormalXInCurrentLine * i;
                        VertexNormalJ = lNormalY + dNormalYInCurrentLine * i;
                        VertexNormalK = lNormalZ + dNormalZInCurrentLine * i;
                        NormalizeVector(ref VertexNormalI, ref VertexNormalJ, ref VertexNormalK);
                        double lightIntensity = VertexNormalI * light.X + VertexNormalJ * light.Y + VertexNormalK * light.Z;
                        lightIntensity = (lightIntensity > 1) ? 1 : lightIntensity;
                        lightIntensity = (lightIntensity < 0) ? 0 : lightIntensity;
                        Vertex R = Vertex.Reflect(light, new Vertex(VertexNormalI, VertexNormalJ, VertexNormalK)).Normalize();

                        int red = (int)(AMBIENT_RED + lightIntensity * DIFFUSE_RED);
                        double shining = R.ScalarMultiplication(view);
                        red = (shining > 0) ? red + (int)(Math.Pow(shining, SHINE_FACTOR) * SPECULAR_RED) : red;
                        red = (red > 255) ? 255 : red;
                        red = (red < 0) ? 0 : red;

                        int green = (int)(AMBIENT_GREEN + lightIntensity * DIFFUSE_GREEN);
                        shining = R.ScalarMultiplication(view);
                        green = (shining > 0) ? green + (int)(Math.Pow(shining, SHINE_FACTOR) * SPECULAR_GREEN) : green;
                        green = (green > 255) ? 255 : green;
                        green = (green < 0) ? 0 : green;

                        int blue = (int)(AMBIENT_BLUE + lightIntensity * DIFFUSE_BLUE);
                        shining = R.ScalarMultiplication(view);
                        blue = (shining > 0) ? blue + (int)(Math.Pow(shining, SHINE_FACTOR) * SPECULAR_BLUE) : blue;
                        blue = (blue > 255) ? 255 : blue;
                        blue = (blue < 0) ? 0 : blue;

                        Color color = Color.FromArgb(red, green, blue);
                        CurrentBitmap.SetPixel(po1.X, po1.Y, color);
                        zBuff[po1.Y, po1.X] = z;
                    }
                }

                lError += lLx;
                while (2 * lError > lLy)
                {
                    lx += lSign;
                    lError -= lLy;
                }

                rError += rLx;
                while (2 * rError > rLy)
                {
                    rx += rSign;
                    rError -= rLy;
                }

                lz += ldz;
                rz += rdz;

                lNormalX += dlNormalX;
                lNormalY += dlNormalY;
                lNormalZ += dlNormalZ;
                rNormalX += drNormalX;
                rNormalY += drNormalY;
                rNormalZ += drNormalZ;

                y++;
            }
        }

        public void NormalizeVector(ref double x, ref double y, ref double z)
        {
            double invLength = 1 / Math.Sqrt(x * x + y * y + z * z);
            x *= invLength;
            y *= invLength;
            z *= invLength;
        }

        private void DrawLine(Vertex v1, Vertex v2)
        {
            int Ly = (int)Math.Abs(v2.Y - v1.Y);
            int Lx = (int)Math.Abs(v2.X - v1.X);
            int x;
            int y;
            int signY;
            double z;
            double dz;
            if (v1.X < v2.X)
            {
                x = (int)v1.X;
                y = (int)v1.Y;
                z = v1.Z;
                dz = (v2.Z - v1.Z) / Lx;
                signY = (v1.Y < v2.Y) ? 1 : -1;
            }
            else
            {
                x = (int)v2.X;
                y = (int)v2.Y;
                z = v2.Z;
                dz = (v1.Z - v2.Z) / Lx;
                signY = (v2.Y < v1.Y) ? 1 : -1;
            }

            int errorY = 0;
            for (int i = 0; i < Lx; i++)
            {
                po1.X = x + i;
                po1.Y = y;
                if (po1.X > 0 && po1.X < width && po1.Y > 0 && po1.Y < height && z <= zBuff[po1.Y, po1.X])
                {
                    CurrentBitmap.SetPixel(po1.X, po1.Y, Color.Red);
                    zBuff[po1.Y, po1.X] = z;
                }
                errorY += Ly;
                while (2 * errorY > Lx)
                {
                    y += signY;
                    errorY -= Lx;
                }
                z += dz;
            }
        }

        public void TranslateCameraX(double dx)
        {
            eye.X += dx;
            FillViewerMatrix();
        }
        public void TranslateCameraY(double dy)
        {
            eye.Y += dy;
            FillViewerMatrix();
        }
        public void TranslateCameraZ(double dz)
        {
            eye.Z += dz;
            FillViewerMatrix();
        }

        public void FillViewerMatrix()
        {
            ZAxis = eye.Subtraction(target).Normalize();
            XAxis = up.VectorMultiplication(ZAxis).Normalize();
            YAxis = up.Clone();

            ViewerMatrix = new Matrix(new double[4, 4]{ { XAxis.X, XAxis.Y, XAxis.Z, -(XAxis.ScalarMultiplication(eye))},
                                                        { YAxis.X, YAxis.Y, YAxis.Z, -(YAxis.ScalarMultiplication(eye))},
                                                        { ZAxis.X, ZAxis.Y, ZAxis.Z, -(ZAxis.ScalarMultiplication(eye))},
                                                        { 0, 0, 0, 1} });
        }

        public void FillProjectionMatrix()
        {
            ProjectionMatrix = new Matrix(new double[4, 4]{ { 2 * zNear / width, 0, 0, 0},
                                                            { 0, 2 * zNear / height, 0, 0},
                                                            { 0, 0, zFar / (zNear - zFar), zNear * zFar / (zNear - zFar)},
                                                            { 0, 0, -1, 0} });
        }




        private void RotateX(double dAngleX)
        {
            angleX += dAngleX;
            RotationMatrixAxisX.SetValue(1, 1, Math.Cos(angleX));
            RotationMatrixAxisX.SetValue(1, 2, -Math.Sin(angleX));
            RotationMatrixAxisX.SetValue(2, 1, Math.Sin(angleX));
            RotationMatrixAxisX.SetValue(2, 2, Math.Cos(angleX));
        }
        private void RotateY(double dAngleY)
        {
            angleY += dAngleY;
            RotationMatrixAxisY.SetValue(0, 0, Math.Cos(angleY));
            RotationMatrixAxisY.SetValue(0, 2, Math.Sin(angleY));
            RotationMatrixAxisY.SetValue(2, 0, -Math.Sin(angleY));
            RotationMatrixAxisY.SetValue(2, 2, Math.Cos(angleY));
        }
        private void RotateZ(double dAngleZ)
        {
            angleZ += dAngleZ;
            RotationMatrixAxisZ.SetValue(0, 0, Math.Cos(angleZ));
            RotationMatrixAxisZ.SetValue(0, 1, -Math.Sin(angleZ));
            RotationMatrixAxisZ.SetValue(1, 0, Math.Sin(angleZ));
            RotationMatrixAxisZ.SetValue(1, 1, Math.Cos(angleZ));
        }

        private void Scale(double scale)
        {
            ScaleMatrix.MultValue(0, 0, scale);
            ScaleMatrix.MultValue(1, 1, scale);
            ScaleMatrix.MultValue(2, 2, scale);
        }

        public void TranslateX(double dx)
        {
            TranslationMatrix.AddValue(0, 3, dx);
        }

        public void TranslateY(double dy)
        {
            TranslationMatrix.AddValue(1, 3, dy);
        }

        public void TranslateZ(double dz)
        {
            TranslationMatrix.AddValue(2, 3, dz);
        }


        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (char)Keys.Q)
            {
                TranslateX(-25);
            }
            else if (e.KeyValue == (char)Keys.W)
            {
                TranslateX(25);
            }
            else if (e.KeyValue == (char)Keys.A)
            {
                TranslateY(-25);
            }
            else if (e.KeyValue == (char)Keys.S)
            {
                TranslateY(25);
            }
            else if (e.KeyValue == (char)Keys.Z)
            {
                TranslateZ(-25);
            }
            else if (e.KeyValue == (char)Keys.X)
            {
                TranslateZ(25);
            }


            else if (e.KeyValue == (char)Keys.Add)
            {
                Scale(1.25);
            }
            else if (e.KeyValue == (char)Keys.Subtract)
            {
                Scale(0.75);
            }


            else if (e.KeyValue == (char)Keys.E)
            {
                RotateX(-0.25);
            }
            else if (e.KeyValue == (char)Keys.R)
            {
                RotateX(0.25);
            }
            else if (e.KeyValue == (char)Keys.D)
            {
                RotateY(-0.25);
            }
            else if (e.KeyValue == (char)Keys.F)
            {
                RotateY(0.25);
            }
            else if (e.KeyValue == (char)Keys.C)
            {
                RotateZ(0.25);
            }
            else if (e.KeyValue == (char)Keys.V)
            {
                RotateZ(-0.25);
            }


            else if (e.KeyValue == (char)Keys.U)
            {
                TranslateCameraZ(-5);
            }
            else if (e.KeyValue == (char)Keys.J)
            {
                TranslateCameraZ(5);
            }
            else if (e.KeyValue == (char)Keys.I)
            {
                TranslateCameraX(-5);
            }
            else if (e.KeyValue == (char)Keys.K)
            {
                TranslateCameraX(5);
            }
            else if (e.KeyValue == (char)Keys.O)
            {
                TranslateCameraY(-5);
            }
            else if (e.KeyValue == (char)Keys.L)
            {
                TranslateCameraY(5);
            }


            DrawModel();
        }
    }
}
