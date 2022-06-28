namespace MovingMotivators.Models.Calculations
{

    public record AnalysisResult(User BaseUser, CompareResult MostSimilarUser, CompareResult LeastSimilarUser);
}