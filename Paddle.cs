using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using Raylib_cs;

class Paddle
{
    public float x, y;
    public float paddleWidth = 15;
    public float paddleHeight = 80;
    public float speed = 4f;

    int screenHeight;

    public Paddle(float x, int screenHeight)
    {
        this.x = x;
        this.screenHeight = screenHeight;
        y = screenHeight / 2f - paddleHeight / 2f;
    }

    public void Update(bool moveUp, bool moveDown)
    {
        if (moveUp && y > 0) y -= speed;
        if (moveDown && y + paddleHeight < screenHeight) y += speed;
    }

    public void Draw()
    {
        Raylib_cs.Raylib.DrawRectangle((int)x, (int)y, (int)paddleWidth, (int)paddleHeight, Color.White);
    }
}