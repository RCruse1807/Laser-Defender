using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Config Parameters")]
    [SerializeField] private float health = 300;
    [SerializeField] private GameObject deathVFX;
    [SerializeField] private AudioClip deathSFX;
    [SerializeField] private int scoreValue = 150;

    [Header("Enemy Projectile Fire Rate Config")]
    [SerializeField] private float shotCounter;
    [SerializeField] private float minTimeBetweenShots = 0.2f;
    [SerializeField] private float maxTimeBetweenShots = 3f;

    [Header("Enemy Projectile Config")]
    [SerializeField] private float enemyProjectileSpeed = 1f;
    [SerializeField] private GameObject enemyProjectile;
    [SerializeField] private AudioClip projectileSXF;

    [Header("Config Parameters")]
    [SerializeField] private float explosionDuration = 1f;
    [Range(0f, 1f)][SerializeField] private float deathSoundVolume = 0.7f;
    [Range(0f, 1f)][SerializeField] private float projectileSoundVolume = 0.7f;
    

    


    
    // Start is called before the first frame update
    void Start()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;

        if(shotCounter <= 0)
        {
            Fire();
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }


    private void Fire()
    {
        GameObject enemyLaser = Instantiate(enemyProjectile, transform.position, Quaternion.identity) as GameObject;
        enemyLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -enemyProjectileSpeed);
        AudioSource.PlayClipAtPoint(projectileSXF, Camera.main.transform.position, projectileSoundVolume);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer)
        {
            return;
        }
        ProcessHit(damageDealer);
        

    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        FindObjectOfType<GameSession>().AddToScore(scoreValue);
        Destroy(gameObject);
        GameObject enemyExplosionVFX = Instantiate(deathVFX, transform.position, Quaternion.identity) as GameObject;
        Destroy(enemyExplosionVFX, explosionDuration);
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSoundVolume);

    }
}
