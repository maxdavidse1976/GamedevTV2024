using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [Header("Singleton")]
    [SerializeField] bool m_destroyOnLoad = false;

    [Space(5)]
    [Header("Game")]
    public bool gameStarted;
    [SerializeField] GameObject player;

    [Space(5)]
    [Header("Cameras")]
    [SerializeField] GameObject titleScreenCam;
    [SerializeField] GameObject gameScreenCam;
    [SerializeField] GameObject deathScreenCam;



    void Awake()
    {
        if (Instance == null) Instance = this;
        else if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }

        if (m_destroyOnLoad) { DontDestroyOnLoad(this.gameObject); }
    }

    void Start()
    {
        TitleScene();
    }

    public void TitleScene()
    {
        gameStarted = false;
        titleScreenCam.SetActive(true);
        gameScreenCam.SetActive(false);
        deathScreenCam.SetActive(false);
        player.SetActive(false);
        UIManager.Instance.ShowTitleScreen();
    }

    public void StartGame()
    {
        titleScreenCam.SetActive(false);
        gameScreenCam.SetActive(true);
        deathScreenCam.SetActive(false);
        player.SetActive(true);
        UIManager.Instance.ShowGameScreen();
        EnemyManager.Instance.StartWave();
        gameStarted = true;
    }

    public void EndGame()
    {
        gameStarted = false;
        titleScreenCam.SetActive(false);
        gameScreenCam.SetActive(false);
        deathScreenCam.SetActive(true);
        UIManager.Instance.ShowEndScreen();
    }
}
