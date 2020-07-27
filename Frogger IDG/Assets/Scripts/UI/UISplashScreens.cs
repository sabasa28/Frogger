using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UISplashScreens : MonoBehaviour
{
    public Image PanelSplashScreen1;
    public TextMeshProUGUI textSplashScreen1;
    public Image imageSplashScreen1;
    public Image PanelSplashScreen2;
    public TextMeshProUGUI textSplashScreen2;
    public Image imageSplashScreen2;

    float timeInBackground = 2.0f;
    float timeToChangeAlpha = 2.0f;
    float timeInScreens = 4.0f;
    void Start()
    {
        StartCoroutine(ShowAndHide());
    }

    IEnumerator ShowAndHide()
    {
        PanelSplashScreen1.gameObject.SetActive(true);

        yield return new WaitForSeconds(timeInBackground);

        float t = 0.0f;
        Color origColorText = textSplashScreen1.color;
        Color targetColorText = new Color(origColorText.r, origColorText.g, origColorText.b, 1);
        Color origColorImage = imageSplashScreen1.color;
        Color targetColorImage = new Color(origColorImage.r, origColorImage.g, origColorImage.b, 1);

        while (textSplashScreen1.color != targetColorText && imageSplashScreen1.color != targetColorImage)
        {
            t += Time.deltaTime / timeToChangeAlpha;
            textSplashScreen1.color = Color.Lerp(origColorText, targetColorText, t);
            imageSplashScreen1.color = Color.Lerp(origColorImage, targetColorImage, t);
            yield return null;
        }

        yield return new WaitForSeconds(timeInScreens);

        t = 0.0f;
        origColorText = textSplashScreen1.color;
        targetColorText = new Color(origColorText.r, origColorText.g, origColorText.b, 0);
        origColorImage = imageSplashScreen1.color;
        targetColorImage = new Color(origColorImage.r, origColorImage.g, origColorImage.b, 0);

        while (textSplashScreen1.color != targetColorText && imageSplashScreen1.color != targetColorImage)
        {
            t += Time.deltaTime / timeToChangeAlpha;
            textSplashScreen1.color = Color.Lerp(origColorText, targetColorText, t);
            imageSplashScreen1.color = Color.Lerp(origColorImage, targetColorImage, t);

            yield return null;
        }

        yield return new WaitForSeconds(timeInBackground);

        PanelSplashScreen1.gameObject.SetActive(false);
        PanelSplashScreen2.gameObject.SetActive(true);

        yield return new WaitForSeconds(timeInBackground);

        t = 0.0f;
        origColorText = textSplashScreen2.color;
        targetColorText = new Color(origColorText.r, origColorText.g, origColorText.b, 1);
        origColorImage = imageSplashScreen2.color;
        targetColorImage = new Color(origColorImage.r, origColorImage.g, origColorImage.b, 1);

        while (textSplashScreen2.color != targetColorText && imageSplashScreen2.color != targetColorImage)
        {
            t += Time.deltaTime / timeToChangeAlpha;
            textSplashScreen2.color = Color.Lerp(origColorText, targetColorText, t);
            imageSplashScreen2.color = Color.Lerp(origColorImage, targetColorImage, t);

            yield return null;
        }

        yield return new WaitForSeconds(timeInScreens);

        t = 0.0f;
        origColorText = textSplashScreen2.color;
        targetColorText = new Color(origColorText.r, origColorText.g, origColorText.b, 0);
        origColorImage = imageSplashScreen2.color;
        targetColorImage = new Color(origColorImage.r, origColorImage.g, origColorImage.b, 0);

        while (textSplashScreen2.color != targetColorText && imageSplashScreen2.color != targetColorImage)
        {
            t += Time.deltaTime / timeToChangeAlpha;
            textSplashScreen2.color = Color.Lerp(origColorText, targetColorText, t);
            imageSplashScreen2.color = Color.Lerp(origColorImage, targetColorImage, t);

            yield return null;
        }

        yield return new WaitForSeconds(timeInBackground);

        PanelSplashScreen2.gameObject.SetActive(false);
        SceneManager.LoadScene(1);
    }
}

