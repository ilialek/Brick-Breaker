using System;
using GXPEngine;
using System.Drawing;
using System.Collections.Generic;

public class MyGame : Game
{
	Aim aim;

    bool _dragging = false;
	int _startSceneNumber = 0;

	int[] layout1 = new int[63] {
		1, 0, 0, 1, 1, 0, 1, 1, 1,
        0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0,
        1, 0, 1, 0, 1, 0, 2, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 1, 1, 0, 0, 1, 1, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 2, 0,
    };

    int[] layout2 = new int[63] {
        0, 1, 0, 0, 1, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 2, 0,
        0, 0, 0, 0, 1, 1, 0, 0, 0,
        1, 0, 1, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 1, 0, 2, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 1, 0, 0, 1,
    };

    int[] layout3 = new int[63] {
        0, 0, 1, 1, 1, 1, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0,
        1, 1, 0, 2, 0, 1, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 1, 0, 0, 1, 0, 2,
        0, 2, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 1, 1, 1, 0, 0,
    };

    Canvas _lineContainer = null;

	Ball ball;

	List<Obstacle> _obstalces;
	public List<LineSegment> _lines;


    public MyGame() : base(600, 750, false, false)
    {
        _lineContainer = new Canvas(width, height);
        AddChild(_lineContainer);

        targetFps = 60;

        _lines = new List<LineSegment>();
        _obstalces = new List<Obstacle>();

        LoadScene(_startSceneNumber);

        PrintInfo();
    }

    public int GetNumberOfLines() {
		return _lines.Count;
	}

    public int GetNumberOfObstalces()
    {
        return _obstalces.Count;
    }

    public LineSegment GetLine(int index) {
		if (index >= 0 && index < _lines.Count) {
			return _lines[index];
		}
		return null;
	}

    public Obstacle GetObstacle(int index)
    {
        if (index >= 0 && index < _obstalces.Count)
        {
            return _obstalces[index];
        }
        return null;
    }

	public void RemoveObstacle(Obstacle _obstacle)
	{
		_obstalces.Remove(_obstacle);
	}

    public void DrawLine(Vec2 start, Vec2 end) {
		_lineContainer.graphics.DrawLine(Pens.White, start.x, start.y, end.x, end.y);
	}

    void AddLine(Vec2 start, Vec2 end) {
		LineSegment line = new LineSegment(start, end, 0xff00ff00, 4);
		AddChild(line);
		_lines.Add(line);
	}

	void AddObstacle(int x, int y, int whichObstacle) {
		Obstacle brick = new Obstacle(x, y, whichObstacle);
		AddChild(brick);
		_obstalces.Add(brick);
	}

	void AddBallAndAim()
	{
		ball = new Ball(13, new Vec2(width / 2, 610));
		//aim = new Arrow(ball.position, ball.position, 0);
        //AddChild(aim);
        AddChild(ball);
    }

	void DestroyBallAndAim()
	{
		//aim.Destroy();
		ball.Destroy();
	}

