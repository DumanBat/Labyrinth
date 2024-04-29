using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Publex.Gameplay
{
    public class CountdownTimer : MonoBehaviour
    {
        private float _remainingTime;
        private Coroutine _routineHolder;

        public Action<float> RemainingTimeChanged;

        public void StartTimer(float duration, Action<bool> onTimerEndCallback)
        {
            _remainingTime = duration;
            _routineHolder = StartCoroutine(CountdownCoroutine(onTimerEndCallback));
        }

        public void StopTimer()
        {
            StopCoroutine(_routineHolder);
            RemainingTimeChanged?.Invoke(_remainingTime);
        }

        public float GetRemainingTime() => _remainingTime;

        private IEnumerator CountdownCoroutine(Action<bool> onTimerEndCallback)
        {
            while (_remainingTime > 0)
            {
                RemainingTimeChanged?.Invoke(_remainingTime);
                yield return new WaitForSeconds(1f);
                _remainingTime -= 1f;
            }

            RemainingTimeChanged?.Invoke(0f);
            onTimerEndCallback?.Invoke(false);
        }
    }
}
