using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        Tower.Instance.RespawnTower();
        titleScreenCam.SetActive(true);
        gameScreenCam.SetActive(false);
        deathScreenCam.SetActive(false);
        player.SetActive(false);
        UIManager.Instance.ShowTitleScreen();
        EnemyManager.Instance.ResetEnemyWaves();

        AudioManager.Instance.PlaySound("BGM_TitleMusic");
    }

    public void StartGame()
    {
        AudioManager.Instance.StopSound("BGM_TitleMusic");
        Cursor.visible = false;
        Tower.Instance.RespawnTower();
        titleScreenCam.SetActive(false);
        deathScreenCam.SetActive(false);
        player.SetActive(true);
        tower.SetActive(true);
        gameScreenCam.SetActive(true);
        UIManager.Instance.ShowGameScreen();
        EnemyManager.Instance.StartWave();
        gameStarted = true;
        AudioManager.Instance.PlaySound("BGM_GameMusic");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void EndGame()
    {
        AudioManager.Instance.StopSound("BGM_GameMusic");
        gameStarted = false;
        titleScreenCam.SetActive(false);
        gameScreenCam.SetActive(false);
        deathScreenCam.SetActive(true);
        UIManager.Instance.ShowEndScreen();
        AudioManager.Instance.PlaySound("BGM_EndMusic");
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
