using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesManager : MonoBehaviour
{
    public enum UpgradeTarget { Player, Tower }
    public enum UpgradeType { PlayerStats, TowerrStats, PowerUpgrade }
    public enum UpgradeStats
    {
        MaxHealth,
        RegenerationAmount,
        MoveSpeed,
        Damage,
        BulletLife,
        Penetration
    }
    public enum UpgradePowers { FireBall, IceBall, ThunderBall }

    public List<Upgrade> availableUpgrades; // List of all possible upgrades

    private List<Upgrade> playerUpgrades;
    private List<Upgrade> towerUpgrades;
    private List<Upgrade> bulletUpgrades;

    void Start()
    {
        // Initialize upgrade lists
        InitializeUpgradeLists();
    }

    void InitializeUpgradeLists()
    {
        // Filter upgrades for the player and tower
        playerUpgrades = availableUpgrades.FindAll(u => u.target == UpgradeTarget.Player && u.type != UpgradeType.PowerUpgrade);
        towerUpgrades = availableUpgrades.FindAll(u => u.target == UpgradeTarget.Tower && u.type != UpgradeType.PowerUpgrade);
        bulletUpgrades = availableUpgrades.FindAll(u => u.type == UpgradeType.PowerUpgrade);
    }

    public void ProvideRandomUpgrades()
    {
        // Select one random upgrade for each category
        Upgrade playerUpgrade = GetRandomUpgrade(playerUpgrades);
        Upgrade towerUpgrade = GetRandomUpgrade(towerUpgrades);
        Upgrade bulletUpgrade = GetRandomUpgrade(bulletUpgrades);

        // Provide the selected upgrades
        ProvideUpgrade(playerUpgrade);
        ProvideUpgrade(towerUpgrade);
        ProvideUpgrade(bulletUpgrade);
    }

    private Upgrade GetRandomUpgrade(List<Upgrade> upgrades)
    {
        if (upgrades.Count == 0)
        {
            return null; // No upgrades available
        }

        int randomIndex = Random.Range(0, upgrades.Count);
        return upgrades[randomIndex];
    }

    private void ProvideUpgrade(Upgrade upgrade)
    {
        if (upgrade == null)
        {
            Debug.LogWarning("No upgrade available for this category.");
            return;
        }

        // Implement the logic to apply the upgrade to the player or tower
        Debug.Log($"Provided {upgrade.target} with {upgrade.upgradeName}: {upgrade.description} (Value: {upgrade.value})");
    }
}
