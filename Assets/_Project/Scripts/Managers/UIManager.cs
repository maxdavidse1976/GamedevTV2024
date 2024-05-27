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


    void Awake()
    {
        Instance = this;
    }

    public void ShowUpgradeScreen()
    {
        
        _upgradeCanvas.gameObject.SetActive(true);
    }

    public void ShowUpgrade(Upgrade upgrade)
    {
        if (upgrade.target == UpgradesManager.UpgradeTarget.Player)
        {
            _playerUpgradeName.text = upgrade.upgradeName;
            //_playerUpgradeIcon = upgrade.icon;
        }

        if (upgrade.target == UpgradesManager.UpgradeTarget.Tower)
        {
            _towerUpgradeName.text = upgrade.upgradeName;
        }

        if (upgrade.type == UpgradesManager.UpgradeType.PowerUpgrade)
        {
            _powerUpgradeName.text = upgrade.upgradeName;
        }

    }
}
