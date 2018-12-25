using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{

    public Transform arrowPrefab;
    public Transform Camera;
	// Use this for initialization
	void Start ()
    {
		
	}

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonUp(0))
        {
            Transform temp = Instantiate(arrowPrefab, transform.position + 3*transform.forward, transform.rotation);
        }
    }
}