using System;
using System.Collections.Generic;
using System.Linq;

namespace MovingMotivators.Models.Calculations;

public class MotivatorAnalyzer
{
    private readonly List<AnalysisResult> _analysisResults;

    public MotivatorAnalyzer()
    {
        List<User> users = new ValueGenerator().GetData().ToList();

        _analysisResults = AnalyzeData(users);
    }

    private List<AnalysisResult> AnalyzeData(List<User> users)
    {
        List<AnalysisResult> analysisResults = new();

        foreach (User user in users)
        {
            List<(User otherUser, int totalDifference)> diffList = new();

            foreach (User otherUser in users.Where(otherUser => otherUser.Id != user.Id))
            {
                int diff = 0;
                foreach (Motivator motivator in user.MotivatorValues.Select(motivator => motivator.Motivator))
                {
                    int currentUserValue = user.MotivatorValues.Single(v => v.Motivator      == motivator).Value;
                    int otherUserValue   = otherUser.MotivatorValues.Single(v => v.Motivator == motivator).Value;

                    diff += Math.Abs(currentUserValue - otherUserValue);
                }

                diffList.Add((otherUser, diff));
            }

            (User otherUser, int totalDifference) mostSimilar  = diffList.MinBy(x => x.totalDifference);
            (User otherUser, int totalDifference) leastSimilar = diffList.MaxBy(x => x.totalDifference);

            // 50 is Max difference at the moment
            int CalculatePercentage(int totalDifference) => 100 - totalDifference * 100 / 50;

            analysisResults.Add(new AnalysisResult(user, new CompareResult(mostSimilar.otherUser, CalculatePercentage(mostSimilar.totalDifference)),
                                                   new CompareResult(leastSimilar.otherUser,      CalculatePercentage(leastSimilar.totalDifference))));
        }

        return analysisResults;
    }

    public List<AnalysisResult> GetAnalysisResults() => _analysisResults;
}

public record AnalysisResult(User BaseUser, CompareResult MostSimilarUser, CompareResult LeastSimilarUser);

public record CompareResult(User OtherUser, int Percentage);