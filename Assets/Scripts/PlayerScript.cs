using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    float speed = 1;

    float maxSpeed = 15;

    //movement friction
    float friction = 0.5f;

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
        //body.velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * speed;

        CapMaxSpeed();
        MovementFriction(); //because this object is kinematic so it has no normal friction
    }

    void CapMaxSpeed() {
        //the max distance they can move in a second is maxSpeed (in a second because everything is multiplied by Time.deltaTime)
        float distance = Vector2.Distance(body.velocity, Vector2.zero);
        if (distance >= maxSpeed) {
            float angle = MathHelper.GetRadians(body.velocity);
            body.velocity = MathHelper.RadianToVector2(angle) * maxSpeed;
        }
    }

    void MovementFriction() {

        //movement friction (because doing it manually gives more control than using physics materials)

        float angle = MathHelper.GetRadians(body.velocity);

        double xspeed = body.velocity.x;
        double yspeed = body.velocity.y;

 		double xchange = - (Mathf.Cos(angle) * friction);
 		double ychange = - (Mathf.Sin(angle) * friction);
 		
 		//if they are going to overshoot the goal, set it to zero (the goal), and don't move it
 		if(xchange > 0 && xspeed + xchange > 0){
 			xspeed = 0;
		}else if(xchange < 0 && xspeed + xchange < 0){
 			xspeed = 0;
 		}
 		if(ychange > 0 && yspeed + ychange > 0){
 			yspeed = 0;
 		}else if(ychange < 0 && yspeed + ychange < 0){
 			yspeed = 0;
 		}
 		
 		if(xspeed != 0) xspeed += xchange;
 		if(yspeed != 0) yspeed += ychange;

        body.velocity = new Vector2((float) xspeed, (float) yspeed);
    }
}
