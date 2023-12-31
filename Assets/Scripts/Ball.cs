﻿using UnityEngine;

/// <summary>
/// This script is copied from Tarodev's video for creating a trajectory line:
/// https://www.youtube.com/watch?v=p8e4Kpl9b28
/// I will probably make some changes to it later on, if needed
/// </summary>

public class Ball : MonoBehaviour {
    
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioClip[] _clips;
    [SerializeField] private GameObject _poofPrefab;
    private bool _isGhost;

    public void Init(Vector3 velocity, bool isGhost) {
        _isGhost = isGhost;
        _rb.AddForce(velocity, ForceMode.Impulse);
    }

    public void OnCollisionEnter(Collision col) {
        if (_isGhost) return;
        Instantiate(_poofPrefab, col.contacts[0].point, Quaternion.Euler(col.contacts[0].normal));

        // miss
        if (col.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Debug.Log("miss");
            GameManager.Instance.OnShotMiss();
            Destroy(gameObject);
        }
        // hit enemy
        else if (col.gameObject.GetComponent<Target>())
        {
            col.gameObject.GetComponent<Target>().OnCannonHit();
            Destroy(gameObject);
        }
        // hit obstacle
        else
        {
            _source.clip = _clips[Random.Range(0, _clips.Length)];
            _source.Play();
        }
        
        
    }
}