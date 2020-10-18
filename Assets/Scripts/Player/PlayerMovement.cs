
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 3f;
    [SerializeField] float sensitivity = 150f;
    [SerializeField] float jumpHeight = 4f;
    [Space]
    [SerializeField] Camera cam = null;
    [SerializeField] Rigidbody rb;
    float clampedXRot;
    bool onGround = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Movement()
    {
        float _xMove = Input.GetAxis("Horizontal"); // Defines Input Axes
        float _zMove = Input.GetAxis("Vertical");
        float _xRot = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        float _yRot = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;

        Vector3 movVert = _zMove * transform.forward;
        Vector3 movHor = _xMove * transform.right;

        Vector3 velocity = (movHor + movVert) * speed * Time.deltaTime;
        Vector3 rotation = new Vector3(0f, _yRot, 0f);

        clampedXRot += _xRot;
        clampedXRot = Mathf.Clamp(clampedXRot, -90f, 90f);

        transform.Rotate(rotation);
        rb.MovePosition(rb.position + velocity);
        //cam.transform.localRotation = Quaternion.Euler(clampedXRot, 0f, 0f); // Parent the camera to the player and reset the transform
    }

    void Jump()
    {
        if (Input.GetButton("Jump"))
        {
            if (onGround == true)
            {
                rb.velocity = new Vector3(0f, jumpHeight, 0f);
                onGround = false;
            }
        }
    }

    void FixedUpdate()
    {
        if (transform != null)
        {
            Movement();
            Jump();
        }
    }

    private void OnCollisionEnter(Collision collision) // Give the ground the tag "Ground"
    {
        if (collision.collider.tag == "Ground")
        {
            onGround = true;
        }
    }
}