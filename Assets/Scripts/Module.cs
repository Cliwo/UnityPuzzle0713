using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class Module : MonoBehaviour {

    public GameObject thunder;
    [HideInInspector] public GameObject actionArrow; //애로우를 만들 붕어빵 틀
    GameObject arrowInstance; //애로우 실체

    private Vector3 offset;
    private GameObject actionIndicator;
    private ActionIndicator script;

    private Thunder.ThunderDirection originDirection;

    bool isExpanded;
    // Use this for initialization
    void Start () {
        //위에 덧씌우는 방식, 배경이 투명이되어야함.
        //존재하지 않을 수도 있고, 존재하지 않는다면 아래작업 스킵
        //For Now , The Background of Sprite is white but, It should be transparent later.
        if (actionArrow != null)
        {
            arrowInstance = Instantiate(actionArrow, new Vector3(transform.position.x, transform.position.y, 0F), Quaternion.identity) as GameObject;
            actionIndicator = arrowInstance;
            script = arrowInstance.GetComponent<ActionIndicator>();
        }      
    }
	
	// Update is called once per frame
	void Update () {
    
	}
    public void setActionArrow(GameObject arrow)
    {
        this.actionArrow = arrow;
    }
    public void deactivateArrow()
    {
        if(this.arrowInstance != null)
            this.arrowInstance.SetActive(false);
        if (this.actionArrow != null)
            this.actionArrow = null;
    }

    private void checkColor()
    {
        //if colors same
        checkAction();
    }
    private void checkAction()
    {
        if (script != null)
            script.execute();
        else
            passThunderWithoutChangingDirection();
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Thunder")
        {
            originDirection = other.GetComponent<Thunder>().getDirection();
            checkColor();
        }

    }

    public void passThunderWithoutChangingDirection()
    {
        GameObject t;
        switch (originDirection)
        {
            case Thunder.ThunderDirection.UP:
                t = Instantiate(thunder, new Vector3(transform.position.x, transform.position.y + 1, 0F), Quaternion.identity) as GameObject;
                t.GetComponent<Thunder>().setDirection(Thunder.ThunderDirection.UP);
                break;
            case Thunder.ThunderDirection.DOWN:
                t = Instantiate(thunder, new Vector3(transform.position.x, transform.position.y - 1, 0F), Quaternion.identity) as GameObject;
                t.GetComponent<Thunder>().setDirection(Thunder.ThunderDirection.DOWN);
                break;
            case Thunder.ThunderDirection.RIGHT:
                t = Instantiate(thunder, new Vector3(transform.position.x + 1, transform.position.y, 0F), Quaternion.identity) as GameObject;
                t.GetComponent<Thunder>().setDirection(Thunder.ThunderDirection.RIGHT);
                break;
            case Thunder.ThunderDirection.LEFT:
                t = Instantiate(thunder, new Vector3(transform.position.x - 1, transform.position.y, 0F), Quaternion.identity) as GameObject;
                t.GetComponent<Thunder>().setDirection(Thunder.ThunderDirection.LEFT);
                break;
        }
    }

    

}
