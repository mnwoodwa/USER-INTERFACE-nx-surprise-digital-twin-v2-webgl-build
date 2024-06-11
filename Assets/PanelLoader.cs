using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelLoader : MonoBehaviour
{
    [SerializeField]
    private GameObject panel;

    [SerializeField]
    private GameObject panelButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowPanel()
    {
        RectTransform panelTransform = panel.GetComponent<RectTransform>();
        RectTransform buttonTransform = panelButton.GetComponent<RectTransform>();
        panelTransform.anchoredPosition = new Vector2(panelTransform.anchoredPosition.x * -1f, panelTransform.anchoredPosition.y);
        if(panelTransform.anchoredPosition.x > 0)
        {
            buttonTransform.anchoredPosition = new Vector2(buttonTransform.anchoredPosition.x - 25f, buttonTransform.anchoredPosition.y);
        }
        else
        {
            buttonTransform.anchoredPosition = new Vector2(buttonTransform.anchoredPosition.x + 25f, buttonTransform.anchoredPosition.y);
        }
    }
}
