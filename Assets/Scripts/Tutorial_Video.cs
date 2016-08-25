using UnityEngine;
using System.Collections;

public class Tutorial_Video : MonoBehaviour {


	// Use this for initialization
	void Start () {
        if (GameManager.instance.state == GameManager.CanvasState.Tutorial_Video)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
