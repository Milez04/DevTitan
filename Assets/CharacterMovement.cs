using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 5f; // Karakterin hızı
    private CharacterController controller; // Karakter Controller bileşeni

    void Start()
    {
        controller = GetComponent<CharacterController>(); // CharacterController'ı al
    }

    void Update()
    {
        MoveCharacter();
    }

    void MoveCharacter()
    {
        // Klavye girdilerini al
        float moveX = Input.GetAxis("Horizontal"); // A/D veya Sol/Sağ ok tuşları
        float moveZ = Input.GetAxis("Vertical"); // W/S veya Yukarı/Aşağı ok tuşları

        // Hareket yönünü belirle
        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        // Karakteri hareket ettir
        controller.Move(move * speed * Time.deltaTime);
    }
}
