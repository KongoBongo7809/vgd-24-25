using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Transform player;
    public float hideDistance;

    private void Update()
    {
        Vector3 dir = target.position - player.position;
        if(dir.magnitude < hideDistance)
        {
            SetChildrenActive(false);
        }
        else
        {
            SetChildrenActive(true);
            transform.gameObject.SetActive(true);
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(player.position, hideDistance);
    }

    private void SetChildrenActive(bool val)
    {
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(val);
        }
    }

}
