﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TGC.MonoGame.Samples.Samples.Shaders.SkyBox;
using TGC.MonoGame.TP.Camera;
using TGC.MonoGame.TP.Geometries;
using TGC.MonoGame.TP.Collisions;
using TGC.MonoGame.TP.MainCharacter;
using TGC.MonoGame.TP.Stages;
using Microsoft.Xna.Framework.Audio;
using TGC.MonoGame.TP.UI;
using Microsoft.Xna.Framework.Media;
using System.ComponentModel.Design;

namespace TGC.MonoGame.TP
{
    /// <summary>
    ///     Esta es la clase principal del juego.
    ///     Inicialmente puede ser renombrado o copiado para hacer mas ejemplos chicos, en el caso de copiar para que se
    ///     ejecute el nuevo ejemplo deben cambiar la clase que ejecuta Program <see cref="Program.Main()" /> linea 10.
    /// </summary>
    public class TGCGame : Game
    {
        public const string ContentFolder3D = "Models/";
        public const string ContentFolderEffects = "Effects/";
        public const string ContentFolderMusic = "Music/";
        public const string ContentFolderSounds = "Sounds/";
        public const string ContentFolderSpriteFonts = "SpriteFonts/";
        public const string ContentFolderTextures = "Textures/";



        /// <summary>
        ///     Constructor del juego.
        /// </summary>
        public TGCGame()
        {
            // Maneja la configuracion y la administracion del dispositivo grafico.
            Graphics = new GraphicsDeviceManager(this);

            Graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - 100;
            Graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - 100;

            // Para que el juego sea pantalla completa se puede usar Graphics IsFullScreen.
            // Carpeta raiz donde va a estar toda la Media.
            Content.RootDirectory = "Content";
            // Hace que el mouse sea visible.
            IsMouseVisible = false;
        }

        private GraphicsDeviceManager Graphics { get; }
        public SpriteBatch SpriteBatch { get; set; }
        private SpriteFont SpriteFont { get; set; }
        private UIManager UI;
        private LightManager Light;


        // Camera to draw the scene
        private FollowCamera FollowCamera { get; set; }

        // BOLITA
        private Character MainCharacter;
        private Stage Stage;
        public int StageNumber { get; set; } = 1;
        protected List<Entity> Entities;

        // Create a full screen quad to post-process
        private FullScreenQuad FullScreenQuad;

        /// <summary>
        ///     Se llama una sola vez, al principio cuando se ejecuta el ejemplo.
        ///     Escribir aqui el codigo de inicializacion: el procesamiento que podemos pre calcular para nuestro juego.
        /// </summary>
        public Point screenSize;


        protected override void Initialize()
        {

            var size = GraphicsDevice.Viewport.Bounds.Size;
            size.X /= 2;
            size.Y /= 2;
            screenSize = size;

            //var cameraPosition = new Vector3(25f, 100f, -1100f);
            //Camera = new FreeCamera(GraphicsDevice.Viewport.AspectRatio, cameraPosition, size);
            // Creo una camaar para seguir a nuestro auto.
            FollowCamera = new FollowCamera(GraphicsDevice.Viewport.AspectRatio, size);


            Light = new LightManager(Content, GraphicsDevice);
            Light.Initialize();

            // La logica de inicializacion que no depende del contenido se recomienda poner en este metodo.

            // Apago el backface culling.
            // Esto se hace por un problema en el diseno del modelo del logo de la materia.
            // Una vez que empiecen su juego, esto no es mas necesario y lo pueden sacar.
            var rasterizerState = new RasterizerState();
            rasterizerState.CullMode = CullMode.None;
            GraphicsDevice.RasterizerState = rasterizerState;
            // Seria hasta aca.

            base.Initialize();
        }

        /// <summary>
        ///     Se llama una sola vez, al principio cuando se ejecuta el ejemplo, despues de Initialize.
        ///     Escribir aqui el codigo de inicializacion: cargar modelos, texturas, estructuras de optimizacion, el procesamiento
        ///     que podemos pre calcular para nuestro juego.
        /// </summary>
        public Effect BallEffect;

        protected override void LoadContent()
        {
            // Aca es donde deberiamos cargar todos los contenido necesarios antes de iniciar el juego.
            Light.LoadContent();

            // Create a full screen quad to post-process
            FullScreenQuad = new FullScreenQuad(GraphicsDevice);

            SpriteBatch = new SpriteBatch(GraphicsDevice);
            SpriteFont = Content.Load<SpriteFont>(ContentFolderSpriteFonts + "CascadiaCode/CascadiaCodePL");
            UI = new UIManager(this, Content, GraphicsDevice, SpriteBatch, SpriteFont);

            LoadStage(StageNumber);

            base.LoadContent();
        }

