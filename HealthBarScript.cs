using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarScript : MonoBehaviour
{
    public Texture backgroundTexture;
    public Texture foregroundTexture;
    public Texture healingForeground;
    public Texture regularForeground;
    public Texture frameTexture;

    public int WindowHeight;
    public int WindowWidth;

    //var leftMargin: int = 0; //Automattically Set. Don't Change.
    public int topMargin = 0; //Automattically Set. Don't Change.

    public float HealthBarLocationXPercent = 0.0f; //Percent Value
    public int HealthBarLocationX = 0;//Pixel value


    public float frameWidthPercent = 1.3f;
    public float frameHeightPercent = 3.0f;

    public int frameHeight; //Automatically Set. Don't Change.
    public int frameWidth; //Automatically Set Don't Change.
    //change bar size using these variables

    //var healing : boolean = false;

    public int HealthBarFULLHeight = 0;
    public int HealthBarWidth = 0; //Automatically Set Don't Change. 

	void Start ()
    {
        WindowHeight = Screen.height;
        WindowWidth = Screen.width;

        HealthBarLocationX = ScreenWidth(HealthBarLocationXPercent);

        frameWidth = ScreenWidth(frameWidthPercent);
        frameHeight = ScreenHeight(frameHeightPercent);

        topMargin = Screen.height;

        HealthBarFULLHeight = frameHeight - 2;
        HealthBarWidth = frameWidth - 2;
    }
	
    void OnGUI()
    {
        if (WindowHeight != Screen.height || WindowWidth != Screen.width)
        {
            HealthBarLocationX = ScreenWidth(HealthBarLocationXPercent);

            frameWidth = ScreenWidth(frameWidthPercent);
            frameHeight = ScreenHeight(frameHeightPercent);

            HealthBarFULLHeight = frameHeight - 2;
            HealthBarWidth = frameWidth - 2;

            topMargin = Screen.height;

            WindowHeight = Screen.height;
            WindowWidth = Screen.width;
        }

        //Background
        GUI.DrawTexture( new Rect(HealthBarLocationX, topMargin, frameWidth, frameHeight*-1), backgroundTexture, ScaleMode.StretchToFill, true, 0);
        //Foreground
        GUI.DrawTexture( new Rect(HealthBarLocationX + 1, topMargin - 1, HealthBarWidth, (healthToPixels(GetComponent<PlayerHealth>().curHealth)) * -1), foregroundTexture, ScaleMode.StretchToFill, true, 0);
        //Frame
        //GUI.DrawTexture( Rect(leftMargin,topMargin,frameWidth,frameHeight), frameTexture, ScaleMode.ScaleToFit, true, 0 );
    }

    int healthToPixels(float health)
    {
        //int temp = Mathf.RoundToInt((health / 100.0f) * HealthBarFULLHeight);
        return Mathf.RoundToInt((health / 100.0f) * HealthBarFULLHeight);

        //return Mathf.RoundToInt((HealthBarFULLHeight / 100) * health);//GHETTO FIX********
    }

    int ScreenWidth(float X)//X = Percent Width
    {
        int width = Screen.width;
        int pixelPerPercent = width / 100;
        int desiredPixels = Mathf.RoundToInt(pixelPerPercent * X);
        //Debug.Log("Width: "+width);
        //Debug.Log("Pixel Per Percent X: "+pixelPerPercent);
        //Debug.Log("Desired Pixels X: "+desiredPixels);
        return desiredPixels;
    }

    int ScreenHeight(float Y)//Y = Percent Height
    {
        int height = Screen.height;
        int pixelPerPercent = height / 100;
        int desiredPixels = Mathf.RoundToInt(pixelPerPercent * Y);
        //Debug.Log("Height: "+height);
        //Debug.Log("Pixel Per Percent Y: "+pixelPerPercent);
        //Debug.Log("Desired Pixels Y: "+desiredPixels);
        return (desiredPixels);
    }
}