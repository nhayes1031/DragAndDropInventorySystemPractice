using UnityEngine;
using UnityEngine.AI;

public class ClickMover : IMover
{
    private readonly Player _player;
    private readonly NavMeshAgent _navmeshAgent;

    public ClickMover(Player player)
    {
        _player = player;
        _navmeshAgent = _player.GetComponent<NavMeshAgent>();
        _navmeshAgent.enabled = true;
    }

    public void Tick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out var hitInfo))
            {
                _navmeshAgent.SetDestination(hitInfo.point);
            }
        }
    }
}
