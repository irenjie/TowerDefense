using Helper;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MTL.Config {
    [CreateAssetMenu(fileName = nameof(AudioConfig), menuName = "MTL/Config/Create Audio Config")]
    public class AudioConfig : ScriptableObject {
        [Header("��ť���")]
        public AudioClip ButtonSound;

        private static AudioConfig _config;
        public static AudioConfig Load() {
            if (_config == null) {
                _config = LoaderHelper.Get().GetAsset<AudioConfig>($"Config/{nameof(AudioConfig)}.asset");
            }

            return _config;
        }
    }
}