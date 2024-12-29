using UnityEngine;

public class CarAI : MonoBehaviour
{
    public float speed = 10f; // Tốc độ di chuyển

    void Update()
    {
        // Tính toán vector di chuyển về phía trước
        Vector3 move = transform.forward * speed * Time.deltaTime;

        // Cập nhật vị trí của xe
        transform.position += move;
    }
}