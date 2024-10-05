using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private float movementSpeed;         // �������� ��������� ��� ��������� �����
    [SerializeField] private float brakingSpeed;          // �������� ����������� ���� ���������� ������
    [SerializeField] private Rigidbody rb;                     // Rigidbody ���������
    public IInputHandler inputHandler;       // ��������� ��� ��������

    private Vector3 currentVelocity;         // ������� �������� ���������

    private void FixedUpdate()
    {
        if (inputHandler.IsDragging)
        {
            // ���� ����� ��������, ������ �������� �� ����� ��������
            Vector2 inputVector = inputHandler.Direction;
            currentVelocity = new Vector3(inputVector.x, 0, inputVector.y) * movementSpeed;
        }
        else
        {
            // ������ �����������, ���� ���� ������
            currentVelocity = Vector3.Lerp(currentVelocity, Vector3.zero, brakingSpeed * Time.fixedDeltaTime);
        }

        rb.velocity = currentVelocity;
    }
}
