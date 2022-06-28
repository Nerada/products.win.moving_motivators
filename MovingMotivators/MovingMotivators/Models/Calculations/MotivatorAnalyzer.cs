using System;
using System.Collections.Generic;
using System.Linq;

namespace MovingMotivators.Models.Calculations
{
    public class MotivatorAnalyzer
    {
        private List<AnalysisResult> _analysisResults = new();

        public MotivatorAnalyzer()
        {
            var users = new ValueGenerator().GetData().ToList();

            _analysisResults = AnalyzeData(users);
        }

        private List<AnalysisResult> AnalyzeData(List<User> users)
        {
            List<AnalysisResult> analysisResults = new();

            foreach (var user in users)
            {
                List<(User otherUser, int totalDifference)> diffList = new();

                foreach (var otherUser in users.Where(otherUser => otherUser.Id != user.Id))
                {
                    int diff = 0;
                    foreach (var motivator in user.MotivatorValues)
                    {
                        var currentUserValue = user.MotivatorValues.Single(v => v.motivator == motivator.motivator).value;
                        var otherUserValue = otherUser.MotivatorValues.Single(v => v.motivator == motivator.motivator).value;

                        diff += Math.Abs(currentUserValue - otherUserValue);
                    }

                    diffList.Add((otherUser, diff));
                }

                (User otherUser, int totalDifference) mostSimilar = diffList.MinBy(x => x.totalDifference);
                (User otherUser, int totalDifference) leastSimilar = diffList.MaxBy(x => x.totalDifference);

                // 50 is Max difference at the moment
                int CalculatePercentage(int totalDifference)
                {
                    return 100 - (totalDifference * 100 / 50);
                }

                analysisResults.Add(new AnalysisResult(user, new(mostSimilar.otherUser, CalculatePercentage(mostSimilar.totalDifference)), new(leastSimilar.otherUser, CalculatePercentage(leastSimilar.totalDifference))));
            }

            return analysisResults;
        }

        public List<AnalysisResult> GetAnalysisResults() => _analysisResults;
    }

    public record AnalysisResult(User BaseUser, CompareResult MostSimilarUser, CompareResult LeastSimilarUser);

    public record CompareResult(User OtherUser, int percentage);
}