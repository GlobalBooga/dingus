using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float sens, speed;
    Rigidbody rb;
    CapsuleCollider cc;
    Camera cam;

    MyInput input;

    Vector3 moveDir;

    float rotationY;
    float rotationX;

    private void Awake()
    {
        input = new MyInput();

        rb = GetComponent<Rigidbody>();
        cc = GetComponent<CapsuleCollider>();


        input.General.Enable();
        cam = Camera.main;
    }
    private void Start()
    {
        input.General.WASD.performed += Move;
        input.General.WASD.canceled += ctx => { moveDir = Vector3.zero; };

        input.General.Look.performed += Look;

        Cursor.lockState = CursorLockMode.Locked; 
    }

    private void OnDestroy()
    {
        input.Dispose();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 newDir = moveDir.x * transform.right + moveDir.z * transform.forward;
        rb.AddForce(newDir * rb.mass * speed, ForceMode.Force);
    }

    void Look(InputAction.CallbackContext ctx)
    {
        Vector2 lookDelta = ctx.ReadValue<Vector2>();

        rotationY += lookDelta.x * sens;
        rotationX = Mathf.Clamp(rotationX + lookDelta.y * sens, -90, 90);

        transform.localEulerAngles = rotationY * Vector3.up;
        cam.transform.localEulerAngles = -rotationX * Vector3.right;
    }

    void Move(InputAction.CallbackContext ctx)
    {
        Vector2 input = ctx.ReadValue<Vector2>();
        moveDir = new Vector3(input.x, 0, input.y);

    }

    private void OnTriggerEnter(Collider other)
    {
        IInteractable interacter;
        if (other.TryGetComponent(out interacter))
        {
            interacter.Interact();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IInteractable interacter;
        if (other.TryGetComponent(out interacter))
        {
            interacter.EndInteraction();
        }
    }
}
