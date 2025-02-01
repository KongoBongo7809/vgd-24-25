using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerManager : MonoBehaviour
{
    public GameObject pointerDefault;
    public int playerId;
    public TargetManager targetManager;
    public float waitTime = 3f;
    private List<Pointer> pointers = new List<Pointer>();
    private bool activeCoroutine = false;

    public void Start()
    {
        AddRandomPointer();
        /*Pointer[] startPointers = transform.GetComponentsInChildren<Pointer>();
        foreach (Pointer p in startPointers)
        {
            pointers.Add(p);
        }
        foreach (Pointer p in transform.GetComponentsInChildren<Pointer>())
        {
            pointers.Add(p);
        }*/
    }

    public void Update()
    {
        foreach (Pointer p in pointers)
        {
            //Check if player has reached target, is not moving and does not have an active coroutine
            if (p.HasReachedTarget() && transform.GetComponent<Animator>().GetFloat("Speed") < 0.05 && !activeCoroutine)
            {
                StartCoroutine(Delivery(p));
                activeCoroutine = true;
            }
        }
    }

    //Add a pointer to the player with a target
    public void AddPointer(int index)
    {
        GameObject newPointer = Instantiate(pointerDefault, transform);
        newPointer.GetComponent<Pointer>().SetTarget(targetManager.GetTarget(index));
        newPointer.GetComponent<Pointer>().SetPlayer(transform);
        targetManager.AddTarget(index, playerId);
        pointers.Add(newPointer.GetComponent<Pointer>());
    }

    //Add a pointer to the player with a random index
    public void AddRandomPointer()
    {
        AddPointer(targetManager.ChooseRandomTargetIndex());
    }

    //Remove a pointer given the game object
    public void RemovePointer(GameObject pointer)
    {
        Pointer p = pointer.GetComponent<Pointer>();

        pointers.Remove(p);
        targetManager.ClearTarget(p.GetIndex());
        Destroy(pointer);
    }

    //Initiate delivery sequence
    IEnumerator Delivery(Pointer p)
    {
        Debug.Log($"Start coroutine for {playerId}");

        Debug.Log("Reached target");
        //Create animation for delivery
        yield return new WaitForSeconds(waitTime);
        Debug.Log("Finished sequence");
        RemovePointer(p.gameObject);
        Debug.Log("Removed current pointer");
        AddRandomPointer();
        Debug.Log("Added new pointer");
        activeCoroutine = false;
        Debug.Log("Ended coroutine");
    }
}
