using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float maxHealth = 100;
    [SerializeField] Image healthBar;
    [SerializeField] Image ghostBar;

    [SerializeField] GameObject player; //Used for the Death
    [SerializeField] GameObject deathUI; //UI to enable on Death

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
    }

    private void Update()
    {
        if (DEBUGGING)
        {
            if (DEBUG_Heal)
            {
                HealPlayer(DEBUG_Value);
                DEBUG_Heal = false;
            }
            if (DEBUG_Damage)
            {
                DamagePlayer(DEBUG_Value);
                DEBUG_Damage = false;
            }
        }

        if(ghostTimer >0)
        {
            ghostTimer -= Time.deltaTime;
        } else
        {
            ghostBar.DOFillAmount(displayedHealth / maxHealth, 0.5f);
        }

            healthBar.DOFillAmount(displayedHealth / maxHealth, 0.5f);
    }

    public void HealPlayer(float amount)
    {
        displayedHealth += amount;
        displayedHealth = Mathf.Clamp(displayedHealth, 0, maxHealth);
    }

    public void DamagePlayer(float Amount)
    {
        ghostTimer = .7f;
        displayedHealth -= Amount;
        displayedHealth = Mathf.Clamp(displayedHealth, 0, maxHealth);
        if(displayedHealth <= 0 && deathUI.activeInHierarchy == false)
        {
            PlayerDeath();
        }
    }

    void PlayerDeath()
    {
        player.SetActive(false);
        deathUI.SetActive(true);
    }
}
