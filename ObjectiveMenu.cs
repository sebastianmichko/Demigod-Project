using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectiveMenu : Menu
{
    private Vector2 scrollPosition;

    void OnGUI ()
    {
        adjust();
        render();

        ProgressManager progressManager = GetComponent<ProgressManager>();

        GUILayout.Label("Objectives");

        //Begins Scorllable area
        GUILayout.BeginVertical();
        //GUILayout.BeginHorizontal();
        scrollPosition = GUILayout.BeginScrollView(scrollPosition, true, true);

        //Display objectives.  Render a line through the objectives when they have been completed.

        //For loop to display all of the unfinished objetives.

        //GUI.Box(new Rect(0, 0, 360, 40), "3D modeling: Sebastian Michko");
        int j = 0;
        for (var i = 0; i < progressManager.objectives.Count; ++i)
        {
            if (!progressManager.objectives[i].completed)//Needs to make it do the objectives move up so they don't stay at the bottom.
            {
                GUI.Box(new Rect(50, 45 * j, 360, 40), progressManager.objectives[i].description);//Only displays the unfinished ones.
                j++;
            }
        }

        //Closes the scroll box
        GUILayout.EndVertical();
        GUILayout.EndScrollView();

        //Exit and Return 
        if (GUILayout.Button("Return") || Input.GetKeyUp(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().buildIndex == 0)//doesnt like this - Fixed need to make sure in Build Settings that the scences match up to these numbers
            {
                gameObject.GetComponent<MainMenu>().enabled = true;
            }
            else//(SceneManager.GetActiveScene().buildIndex != 0)this either
            {
                gameObject.GetComponent<PauseMenu>().enabled = true;
            }
            gameObject.GetComponent<ObjectiveMenu>().enabled = false;
        }
        GUILayout.EndArea();
    }
}