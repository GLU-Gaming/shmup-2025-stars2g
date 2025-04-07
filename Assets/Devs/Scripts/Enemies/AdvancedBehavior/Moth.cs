using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Buffers.Text;

public class Moth : MonoBehaviour
{
    [SerializeField] private MothBottomMovement mothBottomMovement;

    // Uses bee as a base script but changes the base functionality
    public EntryType entryType;
    [SerializeField] float aimOffsetZ;
    [SerializeField] EnemyHealth health;
    [SerializeField] float lookSpeed;

    float actualSpeed;
    [SerializeField] GameObject offset;
    GameObject player;
    public enum MothState { Idle, SilkAttack }
    public MothState currentState = MothState.Idle;


    // Base Data for locations
    Vector2 hozAxes = new Vector2(5, 21);
    Vector2 verticalAxes = new Vector2(-11, 13);

    bool charging;

    private void Start()
    {
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
            while (true)
            {
                actualSpeed = lookSpeed;

                currentState = (Random.value > 0.1f) ? MothState.SilkAttack : MothState.Idle;

                if (currentState == MothState.SilkAttack)
                {
                    mothBottomMovement.performSilkAttack();
                    yield return new WaitForSeconds(1f);
                    currentState = MothState.Idle;
                }

                transform.DOMove(new Vector3(Random.Range(hozAxes.x -1, hozAxes.y -1), Random.Range(verticalAxes.x -1, verticalAxes.y - 1), 0), 3f).SetEase(Ease.OutQuad);

                yield return new WaitForSeconds(2f);
            }
        }
    }

    private void FixedUpdate()
    {
        if (charging)
        {
            // Aim Offset towards player on Z axis with configurable offset
            Vector3 direction = player.transform.position - offset.transform.position;
        }
    }
}
