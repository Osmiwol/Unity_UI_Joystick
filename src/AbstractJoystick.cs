using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class AbstractJoystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    protected event Action onPointerUpEvent;
    protected event Action onPointerDownEvent;
    protected event Action onDoubleClickEvent;
    protected Vector2 _inputVector;
    protected float DelayDoubleClick = 0.45f;

    [SerializeField] private Image _jInputArea;
    [SerializeField] private Image _jBase;
    [SerializeField] private Image _jStick;
    

    //private Vector2 _joystickBackgroundStartPosition;
    
    DateTime _lastPressTime;
    public void AddOnPointerUpHandler(Action action) => onPointerUpEvent += action;
    public void AddOnPointerDownHandler(Action action) => onPointerDownEvent += action;
    public void ClearOnPointerUpHandler() => onPointerUpEvent = EmptyAction;
    public void ClearOnPointerDownHandler() => onPointerDownEvent = EmptyAction;
    public void EmptyAction() { }

    private void Start() 
    {  
        //_joystickBackgroundStartPosition = _jBase.rectTransform.anchoredPosition;
        _lastPressTime = DateTime.Now;
        onPointerDownEvent += DoubleClickHandler;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 joystickPosition;

        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(_jBase.rectTransform, eventData.position, null, out joystickPosition))
        {
            joystickPosition.x = (joystickPosition.x * 2 / _jBase.rectTransform.sizeDelta.x);
            joystickPosition.y = (joystickPosition.y * 2 / _jBase.rectTransform.sizeDelta.y);

            _inputVector = new Vector2(joystickPosition.x, joystickPosition.y);
            _inputVector = (_inputVector.magnitude > 1f) ? _inputVector.normalized : _inputVector;
            _jStick.rectTransform.anchoredPosition = new Vector2(
                _inputVector.x * (_jBase.rectTransform.sizeDelta.x / 2),
                _inputVector.y * (_jBase.rectTransform.sizeDelta.y / 2));
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Down");
        Vector2 joystickBackgoundPosition;
        onPointerDownEvent?.Invoke();

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_jInputArea.rectTransform, eventData.position, null, out joystickBackgoundPosition))
        {
            _jBase.rectTransform.anchoredPosition = new Vector2(joystickBackgoundPosition.x, joystickBackgoundPosition.y);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Up");
        _inputVector = Vector2.zero;
        _jStick.rectTransform.anchoredPosition = Vector2.zero;
        onPointerUpEvent?.Invoke();
    }

    void DoubleClickHandler()
    {
        if (Timer.IsTimePass(_lastPressTime, DelayDoubleClick)) _lastPressTime = DateTime.Now;
        else onDoubleClickEvent?.Invoke();
    }
}
