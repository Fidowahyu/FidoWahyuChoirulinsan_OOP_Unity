using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void Awake()
    {
        // Ensure animator is assigned, or find it in the scene
        if (animator == null)
        {
            animator = GameObject.Find("SceneTransition")?.GetComponent<Animator>();
            if (animator == null)
            {
                Debug.LogWarning("Animator not assigned or not found on SceneTransition object.");
            }
        }
    }

    public IEnumerator LoadSceneAsync(string sceneName)
    {
        // Start transition animation
        animator.SetTrigger("StartTransition");

        // Wait for animation to complete
        yield return new WaitForSeconds(1); // Adjust if animation length differs

        // Load the scene asynchronously
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // End transition animation
        animator.SetTrigger("EndTransition");
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }
}
