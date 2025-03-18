using UnityEngine;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float baseSpeed;
    public float speedModifier = 1f;

    [Header("Rotation Modifiers")]
    [SerializeField] float rotationAngle;
    [SerializeField] Transform objectToRotate;

    //Hidden Requirements
    Rigidbody rb;


    //Variables to actually apply changes to the player
    float AppliedSpeed;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        objectToRotate.DOLocalRotate(new Vector3(0, 0, -Input.GetAxis("Horizontal") * rotationAngle), 0.1f);
    }

    private void FixedUpdate()
    {
        AppliedSpeed = baseSpeed * speedModifier;
        float HozInput = Input.GetAxis("Horizontal");
        float VertInput = Input.GetAxis("Vertical");
        Vector3 MoveDirection = new Vector3(HozInput, VertInput, 0);

        rb.AddForce(MoveDirection * AppliedSpeed);
        rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, AppliedSpeed);
    }
}
