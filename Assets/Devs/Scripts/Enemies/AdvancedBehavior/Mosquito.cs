using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UIElements;

public class Mosquito : MonoBehaviour
{

    //Uses bee as a base script but changes the base functionality

    [SerializeField] EntryType entryType;
    [SerializeField] float aimOffsetZ;
    [SerializeField] EnemyHealth Health;
    [SerializeField] GameObject LaserPrefab;
    [SerializeField] float LookSpeed;
    [SerializeField] ParticleSystem ChargeParts;

    float actualSpeed;

    [SerializeField] float laserOffsetRotation = 90;

    GameObject Offset;
    GameObject player;

    //Base Data for locations
    Vector2 hozAxes = new Vector2(5, 21);
    Vector2 verticalAxes = new Vector2(-11, 13);

    bool Charging;
    private enum EntryType
    {
        bottom,
        top,
        edge
    }

    private void Start()
    {
        Offset = transform.Find("Offset").gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(Animate());
    }

    IEnumerator Animate()
    {
        //Entry Animation
        Offset.transform.localRotation = Quaternion.Euler(0, 0, -180);
        switch (entryType)
        {
            case EntryType.bottom:
                transform.position = new Vector3(Random.Range(hozAxes.x, hozAxes.y), -15, 0);
                transform.rotation = Quaternion.Euler(0, 0, -20);
                float Height = Random.Range(-10, 10);
                float DistanceToFly = Mathf.Abs(-15 - Height);
                float RotateTime = (Random.Range(1.5f, 1.7f) * (DistanceToFly / 10));
                transform.DOMoveY(Height, (Random.Range(1.4f, 1.7f) * (DistanceToFly / 10))).SetEase(Ease.OutBack);
                transform.DORotate(new Vector3(0, 0, 5), RotateTime).SetEase(Ease.OutBack);
                yield return new WaitForSeconds(0.4f);
                Health.invincible = false;
                yield return new WaitForSeconds(RotateTime - 0.4f);

                break;
            case EntryType.top:
                transform.position = new Vector3(Random.Range(hozAxes.x, hozAxes.y), 17, 0);
                break;
            case EntryType.edge:

                transform.position = new Vector3(28, Random.Range(verticalAxes.x, verticalAxes.y), 0);
                transform.rotation = Quaternion.Euler(0, 0, 20);
                transform.DOMoveX(Random.Range(hozAxes.x, hozAxes.y), 2f).SetEase(Ease.OutBack);
                transform.DORotate(new Vector3(0, 0, 0), 2f).SetEase(Ease.OutBack);
                yield return new WaitForSeconds(0.4f);
                Health.invincible = false;
                yield return new WaitForSeconds(1.6f);
                break;
        }
        while (true)
        {
            actualSpeed = LookSpeed;
            Charging = true;
            ChargeParts.Play();
            yield return new WaitForSeconds(Random.Range(2f,4f));
            actualSpeed = 1;
            ChargeParts.Stop();
            StartCoroutine(Firelaser());
            yield return new WaitForSeconds(1f);
            transform.DOMove(new Vector3(Random.Range(hozAxes.x, hozAxes.y), Random.Range(verticalAxes.x, verticalAxes.y), 0), 0.5f).SetEase(Ease.OutQuad);
            yield return new WaitForSeconds(0.5f);
        }


        
    }

    IEnumerator Firelaser()
    {
        LaserPrefab.SetActive(true);
        LaserPrefab.transform.localScale = new Vector3(0, 1.45f, 0);
        LaserPrefab.transform.DOScale(new Vector3(0.005f, 1.45f, 0.005f), .5f);
        yield return new WaitForSeconds(0.5f);
        LaserPrefab.transform.DOScale(new Vector3(0, 1.45f, 0), .5f);
        yield return new WaitForSeconds(0.5f);
        LaserPrefab.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (Charging)
        {
            // Aim Offset towards player on Z axis with configurable offset
            Vector3 direction = player.transform.position - Offset.transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, 0, angle + aimOffsetZ);
            Offset.transform.rotation = Quaternion.Slerp(Offset.transform.rotation, targetRotation, Time.deltaTime * actualSpeed);
        }
    }
}
