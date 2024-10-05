using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private float movementSpeed;         // Швидкість персонажа при активному інпуті
    [SerializeField] private float brakingSpeed;          // Швидкість гальмування після відпускання інпуту
    [SerializeField] private Rigidbody rb;                     // Rigidbody персонажа
    public IInputHandler inputHandler;       // Інтерфейс для введення

    private Vector3 currentVelocity;         // Поточна швидкість персонажа

    private void FixedUpdate()
    {
        if (inputHandler.IsDragging)
        {
            // Якщо інпут активний, задаємо швидкість на основі введення
            Vector2 inputVector = inputHandler.Direction;
            currentVelocity = new Vector3(inputVector.x, 0, inputVector.y) * movementSpeed;
        }
        else
        {
            // Плавне гальмування, коли немає інпуту
            currentVelocity = Vector3.Lerp(currentVelocity, Vector3.zero, brakingSpeed * Time.fixedDeltaTime);
        }

        rb.velocity = currentVelocity;
    }
}
