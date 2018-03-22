using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;
namespace WZK
{
    public class SoundManager : MonoBehaviour
    {
        public enum SoundID
        {
            人声,
            音效
        }
        private float _bgSoundVolume = 0.6f;
        private List<Sound> _playingSoundList = new List<Sound>();
        private static SoundManager _instance = null;
        private float _deltaTime = 0;
        private AssetBundle _soundAB;
        private AssetBundle _voiceAB;
        public string _pathHead = "";//路径
        public string _language = "zh";//语言
        public static SoundManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = (new GameObject("声音管理")).AddComponent<SoundManager>();
                }
                return _instance;
            }
        }
        void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
        /// <summary>
        /// 播放人声
        /// </summary>
        /// <param name="type"></param>
        /// <param name="isRepeatType">同类型的音频，是否叠加播放</param>
        /// <param name="isRepeatSame">该音频正在播，是否叠加播放</param>
        /// <returns></returns>
        public Sound PlayVoice(Enum type, bool isRepeatType = false, bool isRepeatSame = true)
        {
            if (SwitchSoundID(type) != SoundID.人声) Debug.LogError("播放类型不对，应用VoiceType！");
            return Play(type, SoundID.人声, isRepeatType, isRepeatSame);
        }
        /// <summary>
        /// 播放音效
        /// </summary>
        /// <param name="type"></param>
        /// <param name="isRepeatType">同类型的音频，是否叠加播放</param>
        /// <param name="isRepeatSame">该音频正在播，是否叠加播放</param>
        /// <returns></returns>
        public Sound PlaySound(Enum type, bool isRepeatType = true, bool isRepeatSame = true)
        {
            if (SwitchSoundID(type) != SoundID.音效) Debug.LogError("播放类型不对，应用SoundType！");
            return Play(type, SoundID.音效, isRepeatType, isRepeatSame);
        }
        private Sound Play(Enum type, SoundID id, bool isRepeatType, bool isRepeatSame)
        {
            if (isRepeatType == false) DestroyAllSound(id);
            string soundName = type.GetEnumDescription();
            AudioClip ac;
            if (id == SoundID.音效)
            {
                if (_soundAB == null) _soundAB = AssetBundle.LoadFromFile(_pathHead + "sound.unity3d");
                ac = _soundAB.LoadAsset<AudioClip>(soundName);
            }
            else
            {
                if (_voiceAB == null) _voiceAB = AssetBundle.LoadFromFile(_pathHead + _language + ".unity3d");
                ac = _voiceAB.LoadAsset<AudioClip>(soundName);
            }
            if (!isRepeatSame)
                if (IsPlaying(type))
                    return null;
            Sound sound = new Sound();
            sound._clip = ac;
            sound._name = soundName;
            sound._audioSource = gameObject.AddComponent<AudioSource>();
            sound._audioSource.clip = ac;
            sound._audioSource.Play();
            _playingSoundList.Add(sound);
            return sound;
        }
        public void RemoveSound(Sound sound)
        {
            Destroy(sound._audioSource);
            _playingSoundList.Remove(sound);
            sound = null;
        }
        public void PauseAllSound()
        {
            for (int i = 0; i < _playingSoundList.Count; i++)
            {
                _playingSoundList[i]._audioSource.Pause();
            }
        }
        public void ResumeAllSound()
        {
            for (int i = 0; i < _playingSoundList.Count; i++)
            {
                _playingSoundList[i]._audioSource.UnPause();
            }
        }
        public bool IsPlaying(Enum type)
        {
            if (_playingSoundList.Find(x => x._name == type.GetEnumDescription()) != null) return true;
            return false;
        }
        public void DestroyAllSound(SoundID soundID)
        {
            List<Sound> sounds = _playingSoundList.Where(s => s._id.Equals(soundID.ToString())).ToList();
            for (int i = sounds.Count - 1; i >= 0; i--)
            {
                sounds[i].Finish(false);
            }
            sounds.Clear();
        }
        public void DestroyAllSound()
        {
            for (int i = _playingSoundList.Count - 1; i >= 0; i--)
            {
                _playingSoundList[i].Finish(false);
            }
        }
        public void DestroySound(Enum type, bool isComplete = false)
        {
            _playingSoundList.Find(x => x._name == type.GetEnumDescription()).Finish(isComplete);
        }
        public float GetSoundLength(Enum type)
        {
            return _playingSoundList.Find(x => x._name == type.GetEnumDescription())._clip.length;
        }
        private SoundID SwitchSoundID(Enum type)
        {
            if (type.GetType().Name == "VoiceType")
            {
                return SoundID.人声;
            }
            else
            {
                return SoundID.音效;
            }
        }
        void Update()
        {
            _playingSoundList.ToList().ForEach(x =>
            {
                if (x._isScaleTime == false)
                {
                    //Time.unscaledDeltaTime 不考虑timescale时候与deltaTime相同，若timescale被设置，则无效。
                    _deltaTime = Time.unscaledDeltaTime;
                }
                else
                {
                    _deltaTime = Time.deltaTime;
                }
                x._playTime += _deltaTime;
                if (!x._audioSource.isPlaying && x.IsFinish)
                {
                    if (!(x._loop))
                        x.Finish();
                }
            });
        }
        /// <summary>
        /// 卸载资源
        /// </summary>
        public void UnResources()
        {
            Debug.Log("卸载音频资源");
            DestroyAllSound();
            if (_soundAB)
            {
                _soundAB.Unload(true);
                _soundAB = null;
            }
            if (_voiceAB)
            {
                _voiceAB.Unload(true);
                _voiceAB = null;
            }
            _instance = null;
            Destroy(gameObject);
        }
    }
}

