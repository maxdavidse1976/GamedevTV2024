using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] private Canvas _upgradeCanvas;

    void Awake()
    {
        Instance = this;
    }

    public void ShowUpgradeScreen()
    {
        _upgradeCanvas.gameObject.SetActive(true);
    }
}
