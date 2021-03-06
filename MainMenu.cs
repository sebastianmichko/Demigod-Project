﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class MainMenu : Menu
{

    public DataController dataController;//Should be private

    void OnGUI ()
    {
        dataController = FindObjectOfType<DataController>();

        adjust();
        render();

        //Window Label
        GUILayout.Label("Main Menu");

        //Single Player Button
        GUILayout.Space(buttonSpacing);
        if (GUILayout.Button("Single Player"))
        {
            //Needs to retrive save data.
            //LoadGameData();

            //LoadPlayerProgress();

            SceneManager.LoadScene(dataController.gameData.Scene);
        }

        //Settings Menu Button
        GUILayout.Space(buttonSpacing);
        if (GUILayout.Button("Settings"))
        {
            gameObject.GetComponent<SettingsMenu>().enabled = true;
            gameObject.GetComponent<MainMenu>().enabled = false;
        }

        //Credits Menu Button
        GUILayout.Space(buttonSpacing);
        if (GUILayout.Button("Credits"))
        {
            gameObject.GetComponent<CreditsMenu>().enabled = true;
            gameObject.GetComponent<MainMenu>().enabled = false;
        }

        //Exit to Desktop Button
        GUILayout.Space(buttonSpacing);
        if (GUILayout.Button("Exit to Desktop"))
        {
            Application.Quit();
        }

        GUILayout.Space(buttonSpacing);
        GUILayout.Space(buttonSpacing);
        GUILayout.Space(buttonSpacing);
        GUILayout.Space(buttonSpacing);
        GUILayout.Label("                      The Demigod Project Version 0.03", "PlainText");
        GUILayout.Space(buttonSpacing);
        GUILayout.EndArea();
    }
}