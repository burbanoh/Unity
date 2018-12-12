using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MecanimController : MonoBehaviour {

    public Animator animController;
    

	// Use this for initialization
	void Start () {

        animController = GetComponent<Animator>();
		
	}
	
	// Update is called once per frame
	void Update () {

        if(Input.anyKey)
        {
            animController.SetBool("walk", true);
           
        }
        else
        {
            animController.SetBool("walk", false);
        }


    }
}
