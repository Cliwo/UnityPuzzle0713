using UnityEngine;
using System.Collections;
using Newtonsoft.Json;
public class GameManager : MonoBehaviour {

    public static GameManager instance= null;
    public BoardManager boardScript;
    
    public int gameLevel;

    public enum Language { ENG, KOR, FIN };
    public enum CanvasState { Game, Tutorial_Game, Tutorial_Text, Tutorial_Video };
    public Language language;
    public CanvasState state;

    public GameObject Game;
    public GameObject Tutorial_Game;
    public GameObject Tutorial_Text;
    public GameObject Tutorial_Video;

    // Use this for initialization
    void Awake () {
        if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
               
    }
	
    void Start()
    {
        initFirstCanvas();
    }

	// Update is called once per frame
	void Update () {
        if (state == CanvasState.Game)
        {
            checkThunders();
            checkGameClear();
        }
    }

    public void changeCanvas(CanvasState state)
    {
        switch (state)
        {
            case CanvasState.Game:
                instance.Game.SetActive(true);
                instance.Tutorial_Game.SetActive(false);
                instance.Tutorial_Text.SetActive(false);
                instance.Tutorial_Video.SetActive(false);
                Debug.Log("onGame");
                break;
            case CanvasState.Tutorial_Game:
                instance.Game.SetActive(false);
                instance.Tutorial_Game.SetActive(true);
                instance.Tutorial_Text.SetActive(false);
                instance.Tutorial_Video.SetActive(false);
                Debug.Log("onTuto_Game");
                break;
            case CanvasState.Tutorial_Text:
                instance.Game.SetActive(false);
                instance.Tutorial_Game.SetActive(false);
                instance.Tutorial_Text.SetActive(true);
                instance.Tutorial_Video.SetActive(false);
                Debug.Log("onTuto_Text");
                break;
            case CanvasState.Tutorial_Video:
                instance.Game.SetActive(false);
                instance.Tutorial_Game.SetActive(false);
                instance.Tutorial_Text.SetActive(false);
                instance.Tutorial_Video.SetActive(true);
                Debug.Log("onTuto_Video");
                break;
        }
    }
    public void SetCanvasGame()
    {
        changeCanvas(CanvasState.Game);
    }
    public void SetCanvasTutorial_Game()
    {
        changeCanvas(CanvasState.Tutorial_Game);
    }
    public void SetCanvasTutorial_Text()
    {
        changeCanvas(CanvasState.Tutorial_Text);
    }
    public void SetCanvasTutorial_Video()
    {
        changeCanvas(CanvasState.Tutorial_Video);
    }
    public void initFirstCanvas()
    {
        switch(state)
        {
            case CanvasState.Game:
                boardScript = GetComponent<BoardManager>();
                //이 주석 다음부터 초기화 시작
                boardScript.SetUpScene(gameLevel);
                loadLanguageSetting();
                break;
            case CanvasState.Tutorial_Game:
                break;
            case CanvasState.Tutorial_Text:
                break;
            case CanvasState.Tutorial_Video:
                break;
        }
    }
    private void checkThunders()
    {
        GameObject[] thunders = GameObject.FindGameObjectsWithTag("Thunder");
        if (thunders.Length == 0)
        {
            Button.isOnGoing = false;
        }
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
        if (isCleared == true && BoardManager.isStartPressedInThisLevel )
        {   //
            onGameClear();
        }       
    }
   
    private void loadLanguageSetting()
    {
        //http://answers.unity3d.com/questions/1171740/android-devices-does-not-read-json-file-but-unity.html
        TextAsset file = Resources.Load("langauage/" + convertLanguageToString(language)+ "/UI_Settings") as TextAsset;
        string content = file.ToString();
        Debug.Log(content);
        // Then put your convert string -> JSON object after this
        // -> I found this cord http://answers.unity3d.com/questions/1171740/android-devices-does-not-read-json-file-but-unity.html
        Assets.Scripts.LanguageBaseText data = JsonConvert.DeserializeObject<Assets.Scripts.LanguageBaseText>(content);
       
        GameObject.FindGameObjectWithTag("UIColorManager").GetComponent<UIColorManager>().loadLanguageSetting(data);
        GameObject.FindGameObjectWithTag("CurrentModuleTable").GetComponentInChildren<UnityEngine.UI.Text>().text = data.currentModuleText;
    }
    
    public static string convertLanguageToString(Language item)
    {
        switch(item)
        {
            case Language.ENG:
                return "English";
            case Language.KOR:
                return "Korean";
            case Language.FIN:
                return "Finnish";
            default:
                return "ENG";
        }
    }
}
