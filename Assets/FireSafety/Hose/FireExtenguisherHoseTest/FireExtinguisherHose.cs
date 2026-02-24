using UnityEngine;
using UnityEngine.Splines;
using Unity.Mathematics;

[RequireComponent(typeof(SplineContainer))]
public class FireExtinguisherHose : MonoBehaviour
{
    [Header("Main Points")]
    public Transform extinguisherOutlet;
    public Transform nozzleTip;

    [Header("Bezier Tangent Handles")]
    public Transform startTangentTransform;
    public Transform endTangentTransform;

    private SplineContainer splineContainer;
    private Spline spline;

    void Awake()
    {
        InitSpline();
    }

    void LateUpdate()
    {
        if (!extinguisherOutlet || !nozzleTip ||
            !startTangentTransform || !endTangentTransform)
            return;

        UpdateSpline();
    }

    [ContextMenu("Update Spline")]

    void InitSpline()
    {
        splineContainer = GetComponent<SplineContainer>();

        spline = new Spline();
        splineContainer.Spline = spline;

        spline.Add(new BezierKnot());
        spline.Add(new BezierKnot());
        UpdateSpline();
    }
    void UpdateSpline()
    {
        if (splineContainer == null) splineContainer = GetComponent<SplineContainer>();
        Transform root = splineContainer.transform;

        // WORLD positions
        Vector3 worldStart = extinguisherOutlet.position;
        Vector3 worldEnd = nozzleTip.position;

        Vector3 worldStartHandle = startTangentTransform.position;
        Vector3 worldEndHandle = endTangentTransform.position;

        // Convert to LOCAL space
        float3 localStart = root.InverseTransformPoint(worldStart);
        float3 localEnd = root.InverseTransformPoint(worldEnd);

        float3 localStartHandle =
            root.InverseTransformPoint(worldStartHandle);

        float3 localEndHandle =
            root.InverseTransformPoint(worldEndHandle);

        // Tangent = HandlePosition - KnotPosition
        float3 startOutTangent = localStartHandle - localStart;
        float3 endInTangent = localEndHandle - localEnd;

        BezierKnot startKnot = new BezierKnot(
            localStart,
            float3.zero,
            startOutTangent
        );

        BezierKnot endKnot = new BezierKnot(
            localEnd,
            endInTangent,
            float3.zero
        );

        spline.SetKnot(0, startKnot);
        spline.SetKnot(1, endKnot);

        spline.SetTangentMode(0, TangentMode.Broken);
        spline.SetTangentMode(1, TangentMode.Broken);
    }
}