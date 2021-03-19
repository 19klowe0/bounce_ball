using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace bounce_Ball.Content
{
    class MeshCollider
    {
        List<Vector3> verts;
        List<Vector3> norms;

        public MeshCollider(Model model, Matrix world)
        {
            verts = new List<Vector3>();
            norms = new List<Vector3>();

            foreach(ModelMesh mesh in model.Meshes)
            {
                foreach(ModelMeshPart part in mesh.MeshParts)
                {
                    VertexPositionNormalTexture[] vertexData = new VertexPositionNormalTexture[part.NumVertices];

                    ushort[] indices = new ushort[part.IndexBuffer.IndexCount];
                    part.IndexBuffer.GetData<ushort>(indices);

                    Vector3 v = new Vector3();
                    for (int i =0; i<indices.Length; i++)
                    {
                        v.X = vertexData[indices[i]].Position.X;
                        v.Y = vertexData[indices[i]].Position.Y;
                        v.Z = vertexData[indices[i]].Position.Z;

                        verts.Add(Vector3.Transform(v, mesh.ParentBone.Transform * world));

                        if(verts.Count % 3 == 0)
                        {
                            Vector3 Normal = Vector3.Cross(verts[verts.Count - 1]- verts[verts.Count-3], 
                                                            verts[verts.Count-2]- verts[verts.Count - 3]);
                            Normal.Normalize();
                            norms.Add(Normal);
                        }
                    }
                }
            }
        }

        public static bool SameSide(Vector3 p1, Vector3 p2, Vector3 a, Vector3 b)
        {
            Vector3 cp1 = Vector3.Cross(b - a, p1 - a);
            Vector3 cp2 = Vector3.Cross(b - a, p2 - a);
            if(Vector3.Dot(cp1, cp2)>= 0)
            {
                return true;
            }
            return false;
        }

        public static bool PointInTrangle(Vector3 p, Vector3 a, Vector3 b, Vector3 c)
        {
            if(SameSide(p, a, b, c) && SameSide(p, b, a, c) && SameSide(p, a, c, b))
            {
                return true;
            }
            return false;
        }

        public bool checkCollisionAndResponse(ball e)
        {
            bool collision = false;

            //need his notes here 



            return collision;

        }
    }
}
