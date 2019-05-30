using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Config Parameters
    [Header("Player Config Parameters")]
    [SerializeField] private float playerSpeed = 10f;
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private float projectileFiringPeriod = 0.1f;
    [SerializeField] private GameObject laserProjectile;
    
    [Header("Config Parameters")]
    [SerializeField] private float screenPadding = 1f;


    Coroutine firingCoroutine;

    private float xMin;
    private float xMax;
    private float yMin;
    private float yMax;


    // Cached Reference

    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

    private void Move()
    {
        float deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * playerSpeed;
        float newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
       
        float deltaY = Input.GetAxis("Vertical") * Time.deltaTime * playerSpeed;
        float newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector2(newXPos, newYPos);
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }

        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }

       
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0f, 0f, 0f)).x + screenPadding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1f, 0f, 0f)).x - screenPadding;

        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0f, 0f, 0f)).y + screenPadding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0f, 1f, 0f)).y - screenPadding;

    }


    IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject laser = Instantiate(laserProjectile, transform.position, Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, projectileSpeed);
            yield return new WaitForSeconds(projectileFiringPeriod);
        }  
    }
}
