using Pathfinding;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("AI")]
    public AIDestinationSetter ads;
    public AIPath path;
    public Transform player;
    public float stoppingDistens = 1;
    public float maxDistens = 30;

    [Space]
    public bool debug;

    [Header("Sprite")]
    public SpriteRenderer sr;
    public Sprite funnyLitleGuy;

    [HideInInspector]
    public float dist;
    public bool canMove = true;
    bool move = true;

    public void Move()
    {
        if (player == null) player = GameObject.FindGameObjectWithTag("Player").transform;

        if (ads.target == null) ads.target = player;

        //Moves the agent to the player if the distens is more then the stopping distens
        //and if its not in the flashlight
        dist = Vector2.Distance(player.position, transform.position);

        move = dist > stoppingDistens && dist < maxDistens && canMove;

        path.canMove = move;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "EnemyStop")
        {
            canMove = false;

            GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;

            sr.sprite = funnyLitleGuy;
            sr.flipX = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyStop")
        {
            canMove = true;
        }
    }

    private void OnDrawGizmos()
    {
        if (debug)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, maxDistens);

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, stoppingDistens);
        }
    }
}
