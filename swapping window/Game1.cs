using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace swapping_window
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);



        Random rnd = new Random();

        // Frame counter.
        double elapsedTime = 0;
        int frameCount = 0;
        int FPS = 0;

        // Average frame count.
        float avgFC = 60;

        int slowCount = 0;
        float slowPercentage = 0;


        SpriteFont font;

        private Controls.MyCefBrowser b = new Controls.MyCefBrowser();

        System.Windows.Forms.Form f;
        KeyboardState lastKBState;

        List<Cube> listan = new List<Cube>();
        public Rectangle center = new Rectangle(400,300,10,10);
        Texture2D pixel;

        // Castar this till ett vanligt windows form!
        private System.Windows.Forms.Form TheGameAsForm
        {
            get
            {
                return (System.Windows.Forms.Form)System.Windows.Forms.Control.FromHandle(this.Window.Handle);
            }
        }


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            //SetForegroundWindow((System.IntPtr)Program.gamescreen.GetPtr());

            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            TheGameAsForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            TheGameAsForm.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.IsMouseVisible = true;
        }

        protected override void BeginRun()
        {
            base.BeginRun();

            f = (System.Windows.Forms.Form)System.Windows.Forms.Control.FromHandle(this.Window.Handle);
            b.Height = this.graphics.PreferredBackBufferHeight;
            f.Controls.Add(b);

            listan.Add(new Cube());
        }
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            

            base.Initialize();
        }

       

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("font");
            pixel = Content.Load<Texture2D>("pixel");

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Ifall cefsharp INTE är synligt kan vi byta till fullskärmsläge.
            if (b.Visible == false)
            {
                if (lastKBState.IsKeyDown(Keys.S) && Keyboard.GetState().IsKeyUp(Keys.S))
                {
                    if (graphics.IsFullScreen)
                    {
                        graphics.IsFullScreen = false;
                        graphics.ApplyChanges();
                    }
                    else
                    {
                        graphics.IsFullScreen = true;
                        graphics.ApplyChanges();
                    }
                }

            }

            // Ifall vi INTE är i fullskärmsläge kan vi visa cefsharp.
            if (graphics.IsFullScreen == false)
            {
                if (lastKBState.IsKeyDown(Keys.A) && Keyboard.GetState().IsKeyUp(Keys.A))
                {
                    b.Visible = !b.Visible;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }


            if(Keyboard.GetState().IsKeyDown(Keys.OemPlus))
            {
                for(int i = 0; i < 100; i++)
                listan.Add(new Cube());
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.OemMinus))
            {
                listan.RemoveRange(0, 100);
            }

            elapsedTime += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (gameTime.IsRunningSlowly)
            {
                // Last Update() was too slow. If all 60 frames are slow, 100% is slow.
                slowCount++;
            }

            if (elapsedTime > 1000.0)
            {
                FPS = frameCount;
                slowPercentage = (slowCount / 60.0f);

                // Will adapt over a 60 second period.
                avgFC = (avgFC / 60) * 59 + FPS / 60.0f;

                elapsedTime = 0;
                frameCount = 0;
                slowCount = 0;
            }


            base.Update(gameTime);

            lastKBState = Keyboard.GetState();
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here


            frameCount++;

            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
           
            spriteBatch.DrawString(
                font,
                "FPS " + ((int)FPS).ToString(),
                new Vector2(650, 0),
                Color.White);

            spriteBatch.DrawString(
                font,
                "Slow:" + (int)(slowPercentage * 100.0) + "%",
                new Vector2(650, 20),
                Color.White);

            spriteBatch.DrawString(
                font,
                "Amount:" + listan.Count,
                new Vector2(650, 40),
                Color.White);

            spriteBatch.DrawString(
                font,
                "press A to toggle visibility!",
                new Vector2(600, 60),
                Color.White);

            if (graphics.IsFullScreen)
            {
                spriteBatch.DrawString(
                    font,
                    "fullscreen (Press S to switch)",
                    new Vector2(600, 80),
                    Color.White);
            }
            else
            {
                spriteBatch.DrawString(
                    font,
                    "!fullscreen (Press S to switch)",
                    new Vector2(600, 80),
                    Color.White);
            }

            spriteBatch.Draw(pixel, center, Color.White);

            foreach(Cube c in listan)
            {
                c.pos = ComputeVelocity(rnd.Next(50,250), c.angle) + centerpos;
                c.angle += 0.01f;
                spriteBatch.Draw(pixel, c.pos, c.rect, Color.White);
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
        Vector2 centerpos = new Vector2(400, 300);

        public static double CalcAngle(float distx, float disty)
        {
            double angle = Math.Atan2(disty, distx) - Math.PI;

            if (angle > Math.PI * 2)
            {
                angle -= (float)Math.PI * 2;
            }
            if (angle < 0.0f)
            {
                angle += (float)Math.PI * 2;
            }

            return angle;
        }

        public static Vector2 ComputeVelocity(float speed, double angle)
        {
            Vector2 velocity = new Vector2();
            velocity.X = (float)Math.Cos(angle) * speed;
            velocity.Y = (float)Math.Sin(angle) * speed;

            return velocity;
        }
    }

    public class Cube
    {
        public Rectangle rect = new Rectangle(0, 0, 10, 10);
        public Vector2 pos = Vector2.Zero;
        public float angle = 0; 
    }
}
