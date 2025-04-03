using UnityEngine;
using System.Collections;

public class BossIntroManager : MonoBehaviour
{
    public GameObject introPrefab;  // The intro animation prefab
    public GameObject bossPrefab;   // The final boss prefab
    public GameObject warningPrefab;
    public GameObject[] objectsToHide; // Objects to hide during intro
    public float introCooldown = 0.2f;
    public float warningCooldown = 4.5f;
    public float appearCooldown = 5f;
    private bool hasStartedIntro = false;
    private bool hasStartedWarning = false;

    void Update()
    {
        if (!hasStartedIntro)
        {
            if(warningCooldown >= 0f)
            {
                warningCooldown -= Time.deltaTime;
            }
            if (warningCooldown <= 0f)
            {
                introCooldown -= Time.deltaTime;
            }
            if (introCooldown <= 0f)
            {
                hasStartedIntro = true;  // Prevent multiple calls
                StartCoroutine(PlayIntro());
            }
        }
        if (!hasStartedWarning)
        {
            hasStartedWarning = true;
            StartCoroutine(PlayWarning());
        }
    }
    IEnumerator PlayWarning()
    {
        // Spawn intro animation
        GameObject warningInstance = Instantiate(warningPrefab, transform.position + new Vector3(-7.9f, 8.2f ,4.5f ), Quaternion.identity);

        // Wait for a few seconds
        yield return new WaitForSeconds(5f);
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
        GameObject introInstance = Instantiate(introPrefab, transform.position + new Vector3(23.5f, 0.4f, 0), Quaternion.identity);

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
        GameObject boss = Instantiate(bossPrefab, transform.position + new Vector3(23.5f, 0.4f, 0), Quaternion.identity);
        QueenBeebehaviour bossBehavior = boss.GetComponent<QueenBeebehaviour>();

        if (bossBehavior != null)
        {
            bossBehavior.state = "Idle"; // Start boss logic
        }
    }
}
