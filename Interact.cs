using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Narrate;

public class Interact : MonoBehaviour
{
    public Texture2D interactLabel;//the Press E to Interact Label

    //Distance Stuff
    public bool close = false;
    public bool fresh = true;

    //Settings
    public MonoBehaviour enableScript;
    public bool deactAfter = true;//True deactivates after leaving area, False does not deactive afterward
    public bool activateNaration = false;
    public bool replayableNaration = true;
    public OnEnableNarrationTrigger naration;

    void OnGUI()
    {
        if (close && fresh)
        {
            GUI.Label(new Rect(0, 0, interactLabel.width, interactLabel.height), interactLabel);
        }
    }

    void Start()
    {
        if (activateNaration) naration = gameObject.GetComponent<OnEnableNarrationTrigger>();
    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.name == "ThirdPersonController")
        {
            close = true;
            if (Input.GetKeyUp("e"))
            {
                fresh = false;
                enableScript.enabled = true;
                if(activateNaration) naration.enabled = true; 
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.name == "ThirdPersonController")
        {
            close = false;
            fresh = true;
            if (deactAfter == true) enableScript.enabled = false;
            if (replayableNaration)
            {
                print("Disable Naration");
                naration.enabled = false;
            }
        }
    }
}