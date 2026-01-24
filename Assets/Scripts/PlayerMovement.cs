using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 6f;
    public float gravity = -20f;
    public float jumpHeight = 1.2f;

    public float rotationSpeed = 180f;

    private CharacterController controller;
    private float velocityY;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float turn = 0f;
        if (Input.GetKey(KeyCode.A)) turn = -1f;
        if (Input.GetKey(KeyCode.D)) turn = 1f;

        transform.Rotate(0f, turn * rotationSpeed * Time.deltaTime, 0f);

        float forwardInput = 0f;
        if (Input.GetKey(KeyCode.W)) forwardInput = 1f;
        if (Input.GetKey(KeyCode.S)) forwardInput = -1f;

        Vector3 move = transform.forward * forwardInput * moveSpeed;

        if (controller.isGrounded && velocityY < 0f)
            velocityY = -2f;

        if (controller.isGrounded && Input.GetButtonDown("Jump"))
            velocityY = Mathf.Sqrt(jumpHeight * -2f * gravity);

        velocityY += gravity * Time.deltaTime;

        move.y = velocityY;

        controller.Move(move * Time.deltaTime);
    }
}
