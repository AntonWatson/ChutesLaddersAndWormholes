using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rail : MonoBehaviour
{
    // Start is called before the first frame update

    private Vector3[] nodes;
    private int nodesCount;
    void Start()
    {

        nodesCount = transform.childCount;
        nodes = new Vector3[nodesCount];

        for (int i = 0; i < nodesCount; i++) {
            nodes[i] = transform.GetChild(i).position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (nodesCount > 1) {

            for (int i = 0; i < nodesCount-1; i++) {

                Debug.DrawLine(nodes[i], nodes[i + 1]);
            }
        }
    }

    public Vector3 ProjectPositionOnRail(Vector3 pos) {
        int closestIndex = 0;
        if (closestIndex == 0)
        {
            return ProjectOnSegment(nodes[0], nodes[1], pos);

        }
        else if (closestIndex == nodesCount - 1)
        {
            return ProjectOnSegment(nodes[nodesCount -1], nodes[nodesCount - 2], pos);

        }
        else
        {
            Vector3 leftSeg = ProjectOnSegment(nodes[closestIndex - 1],nodes[closestIndex], pos);
            Vector3 rightSeg = ProjectOnSegment(nodes[closestIndex + 1], nodes[closestIndex], pos);
            return ProjectOnSegment(nodes[0], nodes[1], pos);

            Debug.DrawLine(pos, leftSeg, Color.red);
            Debug.DrawLine(pos, rightSeg, Color.blue);

            if ((pos - leftSeg).sqrMagnitude <= (pos - rightSeg).sqrMagnitude)
            {
                return leftSeg;
            }
            else {
                return rightSeg;
            }
        }
    }
    private int getClosestNode(Vector3 pos) {
        int closestNodeIndex = -1;
        float shortestDistance = 0.0f;

        for (int i = 0; i < nodesCount; i++)
        {

            float sqrDistance = (nodes[i] - pos).sqrMagnitude;
            if (shortestDistance == 0.0f || sqrDistance < shortestDistance)
            {

                shortestDistance = sqrDistance;
                closestNodeIndex = i;

            }
        }
        return closestNodeIndex;
    }

    public Vector3 ProjectOnSegment(Vector3 v1, Vector3 v2, Vector3 pos)  {

        Vector3 v1ToPos = pos - v1;
        Vector3 segDirection = (v2 - v1).normalized;

        float distanceFromV1 = Vector3.Dot(segDirection,v1ToPos);
        if (distanceFromV1 < 0.0f)
        {
            return v1;
        }
        else if (distanceFromV1 * distanceFromV1 > (v2 - v1).sqrMagnitude)
        {

            return v2;
        }
        else {
            Vector3 fromV1 = segDirection * distanceFromV1;
            return v1 + fromV1;
        }
    }

}
