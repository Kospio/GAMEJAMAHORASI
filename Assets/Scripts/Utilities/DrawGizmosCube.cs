using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawGizmosCube : MonoBehaviour
{
    public enum colors
    {
     RED,CYAN,YELLOW,GREEN
    }
    public colors targetColor;

    void OnDrawGizmos()
    {
        switch (targetColor)
        {
            case colors.RED:
                {
                    Gizmos.color = Color.red;

                    break;
                }
            case colors.CYAN:
                {
                    Gizmos.color = Color.cyan;

                    break;
                }
            case colors.YELLOW:
                {
                    Gizmos.color = Color.yellow;

                    break;
                }
            case colors.GREEN:
                {
                    Gizmos.color = Color.green;

                    break;
                }
        }

        Gizmos.matrix = this.transform.localToWorldMatrix;
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);

    }
}
