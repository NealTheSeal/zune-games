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

using FocusedGames.Thrust.Input;

namespace Pacman
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class PacmanGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont scoreText;

        Rectangle[] pts;
        Rectangle[] rects;
        Rectangle tunnel1;
        Rectangle tunnel2;
        Rectangle closedoor;
        int totalGhosts = 2;
        int possiblePoints;
        int noRects;
        int noPts;
        int Score;
        int Level;
        int Lives;

        Texture2D spriteClosed;
        Texture2D spriteUp;
        Texture2D spriteDown;
        Texture2D spriteLeft;
        Texture2D spriteRight;
        Texture2D ghost;
        Texture2D ghostEye;
        Texture2D ghostPupil;
        Texture2D rect;
        Texture2D door;
        Texture2D smPoint;
        Texture2D gameover;
        Texture2D background;

        int[] ghostX; // Arrays to hold the X,Y co-ordinate for each ghost
        int[] ghostY;
        int[] ghostmoveX; // Arrays to hold the new X,Y co-ordinate for each ghost
        int[] ghostmoveY;
        int[] ghostLastDirection; // Holds a number representing the last direction of a ghost
        int noGhosts; // Holds the number of ghosts in the game
        int randomSeed = 0;
        double storedSecs = 0;

        int spriteDirection = 3; // Starts pacman facing right
        int spriteX = 112;  // Starting positions for the Pacman character
        int spriteY = 226;  // (just above the ghost's door!!!! :)
        int moveY = 0;
        int moveX = 0;
        bool mouthOpen = false;
        bool[] changeDirection;
        bool[] gtChange;
        bool[] forceGhosts;
        bool forced = false;
        bool endGame = false;
        double firstRun = 0; // gameTime seconds at the time the game started
        bool firstCheck = false; // check to see if the game was started
        bool flashDoor = false;
        bool shutDoor = false;
        bool dying = false;
        bool gameRunning = true;
        int DirectionBuffer;
        DateTime startTime;
        DateTime stopTime;
        bool Timerrunning = false;
        TimeSpan interval;

#if DEBUG
        string debug = "By Umisguy & Marshillboy";
