using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    float speed = 1;

    float maxSpeed = 25;

    Rigidbody2D body;

	void Start () {
        body = GetComponent<Rigidbody2D>();
	}
	
	void FixedUpdate () {
        Vector2 movement = Vector2.zero;

        if (Input.GetKey(KeyCode.D)) {
            movement += new Vector2(1, 0);
        }
        if (Input.GetKey(KeyCode.A)) {
            movement += new Vector2(-1, 0);
        }

        if (Input.GetKey(KeyCode.W)) {
            movement += new Vector2(0, 1);
        }
        if (Input.GetKey(KeyCode.S)) {
            movement += new Vector2(0, -1);
        }

        //body.velocity = movement * speed;
        body.velocity += movement * speed;
        CapMaxSpeed();
        //body.velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * speed;
    }

    void CapMaxSpeed() {
        //the max distance they can move in a second is maxSpeed (in a second because everything is multiplied by Time.deltaTime)
        float distance = Vector2.Distance(body.velocity, Vector2.zero);
        if (distance >= maxSpeed) {
            float angle = MathHelper.GetRadians(body.velocity);
            body.velocity = MathHelper.RadianToVector2(angle) * maxSpeed;
        }
    }
}
