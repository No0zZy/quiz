using System.Collections.Generic;
using HGtest.Questions;
using HGtest.Utils;

namespace HGtest.Level
{
    public class LevelGenerator
    {
        private readonly LevelConfig _levelConfig;
        private readonly LevelModel _levelModel;

        public LevelGenerator(LevelConfig levelConfig, LevelModel levelModel)
        {
            _levelConfig = levelConfig;
            _levelModel = levelModel;
        }

        public void GenerateLevelQuestions()
        {
            var availableQuestions = new List<IQuestion>();

            foreach (var bundle in _levelConfig.UsedBundles)
            {
                availableQuestions.AddRange(bundle.Questions);
            }

            var levelQuestions = availableQuestions.GetRandomElements(_levelConfig.QuestionsCount);

            _levelModel.SetQuestions(levelQuestions);
        }
    }
}