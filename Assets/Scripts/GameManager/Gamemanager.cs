using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public LevelManager LevelManager { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;

        LevelManager = GetComponentInChildren<LevelManager>();

        DontDestroyOnLoad(gameObject);
        var camera = GameObject.Find("Camera");
        if (camera != null)
        {
            DontDestroyOnLoad(camera);
        }
    }
}
