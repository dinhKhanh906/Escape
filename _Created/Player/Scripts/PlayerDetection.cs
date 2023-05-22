
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection: MonoBehaviour
{
    public BaseInteraction currentTarget;
    public PlayerControlInput input;
    [SerializeField] protected List<BaseInteraction> targetsCollection;
    [SerializeField] protected Queue<BaseInteraction> waiting; // for targets detected but not viewable
    [SerializeField] protected int indexTarget = 0;

    // follow camera
    private Transform cam;
    private Vector3 targetEuler;
    private void Awake()
    {
        waiting = new Queue<BaseInteraction>();
        cam = Camera.main.transform;
    }
    private void Start()
    {
        // start with first target
        indexTarget = 0;
        if(targetsCollection.Count > 0)
        {
            currentTarget = targetsCollection[indexTarget];
        }
    }
    private void Update()
    {
        if (input.switchTarget) SwitchTarget();

        // alway rotate follow camera
        targetEuler = transform.rotation.eulerAngles;
        targetEuler.y = cam.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Euler(targetEuler);
        // check targets viewable or not
        foreach(BaseInteraction target in targetsCollection)
        {
            if (!ViewableTarget(target.transform)) waiting.Enqueue(target);
        }
        for(int i=0; i<waiting.Count; i++)
        {
            BaseInteraction checking = waiting.Dequeue();
            if(ViewableTarget(checking.transform)) AddNewTarget(checking);
            else
            {
                waiting.Enqueue(checking);
                RemoveTarget(checking);
            }
        }
    }
    private void OnDrawGizmos()
    {
        // draw line from player to target
        if (currentTarget)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, currentTarget.transform.position);
        }
    }
    public void SwitchTarget()
    {
        // currentTarget = null if collection is empty
        if (targetsCollection.Count <= 0)
        {
            currentTarget = null;
            return;
        }

        // update indexTarget
        indexTarget = indexTarget + 1 >= targetsCollection.Count ? 0 : indexTarget + 1;
        
        // switch target
        currentTarget = targetsCollection[indexTarget];
    }
    public bool AddNewTarget(BaseInteraction newTarget)
    {
        if (newTarget && !targetsCollection.Contains(newTarget))
        {
            // add newTarget
            targetsCollection.Add(newTarget);
            // switch currentTarget to newTarget if collect has only one target
            if(targetsCollection.Count == 1) SwitchTarget();
            return true;
        }
        else 
            return false;
    }
    public bool RemoveTarget(BaseInteraction target)
    {
        // remove target
        if (target && targetsCollection.Contains(target))
        {
            //remove target
            targetsCollection.Remove(target);
            // switch to other target
            SwitchTarget();
            return true;
        }
        else
            return false;
    }
    private void OnTriggerEnter(Collider other)
    {
        BaseInteraction interaction = other.GetComponent<BaseInteraction>();
        if (interaction != null)
        {
            if (ViewableTarget(interaction.transform)) AddNewTarget(interaction);
            else waiting.Enqueue(interaction);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        BaseInteraction interaction = other.GetComponent<BaseInteraction>();
        if (interaction != null)
        {
            RemoveTarget(interaction);
        }
    }
    private bool ViewableTarget(Transform target)
    {
        bool viewable = false;
        float maxDistance = Vector3.Distance (transform.position, target.transform.position);
        RaycastHit[] allHits = Physics.RaycastAll(transform.position, target.transform.position - transform.position, maxDistance);

        if(allHits.Length > 0)
        {
            if (allHits[0].transform == target) viewable = true;
            else viewable = false;
        }
        return viewable;
    }
}