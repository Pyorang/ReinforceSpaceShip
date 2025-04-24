using UnityEngine;

public class PositionScroller : MonoBehaviour
{
    private static readonly float backGroundOffSet = 0.3f;

    [SerializeField] private Transform target;
    [SerializeField] private float scrollRange;
    [SerializeField] private float moveSpeed = 3.0f;
    [SerializeField] private Vector3 moveDirection = Vector3.down;

    private void Update()
    {
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        if(transform.position.y <= -scrollRange)
        {
            transform.position = target.position + Vector3.up * scrollRange - Vector3.up * backGroundOffSet;
        }
    }
}
