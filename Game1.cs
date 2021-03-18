﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;

namespace bounce_Ball
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Model ball, plane;
        Matrix world, view, proj;
        ball b;

        List<ball> ballList;

        //ballList = new List<ball>();
        //    for (int i = 0; i<50; i++)
        //    {
        //        ballList.Add(b = new bounce_ball.ball());
        //    }

    Random rand;
        float fov;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            fov = 60;

  
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            ball = Content.Load<Model>("sphere");
            plane = Content.Load<Model>("plane");

            rand = new Random();
            b = new ball(new Vector3(30, 25, 30),
                         ball.Meshes[0].BoundingSphere.Radius);
            ballList = new List<ball>();
            for (int i = 0; i < 50; i++)
            {
                ballList.Add(b = new ball(new Vector3((float)rand.NextDouble() * 30, 25, (float)rand.NextDouble() * 30),
                    ball.Meshes[0].BoundingSphere.Radius));
            }
        }
    

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                fov -= .5f;
                if(fov <= 0)
                {
                    fov = 0.5f;
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                fov += .5f;
            }

            // TODO: Add your update logic here

            foreach (ball b in ballList)
            {
                b.Update(gameTime);
                b.checkCollision(new Plane(Vector3.Up, 0f));

                // Check collision between balls 
               
                foreach (ball b2 in ballList)
                {
                    if (b != b2)
                        b.checkCollsion(b2);
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            view = Matrix.CreateLookAt(new Vector3(50, 50, 50),
                                        Vector3.Zero,
                                        Vector3.Up);
            proj = Matrix.CreatePerspectiveFieldOfView(
                            MathHelper.ToRadians(fov),
                            _graphics.PreferredBackBufferWidth/_graphics.PreferredBackBufferHeight,
                            1.0f,
                            1000f);
            world = Matrix.CreateScale(100);
            plane.Draw(world, view, proj);

            //world = Matrix.CreateTranslation(b.Pos);

            //ball.Draw(world, view, proj);

            foreach(ball b in ballList)
            {
                world = Matrix.CreateTranslation(b.Pos);
                ball.Draw(world, view, proj);
            }


            base.Draw(gameTime);
        }
    }
}
