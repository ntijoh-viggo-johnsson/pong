using System.Security.Cryptography.X509Certificates;
using Raylib_cs;


class Program
{
    static void Main()
    {
        int screenWidth = 640;
        int screenHeight = 480;

        Raylib.InitWindow(screenWidth, screenHeight, "Pong");
        Raylib.SetTargetFPS(120);

        Ball ball = new Ball(screenWidth, screenHeight);
        Paddle leftPaddle = new Paddle(50f, screenHeight);
        Paddle rightPaddle = new Paddle(575f, screenHeight);

        while (!Raylib.WindowShouldClose())
        {

            leftPaddle.Update(
                Raylib.IsKeyDown(KeyboardKey.W),
                Raylib.IsKeyDown(KeyboardKey.S)
            );

            rightPaddle.Update(
                Raylib.IsKeyDown(KeyboardKey.Up),
                Raylib.IsKeyDown(KeyboardKey.Down)
            );

            ball.Update();

            ball.CheckCollision(leftPaddle);
            ball.CheckCollision(rightPaddle);

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Black);

            leftPaddle.Draw();
            rightPaddle.Draw();
            ball.Draw();

            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
}