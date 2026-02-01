
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Oxygen : MonoBehaviour
{
    private Vector3 maskOriginalPos;
    private float initialWidth;
    private Player player;
    private RectTransform rectTransform;
    private Image image;
    [SerializeField] private GameObject mask;
    [SerializeField] private Sprite[] sprites;
    private Sprite originalMaskSprite;
    // Start is called before the first frame update
    void Start()
    {
        if (mask != null)
        {
            maskOriginalPos = mask.GetComponent<RectTransform>().anchoredPosition;
        }
        player = FindObjectOfType<Player>();
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
        initialWidth = rectTransform.sizeDelta.x;
    }

    public void ShakeMask(float duration = 0.2f, float magnitude = 10f)
    {
        if (mask != null)
            StartCoroutine(ShakeMaskCoroutine(duration, magnitude));
    }

    private IEnumerator ShakeMaskCoroutine(float duration, float magnitude)
    {
        RectTransform maskRect = mask.GetComponent<RectTransform>();
        Vector3 originalPos = maskOriginalPos;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            float offsetX = Random.Range(-1f, 1f) * magnitude;
            float offsetY = Random.Range(-1f, 1f) * magnitude;
            maskRect.anchoredPosition = originalPos + new Vector3(offsetX, offsetY, 0);
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }
        maskRect.anchoredPosition = originalPos;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 size = rectTransform.sizeDelta;
        float oxygenPercent = player.Oxygen / 100f;
        size.x = initialWidth * oxygenPercent;
        rectTransform.sizeDelta = size;

        // Gradient color: green (100%) -> yellow (50%) -> red (0%)
        if (oxygenPercent > 0.5f)
        {
            // Green to Yellow
            float t = (oxygenPercent - 0.5f) * 2f; // t goes from 0 to 1
            image.color = Color.Lerp(Color.yellow, Color.green, t);
        }
        else
        {
            // Yellow to Red
            float t = oxygenPercent * 2f; // t goes from 0 to 1
            image.color = Color.Lerp(Color.red, Color.yellow, t);
        }

        
    }

    public void CrackMask()
    {
        StartCoroutine(CrackMaskCoroutine());
    }

    private IEnumerator CrackMaskCoroutine()
    {
        if (mask != null && sprites.Length > 0)
        {
            Image maskImage = mask.GetComponent<Image>();
            if (maskImage != null)
            {
                maskImage.sprite = sprites[1];
                yield return new WaitForSeconds(1f);
                maskImage.sprite =sprites[0];
            }
        }
    }
}
