using System;
namespace GXPEngine
{
    public class Aim : LineSegment
    {
        PredictedBallPosition predictedBall;
        LineSegment intersectedLine = null;
        MyGame myGame;
        Arrow _velocityIndicator;

        public Aim(Vec2 pStart, Vec2 pEnd) : base(pStart, pEnd, 0xffffffff, 2)
        {
            myGame = (MyGame)game;

            _velocityIndicator = new Arrow(start, new Vec2(0, 0), 1);
            _velocityIndicator.lineWidth = 2;
            AddChild(_velocityIndicator);

            predictedBall = new PredictedBallPosition(13, pStart);
            AddChild(predictedBall);
        }


        void Update() {

            CheckForIntersections();

            if (intersectedLine != null) {

                float alpha = GetAngleBetween(intersectedLine);
                float a = predictedBall.radius / Mathf.Sin(alpha);

                float length = (GetIntersectionPoint(intersectedLine) - start).Length();
                float finalLength = length - a;

                Vec2 direction = (end - start).Normalized();
                end = start + direction * finalLength;
                predictedBall.UpdateScreenPosition(end);

                Vec2 lineSegmentVector = intersectedLine.end - intersectedLine.start;
                Vec2 incident = end - start;

                Vec2 reflectedVector = incident - 2 * incident.Dot(lineSegmentVector.Normal()) * lineSegmentVector.Normal();

                _velocityIndicator.startPoint = end;
                _velocityIndicator.vector = reflectedVector.Normalized() * 200;
            }

        }

        float GetAngleBetween(LineSegment lineSegment) {

            Vec2 vector1 = end - start;
            Vec2 vector2 = lineSegment.end - lineSegment.start;

            float dotProduct = vector1.Dot(vector2);
            float crossProduct = vector1.CrossProduct(vector2);

            return Mathf.Atan2(crossProduct, dotProduct);
        }

        void CheckForIntersections() {

            float distanceToCheck = float.MaxValue;

            for (int i = 0; i < myGame._lines.Count; i++)
            {
                if (LineSegmentsIntersect(myGame._lines[i]))
                {
                    intersectedLine = myGame._lines[i];

                    //if (Distance(start, myGame._lines[i]) < distanceToCheck)
                    //{
                    //    distanceToCheck = Distance(start, myGame._lines[i]);
                    //    intersectedLine = myGame._lines[i];
                    //}
                }

            }
        }

        public bool LineSegmentsIntersect(LineSegment pannel)
        {
            Vec2 A = start;
            Vec2 B = end;
            Vec2 C = pannel.start;
            Vec2 D = pannel.end;

            Vec2 AB = B - A;
            Vec2 AC = C - A;
            Vec2 AD = D - A;
            Vec2 CD = D - C;
            Vec2 CA = A - C;
            Vec2 CB = B - C;

            if ((AB.CrossProduct(AC) * AB.CrossProduct(AD) < 0) && (CD.CrossProduct(CA) * CD.CrossProduct(CB) < 0))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        float Distance(Vec2 _position, LineSegment _line)
        {

            Vec2 difference = _position - _line.start;
            Vec2 lineSegmentVector = _line.end - _line.start;
            Vec2 unitSegmentVector = lineSegmentVector.Normalized();
            float vectorPLength = difference.Dot(unitSegmentVector);
            Vec2 projection = unitSegmentVector * vectorPLength;

            return (difference - projection).Length();
        }

        public Vec2 GetIntersectionPoint(LineSegment pannel)
        {
            Vec2 A = start;
            Vec2 B = end;
            Vec2 C = pannel.start;
            Vec2 D = pannel.end;

            Vec2 AB = B - A;
            Vec2 AC = C - A;
            Vec2 CD = D - C;

            float cross = AB.CrossProduct(CD);

            float t1 = -CD.CrossProduct(AC) / cross;

            Vec2 intersection = A + t1 * AB;
            return intersection;

        }

    }
}
