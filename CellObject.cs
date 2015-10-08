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
    class CellObject
    {
        static Random numberGen = new Random();//this num gen is used called upon instantiasation of object

        static int randomNumer()//running number gen for 1,2 for the letter from the die.1 = dead. 2 = alive
        {
            int roll;//contains the initial roll of the number gen
            int dieRollUpdate;//contains the updated value
            roll = numberGen.Next(1, 3);//uses the global Random generator of numberGen, to gen a no. between 1 & 7, then 1,17
            dieRollUpdate = roll;
            return dieRollUpdate;//returns this value. this is the value that is evaluated, then re-rolled if needed.
        }

        public bool topLeft { get; set; }
        public bool top { get; set; }
        public bool topRight { get; set; }
        public bool right { get; set; }
        public bool bottomRight { get; set; }
        public bool bottom { get; set; }
        public bool bottomLeft { get; set; }
        public bool left { get; set; }
        public bool isAlive { get; set; }//X
        public int generationsPassed { get; set; }
        public int iValue { get; set; }//X
        public int jValue { get; set; }//X
        public string testChar { get; set; }//used for testing after grid creation + population //X
        public int aliveNeighbours { get; set; }//this will be accessed from main method, to tally up living neighbour cells
        //load
        Texture2D cellTexture;

        Texture2D cellAlive;
        Texture2D cellDead;

        Vector2 CellPosition = new Vector2();//will be used to hold X & Y co-ords of this cell
        public int cellX;
        public int cellY;
        
        
        public int initialDOA;//intially, is cell DOA

        public void LoadContent(ContentManager theContent, int cellPositionX, int cellPositionY, int iArrayValue, int jArrayValue)
        {
            CellPosition.X = cellPositionX;
            CellPosition.Y = cellPositionY;
            initialDOA = randomNumer();
            iValue = iArrayValue;
            jValue = jArrayValue;

            if (initialDOA == 1)
            {
                //load cell 1
                //cellTexture = theContent.Load<Texture2D>("AliveCell");
                isAlive = true;
            }
            if (initialDOA == 2)
            {
                //cellTexture = theContent.Load<Texture2D>("DeadCell");
                isAlive = false;
            }
            cellDead = theContent.Load<Texture2D>("DeadCell");
            cellAlive = theContent.Load<Texture2D>("AliveCell");
            //set all neighbour squares to be true initially
            top = true;
            topRight = true;
            right = true;
            bottomRight = true;
            bottom = true;
            bottomLeft = true;
            left = true;
            topLeft = true;

            //run code to find out if neighbours are actually present
            if (iValue == 0 && jValue == 0)
            {
                //top left corner
                bottomLeft = false;
                left = false;
                topLeft = false;
                top = false;
                topRight = false;
            }
            if (iValue == 0 && jValue == 69)
            {
                //top right corner
                topLeft = false;
                top = false;
                topRight = false;
                right = false;
                bottomRight = false;
            }
            if (iValue == 69 && jValue == 0)
            {
                //bottom left
                topLeft = false;
                left = false;
                bottomLeft = false;
                bottom = false;
                bottomRight = false;
            }
            if (iValue == 69 && jValue == 69)
            {
                //bottom right
                bottomLeft = false;
                bottom = false;
                bottomRight = false;
                right = false;
                topRight = false;
            }
            //disable side if on edge
            if (iValue > 0 && iValue < 69 && jValue == 0)
            {
                //left edge
                left = false;
                topLeft = false;
                bottomLeft = false;
            }
            if (iValue == 0 && jValue > 0 && jValue < 69)
            {
                //top edge
                top = false;
                topLeft = false;
                topRight = false;
            }
            if (iValue > 0 && iValue < 69 && jValue == 69)
            {
                //right edge
                right = false;
                topRight = false;
                bottomRight = false;
            }
            if (iValue == 69 && jValue > 0 && jValue < 69)
            {
                //bottom edge
                bottom = false;
                bottomLeft = false;
                bottomRight = false;
            }


        }

        public void Update(GameTime theGameTime)
        {
            //update if alive? this will check every 100ms. this will be determined by a timer
            //check neighbours
        }

        public void Draw(SpriteBatch theSpriteBatch)
        {
            if (isAlive)
            {
                theSpriteBatch.Draw(cellAlive, CellPosition, Color.White);
            }
            if (!isAlive)
            {
                theSpriteBatch.Draw(cellDead, CellPosition, Color.White);
            }
            //theSpriteBatch.Draw(cellTexture, CellPosition, Color.White);
            //need to reload this, due to changing if dead or alive
        }
    


        //update
        public void checkIfNeighbourIsAlive()
        {

        }

        //draw
    }
}
