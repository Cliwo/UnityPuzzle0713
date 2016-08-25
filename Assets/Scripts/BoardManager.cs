using UnityEngine;
using System.Collections;
using Assets.Scripts;
using Newtonsoft.Json;

public class BoardManager : MonoBehaviour {
    private Transform boardHolder;
    public GameObject[] floorTiles;
    public GameObject[] arrows;

    private Transform levelHolder;
    public GameObject startingPoint;
    public GameObject module;
    public GameObject thunder;
    public GameObject destination;
    public GameObject startButton;
    public GameObject allocateButton;
    public GameObject clearButton;
    public GameObject border;
    
    public static GameObject nextModuleObject;
    public static GameObject nextModuleArrow;

    public static int FINAL_LEVEL = 3;
    public static bool isStartPressedInThisLevel = false;
    public static int columns = 25; //x좌표
    public static int rows = 10; //y좌표

    private static float startX_Pos;
    //= 2;
    private static float startY_Pos;
        //= rows / 2 - 3;

    

    public GameObject GetModuleKind(ModuleKind mk)
    { 
        switch(mk)
        {
            case ModuleKind.UpAndDownArrow:
                return arrows[0];
            case ModuleKind.CrossArrow:
                return arrows[1];
        }
        return null;
    }
    public void DestroyScene()
    {
        Debug.Log("Destroy Scene Called");
        GameObject[] modules = GameObject.FindGameObjectsWithTag("Module");
        for (int i = 0; i < modules.Length; i++)
            modules[i].GetComponent<Module>().deactivateModule();
        GameObject[] thunders = GameObject.FindGameObjectsWithTag("Thunder");
        for (int i = 0; i < thunders.Length; i++)
            Destroy(thunders[i].gameObject);
        Destroy(boardHolder.gameObject);
    }
    public void SetUpScene(LevelInformation level)
    {
        isStartPressedInThisLevel = false;
        levelHolder = new GameObject("LevelObjects").transform;

        startX_Pos = level.startPoint.x;
        startY_Pos =  level.startPoint.y;
        GameObject inst = Instantiate(startingPoint, new Vector3(startX_Pos, startY_Pos, 0F), Quaternion.identity) as GameObject;
        inst.transform.SetParent(levelHolder);
        Debug.Log(startX_Pos + " " + startY_Pos);
        for (int i = 0; i < level.destinations.Length; i++)
        {
            GameObject temp = Instantiate(destination, new Vector3(level.destinations[i].x, level.destinations[i].y, 0F), Quaternion.identity)as GameObject;
            temp.transform.SetParent(levelHolder); 
            Destination dest = temp.GetComponent<Destination>();
            dest.setColor(level.destinationColors[i]);
        }
        BoardSetup(level.destinations);
        if (level.modulePositions != null)
        {
            for (int i = 0; i < level.modulePositions.Length; i++)
            {
                GameObject md = Instantiate(module, new Vector3(level.modulePositions[i].x, level.modulePositions[i].y, 0F), Quaternion.identity) as GameObject;
                md.transform.SetParent(levelHolder);
                md.GetComponent<Module>().setActionArrow(GetModuleKind(level.moduleKind[i]));
            }
        }
        levelHolder.transform.SetParent(boardHolder);
    }

    public void startThunder()
    {
        isStartPressedInThisLevel = true;
        GameObject t = Instantiate(thunder, new Vector3(startX_Pos, startY_Pos, 0F), Quaternion.identity) as GameObject;
        t.GetComponent<Thunder>().setColor(2);
        //처음 번개는 노란색으로 시작.
    }

    public void SetUpScene(int level)
    {
        if(level> FINAL_LEVEL)
        {
            Debug.Log("Game Complete");
            return; 
        }

        Debug.Log("SetUp Scene Called" + ", level : " + level);
        string level_name = "level" + level;

        //http://answers.unity3d.com/questions/1171740/android-devices-does-not-read-json-file-but-unity.html
        TextAsset file = Resources.Load("levels/"+level_name) as TextAsset;
        string content = file.ToString();
        Debug.Log(content);
        // Then put your convert string -> JSON object after this
        // -> I found this cord http://answers.unity3d.com/questions/1171740/android-devices-does-not-read-json-file-but-unity.html
        
        LevelInformation levelInfor = JsonConvert.DeserializeObject<LevelInformation>(content);

        SetUpScene(levelInfor);
    }

