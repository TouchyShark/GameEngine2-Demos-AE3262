using UnityEngine;

public class SearchState : BaseState
{
    private float searchTimer;
    private float moveTimer;

    public override void Enter()
    {
        Debug.Log("Entering SearchState");
        enemy.Agent.SetDestination(enemy.LastknowPos); // Move to the last known player position
        searchTimer = 0;
        moveTimer = 0;
    }

    public override void Perform()
    {
        if (enemy.CanSeePlayer())
        {
            stateMachine.ChangeState(new AttackState());
            return;
        }

        // If the enemy has reached the destination
        if (enemy.Agent.remainingDistance < enemy.Agent.stoppingDistance)
        {
            searchTimer += Time.deltaTime;
            moveTimer += Time.deltaTime;

            // Move to a random nearby position after a certain time
            if (moveTimer > Random.Range(3f, 7f))
            {
                Vector3 randomPosition = enemy.transform.position + Random.insideUnitSphere * 5;
                enemy.Agent.SetDestination(randomPosition);
                moveTimer = 0;
            }

            // Exit the search state and return to patrol if the player isn't found after a certain time
            if (searchTimer > 10f)
            {
                stateMachine.ChangeState(new PatrolState());
            }
        }
    }

    public override void Exit()
    {
        Debug.Log("Exiting SearchState");
        // Optional: Reset timers or cleanup if necessary
    }
}
