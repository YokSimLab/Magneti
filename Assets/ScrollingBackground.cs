using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollingBackground : MonoBehaviour
{
    [SerializeField] private GameObject camera;
    [SerializeField] private Rigidbody2D playerRB;
    [SerializeField] private SpriteRenderer image;
    Material material;

    [SerializeField] private float velocityMultiplier;
    private float u, v;


    private void Awake()
    {
        playerRB = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        image = GetComponent<SpriteRenderer>();
        material = image.material;

        velocityMultiplier *= 0.0001f;
    }

    void Update()
    {
        transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y, 0);

        u = playerRB.velocity.x * velocityMultiplier;
        v = playerRB.velocity.y * velocityMultiplier;

        material.mainTextureOffset = material.mainTextureOffset + new Vector2(u, v);
    }
}