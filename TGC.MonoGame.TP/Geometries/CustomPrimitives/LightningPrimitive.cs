#region File Description

//-----------------------------------------------------------------------------
// CubePrimitive.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

#endregion File Description

#region Using Statements

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using TGC.MonoGame.TP.Collisions;

#endregion Using Statements

namespace TGC.MonoGame.TP.Geometries {

    public class LightningPrimitive : CustomPrimitive {
        
        public LightningPrimitive(GraphicsDevice graphicsDevice, ContentManager content, Color color, float size = 25f, Vector3? coordinates = null, Vector3? scale = null, Matrix? rotation = null) {

            Effect = content.Load<Effect>(ContentFolderEffects + "PBR_superficie");
            surfaceTexture = content.Load<Texture2D>(ContentFolderTextures + "materials/ruby/yellow_enserio");
            normalTexture = content.Load<Texture2D>(ContentFolderTextures + "materials/ruby/normal");

            Color = color;

            Vector3[] vertexList =
            {
                new Vector3(0.2f, 1f, 0.2f),
                new Vector3(-0.5f, -0.1f, 0.2f),
                new Vector3(-0.02f, -0.1f, 0.2f),
                new Vector3(-0.2f, -1f, 0.2f),
                new Vector3(0.5f, 0.1f, 0.2f),
                new Vector3(0.02f, 0.1f, 0.2f),
                new Vector3(0.2f, 1f, -0.2f),
                new Vector3(-0.5f, -0.1f, -0.2f),
                new Vector3(-0.02f, -0.1f, -0.2f),
                new Vector3(-0.2f, -1f, -0.2f),
                new Vector3(0.5f, 0.1f, -0.2f),
                new Vector3(0.02f, 0.1f, -0.2f)
            };

            // top - front/back
            AddTriangle(vertexList[0], vertexList[1], vertexList[2], size, color);
            AddTriangle(vertexList[6], vertexList[8], vertexList[7], size, color);
            // top - sides
            AddTriangle(vertexList[6], vertexList[7], vertexList[0], size, color);
            AddTriangle(vertexList[1], vertexList[0], vertexList[7], size, color);
            AddTriangle(vertexList[7], vertexList[8], vertexList[1], size, color);
            AddTriangle(vertexList[2], vertexList[1], vertexList[8], size, color);
            AddTriangle(vertexList[11], vertexList[6], vertexList[5], size, color);
            AddTriangle(vertexList[0], vertexList[5], vertexList[6], size, color);

            // bottom - front/back
            AddTriangle(vertexList[3], vertexList[4], vertexList[5], size, color);
            AddTriangle(vertexList[9], vertexList[11], vertexList[10], size, color);
            // bottom - sides
            AddTriangle(vertexList[9], vertexList[10], vertexList[3], size, color);
            AddTriangle(vertexList[4], vertexList[3], vertexList[10], size, color);
            AddTriangle(vertexList[10], vertexList[11], vertexList[4], size, color);
            AddTriangle(vertexList[5], vertexList[4], vertexList[11], size, color);
            AddTriangle(vertexList[8], vertexList[9], vertexList[2], size, color);
            AddTriangle(vertexList[3], vertexList[2], vertexList[9], size, color);

            World = Matrix.CreateScale(scale ?? Vector3.One) * (rotation ?? Matrix.Identity) * Matrix.CreateTranslation(coordinates ?? Vector3.Zero);

            BoundingCube = new OrientedBoundingBox(coordinates ?? Vector3.Zero, (scale ?? Vector3.One) * 25 / 2);
            BoundingCube.Rotate(rotation ?? Matrix.Identity);

            InitializePrimitive(graphicsDevice, content);
        }

    }
}