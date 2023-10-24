using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] float sens, speed;
    [SerializeField] Animator rightAnimator;
    [SerializeField] Animator leftAnimator;
    Rigidbody rb;
    Camera cam;

    Vector3 moveDir;

    float rotationY;
    float rotationX;

    IInteractable interactable;

    public AudioSource mySfx;
    public AudioSource myFootstepsSFX;
    [SerializeField] AudioClip snipSfx;
    [SerializeField] AudioClip[] footstepsTopFloor;
    [SerializeField] AudioClip[] footstepsBasement;

    bool isInBasement;

    float footstepDelay = 0.3f;
    float footTime;

    public GameObject NormalArm;
    public GameObject GrabbedBrosky;
    public GameObject ChokeholdArm;


    private void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody>();

        StaticStuff.input.General.WASD.performed += Move;
        StaticStuff.input.General.WASD.canceled += ctx => { moveDir = Vector3.zero; };

        StaticStuff.input.General.Look.performed += Look;

        StaticStuff.input.General.Interact.performed += Interact;
        StaticStuff.input.General.MoveFingers.performed += ctx =>
        {
            if (!StaticStuff.input.General.Look.enabled) return;
            rightAnimator.SetTrigger("DoInteract");
        };

        StaticStuff.input.General.Enable();
        Cursor.lockState = CursorLockMode.Locked; 
        mySfx = GetComponent<AudioSource>();

        rotationY = StaticStuff.RadsToDeg(transform.rotation.eulerAngles.y);
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

    private void Update()
    {
        if (moveDir != Vector3.zero && footTime > footstepDelay)
        {
            footTime = 0;
            myFootstepsSFX.clip = !isInBasement? footstepsTopFloor[Random.Range(0, footstepsTopFloor.Length)] :
                footstepsBasement[Random.Range(0, footstepsBasement.Length)];
            myFootstepsSFX.Play();
        }

        footTime += Time.deltaTime;
    }

    void Look(InputAction.CallbackContext ctx)
    {
        Vector2 lookDelta = ctx.ReadValue<Vector2>();

        rotationY += lookDelta.x * sens;

        if (rotationY > 360) rotationY -= 360;
        else if (rotationY < -360) rotationY += 360;

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

        if (other.CompareTag("Basement"))
        {
            isInBasement = true;
        }
        else
        {
            isInBasement = false;
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
        }
    }

    public void PlayAnimation(string triggerName)
    {
        rightAnimator.SetTrigger(triggerName);
    }

    public void SetLookRotation(Vector3 look)
    {
        rotationY = Vector3.SignedAngle(Vector3.forward, look, Vector3.up); 
        transform.Rotate(Vector3.up,Vector3.Angle(transform.forward, look));
    }

    public void PlaySnipSFX()
    {
        mySfx.clip = snipSfx;
        mySfx.Play();
    }

}
