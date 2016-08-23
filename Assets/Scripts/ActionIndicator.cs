using UnityEngine;
using System.Collections;

using Assets.Scripts; // actionArrow 를 위해서

public class ActionIndicator : MonoBehaviour {

    private ActionArrow ct;

    private float coolTimeStart = 0f;
    private float coolTimeLength = 0.5f;
    //Module 이 2초에 한번씩 작동한다.

    public GameObject thunder;
    public ModuleKind Kind;
    public void execute(Module.Color color)
    {
        Debug.Log(System.Environment.Version);
        if (Time.time>coolTimeStart + coolTimeLength)
        {
            coolTimeStart = Time.time;
            Debug.Log("EXECUTE");
            Thunder.ThunderDirection[] arr = ct.generateThunder();
            GameObject t;
            for (int i = 0; i < arr.Length; i++)
            {
                switch (arr[i])
                {
                    case Thunder.ThunderDirection.UP:
                        t = Instantiate(thunder, new Vector3(transform.position.x, transform.position.y + 1, 0F), Quaternion.identity) as GameObject;
                        t.GetComponent<Thunder>().setDirection(Thunder.ThunderDirection.UP);
                        t.GetComponent<Thunder>().setColor(Module.parseColor(color));
                        break;
                    case Thunder.ThunderDirection.DOWN:
                        t = Instantiate(thunder, new Vector3(transform.position.x, transform.position.y - 1, 0F), Quaternion.identity) as GameObject;
                        t.GetComponent<Thunder>().setDirection(Thunder.ThunderDirection.DOWN);
                        t.GetComponent<Thunder>().setColor(Module.parseColor(color));
                        break;
                    case Thunder.ThunderDirection.RIGHT:
                        t = Instantiate(thunder, new Vector3(transform.position.x + 1, transform.position.y, 0F), Quaternion.identity) as GameObject;
                        t.GetComponent<Thunder>().setDirection(Thunder.ThunderDirection.RIGHT);
                        t.GetComponent<Thunder>().setColor(Module.parseColor(color));
                        break;
                    case Thunder.ThunderDirection.LEFT:
                        t = Instantiate(thunder, new Vector3(transform.position.x - 1, transform.position.y, 0F), Quaternion.identity) as GameObject;
                        t.GetComponent<Thunder>().setDirection(Thunder.ThunderDirection.LEFT);
                        t.GetComponent<Thunder>().setColor(Module.parseColor(color));
                        break;
                }
            }
        }     
    }

	// Use this for initialization
	void Start () {
        /*sample*/
        switch(Kind)
        {
            case ModuleKind.UpAndDownArrow:
                ct = new UpAndDownArrow();
                break;
            case ModuleKind.CrossArrow:
                ct = new CrossArrow();
                break;
        }     
    }
	
	// Update is called once per frame
	void Update () {
	
	}

  
}
