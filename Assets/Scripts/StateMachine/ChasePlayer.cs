using UnityEngine.AI;

public class ChasePlayer : IState
{
    private readonly NavMeshAgent _navMeshAgent;
    public Player _player;

    public ChasePlayer(NavMeshAgent navMeshAgent, Player player)
    {
        _navMeshAgent = navMeshAgent;
        _player = player;
    }


    public void OnEnter()
    {
        _navMeshAgent.enabled = true;
    }

    public void OnExit()
    {
        _navMeshAgent.enabled = false;
    }

    public void Tick()
    {
        _navMeshAgent.SetDestination(_player.transform.position);
    }
}
