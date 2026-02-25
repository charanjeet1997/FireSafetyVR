using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class FireExtinguisher : MonoBehaviour
{
    [Header("Fire Particles")]
    private ParticleSystem _fireParticles;

    [Header("Fire Settings")]
    [SerializeField] private float maxFireIntensity = 100f;
    [SerializeField] private float extinguishRate = 30f;
    [SerializeField] private float recoverRate = 8f;
    [SerializeField] private float destroyDelay = 2f;

    [Header("Visual Settings")] 
    [SerializeField] private float minFireSize = 0.2f;
    [SerializeField] private float maxFireSize = 2f;
    [SerializeField] private float maxFireSpeed = 10f;
    [SerializeField] private float minFireSpeed = 20f;
    
    private float _currentFireIntensity;
    private bool _isBeingSprayed = false;
    private bool _isExtinguished = false;

    private ParticleSystem.MainModule fireMain;
    
    private void Awake()
    {
        _fireParticles = GetComponent<ParticleSystem>();
    }

    private void Start()
    {
        _currentFireIntensity = maxFireIntensity;
        fireMain = _fireParticles.main;
    }

    private void Update()
    {
        if(_isExtinguished) return;

        HandleFire();
        UpdateFire();
        
        _isBeingSprayed = false;
    }

    void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Smoke")) 
        {
            _isBeingSprayed = true;
        }
    }

    void HandleFire()
    {
        if (_isBeingSprayed)
        {
            _currentFireIntensity-=extinguishRate*Time.deltaTime;
        }
        else
        {
            _currentFireIntensity += recoverRate*2*Time.deltaTime;
        }
        if (_currentFireIntensity <= 0f && !_isExtinguished)
        {
            _isExtinguished = true;
            StartCoroutine(ExtinguishFire());
        }
        
        _currentFireIntensity =
            Mathf.Clamp(_currentFireIntensity, 0f, maxFireIntensity);
    }


    void UpdateFire()
    {
        float normalized =
            Mathf.Pow(_currentFireIntensity / maxFireIntensity, 1.5f);
        
        fireMain.startSize=
            Mathf.Lerp(minFireSize, maxFireSize, normalized);

        fireMain.startSpeed =
            Mathf.Lerp(maxFireSpeed, minFireSpeed,normalized);

        if (_currentFireIntensity < 15f)
        {
            fireMain.startLifetime = 0.3f;
        }
        else
        {
            fireMain.startLifetime = 1f;
        }
        
    }
    
    private IEnumerator ExtinguishFire()
    {
        _fireParticles.Stop();
        
        yield return new WaitForSeconds(destroyDelay);
        
        Destroy(_fireParticles.gameObject);
    }
}
