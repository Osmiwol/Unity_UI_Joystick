using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealizationJoystick : AbstractJoystick
{
   
    public Vector3 GetVectorDirection()
    {
        if (_inputVector.x != 0 || _inputVector.y != 0) return new Vector3(_inputVector.x, _inputVector.y);
        else return new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    public void AddDoubleClickListener(Action dblClickHandler) => onDoubleClickEvent += dblClickHandler;
    public void RemoveDoubleClickListener(Action dblClickHandler) => onDoubleClickEvent -= dblClickHandler;

    public void AddPointerUpHandler(Action pntrUpHandler) => onPointerUpEvent += pntrUpHandler;
    public void RemovePointerUpHandler(Action pntrUpHandler) => onPointerUpEvent -= pntrUpHandler;

}
