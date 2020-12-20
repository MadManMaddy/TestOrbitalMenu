using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OrbitalPlacement : MonoBehaviour
{
    [SerializeField] Camera cameraReference;
    [SerializeField] Transform OrbitalSpawnParent;

    [SerializeField] SongViewCard songViewCardPrefab;
    [SerializeField] List<SongData> songDatas = new List<SongData>();
    List<SongViewCard> songViewCards = new List<SongViewCard>();

    List<Vector3> positions = new List<Vector3>();

    [ContextMenu("SpawnSongCard")]
    void SpawnSongCards()
    {
        positions.Clear();
        positions = OrbitalPositionCalculator.RecalculateOrbitalPositions(songDatas.Count, 4).ToList();

        foreach (var item in songDatas)
        {
            SongViewCard songViewCard = Instantiate(songViewCardPrefab);
            songViewCard.transform.SetParent(OrbitalSpawnParent);
            songViewCard.SetData(item, cameraReference, positions[songDatas.IndexOf(item)]);
        }
    }

    [ContextMenu("DeleteAllSongCards")]
    void DeleteAllSongCards()
    {
        for (int i = OrbitalSpawnParent.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(OrbitalSpawnParent.GetChild(i).gameObject);
        }
    }
}

[Serializable]
public class SongData
{
    public string name;
    public Sprite coverPic;
}
