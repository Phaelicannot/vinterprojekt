using Raylib_cs;
using System.Numerics;

int windowHeight = 900;
int windowWidth = 900;
int posX = 450;
int posY = 450;
int textureSizeX = 88;
int textureSizeY = 96;

int keyTextureSizeX = 64;
int keyTextureSizeY = 64;

int direction = 1;

int walkTimer = 6;
int walkingIndex = 0;
int keyIndex = 0;

bool bg = false;
bool key1taken = false;
bool key2taken = false;
bool key3taken = false;
bool openDoor = false;

int backgroundIndex = 0;

float x = posX;
float y = posY;


Raylib.InitWindow(windowWidth, windowHeight, "Labyrinth");
Raylib.SetTargetFPS(30);


Color dangerColor = new Color(247, 102, 30, 255);

Rectangle character = new Rectangle(posX - (textureSizeX / 2), posY - (textureSizeY / 2), textureSizeX, textureSizeY);

// Rectangle key1 = new Rectangle(390 - (keyTextureSizeX / 2), windowHeight - 150 - (keyTextureSizeY / 2), keyTextureSizeX, keyTextureSizeY);
// Rectangle key2 = new Rectangle(1 - (keyTextureSizeX / 2), 1 - (keyTextureSizeY / 2), keyTextureSizeX, keyTextureSizeY);
// Rectangle key3 = new Rectangle(1 - (keyTextureSizeX / 2), 1 - (keyTextureSizeY / 2), keyTextureSizeX, keyTextureSizeY);


Color charactercolor = new Color(255, 255, 255, 255);

List<Rectangle> walls = new List<Rectangle>();
walls.Add(new Rectangle());

List<Rectangle> dangers = new List<Rectangle>();
dangers.Add(new Rectangle());

List<Rectangle> door = new List<Rectangle>();
door.Add(new Rectangle());

List<Rectangle> keys = new List<Rectangle>();
keys.Add(new Rectangle());

Texture2D key = Raylib.LoadTexture("key.png");


List<Texture2D> walk = new List<Texture2D>();
walk.Add(Raylib.LoadTexture("rat.png"));
walk.Add(Raylib.LoadTexture("Ratwalk1.png"));
walk.Add(Raylib.LoadTexture("ratwalk2.png"));
walk.Add(Raylib.LoadTexture("Ratwalk3.png"));
walk.Add(Raylib.LoadTexture("ratwalk4.png"));

List<Texture2D> background = new List<Texture2D>();
background.Add(Raylib.LoadTexture("bg_vn.png"));
background.Add(Raylib.LoadTexture("bg_vm.png"));
background.Add(Raylib.LoadTexture("bg_vo.png"));
background.Add(Raylib.LoadTexture("bg_mo.png"));
background.Add(Raylib.LoadTexture("bg_ho.png"));
background.Add(Raylib.LoadTexture("bg_hm.png"));
background.Add(Raylib.LoadTexture("bg_hn.png"));
background.Add(Raylib.LoadTexture("bg_mn.png"));
background.Add(Raylib.LoadTexture("bg_mm.png"));
background.Add(Raylib.LoadTexture("bg_end.png"));

// Rectangle Source = new Rectangle(0,0, background[0].width, background[0].height);
// Rectangle Destination = new Rectangle(0,0, windowWidth, windowHeight);



// Raylib.DrawTexturePro(background[backgroundIndex], Source, Destination, new Vector2(0,0), 0, Color.WHITE);

string currentScene = "start";

