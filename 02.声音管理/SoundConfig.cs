using UnityEngine;
using System.Collections.Generic;
namespace WZK
{
    /// <summary>
    /// 声音配置
    /// </summary>
    [CreateAssetMenu(fileName = "xxx音频资源", menuName = "创建场景序列化音频")]
    public class SoundConfig : ScriptableObject
    {
        [Header("背景音乐声音大小")]
        public float _bgSoundVolume =1f;
        public string _savePath = "";//存储路径
        public string _nameSpace = "";//命名空间
        public string _fileName = "";//.cs配置文件名
        public bool _isResources = false;//是否Resources下资源
        public List<Config> _voiceList = new List<Config>();//人声列表
        public string _voiceEnumType = "VoiceType";//人声枚举类型
        public List<Config> _soundList = new List<Config>();//音效列表
        public string _soundEnumType = "SoundType";//音效枚举类型
        public List<Config> _musiceList = new List<Config>();//背景音乐列表
        public string _musiceEnumType = "MusiceType";//背景音乐枚举类型
        public int _choseLanguage = 0;//选择的国际化语言
        public string _testLanguage = "";//测试语言
        public List<string> _languageTypeList = new List<string>() { "zht", "en" };//其他语言类型
        public List<LanguageAudioClip> _languageAudioClipList = new List<LanguageAudioClip>();//国际化音频
        [System.Serializable]
        public class Config
        {
            public AudioClip _audioClip;//声音源
            public string _desc;//描述-用于枚举
            public string _resourcesPath;//Resources下路径
            public Config(AudioClip audioClip = null, string resourcesPath = "", string desc = "")
            {
                _audioClip = audioClip;
                _resourcesPath = resourcesPath;
                _desc = desc;
            }
        }
        [System.Serializable]
        public class LanguageAudioClip
        {
            public string _language = "";
            public List<AudioClip> _audioClipList = new List<AudioClip>();
            public LanguageAudioClip(string language)
            {
                _language = language;
            }
        }
    }
}
