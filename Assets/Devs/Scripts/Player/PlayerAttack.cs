using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] Transform firePoint;
    public float fireRate = 5; // Per Second

    [SerializeField] BulletIndex bulletIndex;

    GameObject activeBullet; //Prefab that's being fired
    string bulletName; //Name of the bullet being fired

    float rateAccordingToTime;
    float fireTimer;

    public bool canShoot = true;

    AudioSource FireSound;

    private void Start()
    {
        SwitchBullet(0); //Default bullet
        FireSound = GetComponent<AudioSource>();
    }
    void FixedUpdate()
    {
        rateAccordingToTime = 1 / fireRate;

        if (canShoot && fireTimer <= 0)
        {
            fireTimer = rateAccordingToTime;
            Shoot();
        }
        else if (fireTimer > 0)
        {
            fireTimer -= Time.fixedDeltaTime;
        }
    }

    public void SwitchBullet(int index)
    {
        activeBullet = bulletIndex.bulletTypes[index].bulletPrefab;
        bulletName = bulletIndex.bulletTypes[index].bulletName; //To Identify the bullet
    }

    public virtual void Shoot()
    {
        switch (bulletName.ToLower())
        {
            case "basic":
                GameObject firedbullet = Instantiate(activeBullet, firePoint.position, firePoint.rotation);
                firedbullet.GetComponent<BasicBullet>().direction = new Vector3(1,0,0);
                break;
            case "split":
                GameObject firedbullet1 = Instantiate(activeBullet, firePoint.position, firePoint.rotation);
                firedbullet1.GetComponent<BasicBullet>().direction = new Vector3(1, 0, 0);
                GameObject firedbullet2 = Instantiate(activeBullet, firePoint.position, firePoint.rotation);
                firedbullet2.GetComponent<BasicBullet>().direction = new Vector3(1, .1f, 0);
                GameObject firedbullet3 = Instantiate(activeBullet, firePoint.position, firePoint.rotation);
                firedbullet3.GetComponent<BasicBullet>().direction = new Vector3(1, -.1f, 0);
                break;
                //cases should be lowercase
        }
        FireSound.Stop();
        FireSound.Play();
    }
}
