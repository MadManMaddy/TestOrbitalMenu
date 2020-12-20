using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SoundResourceLoader : MonoBehaviour
{
    [SerializeField] SO_SoundSourceJsons soundSourceJsons;

    public List<SongMetaData> GetSongs()
    {
        List<SongMetaData> audioClips = new List<SongMetaData>();
        foreach (var item in soundSourceJsons.GetSongDataPaths().Songs)
        {
            SongMetaData songMetaData = new SongMetaData();
            songMetaData.title = item.Title;
            songMetaData.path = item.Path;
            songMetaData.clip = Resources.Load(item.Path.Split('.')[0]) as AudioClip;
            audioClips.Add(songMetaData);
        }

        return audioClips;
    }
}
