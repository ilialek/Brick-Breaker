using System;
using System.Drawing;
using GXPEngine;

public class Ball : EasyDraw
{

	public static bool drawDebugLine = false;

	public Vec2 velocity;
	public Vec2 position;

	public readonly int radius;
	public readonly bool moving;

    Vec2 _oldPosition;
	Arrow _velocityIndicator;

	public Ball (int pRadius, Vec2 pPosition, Vec2 pVelocity=new Vec2()) : base (pRadius*2 + 1, pRadius*2 + 1)
	{
		radius = pRadius;
		position = pPosition;
		velocity = pVelocity;

        UpdateScreenPosition ();
		SetOrigin (radius, radius);

		Draw (230, 200, 0);

		_velocityIndicator = new Arrow(position, new Vec2(0,0), 10);
		AddChild(_velocityIndicator);
	}

	void Draw(byte red, byte green, byte blue) {
		Fill (red, green, blue);
		Stroke (red, green, blue);
		Ellipse (radius, radius, 2*radius, 2*radius);
	}

	void UpdateScreenPosition() {
		x = position.x;
		y = position.y;
	}

    float BallDistance(Vec2 _position, LineSegment _line) {

        Vec2 difference = _position - _line.start;
        Vec2 lineSegmentVector = _line.end - _line.start;
        Vec2 unitSegmentVector = lineSegmentVector.Normalized();
        float vectorPLength = difference.Dot(unitSegmentVector);
        Vec2 projection = unitSegmentVector * vectorPLength;

        return (difference - projection).Length();
    }


    public void Step()
    {
        _oldPosition = position;
        position += velocity;

        MyGame myGame = (MyGame)game;

        //check collision with lines
        for (int i = 0; i < myGame.GetNumberOfLines(); i++)
        {
            LineSegment line = myGame.GetLine(i);

            //compare distance with ball radius
            if (BallDistance(position, line) < radius)
            {

                Vec2 oldPosition = position - velocity;

                float oldDistance = BallDistance(oldPosition, line);
                float newDistance = BallDistance(position, line);

                float a = (oldDistance - radius);
                float b = oldDistance - newDistance;
                float t = a / b;

                Vec2 POI = oldPosition + t * velocity;
                position = new Vec2(POI.x, POI.y);

                Vec2 lineSegmentVector = line.end - line.start;
                Vec2 velocityOut = velocity - 2 * velocity.Dot(lineSegmentVector.Normal()) * lineSegmentVector.Normal();
                velocity = velocityOut;

            }

        }


        CollisionInfo firstCollision = FindEarliestCollision();

        if (firstCollision != null)
        {
            ResolveCollision(firstCollision);
        }

        UpdateScreenPosition();

        ShowDebugInfo();
    }


    