#endif

        public PacmanGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            this.graphics.PreferredBackBufferWidth = 240;
            this.graphics.PreferredBackBufferHeight = 320;

            Score = 0; // Reset the score
            Level = 1; // Reset the level
            Lives = 3; // Reset the lives
             DirectionBuffer = 5; // Reset the Pac-Man direction of Movement Buffer
            CreateGhosts(totalGhosts); // Create all the ghosts

            CreateOutOfBounds(); // Create all the world-boundaries

            CreatePoints(); // Create all the points
        }

        void CreateGhosts(int howMany)
        {
            noGhosts = howMany;
            ghostX = new int[noGhosts];
            ghostY = new int[noGhosts];
            ghostmoveX = new int[noGhosts];
            ghostmoveY = new int[noGhosts];
            ghostLastDirection = new int[noGhosts];
            gtChange = new bool[noGhosts];
            changeDirection = new bool[noGhosts];
            forceGhosts = new bool[noGhosts];

            for (int i = 0; i < noGhosts; i++)
            {
                ghostX[i] = 97;
                ghostY[i] = 149;
                ghostmoveX[i] = 0;
                ghostmoveY[i] = 0;
                ghostLastDirection[i] = -1;
                gtChange[i] = false;
                changeDirection[i] = false;
                forceGhosts[i] = false;
            }

        }

        void CreateOutOfBounds()
        {
            noRects = 55;
            rects = new Rectangle[noRects];
            rects[0] = new Rectangle(4, 38, 233, 6); //outside boundary
            rects[1] = new Rectangle(115, 38, 10, 39);
            rects[2] = new Rectangle(4, 38, 6, 85);
            rects[3] = new Rectangle(4, 117, 47, 35);
            rects[4] = new Rectangle(4, 167, 47, 34);
            rects[5] = new Rectangle(4, 195, 6, 101);
            rects[6] = new Rectangle(4, 241, 22, 10);
            rects[7] = new Rectangle(4, 290, 233, 6);
            rects[8] = new Rectangle(231, 195, 6, 102);
            rects[9] = new Rectangle(190, 167, 47, 34);
            rects[10] = new Rectangle(190, 117, 47, 35);
            rects[11] = new Rectangle(231, 38, 6, 85);
            rects[12] = new Rectangle(25, 59, 26, 18); //inside barriers
            rects[13] = new Rectangle(66, 59, 34, 18);
            rects[14] = new Rectangle(140, 59, 35, 18);
            rects[15] = new Rectangle(190, 59, 26, 18);
            rects[16] = new Rectangle(25, 92, 26, 10);
            rects[17] = new Rectangle(66, 92, 10, 60);
            rects[18] = new Rectangle(66, 117, 34, 10);
            rects[19] = new Rectangle(91, 92, 59, 10);
            rects[20] = new Rectangle(115, 92, 10, 35);
            rects[21] = new Rectangle(140, 117, 35, 10);
            rects[22] = new Rectangle(190, 92, 26, 10);
            rects[23] = new Rectangle(66, 167, 10, 34); //lower half of level boundaries
            rects[24] = new Rectangle(91, 191, 59, 10);
            rects[25] = new Rectangle(115, 191, 10, 35);
            rects[26] = new Rectangle(165, 167, 10, 34);
            rects[27] = new Rectangle(25, 216, 26, 10);
            rects[28] = new Rectangle(41, 216, 10, 35);
            rects[29] = new Rectangle(66, 216, 34, 10);
            rects[30] = new Rectangle(140, 216, 35, 10);
            rects[31] = new Rectangle(190, 216, 26, 10);
            rects[32] = new Rectangle(190, 216, 9, 35);
            rects[33] = new Rectangle(25, 266, 75, 9);
            rects[34] = new Rectangle(66, 241, 10, 34);
            rects[35] = new Rectangle(91, 241, 59, 10);
            rects[36] = new Rectangle(115, 241, 10, 34);
            rects[37] = new Rectangle(140, 266, 76, 9);
            rects[38] = new Rectangle(165, 241, 10, 34);
            rects[39] = new Rectangle(91, 143, 5, 34); //ghost pen
            rects[40] = new Rectangle(91, 142, 19, 5);
            rects[41] = new Rectangle(131, 143, 19, 5);
            rects[42] = new Rectangle(144, 142, 6, 34);
            rects[43] = new Rectangle(91, 171, 59, 5);
            rects[44] = new Rectangle(110, 144, 21, 4); // this is the door!
            rects[45] = new Rectangle(214, 241, 23, 10);
            rects[46] = new Rectangle(165, 92, 10, 60);

            tunnel1 = new Rectangle(0, 152, 5, 15);
            tunnel2 = new Rectangle(236, 152, 4, 15);
            closedoor = new Rectangle(110, 144, 21, 2);

        }

        void CreatePoints()
        {
            noPts = 756; // 29 * 26 [grid of points]
            pts = new Rectangle[noPts];
            //point array:
            pts[1] = new Rectangle(16, 51, 2, 2);
            pts[2] = new Rectangle(24, 51, 2, 2);
            pts[3] = new Rectangle(33, 51, 2, 2);
            pts[4] = new Rectangle(41, 51, 2, 2);
            pts[5] = new Rectangle(49, 51, 2, 2);
            pts[6] = new Rectangle(57, 51, 2, 2);
            pts[7] = new Rectangle(66, 51, 2, 2);
            pts[8] = new Rectangle(74, 51, 2, 2);
            pts[9] = new Rectangle(82, 51, 2, 2);
            pts[10] = new Rectangle(90, 51, 2, 2);
            pts[11] = new Rectangle(98, 51, 2, 2);
            pts[12] = new Rectangle(107, 51, 2, 2);
            pts[13] = new Rectangle(132, 51, 2, 2);
            pts[14] = new Rectangle(140, 51, 2, 2);
            pts[15] = new Rectangle(148, 51, 2, 2);
            pts[16] = new Rectangle(156, 51, 2, 2);
            pts[17] = new Rectangle(165, 51, 2, 2);
            pts[18] = new Rectangle(173, 51, 2, 2);
            pts[19] = new Rectangle(181, 51, 2, 2);
            pts[20] = new Rectangle(189, 51, 2, 2);
            pts[21] = new Rectangle(198, 51, 2, 2);
            pts[22] = new Rectangle(206, 51, 2, 2);
            pts[23] = new Rectangle(214, 51, 2, 2);
            pts[24] = new Rectangle(222, 51, 2, 2);
            pts[25] = new Rectangle(16, 59, 2, 2);
            pts[26] = new Rectangle(57, 59, 2, 2);
            pts[27] = new Rectangle(107, 59, 2, 2);
            pts[28] = new Rectangle(132, 59, 2, 2);
            pts[29] = new Rectangle(181, 59, 2, 2);
            pts[30] = new Rectangle(222, 59, 2, 2);
            pts[31] = new Rectangle(13, 65, 2, 2);
            pts[32] = new Rectangle(57, 67, 2, 2);
            pts[33] = new Rectangle(107, 67, 2, 2);
            pts[34] = new Rectangle(132, 67, 2, 2);
            pts[35] = new Rectangle(181, 67, 2, 2);
            pts[36] = new Rectangle(220, 65, 2, 2);
            pts[37] = new Rectangle(16, 75, 2, 2);
            pts[38] = new Rectangle(57, 75, 2, 2);
            pts[39] = new Rectangle(107, 75, 2, 2);
            pts[40] = new Rectangle(132, 75, 2, 2);
            pts[41] = new Rectangle(181, 75, 2, 2);
            pts[42] = new Rectangle(223, 75, 2, 2);
            pts[43] = new Rectangle(16, 84, 2, 2);
            pts[44] = new Rectangle(24, 84, 2, 2);
            pts[45] = new Rectangle(33, 84, 2, 2);
            pts[46] = new Rectangle(41, 84, 2, 2);
            pts[47] = new Rectangle(49, 84, 2, 2);
            pts[48] = new Rectangle(57, 84, 2, 2);
            pts[49] = new Rectangle(66, 84, 2, 2);
            pts[50] = new Rectangle(74, 84, 2, 2);
            pts[51] = new Rectangle(82, 84, 2, 2);
            pts[52] = new Rectangle(90, 84, 2, 2);
            pts[53] = new Rectangle(99, 84, 2, 2);
            pts[54] = new Rectangle(107, 84, 2, 2);
            pts[55] = new Rectangle(115, 84, 2, 2);
            pts[56] = new Rectangle(123, 84, 2, 2);
            pts[57] = new Rectangle(132, 84, 2, 2);
            pts[58] = new Rectangle(140, 84, 2, 2);
            pts[59] = new Rectangle(148, 84, 2, 2);
            pts[60] = new Rectangle(156, 84, 2, 2);
            pts[61] = new Rectangle(165, 84, 2, 2);
            pts[62] = new Rectangle(173, 84, 2, 2);
            pts[63] = new Rectangle(181, 84, 2, 2);
            pts[64] = new Rectangle(189, 84, 2, 2);
            pts[65] = new Rectangle(198, 84, 2, 2);
            pts[66] = new Rectangle(206, 84, 2, 2);
            pts[67] = new Rectangle(214, 84, 2, 2);
            pts[68] = new Rectangle(222, 84, 2, 2);
            pts[69] = new Rectangle(16, 92, 2, 2);
            pts[70] = new Rectangle(57, 92, 2, 2);
            pts[71] = new Rectangle(82, 92, 2, 2);
            pts[72] = new Rectangle(156, 92, 2, 2);
            pts[73] = new Rectangle(181, 92, 2, 2);
            pts[74] = new Rectangle(222, 92, 2, 2);
            pts[75] = new Rectangle(16, 100, 2, 2);
            pts[76] = new Rectangle(57, 100, 2, 2);
            pts[77] = new Rectangle(82, 100, 2, 2);
            pts[78] = new Rectangle(156, 100, 2, 2);
            pts[79] = new Rectangle(181, 100, 2, 2);
            pts[80] = new Rectangle(222, 100, 2, 2);
            pts[81] = new Rectangle(16, 108, 2, 2);
            pts[82] = new Rectangle(24, 108, 2, 2);
            pts[83] = new Rectangle(33, 108, 2, 2);
            pts[84] = new Rectangle(41, 108, 2, 2);
            pts[85] = new Rectangle(49, 108, 2, 2);
            pts[86] = new Rectangle(57, 108, 2, 2);
            pts[87] = new Rectangle(82, 108, 2, 2);
            pts[88] = new Rectangle(90, 108, 2, 2);
            pts[89] = new Rectangle(98, 108, 2, 2);
            pts[90] = new Rectangle(107, 108, 2, 2);
            pts[91] = new Rectangle(132, 108, 2, 2);
            pts[92] = new Rectangle(140, 108, 2, 2);
            pts[93] = new Rectangle(148, 108, 2, 2);
            pts[94] = new Rectangle(156, 108, 2, 2);
            pts[95] = new Rectangle(181, 108, 2, 2);
            pts[96] = new Rectangle(189, 108, 2, 2);
            pts[97] = new Rectangle(198, 108, 2, 2);
            pts[98] = new Rectangle(206, 108, 2, 2);
            pts[99] = new Rectangle(214, 108, 2, 2);
            pts[100] = new Rectangle(222, 108, 2, 2);
            pts[101] = new Rectangle(57, 117, 2, 2);
            pts[102] = new Rectangle(181, 117, 2, 2);
            pts[103] = new Rectangle(57, 125, 2, 2);
            pts[104] = new Rectangle(181, 125, 2, 2);
            pts[105] = new Rectangle(57, 133, 2, 2);
            pts[106] = new Rectangle(181, 133, 2, 2);
            pts[107] = new Rectangle(57, 142, 2, 2);
            pts[108] = new Rectangle(181, 142, 2, 2);
            pts[109] = new Rectangle(57, 150, 2, 2);
            pts[110] = new Rectangle(181, 150, 2, 2);
            pts[111] = new Rectangle(57, 158, 2, 2);
            pts[112] = new Rectangle(181, 158, 2, 2);
            pts[113] = new Rectangle(57, 166, 2, 2);
            pts[114] = new Rectangle(181, 166, 2, 2);
            pts[115] = new Rectangle(57, 175, 2, 2);
            pts[116] = new Rectangle(181, 175, 2, 2);
            pts[117] = new Rectangle(57, 183, 2, 2);
            pts[118] = new Rectangle(181, 183, 2, 2);
            pts[119] = new Rectangle(57, 191, 2, 2);
            pts[120] = new Rectangle(181, 191, 2, 2);
            pts[121] = new Rectangle(57, 199, 2, 2);
            pts[122] = new Rectangle(181, 199, 2, 2);
            pts[123] = new Rectangle(16, 207, 2, 2);
            pts[124] = new Rectangle(24, 207, 2, 2);
            pts[125] = new Rectangle(33, 207, 2, 2);
            pts[126] = new Rectangle(41, 207, 2, 2);
            pts[127] = new Rectangle(49, 207, 2, 2);
            pts[128] = new Rectangle(57, 207, 2, 2);
            pts[129] = new Rectangle(66, 207, 2, 2);
            pts[130] = new Rectangle(74, 207, 2, 2);
            pts[131] = new Rectangle(82, 207, 2, 2);
            pts[132] = new Rectangle(90, 207, 2, 2);
            pts[133] = new Rectangle(98, 207, 2, 2);
            pts[134] = new Rectangle(107, 207, 2, 2);
            pts[135] = new Rectangle(131, 207, 2, 2);
            pts[136] = new Rectangle(140, 207, 2, 2);
            pts[137] = new Rectangle(148, 207, 2, 2);
            pts[138] = new Rectangle(156, 207, 2, 2);
            pts[139] = new Rectangle(165, 207, 2, 2);
            pts[140] = new Rectangle(173, 207, 2, 2);
            pts[141] = new Rectangle(181, 207, 2, 2);
            pts[142] = new Rectangle(189, 207, 2, 2);
            pts[143] = new Rectangle(198, 207, 2, 2);
            pts[144] = new Rectangle(206, 207, 2, 2);
            pts[145] = new Rectangle(214, 207, 2, 2);
            pts[146] = new Rectangle(222, 207, 2, 2);
            pts[147] = new Rectangle(16, 216, 2, 2);
            pts[148] = new Rectangle(57, 216, 2, 2);
            pts[149] = new Rectangle(107, 216, 2, 2);
            pts[150] = new Rectangle(131, 216, 2, 2);
            pts[151] = new Rectangle(181, 216, 2, 2);
            pts[152] = new Rectangle(222, 216, 2, 2);
            pts[153] = new Rectangle(16, 224, 2, 2);
            pts[154] = new Rectangle(57, 224, 2, 2);
            pts[155] = new Rectangle(107, 224, 2, 2);
            pts[156] = new Rectangle(131, 224, 2, 2);
            pts[157] = new Rectangle(181, 224, 2, 2);
            pts[158] = new Rectangle(222, 224, 2, 2);
            pts[159] = new Rectangle(13, 230, 2, 2);
            pts[160] = new Rectangle(24, 232, 2, 2);
            pts[161] = new Rectangle(33, 232, 2, 2);
            pts[162] = new Rectangle(57, 232, 2, 2);
            pts[163] = new Rectangle(66, 232, 2, 2);
            pts[164] = new Rectangle(74, 232, 2, 2);
            pts[165] = new Rectangle(82, 232, 2, 2);
            pts[166] = new Rectangle(90, 232, 2, 2);
            pts[167] = new Rectangle(98, 232, 2, 2);
            pts[168] = new Rectangle(107, 232, 2, 2);
            pts[169] = new Rectangle(131, 232, 2, 2);
            pts[170] = new Rectangle(140, 232, 2, 2);
            pts[171] = new Rectangle(148, 232, 2, 2);
            pts[172] = new Rectangle(156, 232, 2, 2);
            pts[173] = new Rectangle(165, 232, 2, 2);
            pts[174] = new Rectangle(173, 232, 2, 2);
            pts[175] = new Rectangle(181, 232, 2, 2);
            pts[176] = new Rectangle(206, 232, 2, 2);
            pts[177] = new Rectangle(214, 232, 2, 2);
            pts[178] = new Rectangle(220, 230, 2, 2);
            pts[179] = new Rectangle(33, 240, 2, 2);
            pts[180] = new Rectangle(57, 241, 2, 2);
            pts[181] = new Rectangle(82, 241, 2, 2);
            pts[182] = new Rectangle(156, 241, 2, 2);
            pts[183] = new Rectangle(181, 241, 2, 2);
            pts[184] = new Rectangle(206, 241, 2, 2);
            pts[185] = new Rectangle(33, 249, 2, 2);
            pts[186] = new Rectangle(57, 249, 2, 2);
            pts[187] = new Rectangle(82, 249, 2, 2);
            pts[188] = new Rectangle(156, 249, 2, 2);
            pts[189] = new Rectangle(181, 249, 2, 2);
            pts[190] = new Rectangle(206, 249, 2, 2);
            pts[191] = new Rectangle(16, 257, 2, 2);
            pts[192] = new Rectangle(24, 257, 2, 2);
            pts[193] = new Rectangle(33, 257, 2, 2);
            pts[194] = new Rectangle(41, 257, 2, 2);
            pts[195] = new Rectangle(49, 257, 2, 2);
            pts[196] = new Rectangle(57, 257, 2, 2);
            pts[197] = new Rectangle(82, 257, 2, 2);
            pts[198] = new Rectangle(90, 257, 2, 2);
            pts[199] = new Rectangle(98, 257, 2, 2);
            pts[200] = new Rectangle(107, 257, 2, 2);
            pts[201] = new Rectangle(131, 257, 2, 2);
            pts[202] = new Rectangle(140, 257, 2, 2);
            pts[203] = new Rectangle(148, 257, 2, 2);
            pts[204] = new Rectangle(156, 257, 2, 2);
            pts[205] = new Rectangle(181, 257, 2, 2);
            pts[206] = new Rectangle(189, 257, 2, 2);
            pts[207] = new Rectangle(198, 257, 2, 2);
            pts[208] = new Rectangle(206, 257, 2, 2);
            pts[209] = new Rectangle(214, 257, 2, 2);
            pts[210] = new Rectangle(222, 257, 2, 2);
            pts[211] = new Rectangle(16, 265, 2, 2);
            pts[212] = new Rectangle(107, 265, 2, 2);
            pts[213] = new Rectangle(131, 265, 2, 2);
            pts[214] = new Rectangle(222, 265, 2, 2);
            pts[215] = new Rectangle(16, 274, 2, 2);
            pts[216] = new Rectangle(107, 274, 2, 2);
            pts[217] = new Rectangle(131, 274, 2, 2);
            pts[218] = new Rectangle(222, 274, 2, 2);
            pts[219] = new Rectangle(16, 282, 2, 2);
            pts[220] = new Rectangle(24, 282, 2, 2);
            pts[221] = new Rectangle(33, 282, 2, 2);
            pts[222] = new Rectangle(41, 282, 2, 2);
            pts[223] = new Rectangle(49, 282, 2, 2);
            pts[224] = new Rectangle(57, 282, 2, 2);
            pts[225] = new Rectangle(66, 282, 2, 2);
            pts[226] = new Rectangle(74, 282, 2, 2);
            pts[227] = new Rectangle(82, 282, 2, 2);
            pts[228] = new Rectangle(90, 282, 2, 2);
            pts[229] = new Rectangle(98, 282, 2, 2);
            pts[230] = new Rectangle(107, 282, 2, 2);
            pts[231] = new Rectangle(115, 282, 2, 2);
            pts[232] = new Rectangle(123, 282, 2, 2);
            pts[233] = new Rectangle(131, 282, 2, 2);
            pts[234] = new Rectangle(140, 282, 2, 2);
            pts[235] = new Rectangle(148, 282, 2, 2);
            pts[236] = new Rectangle(156, 282, 2, 2);
            pts[237] = new Rectangle(165, 282, 2, 2);
            pts[238] = new Rectangle(173, 282, 2, 2);
            pts[239] = new Rectangle(181, 282, 2, 2);
            pts[240] = new Rectangle(189, 282, 2, 2);
            pts[241] = new Rectangle(198, 282, 2, 2);
            pts[242] = new Rectangle(206, 282, 2, 2);
            pts[243] = new Rectangle(214, 282, 2, 2);
            pts[244] = new Rectangle(222, 282, 2, 2);

            possiblePoints = 244;
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
            scoreText = Content.Load<SpriteFont>("font");
            spriteClosed = Content.Load<Texture2D>("pacman2");
            spriteUp = Content.Load<Texture2D>("pacmanUp");
            spriteDown = Content.Load<Texture2D>("pacmanDown");
            spriteLeft = Content.Load<Texture2D>("pacmanLeft");
            spriteRight = Content.Load<Texture2D>("pacmanRight");
            ghost = Content.Load<Texture2D>("ghost");
            ghostEye = Content.Load<Texture2D>("eye");
            ghostPupil = Content.Load<Texture2D>("pupil");
            gameover = Content.Load<Texture2D>("gameover");
            background = Content.Load<Texture2D>("level");
            rect = Content.Load<Texture2D>("rect");
            door = Content.Load<Texture2D>("door");
            smPoint = Content.Load<Texture2D>("point");
            // TODO: use this.Content to load your game content here
           
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

            if (randomSeed > 2147483) // if the Randomseed value is approaching it's maximum, reset it to
                randomSeed = 0;       // prevent crashing! :)

            if (firstCheck == false)
            {
                firstCheck = true;
                firstRun = gameTime.TotalGameTime.TotalSeconds;
            }

            {
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                    this.Exit();
            }

            shutDoor = false;

            if (forced == true)
            {
                shutDoor = true;

                for (int i = 0; i < noGhosts; i++)
                {
                    if (forceGhosts[i] == true)
                        shutDoor = false;
                }

                if (shutDoor == true)
                {
                    // all the ghosts are out of the middle, lets
                    // shut the door now.

                    rects[54] = new Rectangle(110, 144, 21, 4); // shuts the door

                }

            }

            if (!endGame)
            {
                // Otherwise proceed to interpret user input:

                if (forced == false)
                {
                    if (gameTime.TotalGameTime.TotalSeconds > firstRun + 2)
                    {

                        if (storedSecs == 0)
                            storedSecs = gameTime.TotalGameTime.TotalSeconds;

                        if (gameTime.TotalGameTime.TotalSeconds < storedSecs + 2)
                        {
                            if (gameTime.TotalGameTime.Milliseconds % 250 == 0)
                            {
                                if (flashDoor)
                                {
                                    flashDoor = false;
                                }
                                else
                                {
                                    flashDoor = true;
                                }
                            }
                        }
                        else
                        {
                            rects[54] = new Rectangle(1, 1, 1, 1); // the door is open!

                            // need to FORCE ghosts to exit the central section,
                            // otherwise it can take ages for them to leave...
                            for (int i = 0; i < noGhosts; i++)
                            {
                                forceGhosts[i] = true;
                            }
                            forced = true;
                        }
                    }
                }

                AnimateGhosts();
                MoveGhosts();

                int startsecs = 2000;
                int[] moveSecs = new int[noGhosts];

                for (int i = 0; i < noGhosts; i++)
                {
                    moveSecs[i] = startsecs + i * 100;
                }

                for (int i = 0; i < noGhosts; i++)
                {
                    if (gameTime.TotalGameTime.Milliseconds % moveSecs[i] == 0)
                    {
                        gtChange[i] = true;
                    }
                }

                // only check for keys pressed while
                // the game is actually running
                
                if (gameRunning)
                {
                    GamePadState gps = GamePad.GetState(PlayerIndex.One, GamePadDeadZone.Circular);
                    GamePadState state;
                    {
                       
                        if (spriteDirection == 0)
                        {
                            moveY = -1;
                            moveX = 0;
                        
                            if (CheckBounds(spriteX, spriteY, moveX, moveY, spriteUp, true))
                            {
                                spriteY = spriteY + moveY;
                                if (moveX == -1)     spriteX = spriteX + moveX;
                                if (moveX == 1)     spriteX = spriteX + moveX;


                                DirectionBuffer = spriteDirection; // set the buffer to know that Pac-man is moving Up
//startTime = DateTime.Now; Timerrunning = false; 
                                //spriteDirection == 5;
                             }
                            else spriteDirection = DirectionBuffer;
                           // if (CheckBounds(spriteX, spriteY, moveX, moveY, spriteUp, true) == false) Timerrunning = true; interval = DateTime.Now - startTime; if (interval.TotalMilliseconds <= 500) PacmanCornerTurning(); //run the code to make pacman go the previous direction and eventually turn the way the player wanted to go.
}


                        if (spriteDirection == 1)
                        {
                            moveY = 1;
                            moveX = 0;
                            if (CheckBounds(spriteX, spriteY, moveX, moveY, spriteDown, true))
                            {
                                spriteY = spriteY + moveY;
                                if (moveX == -1)
                                    spriteX = spriteX + moveX;
                                if (moveX == 1)
                                    spriteX = spriteX + moveX;
                                DirectionBuffer = spriteDirection; // set the buffer to know that Pac-man is moving Down
                                //spriteDirection == 5;
                                //startTime = DateTime.Now; Timerrunning = false;
                            }
                            else spriteDirection = DirectionBuffer;
                            
                           // else Timerrunning = true; interval = DateTime.Now - startTime; if (interval.TotalMilliseconds <= 500) PacmanCornerTurning(); //run the code to make pacman go the previous direction and eventually turn the way the player wanted to go.
                            
                            }
                            //startTime = DateTime.Now; Timerrunning = false;
                        

                        if (spriteDirection == 2)
                        {
                            moveY = 0;
                            moveX = -1;

                            if (CheckBounds(spriteX, spriteY, moveX, moveY, spriteLeft, true))
                            {
                                Rectangle tempRect = new Rectangle(spriteX, spriteY, spriteLeft.Width, spriteLeft.Height);
                                if (tempRect.Intersects(tunnel1))
                                    spriteX = tunnel2.Left - tempRect.Width - 1;
                                else if (tempRect.Intersects(tunnel2))
                                    spriteX = tunnel1.Left + tunnel1.Width + 1;
                                else
                                    spriteX = spriteX + moveX;
                                if (moveY == -1)
                                    spriteY = spriteY + moveY;
                                if (moveY == 1)
                                    spriteY = spriteY + moveY;
                                DirectionBuffer = spriteDirection; // set the buffer to know that Pac-man is moving Left
                                //spriteDirection == 5;
                            }

                            else spriteDirection = DirectionBuffer;
                            //else Timerrunning = true; interval = DateTime.Now - startTime; if (interval.TotalMilliseconds <= 500) PacmanCornerTurning(); //run the code to make pacman go the previous direction and eventually turn the way the player wanted to go.
                            
                            }
                        //startTime = DateTime.Now; Timerrunning = false;
                        if (spriteDirection == 3)
                        {
                            moveY = 0;
                            moveX = 1;
                            if (CheckBounds(spriteX, spriteY, moveX, moveY, spriteRight, true))
                            {
                                Rectangle tempRect = new Rectangle(spriteX, spriteY, spriteRight.Width, spriteRight.Height);
                                if (tempRect.Intersects(tunnel1))
                                    spriteX = tunnel2.Left - tempRect.Width - 1;
                                else if (tempRect.Intersects(tunnel2))
                                    spriteX = tunnel1.Left + tunnel1.Width + 1;
                                else
                                    spriteX = spriteX + moveX;
                                    if (moveY == -1)
                                        spriteY = spriteY + moveY;
                                    if (moveY == 1)
                                        spriteY = spriteY + moveY;
                                    DirectionBuffer = spriteDirection; // set the buffer to know that Pac-man is moving Right
                                    //spriteDirection == 5;
                            }
                            else spriteDirection = DirectionBuffer; 
                            

                            //else Timerrunning = true; interval = DateTime.Now - startTime; if (interval.TotalMilliseconds <= 500) PacmanCornerTurning(); //run the code to make pacman go the previous direction and eventually turn the way the player wanted to go.
                   //   startTime = DateTime.Now; Timerrunning = false;
                                                //PreviousDirection == spriteDirection;  was an older idea.      
                    }
                            
                    
                       
                            

                        if (gps.DPad.Up == ButtonState.Pressed)
                        {
                            spriteDirection = 0;
                        }
                        if (gps.DPad.Down == ButtonState.Pressed)
                        {
                            spriteDirection = 1;
                        }
                        if (gps.DPad.Left == ButtonState.Pressed)
                        {
                            spriteDirection = 2;
                        }
                        if (gps.DPad.Right == ButtonState.Pressed)
                        {
                            spriteDirection = 3;
                        }
                    }
                }

                if (gameTime.TotalGameTime.Milliseconds % 500 == 0)
                {
                    if (mouthOpen)
                    {
                        mouthOpen = false;
                    }
                    else
                    {
                        mouthOpen = true;
                    }
                }

                if (spriteX < 0)
                    spriteX = 0;
                if (spriteY < 0)
                    spriteY = 0;
                if (spriteX + moveX > graphics.GraphicsDevice.Viewport.Width)
                    spriteX = graphics.GraphicsDevice.Viewport.Width - moveX;
                if (spriteY + moveY > graphics.GraphicsDevice.Viewport.Height)
                    spriteY = graphics.GraphicsDevice.Viewport.Height - moveY;

                // do bounds checking
            }

            base.Update(gameTime);
        }

        void StartGame(int newScore, int newLevel, int newLives, bool killPoints)
        {
            shutDoor = true;
            flashDoor = false;
            gameRunning = false;
            storedSecs = 0;
            Score = newScore;
            Level = newLevel;
            Lives = newLives;
            spriteX = 112;
            spriteY = 226;
            spriteDirection = 3;
            moveY = 0;
            moveX = 0;
            forced = false;
            endGame = false;
            firstCheck = false;
            CreateGhosts(noGhosts);
            CreateOutOfBounds();
            if (killPoints)
                CreatePoints();
            gameRunning = true;
        }

        void MoveGhosts()
        {
            // Now we need to firstly move each ghost so that the X co-ordinate
            // is in line with the door + 1px.

            int doorLeft = 110;
            int doorWidth = 21;
            int doorTop = 142;

            for (int i = 0; i < noGhosts; i++)
            {
                if (forceGhosts[i] == true)
                {
                    if ((ghostX[i] > doorLeft) && (ghostX[i] + ghost.Width < doorLeft + doorWidth))
                    {
                        // the ghost is in line with the door, what next?
                        // = move the ghost out of the cage.
                        ghostmoveX[i] = 0;
                        ghostmoveY[i] = -1;

                        // now we need to re-enable collision, so the ghost collides
                        // with the wall outside the cage, causing the ghost to move randomly
                        // around the world.
                    }
                    else if (ghostX[i] > doorLeft)
                    {
                        ghostmoveX[i] = -1;
                        ghostmoveY[i] = 0;
                    }
                    else if (ghostX[i] < doorLeft)
                    {
                        ghostmoveX[i] = 1;
                        ghostmoveY[i] = 0;
                    }

                    ghostX[i] = ghostX[i] + ghostmoveX[i];
                    ghostY[i] = ghostY[i] + ghostmoveY[i];

                    if (ghostY[i] + ghost.Height < doorTop)
                    {
                        ghostLastDirection[i] = 0;
                        forceGhosts[i] = false;
                    }
                }
            }

        }
