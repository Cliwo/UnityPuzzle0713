using UnityEngine;
using System.Collections;
using Newtonsoft.Json;
public class GameManager : MonoBehaviour {

    public static GameManager instance= null;
    public BoardManager boardScript;
    
    public int gameLevel;

    public enum Language { ENG, KOR };
    public Language language;

    // Use this for initialization
    void Awake () {
        if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        boardScript = GetComponent<BoardManager>();
        //이 주석 다음부터 초기화 시작

        boardScript.SetUpScene(gameLevel);
        loadLanguageSetting();

    }
	
	// Update is called once per frame
	void Update () {
        checkThunders();
        checkGameClear();
	}

    private void checkThunders()
    {
        GameObject[] thunders = GameObject.FindGameObjectsWithTag("Thunder");
        if (thunders.Length == 0)
            Button.isOnGoing = false;
    }
    private void onGameClear()
    {
        gameLevel++;
        boardScript.DestroyScene();
        
        boardScript.SetUpScene(gameLevel);
    }
    private void checkGameClear()
    {
        bool isCleared = true;
        GameObject[] destinations = GameObject.FindGameObjectsWithTag("Destination");
        for (int i = 0; i < destinations.Length; i++)
            if (destinations[i].GetComponent<Destination>().isCharged == false)
                isCleared = false;
        if(isCleared == true)
        {
            onGameClear();
        }
    }
   
    private void loadLanguageSetting()
    {
        //http://answers.unity3d.com/questions/1171740/android-devices-does-not-read-json-file-but-unity.html
        TextAsset file = Resources.Load("langauage/" + convertLanguageToString(language)) as TextAsset;
        string content = file.ToString();
        Debug.Log(content);
        // Then put your convert string -> JSON object after this
        // -> I found this cord http://answers.unity3d.com/questions/1171740/android-devices-does-not-read-json-file-but-unity.html
        Assets.Scripts.LanguageBaseText data = JsonConvert.DeserializeObject<Assets.Scripts.LanguageBaseText>(content);
       
        GameObject.FindGameObjectWithTag("UIColorManager").GetComponent<UIColorManager>().loadLanguageSetting(data);
        GameObject.FindGameObjectWithTag("CurrentModuleTable").GetComponentInChildren<UnityEngine.UI.Text>().text = data.currentModuleText;
    }
    
    private string convertLanguageToString(Language item)
    {
        switch(item)
        {
            case Language.ENG:
                return "ENG";
            case Language.KOR:
                return "KOR";
            default:
                return "ENG";
        }
    }
}
