using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedScene : MonoBehaviour
{
    /*
    var bedCamera : GameObject;
    var characterController : GameObject;
    var grover : GameObject;
    var annabeth : GameObject;
    var argus : GameObject;
    var chiron: GameObject;
    var MrD : GameObject;
    var Percy: GameObject;
    var talkSpeed : float = 2;
    private var currentSentence: String;
    var bedScene : boolean = false;
    var miniMapCam : GameObject;

    var dialog1 : GameObject;
    var dialog1T : Component;

    var dialog2 : GameObject;
    var dialog2T : Component;

    var dialog3 : GameObject;
    var dialog3T : Component;

    var dialog4 : GameObject;
    var dialog4T : Component;

    var Player: GameObject;  

    function OnGUI()
    {
        //GUI.Label(Rect(0,0,500,100),currentSentence);
    }

    function Start()
    {/*
        Player = GameObject.Find("ThirdPersonController");
        if(bedScene && (Player.GetComponent(ProgressManager).wakeUpScene.completed == false) )//If 
        {
            Scene1();
        }
        dialog1T = dialog1.GetComponent("OnEnableNarrationTrigger");
        dialog2T = dialog2.GetComponent("OnEnableNarrationTrigger");
        dialog3T = dialog3.GetComponent("OnEnableNarrationTrigger");
        dialog4T = dialog4.GetComponent("OnEnableNarrationTrigger");
    }*/

    void Update()
    {
        /*if(dialog1.active == true)
        {
            if( dialog1T.enabled )
            {
                Scene2();
            }
        }*/
        /*if(dialog2.enabled == true)
        {
            if(dialog2.gameObject.GetComponent("OnEnableNarrationTrigger").enabled == false)
            {
                Scene3();
            }
        }
        if(dialog3.enabled == true)
        {
            if(dialog3.gameObject.GetComponent("OnEnableNarrationTrigger").enabled == false)
            {
                Scene4();
            }
        }
        if(dialog4.enabled == true)
        {
            if(dialog4.gameObject.GetComponent("OnEnableNarrationTrigger").enabled == false)
            {
                Player.GetComponent(ProgressManager).wakeUpScene.completed = true;
                endScene();
            }
        }*/
    }

    void Scene1()
    {/*
        annabeth.gameObject.SetActive(true);

        characterController.gameObject.SetActive(false);
        bedCamera.gameObject.SetActive(true);
        miniMapCam.gameObject.SetActive(false);

        dialog1.gameObject.SetActive(true);
        */
        /*currentSentence = "Girl: What did you take?";
        yield WaitForSeconds(talkSpeed);
        currentSentence = "Percy: What are you talking about?";
        yield WaitForSeconds(talkSpeed);
        currentSentence = "Girl: We've only got a few weeks";
        yield WaitForSeconds(talkSpeed);
        currentSentence = "Percy: I don't know what you're talking about.......";
        yield WaitForSeconds(talkSpeed);
        currentSentence = "**Pases out**";
        */

        //-------------------------------------------------------------------------------------Passes Out
    }
    /*
    void Scene2()
    
        dialog2.gameObject.SetActive(true);

        annabeth.gameObject.SetActive(false);
        argus.gameObject.SetActive(true);
        yield WaitForSeconds(talkSpeed * 2.5);
        argus.gameObject.SetActive(false);
        grover.gameObject.SetActive(true);
        //bedCamera.gameObject.SetActive(false);
        //yield WaitForSeconds(5);
        //bedCamera.gameObject.SetActive(true);
    }*/


    /*void Scene3()
    {
        dialog3.gameObject.SetActive(true);

        
        yield WaitForSeconds(talkSpeed);
        currentSentence = "Grover: You saved me Percy";
        yield WaitForSeconds(talkSpeed);
        currentSentence = "Grover: I got this for you";
        yield WaitForSeconds(talkSpeed);
        currentSentence = "**Hands Percy Minatur Horn**";
        yield WaitForSeconds(talkSpeed);
        currentSentence = "Percy: The Minataur....";
        yield WaitForSeconds(talkSpeed);
        currentSentence = "Percy: my Mom?";
        yield WaitForSeconds(talkSpeed);
        currentSentence = "Grover: You've been out for two days, your Mom...";
        yield WaitForSeconds(talkSpeed);
        currentSentence = "Grover: She. She didnt make it...";
        yield WaitForSeconds(talkSpeed);
        currentSentence = "Grover: I'm sorry";
        yield WaitForSeconds(talkSpeed);
        currentSentence = "**Silence**";
        yield WaitForSeconds(talkSpeed);
        currentSentence = "Percy: It's not your fault";
        yield WaitForSeconds(talkSpeed);
        currentSentence = "Grover: Yes it is, I was your keeper";
        yield WaitForSeconds(talkSpeed);
        currentSentence = "Grover: Some people need to talk to you";
        
        yield WaitForSeconds(talkSpeed);
        annabeth.gameObject.SetActive(true);
        chiron.gameObject.SetActive(true);
        MrD.gameObject.SetActive(true);
    }*/

    /*void Scene4()
    {
        dialog4.gameObject.SetActive(true);

        
        currentSentence = "Percy: Mr. Bunner?";
        yield WaitForSeconds(talkSpeed);
        currentSentence = "Mr. Brunner: They call me Chiron here";
        yield WaitForSeconds(talkSpeed);
        currentSentence = "Chiron: thi is Mr. D, he is the camp director";
        yield WaitForSeconds(talkSpeed);
        currentSentence = "Mr. D: Welcome to Camp Half Blood although I'm hardly happy you're here";
        yield WaitForSeconds(talkSpeed);
        currentSentence = "Chiron: Annabeth?";
        yield WaitForSeconds(talkSpeed);
        currentSentence = "**Girl From Earlier steps forward**";
        

        annabeth.transform.Translate(Vector3.forward * Time.deltaTime);//Makes Annabeth Step Forward

        
        yield WaitForSeconds(talkSpeed);
        currentSentence = "Chiron: Annabeth here nursed you back to health";
        yield WaitForSeconds(talkSpeed);
        currentSentence = "Annabeth: You drool when you sleep";
        yield WaitForSeconds(talkSpeed);
        currentSentence = "**Annabeth leaves**";
        yield WaitForSeconds(talkSpeed);
        currentSentence = "Chiron: I'm glad you're alright Percy - I'd hate to waste the least year I've spent disguised as your teacher";
        yield WaitForSeconds(talkSpeed);
        currentSentence = "Chiron: As you may have found out by now, the Greek Myths are very real";
        yield WaitForSeconds(talkSpeed);
        currentSentence = "Chiron: You're a demigod, Half man - half God";
        yield WaitForSeconds(talkSpeed);
        currentSentence = "Chiron: Our camp is safe for you and no monsters will find you here";
        yield WaitForSeconds(talkSpeed);
        currentSentence = "Chiron: We make demigods like you into leaders, soldiers and heros";
        yield WaitForSeconds(talkSpeed);
        currentSentence = "Chiron: Now go to the Hermes Cabin";
        yield WaitForSeconds(talkSpeed * 2.5);
        
    }*/
/*
    void endScene()
    {
        bedCamera.gameObject.SetActive(false);
        characterController.gameObject.SetActive(true);
        annabeth.gameObject.SetActive(false);
        chiron.gameObject.SetActive(false);
        MrD.gameObject.SetActive(false);
        grover.gameObject.SetActive(false);
        Percy.gameObject.SetActive(false);
        miniMapCam.gameObject.SetActive(true);

        Player.transform.position = GameObject.Find("RespawnLocation").transform.position;
        Player.transform.rotation = GameObject.Find("RespawnLocation").transform.rotation;
    }*/
}