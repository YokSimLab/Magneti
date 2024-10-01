using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkRandomGeneration : MonoBehaviour
{
    [SerializeField] private ChunkManager chunk;
    [SerializeField] private GameObject magnet;
    [SerializeField] private float rows = 10;
    [SerializeField] private float columns = 10;
    [SerializeField] private float averageMagnetsPerChunk = 3;

    private float minimumProbability = 0.97f;

    private readonly List<Vector2> markedSpots = new List<Vector2>();

    private int randomState = -1;

    private void Awake()
    {
        minimumProbability = 1 - (2 * averageMagnetsPerChunk / (rows * columns));

        int uniqueValue = (int)(0.5 * (transform.position.x + transform.position.y)
            * (1 + transform.position.x + transform.position.y)
            + transform.position.y);

        if ((randomState = TryGetChunkRandomState(uniqueValue)) == -1)
        {
            randomState = AddChunkRandomState(uniqueValue);
        }

        int seed = GameManager.Instance.seed;
        GenerateMagnets(seed + seed * randomState);
    }

    private int TryGetChunkRandomState(int uniqueValue)
    {
        bool doesExist = GameManager.Instance.chunkUniqueValueToRandomState.TryGetValue(uniqueValue, out int randomState);

        return doesExist ? randomState : -1;
    }

    private int AddChunkRandomState(int uniqueValue)
    {
        int randomState = GameManager.Instance.chunkUniqueValueToRandomState.Count;
        GameManager.Instance.chunkUniqueValueToRandomState.Add(uniqueValue, randomState);

        return randomState;
    }

    private void GenerateMagnets(int finalRandomState)
    {
        Random.InitState(finalRandomState);

        for (int i = 0; i <= columns; i++)
        {
            for (int j = 0; j <= rows; j++)
            {
                Vector3 magnetPosition = new(transform.position.x + i - (columns / 2),
                                transform.position.y + j - (rows / 2), 0);

                if (!markedSpots.Contains(new Vector2(i, j)) &&
                    Physics2D.OverlapCircleAll(magnetPosition, 1.5f).Length == 0)
                {
                    float probability = Random.Range(0f, 1f);

                    if (probability >= minimumProbability)
                    {
                        Instantiate(magnet, magnetPosition, new Quaternion(), gameObject.transform);

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
