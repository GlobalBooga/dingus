using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float sens, speed;
    Rigidbody rb;
    Camera cam;

    MyInput input;

    Vector3 moveDir;

    float rotationY;
    float rotationX;

    IInteractable interactable;

    private void Awake()
    {
        input = new MyInput();
        cam = Camera.main;
        rb = GetComponent<Rigidbody>();

        input.General.WASD.performed += Move;
        input.General.WASD.canceled += ctx => { moveDir = Vector3.zero; };

        input.General.Look.performed += Look;

        input.General.Interact.performed += Interact;

        input.General.Enable();
        Cursor.lockState = CursorLockMode.Locked; 
    }

    private void Start()
    {

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

    void Interact(InputAction.CallbackContext ctx)
    {
        if (interactable != null)
        {
            interactable.Interact();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        IInteractable interacter;
        if (other.TryGetComponent(out interacter))
        {
            StaticStuff.instance.ShowInteractPrompt();
            interactable = interacter;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IInteractable interacter;
        if (other.TryGetComponent(out interacter))
        {
            if (interacter == interactable)
            {
                StaticStuff.instance.HideInteractPrompt();
                interacter.EndInteraction();
                interactable = null;
            }
            else
            {
                Debug.Log("a");
            }
        }
    }
}
