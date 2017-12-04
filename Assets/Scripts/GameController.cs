using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public static GameController instance;

    public GameObject player;

    public List<ShapeScript>[] queues = new List<ShapeScript>[4];

    void Start () {
		if(instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }

        for (int i = 0; i < queues.Length; i++) {
            queues[i] = new List<ShapeScript>();
        }
    }
	
	void Update () {
		
	}
}
