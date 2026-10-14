using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _2026_WpfApp3
{
    public partial class MainWindow : Window
    {
        Color strokeColor = Colors.Black;
        Color fillColor = Colors.Transparent;
        int strokeThickness = 1;
        string shapeType = "Line";

        public MainWindow()
        {
            InitializeComponent();
            StrokeColorPicker.SelectedColor = strokeColor;
            FillColorPicker.SelectedColor = fillColor;
        }

        private void ShapeButton_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton targetShapeButton && targetShapeButton.Tag != null)
            {
                shapeType = targetShapeButton.Tag.ToString();
                DisplayStatus();
            }
        }

        private void StrokeColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            // 使用 ?? 避免 null 造成例外錯誤
            strokeColor = StrokeColorPicker.SelectedColor ?? Colors.Black;
            DisplayStatus();
        }

        private void FillColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            fillColor = FillColorPicker.SelectedColor ?? Colors.Transparent;
            DisplayStatus();
        }

        private void ThicknessSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            // 在 InitializeComponent 階段，Slider 可能會尚未準備好就觸發
            if (ThicknessSlider != null)
            {
                strokeThickness = (int)ThicknessSlider.Value;
                DisplayStatus();
            }
        }

        private void DisplayStatus()
        {
            if (ColorLabel != null)
                ColorLabel.Content = $"筆刷色彩:{strokeColor}     填充色彩:{fillColor}     筆刷粗細：{strokeThickness}";
            if (ShapeLabel != null)
                ShapeLabel.Content = $"形狀：{shapeType}";
        }
    }
}