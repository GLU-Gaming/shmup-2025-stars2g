using System.Collections;
using UnityEngine;

public class WaveReader : MonoBehaviour
{

     [SerializeField] AdvancedWave waveCollection;
    EnemyRegistry registry;

    public int enemiesToKill;

    private void FixedUpdate()
    {
        enemiesToKill = Mathf.Clamp(enemiesToKill, 0, int.MaxValue);
    }

    void LoadEnemyWave(int wave)
    {
        foreach(AdvancedWave.Enemy enemy in waveCollection.waveCollection[wave].enemies)
        {
            StartCoroutine(spawnEnemy(enemy.Type, enemy.SpawnDelay, enemy.entryType));
        }
    }

    IEnumerator spawnEnemy(int Type, float Delay, EntryType entry)
    {
        yield return new WaitForSeconds(Delay);
        GameObject newEnemy = Instantiate(registry.Enemies[Type], new Vector3(100,100,0), Quaternion.identity);
        enemiesToKill++;
        switch (Type)
        {
            case 0: // Bee
                newEnemy.GetComponent<Bee>().entryType = entry;
                break;
            case 1: // Mosquito
                newEnemy.GetComponent<Mosquito>().entryType = entry;
                break;
            case 2: //Beetle, works on same script as Bee
                newEnemy.GetComponent<Bee>().entryType = entry;
                break;
        }
    }

}
