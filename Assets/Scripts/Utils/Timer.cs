using UnityEngine;
using System;

[System.Serializable]
public class Timer {
    float currentTime;
    float maxTime;
    public class TimeBlock {
        float _ms;

        public float Miliseconds { get { return _ms; } }
        public float Seconds { get { return _ms / 1000.0f; } }
        public float Minutes { get { return _ms / 1000.0f / 60.0f; } }
        public float Hours { get { return _ms / 1000.0f / 60.0f / 60.0f; } }

        public TimeBlock(float ms) {
            _ms = ms;
        }
    }

    public bool Repeat { get; set; } = false;

    // Returns percent complete from 0 to 1
    public float AmountComplete { get { return maxTime != 0 ? (1.0f - currentTime / maxTime) : 1; } }
    public bool IsDone { get; private set; } = true;
    public bool InProgress { get {return !IsDone;} }
    public TimeBlock TimeRemaining {
        get {
            return new TimeBlock(currentTime);
        }
    }
    public delegate void OnFinishMethod();
    OnFinishMethod _onFinishMethod;

    public Timer() {
    }
    // Initializes timer to amount of seconds
    public void Set(float maxTime) {
        currentTime = maxTime;
        this.maxTime = maxTime;
        IsDone = false;
    }

    // Sets timer back to previously set time
    public void Reset() {
        currentTime = maxTime;
        IsDone = false;
    }

    // Decrements timer *Should be calles 
    public void Tick() {
        if (!IsDone) {
            if (currentTime > 0) {
                currentTime = Mathf.Max(currentTime - Time.deltaTime, 0);
            } else {
                if (_onFinishMethod != null) {
                    _onFinishMethod();
                }
                IsDone = true;

                if (Repeat) {
                    Reset();
                }
            }
        }
    }


    public void OnFinish(OnFinishMethod func) {
        _onFinishMethod = func;
    }

    public override string ToString() {
        return currentTime + " ms.";
    }



}
