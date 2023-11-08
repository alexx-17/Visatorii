using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    public Transition transitionManager;

    IEnumerator makeTransitionToStartScene()
    {
        transitionManager.startTransition = true;

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene("Scene1");
    }

    public void LoadStartScene()
    {
        StartCoroutine(makeTransitionToStartScene());
    }
}
