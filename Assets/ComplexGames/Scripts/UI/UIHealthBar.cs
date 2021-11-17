using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CharacterSpace;

public class UIHealthBar : MonoBehaviour
{
    public Transform target;
    public Image foregroundImage;
    public Image backgroundImage;
    public Text displayText;
    public Vector3 offset;
    public Slider slider;

    // Do this in late update so all other calculations/animations and such are finished before this updates 
    void LateUpdate()
    {
        if(target == null)
        {
            return;
        }
        Vector3 direction = (target.position - Camera.main.transform.position).normalized;
        bool isBehind = Vector3.Dot(direction, Camera.main.transform.forward) <= 0f;
        foregroundImage.enabled = !isBehind;
        backgroundImage.enabled = !isBehind;
        transform.position = Camera.main.WorldToScreenPoint(target.position + offset);
    }
    public void SetUIBarPercentage(float percentage)
    {
        slider.value = percentage;
        //float parentWidth = GetComponent<RectTransform>().rect.width;
        //float width = parentWidth * percentage;
        //foregroundImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
    }

    public void SetMax(float max)
    {
        slider.maxValue = max;
        slider.value = max;
    }
}
