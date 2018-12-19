using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StyxRiver : MonoBehaviour
{
    public float minSpeed = 0.15f;
    public float maxSPeed = 1.0f;
    public float xMin = 50;
    public float xMax = 150;
    public float zMin = 0;
    public float zMax = 500;

    //Prefabs
    public Transform teddyBearPrefab;
    private Quaternion teddyBearRotation;
    public List<Transform> teddyBears = new List<Transform>();
    public int maxTeddyBears = 50;

    public Transform presentPrefab;
    private Quaternion presentRotation;
    public List<Transform> presents = new List<Transform>();
    public int maxPresents = 50;

    public Transform PianoPrefab;
    private Quaternion pianoRotation;
    public List<Transform> pianos = new List<Transform>();
    public int maxPianos = 10;

    void Start()
    {
        while (teddyBears.Count < maxTeddyBears)
        {
            teddyBearRotation.eulerAngles = new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));//Randomly rotateds Objects on their y axis
            Transform temp = Instantiate(teddyBearPrefab, new Vector3(Random.Range(xMin, xMax), 19f, Random.Range(zMin, zMax)), teddyBearRotation);
            temp.name = "Teddy Bear";
            teddyBears.Add(temp);
            temp.GetComponent<Rigidbody>().AddForce(transform.forward * Random.Range(minSpeed, maxSPeed) * 250);
        }
        
        while (presents.Count < maxPresents)
        {
            presentRotation.eulerAngles = new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));//Randomly rotateds Objects on their y axis
            Transform temp = Instantiate(presentPrefab, new Vector3(Random.Range(xMin, xMax), 19f, Random.Range(zMin, zMax)), presentRotation);
            temp.name = "Present";
            presents.Add(temp);
            temp.GetComponent<Rigidbody>().AddForce(transform.forward * Random.Range(minSpeed, maxSPeed) * 250);
        }
        
        while (pianos.Count < maxPianos)
        {
            pianoRotation.eulerAngles = new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));//Randomly rotateds Objects on their y axis
            Transform temp = Instantiate(PianoPrefab, new Vector3(Random.Range(xMin, xMax), 19, Random.Range(zMin, zMax)), pianoRotation);
            temp.name = "Piano";
            pianos.Add(temp);
            temp.GetComponent<Rigidbody>().AddForce(transform.forward * Random.Range(minSpeed, maxSPeed) * 250);
        }
    }

    void Update()
    {
        if (teddyBears.Count < maxTeddyBears)
        {
            teddyBearRotation.eulerAngles = new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));//Randomly rotateds Objects on their y axis
            Transform temp = Instantiate(teddyBearPrefab, new Vector3(Random.Range(xMin, xMax), 19f, 0), teddyBearRotation);
            temp.name = "Teddy Bear";
            teddyBears.Add(temp);
            temp.GetComponent<Rigidbody>().AddForce(transform.forward * Random.Range(minSpeed, maxSPeed) * 250);
            //print("Added Teddy Bear");
        }

        if (presents.Count < maxPresents)
        {
            presentRotation.eulerAngles = new Vector3(0, Random.Range(0, 360), 0);//Randomly rotateds Objects on their y axis
            Transform temp = Instantiate(presentPrefab, new Vector3(Random.Range(xMin, xMax), 19f, 0), presentRotation);
            temp.name = "Present";
            presents.Add(temp);
            temp.GetComponent<Rigidbody>().AddForce(transform.forward * Random.Range(minSpeed, maxSPeed) * 250);
            //print("Added Present");
        }

        if (pianos.Count < maxPianos)
        {
            pianoRotation.eulerAngles = new Vector3(0, Random.Range(0, 360), 0);//Randomly rotateds Objects on their y axis
            Transform temp = Instantiate(PianoPrefab, new Vector3(Random.Range(xMin, xMax), 19, 0), pianoRotation);
            temp.name = "Piano";
            pianos.Add(temp);
            temp.GetComponent<Rigidbody>().AddForce(transform.forward * Random.Range(minSpeed, maxSPeed) * 250);
            //print("Added Piano");
        }
    }
}