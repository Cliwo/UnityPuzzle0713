using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class BoardManager : MonoBehaviour {
    private Transform boardHolder;
    public GameObject[] floorTiles;
    public GameObject[] arrows;

    public GameObject startingPoint;
    public GameObject module;
    public GameObject thunder;
    public GameObject destination;
    public GameObject startButton;
    public GameObject allocateButton;
    
    public static GameObject nextModuleObject;
    public static GameObject nextModuleArrow;

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
    
    public void SetUpScene(LevelInformation level)
    {
        startX_Pos = level.startPoint.x;
        startY_Pos =  level.startPoint.y;
        Instantiate(startingPoint, new Vector3(startX_Pos, startY_Pos, 0F), Quaternion.identity);
        Debug.Log(startX_Pos + " " + startY_Pos);
        for (int i = 0; i < level.destinations.Length; i++)
        {
            Instantiate(destination, new Vector3(level.destinations[i].x, level.destinations[i].y, 0F), Quaternion.identity);  
        }
        BoardSetup(level.destinations);
        if (level.modulePositions != null)
        {
            for (int i = 0; i < level.modulePositions.Length; i++)
            {
                GameObject md = Instantiate(module, new Vector3(level.modulePositions[i].x, level.modulePositions[i].y, 0F), Quaternion.identity) as GameObject;
                md.GetComponent<Module>().setActionArrow(GetModuleKind(level.moduleKind[i]));
            }
        }
    }

    public void startThunder()
    {
        Instantiate(thunder, new Vector3(startX_Pos, startY_Pos, 0F), Quaternion.identity);
    }


    public void SetUpScene(int level)
    {
        switch(level)
        {
            case 1:
                LevelInformation level1 = new LevelInformation();
                level1.startPoint = new Vector2(columns / 3 , rows / 2);
                level1.destinations = new Vector2[1];
                level1.destinations[0] = new Vector2(columns/3 * 2, rows / 2);
                SetUpScene(level1);
                break;

            case 2:
                LevelInformation level2 = new LevelInformation();
                level2.startPoint = new Vector2(columns / 3, rows / 2);
                level2.destinations = new Vector2[2];
                level2.destinations[1] = new Vector2(columns / 3 * 2, rows / 2 +3);
                level2.destinations[0] = new Vector2(columns / 3 * 2, rows / 2 -3);
                SetUpScene(level2);
                break;

            case 3:
                LevelInformation level3 = new LevelInformation();
                level3.startPoint = new Vector2(columns / 3, rows / 2);
                level3.destinations = new Vector2[3];
                level3.destinations[2] = new Vector2(19, 5);
                level3.destinations[1] = new Vector2(columns / 3 * 2, rows / 2 + 3);
                level3.destinations[0] = new Vector2(columns / 3 * 2, rows / 2 - 3); 
                SetUpScene(level3);
                break;

        }
       
    }

    public void BoardSetup(Vector2[] destinations)
    {
        bool isNothing=true;
        for (int i = 0; i < destinations.Length; i++)
            Debug.Log(destinations[i].x + " " + destinations[i].y);
        boardHolder = new GameObject("Board").transform;
        for (int x = 0; x < columns ; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                for (int i = 0; i < destinations.Length; i++)
                    if (destinations[i].x == x && destinations[i].y == y)
                        isNothing=false;
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
        Instantiate(startButton, new Vector3(2, 12, 0F), Quaternion.identity);
        Instantiate(allocateButton, new Vector3(8, 12, 0F), Quaternion.identity);
    }
}
