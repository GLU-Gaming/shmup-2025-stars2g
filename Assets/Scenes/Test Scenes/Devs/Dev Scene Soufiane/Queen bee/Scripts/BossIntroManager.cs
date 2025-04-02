using UnityEngine;
using System.Collections;

public class BossIntroManager : MonoBehaviour
{
    public GameObject introPrefab;  // The intro animation prefab
    public GameObject bossPrefab;   // The final boss prefab
    public GameObject[] objectsToHide; // Objects to hide during intro
    public float introCooldown = 10f;
    public float appearCooldown = 5f;
    private bool hasStartedIntro = false;

    void Update()
    {
        if (!hasStartedIntro)
        {
            introCooldown -= Time.deltaTime;  // Reduce cooldown over time

            if (introCooldown <= 0f)
            {
                hasStartedIntro = true;  // Prevent multiple calls
                StartCoroutine(PlayIntro());
            }
        }
    }

    IEnumerator PlayIntro()
    {
        // Hide objects
        foreach (GameObject obj in objectsToHide)
        {
            if (obj != null)
                obj.SetActive(false);
        }

        // Spawn intro animation
        GameObject introInstance = Instantiate(introPrefab, transform.position, Quaternion.identity);

        // Wait for a few seconds
        yield return new WaitForSeconds(5f);

        // Wait until the intro prefab is destroyed
        if (appearCooldown >= 0f)
        {
            appearCooldown -= Time.deltaTime;
        }
        while (appearCooldown <= 0f)
        {
            yield return null;
        }

        // Show everything back
        foreach (GameObject obj in objectsToHide)
        {
            if (obj != null)
                obj.SetActive(true);
        }

        // Spawn boss
        GameObject boss = Instantiate(bossPrefab, transform.position, Quaternion.identity);
        QueenBeebehaviour bossBehavior = boss.GetComponent<QueenBeebehaviour>();

        if (bossBehavior != null)
        {
            bossBehavior.state = "Idle"; // Start boss logic
        }
    }
}
