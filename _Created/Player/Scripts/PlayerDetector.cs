
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector: MonoBehaviour
{
    [HideInInspector] public BaseInteraction currentTarget;
    public PlayerControlInput input;
    [SerializeField] protected List<BaseInteraction> targetsCollection;
    [SerializeField] protected Queue<BaseInteraction> waiting; // for targets detected but not viewable
    [SerializeField] protected int indexTarget = 0;
    // viewable check
    Ray rayCheck;
    RaycastHit hit;
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
        if (input.interact) InteractToTarget();
        // alway rotate follow camera
        targetEuler = transform.rotation.eulerAngles;
        targetEuler.y = cam.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Euler(targetEuler);
        // check targets viewable or not
        if(currentTarget != null)
        {
            if (!targetsCollection.Contains(currentTarget)) currentTarget = null;
        }
        foreach(BaseInteraction target in targetsCollection)
        {
            if (!ViewableTarget(target.transform)) waiting.Enqueue(target);
        }
        for(int i=0; i<waiting.Count; i++)
        {
            BaseInteraction checking = waiting.Dequeue();

            // make sure any element in waiting is not in targetCollection
            if(targetsCollection.Contains(checking)) targetsCollection.Remove(checking);

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
    public List<BaseInteraction> TargetsCollection() => this.targetsCollection;
    public bool InteractToTarget()
    {
        if (!currentTarget)
        {
            // don't have target
            Debug.Log("Have no target");

            // try switch target
            SwitchTarget();
            return false;
        }
        else if (!currentTarget.AllowInteract())
        {
            // can not interact to Target, maybe it's too far
            Debug.Log("can not interact to Target, maybe it's too far");
            return false;
        }
        else
        {
            // interact to target
            currentTarget.Interact();
            return true;
        }
    }
    public bool SwitchTarget()
    {
        // currentTarget = null if collection is empty
        if (targetsCollection.Count <= 0)
        {
            currentTarget = null;
            return false;
        }

        // update indexTarget
        indexTarget = indexTarget + 1 >= targetsCollection.Count ? 0 : indexTarget + 1;
        
        // switch target
        currentTarget = targetsCollection[indexTarget];
        return true;
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
            // switch to other target if current target is target that was removed
            if(currentTarget == target) SwitchTarget();
            
            return true;
        }
        else
            return false;
    }
    private bool ViewableTarget(Transform target)
    {
        bool viewable = false;

        float maxDistance = Vector3.Distance (transform.position, target.transform.position) + 5f;
        Vector3 direction = (target.transform.position - transform.position).normalized;

        if(Physics.Raycast(transform.position, direction, out hit, maxDistance))
        {
            if (hit.transform == target) viewable = true;
            else viewable = false;
        }
        return viewable;
    }
}