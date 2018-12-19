using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeAttack : MonoBehaviour
{
    public Transform snakePrefab;
    private Quaternion snakeRotation;
    private Transform snake;
    public float meleeATKTime = 1.0f;
    public bool waiting = false;

    private int rot;

    public AudioClip sound; //Snake Lunge soundClip

    public string Name;

    private ChimeraAI ChimeraScript;

    void Start()
    {
        Name = gameObject.name + "Snake(Clone)";
        meleeATKTime = gameObject.GetComponent<NPCAI>().meleeATKTime;
    }

    void Update()
    {
        if(!waiting) Bite();
    }

    public void Bite()
    {
        if (Time.timeScale == 1)
        {
            if (GameObject.Find(Name) == null)//If no swords are instantiated then instantiate one.
            {
                AudioSource.PlayClipAtPoint(sound, transform.position, 0.25f);// object's Z axis
                snakeRotation.eulerAngles = new Vector3(0, transform.rotation.eulerAngles.y, 0);
                snake = Instantiate(snakePrefab, transform.position + (transform.up * 0.5f) + (transform.forward * 0.0f), snakeRotation);//good
                snake.name = Name;
                //snake.transform.SetParent(transform);//Glitches, because the damager is a trigger and it activates OnTriggerExit in ChimeraAI and removes the target from the list
                waiting = true;
                StartCoroutine(resumeAttack());
            }
        }
    }

    public IEnumerator resumeAttack()
    {
        yield return new WaitForSeconds(meleeATKTime);
        waiting = false;
    }
}