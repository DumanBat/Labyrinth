using Publex.Gameplay.Units;
using Publex.Gameplay.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Publex.Gameplay.Behaviour
{
    public class EnemyAttackState : IState
    {
        private static readonly int IsAttacking = Animator.StringToHash("isAttacking");
        private static readonly int AttackHash = Animator.StringToHash("Attack");
        private static readonly int AttackStateHash = Animator.StringToHash("Base Layer.Attack");
        private static readonly int IsIdleHash = Animator.StringToHash("isIdle");

        public StateMachine.States State => StateMachine.States.Attack;

        private EnemyUnit _unit;
        private Animator _animator;
        private IAttack _attack;
        private EnemyDetector _enemyDetector;

        private float _cooldown;
        private float _attackReach;
        private float _damage;

        private ITarget _target;
        private float _lastAttackedAt;
        private Coroutine _routineHolder;

        public EnemyAttackState(EnemyUnit unit, IAttack attack, EnemyDetector enemyDetector)
        {
            _unit = unit;
            _attack = attack;
            _animator = unit.Animator;
            _enemyDetector = enemyDetector;

            _cooldown = unit.Config.AttackCooldown;
            _attackReach = unit.Config.AttackReach;
            _damage = unit.Config.Damage;
        }

        public void Tick()
        {
            if (!_target.Health.IsAlive())
            {
                ResetAttack();
                _unit.CurrentAttackTarget = null;
                return;
            }

            var targetPosition = _target.Position.GetPosition();
            var targetDistance = Vector3.Distance(_unit.transform.position, targetPosition);

            if (targetDistance < _attackReach)
            {
                if (Time.time < _lastAttackedAt + _cooldown)
                    return;

                StartAttack();
            }
            else
            {
                _unit.HasTargetInRange = false;
            }
        }

        private void StartAttack()
        {
            ResetAttack();
            _routineHolder = _unit.StartCoroutine(AttackRoutine());
            _lastAttackedAt = Time.time;
        }

        private void ResetAttack()
        {
            if (_routineHolder == null)
                return;

            _unit.StopCoroutine(_routineHolder);
            _animator.ResetTrigger(AttackHash);
            SetAttackAnimation(false);
        }

        private IEnumerator AttackRoutine()
        {
            SetAttackAnimation(true);
            _animator.SetTrigger(AttackHash);

            var timePassed = 0.0f;
            while (_animator.GetCurrentAnimatorStateInfo(0).fullPathHash != AttackStateHash)
            {
                timePassed += Time.deltaTime;
                yield return null;
            }
            var attackLength = Mathf.Abs(_animator.GetCurrentAnimatorStateInfo(0).length - timePassed);
            var waitForSeconds = new WaitForSeconds(attackLength / 2);

            yield return waitForSeconds;

            if (_target != null)
                _attack.Attack(_target, _damage);

            yield return waitForSeconds;

            SetAttackAnimation(false);
        }

        private void SetAttackAnimation(bool isAttacking)
        {
            _animator.SetBool(IsAttacking, isAttacking);
            _animator.SetBool(IsIdleHash, !isAttacking);
        }

        public void OnEnter()
        {
            _target = _unit.CurrentAttackTarget;
            _unit.transform.LookAt(_target.Position.GetPosition());
            _animator.SetBool(IsAttacking, true);
        }

        public void OnExit()
        {
            if (_routineHolder != null)
                _unit.StopCoroutine(_routineHolder);

            _unit.HasTargetInRange = false;
            _target = null;
            _animator.SetBool(IsAttacking, false);
            _animator.SetBool(IsIdleHash, false);
        }

        public void FixedTick() { }
    }
}
