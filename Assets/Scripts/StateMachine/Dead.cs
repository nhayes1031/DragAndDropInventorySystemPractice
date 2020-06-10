using UnityEngine;

public class Dead : IState
{
    private const float DESPAWN_DELAY = 5f;

    private float _despawnTime;
    private Entity _entity;

    public Dead(Entity entity)
    {
        _entity = entity;
    }

    public void OnEnter()
    {
        _despawnTime = Time.time + DESPAWN_DELAY;
    }

    public void OnExit()
    {
    }

    public void Tick()
    {
        if (Time.time >= _despawnTime)
            GameObject.Destroy(_entity.gameObject);
    }
}