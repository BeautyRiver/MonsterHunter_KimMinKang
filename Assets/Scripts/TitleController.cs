using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour
{
    [SerializeField] private Text pressText;
    [SerializeField] private float blinkSpeed;

    private void Update()
    {
        Color color = pressText.color;
        float newAlpha = Mathf.PingPong(Time.time * blinkSpeed, 1f);
        color.a = newAlpha;
        pressText.color = color;
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main");
        PlayerPrefs.DeleteAll();
    }
}
