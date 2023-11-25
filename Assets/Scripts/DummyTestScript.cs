using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DummyTestScript : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    private const int _maxHealth = 750;
    private int _health = _maxHealth;

    private float _healthResetTimer = 3f;
    private Coroutine _coroutine;

    private void Start()
    {
        _slider.maxValue = _maxHealth;
        _slider.value = _health;
    }

    public void TakeDamage(int damage)
    {
        Debug.Log(damage + " damaged recieved!");

        _health -= damage;
        _slider.value = _health;

        if (_coroutine != null) StopCoroutine(_coroutine);
        _coroutine = StartCoroutine(ResetHealth());
    }

    private IEnumerator ResetHealth()
    {
        yield return new WaitForSeconds(_healthResetTimer);

        _health = _maxHealth;
        _slider.value = _health;
    }
}
