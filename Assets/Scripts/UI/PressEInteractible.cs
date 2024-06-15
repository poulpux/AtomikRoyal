using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PressEInteractible : MonoBehaviour
{
    private Image cadreImage;
    private TextMeshProUGUI text;
    void Start()
    {
        cadreImage = GetComponent<Image>();
        text = GetComponentInChildren<TextMeshProUGUI>();
        StartCoroutine(TryInstantiateEvent());
    }

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    private void ChoseCoroutine(bool open)
    {
        StopCoroutine(CloseOpenVisu(open));
        StartCoroutine(CloseOpenVisu(open));
    }

    private IEnumerator CloseOpenVisu(bool open)
    {
        Color startColor = cadreImage.color;
        Color currentColor = startColor;
        Color targetColor = new Color(1f, 1f, 1f, open ? 1f : 0f);
        float timer = 0f;
        float duration = 0.1f;
        while (timer < duration)
        {
            currentColor = Color.Lerp(startColor, targetColor, timer / duration);
            SetColor(currentColor);
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        SetColor(targetColor);
    }

    private IEnumerator TryInstantiateEvent()
    {
        while(GameManager.Instance.currentPlayer == null)
        {
            yield return new WaitForEndOfFrame();
        }

        GameManager.Instance.currentPlayer.interact.changeStateInteractibleEvent.AddListener((open) => ChoseCoroutine(open));
    }

    private void SetColor(Color color)
    {
        cadreImage.color = color;
        text.color = color;
    }
}
