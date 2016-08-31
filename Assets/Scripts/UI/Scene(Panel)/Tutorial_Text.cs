using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Tutorial_Text : MonoBehaviour {

    [HideInInspector]
    public Image backgroundImage;
    [HideInInspector]
    public Text text;

    public Sprite[] backgroundImageSources;
    private string[] textSources;

    private int currentStep;

    // Use this for initialization
    void Start () {
        
        GameObject obj = GameObject.FindGameObjectWithTag("TutorialTextImage");
        if(obj!=null)
            backgroundImage = obj.GetComponent<Image>();
        text = GameObject.FindGameObjectWithTag("TutorialText-Text").GetComponent<Text>();
        loadTextData(); //For now  text data are loaded from json file.
                        //You can change loadTextData method, If you want to use server.
                        //load data from server, not resources and initialize backgroundImageSources and textSources
                        //and use callback, to call initBackgroundAndText() method.

        initBackgroundAndText(); //If you using server, you can call this method, after all server process has completed.

    }

    // Update is called once per frame
    void Update () {
	
	}

    private void initBackgroundAndText()
    {
        currentStep = 0;
        backgroundImage.sprite = backgroundImageSources[currentStep];
        text.text = textSources[currentStep];
    }

    private void loadTextData()
    {
        TextAsset file = Resources.Load("langauage/" + GameManager.convertLanguageToString(GameManager.instance.language) + "/Tutorial_Text") as TextAsset;
        string content = file.ToString();
        Debug.Log(content);
        parseTextData(content);
    }

    private void parseTextData(string str)
    {
        int count = 0;
        string[] data_set = str.Split(':');
        textSources = new string[data_set.Length - 1];
        for (int i = 1; i < data_set.Length - 1; i++)
        {
            string[] data = data_set[i].Split(',');
            textSources[count++] = stringFilter(data[0]);
        }
        textSources[count++] = stringFilter(data_set[data_set.Length - 1]);
    }

    public string stringFilter(string item)
    {
        //delete ' " ' or ' } '
        char[] before_edit = item.ToCharArray();
        string after_edit = "";
        for (int j = 0; j < before_edit.Length; j++)
        {
            if (before_edit[j] == '}' || before_edit[j] == '"')
                continue;
            after_edit += before_edit[j];
        }
        return after_edit;
    }

    public void onNextPressed()
    {
        currentStep++;
        if (currentStep >= backgroundImageSources.Length)
            currentStep = backgroundImageSources.Length - 1;
        backgroundImage.sprite = backgroundImageSources[currentStep];
        text.text = textSources[currentStep];
    }
    public void onBackPressed()
    {
        currentStep--;
        if (currentStep < 0)
            currentStep = 0;
        backgroundImage.sprite = backgroundImageSources[currentStep];
        text.text = textSources[currentStep];
    }
}
