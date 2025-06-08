using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TargetIndicatorManager : MonoBehaviour
{
    public GameObject[] targets;
    public GameObject indicatorPrefab;

    [SerializeField] private float targetIndicatorDistance;

    private SpriteRenderer indicatorRenderer;
    private GameObject specificIndicatorSprite;
    private float spriteWidth;
    private float spriteHeight;

    private Camera _camera;

    private readonly Dictionary<GameObject, TargetIndicator> targetToIndicators = new();

    private void Start()
    {
        _camera = Camera.main;
        indicatorRenderer = indicatorPrefab.GetComponent<SpriteRenderer>();

        Bounds bounds = indicatorRenderer.bounds;
        spriteWidth = bounds.size.x / 2f;
        spriteHeight = bounds.size.y / 2f;

        foreach (GameObject target in targets)
        {
            TargetIndicator indicator = Instantiate(indicatorPrefab).GetComponent<TargetIndicator>();

            indicator.gameObject.SetActive(false);
            targetToIndicators.Add(target, indicator);
        }
    }

    public void OnNewTarget(GameObject newTarget)
    {
        TargetIndicator indicator = Instantiate(indicatorPrefab).GetComponent<TargetIndicator>();

        indicator.SetIndicatorType(newTarget);
        indicator.gameObject.SetActive(false);
        targetToIndicators.Add(newTarget, indicator);
    }

    public void OnRemoveTarget(GameObject target)
    {
        GameObject indicator = targetToIndicators[target]?.gameObject;
        targetToIndicators.Remove(target);

        if (indicator)
        {
            indicator.gameObject.SetActive(false);
            Destroy(indicator);
        }
    }

    private void Update()
    {
        foreach ((GameObject target, TargetIndicator indicator) in targetToIndicators)
        {
            UpdateTarget(target, indicator);
        }
    }

    private void UpdateTarget(GameObject target, TargetIndicator indicator)
    {
        if (target.GetComponent<Magnet>() && Vector2.Distance(
                new Vector2(target.transform.position.x, target.transform.position.y),
                new Vector2(_camera.transform.position.x, _camera.transform.position.y)) > targetIndicatorDistance)
        {
            indicator.gameObject.SetActive(false);
            return;
        }

        Vector3 screenPos = _camera.WorldToViewportPoint(target.transform.position);
        bool isOffscreen = screenPos.x <= 0 || screenPos.x >= 1 || screenPos.y <= 0 || screenPos.y >= 1;

        if (isOffscreen)
        {
            indicator.gameObject.SetActive(true);
            Vector3 spriteSizeInViewport = _camera.WorldToViewportPoint(new Vector3(spriteWidth, spriteHeight, 0f)) -
                                           _camera.WorldToViewportPoint(Vector3.zero);

            screenPos.x = Mathf.Clamp(screenPos.x, spriteSizeInViewport.x, 1 - spriteSizeInViewport.x);
            screenPos.y = Mathf.Clamp(screenPos.y, spriteSizeInViewport.y, 1 - spriteSizeInViewport.y);

            Vector3 worldPosition = _camera.ViewportToWorldPoint(screenPos);
            worldPosition.z = 0f;
            indicator.transform.position = worldPosition;

            Vector3 direction = target.transform.position - indicator.transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;
            indicator.SetRotation(angle);
        }
        else
        {
            indicator.gameObject.SetActive(false);
        }
    }
}