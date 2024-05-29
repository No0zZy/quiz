using UnityEngine;

namespace HGtest.Questions
{
    [CreateAssetMenu(fileName = nameof(QuestionsBundleConfig), menuName = "HGtest/" + nameof(QuestionsBundleConfig))]
    public class QuestionsBundleConfig : ScriptableObject
    {
        public IQuestion[] Questions => _questions;

        [SerializeReference] private IQuestion[] _questions;
    }
}