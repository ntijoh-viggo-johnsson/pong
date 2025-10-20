using System.IO.Compression;
using Microsoft.VisualBasic;
using Raylib_cs;

class Ball
{
    public float x, y;
    public float dx = 2, dy = 2;
    public float size = 15;

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
        if (x <= 0 || x + size >= screenWidth)
        {
            dx *= -1;
        }
    }

    public void CheckCollision(Paddle paddle)
    {
        bool colliding = x < paddle.x + paddle.paddleWidth &&
                         x + size > paddle.x &&
                         y < paddle.y + paddle.paddleHeight &&
                         y + size > paddle.y;
    
    if (colliding)
    {
        dx *= -1;
    }
    }

    
}