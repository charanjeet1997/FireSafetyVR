using UnityEngine;

namespace ScenarioSystem
{
    // --- THE CONDITION ---
    public abstract class Condition : ScriptableObject
    {
        public abstract bool IsSatisfied(Scenario context);
    }
}
