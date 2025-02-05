using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerManager : MonoBehaviour
{
    public Transform[] players;
    public int pointValue = 10;

    public GameObject pointerDefault;
    public TargetManager targetManager;
    public float waitTime = 3f;
    private List<Pointer> pointers = new List<Pointer>();
    private List<bool> activeCoroutine = new List<bool>();

    public void Start()
    {
        foreach (Transform plyr in players)
        {
            AddRandomPointer(plyr);
            activeCoroutine.Add(false);
        }
    }

    public void Update()
    {
        foreach (Pointer pntr in pointers)
        {
            Transform plyr = pntr.transform.root;
            //Check if player has reached target, is not moving and does not have an active coroutine
            if (pntr.HasReachedTarget() && plyr.GetComponent<Animator>().GetFloat("Speed") < 0.05 && !activeCoroutine[Array.IndexOf(players, plyr)])
            {
                StartCoroutine(Delivery(pntr, plyr));
                activeCoroutine[Array.IndexOf(players, plyr)] = true;
            }
        }
    }

    //Add a pointer to the player with a target
    public void AddPointer(int index, Transform plyr)
    {
        GameObject newPointer = Instantiate(pointerDefault, plyr);
        newPointer.GetComponent<Pointer>().SetTarget(targetManager.GetTarget(index));
        newPointer.GetComponent<Pointer>().SetPlayer(plyr);
        targetManager.AddTarget(index, Array.IndexOf(players, plyr)+1);
        pointers.Add(newPointer.GetComponent<Pointer>());
    }

    //Add a pointer to the player with a random index
    public void AddRandomPointer(Transform plyr)
    {
        AddPointer(targetManager.ChooseRandomTargetIndex(), plyr);
    }

    //Remove a pointer given the game object
    public void RemovePointer(GameObject pointer)
    {
        Pointer pntr = pointer.GetComponent<Pointer>();

        pointers.Remove(pntr);
        targetManager.RemoveTarget(pntr.GetIndex());
        Destroy(pointer);
    }

    //Initiate delivery sequence
    IEnumerator Delivery(Pointer pntr, Transform plyr)
    {
        //Create animation for delivery
        yield return new WaitForSeconds(waitTime);
        plyr.GetComponent<Player>().UpdatePoints(pointValue);
        RemovePointer(pntr.gameObject);
        AddRandomPointer(plyr);
        activeCoroutine[Array.IndexOf(players, plyr)] = false;
    }
}
