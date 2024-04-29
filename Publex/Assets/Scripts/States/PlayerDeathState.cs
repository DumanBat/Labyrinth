using Publex.Gameplay.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Publex.Gameplay.Behaviour
{
    public class PlayerDeathState : IState
    {
        private static readonly int DeadHash = Animator.StringToHash("Dead");
        public StateMachine.States State => StateMachine.States.Death;

        private Animator _animator;

        public PlayerDeathState(Animator animator)
        {
            _animator = animator;
        }

        public void OnEnter()
        {
            _animator.SetBool(DeadHash, true);
        }

        public void OnExit()
        {
            _animator.SetBool(DeadHash, false);
        }

        public void Tick() { }
        public void FixedTick() { }
    }
}
