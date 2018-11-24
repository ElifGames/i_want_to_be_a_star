using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 10f;
    public MapSize mapSize;

    private new Rigidbody2D rigidbody;

	// Use this for initialization
	void Start ()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        Vector2 velocity = new Vector2(inputX, inputY);

        velocity *= speed;
        rigidbody.velocity = velocity;

        rigidbody.position = new Vector2(
            Mathf.Clamp(rigidbody.position.x, mapSize.xMin, mapSize.xMax),
            Mathf.Clamp(rigidbody.position.y, mapSize.yMin, mapSize.yMax));
	}
}

[System.Serializable]
public class MapSize
{
    public float xMin, xMax, yMin, yMax;

}