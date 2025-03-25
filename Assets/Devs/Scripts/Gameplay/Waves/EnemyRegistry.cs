using UnityEngine;


[CreateAssetMenu(fileName = "Enemy Registry", menuName = "ScriptableObjects/EnemyRegistry", order = 1)]
public class EnemyRegistry : ScriptableObject
{
    public GameObject[] Enemies;
}
