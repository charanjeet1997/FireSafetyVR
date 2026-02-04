using UnityEngine;

namespace ScenarioSystem
{
    // --- THE MANAGER (The Host) ---
    public class ScenarioManager : MonoBehaviour
    {
        public ScenarioDatabase database;
        private Coroutine _activeRoutine;

        public void StartScenario(int index)
        {
            if (_activeRoutine != null) StopCoroutine(_activeRoutine);

            GameObject instance = Instantiate(database.scenarios[index]);
            Scenario scenarioScript = instance.GetComponent<Scenario>();

            ScenarioRunner runner = new ScenarioRunner(scenarioScript);
            _activeRoutine = StartCoroutine(runner.Run());
        }
    }
}
