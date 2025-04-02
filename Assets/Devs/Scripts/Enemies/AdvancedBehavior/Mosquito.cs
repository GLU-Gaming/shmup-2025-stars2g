using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Mosquito : MonoBehaviour
{
    // Uses bee as a base script but changes the base functionality

    public EntryType entryType;
    [SerializeField] float aimOffsetZ;
    [SerializeField] EnemyHealth health;
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float lookSpeed;
    [SerializeField] ParticleSystem chargeParts;
    [SerializeField] GameObject LaserIndicator;
    Transform IndicatorTrans;
    SpriteRenderer IndicatorSprite;

    float actualSpeed;

    [SerializeField] float laserOffsetRotation = 90;

    GameObject offset;
    GameObject player;

    // Base Data for locations
    Vector2 hozAxes = new Vector2(5, 21);
    Vector2 verticalAxes = new Vector2(-11, 13);

    bool charging;

    private void Start()
    {
        IndicatorTrans = LaserIndicator.transform;
        IndicatorSprite = LaserIndicator.GetComponent<SpriteRenderer>();
        offset = transform.Find("Offset").gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(Animate());
    }

    IEnumerator Animate()
    {
        // Entry Animation
        offset.transform.localRotation = Quaternion.Euler(0, 0, -180);
        switch (entryType)
        {
            case EntryType.bottom:
                transform.position = new Vector3(Random.Range(hozAxes.x, hozAxes.y), -15, 0);
                transform.rotation = Quaternion.Euler(0, 0, -20);
                float height = Random.Range(-10, 10);
                float distanceToFly = Mathf.Abs(-15 - height);
                float rotateTime = (Random.Range(1.5f, 1.7f) * (distanceToFly / 10));
                transform.DOMoveY(height, (Random.Range(1.4f, 1.7f) * (distanceToFly / 10))).SetEase(Ease.OutBack);
                transform.DORotate(new Vector3(0, 0, 5), rotateTime).SetEase(Ease.OutBack);
                yield return new WaitForSeconds(0.4f);
                health.invincible = false;
                yield return new WaitForSeconds(rotateTime - 0.4f);
                break;
            case EntryType.top:
                transform.position = new Vector3(Random.Range(hozAxes.x, hozAxes.y), 17, 0);
                transform.rotation = Quaternion.Euler(0, 0, -20);
                float height2 = Random.Range(-10, 10);
                float distanceToFly2 = Mathf.Abs(-15 - height2);
                float rotateTime2 = (Random.Range(1.5f, 1.7f) * (distanceToFly2 / 10));
                transform.DOMoveY(height2, (Random.Range(1.4f, 1.7f) * (distanceToFly2 / 10))).SetEase(Ease.OutBack);
                transform.DORotate(new Vector3(0, 0, 5), rotateTime2).SetEase(Ease.OutBack);
                yield return new WaitForSeconds(0.4f);
                health.invincible = false;
                yield return new WaitForSeconds(rotateTime2 - 0.4f);
                break;
            case EntryType.edge:
                transform.position = new Vector3(28, Random.Range(verticalAxes.x, verticalAxes.y), 0);
                transform.rotation = Quaternion.Euler(0, 0, 20);
                transform.DOMoveX(Random.Range(hozAxes.x, hozAxes.y), 2f).SetEase(Ease.OutBack);
                transform.DORotate(new Vector3(0, 0, 0), 2f).SetEase(Ease.OutBack);
                yield return new WaitForSeconds(0.4f);
                health.invincible = false;
                yield return new WaitForSeconds(1.6f);
                break;
        }
        while (true)
        {
            actualSpeed = lookSpeed;
            charging = true;
            chargeParts.Play();
            IndicatorTrans.DOScaleX(0.01f, 1f);
            yield return new WaitForSeconds(Random.Range(2f, 4f) -1);
            IndicatorSprite.color = new Color(1, 0.05747497f, 0, 0.2117647f);
            IndicatorSprite.DOColor(new Color(0.01886177f, 1, 0, 0.2117647f), 0.5f);
            yield return new WaitForSeconds(.5f);
            IndicatorSprite.color = new Color(1, 0.05747497f, 0, 0.2117647f);
            IndicatorSprite.DOColor(new Color(0.01886177f, 1, 0, 0.2117647f), 0.5f);
            yield return new WaitForSeconds(.5f);
            actualSpeed = 1;
            chargeParts.Stop();
            StartCoroutine(FireLaser());
            IndicatorTrans.DOScaleX(0f, 1f);
            yield return new WaitForSeconds(1f);
            transform.DOMove(new Vector3(Random.Range(hozAxes.x, hozAxes.y), Random.Range(verticalAxes.x, verticalAxes.y), 0), 0.5f).SetEase(Ease.OutQuad);
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator FireLaser()
    {
        laserPrefab.SetActive(true);
        laserPrefab.transform.localScale = new Vector3(0, 1.25f, 0);
        laserPrefab.transform.DOScale(new Vector3(0.001f, 1.25f, 0.001f), .2f);
        yield return new WaitForSeconds(0.5f);
        laserPrefab.transform.DOScale(new Vector3(0, 1.25f, 0), .5f);
        yield return new WaitForSeconds(0.5f);
        laserPrefab.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (charging)
        {
            // Aim Offset towards player on Z axis with configurable offset
            Vector3 direction = player.transform.position - offset.transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, 0, angle + aimOffsetZ);
            offset.transform.rotation = Quaternion.Slerp(offset.transform.rotation, targetRotation, Time.deltaTime * actualSpeed);
        }
    }
}
