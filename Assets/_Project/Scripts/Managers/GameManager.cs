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
    [SerializeField] GameObject player,tower;

    [Space(5)]
    [Header("Cameras")]
    [SerializeField] GameObject titleScreenCam;
    [SerializeField] GameObject gameScreenCam;
    [SerializeField] GameObject deathScreenCam;

    Vector3 playerStartPos;

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
        playerStartPos = player.transform.position;
    }

    public void TitleScene()
    {
        gameStarted = false;
        tower.SetActive(true);
        titleScreenCam.SetActive(true);
        gameScreenCam.SetActive(false);
        deathScreenCam.SetActive(false);
        player.SetActive(false);
        UIManager.Instance.ShowTitleScreen();
    }

    public void StartGame()
    {
        Cursor.visible = false;
        Tower.Instance.Heal(Tower.Instance.GetMaxHealthValue());
        titleScreenCam.SetActive(false);
        deathScreenCam.SetActive(false);
        player.SetActive(true);
        tower.SetActive(true);
        gameScreenCam.SetActive(true);
        UIManager.Instance.ShowGameScreen();
        EnemyManager.Instance.StartWave();
        gameStarted = true;
    }

    public void RestartGame()
    {
        Tower.Instance.Heal(Tower.Instance.GetMaxHealthValue());
        EnemyManager.Instance.ClearEnemies();
        player.SetActive(false);
        player.transform.position = playerStartPos;
        StartGame();
    }

    public void EndGame()
    {
        gameStarted = false;
        titleScreenCam.SetActive(false);
        gameScreenCam.SetActive(false);
        deathScreenCam.SetActive(true);
        UIManager.Instance.ShowEndScreen();
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
