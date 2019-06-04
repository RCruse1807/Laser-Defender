using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] private float backgroundSpeed = 1f;

    private Material material;
    private Vector2 offset;

    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<Renderer>().material;
        offset = new Vector2(0f, backgroundSpeed);
        
    }

    // Update is called once per frame
    void Update()
    {
        material.mainTextureOffset += offset * Time.deltaTime;
        
    }
}
