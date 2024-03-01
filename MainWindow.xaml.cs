using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Crochet_Counter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int loopCount;
        private int currentStitch = 0;
        private string[] steps;
        private int currentStepIndex = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            PatternOutput.Text = "";
            if (PatternInput.Text.Length == 0)
            {
                MessageBox.Show("You didn't enter a pattern, oops!");
                return;
            }
            string pattern = PatternInput.Text;
            steps = pattern.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            if (steps.Length > 0)
            {
                SetCurrentStep(steps[0]);
            }
        }

        private void SetCurrentStep(string step)
        {
            var match = Regex.Match(step, @"\((\d+)\)");
            if (match.Success)
            {
                loopCount = int.Parse(match.Groups[1].Value);
                currentStitch = 0;
                PatternOutput.Text = step.Split('(')[0] + "\nCurrent Stitch " + currentStitch;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Enter a crochet pattern in this format:\r\n" +
                "R1: 6 sc in magic ring (6)\r\n" +
                "R2: BLO [inc] x6 (12)\r\n" +
                "R3: BLO [sc, inc] x6 (18)\r\n" +
                "R4: BLO sc, inc, [2 sc, inc] x5, sc (24)\r\n" +
                "R5: BLO [3 sc, inc] x6 (30)\r\n" +
                "R6: 2 sc, inc, [4 sc, inc] x5, 2 sc (36)\r\n" +
                "R7-13: [Sc] x36 (36) 7 rounds\r\n" +
                "R14: 2 sc, invdec, [4 sc, invdec] x5, 2 sc (30)\r\n" +
                "R15: [3 sc, invdec] x6 (24)\r\n" +
                "The most important and only necessary thing is to include the stitch count in brackets()");
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentStitch < loopCount)
            {
                currentStitch++;
                PatternOutput.Text = steps[currentStepIndex].Split('(')[0] + "\nCurrent Stitch " + currentStitch + "(" + loopCount + ")";
            }
            else if (currentStepIndex < steps.Length - 1)
            {
                // Move to the next step
                SetCurrentStep(steps[++currentStepIndex]);
            }
        }
    }
}
