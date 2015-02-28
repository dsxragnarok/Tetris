using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {
	Vector2 pos;
	Vector2 topLeft;
	Vector2 bottomRight;
	Transform _transform;

	float gridTop = 4.5f;	// at vertical grid position 0
	float gridLeft = -3f;	// at horizontal grid position 0
	float gridBottom = -4;	// at vertical grid position max (17)
	float gridRight = 1.5f;	// at horizontal grid position max (9)

	public GameObject block;
	public float width;
	public float height;

	public float Left {
		get{ return topLeft.x; }
	}
	public float Right {
		get{ return bottomRight.x; }
	}
	public float Top {
		get{ return topLeft.y; }
	}
	public float Bottom {
		get{ return bottomRight.y; }
	}

	public static int gridWidth = 10;
	public static int gridHeight = 18;

	public enum Orientation {
		horizontal,
		vertical
	}

	public static GameObject[,] grid = new GameObject[gridWidth,gridHeight];


	void Awake() {
		_transform = transform;
	}

	// Use this for initialization
	void Start () {
		pos = new Vector2(_transform.position.x, _transform.position.y);
		topLeft = pos;// + new Vector2(0f, height / 2f);
		bottomRight = pos + new Vector2(width, -height);

		grid [0, 0] = (GameObject)GameObject.Instantiate (block, new Vector3 (CalculateGridPosition (Orientation.horizontal, 0), CalculateGridPosition (Orientation.vertical, 0), 0f), Quaternion.identity);
		grid [0, 0] = (GameObject)GameObject.Instantiate (block, new Vector3 (CalculateGridPosition (Orientation.horizontal, 9), CalculateGridPosition (Orientation.vertical, 17), 0f), Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnDrawGizmos () {
		pos = new Vector2(transform.position.x, transform.position.y);
		topLeft = pos;// + new Vector2(0f, height / 2f);
		bottomRight = pos + new Vector2(width, -height);

		Vector3 tl = new Vector3 (Left, Top, 9);
		Vector3 br = new Vector3 (Right, Bottom, 9);
		Vector3 tr = new Vector3 (Right, Top, 9);
		Vector3 bl = new Vector3 (Left, Bottom, 9);
		
		Debug.DrawLine (tl, tr, Color.red);
		Debug.DrawLine (tl, bl, Color.red);
		Debug.DrawLine (bl, br, Color.red);
		Debug.DrawLine (tr, br, Color.red);
	}

	public Vector2 CalculateGridPosition (int x, int y) {
		return new Vector2 (CalculateGridPosition (Orientation.horizontal, x), CalculateGridPosition (Orientation.vertical, y));
	}

	public Vector2 CalculateGridPosition (Vector2 position) {
		return CalculateGridPosition ((int)position.x, (int)position.y);
	}

	public float CalculateGridPosition (Orientation dir, int index) {
		switch (dir) {
			case Orientation.horizontal:
				return gridLeft + index * 0.5f;
			case Orientation.vertical:
				return gridTop - index * 0.5f;
		}
		return 0f;
	}

	// round the vector to the nearest 0.5
	public static float RoundToNearestHalf (float number) {
		return (float)System.Math.Round (number, System.MidpointRounding.AwayFromZero) / 2;
	}

	public static Vector2 RoundVector(Vector2 vector) {
		return new Vector2(Grid.RoundToNearestHalf(vector.x), Grid.RoundToNearestHalf(vector.y));
	}

	public static bool InsideContainer (Vector2 position) {
		return position.x > 0 && position.x < gridWidth && position.y < gridHeight;
	}
}