/*
 using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UISplashScreen : MonoBehaviour
{
    public TextMeshProUGUI textSplashScreen1;
    public Image imageSplashScreen1;
    public TextMeshProUGUI textSplashScreen2;
    public Image imageSplashScreen2;

    float timeToChangeAlpha = 2.0f;
    float timeBetweenScreens = 3.0f;
    float timeInScreens = 4.0f;

    private void Start()
    {
        StartCoroutine(ShowAndHide());
    }

    IEnumerator ShowAndHide()
    {
        yield return new WaitForSeconds(timeBetweenScreens);

        float t = 0.0f;
        Color origColorText = textSplashScreen1.color;
        Color targetColorText = new Color(origColorText.r, origColorText.g, origColorText.b, 1);
        Color origColorImage = imageSplashScreen1.color;
        Color targetColorImage = new Color(origColorImage.r, origColorImage.g, origColorImage.b, 1);

        while (textSplashScreen1.color != targetColorText && imageSplashScreen1.color != targetColorImage)
        {
            t += Time.deltaTime / timeToChangeAlpha;
            textSplashScreen1.color = Color.Lerp(origColorText, targetColorText, t);
            imageSplashScreen1.color = Color.Lerp(origColorImage, targetColorImage, t);
            yield return null;
        }
        
        yield return new WaitForSeconds(timeInScreens);

        t = 0.0f;
        origColorText = textSplashScreen1.color;
        targetColorText = new Color(origColorText.r, origColorText.g, origColorText.b, 0);
        origColorImage = imageSplashScreen1.color;
        targetColorImage = new Color(origColorImage.r, origColorImage.g, origColorImage.b, 0);

        while (textSplashScreen1.color != targetColorText && imageSplashScreen1.color != targetColorImage)
        {
            t += Time.deltaTime / timeToChangeAlpha;
            textSplashScreen1.color = Color.Lerp(origColorText, targetColorText, t);
            imageSplashScreen1.color = Color.Lerp(origColorImage, targetColorImage, t);

            yield return null;
        }

        yield return new WaitForSeconds(timeBetweenScreens);

        t = 0.0f;
        origColorText = textSplashScreen2.color;
        targetColorText = new Color(origColorText.r, origColorText.g, origColorText.b, 1);
        origColorImage = imageSplashScreen2.color;
        targetColorImage = new Color(origColorImage.r, origColorImage.g, origColorImage.b, 1);

        while (textSplashScreen2.color != targetColorText && imageSplashScreen2.color != targetColorImage)
        {
            t += Time.deltaTime / timeToChangeAlpha;
            textSplashScreen2.color = Color.Lerp(origColorText, targetColorText, t);
            imageSplashScreen2.color = Color.Lerp(origColorImage, targetColorImage, t);

            yield return null;
        }

        yield return new WaitForSeconds(timeInScreens);

        t = 0.0f;
        origColorText = textSplashScreen2.color;
        targetColorText = new Color(origColorText.r, origColorText.g, origColorText.b, 0);
        origColorImage = imageSplashScreen2.color;
        targetColorImage = new Color(origColorImage.r, origColorImage.g, origColorImage.b, 0);

        while (textSplashScreen2.color != targetColorText && imageSplashScreen2.color != targetColorImage)
        {
            t += Time.deltaTime / timeToChangeAlpha;
            textSplashScreen2.color = Color.Lerp(origColorText, targetColorText, t);
            imageSplashScreen2.color = Color.Lerp(origColorImage, targetColorImage, t);

            yield return null;
        }

        yield return new WaitForSeconds(timeBetweenScreens);
        SceneManager.LoadScene(1);
    }

}*/
