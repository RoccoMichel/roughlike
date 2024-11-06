using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Variables")]
    public float speed;
    public float minMoveDistance;
    private float appliedSpeed;
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
        appliedSpeed = Mathf.Lerp(speed, 400, Mathf.InverseLerp(4, 100, distance));
        if (distance > 100) appliedSpeed = 5000;

        if (distance > minMoveDistance)
        {
            Vector2 newPosition = Vector2.MoveTowards(
                new Vector2(transform.position.x, transform.position.y), 
                new Vector2(target.transform.position.x, target.transform.position.y), 
                appliedSpeed * Time.deltaTime
            );

            transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
        }


    }
}