        public void LoadStage(int stage)
        {
            switch (stage)
            {
                case 1:
                    Stage = new Stage_01(GraphicsDevice, Content);
                    break;

                default:
                    Stage = new Stage_02(GraphicsDevice, Content);
                    break;
            }

            Entities = new List<Entity>();
            MainCharacter = new Character(Content, Stage, Entities);
            BallEffect = Content.Load<Effect>(ContentFolderEffects + "PBR");
            MergeEntities(Stage.Track, Stage.Obstacles, Stage.Signs, Stage.Pickups, Stage.Checkpoints);

            UI.ResetStage(stage);
        }

        private void LoadNextStage()
        {
            StageNumber++;
            if (StageNumber > 2) { StageNumber = 1;  }
            LoadStage(StageNumber);
        }

        private void MergeEntities(List<GeometricPrimitive> Track, List<Obstacle> Obstacles, List<GeometricPrimitive> Signs, List<Pickup> Pickups, List<GeometricPrimitive> Checkpoints)
        {
            foreach (GeometricPrimitive myTrack in Track)
            {
                Entities.Add(myTrack);
            }

            foreach (Obstacle myObstacle in Obstacles)
            {
                Entities.Add(myObstacle.Model);
            }

            foreach (GeometricPrimitive mySign in Signs)
            {
                Entities.Add(mySign);
            }

            foreach (Pickup myPickup in Pickups)
            {
                Entities.Add(myPickup.Model);
            }

            foreach (GeometricPrimitive myCheckpoint in Checkpoints)
            {
                Entities.Add(myCheckpoint);
            }
        }

        /// <summary>
        ///     Se llama en cada frame.
        ///     Se debe escribir toda la logica de computo del modelo, asi como tambien verificar entradas del usuario y reacciones
        ///     ante ellas.
        /// </summary>
        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                UI.UIStatus = GameStatus.Menu;
                MediaPlayer.Volume = AudioManager.MenuVolume;
            }
            UI.Score = MainCharacter.Money;
            UI.Update(gameTime);

            MainCharacter.Status = UI.UIStatus;

            MainCharacter.Update(gameTime);

            FollowCamera.Update(gameTime, MainCharacter.World);
            // actualiza el estado solo si está jugando
            if (UI.UIStatus == GameStatus.Playing)
            {
                if (MainCharacter.FinishedStage)
                {
                    LoadNextStage();
                    UI.UIStatus = GameStatus.Menu;
                }
                Light.Update(gameTime);

                Stage.Update(gameTime);
                MainCharacter.Update(gameTime);
                FollowCamera.Update(gameTime, MainCharacter.World);

                Stage.CamPosition = FollowCamera.CamPosition;

                MainCharacter.ChangeDirection(FollowCamera.CamRotation);
                BallEffect.Parameters["eyePosition"].SetValue(FollowCamera.CamPosition);

                //World = Matrix.CreateRotationY(Rotation);
            }
            else if (UI.UIStatus == GameStatus.Exit)
            {
                Exit();
            }

            base.Update(gameTime);
        }

        /// <summary>
        ///     Se llama cada vez que hay que refrescar la pantalla.
        ///     Escribir aqui el codigo referido al renderizado.
        /// </summary>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightSkyBlue);
            // Aca deberiamos poner toda la logia de renderizado del juego.
            var originalRasterizerState = GraphicsDevice.RasterizerState;
            var rasterizerState = new RasterizerState();
            rasterizerState.CullMode = CullMode.None;
            Graphics.GraphicsDevice.RasterizerState = rasterizerState;
            //GraphicsDevice.DepthStencilState = DepthStencilState.None;

            //GraphicsDevice.Clear(Color.LightSkyBlue);


            //
            //Stage.SkyBox.Draw(FollowCamera.View, FollowCamera.Projection, FollowCamera.CamPosition);
            MainCharacter.Draw(FollowCamera.View, FollowCamera.Projection);

            Stage.Draw(FollowCamera.View, FollowCamera.Projection);
            Light.DrawShadows(FollowCamera.View, FollowCamera.Projection, Stage.GetModelListForShadowmapping());

            GraphicsDevice.RasterizerState = originalRasterizerState;

            // Configuramos el DepthStencilState para la UI
            DepthStencilState depthStencilStateUI = new DepthStencilState();
            depthStencilStateUI.DepthBufferEnable = false; // Desactivamos el uso del búfer de profundidad para la UI
            GraphicsDevice.DepthStencilState = depthStencilStateUI;

            // Dibujamos la interfaz de usuario (UI)
            UI.Draw();

            // Restauramos el estado original del DepthStencilState después de la UI
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;
        }
        
        /// <summary>
        ///     Libero los recursos que se cargaron en el juego.
        /// </summary>
        protected override void UnloadContent()
        {
            // Libero los recursos.
            Content.Unload();

            base.UnloadContent();
        }
    }
}