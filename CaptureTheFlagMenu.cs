using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CaptureTheFlagMenu : Menu
{
    public int numPerTeam = 15;//# Characters per team
    public int numMonsterSpawns = 0;//# of random Ambient Monster Spawns
    public bool side = true;//True is CLose Side - False is far side

    private void Start()
    {
        frameHeightPercent = 40;
        frameWidthPercent = 30;
        displayLogo = false;
    }

    void OnGUI()
    {
        //Pause Stuff
        pause();
        //Time.timeScale = 0;

        adjust();
        render();

        //Window Label
        GUILayout.Label("Capture The Flag");

        GUILayout.BeginVertical();//---------

        GUILayout.Label("Combatants", "PlainText");

        //Numer of Players
        GUILayout.BeginHorizontal();//---------
        GUILayout.Label("Players Per Team", "PlainText");
        GUILayout.Label("" + numPerTeam, "PlainText");
        GUILayout.Space(buttonSpacing);
        numPerTeam = Mathf.RoundToInt( GUILayout.HorizontalSlider(numPerTeam, 5, 50) );
        GUILayout.EndHorizontal();//----------

        //Numer of Monster Spawns
        GUILayout.BeginHorizontal();//---------
        GUILayout.Label("Ambient Monster Spawns", "PlainText");
        GUILayout.Label("" + numMonsterSpawns, "PlainText");
        GUILayout.Space(buttonSpacing);
        numMonsterSpawns = Mathf.RoundToInt( GUILayout.HorizontalSlider(numMonsterSpawns, 0, 10) );
        GUILayout.EndHorizontal();//----------


        GUILayout.Label("Team Selection", "PlainText");
        GUILayout.BeginHorizontal();//---------

        //Team Selection
        GUILayout.Space(buttonSpacing);
        side = GUILayout.Toggle(side, "Red Team - Close Side");
        side = !GUILayout.Toggle(!side, "Blue Team - Far Side");
        GUILayout.EndHorizontal();//----------

        GUILayout.Space(buttonSpacing * 2);
        GUILayout.BeginHorizontal();//---------

        //Fight Button
        GUILayout.Space(buttonSpacing);
        if (GUILayout.Button("Fight"))
        {
            if (numPerTeam >= 5)//Only lets you press fight if you select atleast 5 combatants
            {
                GameObject.Find("Capture the Flag").GetComponent<CaptureTheFlag>().enabled = true;
                //gameObject.GetComponent<CaptureTheFlagMenu>().enabled = false;
                //Time.timeScale = 1;
                resume();
            }
        }

        //Leave Button
        GUILayout.Space(buttonSpacing);
        if (GUILayout.Button("Leave"))
        {
            //gameObject.GetComponent<CaptureTheFlagMenu>().enabled = false;
            //Time.timeScale = 1;
            resume();
        }

        GUILayout.EndHorizontal();//----------
        GUILayout.EndVertical();//---------
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
        gameObject.GetComponent<CaptureTheFlagMenu>().enabled = false;
        StartCoroutine(resumeVarChange());
    }

    public IEnumerator resumeVarChange()
    {
        yield return new WaitForSeconds(0.01f);
        GameObject.Find("ThirdPersonController").GetComponent<PlayerSwordAttack>().meleeBlock = false;
    }
}