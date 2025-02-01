using UnityEngine;

public class Pointer : MonoBehaviour
{
    public float hideDistance = 5.3f;
    private int index;
    private Transform target;
    private Transform player;
    private bool reachedTarget;

    private void Update()
    {
        Vector3 dir = target.position - player.position;
        if(dir.magnitude < hideDistance)
        {
            reachedTarget = true;
            SetChildrenActive(false);
        }
        else
        {
            reachedTarget = false;
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

    //Getter methods
    public int GetIndex()
    {
        return index;
    }

    public Transform GetTarget()
    {
        return target;
    }

    public Transform GetPlayer()
    {
        return player;
    }

    public bool HasReachedTarget()
    {
        return reachedTarget;
    }

    //Setter methods
    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    public void SetPlayer(Transform player)
    {
        this.player = player;
    }

}
