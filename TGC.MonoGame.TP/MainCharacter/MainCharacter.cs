using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TGC.MonoGame.TP.Stages;
using TGC.MonoGame.TP.Collisions;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using TGC.MonoGame.TP.UI;
using System.Net;

namespace TGC.MonoGame.TP.MainCharacter
{
    public class Character : Entity
    {
        const string ContentFolder3D = "3D/";
        const string ContentFolderEffects = "Effects/";
        const string ContentFolderTextures = "Textures/";

        const string ContentFolderMusic = "Music/";
        const string ContentFolderSounds = "Sounds/";

        string TexturePath;

        ContentManager Content;

        Model Sphere;
        public Matrix World;
        Matrix Scale = Matrix.CreateScale(12.5f);
        Effect Effect;

        public GameStatus Status;

        Material CurrentMaterial = Material.RustedMetal;



        // Checkpoints
        public Vector3 LastCheckpoint;
        public bool FinishedStage;
        private OrientedBoundingBox ObbLastCheckpoint;
        // Checkpoints

        // Sonidos
        public SoundEffect CheckpointSound;
        public SoundEffectInstance CheckpointSoundInstance;
        public SoundEffect FinalStageSound;
        public SoundEffectInstance FinalStageSoundInstance;
        public SoundEffect MovementSound;
        public SoundEffectInstance MovementSoundInstance;
        public SoundEffect JumpSound;
        public SoundEffectInstance JumpSoundInstance;
        // Sonidos


        // Colisiones
        public BoundingSphere EsferaBola { get; set; }
        public Vector3 contactPoint { get; set; }
        public Stage ActualStage;
        // Colisiones

        Vector3 BallSpinAxis = Vector3.UnitX;
        float BallSpinAngle = 0f;
        Matrix WorldWithBallSpin;

        Vector3 LightPos { get; set; }
        public Matrix Spin;

        public Vector3 ForwardVector = Vector3.UnitX;

        public Vector3 RightVector = Vector3.UnitZ;
        public Character(ContentManager content, Stage stage, List<Entity> entities)
        {
            Content = content;
            Spin = Matrix.CreateFromAxisAngle(Vector3.UnitZ, 0);

            ActualStage = stage;

            LastCheckpoint = stage.CharacterInitialPosition;

            FinishedStage = false;

            InitializeSounds();
            InitializeEffect();
            InitializeSphere(stage.CharacterInitialPosition);
            InitializeTextures();
            InitializeLight();
        }
        void InitializeSounds()
        {
            CheckpointSound = Content.Load<SoundEffect>(ContentFolderSounds + "checkpoint");
            CheckpointSoundInstance = CheckpointSound.CreateInstance();

            FinalStageSound = Content.Load<SoundEffect>(ContentFolderSounds + "act_cleared");
            FinalStageSoundInstance = FinalStageSound.CreateInstance();

            MovementSound = Content.Load<SoundEffect>(ContentFolderSounds + "moving");
            MovementSoundInstance = MovementSound.CreateInstance();

            JumpSound = Content.Load<SoundEffect>(ContentFolderSounds + "jump");
            JumpSoundInstance = JumpSound.CreateInstance();
        }
        void InitializeLight()
        {
            LightPos = Position + new Vector3(0, 10, 0);
        }

        private void InitializeSphere(Vector3 initialPosition)
        {
            // Got to set a texture, else the translation to mesh does not map UV
            Sphere = Content.Load<Model>(ContentFolder3D + "geometries/sphere");

            Position = initialPosition;
            World = Scale * Matrix.CreateTranslation(Position);
            WorldWithBallSpin = World;

            // Bounding Sphere asociado a la bola principal
            UpdateBBSphere(Position);

            // Apply the effect to all mesh parts
            Sphere.Meshes.FirstOrDefault().MeshParts.FirstOrDefault().Effect = Effect;
        }