while (Raylib.WindowShouldClose() == false)
{
    float speed = 8f;
    bool walking = false;

    keys.Add(new Rectangle());

    Raylib.BeginDrawing();

    Raylib.ClearBackground(Color.SKYBLUE); 


    if (currentScene == "start")
    {
        dangers.Clear();

        door.Clear();

        bg = false;
        Raylib.DrawText("Welcome to the", 180, 350, 60, Color.BLACK);
        Raylib.DrawText("Mouse Labyrinth", 180, 450, 60, Color.BLACK);
        Raylib.DrawText("Press 'Space' to continue", 180, 550, 30, Color.BLACK);
        if (Raylib.IsKeyDown(KeyboardKey.KEY_SPACE))
        {
            currentScene = "instructions";
        }

        character.x = 450;
        character.y = 450;
    }

    if (currentScene == "instructions")
    {
        dangers.Clear();

        door.Clear();

        bg = false;
        Raylib.DrawText("You are a mouse looking for it's cheese.", 180, 350, 30, Color.BLACK);
        Raylib.DrawText("To get to the cheese you need 3 keys.", 180, 400, 30, Color.BLACK);
        Raylib.DrawText("Avoid the lava and survive, good luck!", 180, 450, 30, Color.BLACK);
        Raylib.DrawText("Press 'Enter' to start", 180, 500, 30, Color.BLACK);

        if (Raylib.IsKeyDown(KeyboardKey.KEY_ENTER))
        {
            currentScene = "vn";
            bg = true;
        }
    }

    if (bg == true)
    {
        drawRoom(background[backgroundIndex], 0,0);
        foreach(Rectangle danger in dangers) {
            Raylib.DrawRectangle((int)danger.x, (int)danger.y, (int)danger.width, (int)danger.height, dangerColor);
        }
        foreach(Rectangle wall in walls) {
            Raylib.DrawRectangle((int)wall.x, (int)wall.y, (int)wall.width, (int)wall.height, Color.DARKGRAY);
        }
        foreach(Rectangle dörr in door) {
            Raylib.DrawRectangle((int)dörr.x, (int)dörr.y, (int)dörr.width, (int)dörr.height, Color.BROWN);
        }
    }

    if (currentScene == "vn")
    {
        backgroundIndex = 0;

        dangers.Clear();

        door.Clear();

        walls.Clear();
        walls.Add(new Rectangle(0, 0, 32, windowHeight)); //vänstra
        walls.Add(new Rectangle(0, 0, 360, 32)); //uppe vänstra
        walls.Add(new Rectangle(windowWidth - 360, 0, 360, 32)); //uppe högra
        walls.Add(new Rectangle(windowWidth - 32, 0, 32, 360)); //höger övra
        walls.Add(new Rectangle(windowWidth - 32, windowHeight - 360, 32, 360)); //höger nedre
        walls.Add(new Rectangle(0, windowHeight - 32, windowWidth, 32)); //nere
        
    
        if (character.x > windowWidth  && character.y > 360 && character.y < 540)
        {
            currentScene = "mn";
            character.x = 0;
        }
        if (character.x > 360 && character.x < 540 && character.y < 0)
        {
            currentScene = "vm";
            character.y = windowHeight - textureSizeY;
        }


    }

    if (currentScene == "vm")
    {
        backgroundIndex = 1;
        
        dangers.Clear();
        dangers.Add(new Rectangle(0, 300, 680, 300));

        door.Clear();

        walls.Clear();
        walls.Add(new Rectangle(0, 0, 32, windowHeight)); //vänster
        walls.Add(new Rectangle(0, 0, 360, 32)); //uppe vänstra
        walls.Add(new Rectangle(windowWidth - 360, 0, 360, 32)); //uppe högra
        walls.Add(new Rectangle(windowWidth - 32, 0, 32, 360)); //höger övre
        walls.Add(new Rectangle(windowWidth - 32, windowHeight - 360, 32, 360)); //höger nedre
        walls.Add(new Rectangle(0, windowHeight - 32, 360, 32)); //nere vänstra
        walls.Add(new Rectangle(windowWidth - 360, windowHeight - 32, 360, 32)); //nere högra

        

        if (character.x > 360 && character.x < 540 && character.y > windowHeight)
        {
            currentScene = "vn";
            character.y = 0;
        } //går neråt

        if (character.x > 360 && character.x < 540 && character.y < 0)
        {
            currentScene = "vo";
            character.y = windowHeight;
        } //går uppåt     

        if (character.x > windowWidth && character.y > 360 && character.y < 540)
        {
            currentScene = "mm";
            character.x = 0;
        } //går till höger   
    }

    if (currentScene == "vo")
    {
        backgroundIndex = 2;

        dangers.Clear();
        dangers.Add(new Rectangle(180, 360, 540, 64));
        dangers.Add(new Rectangle(720 - 64, 360, 64, 540));

        door.Clear();

        walls.Clear();
        walls.Add(new Rectangle(0, 0, 32, windowHeight)); //vänstra
        walls.Add(new Rectangle(0, windowHeight -32, 360, 32)); //nere vänstra
        walls.Add(new Rectangle(windowWidth - 360, windowHeight - 32, 360, 32)); //nere högra
        walls.Add(new Rectangle(windowWidth - 32, 0, 32, 360)); //höger övra
        walls.Add(new Rectangle(windowWidth - 32, windowHeight - 360, 32, 360)); //höger nedre
        walls.Add(new Rectangle(0, 0, windowWidth, 32)); //över

        if (character.x > windowWidth && character.y > 360 && character.y < 540)
        {
            currentScene = "mo";
            character.x = 0;
        } //går till höger   

        if (character.x > 360 && character.x < 540 && character.y > windowHeight)
        {
            currentScene = "vm";
            character.y = 0;
        } //går neråt
    }

    if (currentScene == "mo")
    {
        backgroundIndex = 3;

        dangers.Clear();
        dangers.Add(new Rectangle(450 - 32, 0, 64, 540));

        door.Clear();

        walls.Clear();
        walls.Add(new Rectangle(0, 0, 32, 360)); //vänster övra
        walls.Add(new Rectangle(0, windowHeight - 360, 32, 360)); //vänster nedre
        walls.Add(new Rectangle(0, windowHeight -32, 360, 32)); //nere vänstra
        walls.Add(new Rectangle(windowWidth - 360, windowHeight - 32, 360, 32)); //nere högra
        walls.Add(new Rectangle(windowWidth - 32, 0, 32, 360)); //höger övra
        walls.Add(new Rectangle(windowWidth - 32, windowHeight - 360, 32, 360)); //höger nedre
        walls.Add(new Rectangle(0, 0, windowWidth, 32)); //över
        walls.Add(new Rectangle(360 - 32, windowHeight - 180, 32, 180)); //låda vänster
        walls.Add(new Rectangle(360, windowHeight - 180, 180, 32)); //låda topp
        walls.Add(new Rectangle(540 - 32, windowHeight - 180, 32, 180)); //låda höger

        if(key2taken == false)
        {
           keys.Add(new Rectangle(410 - keyTextureSizeX / 2, windowHeight - 150 - keyTextureSizeY / 2, 64, 64));
           drawKey2 (key, 410 - keyTextureSizeX / 2, windowHeight - 150 - keyTextureSizeY / 2); 
        }
      //  Rectangle key1 = new Rectangle(390 - (keyTextureSizeX / 2), windowHeight - 150 - (keyTextureSizeY / 2), keyTextureSizeX, keyTextureSizeY);

        // if (key2taken == false)
        // {
        //     keys.Clear();
        //     keys.Add(new Rectangle(450, windowHeight - 90, keyTextureSizeX, keyTextureSizeY));
        // }

        // if (key2taken == true)
        // {
        //     keys.Clear();
        // }

        if (character.x > windowWidth && character.y > 360 && character.y < 540)
        {
            currentScene = "ho";
            character.x = 0;
        } //går till höger   

        if (character.x > 360 && character.x < 540 && character.y > windowHeight)
        {
            currentScene = "mm";
            character.y = 0;
        } //går neråt

        if (character.x < 0 && character.y > 360 && character.y < 540)
        {
            currentScene = "vo";
            character.x = windowWidth;
        } //går till vänster
    }

    if (currentScene == "ho")
    {
        backgroundIndex = 4;

        dangers.Clear();
        dangers.Add(new Rectangle(180 - 32, 0, 64, 540));
        dangers.Add(new Rectangle(720 - 32, 0, 64, 540));
        dangers.Add(new Rectangle(450 - 32, windowHeight - 540, 64, 540));

        door.Clear();

        walls.Clear();
        walls.Add(new Rectangle(0, 0, 32, 360)); //vänster övra
        walls.Add(new Rectangle(0, windowHeight - 360, 32, 360)); //vänster nedre
        walls.Add(new Rectangle(0, windowHeight -32, 180, 32)); //nere vänstra
        walls.Add(new Rectangle(360, windowHeight -32, 180, 32)); //nere mitten
        walls.Add(new Rectangle(720, windowHeight -32, 180, 32)); //nere högra
        walls.Add(new Rectangle(windowWidth - 32, 0, 32, windowHeight)); //höger
        walls.Add(new Rectangle(0, 0, windowWidth, 32)); //över

        if (character.x > 180 && character.x < 360 && character.y > windowHeight)
        {
            currentScene = "hm";
            character.y = 0;
        } //går neråt

        if (character.x > 540 && character.x < 720 && character.y > windowHeight)
        {
            currentScene = "hm";
            character.y = 0;
        } //går neråt

        if (character.x < 0 && character.y > 360 && character.y < 540)
        {
            currentScene = "mo";
            character.x = windowWidth;
        } //går till vänster
    }

    if (currentScene == "hm")
    {
        backgroundIndex = 5;

        dangers.Clear();
        dangers.Add(new Rectangle(540 - 32, 180, 64, 360));
        dangers.Add(new Rectangle(540 - 32, windowHeight - 360, 64, 180));
        dangers.Add(new Rectangle(360 - 32, 32, 64, windowHeight - 180));

        door.Clear();
        if (openDoor == false)
        {
            door.Add(new Rectangle(windowWidth - 32, 360, 32, 180));
        }

        walls.Clear();
        walls.Add(new Rectangle(0, 0, 32, windowHeight)); //vänster
        walls.Add(new Rectangle(0, 0, 180, 32)); //uppe vänstra
        walls.Add(new Rectangle(360, 0, 180, 32)); //uppe mitten
        walls.Add(new Rectangle(720, 0, 180, 32)); //uppe högra
        walls.Add(new Rectangle(windowWidth - 32, 0, 32, 360)); //höger övra
        walls.Add(new Rectangle(windowWidth - 32, windowHeight - 360, 32, 360)); //höger nedre
        walls.Add(new Rectangle(0, windowHeight -32, 360, 32)); //nere vänstra
        walls.Add(new Rectangle(windowWidth - 360, windowHeight - 32, 360, 32)); //nere högra
        walls.Add(new Rectangle(180 - 32, 0, 32, 180)); //låda vänster
        walls.Add(new Rectangle(180, 180 - 32, 180, 32)); //låda topp
        walls.Add(new Rectangle(360 - 32, 0, 32, 180)); //låda höger

         if(key1taken == false)
        {
           keys.Clear();
           keys.Add(new Rectangle(400 - keyTextureSizeX / 2, windowHeight - 160 - keyTextureSizeY / 2, 64, 64));
           drawKey1 (key, 400 - keyTextureSizeX / 2, windowHeight - 160 - keyTextureSizeY / 2); 
        }

        if (character.x > 180 && character.x < 360 && character.y < 0)
        {
            currentScene = "ho";
            character.y = windowHeight;
        } //går uppåt

        if (character.x > 540 && character.x < 720 && character.y < 0)
        {
            currentScene = "ho";
            character.y = windowHeight;
        } //går uppåt

         if (character.x > 360 && character.x < 540 && character.y > windowHeight)
        {
            currentScene = "hn";
            character.y = 0;
        } //går neråt

        if (character.x > windowWidth && character.y > 360 && character.y < 540)
        {
            if (openDoor == true)
            {
                currentScene = "end";
                character.x = 0;
                Raylib.DrawText("Congrats, you won and got your cheese! Press Esc to exit.", 3500, 400, 20, Color.WHITE);
                Raylib.DrawText("you won and got your cheese!", 375, 400, 20, Color.WHITE);
                Raylib.DrawText("Press Esc to exit.", 400, 400, 20, Color.WHITE);
            }
            
        } //går till höger   


    }

    if (currentScene == "hn")
    {
        backgroundIndex = 6;

        dangers.Clear();
        dangers.Add(new Rectangle(windowWidth - 360, 180, 180, 64));

        door.Clear();

        //  if (key1taken == false)
        // {
        //     keys.Clear();
        //     keys.Add(new Rectangle(450, 90, keyTextureSizeX, keyTextureSizeY));
        //     drawKey1(key, (int)keys.x, (int)keys.y);
        // }

        // if (key1taken == true)
        // {
        //     keys.Clear();
        // }

        walls.Clear();
        walls.Add(new Rectangle(windowWidth - 32, 0, 32, windowHeight)); //höger
        walls.Add(new Rectangle(0, 0, 360, 32)); //uppe vänstra
        walls.Add(new Rectangle(windowWidth - 360, 0, 360, 32)); //uppe högra
        walls.Add(new Rectangle(0, 0, 32, 360)); //vänster övra
        walls.Add(new Rectangle(0, windowHeight - 360, 32, 360)); //vänster nedre
        walls.Add(new Rectangle(0, windowHeight - 32, windowWidth, 32)); //nere
        walls.Add(new Rectangle(360 - 32, 0, 32, 180)); //låda vänster
        walls.Add(new Rectangle(360, 180 - 32, 180, 32)); //låda topp
        walls.Add(new Rectangle(540 - 32, 0, 32, 180)); //låda höger

        if(key3taken == false)
        {
           keys.Add(new Rectangle(560 - keyTextureSizeX / 2, 60 - keyTextureSizeY / 2, 64, 64));
           drawKey3 (key, 400 - keyTextureSizeX / 2, windowHeight - 160 - keyTextureSizeY / 2); 
        }

        if (character.x < 0 && character.y > 360 && character.y < 540)
        {
            currentScene = "mn";
            character.x = windowWidth;
        } //går till vänster

        if (character.x > 360 && character.x < 540 && character.y < 0)
        {
            currentScene = "hm";
            character.y = windowHeight;
        } //går uppåt     
    }

    if (currentScene == "mn")
    {
        backgroundIndex = 7;

        dangers.Clear();
        dangers.Add(new Rectangle(180 - 32, 0, 64, 720));
        dangers.Add(new Rectangle(720 - 32, windowHeight - 720, 64, 720));

        walls.Clear();
        walls.Add(new Rectangle(0, 0, 32, 360)); //vänster övre
        walls.Add(new Rectangle(0, windowHeight - 360, 32, 360)); //vänster nedre
        walls.Add(new Rectangle(0, 0, windowWidth, 32)); //uppe
        walls.Add(new Rectangle(windowWidth - 32, 0, 32, 360)); //höger övre
        walls.Add(new Rectangle(windowWidth - 32, windowHeight - 360, 32, 360)); //höger nedre
        walls.Add(new Rectangle(0, windowHeight - 32, windowWidth, 32)); //nere

        if (character.x > windowWidth && character.y > 360 && character.y < 540)
        {
            currentScene = "hn";
            character.x = 0;
        } //går till höger
        if (character.x < 0 && character.y > 360 && character.y < 540)
        {
            currentScene = "vn";
            character.x = windowWidth;
        } //går till vänster
        
    }

    if (currentScene == "mm")
    {
        backgroundIndex = 8;

        dangers.Clear();
        dangers.Add(new Rectangle(180 - 32, 0, 64, 720));
        dangers.Add(new Rectangle(720 - 32, windowHeight - 720, 64, 540));
        dangers.Add(new Rectangle(180 + 32, 180, 540, 64));

        walls.Clear();
        walls.Add(new Rectangle(windowWidth - 32, 0, 32, windowHeight)); //höger
        walls.Add(new Rectangle(0, 0, 360, 32)); //uppe vänstra
        walls.Add(new Rectangle(windowWidth - 360, 0, 360, 32)); //uppe högra
        walls.Add(new Rectangle(0, 0, 32, 360)); //vänster övra
        walls.Add(new Rectangle(0, windowHeight - 360, 32, 360)); //vänster nedre
        walls.Add(new Rectangle(0, windowHeight - 32, windowWidth, 32)); //nere

        if (character.x < 0 && character.y > 360 && character.y < 540)
        {
            currentScene = "vm";
            character.x = windowWidth;
        } //går till vänster

        if (character.x > 360 && character.x < 540 && character.y < 0)
        {
            currentScene = "mo";
            character.y = windowHeight;
        } //går uppåt     
    }
     
    if (currentScene == "end")
    {

        backgroundIndex = 9;

        dangers.Clear();

        door.Clear();
        door.Add(new Rectangle(windowWidth - 32, 360, 32, 180));

        walls.Clear();
        walls.Add(new Rectangle(windowWidth - 32, 0, 32, windowHeight)); //höger
        walls.Add(new Rectangle(0, 0, windowWidth, 32)); //uppe
        walls.Add(new Rectangle(0, 0, 32, 360)); //vänster övra
        walls.Add(new Rectangle(0, windowHeight - 360, 32, 360)); //vänster nedre
        walls.Add(new Rectangle(0, windowHeight - 32, windowWidth, 32)); //nere

        if (character.x < 0 && character.y > 360 && character.y < 540)
        {
            currentScene = "hm";
            character.x = windowWidth;
        } //går till vänster
    }

    if (currentScene != "mo" || currentScene != "hm" || currentScene != "hn")
    {
        keys.Clear();
    }

    if (currentScene == "fail")
    {
        dangers.Clear();

        door.Clear();
        
        bg = false;
        Raylib.DrawText("You Died", 180, 400, 60, Color.RED);
        Raylib.DrawText("Press 'Enter' to restart", 180, 450, 30, Color.RED);

        if(Raylib.IsKeyDown(KeyboardKey.KEY_ENTER))
        {
            currentScene = "start";
        }
    }

    Rectangle previousPosition = character;

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

    if(AreOverlapping())
    {
        character = previousPosition;
    }

    if(AreOverlappingLava()) 
    {
        currentScene = "fail";
    }

    if(AreOverlappingKey1())
    {
        key1taken = true;
    }
    
    if(AreOverlappingKey2())
    {
        key2taken = true;
    }
    
    if(AreOverlappingKey3())
    {
        key3taken = true;
    }
    
    Raylib.DrawText("keys:" + keyIndex, 20, 20, 20, Color.WHITE);

    if (key1taken == true)
    {
        keys.Clear();
        keyIndex++;
    }
    
    if (key2taken == true)
    {
        keys.Clear();
        keyIndex++;
    }
    if (key3taken == true)
    {
        keys.Clear();
        keyIndex++;
    }
    

    if (keyIndex > 2)
    {
        openDoor = true;
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

    if (bg == true)
    {
        drawCharacter(walk[walkingIndex], (int)character.x + (textureSizeX / 2), (int)character.y + (textureSizeY / 2), direction); 
    }
    if (bg == false)
    {
        walking = false;
    }

    if (Raylib.IsKeyDown(KeyboardKey.KEY_ESCAPE))
    {
        Raylib.WindowShouldClose();
    }

    Raylib.EndDrawing();
}


void drawCharacter(Texture2D ratStep, int x, int y, int direction)
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

        

void drawRoom(Texture2D bg, int x, int y)
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

bool AreOverlapping()
{
    foreach(Rectangle wall in walls)
    {
        if(Raylib.CheckCollisionRecs(character, wall))
        {
            return(true);
        }
    }
    return(false);
}

bool AreOverlappingLava()
{
    foreach(Rectangle danger in dangers)
    {
        if(Raylib.CheckCollisionRecs(character, danger))
        {
            return(true);
        }
    }
    return(false);
}

void drawKey1(Texture2D keyTex, int x, int y)
{
                Raylib.DrawTexture(
                    keyTex,
                    x,
                    y,
                    Color.WHITE
                );  
}

void drawKey2(Texture2D keyTex, int x, int y)
        {
                Raylib.DrawTexturePro(
                    keyTex,
                    new Rectangle(0, 0, keyTextureSizeX, keyTextureSizeY),
                    new Rectangle(x, y, keyTextureSizeX, keyTextureSizeY),
                    new Vector2(keyTextureSizeX / 2, keyTextureSizeY / 2),
                    0,
                    Color.WHITE
                );  
        }

void drawKey3(Texture2D keyTex, int x, int y)
        {
                Raylib.DrawTexturePro(
                    keyTex,
                    new Rectangle(0, 0, keyTextureSizeX, keyTextureSizeY),
                    new Rectangle(x, y, keyTextureSizeX, keyTextureSizeY),
                    new Vector2(keyTextureSizeX / 2, keyTextureSizeY / 2),
                    0,
                    Color.WHITE
                );  
        }

bool AreOverlappingKey1()
{
    if(Raylib.CheckCollisionRecs(character, keys[0]))
    {
        return(true);
    }
    return(false);
}

bool AreOverlappingKey2()
{
            if(Raylib.CheckCollisionRecs(character, keys[0]))
            {
                return(true);
            }
            return(false);
}

bool AreOverlappingKey3()
{
        if(Raylib.CheckCollisionRecs(character, keys[0]))
        {
            return(true);
        }
        return(false);
}