using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectSongButton : MonoBehaviour
{
    public int songNumber;
    public SongManager songManager;

    public void OnClick()
    {
        songManager.SelectSong(songNumber);
    }
}
