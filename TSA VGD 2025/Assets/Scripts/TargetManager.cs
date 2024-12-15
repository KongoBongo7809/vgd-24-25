using System;
using UnityEngine;
using UnityEditor;

public class TargetManager : MonoBehaviour
{
    //Array of all possible targets
    public Transform[] targets;
    //Index of current targets
    private int[] indices;

    public void Start()
    {
        indices = new int[targets.Length];
        Array.Fill(indices, 0);
    }

    //Choose a random target from the array
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

    //Get a target from its index
    public Transform GetTargetFromIndex(int index)
    {
        return targets[index];
    }

    //Add a new target to the index array
    public void AddTarget(int target, int player)
    {
        indices[target] = player;
    }

    //Remove a target from the index array
    public void RemoveTarget(int target)
    {
        indices[target] = 0;
    }
}
