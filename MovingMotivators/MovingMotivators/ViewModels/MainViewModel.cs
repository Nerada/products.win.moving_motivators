using System.Collections.Generic;
using System.Linq;

using MovingMotivators.Models.Calculations;

namespace MovingMotivators.ViewModels
{
    public class MainViewModel
    {
        public MainViewModel()
        {
            AnalysisResult = new MotivatorAnalyzer().GetAnalysisResults();
        }

        public List<AnalysisResult> AnalysisResult { get; }
    }
}
