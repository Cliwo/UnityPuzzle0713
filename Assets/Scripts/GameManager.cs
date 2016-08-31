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

    private GameObject Game;
    private GameObject Tutorial_Game;
    private GameObject Tutorial_Text;
    private GameObject Tutorial_Video;

    // Use this for initialization
    void Awake () {
        if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(gameObject);
        linkPanels();
        CreateBoard();
        ChangePanel(instance.state);
        DontDestroyOnLoad(gameObject);
   }
   
	public void linkPanels()
    {
        Game = GameObject.FindGameObjectWithTag("GamePanel");//.GetComponent<CanvasRenderer>();
        Tutorial_Video = GameObject.FindGameObjectWithTag("TutorialVideoPanel");//.GetComponent<CanvasRenderer>();
        Tutorial_Game = GameObject.FindGameObjectWithTag("TutorialGamePanel");//.GetComponent<CanvasRenderer>();
        Tutorial_Text = GameObject.FindGameObjectWithTag("TutorialTextPanel");//.GetComponent<CanvasRenderer>();
    }

    //Method for Button (Testing, In-Game Panel Changing)
    public void SetPanelTutorial_Video()
    {
        Debug.Log("SetTuto_Video");
        ChangePanel(CanvasState.Tutorial_Video);
    }
    public void SetPanelGame()
    {
        Debug.Log("SetGame");
        ChangePanel(CanvasState.Game);
    }
    public void SetPanelTutorial_Game()
    {
        Debug.Log("SetGame");
        ChangePanel(CanvasState.Tutorial_Game);
    }
    public void SetPanelTutorial_Text()
    {
        Debug.Log("SetGame");
        ChangePanel(CanvasState.Tutorial_Text);
    }
    public void ChangePanel(CanvasState state)
    {
        switch (state)
        {
            case CanvasState.Game:
                SetPanelAlpha(Tutorial_Video, 0);
                SetPanelAlpha(Tutorial_Text, 0);
                SetPanelAlpha(Tutorial_Game, 0);
                SetPanelAlpha(Game, 1);
                instance.state = CanvasState.Game;
                break;
            case CanvasState.Tutorial_Game:
                SetPanelAlpha(Tutorial_Video, 0);
                SetPanelAlpha(Tutorial_Text, 0);
                SetPanelAlpha(Tutorial_Game, 1);
                SetPanelAlpha(Game, 0);
                instance.state = CanvasState.Tutorial_Game;
                break;
            case CanvasState.Tutorial_Text:
                SetPanelAlpha(Tutorial_Video, 0);
                SetPanelAlpha(Tutorial_Text, 1);
                SetPanelAlpha(Tutorial_Game,0);
                SetPanelAlpha(Game, 0);
                instance.state = CanvasState.Tutorial_Text;
                break;
            case CanvasState.Tutorial_Video:
                SetPanelAlpha(Tutorial_Video, 1);
                SetPanelAlpha(Tutorial_Text, 0);
                SetPanelAlpha(Tutorial_Game, 0);
                SetPanelAlpha(Game, 0);
                instance.state = CanvasState.Tutorial_Video;
                break;
        }
    }
    public void SetPanelAlpha(GameObject render, int alpha)
    {
        /* 
         * At first, I tried to change alpha value of every panel (like UIColorManager Panel that I made),
         * but strangely It doesnt work...
         * I don't know why...
         * The comments on the below is that code
         *
         */
        Debug.Log("SetPanelAlpha : " + alpha);
        if (alpha == 0 && render!=null)
            render.SetActive(false);
        else
            render.SetActive(true);
        /*
        render.SetAlpha(alpha);
        CanvasRenderer[] childeren = Tutorial_Video.GetComponentsInChildren<CanvasRenderer>();
        for (int i = 0; i < childeren.Length; i++)
            childeren[i].SetAlpha(alpha);
            */
    }
    public void CreateBoard()
    {
        boardScript = GetComponent<BoardManager>();
        boardScript.SetUpScene(gameLevel);
        loadLanguageSetting();
    }
    public void DestroyBoard()
    {
        if (boardScript != null)
            boardScript.DestroyScene();
    }


	// Update is called once per frame
	void Update () {
        if (state == CanvasState.Game)
        {
            checkThunders();
            checkGameClear();
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
