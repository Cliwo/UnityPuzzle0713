using UnityEngine;
using System.Collections;

public class UIButtonManager : MonoBehaviour {

    public GameObject module;
    public GameObject UpAndDownArrow;
    public GameObject CrossArrow;

    public static bool isHammerDown=false;
    
    public void OnUpAndDownArrowDown()
    {
        isHammerDown = false;
        if (!Button.isOnGoing)
        {
            BoardManager.nextModuleObject = module;
            BoardManager.nextModuleArrow = UpAndDownArrow;
            InitCurrentModuleTableColor();
        }       
    }

    public void OnCrossArrowDown()
    {
        isHammerDown = false;
        if (!Button.isOnGoing)
        {
            BoardManager.nextModuleObject = module;
            BoardManager.nextModuleArrow = CrossArrow;
            InitCurrentModuleTableColor();
        }     
    }

    public void OnModuleDown()
    {
        isHammerDown = false;
        if (!Button.isOnGoing)
        {
            BoardManager.nextModuleObject = module;
            BoardManager.nextModuleArrow = null;
            InitCurrentModuleTableColor();
        }
    }
    public void OnHammerDown()
    {
        isHammerDown = true;
        BoardManager.nextModuleObject = null;
        BoardManager.nextModuleArrow = null;
        InitCurrentModuleTableColor();
    }

    public void InitCurrentModuleTableColor()
    {
        GameObject.FindGameObjectWithTag("CurrentModuleTable").GetComponent<CurrentModuleTable>().onNextModuleObjectOutputColorChanged(0);
        GameObject.FindGameObjectWithTag("CurrentModuleTable").GetComponent<CurrentModuleTable>().onNextModuleObjectInputColorChanged(0);
    }
}
