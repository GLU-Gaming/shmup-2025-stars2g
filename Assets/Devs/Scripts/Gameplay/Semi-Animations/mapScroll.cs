using UnityEngine;

public class mapScroll : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 0.5f;
    [SerializeField] RectTransform BackgroundImage;
    public float ScrollSpeed { get => scrollSpeed; set => scrollSpeed = value; }

    private void Update()
    {
        Vector3 OldPos = transform.position;
        Vector3 NewPos = new Vector3(OldPos.x - scrollSpeed * Time.deltaTime, OldPos.y, OldPos.z);
        BackgroundImage.localPosition = new Vector3(BackgroundImage.localPosition.x - ((scrollSpeed * Time.deltaTime) /5f), BackgroundImage.localPosition.y, BackgroundImage.localPosition.z);
        transform.position = NewPos;
    }
}
