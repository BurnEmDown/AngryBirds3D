﻿using UnityEngine;

/// <summary>
/// This script is copied from Tarodev's video for creating a trajectory line:
/// https://www.youtube.com/watch?v=p8e4Kpl9b28
/// I will probably make some changes to it later on, if needed
/// </summary>

public class Cannon : MonoBehaviour {
    
    [SerializeField] private Projection _projection;

    private void Update() {
        HandleControls();
        _projection.SimulateTrajectory(_ballPrefab, _ballSpawn.position, _ballSpawn.forward * _force);
    }

    #region Handle Controls

    [SerializeField] private Ball _ballPrefab;
    [SerializeField] private float _force = 20;
    [SerializeField] private Transform _ballSpawn;
    [SerializeField] private Transform _barrelPivot;
    [SerializeField] private float _rotateSpeed = 30;
    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioClip _clip;
    [SerializeField] private Transform _leftWheel, _rightWheel;
    [SerializeField] private ParticleSystem _launchParticles;

    [SerializeField] private float minVerticalRotation = 20f;
    [SerializeField] private float maxVerticalRotation = 70f;
    
    [SerializeField] private float minHorizontalRotation = -50f;
    [SerializeField] private float maxHorizontalRotation = 50f;

    /// <summary>
    /// This is absolute spaghetti and should not be look upon for inspiration. I quickly smashed this together
    /// for the tutorial and didn't look back
    /// </summary>
    private void HandleControls()
    {

        float xRotationBarrel = _barrelPivot.rotation.eulerAngles.x;
        xRotationBarrel = ConvertToAngle180(xRotationBarrel);
        
        if (Input.GetKey(KeyCode.S) && xRotationBarrel < maxVerticalRotation)
            _barrelPivot.Rotate(Vector3.right * (_rotateSpeed * Time.deltaTime));
        else if (Input.GetKey(KeyCode.W) && xRotationBarrel > minVerticalRotation)
            _barrelPivot.Rotate(Vector3.left * (_rotateSpeed * Time.deltaTime));
        
        float yRotation = transform.rotation.eulerAngles.y;
        yRotation = ConvertToAngle180(yRotation);
        
        if (Input.GetKey(KeyCode.A) && yRotation > minHorizontalRotation) {
            transform.Rotate(Vector3.down * (_rotateSpeed * Time.deltaTime));
            _leftWheel.Rotate(Vector3.forward * (_rotateSpeed * Time.deltaTime));
            _rightWheel.Rotate(Vector3.back * (_rotateSpeed * Time.deltaTime));
        }
        else if (Input.GetKey(KeyCode.D) && yRotation < maxHorizontalRotation) {
            transform.Rotate(Vector3.up * (_rotateSpeed * Time.deltaTime));
            _leftWheel.Rotate(Vector3.back * (_rotateSpeed * 1.5f * Time.deltaTime));
            _rightWheel.Rotate(Vector3.forward * (_rotateSpeed * 1.5f * Time.deltaTime));
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            var spawned = Instantiate(_ballPrefab, _ballSpawn.position, _ballSpawn.rotation);

            spawned.Init(_ballSpawn.forward * _force, false);
            _launchParticles.Play();
            _source.PlayOneShot(_clip);
        }
    }

    private float ConvertToAngle180(float angle)
    {
        float MaxDegrees = 360;
        if (angle > MaxDegrees)
            angle -= MaxDegrees;
        else if (angle < -MaxDegrees)
            angle += MaxDegrees;
        else if (angle > MaxDegrees / 2)
            angle -= MaxDegrees;
        else if (angle < -MaxDegrees / 2)
            angle += MaxDegrees;

        return angle;
    }

    #endregion
}