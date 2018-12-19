using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockWallMenu : Menu
{
	void OnGUI ()
    {
        pause();

        adjust();
        render();
        //Window Label
        GUILayout.Label("Rock Wall");

        GUILayout.BeginVertical();//---------

        GUILayout.Label("Records", "PlainText");
        GUILayout.Label("1st: Luke Castelian", "PlainText");
        GUILayout.Label("2nd: Annabeth Chase", "PlainText");
        GUILayout.Label("3rd: Grover Underwood", "PlainText");

        GUILayout.Space(buttonSpacing * 2);
        GUILayout.BeginHorizontal();//---------

        //Climb Button
        GUILayout.Space(buttonSpacing);
        if (GUILayout.Button("Climb"))
        {
            gameObject.GetComponent<RockWall>().enabled = true;
            //gameObject.GetComponent<RockWallMenu>().enabled = false;
            //Time.timeScale = 1;
            play();
        }

        //Leave Button
        GUILayout.Space(buttonSpacing);
        if (GUILayout.Button("Leave"))
        {
            //gameObject.GetComponent<RockWallMenu>().enabled = false;
            //Time.timeScale = 1;
            resume();
        }

        GUILayout.EndHorizontal();//----------
        GUILayout.EndVertical();//---------

        GUILayout.Space(buttonSpacing);
        GUILayout.Space(buttonSpacing);
        GUILayout.Space(buttonSpacing);
        GUILayout.Space(buttonSpacing);
        GUILayout.Space(buttonSpacing);
        GUILayout.EndArea();
    }

    public void pause()
    {
        GameObject.Find("ThirdPersonController").GetComponent<PlayerSwordAttack>().meleeBlock = true;
        Time.timeScale = 0;
        //gameObject.GetComponent<CaptureTheFlagMenu>().enabled = true;//Not needed, because this script is called by InteractScript
    }

    public void resume()
    {
        Time.timeScale = 1;
        gameObject.GetComponent<RockWallMenu>().enabled = false;
        StartCoroutine(resumeVarChange());
    }

    public void play()
    {
        Time.timeScale = 1;
        gameObject.GetComponent<RockWallMenu>().enabled = false;
    }

    public IEnumerator resumeVarChange()
    {
        yield return new WaitForSeconds(0.01f);
        GameObject.Find("ThirdPersonController").GetComponent<PlayerSwordAttack>().meleeBlock = false;
    }
}