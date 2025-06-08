using UnityEngine;

public class TargetIndicator : MonoBehaviour
{
    [SerializeField] private GameObject child;
    [SerializeField] private Sprite batterySprite;
    [SerializeField] private Sprite satelliteSprite;

    public void SetIndicatorType(GameObject target)
    {
        int type = target.GetComponent<Magnet>() ? 0 : 1;
        child.GetComponent<SpriteRenderer>().sprite = type == 0 ? satelliteSprite : batterySprite;
    }

    public void SetRotation(float angle)
    {
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        child.transform.rotation = Quaternion.identity;
    }
}