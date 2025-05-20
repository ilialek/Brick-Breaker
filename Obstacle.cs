using System;
namespace GXPEngine
{

    public class Obstacle : EasyDraw
    {

        public int obstacleIndex;
        public Vec2 position;

        public Obstacle(int xPos, int yPos, int whichObstacle) : base(56, 56)
        {

            SetOrigin(width / 2, height / 2);

            if (whichObstacle == 1)
            {
                DrawRectangle(((byte)Utils.Random(20, 70)), (byte)Utils.Random(100, 700), (byte)Utils.Random(100, 500));
            }
            else
            {
                DrawCircle(((byte)Utils.Random(20, 70)), (byte)Utils.Random(100, 700), (byte)Utils.Random(100, 500));
            }

            obstacleIndex = whichObstacle;

            position = new Vec2(xPos, yPos);

            x = xPos;
            y = yPos;
        }

        void DrawRectangle(byte red, byte green, byte blue)
        {
            Fill(red, green, blue);
            Stroke(red, green, blue);
            Rect(28, 28, 56, 56);
        }

        void DrawCircle(byte red, byte green, byte blue)
        {
            Fill(red, green, blue);
            Stroke(red, green, blue);
            Ellipse(28, 28, 56, 56);
        }
    }
}

