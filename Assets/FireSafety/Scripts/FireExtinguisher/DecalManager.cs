using UnityEngine;

public class DecalManager : MonoBehaviour
{
    public GameObject residuePrefab;
    public float spawnChance = 0.25f;
    public float minSize = 0.15f;
    public float maxSize = 0.3f;
    public float destroyTime = 3f;
    
    ParticleSystem ps;
    
    
    
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }
    
    void OnParticleCollision(GameObject other)
    {
        ParticleCollisionEvent[] collisionEvents = new ParticleCollisionEvent[16];
        int numEvents = ps.GetCollisionEvents(other, collisionEvents);

        for (int i = 0; i < numEvents; i++)
        {
            if (Random.value > spawnChance) continue;

            Vector3 pos = collisionEvents[i].intersection;
            Vector3 normal = collisionEvents[i].normal;

            GameObject decal = Instantiate(
                residuePrefab,
                pos,
                Quaternion.LookRotation(normal)
            );

            float size = Random.Range(minSize, maxSize);
            decal.transform.localScale = Vector3.one * size;

            Destroy(decal, 5f); // cleanup
        }
    }
}
