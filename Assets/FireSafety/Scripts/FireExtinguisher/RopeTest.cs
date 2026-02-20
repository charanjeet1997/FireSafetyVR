using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

[RequireComponent(typeof(LineRenderer))]
public class RopeTest : MonoBehaviour
{
    [Header("Pipe Points")]
    public Transform startPoint;
    public Transform startAnchor;
    public Transform endPoint;
    public Transform endAnchor;

    [Header("Pipe Settings")]
    public int segmentLength = 40;
    public float gravity = 0f;
    public int constraintIterations = 40;

    [Range(0f, 1f)]
    public float damping = 0.93f;

    [Header("Pipe Visual")]
    public float pipeWidth = 0.2f;

    private LineRenderer lineRenderer;
    private List<RopeSegment> ropeSegments = new List<RopeSegment>();
    private float ropeSegLen;



    public struct RopeSegment
    {
        public Vector3 currentPos;
        public Vector3 oldPos;

        public RopeSegment(Vector3 pos)
        {
            currentPos = pos;
            oldPos = pos;
        }
    }

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.positionCount = segmentLength;
        lineRenderer.startWidth = pipeWidth;
        lineRenderer.endWidth = pipeWidth;
        lineRenderer.numCapVertices = 8;
        lineRenderer.numCornerVertices = 8;
        lineRenderer.textureMode = LineTextureMode.Tile;
        lineRenderer.useWorldSpace = true;
        lineRenderer.alignment = LineAlignment.View;

        InitializeRope();
    }

    void InitializeRope()
    {
        ropeSegments.Clear();

        if (startPoint == null || endPoint == null)
            return;

        float totalDistance = Vector3.Distance(startPoint.position, endPoint.position);
        ropeSegLen = totalDistance / (segmentLength - 1);

        for (int i = 0; i < segmentLength; i++)
        {
            float t = i / (float)(segmentLength - 1);
            Vector3 pos = Vector3.Lerp(startPoint.position, endPoint.position, t);
            ropeSegments.Add(new RopeSegment(pos));
        }
    }

    void FixedUpdate()
    {
        if (startPoint == null || endPoint == null)
            return;
        
        UpdateSegmentLength();
        Simulate();
        ApplyConstraints();
    }

    void Update()
    {
        DrawRope();
    }

    void UpdateSegmentLength()
    {
        float totalDistance = Vector3.Distance(startPoint.position, endPoint.position);
        ropeSegLen = totalDistance / (segmentLength - 1);
    }

    void Simulate()
    {
        for (int i = 1; i < ropeSegments.Count - 1; i++)
        {
            RopeSegment segment = ropeSegments[i];

            Vector3 velocity = (segment.currentPos - segment.oldPos) * damping;

            segment.oldPos = segment.currentPos;
            segment.currentPos += velocity;
            segment.currentPos += Vector3.up * (gravity * Time.fixedDeltaTime);

            ropeSegments[i] = segment;
        }
    }

    void ApplyConstraints()
    {
        for (int i = 0; i < constraintIterations; i++)
        {
            // Lock main endpoints
            ropeSegments[0] = new RopeSegment(startPoint.position);
            ropeSegments[ropeSegments.Count - 1] = new RopeSegment(endPoint.position);

            // 🔥 Lock anchor segments INSIDE solver
            if (startAnchor != null)
                ropeSegments[1] = new RopeSegment(startAnchor.position);

            if (endAnchor != null)
                ropeSegments[ropeSegments.Count - 2] = new RopeSegment(endAnchor.position);

            for (int j = 0; j < ropeSegments.Count - 1; j++)
            {
                RopeSegment a = ropeSegments[j];
                RopeSegment b = ropeSegments[j + 1];

                float dist = Vector3.Distance(a.currentPos, b.currentPos);
                float error = dist - ropeSegLen;
                Vector3 changeDir = (a.currentPos - b.currentPos).normalized;
                Vector3 change = changeDir * error * 0.5f;

                if (j != 0 && j != 1) // don't move start or startAnchor
                {
                    a.currentPos -= change;
                    ropeSegments[j] = a;
                }

                if (j + 1 != ropeSegments.Count - 1 &&
                    j + 1 != ropeSegments.Count - 2) // don't move end or endAnchor
                {
                    b.currentPos += change;
                    ropeSegments[j + 1] = b;
                }
            }
        }
    }

    void DrawRope()
    {
        for (int i = 0; i < ropeSegments.Count; i++)
        {
            lineRenderer.SetPosition(i, ropeSegments[i].currentPos);
        }
    }
}
