using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedusaStoneScript : MonoBehaviour
{
    public float time = 3.0f;

    void Start ()
    {
        StartCoroutine(reTag());
        Destroy(gameObject, time);
    }

    public IEnumerator reTag()
    {
        yield return new WaitForSeconds(0.01f);
        gameObject.tag = "Untagged";
    }
}