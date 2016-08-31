using UnityEngine;
using System.Collections;

public class UIColorManager : MonoBehaviour {

    static CanvasRenderer canvas;
    static CanvasRenderer[] colorCanvas;

    static GameObject moduleToDye;

    static UnityEngine.UI.Text outputText;
    static UnityEngine.UI.Text inputText;

    // Use this for initialization
    void Start () {
        if (GameManager.instance.state == GameManager.CanvasState.Game)
        {
            canvas = GameObject.FindGameObjectWithTag("UIColorManager").GetComponent<CanvasRenderer>();
            colorCanvas = new CanvasRenderer[2];
            colorCanvas[0] = GameObject.FindGameObjectWithTag("OutputColorCanvas").GetComponent<CanvasRenderer>();
            colorCanvas[1] = GameObject.FindGameObjectWithTag("InputColorCanvas").GetComponent<CanvasRenderer>();
        }
        if (canvas != null)
        {
            canvas.SetAlpha(0);
            CanvasRenderer[] childeren = canvas.GetComponentsInChildren<CanvasRenderer>();
            for (int i = 0; i < childeren.Length; i++)
                childeren[i].SetAlpha(0);

            outputText = GameObject.FindGameObjectWithTag("OutputText").GetComponent<UnityEngine.UI.Text>();
            inputText = GameObject.FindGameObjectWithTag("InputText").GetComponent<UnityEngine.UI.Text>();
        }
	}
    public static void setCanvas()
    {
        canvas = GameObject.FindGameObjectWithTag("UIColorManager").GetComponent<CanvasRenderer>();
    }

    public void loadLanguageSetting(Assets.Scripts.LanguageBaseText obj)
    {
        if(outputText == null)
            outputText = GameObject.FindGameObjectWithTag("OutputText").GetComponent<UnityEngine.UI.Text>();
        if(inputText==null)
            inputText = GameObject.FindGameObjectWithTag("InputText").GetComponent<UnityEngine.UI.Text>();
        outputText.GetComponent<UnityEngine.UI.Text>().text = obj.outputText;
        inputText.GetComponent<UnityEngine.UI.Text>().text = obj.inputText;
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
        Module moduleScript = moduleToDye.GetComponent<Module>();
        GameObject.FindGameObjectWithTag("CurrentModuleTable").GetComponent<CurrentModuleTable>().setCurrentModule(Module.parseColor(moduleScript.outputColor), Module.parseColor(moduleScript.inputColor));
    }

    public void SetVisible()
    {
        canvas.SetAlpha(1);
        colorCanvas[0].SetAlpha(1); //colorCanvas[0] == OutputColor
        CanvasRenderer[] childeren = colorCanvas[0].GetComponentsInChildren<CanvasRenderer>();
        for (int i = 0; i < childeren.Length; i++)
            childeren[i].SetAlpha(1);
        /*CanvasRenderer[] childeren = canvas.GetComponentsInChildren<CanvasRenderer>();
        for (int i = 0; i < childeren.Length; i++)
            childeren[i].SetAlpha(1);
            */
    }

    public void OutputToInput()
    {
        Debug.Log("output to input");
        colorCanvas[0].SetAlpha(0); //colorCanvas[1] == OutputColor
        CanvasRenderer[] childeren_out = colorCanvas[0].GetComponentsInChildren<CanvasRenderer>();
        for (int i = 0; i < childeren_out.Length; i++)
            childeren_out[i].SetAlpha(0);

        colorCanvas[1].SetAlpha(1); //colorCanvas[2] == InputColor
        CanvasRenderer[] childeren_in = colorCanvas[1].GetComponentsInChildren<CanvasRenderer>();
        for (int i = 0; i < childeren_in.Length; i++)
            childeren_in[i].SetAlpha(1);
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
        {
            moduleToDye.GetComponent<Module>().setModuleColor(color);
            GameObject.FindGameObjectWithTag("CurrentModuleTable").GetComponent<CurrentModuleTable>().onNextModuleObjectOutputColorChanged(color);
            //CurrentModuleTable.onNextModuleObjectChanged();
            OutputToInput();
        }
    }
    public void SetInputColor(int color)
    {
        if (moduleToDye != null)
        {
            moduleToDye.GetComponent<Module>().setInputColor(color);
            GameObject.FindGameObjectWithTag("CurrentModuleTable").GetComponent<CurrentModuleTable>().onNextModuleObjectInputColorChanged(color);
            //CurrentModuleTable.onNextModuleObjectChanged();
            SetInvisible();
        }
    }
}
