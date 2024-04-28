using Publex.Gameplay.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Publex.Gameplay.Behaviour
{
    public class EnemyIdleState : IState
    {
        private static readonly int IdleHash = Animator.StringToHash("isIdle");
        public StateMachine.States State => StateMachine.States.Idle;

        private Animator _animator;

        public EnemyIdleState(Animator animator)
        {
            _animator = animator;
        }

        public void OnEnter()
        {
            _animator.SetBool(IdleHash, true);
        }

        public void OnExit()
        {
            _animator.SetBool(IdleHash, false);
        }

        public void FixedTick() { }
        public void Tick() { }
    }
}
