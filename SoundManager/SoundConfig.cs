using UnityEngine;
using System.Collections.Generic;
namespace WZK
{
    /// <summary>
    /// 声音配置
    /// </summary>
    [CreateAssetMenu(fileName = "xxx音频配置", menuName = "创建序列化音频配置")]
    public class SoundConfig : ScriptableObject
    {
        public string _savePath = "";//存储路径
        public string _nameSpace = "";//命名空间
        public string _fileName = "";//.cs配置文件名
        public bool _isResources = false;//是否Resources下资源
        public List<Config> _voiceList = new List<Config>();//人声列表
        public string _voiceEnumType = "VoiceType";//人声枚举类型
        public List<Config> _soundList = new List<Config>();//音效列表
        public string _soundEnumType = "SoundType";//音效枚举类型
        [System.Serializable]
        public class Config
        {
            public AudioClip _audioClip;//声音源
            public string _desc;//描述-用于枚举
            public Config(AudioClip audioClip = null, string desc = "")
            {
                _audioClip = audioClip;
                _desc = desc;
            }
        }
    }
}
