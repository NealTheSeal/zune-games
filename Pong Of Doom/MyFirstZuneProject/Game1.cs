

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;


namespace MyFirstZuneProject
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    /// GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        int start = 0;
        int win = 0;
        int loss = 0;
        int diff = 3;
        int Cake = 0;
        int Player = 1;
        int menuz = 0;
        bool dontstop = false;

        Random RandomClass = new Random();

        float speedy = 1.5f;
        float speedy2 = 0; 
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
       
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
     
       
        private SpriteFont _font;
        private SpriteFont happy;
        protected override void Initialize()
        {
           // audioEngine = new AudioEngine("Content\\Audio\\MyGameAudio.xgs");
           // waveBank = new WaveBank(audioEngine, "Content\\Audio\\Wave Bank.xwb");
           // soundBank = new SoundBank(audioEngine, "Content\\Audio\\Sound Bank.xsb");
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        /// 
        // This is a texture we can render.
        Texture2D myTexture;
        Texture2D paddel;
        Texture2D compdle;
        Texture2D Lost;
        Texture2D Won;
        Texture2D cake;
        Texture2D backround;
        Texture2D menu;
        Texture2D credits;
        Texture2D underline;
        Texture2D setting; 

        // Set the coordinates to draw the sprite at.
        Vector2 spritePosition = Vector2.Zero;
        Vector2 spritePosition2 = Vector2.Zero;
        Vector2 spritePosition3 = Vector2.Zero;
        Vector2 spritePosition4 = Vector2.Zero;
        Vector2 spritePosition5 = Vector2.Zero;
        Vector2 spritePosition6 = Vector2.Zero;
        Vector2 really = Vector2.Zero; 

        protected override void LoadContent()
        {
            graphics.PreferredBackBufferWidth = 240;
            graphics.PreferredBackBufferHeight = 320;



            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            _font = Content.Load<SpriteFont>("Menu");
            happy = Content.Load<SpriteFont>("Menu");
            myTexture = Content.Load<Texture2D>("new3");
           paddel = Content.Load<Texture2D>("Thingy10");
           compdle = Content.Load<Texture2D>("Thingy11");
          Won = Content.Load<Texture2D>("win");
          Lost = Content.Load<Texture2D>("lost");
          cake = Content.Load<Texture2D>("new2");
          backround = Content.Load<Texture2D>("zp"); 
            credits = Content.Load<Texture2D>("creditz");
            menu = Content.Load<Texture2D>("Screen2");
            underline = Content.Load<Texture2D>("Screen");
            setting = Content.Load<Texture2D>("Settings"); 
           spritePosition2.Y = 1;
           spritePosition2.X = 222;


        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
        /// 
       
        // Store some information about the sprite's motion.
        Vector2 spriteSpeed = new Vector2(1001.0f, 1001.0f);
     
        Vector2 spriteSpeed2 = new Vector2(0f, 10);
        Vector2 spriteSpeed3 = new Vector2(0f, 14);


        protected override void Update(GameTime gameTime)
        {
           // audioEngine.Update(); 
            
            // Move the sprite around.
            UpdateSprite(gameTime);
            



                base.Update(gameTime);
            

        }


        void UpdateSprite(GameTime gameTime)
        {
           // KeyboardState keys = Keyboard.GetState();
            if (loss == 5 || win == 5)
            {
                spriteSpeed.Y = 0;
                spriteSpeed.X = 0;
                spriteSpeed2.Y = 0;
                spriteSpeed2.X = 0;
                spriteSpeed3.Y = 0;
                spriteSpeed3.X = 0;
                spritePosition5.Y = 100;
                spritePosition5.X = 24;
                spritePosition4.Y = 100;
                spritePosition4.X = 24;
          
                graphics.GraphicsDevice.Clear(Color.Black);

                // Draw the sprite.
                spriteBatch.Begin(SpriteBlendMode.AlphaBlend);
               

                spriteBatch.End();
            }

          

         
            // Move the sprite by speed, scaled by elapsed time.
            spritePosition += spriteSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
             //Current state of keyboard
           
            int MaxY2 = graphics.GraphicsDevice.Viewport.Height - paddel.Height;
            int MinY2 = 0;
           

            







            //Human Control



            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            {
                start = 0; 
            }


            if (Player == 1)
            {

                if (GamePad.GetState(PlayerIndex.One).DPad.Down == ButtonState.Pressed)
                {
                    spritePosition2 += spriteSpeed2;


                }
                if (GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed)
                {


                    spritePosition2 -= spriteSpeed2;


                }




                //Bellow is the AI... have funz
                spriteSpeed3.Y = 14;
                spriteSpeed2.Y = 10;
          
                if (spritePosition.X > 250)
                {

                    if (spritePosition.X > 250)
                    {

                        if (spritePosition.Y > spritePosition3.Y)
                        {
                            spritePosition3.Y += 4f;
                        }
                        if (spritePosition.Y < spritePosition3.Y)
                        {
                            spritePosition3.Y -= 4f;
                        }

                    }
                    else if (spritePosition.X > 275)
                    {

                        if (spritePosition.Y > spritePosition3.Y)
                        {
                            spritePosition3.Y += 2f;
                        }
                        if (spritePosition.Y < spritePosition3.Y)
                        {
                            spritePosition3.Y -= 2f;
                        }

                    }
                    else if (spritePosition.X > 300)
                    {

                        if (spritePosition.Y > spritePosition3.Y)
                        {
                            spritePosition3.Y += 1f;
                        }
                        if (spritePosition.Y < spritePosition3.Y)
                        {
                            spritePosition3.Y -= 1f;
                        }

                    }
                    else if (spritePosition.X > 325)
                    {

                        if (spritePosition.Y > spritePosition3.Y)
                        {
                            spritePosition3.Y += .5f;
                        }
                        if (spritePosition.Y < spritePosition3.Y)
                        {
                            spritePosition3.Y -= .5f;
                        }

                    }
                    else if (spritePosition.X > 350)
                    {

                        if (spritePosition.Y > spritePosition3.Y)
                        {
                            spritePosition3.Y += 0f;
                        }
                        if (spritePosition.Y < spritePosition3.Y)
                        {
                            spritePosition3.Y -= 0f;
                        }

                    }

                }







                else
                {
                    int RandomNumber = RandomClass.Next(1, 150);
                    if (RandomNumber % diff == 0)
                    {



                        if (spritePosition.Y > spritePosition3.Y)
                        {
                            spritePosition3 += spriteSpeed3;
                        }
                        if (spritePosition.Y < spritePosition3.Y)
                        {
                            spritePosition3 -= spriteSpeed3;
                        }
                    }
                }

            }


     






        

            if (spritePosition2.Y > MaxY2)
            {
                spritePosition2.Y = MaxY2 - 2;
         

              //   spriteSpeed.X = 0.01;
            }
            if (spritePosition2.Y < MinY2)
            {
              
                spritePosition2.Y = MinY2 + 2;
                // spriteSpeed.X = 0.01;
            }
            if (spritePosition3.Y > MaxY2)
            {
                spritePosition3.Y = MaxY2 - 2;


                //   spriteSpeed.X = 0.01;
            }
            if (spritePosition3.Y < MinY2)
            {

                spritePosition3.Y = MinY2 + 2;
                // spriteSpeed.X = 0.01;
            }




            int MinX;
             int MinY;
               int MaxX;
                    int MaxY;
                             


            if (Cake > 0)
            {

                 MaxX = graphics.GraphicsDevice.Viewport.Width - cake.Width;
                 MinX = 0;
                 MaxY = graphics.GraphicsDevice.Viewport.Height - cake.Height;
                 MinY = 0;
            }
            else
            {
                MaxX = graphics.GraphicsDevice.Viewport.Width - myTexture.Width;
                 MinX = 0;
                MaxY = graphics.GraphicsDevice.Viewport.Height - myTexture.Height;
                 MinY = 0;
            }
          

            float totalAverage = spritePosition2.Y + paddel.Height; 
            int paddelarea = (int)totalAverage;
           

                float totalAverag2 = spritePosition3.Y + compdle.Height; 
            int paddelarea2 = (int)totalAverag2;
         



            

           
 


            // Check for bounce.
           if (spritePosition.X > (MaxX-15))
            {
                if (spritePosition.Y > spritePosition2.Y -50 && spritePosition.Y < paddelarea+15)
                {
                    spriteSpeed.X *= -1.9f;
                    spritePosition.X = MaxX-15;
                  
                   
                   //  soundBank.PlayCue("awesome");
                      
                    
                    
                    if (speedy2 < 0.5)
                    speedy2 += 0.01f; 
                
 
                }

                else
                {
                    speedy = 1.5f; 
                    spritePosition.X = 10;
                    spritePosition.Y = 10;
            
                    loss++;
                    speedy2 = 0; 
                
               
                }
               // spriteSpeed.X *= -2;
               // spritePosition.X = MaxX;
            }

            else if (spritePosition.X < MinX)
            {
                if (spritePosition.Y > spritePosition3.Y - 50 && spritePosition.Y < paddelarea2 + 25)
                {
          
                    spriteSpeed.X *= -1.8f;
                    spritePosition.X = MinX;
                    if (speedy2 < 0.5)
                        speedy2 += 0.01f; 
                }
                else
                {
               
                    speedy = 1.5f;
                    spritePosition.X = 10;
                    spritePosition.Y = 10;
                    spriteSpeed.X *= -1.8f;
                    win++;
                    speedy2 = 0; 

                }
            }

            if (spritePosition.Y > MaxY)
            {
                spriteSpeed.Y *= -2.1f;
                spritePosition.Y = MaxY;

                   
            }

            else if (spritePosition.Y < MinY)
            {
               
                spriteSpeed.Y *= -2;
                spritePosition.Y = MinY;
            }
            //spriteSpeed2.Y += 1; 
           
                
            if (spriteSpeed.Y > 80)
                spriteSpeed.Y -= (1 - (speedy2));
            if (spriteSpeed.Y < -80)
                spriteSpeed.Y -= (1 - (speedy2));
            if (spriteSpeed.X > 80)
                spriteSpeed.X -= (1 - (speedy2));
            if (spriteSpeed.X < -80)
                spriteSpeed.X -= (1-(speedy2));

            if (spriteSpeed.X > 150)
                spriteSpeed.X = spriteSpeed.X / speedy;
            if (spriteSpeed.Y > 150)
                spriteSpeed.Y = spriteSpeed.Y / speedy; 


        }
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            //KeyboardState keys = Keyboard.GetState();
            graphics.GraphicsDevice.Clear(Color.Black);
        
        
            // Draw the sprite.
            spriteBatch.Begin(SpriteBlendMode.AlphaBlend);
            spriteBatch.Draw(backround, spritePosition6, Color.White); 
            if (GamePad.GetState(PlayerIndex.One).Buttons.B == ButtonState.Pressed)
            {
                if (Cake > 0)
                   Cake = 0;
              else
                   Cake = 1;
           }
           if (Cake == 0)
                spriteBatch.Draw(myTexture, spritePosition, Color.White);
            if (Cake == 1)
                spriteBatch.Draw(cake, spritePosition, Color.White);
            spriteBatch.Draw(paddel, spritePosition2, Color.White);
            spriteBatch.Draw(compdle, spritePosition3, Color.White);



            spriteBatch.DrawString(_font, loss.ToString(), new Vector2(80, 30), Color.Goldenrod); 
            spriteBatch.DrawString(_font, win.ToString(), new Vector2(160, 30), Color.Purple); 

            if (start == 0)
            {
                spriteBatch.Draw(menu, spritePosition6, Color.White);
                //spriteBatch.DrawString(_font, "Up/Down/Left/Right", new Vector2(35, 50), Color.White);
                float a = spriteSpeed.Y;
                float b = spriteSpeed.X;
                float c = spriteSpeed2.Y;
                float d = spriteSpeed2.X;
                float e = spriteSpeed3.Y;
                float f = spriteSpeed3.X;
                



                spriteSpeed.Y = 0;
                spriteSpeed.X = 0;
                spriteSpeed2.Y = 0;
                spriteSpeed2.X = 0;
                spriteSpeed3.Y = 0;
                spriteSpeed3.X = 0;
                int x = 0;

                if (GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed) dontstop = false;
                if (GamePad.GetState(PlayerIndex.One).DPad.Left == ButtonState.Pressed)
                {
                    menuz--;
                    if (menuz < 0)
                        menuz = 3;

                    while (x < 3000000)
                    {
                        x++;
                    }
                    dontstop = false;

                }
                if (GamePad.GetState(PlayerIndex.One).DPad.Right == ButtonState.Pressed)
                {
                    menuz++;
                    if (menuz > 3)
                        menuz = 0;
                    while (x < 3000000)
                    {
                        x++;
                    }
                    dontstop = false;

                }
                really.Y = 30;
                if (menuz == 3)
                    really.X = 240;
                if (menuz == 2)
                    really.X = 22 + 60 * menuz;
                else
                    really.X = 2 + 60 * menuz;

                spriteBatch.Draw(underline, really, Color.White);
           
                if ((GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed) && menuz == 0)
                {
                    spriteSpeed.Y = 90f;
                    spriteSpeed.X = 90f;
                    spriteSpeed2.Y = 10f;
                    spriteSpeed2.X = 0f;
                    spriteSpeed3.Y = 14f;
                    spriteSpeed3.X = 0f;
                    start++;
                    
                }
               
                if (((GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed) || dontstop == true)&& menuz == 1)
                {
                     dontstop = true;
                    if (dontstop == true)
                    {
                        spriteBatch.Draw(setting, spritePosition6, Color.White);
                       
                            if (GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed)
                            {
                                diff = 1;
                                dontstop = false;
                            }
                            if (GamePad.GetState(PlayerIndex.One).DPad.Down == ButtonState.Pressed)
                            {
                                diff = 10;
                                dontstop = false;
                            }
                            if (GamePad.GetState(PlayerIndex.One).DPad.Left == ButtonState.Pressed)
                            {
                                diff = 5;
                                dontstop = false;
                            }
                            if (GamePad.GetState(PlayerIndex.One).DPad.Right == ButtonState.Pressed)
                            {
                                diff = 7;
                                dontstop = false;
                            }
                        
                    }
                }
                

                
                if (((GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed || dontstop == true)) && menuz == 2)
                {
                    spriteBatch.Draw(credits, spritePosition6, Color.White);
                    if (dontstop == false)
                    {
                        while (x < 870000)
                        {
                            x++;
                        }

                    }
                    dontstop = true;
                    
                      
                        
                       if ( GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed)
                            dontstop = false;
                       
                            
                    }
                
                if ((GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed) && menuz == 3)
                {
                    Exit(); 
                }

                /*
                 if (GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed)
                 {
                     spriteSpeed.Y = 90f;
                     spriteSpeed.X = 90f;
                     spriteSpeed2.Y = 10f;
                     spriteSpeed2.X = 0f;
                     spriteSpeed3.Y = 14f;
                     spriteSpeed3.X = 0f;
                     start++;
                     diff = 1; 
                 }
                 if (GamePad.GetState(PlayerIndex.One).DPad.Down == ButtonState.Pressed)
                 {
                     spriteSpeed.Y = 50f;
                     spriteSpeed.X = 50f;
                     spriteSpeed2.Y = 10f;
                     spriteSpeed2.X = 0f;
                     spriteSpeed3.Y = 14f;
                     spriteSpeed3.X = 0f;
                     start++;
                     diff = 2; 
                 }
              
                 if (GamePad.GetState(PlayerIndex.One).DPad.Left == ButtonState.Pressed)
                 {
                     spriteSpeed.Y = 50f;
                     spriteSpeed.X = 50f;
                     spriteSpeed2.Y = 10f;
                     spriteSpeed2.X = 0f;
                     spriteSpeed3.Y = 14f;
                     spriteSpeed3.X = 0f;
                     start++;
                     diff = 3; 
                 }
                 if (GamePad.GetState(PlayerIndex.One).DPad.Right == ButtonState.Pressed)
                 {
                     spriteSpeed.Y = 50f;
                     spriteSpeed.X = 50f;
                     spriteSpeed2.Y = 10f;
                     spriteSpeed2.X = 0f;
                     spriteSpeed3.Y = 14f;
                     spriteSpeed3.X = 0f;
                     start++;
                     diff = 4; 
                 }
              
                 */


            }
                if (win == 5)
                {
                spriteBatch.Draw(Won, spritePosition4, Color.White);
                 //spriteBatch.DrawString(_font, "Again?(up/down)", new Vector2(120, 150), Color.White);
                 if (GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed)
                 {
                     win = 0;
                     loss = 0;
                     start = 0;
                     spriteSpeed.Y = 1001f;
                     spriteSpeed.X = 1001f;
                     spriteSpeed2.Y = 10f;
                     spriteSpeed2.X = 0f;
                     spriteSpeed3.Y = 14f;
                     spriteSpeed3.X = 0f;
                   
                 }
                 if (GamePad.GetState(PlayerIndex.One).DPad.Down == ButtonState.Pressed)
                    {
                        Exit();
                    }
                
                }
            if (loss == 5)
            {
                spriteBatch.Draw(Lost, spritePosition5, Color.White);
                     //spriteBatch.DrawString(_font, "Again? (y/n)", new Vector2(120, 150), Color.White);
                     if (GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed)
                {
                    win = 0;
                    loss = 0;
                    start = 0;
                    spriteSpeed.Y = 1001f;
                    spriteSpeed.X = 1001f;
                    spriteSpeed2.Y = 10f;
                    spriteSpeed2.X = 0f;
                    spriteSpeed3.Y = 14f;
                    spriteSpeed3.X = 0f;
                    
                }
                if (GamePad.GetState(PlayerIndex.One).DPad.Down == ButtonState.Pressed)
                {
                    win = 0;
                    loss = 0;
                    start = 0;
                    spriteSpeed.Y = 1001f;
                    spriteSpeed.X = 1001f;
                    spriteSpeed2.Y = 10f;
                    spriteSpeed2.X = 0f;
                    spriteSpeed3.Y = 14f;
                    spriteSpeed3.X = 0f;
                    
                }
           
            }
       
          

  

            spriteBatch.End();
 


            base.Draw(gameTime);
        }
    }
}
