using UnityEngine;

public class CarController : MonoBehaviour
{
    public float speed = 50f; // Tốc độ di chuyển
    public float lateralSpeed = 5f; // Tốc độ di chuyển sang trái/phải

    void Update()
    {
        // Lấy input từ bàn phím
        float lateralInput = Input.GetAxis("Horizontal"); // A/D hoặc Left/Right
        float forwardInput = 1;// Input.GetAxis("Vertical"); // W/S hoặc Up/Down

        // Tính toán vector di chuyển
        Vector3 lateralMovement = transform.right * lateralInput * lateralSpeed * Time.deltaTime;
        Vector3 forwardMovement = transform.forward * forwardInput * speed * Time.deltaTime;

        // Cập nhật vị trí
        transform.position += lateralMovement + forwardMovement;
    }
}