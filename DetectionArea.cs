using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionArea : MonoBehaviour
{
    public GameObject root;
    private NPCAI AIScript;

    void Start()
    {
        root = gameObject.transform.parent.gameObject;
        AIScript = root.GetComponent<NPCAI>();
    }

    void OnTriggerEnter(Collider collision)
    {
        if (AIScript.compatibilityChecker(collision.gameObject, AIScript.charType))
        {
            if (collision.gameObject != AIScript.TargetFromGameObject(AIScript.targets, collision.gameObject).Obj)
            {
                global::NPCAI.Target temp = new global::NPCAI.Target(collision.gameObject, AIScript.distance(collision.gameObject));
                temp.radius = collision.gameObject.GetComponent<CapsuleCollider>().radius;
                AIScript.targets.Add(temp);
                AIScript.Agrivated = true;
            }
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (AIScript.compatibilityChecker(collision.gameObject, AIScript.charType))
        {
            AIScript.mRemove(AIScript.TargetFromGameObject(AIScript.targets, collision.gameObject));
            if (AIScript.targets.Count == 0)
            {
                AIScript.meleeAttacking = false;
                AIScript.rangedAttacking = false;
                AIScript.Agrivated = false;
            }
        }
    }
}