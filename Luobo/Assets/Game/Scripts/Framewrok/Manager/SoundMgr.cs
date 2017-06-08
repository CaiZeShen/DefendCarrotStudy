using UnityEngine;

// ****************************************************************
// 功能：声音管理类
// 创建：蔡泽深
// 时间：17-5-4
// 修改内容：										修改者姓名：
// ****************************************************************

[RequireComponent(typeof(AudioSource))]
public class SoundMgr : SingletonMono<SoundMgr> {
    private AudioSource bgAudioSource;

    /// <summary>
    /// 音效大小
    /// </summary>
    public float EffectVolume { get; set; }

    /// <summary>
    /// 背景音乐大小
    /// </summary>
    public float BGVolume {
        get { return bgAudioSource.volume; }
        set { bgAudioSource.volume = value; }
    }

    protected override void Awake() {
        base.Awake();
        bgAudioSource = GetComponent<AudioSource>();
        bgAudioSource.playOnAwake = false;
        bgAudioSource.loop = true;

        DontDestroyOnLoad(gameObject);
    }

    public void PlayBG(string soundName) {
        // 当前在播放的音乐文件
        string oldName;
        if (bgAudioSource.clip == null)
            oldName = "";
        else
            oldName = bgAudioSource.clip.name;

        if (oldName != soundName) {
            // 加载音乐
            AudioClip clip = ResourcesMgr.Instance.Load<AudioClip>(Consts.SoundResDir);

            // 播放
            if (clip != null) {
                bgAudioSource.clip = clip;
                bgAudioSource.Play();
            }
        }
    }

    // 停止音乐
    public void StopBG() {
        bgAudioSource.Stop();
        bgAudioSource.clip = null;
    }

    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="audioName"></param>
    public void PlayEffect(string soundName) {
        AudioClip clip = ResourcesMgr.Instance.Load<AudioClip>(Consts.SoundResDir);
        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position,EffectVolume);
    }
}
