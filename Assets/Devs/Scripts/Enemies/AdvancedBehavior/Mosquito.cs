using System.Collections;
using UnityEngine;

public class Mosquito : MonoBehaviour
{
    [SerializeField] EntryType entryType;



    //Base Data for locations
    Vector2 HozAxes = new Vector2(5, 21);
    Vector2 VerticalAxes = new Vector2(-11, 13);

    private enum EntryType
    {
        bottom,
        top,
        edge
    }

    IEnumerator Animate()
    {
        yield return new WaitForSeconds(0.1f);
        switch (entryType)
        {
            case EntryType.bottom:
                transform.position = new Vector3(Random.Range(HozAxes.x, HozAxes.y), -15, 0);
                break;
            case EntryType.top:
                transform.position = new Vector3(Random.Range(HozAxes.x, HozAxes.y), 17, 0);
                break;
            case EntryType.edge:
                transform.position = new Vector3(28, Random.Range(VerticalAxes.x, VerticalAxes.y), 0);
                break;
        }
    }
}
