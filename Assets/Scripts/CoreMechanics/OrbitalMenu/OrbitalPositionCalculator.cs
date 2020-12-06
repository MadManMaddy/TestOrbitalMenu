using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class OrbitalPositionCalculator
{
    static bool rotating;
    static float radius = 5f;
    static float orbitArc = 90f;
    static float deviationOfCenter;
    static int totalPositionsInOrbit;

    static Vector3[] orbitalPositions;

    [ContextMenu("ReOrder Placement in Orbit")]
    static void RecalculateOrbitalPositions()
    {
        orbitalPositions = new Vector3[totalPositionsInOrbit];
        int totalObjects = totalPositionsInOrbit;
        float deviation = Mathf.PI * Mathf.Lerp(0f, 2f, orbitArc / 360f) / (totalObjects );
        float currentAngle = (Mathf.PI / 180) * (deviationOfCenter - 90f/*front = z axis*/ - orbitArc / 2f);
        for (int i = 0; i < orbitalPositions.Length; i++)
        {
            orbitalPositions[i] = new Vector3((Mathf.Cos(currentAngle) * radius), 0, Mathf.Sin(currentAngle) * radius);
            currentAngle += deviation;
        }
    }

    static Vector3[] GetOrbitalPositions()
    {
        return orbitalPositions;
    }

    /// <summary>
    /// Re-Calculate orbit positions
    /// </summary>
    /// <param name="totalPositions">total number of positions in the orbit</param>
    /// <param name="orbitRadius">radius</param>
    /// <param name="orbitCenter">deviation from center if required. default = center facing local z axis</param>
    static public Vector3[] RecalculateOrbitalPositions(int totalPositions, float orbitRadius, float orbitarc = 360, float orbitCenter = 0)
    {
        totalPositionsInOrbit = totalPositions;
        radius = orbitRadius;
        orbitArc = orbitarc;
        deviationOfCenter = orbitCenter;
        RecalculateOrbitalPositions();
        return GetOrbitalPositions();
    }
}
