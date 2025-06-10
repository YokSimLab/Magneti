using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TargetIndicatorManager : MonoBehaviour
{
    public GameObject indicatorPrefab;

    [SerializeField] private float targetIndicatorDistance;

    private GameObject specificIndicatorSprite;

    private Camera _camera;

    [SerializeField] private Dictionary<GameObject, TargetIndicator> targetToIndicators = new();

    private void Start()
    {
        _camera = Camera.main;
    }

    public void OnNewTarget(GameObject newTarget)
    {
        if (targetToIndicators.ContainsKey(newTarget)) return;

        TargetIndicator indicator = Instantiate(indicatorPrefab).GetComponent<TargetIndicator>();

        indicator.SetIndicatorType(newTarget);
        indicator.gameObject.SetActive(false);
        indicator.SetSize(1337);
        targetToIndicators.Add(newTarget, indicator);
    }

    public void OnRemoveTarget(GameObject target)
    {
        if (!targetToIndicators.ContainsKey(target)) return;

        GameObject indicator = targetToIndicators[target].gameObject;
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
        bool isMagnetAndFarAway = target.GetComponent<Magnet>() && Vector2.Distance(
            new Vector2(target.transform.position.x, target.transform.position.y),
            new Vector2(_camera.transform.position.x, _camera.transform.position.y)) > targetIndicatorDistance;

        Vector3 screenPos = _camera.WorldToViewportPoint(target.transform.position);
        bool isOffscreen = screenPos.x <= 0 || screenPos.x >= 1 || screenPos.y <= 0 || screenPos.y >= 1;

        if (isOffscreen && !isMagnetAndFarAway)
        {
            indicator.gameObject.SetActive(true);

            float spriteWidth = indicator.GetSize().x;
            float spriteHeight = indicator.GetSize().y;
            Vector3 spriteSizeInViewport = _camera.WorldToViewportPoint(new Vector3(spriteWidth, spriteHeight, 0f)) -
                                           _camera.WorldToViewportPoint(Vector3.zero);

            screenPos.x = Mathf.Clamp(screenPos.x, spriteSizeInViewport.x, 1 - spriteSizeInViewport.x);
            screenPos.y = Mathf.Clamp(screenPos.y, spriteSizeInViewport.y, 1 - spriteSizeInViewport.y);
            Vector3 worldPosition = _camera.ViewportToWorldPoint(screenPos);
            worldPosition.z = 0f;
            indicator.transform.position = worldPosition;
            indicator.SetSize(Vector2.Distance(_camera.transform.position, target.transform.position));

            Vector3 direction = target.transform.position - indicator.transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;
            indicator.SetRotation(angle);
        }
        else
        {
            indicator.gameObject.SetActive(false);
            indicator.SetSize(1337);
        }
    }
}