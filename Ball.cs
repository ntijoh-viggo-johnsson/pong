using System.IO.Compression;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic;
using Raylib_cs;

class Ball
{
    public float x, y;
    public float dx = 2f, dy = 2f;
    public float size = 15;

    public float speed = 2f;

    int screenWidth, screenHeight;

    public Ball(int screenWidth, int screenHeight)
    {
        this.screenWidth = screenWidth;
        this.screenHeight = screenHeight;
        Reset();
    }

    public void Reset()
    {
        x = screenWidth / 2 - size / 2;
        y = screenHeight / 2 - size / 2;
        speed = 2f;
        Stop();
    }

    public void Stop()
    {
        dx = 0;
        dy = 0;
    }

    public void Start()
    {
        Random rnd = new Random();
        float dirX = (rnd.Next(0, 2) == 0 ? 1 : -1);
        float dirY = (rnd.Next(0, 2) == 0 ? 1 : -1);

        dx = dirX * speed;
        dy = dirY * speed;
    }

    public void Draw()
    {
        Raylib_cs.Raylib.DrawRectangle((int)x, (int)y, (int)size, (int)size, Color.White);
    }

    public void Update()
    {
        x += dx;
        y += dy;

        if (y <= 0 || y + size >= screenHeight)
        {
            dy *= -1;
        }
    }

    private static Random rnd = new Random();

    public void CheckCollision(Paddle paddle)
    {
        bool colliding = x < paddle.x + paddle.paddleWidth &&
                         x + size > paddle.x &&
                         y < paddle.y + paddle.paddleHeight &&
                         y + size > paddle.y;

        if (colliding)
        {

            dx = -dx;
            speed += 0.5f;



            float nudge = ((float)rnd.NextDouble() - 0.5f) * 0.5f;
            dy += nudge;

            // float magnitude = MathF.Sqrt(dx * dx + dy * dy);
            // dx = dx / magnitude * speed;
            // dy = dy / magnitude * speed;
        }
    }
}
