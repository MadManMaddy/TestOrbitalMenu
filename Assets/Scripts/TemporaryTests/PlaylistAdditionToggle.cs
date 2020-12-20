using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaylistAdditionToggle : MonoBehaviour
{
    public PlaylistsName playlistType;
    public SongManager songManager;
    public bool disableToggleCallback = true;
    public void OnToggleValueChanged(bool toggleValue)
    {
        if (!disableToggleCallback)
        {
            songManager.AddOrRemoveToFromPlaylist(playlistType);
        }
    }
}
