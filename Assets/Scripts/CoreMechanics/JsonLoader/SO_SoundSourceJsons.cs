using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName = "SO/SoundSourceList")]
public class SO_SoundSourceJsons : ScriptableObject
{
    [SerializeField] List<string> soundSourceJsons;

    [SerializeField] int loadFromSourceNumber;

    string jsonString;
    SongsPathData songsPathData;

    public Action JsonDataLoaded;

    void LoadJson()
    {
        TextAsset textAsset = Resources.Load<TextAsset>(soundSourceJsons[loadFromSourceNumber]);
        jsonString = textAsset.text;
        songsPathData = JsonUtility.FromJson<SongsPathData>(jsonString);

        JsonDataLoaded?.Invoke();
    }

    public SongsPathData GetSongDataPaths()
    {
        LoadJson();
        return songsPathData;
    }

}

[Serializable]
public class SongsPathData
{
    public List<Songs> Songs = new List<Songs>();
}

[Serializable]
public class Songs
{
    public string Title;
    public string Path;
}