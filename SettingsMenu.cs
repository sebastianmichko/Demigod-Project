using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsMenu : Menu
{
    private Vector2 scrollPosition;

	void OnGUI ()
    {
        adjust();
        render();

        GUILayout.Label("Settings");

        GUILayout.BeginVertical();
        scrollPosition = GUILayout.BeginScrollView(scrollPosition, true, true);
        GUILayout.BeginHorizontal();
        GUILayout.BeginVertical();
        GUILayout.Label("Action");
        GUILayout.Space(buttonSpacing);
        GUILayout.Label("Move Forward", "PlainText");
        GUILayout.Space(buttonSpacing);
        GUILayout.Label("Turn Left", "PlainText");
        GUILayout.Space(buttonSpacing);
        GUILayout.Label("Turn Right", "PlainText");
        GUILayout.Space(buttonSpacing);
        GUILayout.Label("Pause/Back", "PlainText");
        GUILayout.Space(buttonSpacing);
        GUILayout.Label("Jump", "PlainText");
        GUILayout.Space(buttonSpacing);
        GUILayout.Label("Interact", "PlainText");
        GUILayout.Space(buttonSpacing);
        GUILayout.Label("Attack", "PlainText");
        GUILayout.Space(buttonSpacing);
        GUILayout.Label("Quit MiniGames", "PlainText");
        GUILayout.Space(buttonSpacing);
        GUILayout.Label("Dive(Swimming)", "PlainText");
        GUILayout.Space(buttonSpacing);
        GUILayout.Label("Surface(Swimming)", "PlainText");
        GUILayout.Space(buttonSpacing);
        GUILayout.Label("Cheats(on/off)", "PlainText");

        GUILayout.EndVertical();

        GUILayout.Space(buttonSpacing);
        GUILayout.BeginVertical();
        GUILayout.Label("Control");
        GUILayout.Space(buttonSpacing);
        GUILayout.Label("W", "PlainText");
        GUILayout.Space(buttonSpacing);
        GUILayout.Label("A", "PlainText");
        GUILayout.Space(buttonSpacing);
        GUILayout.Label("D", "PlainText");
        GUILayout.Space(buttonSpacing);
        GUILayout.Label("ESC", "PlainText");
        GUILayout.Space(buttonSpacing);
        GUILayout.Label("Space", "PlainText");
        GUILayout.Space(buttonSpacing);
        GUILayout.Label("E", "PlainText");
        GUILayout.Space(buttonSpacing);
        GUILayout.Label("Mouse 1", "PlainText");
        GUILayout.Space(buttonSpacing);
        GUILayout.Label("Y", "PlainText");
        GUILayout.Space(buttonSpacing);
        GUILayout.Label("E", "PlainText");
        GUILayout.Space(buttonSpacing);
        GUILayout.Label("Q", "PlainText");
        GUILayout.Space(buttonSpacing);
        GUILayout.Label("~", "PlainText");

        GUILayout.EndVertical();
        GUILayout.EndHorizontal();
        GUILayout.EndScrollView();//closes the scroll box
        GUILayout.EndVertical();

        /*
        GUILayout.Space(buttonSpacing*2);
        GUILayout.BeginVertical();
        scrollPosition = GUILayout.BeginScrollView(scrollPosition, true, true);
        GUILayout.Label ("Control Settings:", "PlainText");

        GUILayout.Space(buttonSpacing);
        GUILayout.Label ("Move Forward: W", "PlainText");
        GUILayout.Space(buttonSpacing);
        GUILayout.Label ("Turn Left: A", "PlainText");
        GUILayout.Space(buttonSpacing);
        GUILayout.Label ("Turn Right: D", "PlainText");
        GUILayout.Space(buttonSpacing);
        GUILayout.Label ("Pause/Back: ESC", "PlainText");
        GUILayout.Space(buttonSpacing);
        GUILayout.Label ("Jump: Space", "PlainText");
        GUILayout.Space(buttonSpacing);
        GUILayout.Label ("Interact: E", "PlainText");
        GUILayout.Space(buttonSpacing);
        GUILayout.Label ("Attack: Mouse 1", "PlainText");
        GUILayout.Space(buttonSpacing);
        GUILayout.Label ("Quit Capture the Flag: Y", "PlainText");
        GUILayout.Space(buttonSpacing);
        GUILayout.Label ("Dive(Swimming): E", "PlainText");
        GUILayout.Space(buttonSpacing);
        GUILayout.Label ("Surface(Swimming): Q", "PlainText");
        */
        /*
        //Toggle Controls
        GUILayout.Space(buttonSpacing);
        showFPS = GUILayout.Toggle(showFPS, "Show FPS");

        GUILayout.Space(buttonSpacing);
        playMusic = GUILayout.Toggle(playMusic, "Play Music");

        GUILayout.Space(buttonSpacing);
        onParticles = GUILayout.Toggle(onParticles, "Particles");

        GUILayout.Space(buttonSpacing);
        godMode = GUILayout.Toggle(godMode, "God Mode");
        *//*
        GUILayout.EndVertical();
        GUILayout.EndScrollView();//closes the scroll box
        */

        //Exit and return - Saves Changes  
        if (GUILayout.Button("Save and Return") || Input.GetKeyUp(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                gameObject.GetComponent<MainMenu>().enabled = true;
            }
            else
            {
                gameObject.GetComponent<PauseMenu>().enabled = true;
            }
            /*
                    // Create an instance of StreamWriter to write text to a file.
                    sw = new StreamWriter("Settings.txt");
                    sw.WriteLine( convertString(showFPS) );
                    sw.WriteLine( convertString(playMusic) );
                    sw.WriteLine( convertString(onParticles) );
                    sw.WriteLine( convertString(godMode) );
                    sw.Close();
                    */
            gameObject.GetComponent<SettingsMenu>().enabled = false;
        }
        GUILayout.EndArea();
    }
}