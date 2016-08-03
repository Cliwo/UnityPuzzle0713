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
        }       
    }

    public void OnCrossArrowDown()
    {
        isHammerDown = false;
        if (!Button.isOnGoing)
        {
            BoardManager.nextModuleObject = module;
            BoardManager.nextModuleArrow = CrossArrow;
        }     
    }

    public void OnModuleDown()
    {
        isHammerDown = false;
        if (!Button.isOnGoing)
        {
            BoardManager.nextModuleObject = module;
            BoardManager.nextModuleArrow = null;
        }
    }
    public void OnHammerDown()
    {
        isHammerDown = true;
        BoardManager.nextModuleObject = null;
        BoardManager.nextModuleArrow = null;
    }

}
