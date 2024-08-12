using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class DynamicSortingLayer : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private int _defaultOrder = 0;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _defaultOrder = _spriteRenderer.sortingOrder;
    }

    void LateUpdate()
    {
        _spriteRenderer.sortingOrder = _defaultOrder + (int)(-transform.position.y * 100);
    }
}
