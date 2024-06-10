using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Renderer))]

public class Cube : MonoBehaviour
{
    [SerializeField] private Color _default—olor;
    [SerializeField] private Color _activeColor;

    private Renderer _renderer;
    private int _minLifetime = 2;
    private int _maxLifetime = 5;

    public event Action<Cube> Destroyed;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void OnEnable()
    {
        _renderer.material.color = _default—olor;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_renderer.material.color == _default—olor && collision.gameObject.TryGetComponent<Platform>(out Platform component))
        {
            _renderer.material.color = _activeColor;
            StartCoroutine(Deactivate());
        }
    }

    private IEnumerator Deactivate()
    {
        yield return new WaitForSeconds(Random.Range(_minLifetime, _maxLifetime));
        Destroyed?.Invoke(this);
    }
}
