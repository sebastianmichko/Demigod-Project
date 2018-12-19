using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : Menu
{
    void OnGUI()
    {
        pause();
        adjust();
        render();

        //Window Label
        GUILayout.Label("You Died");

        //Replay Button
        GUILayout.Space(buttonSpacing);
        if (GUILayout.Button("Replay"))
        { 
            replay();
        }

        //Return to Camp Button
        if (SceneManager.GetActiveScene().name != "Camp")
        {
            GUILayout.Space(buttonSpacing);
            if (GUILayout.Button("Return to Camp"))
            {
                Time.timeScale = 1;
                SceneManager.LoadScene(1);
            }
        }

        //Main Menu Button
        GUILayout.Space(buttonSpacing);
        if (GUILayout.Button("Main Menu"))
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("MainMenu");
            
        }
        //Exit to Desktop Button
        GUILayout.Space(buttonSpacing);
        if (GUILayout.Button("Exit to Desktop"))
        {
            Application.Quit();
        }
        GUILayout.EndArea();
    }

    public void pause()
    {
        GameObject.Find("ThirdPersonController").GetComponent<PlayerSwordAttack>().meleeBlock = true;
    }

    public void replay()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        StartCoroutine(resumeVarChange());
    }

    public IEnumerator resumeVarChange()
    {
        yield return new WaitForSeconds(0.01f);
        GameObject.Find("ThirdPersonController").GetComponent<PlayerSwordAttack>().meleeBlock = false;
    }
}