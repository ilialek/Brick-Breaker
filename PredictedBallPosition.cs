using System;
namespace GXPEngine
{
    public class PredictedBallPosition : EasyDraw
    {
        public Vec2 positionToPrint;
        public int radius;

        public PredictedBallPosition(int pRadius, Vec2 pPosition) : base(pRadius * 2 + 1, pRadius * 2 + 1)
        {
          
            radius = pRadius;
            //position = pPosition;

            UpdateScreenPosition(pPosition);
            SetOrigin(radius, radius);
            Draw(230, 200, 0);
        }

        void Draw(byte red, byte green, byte blue)
        {
            Fill(red, green, blue, 160);
            Stroke(red, green, blue, 160);
            Ellipse(radius, radius, 2 * radius, 2 * radius);
        }

        public void UpdateScreenPosition(Vec2 position)
        {
            x = position.x;
            y = position.y;

            positionToPrint = new Vec2(x, y);
        }
    }
}
