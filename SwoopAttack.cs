using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwoopAttack : MonoBehaviour
{
    public AudioClip swoopSound;//Staff Swing
    public float meleeATKTime = 1.0f;
    public bool waiting = false;

    public Transform damagerPrefab;
    public Quaternion damagerRotation;
    public Transform damager;


    void Start()
    {
        meleeATKTime = gameObject.GetComponent<NPCAI>().meleeATKTime;
    }

    void Update()
    {
        if (!waiting) swoop();
    }

    public void swoop()
    {
        if(!waiting)
        {
            waiting = true;
            GetComponent<Animation>().Play("Attack");
            AudioSource.PlayClipAtPoint(swoopSound, transform.position, 3);// object's Z axis
            damagerRotation.eulerAngles = new Vector3(0, gameObject.transform.rotation.eulerAngles.y, 0);
            if (damager == null) damager = Instantiate(damagerPrefab, transform.position + (transform.forward * 1.0f), damagerRotation);//good
            StartCoroutine(resumeAttack());
        }
    }

    public IEnumerator resumeAttack()
    {
        yield return new WaitForSeconds(meleeATKTime);
        waiting = false;
    }
}