using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Variables")]
    public float speed;
    public float minMoveDistance;
    [SerializeField] float distance;

    [Tooltip("Will be set to player if left empty")]
    public GameObject target;

    private void Start()
    {
        if (target == null) target = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        distance = Vector2.Distance(transform.position, target.transform.position);

        if (distance > minMoveDistance)
        {
            Vector2 newPosition = Vector2.MoveTowards(
                new Vector2(transform.position.x, transform.position.y), 
                new Vector2(target.transform.position.x, target.transform.position.y), 
                speed * Time.deltaTime
            );

            transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
        }
    }
}