using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SongManager : MonoBehaviour
{
    [SerializeField] SoundResourceLoader soundResource;
    [SerializeField] SO_PersonalisedPlaylist playlists;

    [SerializeField] Playlist currentPlaylist;
    public Action UpdatedPlaylist;

    public Action<SongMetaData> SongChanged;
    public Action<bool[]> PlaylistStatusChanged; //currently have only 2 playlists so using this later can have individual updating actions

    //for now will attach to itself
    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        LoadAllSongs();

    }

    public void PlaySong()
    {
        audioSource.clip = GetCurrentSong().clip;
        audioSource.Play();
        SongChanged?.Invoke(GetCurrentSong());
        PlaylistStatusChanged?.Invoke(GetPlaylistStatus());
    }
    public void PauseUnPauseSong()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Pause();
        }
        else
        {
            audioSource.UnPause();
        }
    }

    public void NextSong()
    {
        GetNextSong();
        PlaySong();
    }

    public void PreviousSong()
    {
        GetPreviousSong();
        PlaySong();
    }

    public void SelectSong(int i)
    {
        currentPlaylist.SetCurrentSong(i);
        PlaySong();
    }


    void LoadAllSongs()
    {
        if (playlists.playlists.Count == 0)
        {
            playlists.InitializeDictionary();
        }
        if (playlists.playlists[(int)PlaylistsName.all].songs.Count < 1)
        {
            playlists.playlists[(int)PlaylistsName.all].songs = soundResource.GetSongs();
        }
        currentPlaylist = playlists.GetPlaylist((int)PlaylistsName.all);
        UpdatedPlaylist?.Invoke();

    }

    public Playlist GetCurrentPlaylist()
    {
        return currentPlaylist;
    }

    SongMetaData GetCurrentSong()
    {
        return currentPlaylist.GetCurrentSong();
    }

    SongMetaData GetNextSong()
    {
        return currentPlaylist.GetNextSong();
    }

    SongMetaData GetPreviousSong()
    {
        return currentPlaylist.GetPreviousSong();
    }

    public void GetPlaylist(PlaylistsName playlistsName)
    {
        currentPlaylist = playlists.GetPlaylist((int)playlistsName);
        UpdatedPlaylist?.Invoke();
    }
    public void GetPlaylist(int playlistNumber)
    {
        currentPlaylist = playlists.GetPlaylist(playlistNumber);
        UpdatedPlaylist?.Invoke();
    }

    public void AddOrRemoveToFromPlaylist(PlaylistsName playlistsName)
    {
        if (playlists.GetPlaylistStatus(GetCurrentSong())[(int)playlistsName])
        {
            playlists.RemoveFromPlaylist((int)playlistsName, GetCurrentSong());
        }
        else
        {
            playlists.AddToPlaylist((int)playlistsName, GetCurrentSong());
        }
        //PlaylistStatusChanged?.Invoke(GetPlaylistStatus());
    }

    bool[] GetPlaylistStatus()
    {
        return playlists.GetPlaylistStatus(GetCurrentSong());
    }

}

public enum PlaylistsName
{
    all,
    favourite,
    fun
}