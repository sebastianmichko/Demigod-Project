using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
    public int targetScene = 1;
	void Start ()
    {
        SceneManager.LoadScene(targetScene);
    }
}
