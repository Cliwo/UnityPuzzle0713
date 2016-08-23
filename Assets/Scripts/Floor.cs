using UnityEngine;
using System.Collections;

public class Floor : MonoBehaviour {

    GameObject module;
    bool isExpanded;
    public bool hasModule;
    // Use this for initialization
    void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    
    void OnMouseEnter()
    {
        //모듈이 있고 망치가 다운이 아니면 이거 제외
        //모듈이 있는데, 망치가 다운이면 이거 가능
        if (UIButtonManager.isHammerDown || hasModule)
            return;
        if (!isExpanded)
        {
            GetComponent<SpriteRenderer>().sortingOrder = 1;
            transform.localScale += new Vector3(1, 1, 1);
            isExpanded = true;
        }

    }

    void OnMouseExit()
    {
        if (UIButtonManager.isHammerDown || hasModule)
            return;
        if (isExpanded)
        {
            GetComponent<SpriteRenderer>().sortingOrder = 0;
            transform.localScale += new Vector3(-1, -1, 0);
            isExpanded = false;
        }

    }

    void OnMouseDown()
    {
        if(hasModule && UIButtonManager.isHammerDown)
        {
            Debug.Log("module delete");
            module.GetComponent<Module>().deactivateArrow();
            module.GetComponent<Module>().deactivateInputColor();
            Destroy(module);
            hasModule = false;
        }
        else if(hasModule && !UIButtonManager.isHammerDown)
        {
            openColorSetting(module);
        }
        else if (BoardManager.nextModuleObject != null && !Button.isOnGoing && !hasModule)
        {
            module = Instantiate(BoardManager.nextModuleObject, new Vector3(transform.position.x, transform.position.y, 0F), Quaternion.identity) as GameObject;
            hasModule = true;
            Module script = module.GetComponent<Module>();
            script.floor = gameObject; //module과 floor연결.
            if (BoardManager.nextModuleArrow !=null)
                script.setActionArrow(BoardManager.nextModuleArrow);
            GetComponent<SpriteRenderer>().sortingOrder = 0;
            openColorSetting(module);

            transform.localScale += new Vector3(-1, -1, 0);
            isExpanded = false;
        }
        OnMouseExit();
    }

    public void openColorSetting(GameObject module)
    {
        UIColorManager.SetNextModule(module);
        GameObject manager = GameObject.FindGameObjectWithTag("UIColorManager");
        UIColorManager script = manager.GetComponent<UIColorManager>();
        script.SetVisible();
    }

    public void closeColorSetting()
    {
        GameObject manager = GameObject.FindGameObjectWithTag("UIColorManager");
        UIColorManager script = manager.GetComponent<UIColorManager>();
        script.SetInvisible();
    }
}
