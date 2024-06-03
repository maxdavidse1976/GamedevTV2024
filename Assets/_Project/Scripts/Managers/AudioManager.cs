using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public enum AudioClipType
{
    Music,
    SoundEffect,
    UserInterface
}

[System.Serializable]
public class AudioProfileData
{
    public string name;
    public AudioClip clip;
    public AudioClipType clipType;
    [Range (0f,1f)] public float volume;
    public bool playOnAwake, loop, randomizePitch;
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [Header("Singleton")]
    [SerializeField] bool m_destroyOnLoad = false;

    [SerializeField] private List<AudioProfileData> m_audioProfileDataList;
    private List<AudioProfile> m_audioProfiles;
    [SerializeField] private AudioMixer m_mixer;
    [SerializeField] private AudioMixerGroup m_musicMixer, m_sfxMixer, m_uiMixer;
    [SerializeField] private Slider m_musicSlider, m_sfxSlider, m_uiSlider;
    //private List<GameObject> m_audioSourceObjects;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }

        if (m_destroyOnLoad) { DontDestroyOnLoad(this.gameObject); }
    
        InitializeAudioSources();
    }

    private void Start()
    {
        //if (!PlayerPrefs.HasKey("Master")&& !PlayerPrefs.HasKey("Music") && !PlayerPrefs.HasKey("SFX") && !PlayerPrefs.HasKey("UI"))
        //{
        //    ResetSettings();
        //}
        //else
        //{
        //    RecallSettings();
        //}
    }

    private void InitializeAudioSources()
    {
        if (m_audioProfileDataList.Count > 0)
        {
            m_audioProfiles = new List<AudioProfile>();

            foreach (AudioProfileData _data in m_audioProfileDataList)
            {
                GameObject sfxObject = new GameObject(_data.name, typeof(AudioProfile));
                sfxObject.transform.SetParent(this.transform);

                AudioProfile profile = sfxObject.GetComponent<AudioProfile>();

                profile.InitializeProfile(_data.name, _data.clip, _data.playOnAwake, _data.loop,
                                          _data.randomizePitch, _data.volume, _data.clipType, GetAudioMixerGroup(_data.clipType));

                m_audioProfiles.Add(profile);
            }
        }
    }
    private AudioMixerGroup GetAudioMixerGroup(AudioClipType _clipType)
    {
        switch (_clipType)
        {
            case AudioClipType.Music:
                return m_musicMixer;
            case AudioClipType.SoundEffect:
                return m_sfxMixer;
            case AudioClipType.UserInterface:
                return m_uiMixer;
            default:
                return null;
        }
    }

    public bool IsValidName(string _name, out AudioProfile _profile)
    {
        foreach (AudioProfile profile in m_audioProfiles)
        {
            if(profile.GetName() == _name)
            {
                _profile = profile;
                return true;
            }
        }
        _profile = null;
        return false;
    }

    public void PlaySound(string _name)
    {
        if(IsValidName(_name,out AudioProfile _profile))
        {
            _profile.PlayAudio();
        }
    }

    public void PauseSound(string _name)
    {
        if (IsValidName(_name, out AudioProfile _profile))
        {
            _profile.Pause();
        }
    }

    public void StopSound(string _name)
    {
        if (IsValidName(_name, out AudioProfile _profile))
        {
            _profile.Stop();
        }
    }

    public void PauseAllSound()
    {
        foreach (AudioProfile profile in m_audioProfiles)
        {
            profile.Pause();
        }
    }

    public void StopAllSound()
    {
        foreach (AudioProfile profile in m_audioProfiles)
        {
            profile.Stop();
        }
    }

    public void ResetSettings()
    {
        SetMusicVol(0);
        SetSfxVol(0);
        SetUIVol(0);
    }

    private void RecallSettings()
    {
        float musicVol = PlayerPrefs.GetFloat("Music");
        SetMusicVol(musicVol);
        float sfxVol = PlayerPrefs.GetFloat("SFX");
        SetSfxVol(sfxVol);
        float uiVol = PlayerPrefs.GetFloat("UI");
        SetUIVol(uiVol);
    }

    public void SetMusicVol()
    {
        m_mixer.SetFloat("Music", m_musicSlider.value);
        PlayerPrefs.SetFloat("Music", m_musicSlider.value);
    }
    private void SetMusicVol(float _volume)
    {
        m_mixer.SetFloat("Music", _volume);
        PlayerPrefs.SetFloat("Music", _volume);
        m_musicSlider.value = PlayerPrefs.GetFloat("Music");
    }

    public void SetSfxVol()
    {
        m_mixer.SetFloat("SFX", m_sfxSlider.value);
        PlayerPrefs.SetFloat("SFX", m_sfxSlider.value);
    }
    private void SetSfxVol(float volume)
    {
        m_mixer.SetFloat("SFX", volume);
        PlayerPrefs.SetFloat("SFX", volume);
        m_sfxSlider.value = PlayerPrefs.GetFloat("SFX");
    }

    public void SetUIVol()
    {
        m_mixer.SetFloat("UI", m_uiSlider.value);
        PlayerPrefs.SetFloat("UI", m_uiSlider.value);
    }
    private void SetUIVol(float volume)
    {
        m_mixer.SetFloat("UI", volume);
        PlayerPrefs.SetFloat("UI", volume);
        m_uiSlider.value = PlayerPrefs.GetFloat("UI");
    }


}
