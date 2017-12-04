using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeScript : MonoBehaviour {

    Rigidbody2D body;

    public bool attached = false;

    int column;
    int row = 10;
    float direction;
    float rotation;

    float lastMove;
    float lastRowChange; //last movement downward

    bool collided = false; //current colliding with something

	void Start () {
        body = GetComponent<Rigidbody2D>();
	}
	
	void FixedUpdate () {
        GameController gameController = GameController.instance;

        if (attached) {
            float rotationOffset = 0;
            if(rotation == 90 || rotation == 270) {
                rotationOffset = -0.5f;
            }
            //print(direction);
            transform.position = gameController.player.transform.position + MathHelper.DegreeToVector3(direction) * (gameController.player.GetComponent<SpriteRenderer>().bounds.size.y/2 + row + rotationOffset) + MathHelper.DegreeToVector3(direction-90) * (column - 0.5f);

            transform.localEulerAngles = new Vector3(0, 0, rotation);

            if (Input.GetKey(KeyCode.E) && Time.time - lastMove >= 0.2f && !collided) {
                column++;
                if (column > 4) {
                    column = 4;
                }
                lastMove = Time.time;
            }

            if (Input.GetKey(KeyCode.Q) && Time.time - lastMove >= 0.2f && !collided) {
                column--;
                if (column < -3) {
                    column = -3;
                }
                lastMove = Time.time;
            }

            if (Input.GetKey(KeyCode.R) && Time.time - lastMove >= 0.2f && !collided) {
                rotation += 90;
                rotation %= 360;
                lastMove = Time.time;
            }

            //if(Time.time - lastRowChange >= 0.5f && row > 1) {
            if (Time.time - lastRowChange >= 0.5f && !collided) {
                row--;
                lastRowChange = Time.time;
            }

        }else {

            if (Input.GetKey(KeyCode.Space)) {
                body.AddForce(MathHelper.RadianToVector2(MathHelper.GetRadians(transform.position - gameController.player.transform.position) - Mathf.PI) * 100);
            }

            if (Input.GetKey(KeyCode.LeftShift)) {
                body.AddForce(MathHelper.RadianToVector2(MathHelper.GetRadians(transform.position - gameController.player.transform.position) - Mathf.PI) * 10);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.tag == "Circle" && !attached) {

            attached = true;
            GetComponent<Rigidbody2D>().isKinematic = true;
            transform.parent = GameController.instance.player.transform;

            float shortestDistance = -1;
            float shortestDirection = 0;

            for (float f = 0; f < 360; f += 90) {
                float distance = Vector3.Distance(transform.position, GameController.instance.player.transform.position + (MathHelper.DegreeToVector3(f) * (GetComponent<SpriteRenderer>().bounds.size.x / 2)));
                //print(transform.position + "     :      " + GameController.instance.player.transform.position + (MathHelper.DegreeToVector3(f) * (GetComponent<SpriteRenderer>().bounds.size.x / 2)));
                print(distance);
                if (distance < shortestDistance || shortestDistance == -1) {
                    shortestDistance = distance;
                    shortestDirection = f;
                }
            }
            print(shortestDirection);
            direction = shortestDirection;

            return;
        } else if (collider.gameObject.tag == "Circle") return;

        collided = true;
        if(collider.gameObject.tag == "Shape") {
            //transform.position = collider.transform.position - new Vector3(0, 1);
        }
    }
}
