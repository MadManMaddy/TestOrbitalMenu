using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitalPlacement : MonoBehaviour
{
    [SerializeField]
    bool rotating;

    [SerializeField]
    float radius = 5f;

    [SerializeField]
    [Range(1f, 360f)]
    float orbitArc = 90f;

    [SerializeField]
    [Range(0f, 360f)]
    float deviationOfCenter;

    [SerializeField]
    int totalPositionsInOrbit;

    Vector3[] orbitalPositions;

    [ContextMenu("ReOrder Placement in Orbit")]
    void RecalculateOrbitalPositions()
    {
        orbitalPositions = new Vector3[totalPositionsInOrbit];
        int totalObjects = transform.childCount;
        float deviation = Mathf.PI * Mathf.Lerp(0f, 2f, orbitArc / 360f) / (totalObjects - 1);
        float currentAngle = (Mathf.PI / 180) * (deviationOfCenter + 90f/*front = z axis*/ - orbitArc / 2f);
        for (int i = 0; i < orbitalPositions.Length; i++)
        {
            orbitalPositions[i] = new Vector3(Mathf.Cos(currentAngle) * radius, transform.localPosition.y, Mathf.Sin(currentAngle) * radius);
            currentAngle += deviation;
        }
    }

    Vector3[] GetOrbitalPositions()
    {
        return orbitalPositions;
    }

    /// <summary>
    /// Re-Calculate orbit positions
    /// </summary>
    /// <param name="totalPositions">total number of positions in the orbit</param>
    /// <param name="orbitRadius">radius</param>
    /// <param name="orbitCenter">deviation from center if required. default = center facing local z axis</param>
    public Vector3[] RecalculateOrbitalPositions(int totalPositions, float orbitRadius = 360, float orbitCenter = 0)
    {
        totalPositionsInOrbit = totalPositions;
        radius = orbitRadius;
        deviationOfCenter = orbitCenter;
        RecalculateOrbitalPositions();
        return GetOrbitalPositions();
    }

}
