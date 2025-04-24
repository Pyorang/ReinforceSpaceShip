using UnityEngine;

public class Movement2D : MonoBehaviour
{
    [SerializeField] protected float moveSpeed = 0.0f;
    [SerializeField] protected Vector3 moveDirection = Vector3.zero;

    private void Update()
    {
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    public void MoveTo(Vector3 direction)
    {
        moveDirection = direction;
    }
}