    public void BoardSetup(Coord[] destinations)
    {
        Vector2[] v_destinations = new Vector2[destinations.Length];
        for(int i=0;i<v_destinations.Length;i++)
        {
            v_destinations[i].x = destinations[i].x;
            v_destinations[i].y = destinations[i].y;
        }
        BoardSetup(v_destinations);
    }

    public void BoardSetup(Vector2[] destinations)
    {
        bool isNothing=true;
        for (int i = 0; i < destinations.Length; i++)
            Debug.Log(destinations[i].x + " " + destinations[i].y);
        boardHolder = new GameObject("Board").transform;
        for (int x = -1; x <= columns ; x++)
        {
            for (int y = -1; y <= rows; y++)
            {
                if(x==-1 || x==columns || y==-1 || y==rows)
                {
                    GameObject inst = Instantiate(border, new Vector3(x, y, 0F), Quaternion.identity) as GameObject;
                    inst.transform.SetParent(boardHolder);
                }
                else
                {
                    for (int i = 0; i < destinations.Length; i++)
                        if (destinations[i].x == x && destinations[i].y == y)
                            isNothing = false;
                    if (startX_Pos == x && startY_Pos == y)
                        isNothing = false;
                    if (isNothing)
                    {
                        GameObject toInstantiae = floorTiles[0];
                        //floorTiles[Random.Range(0, floorTiles.Length)];
                        GameObject instance = Instantiate(toInstantiae, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
                        SpriteRenderer sp = instance.GetComponent<SpriteRenderer>();
                        //sp.transform.localScale = new Vector2(2.0f, 1.0f);
                        //Quaternion이 머야 ?
                        instance.transform.SetParent(boardHolder);
                    }
                    else
                        isNothing = true;
                }
            }
        }
  
        Instantiate(startButton, new Vector3(2, 12, 0F), Quaternion.identity);
        Instantiate(allocateButton, new Vector3(8, 12, 0F), Quaternion.identity);
        Instantiate(clearButton, new Vector3(14, 12, 0f), Quaternion.identity);
    }

    /*
     *
     *
    public void SetUpSceneWithTestCord(int level)
    {
       
        TestCoord temp2 = new TestCoord();
        string v = JsonConvert.SerializeObject(temp2);
        Debug.Log(v); 
     
        switch (level)
        {
            case 1:
                LevelInformation level1 = new LevelInformation();
    level1.startPoint = new Coord(columns / 3 , rows / 2);
    level1.destinations = new Coord[1];
                level1.destinations[0] = new Coord(columns/3 * 2, rows / 2);
    level1.destinationColors = new Module.Color[1];
                level1.destinationColors[0] = Module.Color.YELLOW;
                SetUpScene(level1);
    string v = JsonConvert.SerializeObject(level1);
    Debug.Log(v);
                break;

            case 2:
                LevelInformation level2 = new LevelInformation();
    level2.startPoint = new Coord(columns / 3, rows / 2);
    level2.destinations = new Coord[2];
                level2.destinations[1] = new Coord(columns / 3 * 2, rows / 2 +3);
    level2.destinations[0] = new Coord(columns / 3 * 2, rows / 2 -3);
    level2.destinationColors = new Module.Color[2];
                level2.destinationColors[0] = Module.Color.RED;
                level2.destinationColors[1] = Module.Color.RED;
                SetUpScene(level2);
    string v2 = JsonConvert.SerializeObject(level2);
    Debug.Log(v2);
                break;

            case 3:
                LevelInformation level3 = new LevelInformation();
    level3.startPoint = new Coord(columns / 3, rows / 2);
    level3.destinations = new Coord[3];
                level3.destinations[2] = new Coord(19, 5);
    level3.destinations[1] = new Coord(columns / 3 * 2, rows / 2 + 3);
    level3.destinations[0] = new Coord(columns / 3 * 2, rows / 2 - 3);
    level3.destinationColors = new Module.Color[3];
                level3.destinationColors[0] = Module.Color.BLUE;
                level3.destinationColors[1] = Module.Color.BLUE;
                level3.destinationColors[2] = Module.Color.BLUE;
                string v3 = JsonConvert.SerializeObject(level3);
    Debug.Log(v3);
                //string json = JsonConvert.SerializeObject(level3);
                //Debug.Log(level3);

                SetUpScene(level3);
                break;

        }
       
    }


     *
     * 
     */
}
