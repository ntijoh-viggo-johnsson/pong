using System.Security.Cryptography.X509Certificates;
using Raylib_cs;


class Program
{
    static void Main()
    {
        bool paused = false;

        int score1 = 0;
        int score2 = 0;

        int screenWidth = 640;
        int screenHeight = 480;

        Raylib.InitWindow(screenWidth, screenHeight, "Pong");
        Raylib.SetTargetFPS(120);

        Ball ball = new Ball(screenWidth, screenHeight);
        ball.Start();

        Paddle leftPaddle = new Paddle(50f, screenHeight);
        Paddle rightPaddle = new Paddle(575f, screenHeight);

        GameState state = GameState.Menu;


        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Black);

            if (state == GameState.Menu)
            {
                Raylib.DrawText("PONG", 220, 100, 80, Color.White);
                Raylib.DrawText("Press ENTER to Start", 100, 250, 40, Color.Gray);

                if (Raylib.IsKeyPressed(KeyboardKey.Enter))
                {
                    state = GameState.Playing;
                    ball.Start();
                }
            }
            else if (state == GameState.Playing)
            {

                if (ball.x < 0)
                {
                    score2++;
                    ball.Reset();
                    paused = true;
                }
                else if (ball.x + ball.size > screenWidth)
                {
                    score1++;
                    ball.Reset();
                    paused = true;
                }
                else if (paused == true)
                {
                    Raylib.DrawText("Press SPACE to start", 100, 250, 40, Color.Gray);
                    if (Raylib.IsKeyPressed(KeyboardKey.Space))
                    {
                        state = GameState.Playing;
                        ball.Start();
                    }
                }
            

                if (paused && Raylib.IsKeyPressed(KeyboardKey.Space))
                {
                    paused = false;
                    ball.Start();
                }

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


                leftPaddle.Draw();
                rightPaddle.Draw();
                ball.Draw();

                Raylib.DrawText($"{score1}", screenWidth / 4, 20, 60, Color.White);
                Raylib.DrawText($"{score2}", screenWidth * 3 / 4, 20, 60, Color.White);

                for (float y = 0; y < screenHeight; y += 50)
                {
                    Raylib.DrawRectangle(screenWidth / 2 - 2, (int)y, 7, 25, Color.Gray);
                }

            } 
                Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
}