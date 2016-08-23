using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class CurrentModuleTable : MonoBehaviour {

    [HideInInspector]public Text currentModuleText;

    public Sprite[] outputSprites;
    public Sprite[] inputSprites;

    public GameObject[] currentModuleColorOutput;
    public GameObject[] currentModuleColorInput;
    // Use this for initialization
    void Start () {
        currentModuleText = GetComponentInChildren<Text>();
        currentModuleColorOutput = GameObject.FindGameObjectsWithTag("CurrentModuleTableOutputColor");
        currentModuleColorInput = GameObject.FindGameObjectsWithTag("CurrentModuleTableInputColor");
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void onNextModuleObjectOutputColorChanged(int color)
    {
        color--;
        if (color == -1)
            color = 4;
        for (int i = 0; i < 2; i++)
        {
            currentModuleColorOutput[i].GetComponent<Image>().sprite = outputSprites[color];
            
        }
    }
    public void onNextModuleObjectInputColorChanged(int color)
    {
        color--;
        if (color == -1)
            color = 4;
        for (int i = 0; i < 2; i++)
        {

            currentModuleColorInput[i].GetComponent<Image>().sprite = inputSprites[color];
        }
    }

}
