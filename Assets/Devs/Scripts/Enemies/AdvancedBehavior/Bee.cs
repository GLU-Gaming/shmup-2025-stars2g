using System.Collections;
using UnityEngine;
using DG.Tweening;
public class Bee : MonoBehaviour
{
    [SerializeField] EntryType entryType;
    [SerializeField] float aimOffsetZ;
    [SerializeField] EnemyHealth health;
    [SerializeField] float speed = 50f;

    GameObject offset;
    GameObject player;

    // Base Data for locations
    Vector2 hozAxes = new Vector2(5, 21);
    Vector2 verticalAxes = new Vector2(-11, 13);

    bool charging;
    private enum EntryType
    {
        bottom,
        top,
        edge
    }

    private void Start()
    {
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
        charging = true;
        // Aim Offset towards player on Z axis with configurable offset
        Vector3 direction = player.transform.position - offset.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        offset.transform.rotation = Quaternion.Euler(0, 0, angle + aimOffsetZ);
        // Calculate the target position beyond the player
        Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
        Vector3 targetPosition = player.transform.position + directionToPlayer * 50f; // Move 5 units beyond the player
        // Move towards the target position
        float distanceToTarget = (targetPosition - transform.position).magnitude;
        float moveDuration = distanceToTarget / speed; // Adjust speed as needed
        void Num()
        {
            StartCoroutine(CompleteNum());
        }
        IEnumerator CompleteNum()
        {
            yield return new WaitForSeconds(0.1f);
            entryType = EntryType.edge;
            charging = false;
            StartCoroutine(Animate());
        }

        transform.DOMove(targetPosition, moveDuration).SetEase(Ease.Linear).OnComplete(Num);
    }

    private void FixedUpdate()
    {
        if (charging)
        {
            if (transform.position.x < player.transform.position.x + 2)
            {
                health.invincible = true;
            }
            else
            {
                health.invincible = false;
            }
        }
    }
}
