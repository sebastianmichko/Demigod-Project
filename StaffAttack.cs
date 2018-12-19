using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffAttack : MonoBehaviour
{
    public Transform staffPrefab;
    public Quaternion staffRotation;
    public Transform staff;

    public AudioClip staffSound;//Staff Swing
    public string Name;
    public float meleeATKTime = 1.0f;
    public bool waiting = false;

    // Use this for initialization
    void Start()
    {
        Name = gameObject.name + "Staff(Clone)";
        meleeATKTime = gameObject.GetComponent<NPCAI>().meleeATKTime;
    }

    void Update()
    {
        if (!waiting) swing();
    }

    public void swing()
    {
        if (GameObject.Find(Name) == null)
        {
            staffRotation.eulerAngles = new Vector3(90, gameObject.transform.rotation.eulerAngles.y, 0);
            staff = Instantiate(staffPrefab, transform.position + (transform.right) + (transform.up) + (transform.forward * 1.5f), staffRotation);//good
            staff.name = Name;                                                                                                         //sword.transform.SetParent(Player.transform);
            AudioSource.PlayClipAtPoint(staffSound, transform.position, 3);// object's Z axis
            waiting = true;
            StartCoroutine(resumeAttack());
        }
    }

    public IEnumerator resumeAttack()
    {
        yield return new WaitForSeconds(meleeATKTime);
        waiting = false;
    }
}