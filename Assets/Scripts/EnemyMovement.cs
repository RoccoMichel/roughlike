using Pathfinding;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public AIDestinationSetter ads;
    public AIPath path;
    public Transform player;
    public float stoppingDistens = 1;

    public LayerMask Player;

    private void Update()
    {
        if (ads.target == null) ads.target = player;

        float dist = Vector2.Distance(player.position, transform.position);
        if (dist > stoppingDistens)
            path.canMove = true;
        else
        {
            path.canMove = false;

            Vector3 dir = (transform.position - player.position).normalized;
            RaycastHit hit;
            if(Physics.Raycast(transform.position, dir, out hit, 10, Player))
            {

            }
        }
    }
}
