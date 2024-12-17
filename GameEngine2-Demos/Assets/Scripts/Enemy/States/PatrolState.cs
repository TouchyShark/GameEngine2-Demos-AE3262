using UnityEngine;

public class PatrolState : BaseState
{
    private int waypointIndex;
    private float waitTimer;

    public override void Enter()
    {
        Debug.Log("Entering PatrolState");

        // Set the first waypoint if there are waypoints defined
        if (enemy.path.waypoints.Count > 0)
        {
            waypointIndex = 0;
            enemy.Agent.SetDestination(enemy.path.waypoints[waypointIndex].position);
        }
        else
        {
            Debug.LogWarning("No waypoints defined in the enemy's path!");
        }
    }

    public override void Perform()
    {
        // Patrol logic
        PatrolCycle();

        // Transition to AttackState if the player is spotted
        if (enemy.CanSeePlayer())
        {
            stateMachine.ChangeState(new AttackState());
        }
    }

    public override void Exit()
    {
        Debug.Log("Exiting PatrolState");
    }

    private void PatrolCycle()
    {
        // Ensure there are waypoints to patrol
        if (enemy.path.waypoints.Count == 0) return;

        // Check if the enemy has reached the current waypoint
        if (enemy.Agent.remainingDistance < 0.5f && !enemy.Agent.pathPending)
        {
            waitTimer += Time.deltaTime;

            if (waitTimer > 3f) // Wait for 3 seconds before moving to the next waypoint
            {
                // Move to the next waypoint (loop back to the first after the last)
                waypointIndex = (waypointIndex + 1) % enemy.path.waypoints.Count;
                enemy.Agent.SetDestination(enemy.path.waypoints[waypointIndex].position);
                waitTimer = 0f; // Reset the wait timer
            }
        }
    }
}
