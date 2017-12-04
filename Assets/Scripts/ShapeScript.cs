﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeScript : MonoBehaviour {

    Rigidbody2D body;

    public bool attached = false;

    int column;
    int row = 15;
    float rotation;

    float lastMove;
    float lastRowChange; //last movement downward

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
            transform.position = gameController.player.transform.position + new Vector3(column - 0.5f, gameController.player.GetComponent<SpriteRenderer>().bounds.size.y/2 + row);

            transform.localEulerAngles = new Vector3(0, 0, rotation);

            if (Input.GetKey(KeyCode.E) && Time.time - lastMove >= 0.2f) {
                column++;
                if(column > 4) {
                    column = 4;
                }
                lastMove = Time.time;
            }

            if (Input.GetKey(KeyCode.Q) && Time.time - lastMove >= 0.2f) {
                column--;
                if (column < -3) {
                    column = -3;
                }
                lastMove = Time.time;
            }

            if (Input.GetKey(KeyCode.R) && Time.time - lastMove >= 0.2f) {
                rotation += 90;
                rotation %= 360;
                lastMove = Time.time;
            }

            if(Time.time - lastRowChange >= 0.5f && row > 1) {
                row--;
                lastRowChange = Time.time;
            }

        }
    }
}
