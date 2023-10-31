using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Rigidbody2D _rigidBody2D;

    private void Start()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_rigidBody2D.velocity != Vector2.zero)
        {
            float angle = Mathf.Atan2(_rigidBody2D.velocity.y, _rigidBody2D.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }

}
