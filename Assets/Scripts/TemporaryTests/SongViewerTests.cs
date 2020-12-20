using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SongViewerTests : MonoBehaviour
{
    public SongManager songManager;

    public Text currentPlaylistHeader;
    public Button songPrefabButton;
    public Transform songsButtonParent;
    List<GameObject> currentSongButtons = new List<GameObject>();

    public Text currentSongName;
    public Toggle fav, fun;

    public List<GameObject> playerObjects;

    private void Awake()
    {
        songManager.UpdatedPlaylist += ChangePlaylist;
        songManager.SongChanged += UpdateSongVisualizer;
        songManager.PlaylistStatusChanged += UpdateSongPlaylistStatus;
        foreach (var item in playerObjects)
        {
            item.SetActive(false);
        }
    }

    void ChangePlaylist()
    {
        for (int i = currentSongButtons.Count - 1; i >= 0; i--)
        {
            Destroy(currentSongButtons[i]);
        }
        currentPlaylistHeader.text = songManager.GetCurrentPlaylist().playlistName;

        List<SongMetaData> currentPlaylist = songManager.GetCurrentPlaylist().songs;
        foreach (var item in currentPlaylist)
        {
            GameObject button = Instantiate(songPrefabButton.gameObject);
            button.transform.SetParent(songsButtonParent);
            button.GetComponentInChildren<Text>().text = item.title;
            currentSongButtons.Add(button);
            SelectSongButton selectSongButton = button.GetComponent<SelectSongButton>();
            selectSongButton.songManager = songManager;
            selectSongButton.songNumber = currentPlaylist.IndexOf(item);
            button.SetActive(true);
        }
    }

    void UpdateSongVisualizer(SongMetaData s)
    {
        currentSongName.text = s.title;
        foreach (var item in playerObjects)
        {
            if (!item.activeSelf)
            {
                item.SetActive(true);
            }
        }
    }
    void UpdateSongPlaylistStatus(bool[] status)
    {
        fav.GetComponent<PlaylistAdditionToggle>().disableToggleCallback = true;
        fun.GetComponent<PlaylistAdditionToggle>().disableToggleCallback = true;
        fav.isOn = status[1];
        fun.isOn = status[2];
        fav.GetComponent<PlaylistAdditionToggle>().disableToggleCallback = false;
        fun.GetComponent<PlaylistAdditionToggle>().disableToggleCallback = false;
    }



}
