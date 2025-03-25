using System.Collections;
using UnityEngine;

public class SimpleWave : MonoBehaviour
{
    [SerializeField] EnemyRegistry enemyRegistry; //Contains all the enemies that can be spawned

    private void Start()
    {
        StartCoroutine(WaveSpawner());
    }

    IEnumerator WaveSpawner()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(2, 5));
            Instantiate(SelectEnemy());
        }
    }

    GameObject SelectEnemy()
    {
        GameObject ChosenEnemy;
        ChosenEnemy = enemyRegistry.Enemies[Random.Range(0, enemyRegistry.Enemies.Length)];

        return ChosenEnemy;
    }
}
