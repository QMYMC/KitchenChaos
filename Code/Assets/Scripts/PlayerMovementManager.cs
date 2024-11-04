using UnityEngine;

public class PlayerMovementManager : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float rotateSpeed = 10f;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }

    public void Move(Vector3 direction)
    {
        if (direction != Vector3.zero)
        {
            Vector3 targetPosition = rb.position + direction * Time.deltaTime * moveSpeed; // 使用 Time.deltaTime
            rb.MovePosition(targetPosition);

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, Time.deltaTime * rotateSpeed)); // 使用 Time.deltaTime
        }
    }

    public Vector3 GetMovementDirection()
    {
        Vector3 direction = Vector3.zero;
        if (Input.GetKey(KeyBindingManager.Instance.GetMoveUpKey())) direction += Vector3.forward;
        if (Input.GetKey(KeyBindingManager.Instance.GetMoveDownKey())) direction += Vector3.back;
        if (Input.GetKey(KeyBindingManager.Instance.GetMoveLeftKey())) direction += Vector3.left;
        if (Input.GetKey(KeyBindingManager.Instance.GetMoveRightKey())) direction += Vector3.right;
        return direction.normalized;
    }
}