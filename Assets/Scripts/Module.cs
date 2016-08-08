using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class Module : MonoBehaviour {

    public GameObject thunder;
    public GameObject inputColorRenderer;
    [HideInInspector] public GameObject actionArrow; //애로우를 만들 붕어빵 틀
    [HideInInspector] public GameObject floor;
    GameObject arrowInstance; //애로우 실체
    GameObject inputColorRendererInstance; //inputColor 실체

    private Vector3 offset;
    private GameObject actionIndicator;
    private ActionIndicator script;

    private Thunder.ThunderDirection originDirection;
    private Color originColor;

    public Sprite[] sprites = new Sprite[5];
    public enum Color { BLUE, RED, YELLOW, GREEN, ORANGE };
    public Color outputColor;
    public Color inputColor;
    public Sprite[] inputSprites = new Sprite[5];

    bool isExpanded;
    // Use this for initialization
    void Start () {
        //위에 덧씌우는 방식, 배경이 투명이되어야함.
        //존재하지 않을 수도 있고, 존재하지 않는다면 아래작업 스킵
        //For Now , The Background of Sprite is white but, It should be transparent later.

        outputColor = Color.BLUE; //기본적으로 파란색.
        inputColor = Color.BLUE;
        if (actionArrow != null)
        {
            arrowInstance = Instantiate(actionArrow, new Vector3(transform.position.x, transform.position.y, -10F), Quaternion.identity) as GameObject;
            actionIndicator = arrowInstance;
            script = arrowInstance.GetComponent<ActionIndicator>();
        }
        inputColorRendererInstance = Instantiate(inputColorRenderer, new Vector3(transform.position.x, transform.position.y, 0F), Quaternion.identity)as GameObject;
    }
	
	// Update is called once per frame
	void Update () {
        
    }
    public void setModuleColor(int color)
    {
        if (color < 5 && color >-1)
        {
            SpriteRenderer render = GetComponent<SpriteRenderer>();
            render.sprite = sprites[color];
            outputColor=parseColor(color);
        }
    }
    public void setInputColor(int color)
    {
        Debug.Log("Input");
        if (color < 5 && color > -1)
        {
            SpriteRenderer render = inputColorRendererInstance.GetComponent<SpriteRenderer>();
            render.sprite = inputSprites[color];
            inputColor = parseColor(color);
        }
    }
    public static Color parseColor(int val)
    {
        switch(val)
        {
            case 0:
                return Color.BLUE;
            case 1:
                return Color.RED;
            case 2:
                return Color.YELLOW;
            case 3:
                return Color.GREEN;
            case 4:
                return Color.ORANGE;
            default:
                return Color.BLUE;
        }
    }
    public static int parseColor(Color color)
    {
        switch(color)
        {
            case Color.RED:
                return 1;
            case Color.YELLOW:
                return 2;
            case Color.GREEN:
                return 3;
            case Color.ORANGE:
                return 4;
            case Color.BLUE:
                return 0;
            default:
                return 0;

        }
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
    public void deactivateInputColor()
    {
        if (inputColorRendererInstance != null)
            inputColorRendererInstance.SetActive(false);
    }

    private void checkColor(Thunder thunder)
    {
        //if colors same
        if (inputColor == thunder.getColor())
            checkAction();
        else
            passThunderWithoutChangingDirection(); //Color가 맞지않으면, Thunder 의 Color를 바꾸지않는다.
    }
    private void checkAction()
    {
        if (script != null)
            script.execute(outputColor); //여기다 color를 넣어주는걸로하자.
        else
            passThunderChangingColorWithoutChangingDirection(); //Color가 맞으면 color를 바꾼다. 
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Thunder")
        {
            originDirection = other.GetComponent<Thunder>().getDirection();
            originColor = other.GetComponent<Thunder>().getColor();
            checkColor(other.GetComponent<Thunder>());
        }

    }

    public void passThunderWithoutChangingDirection()
    {
        Debug.Log("WithOut Changing");
        GameObject t;
        switch (originDirection)
        {
            case Thunder.ThunderDirection.UP:
                t = Instantiate(thunder, new Vector3(transform.position.x, transform.position.y + 1, 0F), Quaternion.identity) as GameObject;
                t.GetComponent<Thunder>().setDirection(Thunder.ThunderDirection.UP);
                t.GetComponent<Thunder>().setColor(parseColor(originColor));
                break;
            case Thunder.ThunderDirection.DOWN:
                t = Instantiate(thunder, new Vector3(transform.position.x, transform.position.y - 1, 0F), Quaternion.identity) as GameObject;
                t.GetComponent<Thunder>().setDirection(Thunder.ThunderDirection.DOWN);
                t.GetComponent<Thunder>().setColor(parseColor(originColor));
                break;
            case Thunder.ThunderDirection.RIGHT:
                t = Instantiate(thunder, new Vector3(transform.position.x + 1, transform.position.y, 0F), Quaternion.identity) as GameObject;
                t.GetComponent<Thunder>().setDirection(Thunder.ThunderDirection.RIGHT);
                t.GetComponent<Thunder>().setColor(parseColor(originColor));
                break;
            case Thunder.ThunderDirection.LEFT:
                t = Instantiate(thunder, new Vector3(transform.position.x - 1, transform.position.y, 0F), Quaternion.identity) as GameObject;
                t.GetComponent<Thunder>().setDirection(Thunder.ThunderDirection.LEFT);
                t.GetComponent<Thunder>().setColor(parseColor(originColor));
                break;
        }
    }

    public void passThunderChangingColorWithoutChangingDirection()
    {
        Debug.Log("Changing");
        GameObject t;
        switch (originDirection)
        {
            case Thunder.ThunderDirection.UP:
                t = Instantiate(thunder, new Vector3(transform.position.x, transform.position.y + 1, 0F), Quaternion.identity) as GameObject;
                t.GetComponent<Thunder>().setDirection(Thunder.ThunderDirection.UP);
                t.GetComponent<Thunder>().setColor(parseColor(outputColor));
                break;
            case Thunder.ThunderDirection.DOWN:
                t = Instantiate(thunder, new Vector3(transform.position.x, transform.position.y - 1, 0F), Quaternion.identity) as GameObject;
                t.GetComponent<Thunder>().setDirection(Thunder.ThunderDirection.DOWN);
                t.GetComponent<Thunder>().setColor(parseColor(outputColor));
                break;
            case Thunder.ThunderDirection.RIGHT:
                t = Instantiate(thunder, new Vector3(transform.position.x + 1, transform.position.y, 0F), Quaternion.identity) as GameObject;
                t.GetComponent<Thunder>().setDirection(Thunder.ThunderDirection.RIGHT);
                t.GetComponent<Thunder>().setColor(parseColor(outputColor));
                break;
            case Thunder.ThunderDirection.LEFT:
                t = Instantiate(thunder, new Vector3(transform.position.x - 1, transform.position.y, 0F), Quaternion.identity) as GameObject;
                t.GetComponent<Thunder>().setDirection(Thunder.ThunderDirection.LEFT);
                t.GetComponent<Thunder>().setColor(parseColor(outputColor));
                break;
        }
    }

    void OnMouseDown()
    {
        if (!UIButtonManager.isHammerDown)
        {
            UIColorManager.SetNextModule(gameObject);
            GameObject manager = GameObject.FindGameObjectWithTag("UIColorManager");
            UIColorManager script = manager.GetComponent<UIColorManager>();
            script.SetVisible();
        }
        if (UIButtonManager.isHammerDown)
        {
            Debug.Log("module delete");
            deactivateArrow();
            deactivateInputColor();
            Floor script = floor.GetComponent<Floor>();
            script.hasModule = false;
            gameObject.SetActive(false);
        }
    }



}
