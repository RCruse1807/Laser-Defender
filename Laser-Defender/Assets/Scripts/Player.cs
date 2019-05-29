using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private float playerSpeed = 10f;
    [SerializeField] private float screenPadding = 1f;

    private float xMin;
    private float xMax;
    private float yMin;
    private float yMax;

    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0f, 0f, 0f)).x + screenPadding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1f, 0f, 0f)).x - screenPadding;

        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0f, 0f, 0f)).y + screenPadding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0f, 1f, 0f)).y - screenPadding;

    }

    // Update is called once per frame
    void Update()
    {
        MoveMent();
    }

    private void MoveMent()
    {
        float deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * playerSpeed;
        float newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
       
        float deltaY = Input.GetAxis("Vertical") * Time.deltaTime * playerSpeed;
        float newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector2(newXPos, newYPos);
    }
}
