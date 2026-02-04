using UnityEngine;
using System.Collections.Generic;

namespace ScenarioSystem
{
    // --- COMPOSITE ACTION (A group of tasks) ---
    [CreateAssetMenu(fileName = "CompositeAction", menuName = "ScenarioSystem/Actions/Composite")]
    public class CompositeAction : Action
    {
        public List<Action> subActions;

        public override bool IsComplete(Scenario context)
        {
            // This checks if all nested actions (Simple or Composite) are done
            foreach (var subAction in subActions)
            {
                if (!subAction.IsComplete(context)) return false;
            }
            return true;
        }
    }
}
