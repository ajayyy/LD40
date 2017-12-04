using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeScript : MonoBehaviour {

    Rigidbody2D body;

    public bool attached = false;

    int column;
    float rotation;

    float lastMove;

	void Start () {
        body = GetComponent<Rigidbody2D>();
	}
	
	void FixedUpdate () {
        GameController gameController = GameController.instance;

        if (Input.GetKey(KeyCode.Space)) {
            body.AddForce(MathHelper.RadianToVector2(MathHelper.GetRadians(transform.position - gameController.player.transform.position) - Mathf.PI) * 100);
        }

        if (Input.GetKey(KeyCode.LeftShift)) {
            body.AddForce(MathHelper.RadianToVector2(MathHelper.GetRadians(transform.position - gameController.player.transform.position) - Mathf.PI) * 10);
        }

        if (attached) {
            transform.position = gameController.player.transform.position + new Vector3(column, gameController.player.GetComponent<SpriteRenderer>().bounds.size.y);

            transform.localEulerAngles = new Vector3(0, 0, rotation);

            if (Input.GetKey(KeyCode.E) && Time.time - lastMove >= 0.2f) {
                column++;
                lastMove = Time.time;
            }

            if (Input.GetKey(KeyCode.Q) && Time.time - lastMove >= 0.2f) {
                column--;
                lastMove = Time.time;
            }

            if (Input.GetKey(KeyCode.R) && Time.time - lastMove >= 0.2f) {
                rotation += 90;
                rotation %= 360;
                lastMove = Time.time;
            }
            print(transform.localEulerAngles);

        }
    }
}
