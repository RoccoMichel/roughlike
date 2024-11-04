using Pathfinding;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("AI")]
    public AIDestinationSetter ads;
    public AIPath path;
    public Transform player;
    public float stoppingDistens = 1;
    bool canMove = true;

    public LayerMask Player;

    private void Update()
    {
        if (ads.target == null) ads.target = player;

        //Moves the agent to the player if the distens is more then the stopping distens
        float dist = Vector2.Distance(player.position, transform.position);
        if (dist > stoppingDistens && canMove)
            path.canMove = true;
        else
        {
            path.canMove = false;

            //Shoting
            Vector3 dir = -(transform.position - player.position).normalized;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 10, Player);
            if(hit)
            {
                //Shoot
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "EnemyStop")
        {
            canMove = false;
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
