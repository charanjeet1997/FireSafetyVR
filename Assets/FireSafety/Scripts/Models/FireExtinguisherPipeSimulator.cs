using UnityEngine;
using System.Collections.Generic;

namespace VRTraining.FireSafetyTraining
{
    public class FireExtinguisherPipeSimulator : MonoBehaviour
    {
        [Header("Pipe Anchors")]
        public Transform hoseStart;   // Fixed holder
        public Transform hoseEnd;     // Grabbed nozzle (VR hand)

        [Header("Bone Setup")]
        public Transform hoseRootBone; // First hose bone

        [Header("Bezier Settings")]
        [Range(0f, 1f)]
        public float controlOffset = 0.3f;

        [Range(1f, 20f)]
        public float bendSmoothness = 10f;

        private readonly List<Transform> hoseBones = new();
        private Vector3[] velocities;

        void Start()
        {
            if (!hoseStart || !hoseEnd || !hoseRootBone)
            {
                Debug.LogError("Assign Hose Start, Hose End, and Root Bone.");
                enabled = false;
                return;
            }

            CollectBones(hoseRootBone);
            velocities = new Vector3[hoseBones.Count];
        }

        void LateUpdate()
        {
            SimulateBezierPipe();
        }

        void CollectBones(Transform bone)
        {
            hoseBones.Add(bone);
            if (bone.childCount > 0)
                CollectBones(bone.GetChild(0));
        }

        void SimulateBezierPipe()
        {
            Vector3 p0 = hoseStart.position;
            Vector3 p3 = hoseEnd.position;

            Vector3 dir = (p3 - p0).normalized;
            float distance = Vector3.Distance(p0, p3);

            // Auto control points (gives natural bend)
            Vector3 p1 = p0 + Vector3.down * distance * controlOffset;
            Vector3 p2 = p3 + Vector3.down * distance * controlOffset;

            for (int i = 0; i < hoseBones.Count; i++)
            {
                float t = (float)i / (hoseBones.Count - 1);
                Vector3 bezierPos = EvaluateBezier(p0, p1, p2, p3, t);

                hoseBones[i].position = Vector3.SmoothDamp(
                    hoseBones[i].position,
                    bezierPos,
                    ref velocities[i],
                    1f / bendSmoothness
                );

                if (i > 0)
                {
                    Vector3 forward = hoseBones[i].position - hoseBones[i - 1].position;
                    if (forward.sqrMagnitude > 0.0001f)
                        hoseBones[i].rotation = Quaternion.LookRotation(
                            forward.normalized,
                            Vector3.up
                        );
                }
            }
        }

        Vector3 EvaluateBezier(
            Vector3 p0,
            Vector3 p1,
            Vector3 p2,
            Vector3 p3,
            float t)
        {
            float u = 1 - t;
            float tt = t * t;
            float uu = u * u;
            float uuu = uu * u;
            float ttt = tt * t;

            return
                uuu * p0 +
                3f * uu * t * p1 +
                3f * u * tt * p2 +
                ttt * p3;
        }
    }
}
