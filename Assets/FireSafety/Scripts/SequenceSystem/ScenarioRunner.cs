using System.Collections;
using UnityEngine;

namespace ScenarioSystem
{
    // --- THE RUNNER (Pure C# Class) ---
    public class ScenarioRunner
    {
        private Scenario _scenario;

        public ScenarioRunner(Scenario scenario)
        {
            _scenario = scenario;
        }

        public IEnumerator Run()
        {
            if (_scenario == null) yield break;

            foreach (Action action in _scenario.sequence)
            {
                Debug.Log($"Starting: {action.actionDescription}");

                while (!action.IsComplete(_scenario))
                {
                    // Check for 'Time-Out' or 'Fire Growth' failure here
                    if (_scenario.fireIntensity >= _scenario.criticalFailureLimit)
                    {
                        Debug.LogError("Scenario Failed: Fire spread too far!");
                        yield break; 
                    }

                    yield return new WaitForSeconds(0.1f);
                }
            }

            Debug.Log("All Composite Actions Cleared!");
            _scenario.Cleanup();
        }
    }
}
