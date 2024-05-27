using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesManager : MonoBehaviour
{
    public enum UpgradeTarget { Player, Tower}
    public enum UpgradeType { Stats, PowerUpgrade }
    public enum UpgradeStats
    {
        MaxHealth,
        RegenerationAmount,
        MoveSpeed,
        Damage,
        BulletLife,
        Penetration
    }
    public enum UpgradePowers { }
}
