using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Publex.Gameplay.Behaviour
{
    [RequireComponent(typeof(Rigidbody))]
    public class BasicMovement : MonoBehaviour, IMoveable
    {
        private Rigidbody _rb;

        private void Awake() => _rb = GetComponent<Rigidbody>();

        public void Move(Vector3 direction, float moveSpeed)
        {
            _rb.MovePosition(_rb.position + direction * Time.fixedDeltaTime * moveSpeed);
        }

        public void Stop()
        {
            _rb.angularVelocity = Vector3.zero;
        }
    }
}
