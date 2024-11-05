using Pathfinding;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("AI")]
    public AIDestinationSetter ads;
    public AIPath path;
    public Transform player;
    public float stoppingDistens = 1;

    [HideInInspector]
    public float dist;
    bool canMove = true;
    bool move = true;

    public void Move()
    {
        if (player == null) player = GameObject.FindGameObjectWithTag("Player").transform;

        if (ads.target == null) ads.target = player;

        //Moves the agent to the player if the distens is more then the stopping distens
        //and if its not in the flashlight
        dist = Vector2.Distance(player.position, transform.position);

        move = dist > stoppingDistens && canMove;

        path.canMove = move;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "EnemyStop")
        {
            canMove = false;

            GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyStop")
        {
            canMove = true;
        }
    }
}
