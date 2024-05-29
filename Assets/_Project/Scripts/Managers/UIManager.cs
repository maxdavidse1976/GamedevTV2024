using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] Canvas _upgradeCanvas;
    [SerializeField] TMP_Text _playerUpgradeName;
    [SerializeField] Sprite _playerUpgradeIcon;
    [SerializeField] TMP_Text _towerUpgradeName;
    [SerializeField] Sprite _towerUpgradeIcon;
    [SerializeField] TMP_Text _powerUpgradeName;
    [SerializeField] Sprite _powerUpgradeIcon;


    public bool IsUpgradeScreenActive() => _upgradeCanvas.gameObject.activeSelf;
    
    void Awake()
    {
        Instance = this;
    }

    public void ShowUpgradeScreen()
    {
        _upgradeCanvas.gameObject.SetActive(true);
    }

    public void HideUpgradeScreen()
    {
        _upgradeCanvas.gameObject.SetActive(false);
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
        
    }
}
