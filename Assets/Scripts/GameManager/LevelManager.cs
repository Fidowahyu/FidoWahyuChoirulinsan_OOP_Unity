using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void Awake()
    {
        animator.enabled = false;
    }

    public IEnumerator LoadSceneAsync(string sceneName)
    {
        animator.enabled = true;
        animator.ResetTrigger("StartTransition");
        yield return new WaitForSeconds(1);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

    if (asyncLoad.isDone)
    {
        animator.SetTrigger("EndTransition");
    } 
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }
}
