using UnityEngine;
using System;
namespace WZK
{
    public class Sound
    {
        public string _name;
        public string _id;
        public AudioClip _clip;
        public bool _loop = false;
        public AudioSource _audioSource;
        public Action _competeAction;
        public float _playTime = 0;
        public bool _isScaleTime = false;
        public bool IsFinish
        {
            get { return _playTime >= _clip.length; }
        }
        public float Process
        {
            get { return ((float)_audioSource.timeSamples) / ((float)_clip.samples); }
        }
        public Sound SetLoop(bool loop = true)
        {
            _loop = loop;
            _audioSource.loop = loop;
            return this;
        }
        public Sound SetVolume(float value = 1.0f)
        {
            _audioSource.volume = value;
            return this;
        }
        public Sound SetID(string id)
        {
            _id = id;
            return this;
        }
        public Sound OnComplete(Action complete)
        {
            _competeAction = complete;
            return this;
        }
        public void Finish(bool b = true)
        {
            if (b && _competeAction != null) _competeAction();
            SoundManager.Instance.RemoveSound(this);
        }
    }
}

