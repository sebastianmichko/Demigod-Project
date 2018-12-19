using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharonFerry : MonoBehaviour
{
    public Vector3 finishPosition;
    public Vector3 startPosition;
    public float speed;
    private float startTime;
    private float journeyLength;

    // Use this for initialization
    void Start ()
    {
        startTime = Time.time;
        journeyLength = Vector3.Distance(startPosition, finishPosition);
        startPosition = gameObject.transform.position;
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        float distCovered = (Time.time - startTime) * speed;

        // Fraction of journey completed = current distance divided by total distance.
        float fracJourney = distCovered / journeyLength;

        // Set our position as a fraction of the distance between the markers.
        transform.position = Vector3.Lerp(startPosition, finishPosition, fracJourney);
    }
}