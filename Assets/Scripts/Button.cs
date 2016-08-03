using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

    bool isExpanded;
    public static bool isOnGoing;
    public GameManager manager;

	// Use this for initialization
	void Start () {
        isOnGoing = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseEnter()
    {
        if (!isExpanded)
        {
            GetComponent<SpriteRenderer>().sortingOrder = 1;
            transform.localScale += new Vector3(1, 1, 1);
            isExpanded = true;
        }

    }
    void OnMouseExit()
    {
        GetComponent<SpriteRenderer>().sortingOrder = 0;
        transform.localScale += new Vector3(-1, -1, 0);
        isExpanded = false;
    }

    void OnMouseDown()
    {
        if(tag=="StartButton")
        {
            if (!isOnGoing)
            {
                Debug.Log("Start");
                isOnGoing = true;
                manager.GetComponent<BoardManager>().startThunder();
            }
        }
        else if (tag == "AllocateButton")
        {
            Debug.Log("Allo");
            if (isOnGoing)
            {
                isOnGoing = false;
                GameObject[] thunders = GameObject.FindGameObjectsWithTag("Thunder");
                for (int i = 0; i < thunders.Length; i++)
                    thunders[i].SetActive(false);
            }
        }
    }

   
}
