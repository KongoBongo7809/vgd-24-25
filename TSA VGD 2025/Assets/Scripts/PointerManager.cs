using System;
using UnityEngine;

public class PointerManager : MonoBehaviour
{
    public GameObject pointer;
    public int playerId;
    public TargetManager targetManager;

    //Add a pointer to the player with a target
    public void AddPointer(int index)
    {
        GameObject newPointer = Instantiate(pointer, transform);
        newPointer.GetComponent<Pointer>().target = targetManager.GetTargetFromIndex(index);
        newPointer.GetComponent<Pointer>().player = transform;
        targetManager.AddTarget(index, playerId);
    }

    public void RemovePointer(GameObject pointer)
    {
        targetManager.RemoveTarget(pointer.)
        Destroy(pointer);
    }
}
