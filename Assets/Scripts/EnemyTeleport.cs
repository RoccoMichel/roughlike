using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyTeleport : EnemyMovement
{
    public Tilemap tm;

    List<Vector3> positions = new List<Vector3>();

    [Header("Settings")]
    public float maxTeleportDist = 20;
    public float damage;
    public float fireRate;
    public LayerMask Player;
    bool hasFired;
    bool canShoot;

    [Header("Sprits")]
    public Sprite front;
    public Sprite back;
    public Sprite left;
    public Sprite right;

    EnemyStats es;
    float curentHealth;

    private void Start()
    {
        es = GetComponent<EnemyStats>();
        curentHealth = es.health;

        if (tm == null) tm = GameObject.FindGameObjectWithTag("floor").GetComponent<Tilemap>();

        for(int x = tm.cellBounds.xMin; x < tm.cellBounds.xMax; x++)
        {
            for(int y = tm.cellBounds.yMin; y < tm.cellBounds.yMax; y++)
            {
                Vector3Int localLocation = new Vector3Int(
                    x: x,
                    y: y,
                    z: 0);

                Vector3 location = tm.CellToWorld(localLocation) + new Vector3(2.5f, 2.5f, 0);
                if (tm.HasTile(localLocation))
                {
                    positions.Add(location);
                }
            }
        }
    }

    private void FixedUpdate()
    {
        Move();

        Teleport();

        Rotate();

        canShoot = dist <= stoppingDistens && canMove;
        if (canShoot)
            Shoot();
    }

    void Teleport()
    {
        if (curentHealth > es.health)
        {
            Vector3 teleportPos = positions[Random.Range(0, positions.Count)];

            while (Vector2.Distance(teleportPos, player.position) > maxTeleportDist)
            {
                teleportPos = positions[Random.Range(0, positions.Count)];
            }

            transform.position = teleportPos;

            curentHealth = es.health;
        }
    }

    void Shoot()
    {
        if (!hasFired)
        {
            //Shoots a ray towards the player
            Vector3 dir = -(transform.position - player.position).normalized;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 10, Player);
            if (hit)
            {
                hit.transform.gameObject.GetComponent<Player>().TakeDamage(damage);
            }

            hasFired = true;
            StartCoroutine(SetHasFiredToFalse());
        }
    }

    void Rotate()
    {
        //Facing left
        if (path.desiredVelocity.x < 0 && Mathf.Abs(path.desiredVelocity.y) < 0.5f)
        {
            sr.sprite = left;
        }

        //Facing right
        if (path.desiredVelocity.x > 0 && Mathf.Abs(path.desiredVelocity.y) < 0.5f)
        {
            sr.sprite = right;
        }

        //Facing up
        if (path.desiredVelocity.y > 0.5f)
        {
            sr.sprite = back;
        }

        //Facing down
        if (path.desiredVelocity.y < -0.5f)
        {
            sr.sprite = front;
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

            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, maxTeleportDist);
        }
    }

    public IEnumerator SetHasFiredToFalse()
    {
        yield return new WaitForSeconds(fireRate);
        hasFired = false;
    }
}
