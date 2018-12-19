using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CombatArenaMenu : Menu
{
    public int numCD = 0;
    public int numMyrmeke = 0;
    public int numMinotaur = 0;
    public int numMedusas = 0;
    public int numChimeras = 0;

    private void Start()
    {
        frameHeightPercent = 50;
        frameWidthPercent = 30;
        displayLogo = false;
    }

    void OnGUI()
    {
        pause();
        //Time.timeScale = 0;

        adjust();
        render();    

        //Window Label
        GUILayout.Label("Combat Arena");

        GUILayout.BeginVertical();//---------

        GUILayout.Label("Combatants", "PlainText");

        //Demigods
        GUILayout.BeginHorizontal();//---------
        GUILayout.Label("Demigods", "PlainText");
        GUILayout.Label("" + numCD, "PlainText");
        GUILayout.Space(buttonSpacing);
        numCD = Mathf.RoundToInt(GUILayout.HorizontalSlider(numCD, 0, 5));
        GUILayout.EndHorizontal();//----------

        //Myrmekes
        GUILayout.BeginHorizontal();//---------
        GUILayout.Label("Myrmekes", "PlainText");
        GUILayout.Label("" + numMyrmeke, "PlainText");
        GUILayout.Space(buttonSpacing);
        numMyrmeke = Mathf.RoundToInt(GUILayout.HorizontalSlider(numMyrmeke, 0, 5));
        GUILayout.EndHorizontal();//----------

        //Minotaurs
        GUILayout.BeginHorizontal();//---------
        GUILayout.Label("Minotaurs", "PlainText");
        GUILayout.Label("" + numMinotaur, "PlainText");
        GUILayout.Space(buttonSpacing);
        numMinotaur = Mathf.RoundToInt(GUILayout.HorizontalSlider(numMinotaur, 0, 3));
        GUILayout.EndHorizontal();//----------

        //Medusas
        GUILayout.BeginHorizontal();//---------
        GUILayout.Label("Medusa", "PlainText");
        GUILayout.Label("" + numMedusas, "PlainText");
        GUILayout.Space(buttonSpacing);
        numMedusas = Mathf.RoundToInt(GUILayout.HorizontalSlider(numMedusas, 0, 1));
        GUILayout.EndHorizontal();//----------

        //Chimeras
        GUILayout.BeginHorizontal();//---------
        GUILayout.Label("Chimera", "PlainText");
        GUILayout.Label("" + numChimeras, "PlainText");
        GUILayout.Space(buttonSpacing);
        numChimeras = Mathf.RoundToInt(GUILayout.HorizontalSlider(numChimeras, 0, 1));
        GUILayout.EndHorizontal();//----------

        GUILayout.Space(buttonSpacing * 2);
        GUILayout.BeginHorizontal();//---------

        //Fight Button
        GUILayout.Space(buttonSpacing);
        if (GUILayout.Button("Fight"))
        {
            if (numCD > 0 || numMyrmeke > 0 || numMinotaur > 0 || numMedusas > 0 || numChimeras > 0)//Only lets you press fight if you select a combatant
            {
                gameObject.GetComponent<CombatArena>().enabled = true;
                //gameObject.GetComponent<CombatArenaMenu>().enabled = false;
                //Time.timeScale = 1;
                resume();
            }
        }

        //Leave Button
        GUILayout.Space(buttonSpacing);
        if (GUILayout.Button("Leave"))
        {
            //gameObject.GetComponent<CombatArenaMenu>().enabled = false;
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
        gameObject.GetComponent<CombatArenaMenu>().enabled = false;
        StartCoroutine(resumeVarChange());
    }

    public IEnumerator resumeVarChange()
    {
        yield return new WaitForSeconds(0.01f);
        GameObject.Find("ThirdPersonController").GetComponent<PlayerSwordAttack>().meleeBlock = false;
    }
}