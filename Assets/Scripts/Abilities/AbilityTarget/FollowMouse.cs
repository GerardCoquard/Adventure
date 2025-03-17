using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    private RectTransform _rectTransform;
    [SerializeField] private Vector2 _offset;

    void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        Follow();
    }

    public void Follow()
    {
        if(_rectTransform == null)
            _rectTransform = GetComponent<RectTransform>();
        
        Vector2 mousePosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_rectTransform.parent as RectTransform, Input.mousePosition, null, out mousePosition);
        _rectTransform.anchoredPosition = mousePosition + _offset;
    }
}
