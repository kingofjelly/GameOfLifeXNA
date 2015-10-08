using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace GameOfLifeXNA
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public int cellPositionX = 0;
        public int cellPositionY = 0;

        public const int screenWidth = 777;
        public const int screenHeight = 777;

        //to be used to determine if neighbors are present
        public int arrayPositioni;
        public int arrayPositionj;

        public int refreshTimer = 0;

        CellObject[,] GameGrid = new CellObject[70, 70];
        //this is a recreation of my game of life game from cmd, but using XNA.
        //It will be to a larger scale and look better, due to non-oblong shaped cells. 

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            //allow screen resizing
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;
            Window.AllowUserResizing = true;
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
            //default screen size = 800 x 600. change it to 777x777
            //in here i initialize the array and fill it with objects
            for (int i = 1; i <= 70; i++)
            {
                for (int j = 1; j <= 70; j++)
                {
                    GameGrid[i -1, j -1] = new CellObject();//initialize all the cell objects
                }
            }
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

            
            // TODO: use this.Content to load your game content here
            //loop through 2d array, loading content. thats spriteBatch only
            for (int i = 1; i <= 70; i++)
            {
                for (int j = 1; j <= 70; j++)
                {
                    arrayPositioni = i - 1;
                    arrayPositionj = j - 1;

                    GameGrid[i -1, j -1].LoadContent(this.Content, cellPositionX, cellPositionY, arrayPositioni, arrayPositionj);
                    cellPositionX = cellPositionX + 11;
                }
                cellPositionY = cellPositionY + 11;//bump down == spaces. Adding it here, ensures
                //that first iteration has 0 for x axis
                //reset X as well, as it's back to 0
                cellPositionX = 0;
            }
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
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            if (refreshTimer == 25)
            {
                //run update on for loop. will cause a refresh every second
                for (int i = 1; i <= 70; i++)
                {
                    for (int j = 1; j <= 70; j++)
                    {
                        //do code here.
                        //Deal with:
                        //1. Check neighbors + add to is neighbor alive. Reset to 0 each loop?
                        //2. 
                        /*BELOW WILL ONLY RUN IF IT HAS NEIGHBORS*/

                        GameGrid[i - 1, j - 1].aliveNeighbours = 0;//RESETS THE INDIVIDUAL CELLS NEIGHBOUR COUNT TO 0, WHEN RE-CALLED

                        if (GameGrid[i - 1, j - 1].topLeft == true)//if it has a top left neighbout
                        {
                            if (GameGrid[i - 2, j - 2].isAlive == true)//-2 as we're going up a level and back one
                            {
                                GameGrid[i - 1, j - 1].aliveNeighbours++;//add one to this variable
                            }
                        }

                        if (GameGrid[i - 1, j - 1].top == true)
                        {
                            if (GameGrid[i - 2, j - 1].isAlive == true)//-2 as we're going up a level only
                            {
                                GameGrid[i - 1, j - 1].aliveNeighbours++;//add one to this variable
                            }
                        }

                        if (GameGrid[i - 1, j - 1].topRight == true)//-2 and 0 as we're going up one and to the right
                        {
                            if (GameGrid[i - 2, j].isAlive == true)//-2 as we're going up a level right one
                            {
                                GameGrid[i - 1, j - 1].aliveNeighbours++;//add one to this variable
                            }
                        }

                        if (GameGrid[i - 1, j - 1].right == true)
                        {
                            if (GameGrid[i - 1, j].isAlive == true)//we're going just one to right
                            {
                                GameGrid[i - 1, j - 1].aliveNeighbours++;//add one to this variable
                            }
                        }

                        if (GameGrid[i - 1, j - 1].bottomRight == true)
                        {
                            if (GameGrid[i, j].isAlive == true)//we're going down 1, 1 to right
                            {
                                GameGrid[i - 1, j - 1].aliveNeighbours++;//add one to this variable
                            }
                        }

                        if (GameGrid[i - 1, j - 1].bottom == true)
                        {
                            if (GameGrid[i, j - 1].isAlive == true)//we're going down 1, 1 to right
                            {
                                GameGrid[i - 1, j - 1].aliveNeighbours++;//add one to this variable
                            }
                        }

                        if (GameGrid[i - 1, j - 1].bottomLeft == true)
                        {
                            if (GameGrid[i, j - 2].isAlive == true)//we're going down 1, 1 to right
                            {
                                GameGrid[i - 1, j - 1].aliveNeighbours++;//add one to this variable
                            }
                        }

                        if (GameGrid[i - 1, j - 1].left == true)
                        {
                            if (GameGrid[i - 1, j - 2].isAlive == true)//we're going down 1, 1 to right
                            {
                                GameGrid[i - 1, j - 1].aliveNeighbours++;//add one to this variable
                            }
                        }
                    }
                }

                //LOOP THROUGH NOW TO CHANGE STATUS OF EACH CELL, DEPENDING ON LIVE NEIGHBORS AND RULES
                for (int i = 1; i <= 70; i++)
                {
                    for (int j = 1; j <= 70; j++)
                    {
                        //if under 2 live neighbours, dies by under population
                        if (GameGrid[i - 1, j - 1].aliveNeighbours < 2 && GameGrid[i - 1, j - 1].isAlive == true)
                        {
                            GameGrid[i - 1, j - 1].isAlive = false;//it dies
                        }
                        //if 2 or 3 neighbours, cell remains alive
                        if (GameGrid[i - 1, j - 1].aliveNeighbours == 2 && GameGrid[i - 1, j - 1].isAlive == true || GameGrid[i - 1, j - 1].aliveNeighbours == 3 && GameGrid[i - 1, j - 1].isAlive == true)
                        {
                            GameGrid[i - 1, j - 1].isAlive = true;//
                        }
                        //if over 3 neighbours alive, cell dies due to overcrowding
                        if (GameGrid[i - 1, j - 1].aliveNeighbours > 3 && GameGrid[i - 1, j - 1].isAlive == true)
                        {
                            GameGrid[i - 1, j - 1].isAlive = false;
                        }
                        //if dead && neighbours = 3, alive due to repopulation
                        if (GameGrid[i - 1, j - 1].aliveNeighbours == 3 && GameGrid[i - 1, j - 1].isAlive == false)
                        {
                            GameGrid[i - 1, j - 1].isAlive = true;
                        }
                    }
                }


                refreshTimer = 0;
            }
            refreshTimer++;
            
            // TODO: Add your update logic here
            //not needed first iteration. will be the check to check if alive, then re-load the cell
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            //update 2d array, pass in x + Y co-ords. or do this during load? could be during load
            spriteBatch.Begin();
            for (int i = 1; i <= 70; i++)
            {
                for (int j = 1; j <= 70; j++)
                {
                    GameGrid[i -1, j -1].Draw(this.spriteBatch);
                }
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }



    }
}
