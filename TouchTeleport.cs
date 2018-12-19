using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TouchTeleport : MonoBehaviour
{
    public int targetScene;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Demigod")
        {
            SceneManager.LoadScene(targetScene);
        }
    }
}
