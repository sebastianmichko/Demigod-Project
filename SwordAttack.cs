using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public Transform swordPrefab;
    private Quaternion swordRotation;
    private Transform sword;
    private int rot;
    public AudioClip sound; //Sword Swing soundClip
    private string Name;
    public float meleeATKTime = 1.0f;
    public bool waiting = false;

    //bool IW = false;
    //bool meleeAttacking = false;

    //public int whichScript = 0;//0=null, 1=ANT, 2=Demigod, 3=Minotaur, 4=Medusa, 5=player, 6=CTFCampers ...

    void Start()
    {
        Name = gameObject.name + "Sword(Clone)";
        meleeATKTime = gameObject.GetComponent<NPCAI>().meleeATKTime;
    }

    void Update()
    {
        if(!waiting) swing();
    }

    public void swing()
    {
        if (Time.timeScale == 1)
        {
            if (GameObject.Find(Name) == null)//If no swords are instantiated then instantiate one.
            {
                swordRotation.eulerAngles = new Vector3(90, transform.rotation.eulerAngles.y - 90, 0);
                sword = Instantiate(swordPrefab, transform.position + (transform.right * 1.0f) + (transform.up) + (transform.forward * 0.75f), swordRotation);//good
                sword.name = Name;
                AudioSource.PlayClipAtPoint(sound, transform.position, 1);// object's Z axis
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