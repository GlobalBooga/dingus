using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float sens, speed;
    [SerializeField] Animator rightAnimator;
    Rigidbody rb;
    Camera cam;

    Vector3 moveDir;

    float rotationY;
    float rotationX;

    IInteractable interactable;

    private void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody>();

        StaticStuff.input.General.WASD.performed += Move;
        StaticStuff.input.General.WASD.canceled += ctx => { moveDir = Vector3.zero; };

        StaticStuff.input.General.Look.performed += Look;

        StaticStuff.input.General.Interact.performed += Interact;

        StaticStuff.input.General.Enable();
        Cursor.lockState = CursorLockMode.Locked; 
    }

    private void OnDestroy()
    {
        StaticStuff.input.Dispose();
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
            StaticStuff.instance.HideInteractPrompt();
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

    public void PlayAnimation(string triggerName)
    {
        rightAnimator.SetTrigger(triggerName);
    }
}
