using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

using Arction.Wpf.Charting;
using Arction.Wpf.Charting.Axes;
using Arction.Wpf.Charting.SeriesPolar;
using Arction.Wpf.Charting.Views.ViewPolar;
using Arction.Wpf.Charting.Views.ViewXY;

using MovingMotivators.Models;
using MovingMotivators.Models.Calculations;
using MovingMotivators.ViewModels;

namespace MovingMotivators
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainView : Window
    {
        //Chart
        private LightningChart _chart;

        int _attributeCount = Enum.GetNames(typeof(Motivator)).Length - 1;
        private readonly MainViewModel _mainViewModel;

        public MainView(MainViewModel mainViewModel)
        {
            InitializeComponent();

            _mainViewModel = mainViewModel;

            DataContext = _mainViewModel;

            mainViewModel.PropertyChanged += MainViewModel_PropertyChanged;

            CreateChart();
        }

        private void MainViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(MainViewModel.SelectedResult))
            {
                if (_mainViewModel.SelectedResult is not { } selectedResult) return;

                UpdateDataPoints(selectedResult);
            }
        }


        /// <summary>
        /// Create chart
        /// </summary>
        private void CreateChart()
        {
            //Create new chart
            _chart = new LightningChart();
            _chart.Title.Text = string.Empty;

            //Disable rendering, strongly recommended before updating chart properties
            _chart.BeginUpdate();

            //Set active view
            _chart.ActiveView = ActiveView.ViewPolar;

            ViewPolar view = _chart.ViewPolar;

            _chart.ChartBackground.GradientFill = GradientFill.Solid;
            _chart.ChartBackground.Color = Colors.Silver;

            view.GraphBackground.GradientFill = GradientFill.Solid;
            view.GraphBackground.Color = Colors.DarkGray;

            // Disable button clicks, which allow to drag the traces inner view
            view.ZoomPanOptions.WheelZooming = false;
            view.ZoomPanOptions.DevicePrimaryButtonAction = UserInteractiveDeviceButtonAction.None;
            view.ZoomPanOptions.DeviceSecondaryButtonAction = UserInteractiveDeviceButtonAction.None;
            view.ZoomPanOptions.DeviceTertiaryButtonAction = UserInteractiveDeviceButtonAction.None;

            view.ZoomPanOptions.AxisWheelAction = AxisWheelAction.None;


            //Create axes 
            view.Axes.Clear();

            foreach (var motivator in Enum.GetNames(typeof(Motivator)).Where(m => m != Enum.GetName(Motivator.Unknown)))
            {
                AxisPolar motivatorAxis = new AxisPolar(view);
                motivatorAxis.Units.Text = motivator;
                motivatorAxis.SetRange(0, Enum.GetNames(typeof(Motivator)).Where(m => m != Enum.GetName(Motivator.Unknown)).Count());
                view.Axes.Add(motivatorAxis);
            }

            double dAngleStep = 360.0 / _attributeCount;

            for (int iAxis = 0; iAxis < _attributeCount; iAxis++)
            {
                AxisPolar axis = view.Axes[iAxis];
                axis.Title.Text = string.Empty;
                axis.AngleOrigin = 90;
                axis.AllowScaling = false;
                axis.GridAngular.Visible = false;
                axis.AngularLabelsVisible = false;
                axis.AngularAxisCircleVisible = false;
                axis.UsePreviousAxisDiameter = true;
                axis.MajorGrid.Visible = false;
                axis.AxisThickness = 1;
                axis.MinorDivTickStyle.Visible = false;
                axis.AngularAxisMajorDivCount = 0;
                axis.AngularLabelsVisible = false;
                axis.AxisColor = Colors.Transparent;
                axis.Units.Font = new WpfFont("Segoe UI", 10.0, true, false);
                axis.Units.Angle = 270 - dAngleStep * iAxis;
                axis.Units.RadialOffsetPercentage = 55;
                axis.Units.RadialOffsetPixels = 80;
                axis.MajorDivTickStyle.Visible = false;
                axis.AngularTicksVisible = false;
                axis.AmplitudeLabelsVisible = false;
                axis.AllowScaling = false;
                axis.AllowScrolling = false;
                axis.AllowUserInteraction = false;
                axis.DragSnapToDiv = false;

                axis.Highlight = Highlight.None;

                axis.RangeChanged += axis_RangeChanged;
            }

            view.ZoomScale = 0.8f;

            view.LegendBox.Layout = LegendBoxLayout.Horizontal;

            int imotivator = 0;

            AxisPolar axisMaster = view.Axes[0];

            List<(string, Color, byte alpha)> displayValues = new(){("Base", Colors.Blue, 200),("Most similar", Colors.SpringGreen, 10),("Least similar", Colors.Red, 10)};

            //Add series for each car and attach them to the first axis 
            foreach ((string valueName, Color color, byte alpha) display in displayValues)
            {
                AreaSeriesPolar series = new AreaSeriesPolar(view, axisMaster)
                {
                    PointsVisible = true
                };
                
                series.Title.Text = display.valueName;
                series.ClipInsideGraph = false;
                series.LineStyle.Color = Color.FromArgb(200, display.color.R, display.color.G, display.color.B);
                series.PointStyle.Color1 = Color.FromArgb(200, display.color.R, display.color.G, display.color.B);
                series.PointStyle.Height = series.PointStyle.Width = 1;
                series.FillColor = Color.FromArgb(display.alpha, display.color.R, display.color.G, display.color.B);
                view.AreaSeries.Add(series);

                imotivator++;
            }

            //Allow chart rendering
            _chart.EndUpdate();

            _chartGrid.Children.Add(_chart);

            //Points must be updated after chart has got its rendering size
            _chart.Loaded += _chart_Loaded;


        }

        /// <summary>
        /// Chart loaded
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Args</param>
        private void _chart_Loaded(object sender, RoutedEventArgs e)
        {
            _chart.BeginUpdate();

            UpdateGrid();

            _chart.EndUpdate();
        }

        /// <summary>
        /// Axis range changed
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Args</param>
        private void axis_RangeChanged(object sender, RangeChangedPolarEventArgs e)
        {
            _chart.BeginUpdate();

            UpdateGrid();

            _chart.EndUpdate();
        }

        /// <summary>
        /// Update data points 
        /// </summary>
        private void UpdateDataPoints(AnalysisResult analysisResult)
        {
            AddUserValues(analysisResult.BaseUser, 0);
            AddUserValues(analysisResult.MostSimilarUser.OtherUser, 1);
            AddUserValues(analysisResult.LeastSimilarUser.OtherUser, 2);
        }

        private void AddUserValues(User user, int iMotivator)
        {
            _chart.BeginUpdate();

            ViewPolar v = _chart.ViewPolar;
            AxisPolar axisMaster = v.Axes[0];


            AreaSeriesPolar series = _chart.ViewPolar.AreaSeries[iMotivator];
            PolarSeriesPoint[] points = new PolarSeriesPoint[_attributeCount + 1]; //+1 so we can connect last to first point
            double[] attributeValues = GetMotivatorValues(user.MotivatorValues);
            for (int iAttrib = 0; iAttrib < _attributeCount; iAttrib++)
            {
                PointPolar scaledPoint = ScaleToMasterAxis(v.Axes[iAttrib], axisMaster, attributeValues[iAttrib]);
                points[iAttrib].Amplitude = scaledPoint.Amplitude;
                points[iAttrib].Angle = scaledPoint.Angle;
            }
            points[_attributeCount] = points[0];

            series.Points = points;

            _chart.EndUpdate();
        }

        private double[] GetMotivatorValues(ImmutableList<MotivatorValue> motivatorValues)
        {
            return motivatorValues.Select(m => (double)m.value).ToArray<double>();
        }

        /// <summary>
        /// Update grid lines
        /// </summary>
        private void UpdateGrid()
        {
            _chart.BeginUpdate();

            //Update spider grid 

            ViewPolar v = _chart.ViewPolar;
            AxisPolar axisMaster = v.Axes[0];
            v.PointLineSeries.Clear();

            int iGridLineCount = axisMaster.MajorDivCount;
            for (int iGrid = 0; iGrid < iGridLineCount; iGrid++)
            {
                PointLineSeriesPolar gridLine = new PointLineSeriesPolar(v, axisMaster);
                gridLine.LineStyle.Pattern = LinePattern.Dot;
                gridLine.LineStyle.Width = 1;
                gridLine.LineStyle.Color = Color.FromArgb(100, 255, 255, 255);
                gridLine.PointsVisible = false;
                gridLine.ShowInLegendBox = false;
                gridLine.AllowUserInteraction = false;
                int iDataPointCount = _attributeCount + 1; //closed path 
                PolarSeriesPoint[] points = new PolarSeriesPoint[iDataPointCount];

                for (int iAttrib = 0; iAttrib < _attributeCount; iAttrib++)
                {
                    AxisPolar axis = v.Axes[iAttrib];
                    double dAmplitudeStep = (axis.MaxAmplitude - axis.MinAmplitude) / iGridLineCount;
                    double dAmplitude = axis.MinAmplitude + (iGrid + 1) * dAmplitudeStep;
                    PointPolar scaledPoint = ScaleToMasterAxis(axis, axisMaster, dAmplitude);
                    points[iAttrib].Amplitude = scaledPoint.Amplitude;
                    points[iAttrib].Angle = scaledPoint.Angle;
                }
                points[iDataPointCount - 1] = new PolarSeriesPoint(points[0].Angle, points[0].Amplitude);
                gridLine.Points = points;
                v.PointLineSeries.Add(gridLine);
            }

            _chart.EndUpdate();
        }

        /// <summary>
        /// Scale given attribute to master axis scale, by converting the attribute value to screen coordinate and then to master axis range 
        /// </summary>
        private PointPolar ScaleToMasterAxis(AxisPolar attributeAxis, AxisPolar masterAxis, double value)
        {

            PointFloat coord = attributeAxis.ValueToCoord(new PointPolar(0, value), false);
            return masterAxis.CoordToValue(coord, false);
        }

        /// <summary>
        /// Call this method to stop threads, dispose unmanaged resources or 
        /// any other job that needs to be done before this example object is 
        /// ready for garbage collector.
        /// </summary>
        public void Dispose()
        {
            if (_chart != null)
            {
                (Content as Grid).Children.Remove(_chart);

                _chart.Dispose();
                _chart = null;
            }

        }
    }
}
