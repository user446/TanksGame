using UnityEngine;

/// <summary>
/// Monster brain class
/// Manages all monster movements and actions
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Combat))]
public class MonsterBrain : MonoBehaviour
{
    public float moveSpeed;
    private Transform target;
    private Combat combat;
    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        combat = GetComponent<Combat>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null)
        {
            Vector3 direction = target.position - transform.position;
            direction.Normalize();
            movement = direction;
            combat.Attack(); //call for combat attack each Update
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
            MoveTowards(movement);
            Debug.Log("Crawling...");
        }
    }

    /// <summary>
    /// Moves rigidbody of a monster following given direction
    /// </summary>
    /// <param name="direction">Direction to move on</param>
    void MoveTowards(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }
}
