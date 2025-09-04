using System;
using UnityEngine;

namespace _GameFolders.Scripts.Functionaries
{
    public class RegenTimer
    {
        private int _current;
        private readonly int _max;
        private readonly float _cooldown; 
        private DateTime _nextRegenTime;

        public int Current => _current;
        public int Max => _max;
        public float Cooldown => _cooldown;
        public DateTime NextRegenTime => _nextRegenTime;

        public RegenTimer(int startValue, int max, float cooldown)
        {
            _current = startValue;
            _max = max;
            _cooldown = cooldown;
            _nextRegenTime = DateTime.Now.AddSeconds(cooldown);
        }

        public void Load(int savedValue, string lastExitString)
        {
            _current = Mathf.Min(savedValue, _max);

            if (!string.IsNullOrEmpty(lastExitString))
            {
                var lastExit = DateTime.Parse(lastExitString);
                var passed = DateTime.Now - lastExit;

                var regenCount = (int)(passed.TotalSeconds / _cooldown);
                _current = Mathf.Min(_current + regenCount, _max);

                if (_current < _max)
                {
                    var leftover = passed.TotalSeconds % _cooldown;
                    _nextRegenTime = DateTime.Now.AddSeconds(_cooldown - leftover);
                }
                else
                {
                    _nextRegenTime = DateTime.Now;
                }
            }
            else
            {
                _nextRegenTime = DateTime.Now.AddSeconds(_cooldown);
            }
        }

        public bool Tick()
        {
            if (_current >= _max) return false;

            if (DateTime.Now >= _nextRegenTime)
            {
                _current++;
                if (_current < _max)
                    _nextRegenTime = DateTime.Now.AddSeconds(_cooldown);

                return true;
            }

            return false;
        }

        public TimeSpan GetRemainingTime()
        {
            return _current >= _max ? TimeSpan.Zero : _nextRegenTime - DateTime.Now;
        }

        public void UseOne()
        {
            if (_current > 0)
            {
                _current--;

                if (_current == _max - 1 && _nextRegenTime <= DateTime.Now)
                {
                    _nextRegenTime = DateTime.Now.AddSeconds(_cooldown);
                }
            }
        }
    }
}