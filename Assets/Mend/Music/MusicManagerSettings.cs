using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
#endif

namespace PointNSheep.Common.Music
{
    
    public class MusicManagerSettings : ScriptableObject
    {
        public const string FullPath = "Assets/JammerTools/Resources/MusicManagerSettings.asset";
        public const string PrefabPath = "Assets/JammerTools/Resources/MusicManagerSettings.asset";
        public const string ResourcesPath = "Resources/MusicManagerSettings.asset";


        public float DefaultFadeInTime = .5f;
        public TrackPlayer trackPlayerPrefab;
        public MusicTrack startingTrack;
        public MusicTrack startStinger;

#if UNITY_EDITOR
        [MenuItem("JammerTooks/Create Music Settings")]
        public static void CreateMusicSettings()
        {
            var asset = ScriptableObject.CreateInstance<MusicManagerSettings>();

            AssetDatabase.CreateAsset(asset, FullPath);


            var obj = new GameObject("Track Player");
            var tp = obj.AddComponent<TrackPlayer>();
            var audio = obj.AddComponent<AudioSource>();
            audio.playOnAwake = false;

            var serialized = new SerializedObject(obj);
            serialized.FindProperty("audioSource").objectReferenceValue = audio;
            var prefab = PrefabUtility.SaveAsPrefabAsset(obj, PrefabPath);
            asset.trackPlayerPrefab = prefab.GetComponent<TrackPlayer>();


            AssetDatabase.SaveAssets();
            EditorUtility.FocusProjectWindow();

            Selection.activeObject = asset;
        }
#endif
    }
}
