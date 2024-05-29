using System;
using UnityEngine;

namespace HGtest.Utils
{
    //For unity json 
    [Serializable]
    public class MyScoreClass
    {
        public int Score
        {
            get => _score;
            set => _score = value;
        }

        [SerializeField] private int _score;
    }
}