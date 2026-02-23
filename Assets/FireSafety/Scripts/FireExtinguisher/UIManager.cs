using Unity.XR.CoreUtils;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject extinguisher;

    public void SpawnFireExtinguisher()
    {
        extinguisher.SetActive(true);
    }
}
