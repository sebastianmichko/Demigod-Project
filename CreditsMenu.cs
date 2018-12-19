using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsMenu : Menu
{
    private Vector2 scrollPosition;

    void OnGUI ()
    {
        adjust();
        render();

        GUILayout.Label("Credits");

        //Begins Scorllable area
        GUILayout.BeginVertical();
        //GUILayout.BeginHorizontal();
        scrollPosition = GUILayout.BeginScrollView(scrollPosition, true, true);

        GUI.Box(new Rect(0, 0, 360, 35), "3D modeling: Sebastian Michko");
        GUI.Box(new Rect(0, 40, 360, 35), "Design: Sebastian Michko");
        GUI.Box(new Rect(0, 80, 360, 35), "Testing: Sebastian, Hunter, and Tucker Michko");
        GUI.Box(new Rect(0, 120, 360, 35), "Programming: Sebastian and Hunter Michko");
        GUI.Box(new Rect(0, 160, 360, 35), "Menu Graphics: Nercomance GUI by Enemy Giant");
        GUI.Box(new Rect(0, 200, 360, 35), "'Prologue' Song: Telaron");
        GUI.Box(new Rect(0, 240, 360, 35), "'Soft Mysterious Harp' Song: Jordy Hake");
        GUI.Box(new Rect(0, 280, 360, 35), "'Invasion of Chaos' Song: Alexandr Zhelanov");

        //Closes the scroll box
        GUILayout.EndVertical();
        GUILayout.EndScrollView();

        //Exit and Return 
        if (GUILayout.Button("Return") || Input.GetKeyUp(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().buildIndex == 0)//doesnt like this
            {
                gameObject.GetComponent<MainMenu>().enabled = true;
            }
            else// if(SceneManager.GetActiveScene().buildIndex != 0)//this either
            {
                gameObject.GetComponent<PauseMenu>().enabled = true;
            }
            gameObject.GetComponent<CreditsMenu>().enabled = false;
        }

        GUILayout.EndArea();
    }
}