using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    [SerializeField] private float speedOfSpin = 1f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, 0f, speedOfSpin * Time.deltaTime);
    }
}
