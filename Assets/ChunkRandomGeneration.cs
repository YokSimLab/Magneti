using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkRandomGeneration : MonoBehaviour
{
    [Range(1, 133420)]
    [SerializeField] private int seed = 0;

    [Range(0, 1)]
    [SerializeField] private float minimumProbability = 0.9f;

    [SerializeField] private ChunkManager chunk;
    [SerializeField] private GameObject magnet;
    [SerializeField] private float rows = 10;
    [SerializeField] private float columns = 10;

    private readonly List<Vector2> markedSpots = new List<Vector2>();

    private void Awake()
    {
        //Pairing Function
        int uniqueValue = (int)(0.5 * (transform.position.x + transform.position.y)
                            * (1 + transform.position.x + transform.position.y)
                            + transform.position.y);
        Random.InitState(seed + uniqueValue * seed);

        for (int i = 0; i <= columns; i++)
        {
            for (int j = 0; j <= rows; j++)
            {
                if (!markedSpots.Contains(new Vector2(i, j)))
                {
                    float probability = Random.Range(0f, 1f);
                    if (probability >= minimumProbability)
                    {
                        Vector3 magnetPosition = new(transform.position.x + i - (columns / 2),
                                                        transform.position.y + j - (rows / 2), 0);
                        Instantiate(magnet, magnetPosition, new Quaternion());

                        markedSpots.Add(new Vector2(i - 1, j + 1));
                        markedSpots.Add(new Vector2(i, j + 1));
                        markedSpots.Add(new Vector2(i + 1, j + 1));
                        markedSpots.Add(new Vector2(i - 1, j));
                        markedSpots.Add(new Vector2(i, j));
                        markedSpots.Add(new Vector2(i + 1, j));
                        markedSpots.Add(new Vector2(i - 1, j - 1));
                        markedSpots.Add(new Vector2(i, j - 1));
                        markedSpots.Add(new Vector2(i + 1, j - 1));
                    }
                }
            }
        }
    }
}
