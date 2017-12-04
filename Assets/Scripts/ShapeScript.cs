using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeScript : MonoBehaviour {

    Rigidbody2D body;

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
    }
}
