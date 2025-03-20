using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Mosquito : MonoBehaviour
{
    [SerializeField] EntryType entryType;

    GameObject player;

    //Base Data for locations
    Vector2 hozAxes = new Vector2(5, 21);
    Vector2 verticalAxes = new Vector2(-11, 13);

    private enum EntryType
    {
        bottom,
        top,
        edge
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(Animate());

    }

    IEnumerator Animate()
    {
        //Entry Animation
        yield return new WaitForSeconds(0.1f);
        switch (entryType)
        {
            case EntryType.bottom:
                transform.position = new Vector3(Random.Range(hozAxes.x, hozAxes.y), -15, 0);
                transform.rotation = Quaternion.Euler(0, 0, -20);
                float Height = Random.Range(-10, 10);
                float DistanceToFly = Mathf.Abs(-15 - Height);
                print(DistanceToFly);
                float RotateTime = (Random.Range(1.5f, 2f) * (DistanceToFly / 10));
                transform.DOMoveY(Height, (Random.Range(1.4f, 1.7f) * (DistanceToFly/10))).SetEase(Ease.OutBack);
                transform.DORotate(new Vector3(0, 0, 5), RotateTime).SetEase(Ease.OutBack);
                yield return new WaitForSeconds(RotateTime);    
                break;
            case EntryType.top:
                transform.position = new Vector3(Random.Range(hozAxes.x, hozAxes.y), 17, 0);
                break;
            case EntryType.edge:
                transform.position = new Vector3(28, Random.Range(verticalAxes.x, verticalAxes.y), 0);
                break;
        }
    }
}
