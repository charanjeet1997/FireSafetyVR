using Unity.XR.CoreUtils;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public Transform extinguisherTransform;


    private GameObject extinguisher;

    public void SpawnFireExtinguisher(GameObject extinguisher)
    {
        if (extinguisher != null)
        {
            Destroy(extinguisher);
        }
        extinguisher=Instantiate(extinguisher,
                    extinguisher.transform.position,
                    Quaternion.identity);

        GameObject pin = extinguisher.GetNamedChild("Lacre");
        pin.transform.SetParent(null);
    }
}
