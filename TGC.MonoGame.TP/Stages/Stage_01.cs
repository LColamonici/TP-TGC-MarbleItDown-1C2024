using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using TGC.MonoGame.TP.Geometries;
using TGC.MonoGame.TP.Stages;
using TGC.MonoGame.TP.Stages.Items;

class Stage_01 : Stage
{

    public Stage_01(GraphicsDevice graphicsDevice, ContentManager content) :
        base(graphicsDevice, content, characterPosition: new Vector3(25, 40, -800))
    { 
        BackgroundMusic = Content.Load<Song>(ContentFolderMusic + "stage_01");
        MediaPlayer.Volume = 0.25f;
        MediaPlayer.Play(BackgroundMusic);
        MediaPlayer.IsRepeating = true;
    }

    protected override void LoadColliders()
    {
        for (int i = 0; i < Track.Count; i++)
        {
            GeometricPrimitive objetoActual = Track[i];
            if (objetoActual is CubePrimitive)
            {
                CubePrimitive aux = (CubePrimitive)objetoActual;
                Colliders.Add(aux.BoundingCube);
            }
            if (objetoActual is RampPrimitive)
            {
                RampPrimitive aux = (RampPrimitive)objetoActual;
                foreach (var boundingRamp in aux.BoundingRamps)
                {
                    Colliders.Add(boundingRamp);
                }
            }
        }

        for (int i = 0; i < Obstacles.Count; i++)
        {
            CubePrimitive cuboActual = (CubePrimitive)Obstacles[i];
            Colliders.Add(cuboActual.BoundingCube);
        }

        for (int i = 0; i < Signs.Count; i++)
        {
            CubePrimitive cuboActual = (CubePrimitive)Signs[i];
            Colliders.Add(cuboActual.BoundingCube);
        }

        PickupColliders = Pickups;

        for (int i = 0; i < Checkpoints.Count; i++)
        {
            CubePrimitive cuboActual = (CubePrimitive)Checkpoints[i];
            CheckpointColliders.Add(cuboActual.BoundingCube);
        }
    }