        private void UpdateBBSphere(Vector3 center)
        {
            EsferaBola = new BoundingSphere(center, 12.5f);
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
            Effect.Parameters["lightPosition"].SetValue(LightPos);
            Effect.Parameters["lightColor"].SetValue(new Vector3(253, 251, 211));
            Effect.GraphicsDevice.BlendState = BlendState.Opaque;
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


        public float DistanceToGround(Vector3 pos)
        {
            float dist = 10000000.0f;
            //float dist = float.MaxValue;
            foreach (OrientedBoundingBox box in ActualStage.Colliders)
            {
                if (box is null)
                    continue;
                Ray tempRay = new Ray(pos, -Vector3.Up);
                float? tempDist = box.Intersects(tempRay);

                if (dist > tempDist)
                {
                    dist = (float)tempDist;
                }
            }

            return dist;
        }

        public void UpdateLastCheckpoint()
        {
            OrientedBoundingBox nearestCheckpoint = null;
            bool foundCheckpoint = false;

            foreach (OrientedBoundingBox box in ActualStage.CheckpointColliders)
            {
                if (box is null)
                    continue;

                if (box.Intersects(EsferaBola))
                {
                    if (nearestCheckpoint == null || ActualStage.CheckpointColliders.IndexOf(box) > ActualStage.CheckpointColliders.IndexOf(nearestCheckpoint))
                    {
                        nearestCheckpoint = box;
                        foundCheckpoint = true;
                    }
                }
            }

            if (foundCheckpoint && ObbLastCheckpoint != nearestCheckpoint)
            {
                LastCheckpoint = nearestCheckpoint.Center + new Vector3 (0f, 0f, 5f) * EsferaBola.Radius;
                ObbLastCheckpoint = nearestCheckpoint;

                CheckpointSound.Play();

                // Verificar si es el último checkpoint
                if (ActualStage.CheckpointColliders.IndexOf(ObbLastCheckpoint) == ActualStage.CheckpointColliders.Count - 1)
                {
                    // Imprimir algún mensaje o cambiar de nivel
                    FinishedStage = true;
                    MediaPlayer.Stop();
                    FinalStageSound.Play();
                    ObbLastCheckpoint = null; // Reiniciar el último checkpoint, si es necesario
                }
            }
        }

        public bool IsColliding()
        {
            foreach (OrientedBoundingBox box in ActualStage.Colliders)
            {
                if (box is null)
                    continue;
                if (box.Intersects(EsferaBola))
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsOnGround(Vector3 pos)
        {
            foreach (OrientedBoundingBox box in ActualStage.Colliders)
            {
                if (box is null)
                    continue;
                Ray tempRay = new Ray(pos, -Vector3.Up);
                float? dist = box.Intersects(tempRay);
                if (dist.HasValue && dist <= 12.5f)
                {
                    return true;
                }
            }
            return false;
        }

        Vector3 getCollisionNormalNEW(BoundingSphere sphere, OrientedBoundingBox box)
        {
            Vector3 puntoContacto = box.ClosestPointTo(sphere.Center);
            return Vector3.Normalize(sphere.Center - puntoContacto);
        }

        public void ProcessCollisionNEW(float deltaTime)
        {
            Vector3 oldPosition = Position;
            Vector3 movement = Velocity * deltaTime * deltaTime * 0.5f;
            Vector3 newPosition = oldPosition + movement;

            UpdateBBSphere(newPosition);

            Vector3 desirePosition = newPosition;
            Vector3 surfaceNormal = Vector3.Zero;

            float squareRadius = EsferaBola.Radius * EsferaBola.Radius;
            foreach (var collider in ActualStage.Colliders)
            {
                if (collider is null)
                    continue;

                Vector3 puntoContacto = collider.ClosestPointTo(EsferaBola.Center);
                float distanceToSquared = Vector3.DistanceSquared(puntoContacto, EsferaBola.Center);
                if (distanceToSquared <= squareRadius)
                {
                    contactPoint = puntoContacto;

                    // Manejar la colisión
                    surfaceNormal = getCollisionNormalNEW(EsferaBola, collider);

                    // Determinar si la colisión es con el suelo (por ejemplo, si la normal es vertical hacia arriba)                    
                    newPosition = contactPoint + surfaceNormal * EsferaBola.Radius;
                    UpdateBBSphere(newPosition);
                }
            }

            // Calculamos la diferencia entre la nueva posición y la "anterior"
            float difference = Vector3.Distance(newPosition, oldPosition);

            // Modificamos la velocidad en base a la colisión
            float newVelocity = Vector3.Dot(Velocity, -surfaceNormal);
            Velocity += 1.5f * newVelocity * surfaceNormal;

            Position = newPosition;

            if (IsOnGround(newPosition) && newPosition != oldPosition && difference > 0.3f)
            {
                MovementSoundInstance.Play();
            }

            UpdateBBSphere(newPosition);

        }

        public void ChangeDirection(float angle)
        {
            ForwardVector = Vector3.Transform(Vector3.UnitX, Matrix.CreateRotationY(angle));
            RightVector = Vector3.Transform(Vector3.UnitZ, Matrix.CreateRotationY(angle));
        }

        private void ProcessMovement(GameTime gameTime)
        {
            if(Status != GameStatus.Playing)
            {
                return;
            }

            // Aca deberiamos poner toda la logica de actualizacion del juego.
            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            float speed;
            switch (CurrentMaterial)
            {
                case Material.Grass:
                    speed = 95f;
                    break;
                case Material.Gold:
                    speed = 105f;
                    break;
                case Material.Marble:
                    speed = 100f;
                    break;
                case Material.Metal:
                    speed = 90f;
                    break;
                default:
                    speed = 98f;
                    break;
            }

            // Capturar Input teclado
            var keyboardState = Keyboard.GetState();

            // Procesamiento del movimiento horizontal           
            if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D))
            {
                Acceleration += Vector3.Transform(ForwardVector * -speed, Rotation); //amtes unitx
            }
            if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A))
            {
                Acceleration += Vector3.Transform(ForwardVector * -speed, Rotation) * (-1);
            }
            if (keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.W))
            {
                Acceleration += Vector3.Transform(RightVector * speed, Rotation); //antes unitz
            }
            if (keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.S))
            {
                Acceleration += Vector3.Transform(RightVector * speed, Rotation) * (-1);
            }

            Acceleration += new Vector3(0f, -100f, 0f);

            //Procesamiento del movimiento vertical
            float distGround = DistanceToGround(Position);
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && (distGround <= 12.5f || (IsColliding() && IsOnGround(Position))))
            {
                JumpSoundInstance.Play();
                // Seteo la velocidad vertical en 0 para que el salto sea siempre a la misma distancia
                Velocity = new Vector3(Velocity.X, 0f, Velocity.Z);
                Velocity += Vector3.Up * speed * 100f;
            }

            Vector3 HorizontalVelocity = new Vector3(Velocity.X, 0, Velocity.Z);
            BallSpinAngle += HorizontalVelocity.Length() * elapsedTime * elapsedTime / (MathHelper.Pi * 12.5f);

            // se normaliza el vector yCrossVelocity solo si alguna de componentes es distinta de 0
            Vector3 yCrossVelocity = DeleteInfinitesimalValues(Vector3.Cross(Vector3.UnitY, Velocity));
            if (Math.Abs(yCrossVelocity.X) > 0.1f || Math.Abs(yCrossVelocity.Y) > 0.1f || Math.Abs(yCrossVelocity.Z) > 0.1f)
            {
                BallSpinAxis = Vector3.Normalize(yCrossVelocity);
            }

            if (Acceleration == Vector3.Zero || Vector3.Dot(Acceleration, Velocity) < 0)
            {
                Velocity = DeleteInfinitesimalValues(Velocity * (1 - elapsedTime));
            }

            Rotation = Quaternion.CreateFromAxisAngle(RotationAxis, RotationAngle);

            Velocity += Acceleration;

            ProcessCollisionNEW(elapsedTime);

            MoveTo(Position);

            UpdateLastCheckpoint();

            // Resetea la posición inicial del nivel si se cae al vacío
            if (Position.Y < -500)
            {
                Position = LastCheckpoint;
                Velocity = Vector3.Zero;
                MoveTo(Position);
                UpdateBBSphere(Position);
            }

            Acceleration = Vector3.Zero;
        }
        //float DeltaX, DeltaZ;
        public void MoveTo(Vector3 position)
        {
            World = Scale * Matrix.CreateTranslation(position);
            WorldWithBallSpin = Matrix.CreateFromAxisAngle(BallSpinAxis, BallSpinAngle) * World;
            LightPos = position + new Vector3(0, 30, -30);

            //WorldWithBallSpin=Matrix.CreateRotationX(DeltaX) * Matrix.CreateRotationZ(DeltaZ) * World;
        }
        private Vector3 DeleteInfinitesimalValues(Vector3 vector)
        {
            if (Math.Abs(vector.X) < 0.1f) { vector.X = 0; }
            if (Math.Abs(vector.Y) < 0.1f) { vector.Y = 0; }
            if (Math.Abs(vector.Z) < 0.1f) { vector.Z = 0; }
            return vector;
        }

    }
}