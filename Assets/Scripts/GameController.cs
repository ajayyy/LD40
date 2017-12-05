using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public static GameController instance;

    public GameObject player;

    public List<ShapeScript>[] queues = new List<ShapeScript>[4];

    public Text scoreText;
    public int score;

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
