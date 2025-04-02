using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Net.Sockets;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float maxHealth = 100;
    [SerializeField] Image healthBar;
    [SerializeField] Image ghostBar;

    [SerializeField] GameObject enemyModel; //Used for the Death
    [SerializeField] GameObject sparksPrefab; //Prefab of the ParticleSystem
    [SerializeField] GameObject explosionPrefab; //Used on Death

    [SerializeField] PowerupRegist Powerups;

    [Header("Score System")]
    [SerializeField] int scoreValue = 100; //score to give
    [SerializeField] float Multiplier = 1; //Multiplier for the score

    

    ScoreSystem scoreSystem; //referenced
    WaveReader waveReader; //referenced
    //------------------------------------------

    GameObject root;

    public bool invincible = false;

    float displayedHealth;

    float ghostTimer; //Timer for the ghost bar to disappear

    bool hit = false; //Used to check if the enemy has been hit

    [Header("DEBUG ONLY")]
    [SerializeField] bool DEBUGGING = false;
    [SerializeField] float DEBUG_Value;
    [SerializeField] bool DEBUG_Heal;
    [SerializeField] bool DEBUG_Damage;
    private void Start()
    {
        displayedHealth = maxHealth;
        root = transform.root.gameObject;
        scoreSystem = GameObject.FindFirstObjectByType<ScoreSystem>();
        waveReader = GameObject.FindFirstObjectByType<WaveReader>();
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
            if (displayedHealth <= 0)
            {
                EnemyDeath();
            }
        }
    }

    void EnemyDeath()
    {
        if (Random.Range(0, 100) < 20)
        {
            Instantiate(Powerup(), enemyModel.transform.position, Quaternion.identity);
        }
        scoreSystem.AddScore((int)(scoreValue * Multiplier));
        Instantiate(explosionPrefab, enemyModel.transform.position, Quaternion.identity);
        waveReader.enemiesToKill--;
        Destroy(root);
    }

    void InstantiateSparks()
    {
        GameObject sparksInstance = Instantiate(sparksPrefab, enemyModel.transform);
        sparksInstance.transform.localPosition = new Vector3(0, 0, -1.25f);
        sparksInstance.transform.parent = null;
        sparksInstance.transform.localScale = new Vector3(0.75f,0.75f,0.75f);
        sparksInstance.transform.rotation = Quaternion.Euler(0, 0, 0);

    }

    GameObject Powerup()
    {
        return Powerups.powerups[Random.Range(0, Powerups.powerups.Length)];
    }
}
