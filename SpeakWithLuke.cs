﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakWithLuke : MonoBehaviour
{
	void Start ()
    {
        GameObject.Find("ThirdPersonController").GetComponent<ProgressManager>().GetObjectiveFromString("spokeToLuke").completed = true;
    }
}