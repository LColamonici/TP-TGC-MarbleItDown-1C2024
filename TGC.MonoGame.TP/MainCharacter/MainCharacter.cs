using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TGC.MonoGame.TP.MainCharacter
{
    public class Character
    {
        const string ContentFolder3D = "3D/";
        const string ContentFolderEffects = "Effects/";
        const string ContentFolderTextures = "Textures/";

        string TexturePath;

        ContentManager Content;

        Model Sphere;
        public Matrix World;
        const float radius = 12.5f;
        Matrix Scale = Matrix.CreateScale(radius);
        Effect Effect;

        Material CurrentMaterial = Material.RustedMetal;

        Vector3 Position;
        Vector3 Velocity;
        Vector3 Acceleration = Vector3.Zero;
        Quaternion Rotation = Quaternion.Identity;
        Vector3 RotationAxis = Vector3.UnitY;
        float RotationAngle = 0f;

        Vector3 BallSpinAxis=Vector3.UnitX;
        float BallSpinAngle=0f;
        Matrix WorldWithBallSpin;
        float BallPitch=0f;
        float BallRoll=0f;
        BoundingSphere BoundingSphere;

        public Character(ContentManager content, Vector3 initialPosition, float initialYaw)
        {
            Content = content;


            InitializeEffect();
            InitializeSphere(initialPosition, initialYaw);
            InitializeTextures();
        }


        private void InitializeSphere(Vector3 initialPosition, float initialYaw)
        {
            // Got to set a texture, else the translation to mesh does not map UV
            Sphere = Content.Load<Model>(ContentFolder3D + "geometries/sphere");

            Position = initialPosition;
            World = Scale * Matrix.CreateFromYawPitchRoll(initialYaw, 0f, 0f) * Matrix.CreateTranslation(Position);
            WorldWithBallSpin=World;

            BoundingSphere = new BoundingSphere(Position, radius);

            // Apply the effect to all mesh parts
            Sphere.Meshes.FirstOrDefault().MeshParts.FirstOrDefault().Effect = Effect;
        }

        private void InitializeEffect()
        {
            Effect = Content.Load<Effect>(ContentFolderEffects + "PBR");
            Effect.CurrentTechnique = Effect.Techniques["PBR"];
        }

        private void InitializeTextures()
        {
            UpdateMaterialPath();
            LoadTextures();
        }

        public void Update(GameTime gameTime)
        {
            ProcessMaterialChange();
            ProcessMovement(gameTime);
        }

        public void Draw(Matrix view, Matrix projection)
        {
            var worldView = WorldWithBallSpin * view;
            Effect.Parameters["matWorld"].SetValue(WorldWithBallSpin);
            Effect.Parameters["matWorldViewProj"].SetValue(worldView * projection);
            Effect.Parameters["matInverseTransposeWorld"].SetValue(Matrix.Transpose(Matrix.Invert(WorldWithBallSpin)));
            Effect.Parameters["lightPosition"].SetValue(new Vector3(25, 80, -800));
            Effect.Parameters["lightColor"].SetValue(new Vector3(253, 251, 211));

            //Game.Gizmos.DrawSphere(World, Vector3.One*20, Color.Red);

            Sphere.Meshes.FirstOrDefault().Draw();
        }

        private void LoadTextures()
        {
            Texture2D albedo, ao, metalness, roughness, normals;

            normals = Content.Load<Texture2D>(TexturePath + "normal");
            ao = Content.Load<Texture2D>(TexturePath + "ao");
            metalness = Content.Load<Texture2D>(TexturePath + "metalness");
            roughness = Content.Load<Texture2D>(TexturePath + "roughness");
            albedo = Content.Load<Texture2D>(TexturePath + "color");

            Effect.Parameters["albedoTexture"]?.SetValue(albedo);
            Effect.Parameters["normalTexture"]?.SetValue(normals);
            Effect.Parameters["metallicTexture"]?.SetValue(metalness);
            Effect.Parameters["roughnessTexture"]?.SetValue(roughness);
            Effect.Parameters["aoTexture"]?.SetValue(ao);
        }

        private void ProcessMaterialChange()
        {
            var keyboardState = Keyboard.GetState();

            var NewMaterial = CurrentMaterial;

            if (keyboardState.IsKeyDown(Keys.D1))
            {
                NewMaterial = Material.RustedMetal;
            }
            else if (keyboardState.IsKeyDown(Keys.D2))
            {
                NewMaterial = Material.Grass;
            }
            else if (keyboardState.IsKeyDown(Keys.D3))
            {
                NewMaterial = Material.Gold;
            }
            else if (keyboardState.IsKeyDown(Keys.D4))
            {
                NewMaterial = Material.Marble;
            }
            else if (keyboardState.IsKeyDown(Keys.D5))
            {
                NewMaterial = Material.Metal;
            }

            if (NewMaterial != CurrentMaterial) 
            {
                CurrentMaterial = NewMaterial;
                SwitchMaterial();
            }
                
        }

        private void UpdateMaterialPath()
        {
            TexturePath = ContentFolderTextures + "materials/";
            switch (CurrentMaterial)
            {
                case Material.RustedMetal:
                    TexturePath += "harsh-metal";
                    break;

                case Material.Marble:
                    TexturePath += "marble";
                    break;

                case Material.Gold:
                    TexturePath += "gold";
                    break;

                case Material.Metal:
                    TexturePath += "metal";
                    break;

                case Material.Grass:
                    TexturePath += "ground";
                    break;
            }

            TexturePath += "/";
        }

        private void SwitchMaterial()
        {
            // We do not dispose textures, as they cannot be loaded again
            UpdateMaterialPath();
            LoadTextures();
        }

        private void ProcessMovement(GameTime gameTime) 
        {
            // Aca deberiamos poner toda la logica de actualizacion del juego.
            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            var directionX = new Vector3();
            var directionY = new Vector3();
            var directionZ = new Vector3();
            
            bool salto = false;
            float speed = 100;
            // Capturar Input teclado
            var keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D))
            {
                Acceleration += Vector3.Transform(Vector3.UnitX * -speed, Rotation);
            }
            if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A))
            {
                Acceleration += Vector3.Transform(Vector3.UnitX * -speed, Rotation) * (- 1);
            }
            if (keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.W))
            {
                Acceleration += Vector3.Transform(Vector3.UnitZ * speed, Rotation);
            }
            if (keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.S))
            {
                Acceleration += Vector3.Transform(Vector3.UnitZ * speed, Rotation) * (-1);
            }
            if(Keyboard.GetState().IsKeyDown(Keys.Space) && Velocity.Y == 0f)
            {
                Velocity += Vector3.Up * speed;
                salto = true;
            }

            Vector3 gravity = Vector3.Zero;

            Vector3 SpinVelocity=Velocity;


            BallSpinAngle += Velocity.Length()*elapsedTime / (MathHelper.Pi*12.5f);
            BallSpinAxis = Vector3.Normalize(Vector3.Cross(Vector3.UnitY, Velocity));
            
            //if(Acceleration==Vector3.Zero && Velocity!=Vector3.Zero) Velocity *= (1-(elapsedTime/2));
            if(Acceleration==Vector3.Zero || Vector3.Dot(Acceleration, Velocity)<0) Velocity *= (1-(elapsedTime));
            if(Velocity==Vector3.Zero) BallSpinAxis = Vector3.UnitZ;

            Rotation = Quaternion.CreateFromAxisAngle(RotationAxis, RotationAngle);

            directionX = Vector3.Transform(Vector3.UnitX, Rotation);
            directionY = Vector3.Transform(Vector3.UnitY, Rotation);
            directionZ = Vector3.Transform(Vector3.UnitZ, Rotation);

            if(Position.Y <= 25f)
            {
                gravity = new Vector3(0f, 0f, 0f);
                if (!salto)
                {
                    Velocity = new Vector3(Velocity.X, 0f, Velocity.Z);
                }
            }
            else
            {
                gravity = new Vector3(0f, -100f, 0f);
            }

            Velocity += (Acceleration + gravity) * elapsedTime;
            Position += (directionX + directionY + directionZ) * Velocity * elapsedTime * 0.5f;

            MoveTo(Position);

            Acceleration = Vector3.Zero;
        }

        public void MoveTo(Vector3 position)
        {
            BoundingSphere.Center = position;            
            World = Scale * Matrix.CreateTranslation(position);
            WorldWithBallSpin=Matrix.CreateFromAxisAngle(BallSpinAxis, BallSpinAngle) * World;
        }

        public BoundingSphere GetBoundingSphere()
        {
            return this.BoundingSphere; 
        }

        public void Respawn (Vector3 position, float yaw)
        {
            Velocity = Vector3.Zero;
            Acceleration = Vector3.Zero;
            BoundingSphere.Center = position;
            World = Scale * Matrix.CreateFromYawPitchRoll(yaw, 0, 0) * Matrix.CreateTranslation(position);
            WorldWithBallSpin = Matrix.CreateFromAxisAngle(BallSpinAxis, BallSpinAngle) * World;
        }

    }
}