using DG.Tweening;
using System.Collections;
using UnityEngine;

public class WaveReader : MonoBehaviour
{
    [SerializeField] AdvancedWave waveCollection;
    [SerializeField] EnemyRegistry registry;
    [SerializeField] GameObject finishline;
    [SerializeField] GameObject BossSpawner;
    [SerializeField] bool EndwithBoss;

    Transitions Transitions;

    public int enemiesToKill;
    int currentWave = 0;

    bool waitForEnemies;
    bool debounce = true;

    int expectedEnemies = 0;

    public void EndLevelEarly(bool Toggle)
    {
        StartCoroutine(EndFinishline(Toggle));
    }

    private void Start()
    {
        StartCoroutine(InitWaves());
        Transitions = GameObject.FindWithTag("Transitionmanager").GetComponent<Transitions>();
        Transitions.SetTransition(false);
    }

    IEnumerator InitWaves()
    {
        yield return new WaitForSeconds(3);
        currentWave = 0;
        StartCoroutine(LoadEnemyWave(0));
    }

    private void FixedUpdate()
    {
        enemiesToKill = Mathf.Clamp(enemiesToKill, 0, int.MaxValue);

        if (enemiesToKill == 0 && waitForEnemies && !debounce)
        {
            debounce = true;
            currentWave++;
            if (currentWave < waveCollection.waveCollection.Count)
            {
                StartCoroutine(DelaySpawn(waveCollection.waveCollection[currentWave].waveDelay, currentWave));
            }
            else
            {
                StartCoroutine(DelaySpawn(waveCollection.waveCollection[currentWave - 1].waveDelay, currentWave));
            }
        }
    }

    IEnumerator DelaySpawn(float time, int wave, bool isNext = false)
    {
        yield return new WaitForSeconds(time);
        if (isNext)
        {
            currentWave++;
            StartCoroutine(LoadEnemyWave(currentWave));
        }
        else
        {
            StartCoroutine(LoadEnemyWave(wave));
        }
    }

    IEnumerator LoadEnemyWave(int wave)
    {
        print("Loading Wave: " + wave + " out of " + waveCollection.waveCollection.Count);
        if (wave >= waveCollection.waveCollection.Count)
        {
            if (EndwithBoss)
            {
                Instantiate(BossSpawner, Vector3.zero, Quaternion.identity);
                //Will rely on the boss to end the level
            }
            else
            {
                StartCoroutine(EndFinishline());
            }
                
        }
        else
        {
            expectedEnemies = waveCollection.waveCollection[wave].enemies.Count;
            foreach (AdvancedWave.Enemy enemy in waveCollection.waveCollection[wave].enemies)
            {
                StartCoroutine(SpawnEnemy(enemy.Type, enemy.SpawnDelay, enemy.entryType));
            }

            if (waveCollection.waveCollection[wave].waveType == WaveType.WaitForEnemies)
            {
                waitForEnemies = true;
            }
            else
            {
                StartCoroutine(DelaySpawn(waveCollection.waveCollection[wave].waveDelay, wave));
            }

            yield return new WaitUntil(() => enemiesToKill == 0);
            debounce = false;
        }
    }

    IEnumerator EndFinishline(bool Instant = false)
    {
        GameObject line = Instantiate(finishline, Vector3.zero, Quaternion.identity);
        Transform lineTrans = line.transform.Find("finish line");
        lineTrans.gameObject.SetActive(true);
        if (!Instant)
        {
            lineTrans.transform.localScale = new Vector3(1.46f, 1, 1);
            lineTrans.transform.DOMoveX(-25, 4f);
            yield return null;
        } else
        {
            lineTrans.GetComponent<Finish>().PlayerWin();
        }
    }

    IEnumerator SpawnEnemy(int type, float delay, EntryType entry)
    {
        yield return new WaitForSeconds(delay);
        GameObject newEnemy = Instantiate(registry.Enemies[type], new Vector3(100, 100, 0), Quaternion.identity);
        enemiesToKill++;
        EnemyHealth health = newEnemy.transform.Find("HealthUI").GetComponent<EnemyHealth>();
        health.RegisterKilled = true;
        switch (type)
        {
            case 0: // Bee
                newEnemy.GetComponent<Bee>().entryType = entry;
                break;
            case 1: // Mosquito
                newEnemy.GetComponent<Mosquito>().entryType = entry;
                break;
            case 2: // Beetle, works on same script as Bee
                newEnemy.GetComponent<Bee>().entryType = entry;
                break;
        }
    }
}
