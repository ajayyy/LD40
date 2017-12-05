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

    SpriteRenderer spriteRenderer;

    public LayerMask layerMask;

    void Start () {
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	void FixedUpdate () {
        GameController gameController = GameController.instance;

        if (attached && (gameController.queues[(int)(direction / 90)].Count == 0 || gameController.queues[(int) (direction / 90)][0] == this)) {
            float rotationOffset = 0;
            if(rotation == 90 || rotation == 270) {
                rotationOffset = -0.5f;
            }

            float width = spriteRenderer.bounds.size.x;
            float height = spriteRenderer.bounds.size.y;

            if(rotation + direction == 90 || rotation + direction == 270) {
                float middle = width;
                width = height;
                height = middle;
            }

            transform.position = gameController.player.transform.position + MathHelper.DegreeToVector3(direction) * (gameController.player.GetComponent<SpriteRenderer>().bounds.size.y/2 + row + width/2) + MathHelper.DegreeToVector3(direction-90) * (column - 0.5f);

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
                //if (row + (Mathf.Sin(rotation) * (spriteRenderer.bounds.size.y / 2)) < 0) {
                //    row++;
                //    collided = true;
                //}

                Vector3 dir = MathHelper.DegreeToVector3(direction + 180);

                RaycastHit2D hit = Physics2D.Raycast(transform.position + (dir * height/2), dir, Mathf.Infinity, layerMask, 0);
                if (hit.collider != null) {
                    if(hit.distance < 1) {
                        if (!collided) {
                            GameController.instance.queues[(int)(direction / 90)].Remove(this);
                        }
                        collided = true;

                        Collider[] colliders = GetComponents<Collider>();
                        foreach(Collider collider in colliders) {
                            if (!collider.isTrigger) {
                                collider.enabled = false;
                            }
                        }
                    }
                }

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

            GameController.instance.queues[(int)(direction / 90)].Add(this);

            return;
        } else if (collider.gameObject.tag == "Circle") return;

        //if (!collided) {
        //    GameController.instance.queues[(int)(direction / 90)].Remove(this);
        //}

        //collided = true;
        if (collider.gameObject.tag == "Shape") {
            //transform.position = collider.transform.position - new Vector3(0, 1);
        }
    }
}
