using UnityEngine;
using Pathfinding;

/// <summary>
/// Monster brain class
/// Manages all monster movements and actions
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Combat))]
[RequireComponent(typeof(Seeker))]
public class MonsterBrain : MonoBehaviour
{
    public float moveSpeed;
    public float nextWaypointDistance = 3;
    private int currentWaypoint;
    private bool reachedEnd;
    private Transform target;
    private Combat combat;
    private Rigidbody2D rb;
    private Path path;
    private Seeker seeker;

    void Start()
    {
        combat = GetComponent<Combat>();
        rb = GetComponent<Rigidbody2D>();
        seeker = GetComponent<Seeker>();

        InvokeRepeating("FindPath", 0, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null)
        {
            combat.Attack(); //call for combat attack each Update
        }
    }

    void FindMovement()
    {
            if(path != null)
            {
                if(currentWaypoint >= path.vectorPath.Count)
                {
                    reachedEnd = true;
                    return;
                }
                else
                    reachedEnd = false;
                
                Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;

                rb.MovePosition((Vector2)rb.position + (direction * moveSpeed * Time.deltaTime));

                float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
                if(distance < nextWaypointDistance)
                {
                    currentWaypoint++;
                }
            }
    }

    void FindPath()
    {
        if(target != null)
        if(seeker.IsDone())
            seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    /// <summary>
    /// 
    /// </summary>
    void OnPathComplete(Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    /// <summary>
    /// Function to set a new target to follow by monser
    /// </summary>
    /// <param name="newTarget">New target transform</param>
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    void FixedUpdate()
    {
        if(target != null)
        {
            FindMovement();
        }
    }

}
