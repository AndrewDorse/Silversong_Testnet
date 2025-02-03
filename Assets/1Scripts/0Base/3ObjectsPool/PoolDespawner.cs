using UnityEngine;

public class PoolDespawner : MonoBehaviour
{
    [field: SerializeField] public float Delay { get; set; }

    private float _timer = 1f;

    private void OnEnable()
    {
        _timer = 0;
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= Delay)
        {
            ObjectsPool.Despawn(gameObject);
        }
    }
}