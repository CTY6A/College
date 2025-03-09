using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;

namespace CGA
{
    class LogicalObject
    {
        public List<Vertex> Points = new List<Vertex>();
        public List<Polygon> Polygons = new List<Polygon>();

        public void ReadFile(string CurrentFileName)
        {
            string CurrentLine;
            int VertexTextureIndex = 0;
            int VertexNormalIndex = 0;

            StreamReader CurrnetStreamReader = new StreamReader(CurrentFileName);

            while (!CurrnetStreamReader.EndOfStream)
            {
                CurrentLine = CurrnetStreamReader.ReadLine();

                if (CurrentLine.Length >= 2)
                {
                    if (CurrentLine[0] == 'v' && CurrentLine[1] == ' ')
                    {
                        string[] CurrentVertexCoordinates = CurrentLine.Split(' ');
                        Vertex CurrentVertex = new Vertex();

                        CurrentVertex.X = Convert.ToDouble(CurrentVertexCoordinates[1], NumberFormatInfo.InvariantInfo);
                        CurrentVertex.Y = Convert.ToDouble(CurrentVertexCoordinates[2], NumberFormatInfo.InvariantInfo);
                        CurrentVertex.Z = Convert.ToDouble(CurrentVertexCoordinates[3], NumberFormatInfo.InvariantInfo);
                        CurrentVertex.Normal = new Vertex(0, 0, 0);

                        Points.Add(CurrentVertex);
                    }
                    else if (CurrentLine[0] == 'v' && CurrentLine[1] == 't' && CurrentLine[2] == ' ')
                    {
                        string[] CurrentVertexTextureCoordinates = CurrentLine.Split(' ');


                        Vertex CurrentVertex;
                        if (Points.Count < VertexTextureIndex) {
                            CurrentVertex = Points[VertexTextureIndex];
                        }
                        else
                        {
                            CurrentVertex = new Vertex();

                            CurrentVertex.X = 0;
                            CurrentVertex.Y = 0;
                            CurrentVertex.Z = 0;

                            CurrentVertex.Normal = new Vertex(0, 0, 0);
                        }

                        CurrentVertex.VertexTextureU = Convert.ToDouble(CurrentVertexTextureCoordinates[2], NumberFormatInfo.InvariantInfo);
                        CurrentVertex.VertexTextureV = Convert.ToDouble(CurrentVertexTextureCoordinates[3], NumberFormatInfo.InvariantInfo);
                        CurrentVertex.VertexTextureW = Convert.ToDouble(CurrentVertexTextureCoordinates[4], NumberFormatInfo.InvariantInfo);
                        
                        if (Points.Count == VertexTextureIndex)
                        {
                            Points.Add(CurrentVertex);
                        }


                        VertexTextureIndex++;
                    }
                    else if (CurrentLine[0] == 'v' && CurrentLine[1] == 'n' && CurrentLine[2] == ' ')
                    {
                        string[] CurrentVertexNormalCoordinates = CurrentLine.Split(' ');
                        Vertex CurrentVertex = Points[VertexNormalIndex];

                        CurrentVertex.Normal = new Vertex(Convert.ToDouble(CurrentVertexNormalCoordinates[2], NumberFormatInfo.InvariantInfo),
                                                          Convert.ToDouble(CurrentVertexNormalCoordinates[3], NumberFormatInfo.InvariantInfo),
                                                          Convert.ToDouble(CurrentVertexNormalCoordinates[4], NumberFormatInfo.InvariantInfo));
                        
                        VertexNormalIndex++;
                    }
                    else if (CurrentLine[0] == 'f' && CurrentLine[1] == ' ')
                    {
                        string[] CurrentArray;
                        CurrentArray = CurrentLine.Split(' ');
                        Polygons.Add(new Polygon(Convert.ToInt32(CurrentArray[1].Split('/')[0]), Convert.ToInt32(CurrentArray[1].Split('/')[1]), Convert.ToInt32(CurrentArray[1].Split('/')[2]),
                                                 Convert.ToInt32(CurrentArray[2].Split('/')[0]), Convert.ToInt32(CurrentArray[2].Split('/')[1]), Convert.ToInt32(CurrentArray[2].Split('/')[2]),
                                                 Convert.ToInt32(CurrentArray[3].Split('/')[0]), Convert.ToInt32(CurrentArray[3].Split('/')[1]), Convert.ToInt32(CurrentArray[3].Split('/')[2])));
                    }
                }
            }

            CurrnetStreamReader.Close();
        }
    }
}
