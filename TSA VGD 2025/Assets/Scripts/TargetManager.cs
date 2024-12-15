using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TargetManager : MonoBehaviour
{
    public Transform[] targets;

    //Return a random target from the array
    public Transform ChooseNewTarget()
    {
        return targets[Random.Range(0, targets.Length - 1)];
    }

    public void RemoveTarget(Transform target)
    {
        ArrayUtility.Remove(ref targets, target);
    }
}
