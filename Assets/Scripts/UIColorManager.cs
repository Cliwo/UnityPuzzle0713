using UnityEngine;
using System.Collections;

public class UIColorManager : MonoBehaviour {

    CanvasRenderer canvas;

	// Use this for initialization
	void Start () {
        canvas = GetComponent<CanvasRenderer>();
        canvas.SetAlpha(0);
        CanvasRenderer[] childeren = canvas.GetComponentsInChildren<CanvasRenderer>();
        for (int i = 0; i < childeren.Length; i++)
            childeren[i].SetAlpha(0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetVisible()
    {
        canvas.SetAlpha(1);
        CanvasRenderer[] childeren = canvas.GetComponentsInChildren<CanvasRenderer>();
        for (int i = 0; i < childeren.Length; i++)
            childeren[i].SetAlpha(1);
    }

    public void SetInvisible()
    {
        canvas.SetAlpha(0);
        CanvasRenderer[] childeren = canvas.GetComponentsInChildren<CanvasRenderer>();
        for (int i = 0; i < childeren.Length; i++)
            childeren[i].SetAlpha(0);
    }
}
