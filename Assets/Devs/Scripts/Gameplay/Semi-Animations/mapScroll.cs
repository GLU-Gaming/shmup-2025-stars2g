using UnityEngine;

public class mapScroll : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 0.5f;
    public float ScrollSpeed { get => scrollSpeed; set => scrollSpeed = value; }

    private void Update()
    {
        Vector3 OldPos = transform.position;
        Vector3 NewPos = new Vector3(OldPos.x - scrollSpeed * Time.deltaTime, OldPos.y, OldPos.z);
        transform.position = NewPos;
    }
}
