using System.Collections;
using UnityEngine;

public class PointerManager : MonoBehaviour
{
    public GameObject pointerDefault;
    public int playerId;
    public TargetManager targetManager;
    public float waitTime = 3f;
    private Pointer[] pointers;

    public void Start()
    {
        pointers = GetComponentsInChildren<Pointer>();
        foreach (Pointer p in pointers)
            StartCoroutine(Delivery(p));
    }

    //Add a pointer to the player with a target
    public void AddPointer(int index)
    {
        GameObject newPointer = Instantiate(pointerDefault, transform);
        newPointer.GetComponent<Pointer>().target = targetManager.GetTargetFromIndex(index);
        newPointer.GetComponent<Pointer>().player = transform;
        targetManager.AddTarget(index, playerId);
    }

    //Add a pointer to the player with a random index
    public void AddRandomPointer()
    {
        AddPointer(targetManager.ChooseRandomTargetIndex());
    }

    //Remove a pointer given the game object
    public void RemovePointer(GameObject pointer)
    {
        targetManager.ClearTarget(pointer.GetComponent<Pointer>().index);
        Destroy(pointer);
    }

    //Initiate delivery sequence
    IEnumerator Delivery(Pointer p)
    {
        Debug.Log($"Start coroutine for {playerId}");
        if (p.reachedTarget && transform.GetComponent<Animator>().GetFloat("Speed") < 0.05)
        {
            Debug.Log("Reached target");
            //Create animation for delivery
            yield return new WaitForSeconds(waitTime);
            RemovePointer(p.gameObject);
            AddRandomPointer();
        }  
    }
}
