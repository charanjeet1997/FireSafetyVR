using UnityEngine;
using System.Collections.Generic;

namespace ScenarioSystem
{
    // --- THE DATABASE ---
    [CreateAssetMenu(fileName = "ScenarioDatabase", menuName = "ScenarioSystem/Database")]
    public class ScenarioDatabase : ScriptableObject
    {
        public List<GameObject> scenarios;
    }
}