//        void PacmanCornerTurning()
//        {
//        if (DirectionBuffer == "U") moveY = -1; moveX = 0; if (CheckBounds(spriteX, spriteY, moveX, moveY, spriteUp, true)) spriteY = spriteY + moveY; if (moveX == -1)     spriteX = spriteX + moveX; if (moveX == 1)     spriteX = spriteX + moveX;        
//if (DirectionBuffer == "D") moveY = 1; moveX = 0; if (CheckBounds(spriteX, spriteY, moveX, moveY, spriteDown, true)) spriteY = spriteY + moveY; if (moveX == -1) spriteX = spriteX + moveX; if (moveX == 1) spriteX = spriteX + moveX;
//if (DirectionBuffer == "L") moveY = 0; moveX = -1; if (CheckBounds(spriteX, spriteY, moveX, moveY, spriteLeft, true))  
//{
//    Rectangle tempRect = new Rectangle(spriteX, spriteY, spriteLeft.Width, spriteLeft.Height); if (tempRect.Intersects(tunnel1))  spriteX = tunnel2.Left - tempRect.Width - 1; else if (tempRect.Intersects(tunnel2))  spriteX = tunnel1.Left + tunnel1.Width + 1; else spriteX = spriteX + moveX; if (moveY == -1) spriteY = spriteY + moveY; if (moveY == 1) spriteY = spriteY + moveY;
//}
//if (DirectionBuffer == "R") moveY = 0; moveX = 1; if (CheckBounds(spriteX, spriteY, moveX, moveY, spriteRight, true))
//{
//    Rectangle tempRect = new Rectangle(spriteX, spriteY, spriteRight.Width, spriteRight.Height); if (tempRect.Intersects(tunnel1)) spriteX = tunnel2.Left - tempRect.Width - 1; else if (tempRect.Intersects(tunnel2)) spriteX = tunnel1.Left + tunnel1.Width + 1; else spriteX = spriteX + moveX; if (moveY == -1) spriteY = spriteY + moveY; if (moveY == 1) spriteY = spriteY + moveY;
//}                                  
//}

        void AnimateGhosts()
        {
            // need to select random direction,
            // move in that direction until collision
            // then select a new random direction that isn't the last direction
            // then move in that direction until collision

            for (int i = 0; i < noGhosts; i++)
            {
                if (forceGhosts[i] == false)
                {
                    if (changeDirection[i])
                    {
                        changeDirection[i] = false; // makes sure we can still move

                        randomSeed++;

                        Random rnd = new Random(randomSeed);
                        // 0 up 1 down 2 left 3 right

                        int randomDirection = rnd.Next(0, 4);

                        if ((ghostLastDirection[i] == 0) || (ghostLastDirection[i] == 1))
                            randomDirection = rnd.Next(2, 4);
                        else
                            randomDirection = rnd.Next(0, 2);

                        if (randomDirection == 0)
                        {
                            ghostmoveY[i] = -1;
                            ghostmoveX[i] = 0;
                        }
                        if (randomDirection == 1)
                        {
                            ghostmoveY[i] = 1;
                            ghostmoveX[i] = 0;
                        }
                        if (randomDirection == 2)
                        {
                            ghostmoveY[i] = 0;
                            ghostmoveX[i] = -1;
                        }
                        if (randomDirection == 3)
                        {
                            ghostmoveY[i] = 0;
                            ghostmoveX[i] = 1;
                        }

                        ghostLastDirection[i] = randomDirection;
                    }

                    if (CheckBounds(ghostX[i], ghostY[i], ghostmoveX[i], ghostmoveY[i], ghost, false))
                    {
                        Rectangle tempRect = new Rectangle(ghostX[i], ghostY[i], ghost.Width, ghost.Height);
                        if (tempRect.Intersects(tunnel1))
                            ghostX[i] = tunnel2.Left - tempRect.Width - 1;
                        else if (tempRect.Intersects(tunnel2))
                            ghostX[i] = tunnel1.Left + tunnel1.Width + 1;
                        else
                            ghostX[i] = ghostX[i] + ghostmoveX[i];

                        ghostY[i] = ghostY[i] + ghostmoveY[i];
                    }
                    else // The ghost just collided with something - mark it for a direction change next time!
                    {
                        changeDirection[i] = true;
                    }

                    if (gtChange[i])
                    {
                        gtChange[i] = false;
                        randomSeed++;
                        Random rndShallWe = new Random(randomSeed);
                        if (rndShallWe.Next(0, 100) > 50)
                        {
                            changeDirection[i] = true;
                        }
                    }
                }
            }
        }

        bool CheckBounds(int CurrentX, int CurrentY, int AddX, int AddY, Texture2D character, bool isSprite)
        {
            /* need to check here to see if our character rectangle falls within
             * any of our array-ed rectangles! if it does, return false so the
             * character is unable to move.
             */

            // Also, if the character isn't sprite, then check if we're colliding with the sprite.

            Rectangle tempRect = new Rectangle(CurrentX + AddX, CurrentY + AddY, character.Width, character.Height);

            bool tempReturn = true;

            if (isSprite)
            {
                for (int i = 0; i < noPts; i++)
                {
                    if (tempRect.Intersects(pts[i]))
                    {
                        if (gameRunning)
                        {
                            pts[i] = new Rectangle(1, 1, 1, 1);
                            Score += (Level * 10);
                            possiblePoints--;
                            if (possiblePoints == 0)  // Level complete, we need to advance!
                            {
                                noGhosts++;
                                Level++;
                                possiblePoints = 244;

                                StartGame(Score, Level, Lives, true); // Advance to next level.
                                break;
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < noRects; i++)
            {
                if (tempRect.Intersects(rects[i]))
                    tempReturn = false;
            }

            if (!isSprite)
            {
                if (tempRect.Intersects(new Rectangle(spriteX, spriteY, spriteUp.Width, spriteUp.Height)))
                {
                    tempReturn = false;
                    if (!dying)
                    {
                        Lives--;
                        dying = true;
                        StartGame(Score, Level, Lives, false);
                        dying = false;
                        if (Lives == 0)
                            endGame = true;
                    }
                }

            }

            return tempReturn;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            spriteBatch.Begin(SpriteBlendMode.AlphaBlend);
            spriteBatch.Draw(background, new Rectangle(0, 0, background.Width, background.Height), Color.White);

            if (flashDoor || forced)
            {
                if (!shutDoor)
                    spriteBatch.Draw(door, closedoor, Color.White);
            }

            for (int i = 0; i < noPts; i++)
            {
                Rectangle tester = new Rectangle(1, 1, 1, 1); // this checks to see if the rectangle should be drawn.
                if (pts[i] != tester)
                    spriteBatch.Draw(smPoint, pts[i], Color.White); // this is the points.
            }

            Rectangle spriteRectangle = new Rectangle(spriteX, spriteY, spriteClosed.Width, spriteClosed.Height);

            if (mouthOpen)
            {
                if (spriteDirection == 0)
                    spriteBatch.Draw(spriteUp, spriteRectangle, Color.White);
                else if (spriteDirection == 1)
                    spriteBatch.Draw(spriteDown, spriteRectangle, Color.White);
                else if (spriteDirection == 2)
                    spriteBatch.Draw(spriteLeft, spriteRectangle, Color.White);
                else if (spriteDirection == 3)
                    spriteBatch.Draw(spriteRight, spriteRectangle, Color.White);
            }
            else
                spriteBatch.Draw(spriteClosed, spriteRectangle, Color.White);

            // need to draw ghosts in different colours
            //
            // the maximum number of ghosts is 5, so we need
            // 5 different colours
            // RGB
            // 87, 171, 255 : light blue
            // 255, 171, 255: light red
            // 255, 131, 3  : light orange
            // 255, 255, 255: light white
            // 25, 255, 0   : light green

            Color[] ghostColours = new Color[5];
            ghostColours[0] = new Color(87, 171, 255);
            ghostColours[1] = new Color(255, 171, 255);
            ghostColours[2] = new Color(255, 131, 3);
            ghostColours[3] = new Color(255, 255, 255);
            ghostColours[4] = new Color(25, 255, 0);

            int tempCounter = -1;

            for (int i = 0; i < noGhosts; i++)
            {
                tempCounter++;

                spriteBatch.Draw(ghost, new Rectangle(ghostX[i], ghostY[i], ghost.Width, ghost.Height), ghostColours[tempCounter]);
                spriteBatch.Draw(ghostEye, new Rectangle(ghostX[i] + 3, ghostY[i] + 3, ghostEye.Width, ghostEye.Height), Color.White);
                spriteBatch.Draw(ghostEye, new Rectangle(ghostX[i] + 9, ghostY[i] + 3, ghostEye.Width, ghostEye.Height), Color.White);

                if ((ghostLastDirection[i] == 0) || (ghostLastDirection[i] == -1)) // up (or starting)
                {
                    spriteBatch.Draw(ghostPupil, new Rectangle(ghostX[i] + 4, ghostY[i] + 3, ghostPupil.Width, ghostPupil.Height), Color.White);
                    spriteBatch.Draw(ghostPupil, new Rectangle(ghostX[i] + 10, ghostY[i] + 3, ghostPupil.Width, ghostPupil.Height), Color.White);
                }

                if (ghostLastDirection[i] == 1) // down
                {
                    spriteBatch.Draw(ghostPupil, new Rectangle(ghostX[i] + 4, ghostY[i] + 6, ghostPupil.Width, ghostPupil.Height), Color.White);
                    spriteBatch.Draw(ghostPupil, new Rectangle(ghostX[i] + 10, ghostY[i] + 6, ghostPupil.Width, ghostPupil.Height), Color.White);
                }

                if (ghostLastDirection[i] == 2) // left
                {
                    spriteBatch.Draw(ghostPupil, new Rectangle(ghostX[i] + 3, ghostY[i] + 4, ghostPupil.Width, ghostPupil.Height), Color.White);
                    spriteBatch.Draw(ghostPupil, new Rectangle(ghostX[i] + 9, ghostY[i] + 4, ghostPupil.Width, ghostPupil.Height), Color.White);
                }

                if (ghostLastDirection[i] == 3) // right
                {
                    spriteBatch.Draw(ghostPupil, new Rectangle(ghostX[i] + 6, ghostY[i] + 4, ghostPupil.Width, ghostPupil.Height), Color.White);
                    spriteBatch.Draw(ghostPupil, new Rectangle(ghostX[i] + 11, ghostY[i] + 4, ghostPupil.Width, ghostPupil.Height), Color.White);
                }

                if (tempCounter == 4)
                    tempCounter = -1;
            }

            //for (int i = 0; i < noRects; i++)
            //{
               // spriteBatch.Draw(rect, rects[i], Color.White); // this line shows the walls!
           // }

            if (endGame)
            {
                spriteBatch.Draw(gameover, new Rectangle(0, 0, gameover.Width, gameover.Height), Color.White);
                noGhosts = totalGhosts;
                GamePadState gps = GamePad.GetState(PlayerIndex.One, GamePadDeadZone.Circular);
                if (gps.Buttons.A == ButtonState.Pressed)
                    StartGame(0, 1, 3, true);
            }

            //spriteBatch.Draw(rect, tunnel1, Color.White);
            //spriteBatch.Draw(rect, tunnel2, Color.White);

            string oneup = "1UP";
            spriteBatch.DrawString(scoreText, oneup, new Vector2(36.0f, 10.0f), Color.White);
            string scoreString = Score.ToString("00000");
            spriteBatch.DrawString(scoreText, scoreString, new Vector2(49.0f, 18.0f), Color.White);

            for (int i = 0; i < (Lives - 1); i++)
            {
                float tempLeft = (float)spriteRight.Width * (float)i * 1.3f;
                spriteBatch.Draw(spriteRight, new Rectangle(28 + (int)tempLeft, 300, spriteRight.Width, spriteRight.Height), Color.White);
            }

#if DEBUG
            spriteBatch.DrawString(scoreText, debug, new Vector2(1.0f, 1.0f), Color.White);
#endif

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}