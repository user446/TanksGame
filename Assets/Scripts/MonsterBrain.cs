using UnityEngine;

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
            combat.Attack();
        }
    }

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

    void MoveTowards(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }
}
