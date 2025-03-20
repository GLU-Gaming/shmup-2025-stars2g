using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float maxHealth = 100;
    [SerializeField] Image healthBar;
    [SerializeField] Image ghostBar;

    [SerializeField] GameObject enemyModel; //Used for the Death
    [SerializeField] GameObject sparksPrefab; //Prefab of the ParticleSystem
    [SerializeField] GameObject explosionPrefab; //Used on Death

    GameObject root;

    float displayedHealth;

    float ghostTimer; //Timer for the ghost bar to disappear

    [Header("DEBUG ONLY")]
    [SerializeField] bool DEBUGGING = false;
    [SerializeField] float DEBUG_Value;
    [SerializeField] bool DEBUG_Heal;
    [SerializeField] bool DEBUG_Damage;
    private void Start()
    {
        displayedHealth = maxHealth;
        root = transform.root.gameObject;
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

    public void HealEnemy(float amount)
    {
        displayedHealth += amount;
        displayedHealth = Mathf.Clamp(displayedHealth, 0, maxHealth);
    }

    public void DamageEnemy(float amount)
    {
        ghostTimer = 0.3f;
        displayedHealth -= amount;
        displayedHealth = Mathf.Clamp(displayedHealth, 0, maxHealth);
        InstantiateSparks();
        if (displayedHealth <= 0)
        {
            EnemyDeath();
        }
    }

    void EnemyDeath()
    {
        Instantiate(explosionPrefab, enemyModel.transform.position, Quaternion.identity);
        Destroy(root);
    }

    void InstantiateSparks()
    {
        GameObject sparksInstance = Instantiate(sparksPrefab, enemyModel.transform);
        sparksInstance.transform.localPosition = new Vector3(0, 0, -1.25f);
        sparksInstance.transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
