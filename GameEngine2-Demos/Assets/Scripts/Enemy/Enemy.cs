using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private StateMachine stateMachine;
    private NavMeshAgent agent;
    private GameObject player;
    private Vector3 lastKnownPos;

    public NavMeshAgent Agent => agent;
    public GameObject Player => player;
    public Vector3 LastknowPos { get => lastKnownPos; set => lastKnownPos = value; }

    public Path path;

    [Header("Sight Values")]
    public float sightDistance = 20f;
    public float fieldOfView = 85f;
    public float eyeHeight = 1.5f;

    [Header("Weapon Values")]
    public Transform gunBarrel;
    [Range(0.1f, 10f)] public float fireRate;

    [SerializeField] public string currentState;

    void Start()
    {
        stateMachine = GetComponent<StateMachine>();
        agent = GetComponent<NavMeshAgent>();

        // Initialize the state machine correctly (fixed missing method call)
        stateMachine.Initialise();

        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        // Update currentState if stateMachine or its activeState is not null
        currentState = stateMachine.activeState?.GetType().Name;
    }

    public bool CanSeePlayer()
    {
        if (player == null)
            return false;

        // Calculate the direction to the player
        Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        // Check if the player is within sight range
        if (distanceToPlayer <= sightDistance)
        {
            // Calculate the angle between the forward direction and the direction to the player
            float angleToPlayer = Vector3.Angle(directionToPlayer, transform.forward);

            // If the player is within the field of view
            if (angleToPlayer <= fieldOfView / 2)
            {
                // Perform a raycast to check if there's line-of-sight to the player
                if (Physics.Raycast(transform.position + Vector3.up * eyeHeight, directionToPlayer, out RaycastHit hit, sightDistance))
                {
                    if (hit.transform.CompareTag("Player"))
                    {
                        Debug.DrawRay(transform.position + Vector3.up * eyeHeight, directionToPlayer * sightDistance, Color.red);
                        return true;
                    }
                }
            }
        }
        return false;
    }
}
