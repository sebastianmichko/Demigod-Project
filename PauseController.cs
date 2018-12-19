using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1 && gameObject.GetComponent<PauseMenu>().enabled == false)
            {
                pause();
            }
            else if (Time.timeScale == 0 && gameObject.GetComponent<PauseMenu>().enabled == true)//UnPause
            {
                resume();
            }
        }
    }

    public void pause()
    {
        GetComponent<PlayerSwordAttack>().meleeBlock = true;
        Time.timeScale = 0;
        gameObject.GetComponent<PauseMenu>().enabled = true;
    }

    public void resume()
    {
        //print("Resumed");
        Time.timeScale = 1;
        gameObject.GetComponent<PauseMenu>().enabled = false;
        StartCoroutine(resumeVarChange());

    }

    public IEnumerator resumeVarChange()
    {
        //print("ResumedVarCahnge");
        yield return new WaitForSeconds(0.01f);
        GetComponent<PlayerSwordAttack>().meleeBlock = false;
        //print("melee block disabled");
    }
}