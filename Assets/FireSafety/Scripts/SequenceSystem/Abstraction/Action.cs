using UnityEngine;

namespace ScenarioSystem
{
    // --- THE BASE ACTION ---
    public abstract class Action : ScriptableObject
    {
        public string actionDescription;
        public abstract bool IsComplete(Scenario context);
    }
}
