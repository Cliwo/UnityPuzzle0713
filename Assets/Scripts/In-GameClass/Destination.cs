using UnityEngine;
using System.Collections;

public class Destination : MonoBehaviour {

    private Animator animator;
    // Use this for initialization

    public Module.Color color;
    public bool isCharged;
    void Start () {
        animator = GetComponent<Animator>();
        isCharged = false;
        InvokeRepeating("setColorAnimationPerSecond", 0, 1.0F);
    }

    public void setColorAnimationPerSecond()
    {
        if (animator == null)
            animator = GetComponent<Animator>();
        switch (Module.parseColor(color))
        {
            case 1:
                animator.SetTrigger("isRED");
                break;
            case 2:
                animator.SetTrigger("isYELLOW");
                break;
            case 3:
                animator.SetTrigger("isGREEN");
                break;
            case 4:
                animator.SetTrigger("isORANGE");
                break;
            case 0:
                animator.SetTrigger("isBLUE");
                break;
        }
    }

    public bool checkColor(Thunder thunder)
    {
        if (thunder.getColor() == color)
            return true;
        else
            return false;
    }
    public void setColor(int colorValue)
    {
        if(animator == null)
            animator = GetComponent<Animator>();
        color = Module.parseColor(colorValue);
        switch(colorValue)
        {
            case 1:
                animator.SetTrigger("isRED");
                break;
            case 2:
                animator.SetTrigger("isYELLOW");
                break;
            case 3:
                animator.SetTrigger("isGREEN");
                break;
            case 4:
                animator.SetTrigger("isORANGE");
                break;
            case 0:
                animator.SetTrigger("isBLUE");
                break;
        }
    }
    public void setColor(Module.Color color)
    {
        if (animator == null)
            animator = GetComponent<Animator>();
        this.color = color;
        int colorValue = Module.parseColor(color);
        switch (colorValue)
        {
            case 1:
                animator.SetTrigger("isRED");
                break;
            case 2:
                animator.SetTrigger("isYELLOW");
                break;
            case 3:
                animator.SetTrigger("isGREEN");
                break;
            case 4:
                animator.SetTrigger("isORANGE");
                break;
            case 0:
                animator.SetTrigger("isBLUE");
                break;
        }
    }

    public Module.Color getColor()
    {
        return color;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Thunder")
        {
            if (checkColor(other.GetComponent<Thunder>()))
            {
                animator.SetTrigger("isCharged");
                isCharged = true;
            }
        }

    }
}
