using Raylib_cs;
using System;
using System.Globalization;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;

namespace HelloWorld;

class Program
{
    static int  sizeX = 20,
                sizeY = 100,
                p1 = 0, p2 = 0;
    public static void Main()
    {
        const int screenWidth = 800; //x
        const int screenHeight = 480; //y

        Raylib.InitWindow(screenWidth, screenHeight, "Pong game");
        Rectangle Rect = new Rectangle(0, 0, sizeX, sizeY);
        Rectangle Rect2 = new Rectangle(0, 0, sizeX, sizeY);


        Vector2 Position = new Vector2(15, (screenHeight/2) - (sizeY/2) + 22);
        Vector2 Position2 = new Vector2(screenWidth - 15 - sizeX, (screenHeight/2) - (sizeY/2) + 22);
        
        Vector2 ballPosition = new(screenWidth/2, (screenHeight/2)+22);
        Vector2 ballSpeed = new(2.0f, 2.0f);
        //Vector2 ballSpeed = new(10.5f, 10.5f);
        int ballRadius = 10;

        Raylib.SetTargetFPS(60);

        //Main loop Game
        while (!Raylib.WindowShouldClose())
        {
            float deltaTime = 1.0f/Raylib.GetFrameTime(); //Frame rate

            // Draw
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Black);

            // Enventos do teclado P1
            if (Raylib.IsKeyDown(KeyboardKey.Down) && Position.Y < screenHeight - sizeY - 10){
                Position.Y += 100 * Raylib.GetFrameTime();
            } else if(Raylib.IsKeyDown(KeyboardKey.Up) && Position.Y > 50){
                Position.Y -= 100 * Raylib.GetFrameTime();
            }

            // Eventos do teclado P2
            if (Raylib.IsKeyDown(KeyboardKey.S) && Position2.Y < screenHeight - sizeY - 10){
                Position2.Y += 100 * Raylib.GetFrameTime();
            } else if(Raylib.IsKeyDown(KeyboardKey.W) && Position2.Y > 50){
                Position2.Y -= 100 * Raylib.GetFrameTime();
            }

            // ball
            ballPosition.X += ballSpeed.X;
            ballPosition.Y += ballSpeed.Y;

            // Check walls collision for bouncing
            if ((ballPosition.X >= (Raylib.GetScreenWidth() - ballRadius)))
            {
                ballPosition = new(screenWidth / 2, (screenHeight / 2) + 22);
                ballSpeed.X *= -1.0f;
                p1++;
            }
            if((ballPosition.X <= ballRadius))
            {
                ballPosition = new(screenWidth / 2, (screenHeight / 2) + 22);
                ballSpeed.X *= -1.0f;
                p2++;
            }
            if ((ballPosition.Y >= (Raylib.GetScreenHeight() - ballRadius)) || (ballPosition.Y <= ballRadius + 45))
            {
                ballSpeed.Y *= -1.0f;
            }

            // Check player 1 collision
            if ( (ballPosition.X - ballRadius <= Position.X + sizeX) && (ballPosition.Y >= Position.Y) && (ballPosition.Y <= Position.Y + sizeY)) //             <= Rect2.x + Rect2.width && ballPosition.Y + ballRadius >= Rect2.y && ballPosition.Y - ballRadius <= Rect2.y + Rect2.height){
                ballSpeed.X *= -1.0f;//Console.WriteLine("Colidiu");
            // Check player 2 collision
            if ((ballPosition.X + ballRadius >= screenWidth - 15 - sizeX) && (ballPosition.Y >= Position2.Y) && (ballPosition.Y <= Position2.Y + sizeY)) //             <= Rect2.x + Rect2.width && ballPosition.Y + ballRadius >= Rect2.y && ballPosition.Y - ballRadius <= Rect2.y + Rect2.height){
                ballSpeed.X *= -1.0f;// Console.WriteLine("Colidiu");


            // Draw game map
            Raylib.DrawLine(screenWidth/2, 50, screenWidth/2, screenHeight-10, Color.Gray);     // Draw vertical line
            Raylib.DrawRectangleLines(5, 42, screenWidth - 10, screenHeight-45, Color.White);   // Draw limit game
            Raylib.DrawText("Pong", (screenWidth/2)-25, 12, 20, Color.White);                   // Draw name
            Raylib.DrawText("FPS " + Math.Round(deltaTime).ToString(), screenWidth-80, 12, 20, Color.White);    //Draw FPS
            Raylib.DrawText(p1.ToString(), (screenWidth/2) - 50, 50, 30, Color.White);  // p1 points
            Raylib.DrawText(p2.ToString(), (screenWidth/2) + 35, 50, 30, Color.White);  // p2 points
            
            // P1 e p2
            Rect = new(15, Position.Y, sizeX, sizeY);
            Rect2 = new(screenWidth-15-sizeX, Position2.Y, sizeX, sizeY);
            
            //Draw p1 e p2
            Raylib.DrawRectangleRec(Rect, Color.White);
            Raylib.DrawRectangleRec(Rect2, Color.White);

            // Draw ball
            Raylib.DrawCircleV(ballPosition, ballRadius, Color.White);
            Raylib.CheckCollisionCircleRec(ballPosition, ballRadius, Rect);

            //Raylib.CheckCollisionRecs(Rect, Rect2);

            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
}