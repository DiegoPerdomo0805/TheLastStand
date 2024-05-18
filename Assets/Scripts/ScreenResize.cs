using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScreenResize : MonoBehaviour
{
    // Start is called before the first frame update

    public TextMeshProUGUI[] textos;
    void Start()
    {
        
    }

    void AlignTextElements()
    {
        foreach(TextMeshProUGUI t in textos)
        {
            RectTransform r = t.GetComponent<RectTransform>();

            r.anchorMin = new Vector2(0, 1);
            r.anchorMax = new Vector2(0, 1);
            r.pivot = new Vector2(0, 1);
            r.anchoredPosition = Vector2.zero;
            r.offsetMin = Vector2.zero;
            r.offsetMax = Vector2.zero;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
