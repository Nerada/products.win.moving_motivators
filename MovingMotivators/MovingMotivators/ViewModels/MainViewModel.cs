using System.Collections.Generic;
using MovingMotivators.Models.Calculations;

namespace MovingMotivators.ViewModels;

public class MainViewModel : ViewModelBase
{
    private AnalysisResult? _analysisResult;

    public MainViewModel()
    {
        AnalysisResult = new MotivatorAnalyzer().GetAnalysisResults();
    }

    public List<AnalysisResult> AnalysisResult { get; }

    public AnalysisResult? SelectedResult
    {
        get => _analysisResult;
        set => Set(ref _analysisResult, value);
    }
}