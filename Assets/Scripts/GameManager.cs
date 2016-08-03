using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager instance= null;
    public BoardManager boardScript;

    public int gameLevel;

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
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
