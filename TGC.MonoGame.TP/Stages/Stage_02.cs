﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using TGC.MonoGame.TP.Geometries;
using TGC.MonoGame.TP.MainCharacter;
using TGC.MonoGame.TP.Stages.Items;

class Stage_02 : Stage
{

    public Stage_02(GraphicsDevice graphicsDevice, ContentManager content) :
        base(graphicsDevice, content, characterPosition: new Vector3(300f, 25f, 0f),-MathHelper.PiOver2) { }

    protected override void LoadTrack()
    {
        Track = new List<GeometricPrimitive>()
            {
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(0, 0, 0), scale: new Vector3(30, 1, 10), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Black, coordinates: new Vector3(87.5f, 50, -112.5f), scale: new Vector3(1, 3, 1), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Black, coordinates: new Vector3(87.5f, 50, 112.5f), scale: new Vector3(1, 3, 1), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Black, coordinates: new Vector3(87.5f, 112.5f, 0), scale: new Vector3(1, 2, 10), rotation: Matrix.CreateFromYawPitchRoll(3.1415927f, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(-625, 0, 0), scale: new Vector3(20, 1, 8), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(-1000, 0, 0), scale: new Vector3(10, 1, 6), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(-1212.5f, 0, 0), scale: new Vector3(7, 1, 4), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Black, coordinates: new Vector3(-1212.5f, 25, 37.5f), scale: new Vector3(7, 1, 1), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Black, coordinates: new Vector3(-1287.5f, 25, -25), scale: new Vector3(1, 1, 4), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(-1250, 0, -175), scale: new Vector3(4, 1, 10), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(-1275, -25f, -362.5f), scale: new Vector3(2, 1, 5), rotation: Matrix.CreateFromYawPitchRoll(3.1415927f, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(-1225, -12.5f, -400), scale: new Vector3(2, 2, 8), rotation: Matrix.CreateFromYawPitchRoll(-3.1415927f, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(-1250, -50, -537.5f), scale: new Vector3(10, 1, 4), rotation: Matrix.CreateFromYawPitchRoll(1.5707964f, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(-1250, -25, -650f), scale: new Vector3(4, 1, 1), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(-1150, -50, -687.5f), scale: new Vector3(4, 1, 10), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(-1350, -50, -687.5f), scale: new Vector3(4, 1, 10), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(-1250, -50, -837.5f), scale: new Vector3(10, 1, 2), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(-1237.5f, -75, -925), scale: new Vector3(6, 1, 6), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(-1237.5f, -100, -1012.5f), scale: new Vector3(4, 1, 6), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(-1237.5f, -125, -1162.5f), scale: new Vector3(3, 1, 7), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(-1225, -125, -1300), scale: new Vector3(4, 1, 4), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(-1025, -125, -1312.5f), scale: new Vector3(12, 1, 3), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(-775, -125, -1312.5f), scale: new Vector3(4, 1, 3), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(-625, -125, -1312.5f), scale: new Vector3(4, 1, 3), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(-475, -125, -1312.5f), scale: new Vector3(4, 1, 3), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(-375, -150, -1275), scale: new Vector3(2, 1, 6), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(-287.5f, -175, -1250), scale: new Vector3(3, 1, 8), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(-200, -200, -1225), scale: new Vector3(2, 1, 10), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(12.5f, -200, -1287.5f), scale: new Vector3(15, 1, 5), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(-25, -200, -1125), scale: new Vector3(12, 1, 2), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(237.5f, -225, -1162.5f), scale: new Vector3(5, 1, 15), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(287.5f, -237.5f, -850), scale: new Vector3(3, 1, 10), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(262.5f, -250, -675), scale: new Vector3(3, 1, 4), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(250, -262.5f, -575), scale: new Vector3(4, 1, 4), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(-175, -262.5f, -537.5f), scale: new Vector3(30, 1, 1), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(-175, -262.5f, -600), scale: new Vector3(30, 1, 2), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(-600, -287.5f, -575), scale: new Vector3(4, 1, 4), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(-700, -312.5f, -575), scale: new Vector3(4, 1, 4), rotation: Matrix.CreateFromYawPitchRoll(-1.5707964f, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(-762.5f, -337.5f, -575), scale: new Vector3(1, 1, 4), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(-787.5f, -362.5f, -575), scale: new Vector3(1, 1, 4), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(-812.5f, -387.5f, -575), scale: new Vector3(1, 1, 4), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(-875, -412.5f, -575), scale: new Vector3(4, 1, 4), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(-875, -412.5f, -725), scale: new Vector3(4, 1, 6), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(-875, -412.5f, -1500), scale: new Vector3(6, 1, 6), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Black, coordinates: new Vector3(-762.5f, -362.5f, -1737.5f), scale: new Vector3(1, 3, 1), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Black, coordinates: new Vector3(-987.5f, -362.5f, -1737.5f), scale: new Vector3(1, 3, 1), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Black, coordinates: new Vector3(-875, -300f, -1737.5f), scale: new Vector3(10, 2, 1), rotation: Matrix.CreateFromYawPitchRoll(0, 0, 0)),
                new CubePrimitive(GraphicsDevice, Content, Color.Gray, coordinates: new Vector3(-875f, -412.5f, -1825f), scale: new Vector3(10f, 1f, 20f), rotation: Matrix.CreateFromYawPitchRoll(0f, 0f, 0f))
            };
    }

    protected override void LoadObstacles()
    {
        Obstacles = new List<MobileObstacle>()
        {
            //new MobileObstacle(GraphicsDevice, Content, new Vector3(-887.5f, 37.5f, 37.5f), new Vector3(1, 2, 3), new Vector3(0, 0, 0), new Vector3(0, 0, 3)),
            //new MobileObstacle(GraphicsDevice, Content, new Vector3(-525, -225, -537.5f), new Vector3(2, 2, 1), new Vector3(0, 0, 0), new Vector3(0, 0, 0)),
            //new MobileObstacle(GraphicsDevice, Content, new Vector3(-400, -225, -537.5f), new Vector3(2, 2, 1), new Vector3(0, 0, 0), new Vector3(0, 0, 0)),
            //new MobileObstacle(GraphicsDevice, Content, new Vector3(-275, -225, -537.5f), new Vector3(2, 2, 1), new Vector3(0, 0, 0), new Vector3(0, 0, 0)),
            //new MobileObstacle(GraphicsDevice, Content, new Vector3(-962.5f, 37.5f, -37.5f), new Vector3(1, 2, 3), new Vector3(0, 0, 0), new Vector3(0, 0, 4)),
            //new MobileObstacle(GraphicsDevice, Content, new Vector3(-1037.5f, 37.5f, 37.5f), new Vector3(1, 2, 3), new Vector3(0, 0, 0), new Vector3(0, 0, -4)),
            //new MobileObstacle(GraphicsDevice, Content, new Vector3(-1250, -12.5f, -825), new Vector3(2, 2, 1), new Vector3(0, 0, 0), new Vector3(0, 0, 4)),
            //new MobileObstacle(GraphicsDevice, Content, new Vector3(-1137.5f, -87.5f, -1337.5f), new Vector3(1, 2, 1), new Vector3(0, 0, 0), new Vector3(0, 0, -4)),
            //new MobileObstacle(GraphicsDevice, Content, new Vector3(-1062.5f, -87.5f, -1287.5f), new Vector3(1, 2, 1), new Vector3(0, 0, 0), new Vector3(0, 0, 4)),
            //new MobileObstacle(GraphicsDevice, Content, new Vector3(-987.5f, -87.5f, -1337.5f), new Vector3(1, 2, 1), new Vector3(0, 0, 0), new Vector3(0, 0, -4)),
            new MobileObstacle(GraphicsDevice, Content, new Vector3(-875, -412.5f, -875), new Vector3(4, 1, 4), new Vector3(0, 0, 0), new Vector3(0, 0, -5)),
            new MobileObstacle(GraphicsDevice, Content, new Vector3(-875, -412.5f, -975), new Vector3(4, 1, 4), new Vector3(0, 0, 0), new Vector3(0, 0, 5)),
            new MobileObstacle(GraphicsDevice, Content, new Vector3(-875, -412.5f, -1075), new Vector3(4, 1, 4), new Vector3(0, 0, 0), new Vector3(0, 0,-5)),
            new MobileObstacle(GraphicsDevice, Content, new Vector3(-875, -412.5f, -1175), new Vector3(4, 1, 4), new Vector3(0, 0, 0), new Vector3(0, 0, 0)),
            new MobileObstacle(GraphicsDevice, Content, new Vector3(-875, -412.5f, -1275), new Vector3(4, 1, 4), new Vector3(0, 0, 0), new Vector3(0, 0, 3)),
            new MobileObstacle(GraphicsDevice, Content, new Vector3(-875, -412.5f, -1375), new Vector3(4, 1, 4), new Vector3(0, 0, 0), new Vector3(0, 0, -3))
        };
    }

    protected override void LoadSigns()
    {
        Signs = new List<GeometricPrimitive>
        {

        };
    }

    protected override void LoadPickups()
    {
        Pickups = new List<Pickup>()
        {
            new Rupee(GraphicsDevice, Content, new Vector3(-387.5f, 25f, 0f)),
            new Rupee(GraphicsDevice, Content, new Vector3(-412.5f, 25f, 0f)),
            new Rupee(GraphicsDevice, Content, new Vector3(-437.5f, 25f, 0f)),
            new Rupee(GraphicsDevice, Content, new Vector3(-462.5f, 25f, 0f)),
            new Rupee(GraphicsDevice, Content, new Vector3(-487.5f, 25f, 0f)),
            new Rupee(GraphicsDevice, Content, new Vector3(-662.5f, 25f, 62.5f)),
            new Rupee(GraphicsDevice, Content, new Vector3(-687.5f, 25f, 62.5f)),
            new Rupee(GraphicsDevice, Content, new Vector3(-712.5f, 25f, 62.5f)),
            new Rupee(GraphicsDevice, Content, new Vector3(-737.5f, 25f, 62.5f)),
            new Rupee(GraphicsDevice, Content, new Vector3(-887.5f, 25f, -75f)),
            new Rupee(GraphicsDevice, Content, new Vector3(-912.5f, 25f, -75f)),
            new Rupee(GraphicsDevice, Content, new Vector3(-937.5f, 25f, -75f)),
            new Rupee(GraphicsDevice, Content, new Vector3(-962.5f, 25f, -75f)),
            new Rupee(GraphicsDevice, Content, new Vector3(-1250f, 25f, -150f)),
            new Rupee(GraphicsDevice, Content, new Vector3(-1250f, 25f, -175f)),
            new Rupee(GraphicsDevice, Content, new Vector3(-1250f, 25f, -200f)),
            new Rupee(GraphicsDevice, Content, new Vector3(-1250f, -25f, -550f)),
            new Rupee(GraphicsDevice, Content, new Vector3(-1250f, -25f, -575f)),
            new Rupee(GraphicsDevice, Content, new Vector3(-1250f, -25f, -600f)),
            new Rupee(GraphicsDevice, Content, new Vector3(-1150f, -25f, -675f)),
            new Rupee(GraphicsDevice, Content, new Vector3(-1150f, -25f, -700f)),
            new Antigravity(GraphicsDevice, Content, new Vector3(-1150f, -25f, -737.5f)),
            new SpeedBoost(GraphicsDevice, Content, new Vector3(-1350f, -25f, -737.5f)),
            new Rupee(GraphicsDevice, Content, new Vector3(-1350f, -25f, -700f)),
            new Rupee(GraphicsDevice, Content, new Vector3(-1350f, -25f, -675f)),
            new Rupee(GraphicsDevice, Content, new Vector3(-1237.5f, -50f, -912.5f)),
            new Rupee(GraphicsDevice, Content, new Vector3(-1237.5f, -50f, -987.5f)),
            new Rupee(GraphicsDevice, Content, new Vector3(-1237.5f, -75f, -1050f)),
            new Rupee(GraphicsDevice, Content, new Vector3(-1237.5f, -75f, -1075f)),
            new Rupee(GraphicsDevice, Content, new Vector3(-1237.5f, -100f, -1125f)),
            new Rupee(GraphicsDevice, Content, new Vector3(-1237.5f, -100f, -1150f)),
            new Rupee(GraphicsDevice, Content, new Vector3(-1237.5f, -100f, -1175f)),
            new Rupee(GraphicsDevice, Content, new Vector3(-1237.5f, -100f, -1200f)),
            new Rupee(GraphicsDevice, Content, new Vector3(-937.5f, -100f, -1312.5f)),
            new Rupee(GraphicsDevice, Content, new Vector3(-912.5f, -100f, -1312.5f)),
            new Rupee(GraphicsDevice, Content, new Vector3(-787.5f, -100f, -1312.5f)),
            new Rupee(GraphicsDevice, Content, new Vector3(-762.5f, -100f, -1312.5f)),
            new Rupee(GraphicsDevice, Content, new Vector3(-637.5f, -100f, -1312.5f)),
            new Rupee(GraphicsDevice, Content, new Vector3(-612.5f, -100f, -1312.5f)),
            new Rupee(GraphicsDevice, Content, new Vector3(-487.5f, -100f, -1312.5f)),
            new Rupee(GraphicsDevice, Content, new Vector3(-462.5f, -100f, -1312.5f)),
            new Rupee(GraphicsDevice, Content, new Vector3(-162.5f, -175f, -1125f)),
            new Rupee(GraphicsDevice, Content, new Vector3(-137.5f, -175f, -1125f)),
            new Rupee(GraphicsDevice, Content, new Vector3(-112.5f, -175f, -1125f)),
            new Rupee(GraphicsDevice, Content, new Vector3(-87.5f, -175f, -1125f)),
            new Rupee(GraphicsDevice, Content, new Vector3(-62.5f, -175f, -1125f)),
            new Rupee(GraphicsDevice, Content, new Vector3(-37.5f, -175f, -1125f)),
            new Rupee(GraphicsDevice, Content, new Vector3(-12.5f, -175f, -1125f)),
            new Rupee(GraphicsDevice, Content, new Vector3(12.5f, -175f, -1125f)),
            new Rupee(GraphicsDevice, Content, new Vector3(37.5f, -175f, -1125f)),
            new Rupee(GraphicsDevice, Content, new Vector3(62.5f, -175f, -1125f)),
            new Ruby(GraphicsDevice, Content, new Vector3(87.5f, -175f, -1125f)),
            new Rupee(GraphicsDevice, Content, new Vector3(100f, -175f, -1287.5f)),
            new Rupee(GraphicsDevice, Content, new Vector3(150f, -175f, -1287.5f)),
            new Rupee(GraphicsDevice, Content, new Vector3(50f, -175f, -1287.5f)),
            new Rupee(GraphicsDevice, Content, new Vector3(0f, -175f, -1287.5f)),
            new Rupee(GraphicsDevice, Content, new Vector3(-50f, -175f, -1287.5f)),
            new Rupee(GraphicsDevice, Content, new Vector3(-100f, -175f, -1287.5f)),
            new Rupee(GraphicsDevice, Content, new Vector3(-150f, -175f, -1287.5f)),
            new Rupee(GraphicsDevice, Content, new Vector3(287.5f, -200f, -987.5f)),
            new Rupee(GraphicsDevice, Content, new Vector3(287.5f, -212.5f, -737.5f)),
            new Rupee(GraphicsDevice, Content, new Vector3(262.5f, -225f, -650f)),
            new Rupee(GraphicsDevice, Content, new Vector3(187.5f, -237.5f, -600f)),
            new Rupee(GraphicsDevice, Content, new Vector3(137.5f, -237.5f, -600f)),
            new Rupee(GraphicsDevice, Content, new Vector3(87.5f, -237.5f, -600f)),
            new Rupee(GraphicsDevice, Content, new Vector3(37.5f, -237.5f, -600f)),
            new Rupee(GraphicsDevice, Content, new Vector3(-12.5f, -237.5f, -600f)),
            new Rupee(GraphicsDevice, Content, new Vector3(-62.5f, -237.5f, -600f)),
            new Rupee(GraphicsDevice, Content, new Vector3(-112.5f, -237.5f, -600f)),
            new Rupee(GraphicsDevice, Content, new Vector3(-162.5f, -237.5f, -600f)),
            new Rupee(GraphicsDevice, Content, new Vector3(-212.5f, -237.5f, -600f)),
            new Rupee(GraphicsDevice, Content, new Vector3(-262.5f, -237.5f, -600f)),
            new Rupee(GraphicsDevice, Content, new Vector3(-312.5f, -237.5f, -600f)),
            new Rupee(GraphicsDevice, Content, new Vector3(-362.5f, -237.5f, -600f)),
            new Rupee(GraphicsDevice, Content, new Vector3(-412.5f, -237.5f, -600f)),
            new Rupee(GraphicsDevice, Content, new Vector3(-462.5f, -237.5f, -600f)),
            new Rupee(GraphicsDevice, Content, new Vector3(-512.5f, -237.5f, -600f)),
            new Ruby(GraphicsDevice, Content, new Vector3(-487.5f, -237.5f, -537.5f)),
            new Ruby(GraphicsDevice, Content, new Vector3(-450f, -237.5f, -537.5f)),
            new Ruby(GraphicsDevice, Content, new Vector3(-362.5f, -237.5f, -537.5f)),
            new Ruby(GraphicsDevice, Content, new Vector3(-325f, -237.5f, -537.5f)),
            new Rupee(GraphicsDevice, Content, new Vector3(-237.5f, -237.5f, -537.5f)),
            new Rupee(GraphicsDevice, Content, new Vector3(-212.5f, -237.5f, -537.5f)),
            new Rupee(GraphicsDevice, Content, new Vector3(-187.5f, -237.5f, -537.5f)),
            new Rupee(GraphicsDevice, Content, new Vector3(-162.5f, -237.5f, -537.5f)),
            new Rupee(GraphicsDevice, Content, new Vector3(-137.5f, -237.5f, -537.5f)),
            new Rupee(GraphicsDevice, Content, new Vector3(-112.5f, -237.5f, -537.5f)),
            new Rupee(GraphicsDevice, Content, new Vector3(-87.5f, -237.5f, -537.5f)),
            new Rupee(GraphicsDevice, Content, new Vector3(-62.5f, -237.5f, -537.5f)),
            new Rupee(GraphicsDevice, Content, new Vector3(-37.5f, -237.5f, -537.5f)),
            new Rupee(GraphicsDevice, Content, new Vector3(-12.5f, -237.5f, -537.5f)),
            new Rupee(GraphicsDevice, Content, new Vector3(12.5f, -237.5f, -537.5f)),
            new Rupee(GraphicsDevice, Content, new Vector3(37.5f, -237.5f, -537.5f)),
            new Rupee(GraphicsDevice, Content, new Vector3(62.5f, -237.5f, -537.5f)),
            new Rupee(GraphicsDevice, Content, new Vector3(87.5f, -237.5f, -537.5f)),
            new Rupee(GraphicsDevice, Content, new Vector3(112.5f, -237.5f, -537.5f)),
            new Rupee(GraphicsDevice, Content, new Vector3(137.5f, -237.5f, -537.5f)),
            new Rupee(GraphicsDevice, Content, new Vector3(162.5f, -237.5f, -537.5f)),
            new SpeedBoost(GraphicsDevice, Content, new Vector3(187.5f, -237.5f, -537.5f)),
            new Rupee(GraphicsDevice, Content, new Vector3(-600f, -262.5f, -575f)),
            new Rupee(GraphicsDevice, Content, new Vector3(-712.5f, -287.5f, -575f)),
            new Rupee(GraphicsDevice, Content, new Vector3(-875f, -387.5f, -575f)),
            new Rupee(GraphicsDevice, Content, new Vector3(-875f, -387.5f, -712.5f))
        };
    }

    protected override void LoadCheckpoints()
    {
        Checkpoints = new List<Checkpoint>()
            {
                new Checkpoint(GraphicsDevice, Content, new Vector3(-1250f, 62.5f, -87.5f), scale: new Vector3(4f, 4f, 1f), MathHelper.Pi),
                new Checkpoint(GraphicsDevice, Content, new Vector3(-1237.5f, -12.5f, -987.5f), scale: new Vector3(6f, 4f, 1f), MathHelper.Pi),
                new Checkpoint(GraphicsDevice, Content, new Vector3(-887.5f, -62.5f, -1312.5f), scale: new Vector3(1f, 4f, 3f), MathHelper.Pi * .75f),
                new Checkpoint(GraphicsDevice, Content, new Vector3(287.5f, -175f, -862.5f), scale: new Vector3(3f, 4f, 1f), 0f),
                new Checkpoint(GraphicsDevice, Content, new Vector3(-875f, -350f, -787.5f), scale: new Vector3(4f, 4f, 1f), MathHelper.Pi),
                new Checkpoint(GraphicsDevice, Content, new Vector3(-875f, -362.5f, -1737.5f), new Vector3(8, 3, 1), MathHelper.Pi) // meta
            };
    }

}
