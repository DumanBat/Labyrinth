using Publex.Gameplay.Behaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Publex.Gameplay
{
    [RequireComponent(typeof(Collider))]
    public class LevelFinal : MonoBehaviour
    {
        private Collider _collider;
        private Level _origin;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
        }

        public void Init(Level origin)
        {
            _origin = origin;
        }

        private void OnTriggerEnter(Collider other)
        {
            var player = other.GetComponent<ITarget>();
            if (player != null)
            {
                _origin.LevelFinished?.Invoke(true);
            }
        }
    }
}
