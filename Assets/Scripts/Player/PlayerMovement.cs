using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
  class PlayerMovement : MonoBehaviour
  {
    private Animator _animator;
    private Rigidbody _rb;
    private PlayerCamera _camera;

    [SerializeField]
    private float _runSpeed = 50.0f;
    [SerializeField]
    private float _walkSpeed = 15.0f;
    [SerializeField]
    private float _currentSpeed = 50f;
    [SerializeField]
    private float _rotationSpeed = 10.0f;
    [SerializeField]
    private float _moveDrag = 0.1f;

    private void Awake() 
    {
      _animator = GetComponentInChildren<Animator>();
      _rb = GetComponent<Rigidbody>();
      _camera = GetComponent<PlayerCamera>();

      // TODO: err handle
    }

    private void FixedUpdate() 
    {
      Move();
    }

    void Update()
    {
      // RESET POSITION AND VELOCITY FOR DEBUGGING
      if (Input.GetKeyDown(KeyCode.R)) DebugResetRB();
      if (Input.GetKeyDown(KeyCode.X)) ToggleWalk();
    }

    private void ToggleWalk()
    {
      _currentSpeed = _currentSpeed == _runSpeed ? _walkSpeed : _runSpeed;
    }

    public void DebugResetRB()
    {
      _rb.velocity = Vector3.zero;
      _rb.MovePosition(Vector3.zero);
    }

    public void Move()
    {
      // get x and z values from inputs
      float horizontalMove = Input.GetAxis("Horizontal");
      float verticalMove = Input.GetAxis("Vertical");
      Vector3 movement = new Vector3(horizontalMove, 0f, verticalMove).normalized;

      /*
      Vector3 desiredForce = (movement * _force) - _rb.velocity;
      _rb.AddForce(desiredForce, ForceMode.Force);
      */

      // apply drag
      Vector3 currentVelocity = _rb.velocity;
      currentVelocity.x /= (_moveDrag + 1f);
      currentVelocity.z /= (_moveDrag + 1f);
      _rb.velocity = currentVelocity;

      /*
      // ISSUE: fucking jitter. This rapidly switches between not applying force and applying force
      //Vector3 xzVelocity = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);
      if (xzVelocity.magnitude < _maxSpeed)
      {
        _rb.AddForce((movement * _force) - _rb.velocity, ForceMode.Force);
      }
      */

      // apply movement direction based on the angle of the camera
      movement = _camera.CameraDirection() * movement;

      // add force
      _rb.AddForce(movement * _currentSpeed, ForceMode.Force);

      // limit max speed
      // this is going to have issues... we need to clamp ONLY the x and z. How would we jump?
      // it also auto limits ALL external forces... RIP DODGE
      // forcing the velocity to a specific Vector also means that we cannot transition from run to walk smoothly
      /*
      if (_rb.velocity.magnitude > _maxSpeed)
      {
        _rb.velocity = Vector3.ClampMagnitude(_rb.velocity, _maxSpeed);
      }
      */

      // get zx velocity after adding force and resistance
      Vector3 xzVelocity = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);

      _animator?.SetFloat("speed", Mathf.Clamp(xzVelocity.magnitude / 8f, 0f, 1f));

      // match rotation with current velocity as long as player is doing some movement input
      // TODO: investigate more precise solution
      if (movement.magnitude > 0 && xzVelocity.magnitude > 0.1f) 
      {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(xzVelocity), Time.deltaTime * _rotationSpeed);
      }

      /* PHYSICS ATTEMPT 1
      =================================================================
      //Vector3 intendedVelocity = movement * _maxSpeed;
      if (_rb.velocity.magnitude < _maxSpeed)
      {
        // ignore y 
        _rb.AddForce(movement * _force, ForceMode.Force);
      }
      //Vector3 testForce = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z) - intendedVelocity;
      Vector3 testForce = (intendedVelocity - new Vector3(_rb.velocity.x, 0f, _rb.velocity.z));
      if (testForce.magnitude > _maxDampenForce)
      {
        testForce = testForce.normalized * _maxDampenForce;
      }
      _rb.AddForce(testForce, ForceMode.Force);
      _animator?.SetFloat("speed", Mathf.Clamp(_rb.velocity.magnitude / _maxSpeed, 0f, 1f));

      transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(_rb.velocity.x, 0, _rb.velocity.z)), Time.deltaTime * _rotationSpeed);
      */

      /* PHYSICS ATTEMPT 2 - instant velocity half when input stops
      =================================================================
      if (horizontalMove == 0 && _rb.velocity.x > 0) 
      {
        //Vector3 testForce = (intendedVelocity - new Vector3(_rb.velocity.x, 0f, _rb.velocity.z));
        Vector3 testForce = new Vector3(_rb.velocity.x * 0.5f, _rb.velocity.y, _rb.velocity.z);
        _rb.AddForce(testForce, ForceMode.VelocityChange);
      }
      if (verticalMove == 0 && _rb.velocity.z > 0) 
      {
        Vector3 testForce = new Vector3(_rb.velocity.x, _rb.velocity.y, _rb.velocity.z * 0.5f);
        _rb.AddForce(testForce, ForceMode.VelocityChange);
      }
      */
    }
  }
}