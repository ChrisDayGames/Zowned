using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof (Image))]
public class Bar : MonoBehaviour {

    private RectTransform rectTransform;
    private float maxWidth;

    private Image vignette;
    public float minOpacity;
    public float maxOpacity;

    // Use this for initialization
    void Start () {

        rectTransform = GetComponent<RectTransform>();
        maxWidth = rectTransform.sizeDelta.x;

        vignette = GetComponent<Image>();

    }
	
    public void SetOpacity (float opacityPercent) {

        float a = minOpacity + (maxOpacity - minOpacity) * opacityPercent;

        vignette.color = new Color(vignette.color.r, vignette.color.g, vignette.color.b, a / 255);

    }

    public void SetSize(float percentWidth) {

        rectTransform.sizeDelta = new Vector2(maxWidth * percentWidth, rectTransform.sizeDelta.y);

    }

}
