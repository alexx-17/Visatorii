using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{
    /*
     * NU AM NICI CEA MAI MICA IDEE CE AM SCRIS AICI
     * era primul tutorial pe care l am vzt despre asta si pare sa mearga destul de bine
     * tranzitiile de aici functioneaza ca un switch
     * are implementat si fade in si fade out
     */
    public float speedScale = 1f;
    public Color fadeColor = Color.black;
    public AnimationCurve Curve = new AnimationCurve(new Keyframe(0, 1), new Keyframe(0.5f, 0.5f, -1.5f, -1.5f), new Keyframe(1, 0));
    public bool startFadeOut = false;
    public bool startTransition = false;

    private float alpha = 0f;
    private Texture2D texture;
    private int direction = 0;
    private float time = 0f;

    // Start is called before the first frame update
    void Start()
    {
        if(startFadeOut)
        {
            alpha = 1f;
        }
        else
        {
            alpha = 0f;
        }

        texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, new Color(fadeColor.r, fadeColor.g, fadeColor.b, alpha));
        texture.Apply();

    }

    // Update is called once per frame
    void Update()
    {
        if(direction == 0 && startTransition == true)
        {
            startTransition = false;
            if(alpha >= 1f)
            {
                alpha = 1f;
                time = 0f;
                direction = 1;
            }
            else
            {
                alpha = 0f;
                time = 1f;
                direction = -1;
            }
        }
    }

    public void OnGUI()
    {
        if(alpha > 0f)
        {
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), texture);
        }

        if(direction != 0)
        {
            time += direction * Time.deltaTime * speedScale;
            alpha = Curve.Evaluate(time);
            texture.SetPixel(0, 0, new Color(fadeColor.r, fadeColor.g, fadeColor.b, alpha));
            texture.Apply();
            if(alpha <= 0f || alpha >= 1f)
            {
                direction = 0;
            }
        }
    }
}