    CollisionInfo FindEarliestCollision()
    {
        MyGame myGame = (MyGame)game;

        CollisionInfo minCollisionInfo = null;

        for (int i = 0; i < myGame.GetNumberOfObstalces(); i++)
        {
            Obstacle obstacle = myGame.GetObstacle(i);

            //check if obstacle is a circle
            if (obstacle.obstacleIndex == 2)
            {

                Vec2 relativePosition = position - obstacle.position;

                float a = velocity.Length() * velocity.Length();
                float b = 2 * relativePosition.Dot(velocity);
                float c = relativePosition.Length() * relativePosition.Length() - (radius + 28) * (radius + 28);
                float D = b * b - 4 * a * c;

                float TOI1 = (-b - Mathf.Sqrt(D)) / (2 * a);

                if (TOI1 >= 0 && TOI1 < 1)
                {
                    myGame.RemoveObstacle(obstacle);
                    obstacle.Destroy();

                    minCollisionInfo = new CollisionInfo(obstacle, TOI1);
                }

            }

            //check if obstacle is a square
            if (obstacle.obstacleIndex == 1)
            {

                float testX = position.x;
                float testY = position.y;

                if (position.x < obstacle.x - 28)
                {
                    testX = obstacle.x - 28;
                }
                else if (position.x > obstacle.x + 28)
                {
                    testX = obstacle.x + 28;
                }

                if (position.y < obstacle.y - 28)
                {
                    testY = obstacle.y - 28;
                }
                else if (position.y > obstacle.y + 28)
                {
                    testY = obstacle.y + 28;
                }

                Vec2 relativePosition = new Vec2(position.x - testX, position.y - testY);


                if (relativePosition.Length() < radius)
                {


                    //left hit
                    if (position.y > obstacle.position.y - 28 && position.y < obstacle.position.y + 28 && position.x > obstacle.position.x)
                    {
                        Vec2 oldPosition = position - velocity;

                        float oldDistance = oldPosition.x - (obstacle.position.x + 28);
                        float newDistance = position.x - (obstacle.position.x + 28);

                        float a = (oldDistance - radius);
                        float b = oldDistance - newDistance;
                        float t = a / b;

                        Vec2 POI = oldPosition + t * velocity;
                        position = POI;

                        myGame.RemoveObstacle(obstacle);
                        obstacle.Destroy();

                        minCollisionInfo = new CollisionInfo(obstacle, t, new Vec2(0, obstacle.height).Normal(), oldPosition);

                    }

                    //right hit
                    if (position.y > obstacle.position.y - 28 && position.y < obstacle.position.y + 28 && position.x < obstacle.position.x)
                    {
                        Vec2 oldPosition = position - velocity;

                        float oldDistance = (obstacle.position.x - 28) - oldPosition.x;
                        float newDistance = (obstacle.position.x + 28) - position.x;

                        float a = (oldDistance - radius);
                        float b = oldDistance - newDistance;
                        float t = a / b;

                        Vec2 POI = oldPosition + t * velocity;
                        position = POI;

                        myGame.RemoveObstacle(obstacle);
                        obstacle.Destroy();

                        minCollisionInfo = new CollisionInfo(obstacle, t, new Vec2(0, obstacle.height).Normal(), oldPosition);

                    }

                    //top hit
                    if (position.x > obstacle.position.x - 28 && position.x < obstacle.position.x + 28 && position.y < obstacle.position.y)
                    {

                        Vec2 oldPosition = position - velocity;

                        float oldDistance = (obstacle.position.y - 28) - oldPosition.y;
                        float newDistance = (obstacle.position.y + 28) - position.y;

                        float a = (oldDistance - radius);
                        float b = oldDistance - newDistance;
                        float t = a / b;

                        Vec2 POI = oldPosition + t * velocity;
                        position = POI;

                        myGame.RemoveObstacle(obstacle);
                        obstacle.Destroy();

                        minCollisionInfo = new CollisionInfo(obstacle, t, new Vec2(obstacle.width, 0).Normal(), oldPosition);

                    }

                    //bottom hit
                    if (position.x > obstacle.position.x - 28 && position.x < obstacle.position.x + 28 && position.y > obstacle.position.y)
                    {
                        Vec2 oldPosition = position - velocity;

                        float oldDistance = oldPosition.y - (obstacle.position.y - 28);
                        float newDistance = position.y - (obstacle.position.y + 28);

                        float a = (oldDistance - radius);
                        float b = oldDistance - newDistance;
                        float t = a / b;

                        Vec2 POI = oldPosition + t * velocity;
                        position = POI;

                        myGame.RemoveObstacle(obstacle);
                        obstacle.Destroy();

                        minCollisionInfo = new CollisionInfo(obstacle, t, new Vec2(obstacle.width, 0).Normal(), oldPosition);

                    }


                }

            }


        }

        return minCollisionInfo;

    }


    void ResolveCollision(CollisionInfo col) {

        if (col.other is Obstacle && ((Obstacle)col.other).obstacleIndex == 2)
        {
            Vec2 POI = position + col.timeOfImpact * velocity;
            position = POI;
            Vec2 velocityOut = velocity - 2 * velocity.Dot((position - ((Obstacle)col.other).position).Normalized()) * (position - ((Obstacle)col.other).position).Normalized();
            velocity = velocityOut;
        }

        if (col.other is Obstacle && ((Obstacle)col.other).obstacleIndex == 1)
        {
            Vec2 POI = col.oldPosition + col.timeOfImpact * velocity;
            position = POI;
            Vec2 velocityOut = velocity - 2 * velocity.Dot(col.normal) * col.normal;
            velocity = velocityOut;
        }
 
    }


    void ShowDebugInfo() {
		if (drawDebugLine) {
			((MyGame)game).DrawLine (_oldPosition, position);
		}
		_velocityIndicator.startPoint = position;
		_velocityIndicator.vector = velocity;
	}
}

