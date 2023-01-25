using Raylib_cs;
using System.Numerics;

int windowHeight = 1024;
int windowWidth = 1024;
int posX = 256;
int posY = 256;
int textureSizeX = 128;
int textureSizeY = 128;

int direction = 1;

int walkTimer = 6;
int walkingIndex = 0;


int backgroundIndex = 0;

float x = posX;
float y = posY;

Raylib.InitWindow(windowWidth, windowHeight, "Labyrinth");
Raylib.SetTargetFPS(30);




Rectangle character = new Rectangle(posX, posY, textureSizeX, textureSizeY);

Color charactercolor = new Color(255, 255, 255, 255);

List<Texture2D> walk = new List<Texture2D>();
walk.Add(Raylib.LoadTexture("rat.png"));
walk.Add(Raylib.LoadTexture("Ratwalk1.png"));
walk.Add(Raylib.LoadTexture("ratwalk2.png"));
walk.Add(Raylib.LoadTexture("Ratwalk3.png"));
walk.Add(Raylib.LoadTexture("ratwalk4.png"));

List<Texture2D> background = new List<Texture2D>();
background.Add(Raylib.LoadTexture("bg_vn"));
background.Add(Raylib.LoadTexture("bg_vm"));
background.Add(Raylib.LoadTexture("bg_vö"));
background.Add(Raylib.LoadTexture("bg_mö"));
background.Add(Raylib.LoadTexture("bg_hö"));
background.Add(Raylib.LoadTexture("bg_hm"));
background.Add(Raylib.LoadTexture("bg_hn"));
background.Add(Raylib.LoadTexture("bg_mn"));
background.Add(Raylib.LoadTexture("bg_mm"));
background.Add(Raylib.LoadTexture("bg_end"));

// Rectangle Source = new Rectangle(0,0, background[0].width, background[0].height);
// Rectangle Destination = new Rectangle(0,0, windowWidth, windowHeight);



// Raylib.DrawTexturePro(background[backgroundIndex], Source, Destination, new Vector2(0,0), 0, Color.WHITE);
string currentScene = "vn";

while (Raylib.WindowShouldClose() == false)
{
    float speed = 5f;
    bool walking = false;

    Raylib.BeginDrawing();

    Raylib.ClearBackground(Color.SKYBLUE); 

    Background(background[backgroundIndex], 0, 0);

    if (currentScene == "vn")
    {
        backgroundIndex = 0;
        if (character.x > 1028 - textureSizeX / 2 && character.y > 500 && character.y < 524)
        {
            currentScene = "mm";
        }
    }

    if (currentScene == "vm")
    {
        backgroundIndex = 1;
    }

    if (currentScene == "vö")
    {
        backgroundIndex = 2;
    }

    if (currentScene == "mö")
    {
        backgroundIndex = 3;
    }

    if (currentScene == "hö")
    {
        backgroundIndex = 4;
    }

    if (currentScene == "hm")
    {
        backgroundIndex = 5;
    }

    if (currentScene == "hn")
    {
        backgroundIndex = 6;
    }

    if (currentScene == "mn")
    {
        backgroundIndex = 7;
    }

    if (currentScene == "mm")
    {
        backgroundIndex = 8;
    }
     
    if (currentScene == "end")
    {
        backgroundIndex = 9;
    }

    
    
    if (Raylib.IsKeyDown(KeyboardKey.KEY_W))
    {
        walking = true;
        character.y += -speed;
    }
    if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
    {
        walking = true;
        character.x += -speed;
        direction = -1;
        
    }
    if (Raylib.IsKeyDown(KeyboardKey.KEY_S))
    {
        walking = true;
        character.y += speed;
    }
    if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
    {
        walking = true;
        character.x += speed;
        direction = 1;
    }

    
    if (walking == true)
    {
        if (walkTimer > 6)
                { 
                    walkingIndex ++;
                    walkTimer = 0;
                }
                walkTimer ++;
                if (walkingIndex > 4)
                {
                    walkingIndex = 1;
                }
    }
    else if (walking == false)
    {
        walkingIndex = 0;
        walkTimer = 6;
    }

    Character(walk[walkingIndex], (int)character.x, (int)character.y, direction);

    

    Raylib.EndDrawing();
}


void Character (Texture2D ratStep, int x, int y, int direction)
        {
                Raylib.DrawTexturePro(
                    ratStep,
                    new Rectangle(0, 0, textureSizeX * direction, textureSizeY),
                    new Rectangle(x, y, textureSizeX, textureSizeY),
                    new Vector2(textureSizeX / 2, textureSizeY / 2),
                    0,
                    charactercolor
                );  
        }

void Background (Texture2D bg, int x, int y)
        {
            Raylib.DrawTexturePro(
                bg,
                new Rectangle(0, 0,background[0].width, background[0].height),
                new Rectangle(0,0, windowWidth, windowHeight),
                new Vector2(0,0), 
                0, 
                Color.WHITE
            );
        }
