using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using Raylib_cs;

class Paddle
{
    public float x, y;
    public float paddleWidth = 7;
    public float paddleHeight = 80;
    public float speed = 7f;

    int screenHeight;
    bool isAI;
    Ball? trackedBall;
    public Paddle(float x, int screenHeight, Ball? ball = null, bool isAI = false)
    {
        this.x = x;
        this.screenHeight = screenHeight;
        this.isAI = isAI;
        this.trackedBall = ball;
        y = screenHeight / 2f - paddleHeight / 2f;
    }

    public void Update(bool moveUp, bool moveDown)
    {
        if (isAI && trackedBall != null)
        {
                float targetY = trackedBall.y + trackedBall.size / 2 - paddleHeight / 2;
                y += (targetY - y) * 0.1f; // 0.1 = smooth factor
        }
        else
        {
            if (moveUp) y -= speed;
            if (moveDown) y += speed;

            // if (moveUp && y > 0) y -= speed;
            // if (moveDown && y + paddleHeight < screenHeight) y += speed;
        }

        if (y < 0) y = 0;
        if (y + paddleHeight > screenHeight) y = screenHeight - paddleHeight;

    }

    public void Draw()
    {
        Raylib_cs.Raylib.DrawRectangle((int)x, (int)y, (int)paddleWidth, (int)paddleHeight, Color.White);
    }
}