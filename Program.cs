using System.Security.Cryptography.X509Certificates;
using Raylib_cs;


class Program
{
    static void Main()
    {
        int screenWidth = 800;
        int screenHeight = 600;
        Raylib.InitWindow(screenWidth, screenHeight, "Pong");
        Raylib.InitAudioDevice();
        Raylib.SetTargetFPS(120);

        Sound hitPaddle = Raylib.LoadSound("assets/sounds_ping_pong_8bit/ping_pong_8bit_plop.ogg");
        Sound hitWall = Raylib.LoadSound("assets/sounds_ping_pong_8bit/ping_pong_8bit_plop.ogg");
        
        int score1 = 0;
        int score2 = 0;
        bool paused = false;
        bool onePlayer = false;



        Ball ball = new Ball(screenWidth, screenHeight, hitPaddle, hitWall);

        Paddle leftPaddle = new Paddle(70f, screenHeight);
        Paddle rightPaddle = new Paddle(723f, screenHeight);

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
                    state = GameState.SelectMode;
                }
            }
                else if (state == GameState.SelectMode)
            {
                Raylib.DrawText("Select Mode", 180, 100, 60, Color.White);
                Raylib.DrawText("1 PLAYER (Press 1)", 150, 200, 40, Color.Gray);
                Raylib.DrawText("2 PLAYER (Press 2)", 150, 300, 40, Color.Gray);

                if (Raylib.IsKeyPressed(KeyboardKey.One))
                {
                    onePlayer = true;
                    rightPaddle = new Paddle(723f, screenHeight, ball, true);
                    state = GameState.Playing;
                    ball.Start();
                } else if (Raylib.IsKeyPressed(KeyboardKey.Two))
                {
                    onePlayer = false;
                    rightPaddle = new Paddle(723f, screenHeight);
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

                if (onePlayer)
                {
                    rightPaddle.Update(false, false);
                }
                else
                {
                    rightPaddle.Update(
                    Raylib.IsKeyDown(KeyboardKey.Up),
                    Raylib.IsKeyDown(KeyboardKey.Down)
                );
                }
                    ball.Update();

                ball.CheckCollision(leftPaddle);
                ball.CheckCollision(rightPaddle);


                leftPaddle.Draw();
                rightPaddle.Draw();
                ball.Draw();

                Raylib.DrawText($"{score1}", screenWidth / 4, 20, 80, Color.White);
                Raylib.DrawText($"{score2}", screenWidth * 3 / 4, 20, 80, Color.White);

                for (float y = 0; y < screenHeight; y += 50)
                {
                    Raylib.DrawRectangle(screenWidth / 2 - 2, (int)y, 7, 25, Color.Gray);
                }

            } 
                Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
        Raylib.UnloadSound(hitPaddle);
        Raylib.UnloadSound(hitWall);
        Raylib.CloseAudioDevice();
    }
}