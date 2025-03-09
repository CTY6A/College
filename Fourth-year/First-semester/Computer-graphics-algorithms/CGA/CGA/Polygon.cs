using System.Collections.Generic;

namespace CGA
{
    class Polygon
    {
        public int Vertex1Id { get; set; }
        public Vertex Vertex1 { get; set; }
        public Vertex Vertex1BeforeProjection { get; set; }

        public int VertexTexture1Id { get; set; }
        public Vertex VertexTexture1 { get; set; }
        public Vertex VertexTexture1BeforeProjection { get; set; }

        public int VertexNormal1Id { get; set; }
        public Vertex VertexNormal1 { get; set; }
        public Vertex VertexNormal1BeforeProjection { get; set; }



        public int Vertex2Id { get; set; }
        public Vertex Vertex2 { get; set; }
        public Vertex Vertex2BeforeProjection { get; set; }

        public int VertexTexture2Id { get; set; }
        public Vertex VertexTexture2 { get; set; }
        public Vertex VertexTexture2BeforeProjection { get; set; }

        public int VertexNormal2Id { get; set; }
        public Vertex VertexNormal2 { get; set; }
        public Vertex VertexNormal2BeforeProjection { get; set; }



        public int Vertex3Id { get; set; }
        public Vertex Vertex3 { get; set; }
        public Vertex Vertex3BeforeProjection { get; set; }

        public int VertexTexture3Id { get; set; }
        public Vertex VertexTexture3 { get; set; }
        public Vertex VertexTexture3BeforeProjection { get; set; }

        public int VertexNormal3Id { get; set; }
        public Vertex VertexNormal3 { get; set; }
        public Vertex VertexNormal3BeforeProjection { get; set; }


        public Vertex Normal { get; set; }

        public Polygon(int vertex1Id, int vertexTexture1Id, int vertexNormal1Id, 
                       int vertex2Id, int vertexTexture2Id, int vertexNormal2Id,
                       int vertex3Id, int vertexTexture3Id, int vertexNormal3Id)
        {
            this.Vertex1Id = vertex1Id;
            this.VertexTexture1Id = vertexTexture1Id;
            this.VertexNormal1Id = vertexNormal1Id;

            this.Vertex2Id = vertex2Id;
            this.VertexTexture2Id = vertexTexture2Id;
            this.VertexNormal2Id = vertexNormal2Id;

            this.Vertex3Id = vertex3Id;
            this.VertexTexture3Id = vertexTexture3Id;
            this.VertexNormal3Id = vertexNormal3Id;
        }

        public void CalculateNormal()
        {
            double x2 = Vertex2.X - Vertex1.X;
            double x3 = Vertex3.X - Vertex1.X;
            double y2 = Vertex2.Y - Vertex1.Y;
            double y3 = Vertex3.Y - Vertex1.Y;
            double z2 = Vertex2.Z - Vertex1.Z;
            double z3 = Vertex3.Z - Vertex1.Z;
            Normal = new Vertex(y2 * z3 - y3 * z2,  x2 * z3 - x3 * z2, x2 * y3 - x3 * y2).Normalize();
        }

        public void CalculateNormalBeforeProjection(List<Vertex> Points)
        {
            double x2 = Points[Vertex2Id - 1].X - Points[Vertex1Id - 1].X;
            double x3 = Points[Vertex3Id - 1].X - Points[Vertex1Id - 1].X;
            double y2 = Points[Vertex2Id - 1].Y - Points[Vertex1Id - 1].Y;
            double y3 = Points[Vertex3Id - 1].Y - Points[Vertex1Id - 1].Y;
            double z2 = Points[Vertex2Id - 1].Z - Points[Vertex1Id - 1].Z;
            double z3 = Points[Vertex3Id - 1].Z - Points[Vertex1Id - 1].Z;
            Normal = new Vertex(y2 * z3 - y3 * z2, x2 * z3 - x3 * z2, x2 * y3 - x3 * y2).Normalize();
        }
    }
}
