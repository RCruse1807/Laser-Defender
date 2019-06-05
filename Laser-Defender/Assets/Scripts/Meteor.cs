using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    [Header("Meteor Config")]
    [SerializeField] private float health = 600;
    [SerializeField] private GameObject destroyVFX;
    [SerializeField] private GameObject powerUpPrefab;
    [SerializeField] private AudioClip destroySFX;
    

    [Header("Script Config")]
    [SerializeField] private float explosionDuration = 1f;
    [SerializeField] private float powerUpDropSpeed = 5f;
    [Range(0f, 1f)][SerializeField] private float destroySFXVolume = 0.2f;
    


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
        Destroy(gameObject);
        GameObject destroyMeteorVFX = Instantiate(destroyVFX, transform.position, Quaternion.identity) as GameObject;
        Destroy(destroyMeteorVFX, explosionDuration);
        AudioSource.PlayClipAtPoint(destroySFX, Camera.main.transform.position, destroySFXVolume);
        SpawnPowerUp();

    }

    private void SpawnPowerUp()
    {
        GameObject healthPowerUpDrop =  Instantiate(powerUpPrefab, transform.position, Quaternion.identity) as GameObject;
        healthPowerUpDrop.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -powerUpDropSpeed);

        
    }

    


}
