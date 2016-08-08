using UnityEngine;
using System.Collections;

public class UIColorManager : MonoBehaviour {

    static CanvasRenderer canvas;

    static GameObject moduleToDye;

	// Use this for initialization
	void Start () {
        canvas = GetComponent<CanvasRenderer>();
        if (canvas != null)
        {
            canvas.SetAlpha(0);
            CanvasRenderer[] childeren = canvas.GetComponentsInChildren<CanvasRenderer>();
            for (int i = 0; i < childeren.Length; i++)
                childeren[i].SetAlpha(0);
        }
	}

    public void onRedDown()
    {
        SetColor(1);
    }
    public void onYelllowDown()
    {
        SetColor(2);
    }
    public void onGreenDown()
    {
        SetColor(3);
    }
    public void onOrangeDown()
    {
        SetColor(4);
    }
    public void onBlueDown()
    {
        SetColor(0);
    }

    public void onInputRedDown()
    {
        SetInputColor(1);
    }
    public void onInputYelllowDown()
    {
        SetInputColor(2);
    }
    public void onInputGreenDown()
    {
        SetInputColor(3);
    }
    public void onInputOrangeDown()
    {
        SetInputColor(4);
    }
    public void onInputBlueDown()
    {
        SetInputColor(0);
    }

    public static void SetNextModule(GameObject obj)
    {
        moduleToDye = obj;
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
    public void SetColor(int color)
    {
        if (moduleToDye != null)
            moduleToDye.GetComponent<Module>().setModuleColor(color); 
    }
    public void SetInputColor(int color)
    {
        if (moduleToDye != null)
            moduleToDye.GetComponent<Module>().setInputColor(color); 
    }
}
