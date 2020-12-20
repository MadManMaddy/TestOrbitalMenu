using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/PersonalisedPlaylist")]
public class SO_PersonalisedPlaylist : ScriptableObject
{
    //have to use custom editor to view this
    public Dictionary<int, Playlist> playlists = new Dictionary<int, Playlist>();

    public void InitializeDictionary()
    {
        playlists = new Dictionary<int, Playlist>(); // have to remove this when saving to memory
        foreach (var item in Enum.GetNames(typeof(PlaylistsName)))
        {
            playlists.Add(playlists.Count, new Playlist(item));
        }
    }

    public void AddToPlaylist(int playlistNumber, SongMetaData song)
    {
        if (playlists.ContainsKey(playlistNumber))
        {
            if (!playlists[playlistNumber].songs.Contains(song))
            {
                playlists[playlistNumber].songs.Add(song);
            }
        }
        else
        {
            playlists.Add(playlistNumber, new Playlist(song));
            playlists[playlistNumber].playlistName = ((PlaylistsName)playlistNumber).ToString();
        }
    }
    public void RemoveFromPlaylist(int playlistNumber, SongMetaData song)
    {
        if (playlists.ContainsKey(playlistNumber))
        {
            if (playlists[playlistNumber].songs.Contains(song))
            {
                playlists[playlistNumber].songs.Remove(song);
            }
        }
    }
    public Playlist GetPlaylist(int playlistNumber)
    {
        return playlists[playlistNumber];
    }
    public bool[] GetPlaylistStatus(SongMetaData songMetaData)
    {
        bool[] playlistStatus = new bool[playlists.Count];
        for (int i = 0; i < playlists.Count; i++)
        {
            playlistStatus[i] = playlists[i].songs.Contains(songMetaData);
        }
        return playlistStatus;
    }

}

[Serializable]
public class Playlist
{
    public string playlistName = "favourite";
    public List<SongMetaData> songs = new List<SongMetaData>();
    public int currentSelection = 0;

    public Playlist(SongMetaData songMetaData)
    {
        songs.Clear();
        songs.Add(songMetaData);
    }

    public Playlist(string name)
    {
        playlistName = name;
    }
    // Can Change When Switched To Itterator Pattern
    #region Itteration 

    public void SetCurrentSong(int i)
    {
        if (i >= 0 && i < songs.Count)
        {
            currentSelection = i;
        }
    }

    public SongMetaData GetCurrentSong()
    {
        return songs[currentSelection];
    }

    public SongMetaData GetNextSong()
    {
        currentSelection = (currentSelection + 1 > songs.Count - 1) ? 0 : currentSelection + 1;
        return GetCurrentSong();
    }

    public SongMetaData GetPreviousSong()
    {
        currentSelection = (currentSelection - 1 < 0) ? songs.Count - 1 : currentSelection - 1;
        return GetCurrentSong();
    }
    #endregion
}