using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class CustomButton : Button
{
    private DimensionHandler dimensionHandler;
    protected override void Start()
    {
        dimensionHandler = FindObjectOfType<DimensionHandler>();
    }

    public override void OnPointerDown(PointerEventData eventData)    // Button is Pressed
    {
        base.OnPointerDown(eventData);
        dimensionHandler.ShowOtherDimension();
    }

    public override void OnPointerUp(PointerEventData eventData)    // Button is released
    {
        base.OnPointerUp(eventData);
        dimensionHandler.ShowOtherDimension();
    }

}
