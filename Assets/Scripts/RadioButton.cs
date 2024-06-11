using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadioButton : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Sprite[] buttonSprites;
    [SerializeField] private Image targetButton;
    public void changeUI() {
        if (targetButton.sprite == buttonSprites[0])
        {
            targetButton.sprite = buttonSprites[1];
            return;
        }

        targetButton.sprite = buttonSprites[0];
    }
}
