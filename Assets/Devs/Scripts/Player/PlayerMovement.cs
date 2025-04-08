using UnityEngine;
using DG.Tweening;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float baseSpeed;
    public float speedModifier = 1f;

    [Header("Rotation Modifiers")]
    [SerializeField] float rotationAngle;
    [SerializeField] Transform objectToRotate;

    [Header("Visual Effects")]
    [SerializeField] ParticleSystem trail; //the thruster Effect

    //Hidden Requirements
    Rigidbody rb;


    //Variables to actually apply changes to the player
    float AppliedSpeed;
    bool InputActive = true;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void EndAnim()
    {
        InputActive = false;
        StartCoroutine(EndNumerator());
    }

    IEnumerator EndNumerator()
    {
        objectToRotate.DOLocalRotate(new Vector3(360, 0, 0), 1f).SetEase(Ease.InSine);
        objectToRotate.DOLocalMoveX(50, 1).SetEase(Ease.InSine);
        yield return new WaitForSeconds(0.5f);
    }

    private void Update()
    {
        objectToRotate.DOLocalRotate(new Vector3(0, 0, -Input.GetAxis("Horizontal") * rotationAngle), 0.1f); //Gives a nice tilt effect
    }

    private void FixedUpdate()
    {
        AppliedSpeed = baseSpeed * speedModifier; //In case of speed changes and keeping the base speed intact
        float HozInput = Input.GetAxis("Horizontal");
        float VertInput = Input.GetAxis("Vertical");
        Vector3 MoveDirection = new Vector3(HozInput, VertInput, 0);

        rb.AddForce(MoveDirection * AppliedSpeed);
        rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, AppliedSpeed); //Prevents Strafing (increasing speed by moving diagonally)

        if(HozInput < 0)
        {
            trail.Stop();
        } else
        {
            trail.Play();
        }
    }
}
