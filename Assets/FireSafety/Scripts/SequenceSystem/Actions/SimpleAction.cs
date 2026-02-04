using UnityEngine;
using System.Collections.Generic;

namespace ScenarioSystem
{
    // --- SIMPLE ACTION (A single task with rules) ---
    [CreateAssetMenu(fileName = "SimpleAction", menuName = "ScenarioSystem/Actions/Simple")]
    public class SimpleAction : Action
    {
        public List<Condition> conditions;

        public override bool IsComplete(Scenario context)
        {
            // Returns true only if every single rule is met
            foreach (var condition in conditions)
            {
                if (!condition.IsSatisfied(context)) return false;
            }
            return true;
        }
    }
}