    protected override void LoadTrack()
    {
        Track = new List<GeometricPrimitive>
        {
            // PRIMERA PLATAFORMA
            new CubePrimitive(GraphicsDevice, Content, Color.BlueViolet, coordinates: new Vector3(25, 0, 0), scale: new Vector3(3f, 1f, 1f)),
            new CubePrimitive(GraphicsDevice, Content, Color.Aquamarine, coordinates: new Vector3(25, 0, 25), scale: new Vector3(3f, 1f, 1f)),
            new CubePrimitive(GraphicsDevice, Content, Color.LightPink, coordinates: new Vector3(25, 0, 50), scale: new Vector3(3f, 1f, 1f)),
            new CubePrimitive(GraphicsDevice, Content, Color.BlueViolet, coordinates: new Vector3(25, 0, 75), scale: new Vector3(3f, 1f, 1f)),
            new CubePrimitive(GraphicsDevice, Content, Color.Aquamarine, coordinates: new Vector3(25, 0, 100), scale: new Vector3(3f, 1f, 1f)),
            new CubePrimitive(GraphicsDevice, Content, Color.LightPink, coordinates: new Vector3(25, 0, 125), scale: new Vector3(3f, 1f, 1f)),

            // SEGUNDA PLATAFORMA
            new CubePrimitive(GraphicsDevice, Content, Color.BlueViolet, coordinates: new Vector3(25, 0, 175), scale: new Vector3(3f, 1f, 1f)),
            new CubePrimitive(GraphicsDevice, Content, Color.Aquamarine, coordinates: new Vector3(25, 0, 200), scale: new Vector3(3f, 1f, 1f)),
            new CubePrimitive(GraphicsDevice, Content, Color.LightPink, coordinates: new Vector3(25, 0, 225), scale: new Vector3(3f, 1f, 1f)),

            // ESCALERA
            new CubePrimitive(GraphicsDevice, Content, Color.BlueViolet, coordinates: new Vector3(75, 25, 200), scale: new Vector3(1f, 1f, 3f)),
            new CubePrimitive(GraphicsDevice, Content, Color.Aquamarine, coordinates: new Vector3(100, 50, 200), scale: new Vector3(1f, 1f, 3f)),
            new CubePrimitive(GraphicsDevice, Content, Color.LightPink, coordinates: new Vector3(125, 75, 200), scale: new Vector3(1f, 1f, 3f)),

            // TERCERA PLATAFORMA
            new CubePrimitive(GraphicsDevice, Content, Color.BlueViolet, coordinates: new Vector3(175, 100, 225), scale: new Vector3(3f, 1f, 1f)),
            new CubePrimitive(GraphicsDevice, Content, Color.Aquamarine, coordinates: new Vector3(175, 100, 200), scale: new Vector3(3f, 1f, 1f)),
            new CubePrimitive(GraphicsDevice, Content, Color.LightPink, coordinates: new Vector3(175, 100, 175), scale: new Vector3(3f, 1f, 1f)),

            //CUARTA PLATAFORMA
            new CubePrimitive(GraphicsDevice, Content, Color.Aquamarine, coordinates: new Vector3(175, 100, 62), scale: new Vector3(1f, 1f, 6f)),

            // QUINTA PLATAFORMA
            new CubePrimitive(GraphicsDevice, Content, Color.BlueViolet, coordinates: new Vector3(175, 100, -50), scale: new Vector3(3f, 1f, 1f)),
            new CubePrimitive(GraphicsDevice, Content, Color.Aquamarine, coordinates: new Vector3(175, 100, -75), scale: new Vector3(3f, 1f, 1f)),
            new CubePrimitive(GraphicsDevice, Content, Color.LightPink, coordinates: new Vector3(175, 100, -100), scale: new Vector3(3f, 1f, 1f)),

            // RAMPA
            new CubePrimitive(GraphicsDevice, Content, Color.Aquamarine, coordinates: new Vector3(175, 110, -135), scale: new Vector3(2.5f, 2.5f, 0.75f), rotation: Matrix.CreateFromYawPitchRoll(0, (float)Math.PI / (-3), 0)),

            // CANALETA
            new CubePrimitive(GraphicsDevice, Content, Color.BlueViolet, coordinates: new Vector3(175, 125, -250), scale: new Vector3(3f, 1f, 9f)),
            new CubePrimitive(GraphicsDevice, Content, Color.BlueViolet, coordinates: new Vector3(150, 145, -250), scale: new Vector3(9f, 1f, 0.25f), rotation: Matrix.CreateFromYawPitchRoll((float)Math.PI / 2, (float)Math.PI / (-6), 0)),
            new CubePrimitive(GraphicsDevice, Content, Color.BlueViolet, coordinates: new Vector3(200, 145, -250), scale: new Vector3(9f, 1f, 0.25f), rotation: Matrix.CreateFromYawPitchRoll((float)Math.PI / (-2), (float)Math.PI / (-6), 0)),

            // PLANOS INCLINADOS (ROLL)
            new CubePrimitive(GraphicsDevice, Content, Color.LightPink, coordinates: new Vector3(25, 0, -75), scale: new Vector3(3f, 1f, 4f), rotation: Matrix.CreateFromYawPitchRoll(0, 0, (float)Math.PI / (-6))),
            new CubePrimitive(GraphicsDevice, Content, Color.Aquamarine, coordinates: new Vector3(25, 0, -175), scale: new Vector3(3f, 1f, 4f)),
            new CubePrimitive(GraphicsDevice, Content, Color.BlueViolet, coordinates: new Vector3(25, 0, -275), scale: new Vector3(3f, 1f, 4f), rotation: Matrix.CreateFromYawPitchRoll(0, 0, (float)Math.PI / 6)),
            new CubePrimitive(GraphicsDevice, Content, Color.LightPink, coordinates: new Vector3(25, 0, -375), scale: new Vector3(3f, 1f, 4f)),
            new CubePrimitive(GraphicsDevice, Content, Color.Aquamarine, coordinates: new Vector3(25, 0, -475), scale: new Vector3(3f, 1f, 4f), rotation: Matrix.CreateFromYawPitchRoll(0, 0, (float)Math.PI / (-6))),
            new CubePrimitive(GraphicsDevice, Content, Color.BlueViolet, coordinates: new Vector3(25, 0, -575), scale: new Vector3(3f, 1f, 4f)),
            new CubePrimitive(GraphicsDevice, Content, Color.LightPink, coordinates: new Vector3(25, 0, -675), scale: new Vector3(3f, 1f, 4f), rotation: Matrix.CreateFromYawPitchRoll(0, 0, (float)Math.PI / 6)),
            new CubePrimitive(GraphicsDevice, Content, Color.Aquamarine, coordinates: new Vector3(25, 0, -775), scale: new Vector3(3f, 1f, 4f)),

            // RAMPA GRANDE
            new CubePrimitive(GraphicsDevice, Content, Color.LightPink, coordinates: new Vector3(240, 80, -412), scale: new Vector3(9f, 1f, 4f), rotation: Matrix.CreateFromYawPitchRoll(0, 0, (float)Math.PI / (-6))),

            // CUADRADOS GRANDES 
            new CubePrimitive(GraphicsDevice, Content, Color.Red, coordinates: new Vector3(275, 25, -425), scale: new Vector3(9f, 1f, 9f)),
            new CubePrimitive(GraphicsDevice, Content, Color.Orange, coordinates: new Vector3(300, 0, -400), scale: new Vector3(9f, 1f, 9f)),
            new CubePrimitive(GraphicsDevice, Content, Color.Yellow, coordinates: new Vector3(325, -25, -375), scale: new Vector3(9f, 1f, 9f)),
            new CubePrimitive(GraphicsDevice, Content, Color.Green, coordinates: new Vector3(350, -50, -350), scale: new Vector3(9f, 1f, 9f)),
            new CubePrimitive(GraphicsDevice, Content, Color.Blue, coordinates: new Vector3(375, -75, -325), scale: new Vector3(9f, 1f, 9f)),
            new CubePrimitive(GraphicsDevice, Content, Color.Indigo, coordinates: new Vector3(400, -100, -300), scale: new Vector3(9f, 1f, 9f)),
            new CubePrimitive(GraphicsDevice, Content, Color.Purple, coordinates: new Vector3(425, -125, -275), scale: new Vector3(9f, 1f, 9f)),

            // PLATAFORMA FINAL
            new CubePrimitive(GraphicsDevice, Content, Color.Aquamarine, coordinates: new Vector3(425, -125, -75), scale: new Vector3(7f, 1f, 6f))
        };
    }

