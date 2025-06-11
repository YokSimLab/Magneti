using System;
using UnityEngine;

public class TargetIndicator : MonoBehaviour
{
    [SerializeField] private GameObject child;
    [SerializeField] private Sprite batterySprite;
    [SerializeField] private Sprite satelliteSprite;

    private Vector2 maxScale;
    private Vector2 currentScale;
    float maxSpriteWidth = 0;
    float maxSpriteHeight = 0;
    private int type;

    private void Start()
    {
        Bounds bounds = GetComponent<SpriteRenderer>().bounds;
        maxSpriteWidth = bounds.size.x / 2f;
        maxSpriteHeight = bounds.size.y / 2f;
        maxScale = transform.localScale;

        transform.localScale = Vector3.zero;
        currentScale = transform.localScale;
    }

    public void SetIndicatorType(GameObject target)
    {
        type = target.GetComponent<Magnet>() ? 0 : 1;
        child.GetComponent<SpriteRenderer>().sprite = type == 0 ? satelliteSprite : batterySprite;
    }

    public void SetRotation(float angle)
    {
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        child.transform.rotation = Quaternion.identity;
    }

    public void SetSize(float distance)
    {
        distance = Mathf.Abs(distance);
        distance = Mathf.Clamp(distance, 0, type == 0 ? 7 : 5);
        float xScale = (1 - distance / 10) * maxScale.x;
        float yScale = (1 - distance / 10) * maxScale.y;

        currentScale = new Vector2(xScale, yScale);
    }

    public Vector2 GetSize()
    {
        transform.localScale = Vector2.Lerp(transform.localScale, currentScale, 0.1f);
        return new Vector2(maxSpriteWidth, maxSpriteHeight) * (currentScale.x / maxScale.x);
    }
}