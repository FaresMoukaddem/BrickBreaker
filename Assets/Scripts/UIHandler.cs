using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIHandler : MonoBehaviour
{

    public static UIHandler instance;

    public Canvas canvas;

    public Slider movementSlider;

    public Text levelText, livesText;

    private GameObject player;

    void Awake()
    {
        if (!instance)
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.instance.player;

        ConfigureSlider();
    }

    public void SetLevelText(int level)
    {
        levelText.text = "Level: " + level;
    }

    public void SetLivesText(int lives)
    {
        livesText.text = "Lives: " + lives;
    }

    public void ConfigureSlider()
    {
        RectTransform sliderTransform = movementSlider.GetComponent<RectTransform>();
        sliderTransform.sizeDelta = new Vector2(Screen.width, Screen.height / 2);
        sliderTransform.localPosition = new Vector3(0, -(Screen.height / 4), 0);
        movementSlider.maxValue = 1;
        movementSlider.minValue = 0;
    }

    // Update is called once per frame
    public void OnSliderMove(float newValue)
    {
        Vector3 viewportToWorldVector = Camera.main.ViewportToWorldPoint(new Vector3(newValue, 0, 0));
        player.transform.position = new Vector3(viewportToWorldVector.x, player.transform.position.y, player.transform.position.z);
    }
}
