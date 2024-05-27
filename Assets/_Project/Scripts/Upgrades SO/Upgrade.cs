using UnityEngine;

[CreateAssetMenu(fileName = "NewUpgrade", menuName = "Upgrades/Upgrade")]
public class Upgrade : ScriptableObject
{
    public UpgradesManager.UpgradeTarget target;
    public UpgradesManager.UpgradeType type;
    public UpgradesManager.UpgradeStats stat;
    public UpgradesManager.UpgradePowers power;
    public string upgradeName;
    public string description;
    public float value; // Value of the stat increase or power effect
}