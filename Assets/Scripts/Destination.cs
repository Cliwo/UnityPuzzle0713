using UnityEngine;
using System.Collections;

public class Destination : MonoBehaviour {

    private Animator animator;
	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Thunder")
        {
            animator.SetTrigger("isCharged");
        }

    }
}
