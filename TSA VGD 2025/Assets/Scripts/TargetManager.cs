using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script purpose: manage which targets belong to which player
public class TargetManager : MonoBehaviour
{
    //Array of all possible targets
    public Transform[] targets;
    //Index of current targets
    private List<int> indices = new List<int>();

    private void Start()
    {
        for (int i = 0; i < targets.Length; i++)
        {
            indices.Add(0);
        }
    }

    //Choose a random target from the array that isn't already taken
    public int ChooseRandomTargetIndex()
    {
        int rndIndex;
        do
        {
            rndIndex = UnityEngine.Random.Range(0, targets.Length);
        }
        while (indices[rndIndex] != 0);

        return rndIndex;
    }

    //Get a target transform from its index
    public Transform GetTarget(int index)
    {
        return targets[index];
    }

    //Get an index from the target
    public int GetTargetIndex(Transform target)
    {
        return Array.IndexOf(targets, target);
    }

    //Add a new target to the index array, using the target index
    public void AddTarget(int target, int player)
    {
        indices[target] = player;
    }

    //Add a new target to the index array, using the target transform
    public void AddTarget(Transform target, int player)
    {
        indices[Array.IndexOf(targets, target)] = player;
    }

    //Remove a target from the index array
    public void RemoveTarget(int target)
    {
        indices[target] = 0;
    }
}
