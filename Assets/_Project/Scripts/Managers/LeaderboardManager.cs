using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Dan.Main;
using Dan.Models;

public class LeaderboardManager : MonoBehaviour
{
    public static LeaderboardManager Instance;
    [Header("Singleton")]
    [SerializeField] bool _destroyOnLoad = false;

    string _publicKey = LeaderboardKey.PUBLIC_KEY;

    [Header("Leaderboard Settings")]
    [SerializeField] Transform _leaderboardContentTF;
    [SerializeField] GameObject _leaderboardEntryPrefab;
    [SerializeField] GameObject _leaderboardPanel, _leaderboardSubmissionPanel, _leaderboardSubmissionText;
    [SerializeField] TMP_InputField _playerNameInputField;

    [SerializeField] int _maxEntryCount = 30;

    List<LeaderboardEntry> _currentEntries;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }

        if (_destroyOnLoad) { DontDestroyOnLoad(this.gameObject); }
    }

    void Start()
    {
        _currentEntries = new List<LeaderboardEntry>();
        ResetLeaderboardUI();
        GetLeaderBoard();
    }

    public void GetLeaderBoard()
    {
        LeaderboardCreator.GetLeaderboard(_publicKey, OnLeaderboardLoaded, ErrorCallback);
    }

    public void UploadNewEntry()
    {
        if (_playerNameInputField.text == string.Empty)
        { return; }

        string username = _playerNameInputField.text;
        int score = Player.Instance.currentScore;

        SetEntry(username, score);

    }

    void SetEntry(string _username, int _score)
    {
        LeaderboardCreator.UploadNewEntry(_publicKey, _username, _score, ((msg) =>
        {
            _leaderboardSubmissionPanel.SetActive(false);
            _leaderboardPanel.SetActive(true);
            _leaderboardSubmissionText.SetActive(true);
            GetLeaderBoard();
        }));
    }

    void OnLeaderboardLoaded(Entry[] entries)
    {
        foreach (Transform _childTF in _leaderboardContentTF)
        {
            _currentEntries = new List<LeaderboardEntry>();
            Destroy(_childTF.gameObject);
        }

        if (entries.Length < _maxEntryCount)
        {
            foreach (var t in entries)
            {
                LeaderboardEntry newEntry = Instantiate(_leaderboardEntryPrefab, _leaderboardContentTF)
                                               .GetComponent<LeaderboardEntry>();

                newEntry.InitializeEntry(t.Rank, t.Username, t.Score);
                _currentEntries.Add(newEntry);
            }
        }
        else
        {
            for (int i = 0; i < _maxEntryCount; i++)
            {
                Entry t = entries[i];
                LeaderboardEntry newEntry = Instantiate(_leaderboardEntryPrefab, _leaderboardContentTF)
                                               .GetComponent<LeaderboardEntry>();

                newEntry.InitializeEntry(t.Rank, t.Username, t.Score);
                _currentEntries.Add(newEntry);
            }

            GetPersonalEntry();
        }
    }

    public void GetPersonalEntry()
    {
        LeaderboardCreator.GetPersonalEntry(_publicKey,OnPersonalEntryLoaded, ErrorCallback);
    }

    void OnPersonalEntryLoaded(Entry _entry)
    {
        LeaderboardEntry newEntry = Instantiate(_leaderboardEntryPrefab, _leaderboardContentTF)
                                               .GetComponent<LeaderboardEntry>();

        newEntry.InitializeEntry(_entry.Rank, _entry.Username, _entry.Score);
    }

    void ErrorCallback(string error)
    {
        Debug.LogError(error);
    }

    void ResetLeaderboardUI()
    {
        _leaderboardSubmissionPanel.SetActive(true);
        _leaderboardPanel.SetActive(false);
        _leaderboardSubmissionText.SetActive(false);
    }
}
