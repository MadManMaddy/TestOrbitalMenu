using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

[RequireComponent(typeof(LookAtConstraint))]
public class SongViewCard : MonoBehaviour
{
    [SerializeField] TMP_Text songTitle;
    [SerializeField] Image coverPic;
    [SerializeField] Canvas canvas;

    private void OnMouseDown()
    {
        Debug.Log("clicked : " + songTitle.text);
    }

    public void SetData(SongData songData, Camera lookatCameraTarget, Vector3 spawnPosition)
    {
        songTitle.text = songData.name;
        coverPic.sprite = songData.coverPic;

        ConstraintSource source = new ConstraintSource();
        source.sourceTransform = lookatCameraTarget.transform;
        source.weight = 1;
        GetComponent<LookAtConstraint>().SetSource(0, source);

        canvas.worldCamera = lookatCameraTarget;

        transform.localPosition = spawnPosition;
    }

    public void UpdatePosition(Vector3 newPosition)
    {
        transform.localPosition = newPosition;
    }

}
