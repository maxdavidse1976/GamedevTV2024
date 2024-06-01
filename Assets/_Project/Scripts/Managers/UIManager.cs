using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [Header("Singleton")]
    [SerializeField] bool m_destroyOnLoad = false;

    [SerializeField] Canvas _upgradeCanvas;
    [SerializeField] TMP_Text _playerUpgradeName;
    [SerializeField] Sprite _playerUpgradeIcon;
    [SerializeField] TMP_Text _towerUpgradeName;
    [SerializeField] Sprite _towerUpgradeIcon;
    [SerializeField] TMP_Text _powerUpgradeName;
    [SerializeField] Sprite _powerUpgradeIcon;

    [Space(5)]
    [Header("UI Screens")]
    [SerializeField] GameObject TitleScreen;
    [SerializeField] GameObject GameScreen;
    [SerializeField] GameObject UpgradeScreen,HUDScreen;
    [SerializeField] GameObject EndScreen;



    public bool IsUpgradeScreenActive() => _upgradeCanvas.gameObject.activeSelf;
    
    void Awake()
    {
        if (Instance == null) Instance = this;
        else if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }

        if (m_destroyOnLoad) { DontDestroyOnLoad(this.gameObject); }
    }

    public void ShowUpgradeScreen()
    {
        HUDScreen.SetActive(false);
        UpgradeScreen.SetActive(true);
    }

    void HideUpgradeScreen()
    {
        UpgradeScreen.SetActive(false);
        HUDScreen.SetActive(true);
    }

    public void ShowUpgrade(Upgrade upgrade)
    {
        if (upgrade.target == UpgradesManager.UpgradeTarget.Player)
        {
            Debug.Log("We have received a player upgrade");
            _playerUpgradeName.text = upgrade.upgradeName;
            //_playerUpgradeIcon = upgrade.icon;
        }

        if (upgrade.target == UpgradesManager.UpgradeTarget.Tower)
        {
            Debug.Log("We have received a tower upgrade");
            _towerUpgradeName.text = upgrade.upgradeName;
        }

        if (upgrade.type == UpgradesManager.UpgradeType.PowerUpgrade)
        {
            Debug.Log("We have received a power up");
            _powerUpgradeName.text = upgrade.upgradeName;
        }
    }

    public void UpgradePlayer()
    {
        
    }

    public void UpgradeTower()
    {
        
    }

    public void PowerUp()
    {
        
    }

    public void ContinueGamePlay()
    {
        HideUpgradeScreen();
        EnemyManager.Instance.StartWave();
    }

    public void ShowTitleScreen()
    {
        TitleScreen.SetActive(true);
        GameScreen.SetActive(false);
        EndScreen.SetActive(false);
    }

    public void ShowGameScreen()
    {
        TitleScreen.SetActive(false);
        GameScreen.SetActive(true);
        EndScreen.SetActive(false);
    }

    public void ShowEndScreen()
    {
        TitleScreen.SetActive(false);
        GameScreen.SetActive(false);
        EndScreen.SetActive(true);
    }
}
