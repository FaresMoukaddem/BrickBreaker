using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PopupHandler : MonoBehaviour
{

    public static PopupHandler instance;

    public Button button;
    public Text titleText;
    public GameObject popup;

    void Awake()
    {
        if (!instance)
            instance = this;
    }

    // Start is called before the first frame update
    public void TogglePopup(bool show, string title = "", string buttonText = "", Action buttonCallback = null)
    {
        if (show)
        {
            titleText.text = title;
            button.GetComponentInChildren<Text>().text = buttonText;
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => buttonCallback?.Invoke());
        }

        popup.SetActive(show);
    }
}
