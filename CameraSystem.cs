using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    public Camera cam;
    private float d;
    public float zoomRadius;//Default value is ?
    public float zoomMin = 2.5f;
    public float zoomMax = 8.0f;
    public Transform Player;
    public float defaultY = 2.9f;
    public float defaultZ = 5.3f;
    public Transform camPivet;

    public Vector2 mouseLoc;

    public float turnSpeed = 10.0f;
    public float mouseTurnMultiplier = 1;

    private float x;

    void Update ()
    {
        //d = Input.GetAxis("Mouse ScrollWheel");
        //Scroll Wheel Controls
        if (Time.timeScale == 1)
        {
                if(d > 0)
                {
                    if(zoomRadius > zoomMin) cam.transform.Translate(0, 0, 2*d);
                }
                else if(d < 0)
                {
                    if (zoomRadius < zoomMax) cam.transform.Translate(0, 0, 2*d);
                }
            lookAt(Player);
        }
        //if (Input.GetKey("y") && (zoomRadius > zoomMin)) cam.transform.Translate(0,0, 0.1f);
        //if (Input.GetKey("t") && (zoomRadius < zoomMax)) cam.transform.Translate(0, 0, -0.1f);
        zoomRadius = Vector3.Distance(Player.position,cam.transform.position);

        //if(Input.GetMouseButtonUp(1)) mouseLoc = Input.mousePosition;
        /*if (Input.GetMouseButton(1))
        {
            //print(Input.mousePosition.x);
            //camPivet.Rotate(0, (Input.mousePosition.x - mouseLoc.x)/1000, 0);
            //if (Input.GetKey("g")) camPivet.Rotate(0, 3, 0);
            ///if (Input.GetKey("h")) camPivet.Rotate(0, -3, 0); 
        }*/
        //if (Input.GetKeyDown("r")) returnCenter();

        if (Input.GetMouseButton(1))
        {
            // Get the difference in horizontal mouse movement
            x = Input.GetAxis("Mouse X") * turnSpeed * mouseTurnMultiplier;
        }

        // rotate the character based on the x value
        Player.transform.Rotate(0, x, 0);
    }

    
    public void lookAt(Transform target)
    {
        Vector3 targetPostition = new Vector3(target.position.x, target.position.y + 1.2f, target.position.z);
        this.transform.LookAt(targetPostition);
    }

    public void returnCenter()
    {
        camPivet.rotation = Quaternion.Euler(0, Player.transform.eulerAngles.y, 0);//works
    }
}