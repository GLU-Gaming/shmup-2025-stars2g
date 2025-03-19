using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BulletIndex", menuName = "Bullet Index")]
public class BulletIndex : ScriptableObject
{
    public List<BulletType> bulletTypes;

    [System.Serializable]
    public class BulletType
    {
        public string bulletName;
        public GameObject bulletPrefab;
    }
}
