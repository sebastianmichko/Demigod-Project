using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetrifyAttack : MonoBehaviour {

    public AudioClip earthSound;//Petrify Warning Sound
    public AudioClip stoneSound;//Stone Spawn Sound
    public Transform stone;//Prefab
    public Quaternion stoneRot;
    public MedusaAI MudusaScript;
    public Vector3 pos;
    public Transform curStone;
    public int positionDelay = 2;
    public float rangedATKTime = 1.0f;
    public bool waiting = false;

    void Start ()
    {
        stoneRot.eulerAngles = new Vector3(0, 0, 0);
        MudusaScript = gameObject.GetComponent<MedusaAI>();
        rangedATKTime = gameObject.GetComponent<NPCAI>().rangedATKTime;
    }

    //Needs to take position, wait a second then spawn stone, do player has a chance to avoid getting damaged.
    void Update ()
    {
        if (!waiting) StartCoroutine(Attack());
    }
            
    void updatePosition()
    {
        if(MudusaScript.target != null)
        {
            if (MudusaScript.target.Obj != null)
            {
                GameObject target = MudusaScript.target.Obj;
                pos = target.transform.position;
            }
        }
    }

    public IEnumerator Attack()
    {
        waiting = true;
        updatePosition();
        AudioSource.PlayClipAtPoint(earthSound, pos, 3);// object's Z axis
        yield return new WaitForSeconds(3);
        Stone();

    }

    public void Stone()
    {
        if (!GameObject.Find("MedusaStone 1(Clone)") && MudusaScript.target.Obj)
        {
            curStone = Instantiate(stone, pos, stoneRot);
            AudioSource.PlayClipAtPoint(stoneSound, pos, 1);// object's Z axis
        }
        waiting = true;
        StartCoroutine(resumeAttack());

    }

    public IEnumerator resumeAttack()
    {
        yield return new WaitForSeconds(rangedATKTime);
        waiting = false;
    }
}