	void LoadScene(int sceneNumber)
	{
		_startSceneNumber = sceneNumber;

		foreach (LineSegment line in _lines)
		{
			line.Destroy();
		}
		_lines.Clear();

		foreach (Obstacle brick in _obstalces)
		{
			brick.Destroy();
		}
		_obstalces.Clear();

		switch (sceneNumber)
		{

			case 1:

				DestroyBallAndAim();

				for (int i = 0; i < 7; i++)
				{
					for (int j = 0; j < 9; j++)
					{
						if (layout1[i * 9 + j] == 1)
						{
							AddObstacle(j * 56 + (width / 2 - 252) + 28, i * 56 + 30 + 28, 1);
						}
						if (layout1[i * 9 + j] == 2)
						{
							AddObstacle(j * 56 + (width / 2 - 252) + 28, i * 56 + 30 + 28, 2);
						}

					}

				}

				AddBallAndAim();

				break;
			case 2:

				DestroyBallAndAim();

				for (int i = 0; i < 7; i++)
				{
					for (int j = 0; j < 9; j++)
					{
						if (layout2[i * 9 + j] == 1)
						{
							AddObstacle(j * 56 + (width / 2 - 252) + 28, i * 56 + 30 + 28, 1);
						}
						if (layout2[i * 9 + j] == 2)
						{
							AddObstacle(j * 56 + (width / 2 - 252) + 28, i * 56 + 30 + 28, 2);
						}

					}

				}

				AddBallAndAim();

				break;
			case 3:

				DestroyBallAndAim();

				for (int i = 0; i < 7; i++)
				{
					for (int j = 0; j < 9; j++)
					{
						if (layout3[i * 9 + j] == 1)
						{
							AddObstacle(j * 56 + (width / 2 - 252) + 28, i * 56 + 30 + 28, 1);
						}
						if (layout3[i * 9 + j] == 2)
						{
							AddObstacle(j * 56 + (width / 2 - 252) + 28, i * 56 + 30 + 28, 2);
						}

					}

				}

				AddBallAndAim();

				break;

			default:

				for (int i = 0; i < 7; i++)
				{
					for (int j = 0; j < 9; j++)
					{
						if (layout1[i * 9 + j] == 1)
						{
							AddObstacle(j * 56 + (width / 2 - 252) + 28, i * 56 + 30 + 28, 1);
						}
						if (layout1[i * 9 + j] == 2)
						{
							AddObstacle(j * 56 + (width / 2 - 252) + 28, i * 56 + 30 + 28, 2);
						}

					}

				}

				AddBallAndAim();

				break;
		}

        // boundary:
        AddLine(new Vec2(width / 2 - 252, 30), new Vec2(width / 2 + 252, 30)); //top
        AddLine(new Vec2(width / 2 + 252, 630), new Vec2(width / 2 - 252, 630)); //bottom
        AddLine(new Vec2(width / 2 - 252, 630), new Vec2(width / 2 - 252, 30)); //left
        AddLine(new Vec2(width / 2 + 252, 30), new Vec2(width / 2 + 252, 630)); //right

    }


	void PrintInfo() {
		Console.WriteLine("Hold spacebar to slow down the frame rate.");
		Console.WriteLine("Press D to draw debug lines.");
        Console.WriteLine("Press T to do testing.");
    }

	void HandleInput() {
		targetFps = Input.GetKey(Key.SPACE) ? 5 : 60;

		if (Input.GetKeyDown(Key.D)) {
			Ball.drawDebugLine ^= true;
		}

        if (Input.GetKeyDown(Key.T))
        {
			//DoTests();
        }

        for (int i = 0; i < 10; i++) {
            if (Input.GetKeyDown(48 + i)) {
				if (i < 4 && i != 0) {
					LoadScene(i);
				}

			}
		}
	}

	//tests

    //static void DoTests(Vec2 testVec2)
    //{
    //    Console.WriteLine("Length ok? This should be : {0}" + Mathf.Sqrt(testVec2.x * testVec2.x + testVec2.y * testVec2.y), testVec2.Length());
    //    Console.WriteLine("Normalized ok? {0}  This should be: {1}" + "(" + testVec2.x / testVec2.Length() + ", " + testVec2.y / testVec2.Length() + ")", testVec2.Normalized());
    //}


    //check if the aim is dragged
    void Drag()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Vec2 mouseDown = new Vec2(Input.mouseX, Input.mouseY);
			float dist = (ball.position - mouseDown).Length();

			if (dist < 13)
			{
				aim = new Aim(ball.position, ball.position);
				AddChild(aim);

				_dragging = true;
			}
		}

	}

	//drawing the aim
	void Aiming()
	{
		if (_dragging)
		{

			//Vec2 aimVector = (new Vec2(ball.position.x + (ball.position.x - Input.mouseX), ball.position.y + (ball.position.y - Input.mouseY)) - aim.startPoint);
			//aim.vector = aimVector;
			//aim.scaleFactor = 2;

			Vec2 aimDirection = new Vec2(Input.mouseX - ball.position.x, Input.mouseY - ball.position.y).Normalized();
			aim.end = aim.start + aimDirection * -770;

			if (Input.GetMouseButtonUp(0))
			{
				_dragging = false;

                ball.velocity = (aim.end - aim.start).Normalized() * 6;
                aim.end = aim.start;
            }

		}
	}


	void HandleBall() {
		ball.Step();
	}

	void Update () {
		HandleInput();
		HandleBall();
        Aiming();
		Drag();
    }

	static void Main() {
		new MyGame().Start();
	}
}