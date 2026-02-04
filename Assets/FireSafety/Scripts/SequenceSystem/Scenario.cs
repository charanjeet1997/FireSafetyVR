using UnityEngine;
using System.Collections.Generic;

namespace ScenarioSystem
{
    // --- THE SCENARIO (MonoBehaviour on Prefab) ---
    public class Scenario : MonoBehaviour
    {
        public string scenarioName;
        public List<Action> sequence; // The main path of tasks

        [Header("Room References")]
        public GameObject fireObject;
        public GameObject extinguisher;
        
        // Used to track if the fire has grown too large
        public float fireIntensity = 0f; 
        public float criticalFailureLimit = 100f;

        public void Cleanup() => Destroy(gameObject);
    }
}
