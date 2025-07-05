using UnityEngine;

public class Rotator : MonoBehaviour
{
	public float speed = 100f;
	public bool clockwise = true;

	void Update ()
	{
		float direction = clockwise ? -1f : 1f;
		transform.Rotate (0f, 0f, speed * direction * Time.deltaTime);
	}
}