    protected override void LoadObstacles()
    {
        Obstacles = new List<GeometricPrimitive>();
    }

    protected override void LoadSigns()
    {
        Signs = new List<GeometricPrimitive>
        {
            // aca irian cartelitos
            //jump
            new CubePrimitive(GraphicsDevice, Content, Color.Black, coordinates: new Vector3(-25, 50, 150)),

            //up (flechita?)
            new CubePrimitive(GraphicsDevice, Content, Color.Black, coordinates: new Vector3(50, 75, 250)),

            //jump
            new CubePrimitive(GraphicsDevice, Content, Color.Black, coordinates: new Vector3(200, 150, 150)),

            //jump
            new CubePrimitive(GraphicsDevice, Content, Color.Black, coordinates: new Vector3(200, 150, -25)),

            //down (flechita?)
            new CubePrimitive(GraphicsDevice, Content, Color.Black, coordinates: new Vector3(200, 175, -475))
        };
    }

    protected override void LoadPickups()
    {
        Pickups = new List<Pickup>() {

            // Plataformas iniciales
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(10f, 25f, -600f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(40f, 25f, -600f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new SpeedBoost(GraphicsDevice, Content, Color.Yellow, 25f, coordinates: new Vector3(10f, 30f, -500f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(10f, 25f, -400f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(40f, 25f, -400f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(10f, 25f, -200f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(40f, 25f, -200f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(10f, 25f, 0f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(40f, 25f, 0f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(10f, 25f, 20f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(40f, 25f, 20f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(10f, 25f, 40f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(40f, 25f, 40f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(10f, 25f, 60f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(40f, 25f, 60f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(10f, 25f, 80f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(40f, 25f, 80f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(10f, 25f, 100f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(40f, 25f, 100f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(10f, 25f, 120f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(40f, 25f, 120f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(10f, 25f, 160f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(40f, 25f, 160f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(10f, 25f, 180f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(40f, 25f, 180f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),

            // Una vez superada la escalera
            new Ruby(GraphicsDevice, Content, Color.Red, 25f, coordinates: new Vector3(175f, 130f, 200f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(175f, 130f, 120f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(175f, 130f, 90f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(175f, 130f, 60f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(175f, 130f, 30f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(175f, 130f, 0f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            
            // En la rampa ponemos un rubí azul
            new Ruby(GraphicsDevice, Content, Color.Blue, 25f, coordinates: new Vector3(180f, 160f, -412f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            
            // Rodeando el checkpoint final
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(385f, -100f, -105f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(385f, -100f, -75f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(385f, -100f, -55f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(385f, -100f, -35f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(405f, -100f, -35f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(425f, -100f, -35f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(445f, -100f, -35f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(465f, -100f, -35f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(465f, -100f, -55f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(465f, -100f, -75f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(465f, -100f, -105f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(465f, -100f, -125f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(445f, -100f, -125f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(425f, -100f, -125f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(405f, -100f, -125f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            new Rupee(GraphicsDevice, Content, Color.Green, 25f, coordinates: new Vector3(385f, -100f, -125f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),

            // Ponemos otro rubí justo arriba del checkpoint
            new Ruby(GraphicsDevice, Content, Color.Yellow, 25f, coordinates: new Vector3(425f, -80f, -75f), scale: new Vector3(1f, 1f, 1f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f)),
            
        };
    }

    protected override void LoadCheckpoints()
    {
        Checkpoints = new List<GeometricPrimitive>()
        {
            // aca iria la banderita del final
            new CubePrimitive(GraphicsDevice, Content, Color.White, coordinates: new Vector3(425, -100, -75))
        };
    }



    public override void Update(GameTime gameTime)
    {
        // TODO: actualizar el estado de todas las piezas m�viles del nivel
    }

}







