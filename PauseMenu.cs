using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class PauseMenu : Menu
{
    public DataController dataController;//Should be private

    void OnGUI()
    {
        dataController = FindObjectOfType<DataController>();

        adjust();
        render();

        //Window Label
        GUILayout.Label("Pause Menu");

        //Resume Game Button
        //GUILayout.Space(buttonSpacing);
        if (GUILayout.Button("Resume Game") && Time.timeScale == 0)
        {
            resume();
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

        //Objectives Menu Button
        //GUILayout.Space(buttonSpacing);
        if (GUILayout.Button("Objectives"))
        {
            gameObject.GetComponent<PauseMenu>().enabled = false;
            gameObject.GetComponent<ObjectiveMenu>().enabled = true;
        }

        //Settings Menu Button
        //GUILayout.Space(buttonSpacing);
        if (GUILayout.Button("Settings"))
        {
            gameObject.GetComponent<PauseMenu>().enabled = false;
            gameObject.GetComponent<SettingsMenu>().enabled = true;
        }

        //Credits Menu Button
        //GUILayout.Space(buttonSpacing);
        if (GUILayout.Button("Credits"))
        {
            gameObject.GetComponent<PauseMenu>().enabled = false;
            gameObject.GetComponent<CreditsMenu>().enabled = true;
        }

        //Save Button
        GUILayout.Space(buttonSpacing);
        if (GUILayout.Button("Save Game"))
        {
            dataController.SaveGameData();
        }

        //Main Menu Button
        //GUILayout.Space(buttonSpacing);
        if (GUILayout.Button("Main Menu"))
        {
            SceneManager.LoadScene("MainMenu");
            Time.timeScale = 1;
        }

        //Exit to Desktop Button
        //GUILayout.Space(buttonSpacing);
        if (GUILayout.Button("Exit to Desktop"))
        {
            Application.Quit();
        }

        //GUILayout.Space(buttonSpacing);
        //GUILayout.Space(buttonSpacing);
        //GUILayout.Space(buttonSpacing);
        //GUILayout.Space(buttonSpacing);
        GUILayout.Label("                      The Demigod Project Version 0.03", "PlainText");
        //GUILayout.Space(buttonSpacing);
        GUILayout.EndArea();
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