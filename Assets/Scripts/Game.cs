using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (GameManager.instance.state == GameManager.CanvasState.Game)
            gameObject.SetActive(true);
        else
            gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
