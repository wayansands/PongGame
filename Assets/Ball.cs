using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    public float speed = 30;
    public int pointsLeftPlayer = 0;
    public int pointsRightPlayer = 0;

	// Use this for initialization
	void Start ()
    {
        // Intial Velocity
        GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
        SetPoints(pointsLeftPlayer, pointsRightPlayer);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Note: 'col' holds the collision information. If the
        // Ball collided with a racket, then:
        //   col.gameObject is the racket
        //   col.transform.position is the racket's position
        //   col.collider is the racket's collider

        var direction = 0;

        if (collision.gameObject.name == "RacketLeft")
        {
            direction = 1;
            ProcessCollision(direction, collision);
        }
        if (collision.gameObject.name == "RacketRight")
        {
            direction = -1;
            ProcessCollision(direction, collision);
        }
        if (collision.gameObject.name == "WallRight")
        {
            pointsLeftPlayer++;
            SetPoints(pointsLeftPlayer, pointsRightPlayer);
        }
        if (collision.gameObject.name == "WallLeft")
        {
            pointsRightPlayer++;
            SetPoints(pointsLeftPlayer, pointsRightPlayer);
        }
    }

    void ProcessCollision(int direction, Collision2D collision)
    {
        // Calculate hit factor
        var y = HitFactor(transform.position, collision.transform.position, collision.collider.bounds.size.y);

        // Calculte direction, make length = 1 via .nomarlized
        var dir = new Vector2(direction, y).normalized;

        // Set Velocity with dir * speed
        GetComponent<Rigidbody2D>().velocity = dir * speed;
    }

    float HitFactor(Vector2 ballPos, Vector2 racketPos, float racketHeight)
    {
        // ascii art:
        // ||  1 <- at the top of the racket
        // ||
        // ||  0 <- at the middle of the racket
        // ||
        // || -1 <- at the bottom of the racket

        return (ballPos.y - racketPos.y) / racketHeight;
    }

    private void SetPoints(int pointsLeft, int pointsRight)
    {
        GameObject.Find("PointLeft").GetComponent<Text>().text = pointsLeft.ToString();
        GameObject.Find("PointRight").GetComponent<Text>().text = pointsRight.ToString();
    }
}
