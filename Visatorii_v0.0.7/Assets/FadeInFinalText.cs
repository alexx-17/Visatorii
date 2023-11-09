using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FadeInFinalText : MonoBehaviour
{
    public TextMeshPro Titlu;
    public TextMeshPro Semnficatii;

    // Start is called before the first frame update
    void Start()
    {
        Titlu.color = new Color(1, 1, 1, 0);
        Semnficatii.color = new Color(1, 1, 1, 0);

        StartCoroutine(FadeBothTexts());
    }

    IEnumerator FadeBothTexts()
    {
        StartCoroutine(FadeTextToFullAlpha(1f, Titlu));

        yield return new WaitForSeconds(4);

        StartCoroutine(FadeTextToFullAlpha(1f, Semnficatii));
    }

    public IEnumerator FadeTextToFullAlpha(float t, TextMeshPro i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        yield return new WaitForSeconds(2);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
    }
}
