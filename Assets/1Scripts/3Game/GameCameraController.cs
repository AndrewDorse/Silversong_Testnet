using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCameraController : MonoBehaviour
{
    [SerializeField] private float distance = 7f;
    [SerializeField] private float height = 12f;
    [SerializeField] private float smoothSpeed;
    private Transform _target;
    private Vector3 velocity = Vector3.zero;

    private void Start()
    { 
        transform.rotation = Quaternion.Euler(50f, 0.0f, 0.0f);
    }

    private void LateUpdate()
    {
        if (_target != null)
        {
            Vector3 targetPosition = Vector3.zero;
            targetPosition.x = _target.position.x;
            targetPosition.y = _target.position.y + height;
            targetPosition.z = _target.position.z - distance;

            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothSpeed);

        }
    }

    public void Setup(GameObject hero)
    {
        gameObject.SetActive(true);

        if (hero != null)
        {
            _target = hero.transform;
        }
    }

}
