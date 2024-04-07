using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FpsMeter : MonoBehaviour
{
    Text fpsText;
    public float updateRate = 0.5f; // FPS güncelleme hızı (varsayılan olarak yarım saniye)

    private float deltaTime = 0.0f;

    private void Start()
    {
        fpsText = GetComponent<Text>();
        InvokeRepeating("UpdateFPS", 0f, updateRate);
    }

    private void UpdateFPS()
    {
        float fps = 1.0f / deltaTime;
        fpsText.text = "FPS: " + Mathf.Round(fps);
    }

    private void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
    }
}
