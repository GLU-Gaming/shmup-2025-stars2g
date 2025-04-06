using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Net.Sockets;
using System.Collections;

public class QueenBeeHealth : MonoBehaviour
{
    [SerializeField] public float maxHealth = 2500;
    [SerializeField] Image healthBar;
    [SerializeField] Image ghostBar;

    WaveReader waveReader; //to make sure the game can end

    [SerializeField] GameObject bossModel; //Used for the Death
    [SerializeField] GameObject sparksPrefab; //Prefab of the ParticleSystem
    [SerializeField] GameObject explosionPrefab; //Used on Death
    [SerializeField] GameObject bigexplosionPrefab;

    [Header("Score System")]
    [SerializeField] int scoreValue = 1500; //score to give
    [SerializeField] float Multiplier = 1; //Multiplier for the score

    ScoreSystem scoreSystem; //referenced
    //------------------------------------------

    GameObject root;

    public bool invincible = false;

    public float displayedHealth;

    float ghostTimer; //Timer for the ghost bar to disappear

    bool hit = false; //Used to check if the enemy has been hit

    private QueenBeebehaviour queenBeebehaviour;

    [Header("DEBUG ONLY")]
    [SerializeField] bool DEBUGGING = false;
    [SerializeField] float DEBUG_Value;
    [SerializeField] bool DEBUG_Heal;
    [SerializeField] bool DEBUG_Damage;
    private void Start()
    {
        Debug.Log("QueenBeeHealth Initialized - Max Health: " + maxHealth);
        displayedHealth = maxHealth;
        root = transform.root.gameObject;
        scoreSystem = GameObject.FindFirstObjectByType<ScoreSystem>();
        queenBeebehaviour = FindFirstObjectByType<QueenBeebehaviour>();
        waveReader = FindFirstObjectByType<WaveReader>();
    }


    private void Update()
    {
        if (DEBUGGING)
        {
            if (DEBUG_Heal)
            {
                HealEnemy(DEBUG_Value);
                DEBUG_Heal = false;
            }
            if (DEBUG_Damage)
            {
                DamageEnemy(DEBUG_Value);
                DEBUG_Damage = false;
            }
        }

        if (ghostTimer > 0)
        {
            ghostTimer -= Time.deltaTime;
        }
        else
        {
            healthBar.DOFillAmount(displayedHealth / maxHealth, 0.5f);
            ghostBar.DOFillAmount(displayedHealth / maxHealth, 0.5f);
        }

        healthBar.DOFillAmount(displayedHealth / maxHealth, 0.5f);
    }

    private void FixedUpdate()
    {
        if (hit)
        {
            DecreaseMultiplier(); //Slowly Decreases the amount of score you get based on how long you take to kill the enemy
        }

        if (displayedHealth <= 0 && queenBeebehaviour.isDying)
        {
            queenBeebehaviour.TriggerDeath();
        }

        if (maxHealth <= 0 && queenBeebehaviour.isDying)
        {
            queenBeebehaviour.TriggerDeath();
        }
    }

    void DecreaseMultiplier()
    {
        Multiplier -= 0.0001f;
        Multiplier = Mathf.Clamp(Multiplier, .2f, 1);
    }

    public void HealEnemy(float amount)
    {
        displayedHealth += amount;
        displayedHealth = Mathf.Clamp(displayedHealth, 0, maxHealth);
    }

    public void DamageEnemy(float amount)
    {
        if (!invincible)
        {
            ghostTimer = 0.3f;
            displayedHealth -= amount;
            displayedHealth = Mathf.Clamp(displayedHealth, 0, maxHealth);
            InstantiateSparks();
            hit = true;

            // Move UI updates here, after modifying displayedHealth
            healthBar.DOFillAmount(displayedHealth / maxHealth, 0.5f);
            ghostBar.DOFillAmount(displayedHealth / maxHealth, 0.5f);

            if (displayedHealth <= 0)
            {
                EnemyDeath();
            }
        }
    }

    void EnemyDeath()
    {
        // NEW: Tell QueenBeeBehaviour to stop 
        queenBeebehaviour.TriggerDeath();

        // Add Score Immediately
        scoreSystem.AddScore((int)(scoreValue * Multiplier));

        // Start Semi-Cutscene
        StartCoroutine(DeathCutscene());
    }


    IEnumerator DeathCutscene()
    {
        float duration = 4f; // Time before final explosion
        float explosionInterval = 2f; // Time between small explosions
        float elapsed = 0f;

        // Move the boss down slowly
        bossModel.transform.DOMoveY(bossModel.transform.position.y - 105f, duration);

        // Add shaking effect
        bossModel.transform.DOShakePosition(duration, 0.5f, 10, 90, false, true);

        // Spawn small explosions randomly on the boss
        while (elapsed < duration)
        {
            Vector3 randomOffset = new Vector3(
                Random.Range(-10f, 10f), // Random X
                Random.Range(-10f, 10f), // Random Y
                Random.Range(-0f, 0f)  // none for Z
            );

            Instantiate(explosionPrefab, bossModel.transform.position + randomOffset, Quaternion.identity);
            elapsed += explosionInterval;
            yield return new WaitForSeconds(explosionInterval);
        }

        // Final Big Explosion
        Instantiate(bigexplosionPrefab, bossModel.transform.position, Quaternion.identity);

        // Destroy the boss
        Destroy(root);
        waveReader.EndLevelEarly();
    }

    void InstantiateSparks()
    {
        GameObject sparksInstance = Instantiate(sparksPrefab, bossModel.transform);
        sparksInstance.transform.localPosition = new Vector3(0, 0, -1.25f);
        sparksInstance.transform.parent = null;
        sparksInstance.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
        sparksInstance.transform.rotation = Quaternion.Euler(0, 0, 0);

    }
}
