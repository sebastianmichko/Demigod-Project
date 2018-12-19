using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalTeleport : MonoBehaviour
{
    public GameObject Player;
    public Vector3 TeleportTo;
    public float yRot;
    public AudioSource bossMusic;
    public AudioSource elevatorMusic;
    public float delay;

    private void OnEnable()
    {
        elevatorMusic.Play();
        StartCoroutine(Teleport());
    }

    public IEnumerator Teleport()
    {
        yield return new WaitForSeconds(delay);
        Player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        Player.transform.position = TeleportTo;
        Player.transform.eulerAngles = new Vector3(0, yRot, 0);
        elevatorMusic.Stop();
        bossMusic.Play();
    }
}