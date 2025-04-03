using UnityEngine;
using System.Collections;

public class BossIntroManager : MonoBehaviour
{
    public GameObject introPrefab;  // The intro animation prefab
    public GameObject bossPrefab;   // The final boss prefab
    public GameObject warningPrefab;
    public GameObject[] objectsToHide; // Objects to hide during intro
    public float appearDelay = 2f;
    public float introCooldown = 0.2f;
    public float warningCooldown = 4.5f;
    public float appearCooldown = 5f;
    private bool hasStartedIntro = false;
    private bool hasStartedWarning = false;

    void Update()
    {
        if (!hasStartedIntro)
        {
            if (appearDelay > 0)
            {
                appearDelay -= Time.deltaTime;
            }

            if (appearDelay <= 0f && !hasStartedWarning)
            {
                hasStartedWarning = true;
                StartCoroutine(PlayWarning());
            }

            if (hasStartedWarning) // Only start decrementing after warning starts
            {
                warningCooldown -= Time.deltaTime;
            }

            if (warningCooldown <= 0f)
            {
                introCooldown -= Time.deltaTime;
            }

            if (introCooldown <= 0f && hasStartedWarning)
            {
                hasStartedIntro = true;  // Prevent multiple calls
                StartCoroutine(PlayIntro());
            }
        }
    }
    IEnumerator PlayWarning()
    {
        // Spawn intro animation
        GameObject warningInstance = Instantiate(warningPrefab, transform.position + new Vector3(-2.8f, 5f ,4.5f), Quaternion.identity);

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
        GameObject introInstance = Instantiate(introPrefab, transform.position + new Vector3(20.51f, 0.4f, 0), Quaternion.identity);

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
        GameObject boss = Instantiate(bossPrefab, transform.position + new Vector3(20.51f, 0.4f, 0), Quaternion.identity);
        QueenBeebehaviour bossBehavior = boss.GetComponent<QueenBeebehaviour>();

        if (bossBehavior != null)
        {
            bossBehavior.state = "Idle"; // Start boss logic
        }
    }
}
