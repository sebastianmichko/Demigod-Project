using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public float buttonSpacing;
    public Texture2D titleTexture;
    public GUISkin customSkin;
    public GUIStyle layoutStyle;
    protected int WindowHeight;
    protected int WindowWidth;
    protected int frameHeight;
    protected int frameWidth;
    public float frameHeightPercent = 60;
    public float frameWidthPercent = 42;

    //logo stuff
    public bool displayLogo = true;
    public float logoHeightPercent = 22;
    public float logoWidthPercent = 55;
    protected float logoHeight;
    protected float logoWidth;
    public float logoYPercent = 0;
    protected float logoY;

    //Player Actions
    //public bool meleeBlock = false;

    public void adjust()//Requires Variables
    {
        if (WindowHeight != Screen.height || WindowWidth != Screen.width)
        {
            frameHeight = ScreenHeight(frameHeightPercent);
            frameWidth = ScreenWidth(frameWidthPercent);

            //logo stuff
            logoHeight = ScreenHeight(logoHeightPercent);
            logoWidth = ScreenWidth(logoWidthPercent);
            logoY = ScreenHeight(logoYPercent);

            WindowHeight = Screen.height;
            WindowWidth = Screen.width;

            //buttonSpacing = WindowHeight / 80;
            buttonSpacing = frameHeight / 80;
        }
    }

    public void render()
    {
        //Draws the logo
        if(displayLogo)
        {
            GUI.DrawTexture(new Rect(Screen.width / 2 - logoWidth / 2, logoY, logoWidth, logoHeight), titleTexture, ScaleMode.StretchToFill, true, 0);  
        }
        GUI.skin = customSkin;
        GUILayout.BeginArea(new Rect(Screen.width / 2 - frameWidth / 2, Screen.height / 2 - frameHeight / 2, frameWidth, frameHeight), layoutStyle);
        //GUILayout.Space(25);
    }

    public int ScreenWidth(float X)//X = Percent Width
    {
        int width = Screen.width;
        int pixelPerPercent = width / 100;
        int desiredPixels = Mathf.RoundToInt(pixelPerPercent * X);
        //Debug.Log("Width: "+width);
        //Debug.Log("Pixel Per Percent X: "+pixelPerPercent);
        //Debug.Log("Desired Pixels X: "+desiredPixels);
        return desiredPixels;
    }

    public int ScreenHeight(float Y)//Y = Percent Height
    {
        int height = Screen.height;
        int pixelPerPercent = height / 100;
        int desiredPixels = Mathf.RoundToInt(pixelPerPercent * Y);
        //Debug.Log("Height: "+height);
        //Debug.Log("Pixel Per Percent Y: "+pixelPerPercent);
        //Debug.Log("Desired Pixels Y: "+desiredPixels);
        return (desiredPixels);
    }
    /*
    public void pause()
    {
        meleeBlock = true;
        Time.timeScale = 0;
        gameObject.GetComponent<PauseMenu>().enabled = true;
    }

    public void resume()
    {
        print("Resumed");
        Time.timeScale = 1;
        gameObject.GetComponent<PauseMenu>().enabled = false;
        StartCoroutine(resumeVarChange());

    }

    public IEnumerator resumeVarChange()
    {
        print("ResumedVarCahnge");
        yield return new WaitForSeconds(0.01f);
        GetComponent<PauseController>().meleeBlock = false;
        print("melee block disabled");
    }*/
}