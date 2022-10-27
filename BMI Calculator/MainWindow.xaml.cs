using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace BMI_Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    [XmlRoot("BMI Calc", Namespace = "www.bmicalc.ninja")]
    public partial class MainWindow : Window
    { 
        public string FilePath = "C:/Temp/";
        public string FileName = "YourBMI.xml";
        public class Customer
        {
            [XmlAttribute("Last Name")]
            public string? lastName { get; set; }
            [XmlAttribute("First Name")]
            public string? firstName { get; set; }
            [XmlAttribute("Phone Number")]
            public string? phoneNumber { get; set; }
            [XmlAttribute("Height")]
            public int heigthInches { get; set; }
            [XmlAttribute("Weight")]
            public int weightIbs { get; set; }
            [XmlAttribute("Customer BMI")]
            public int custBMI { get; set; }
            [XmlAttribute("Status")]
            public string? statusTitle { get; set; }

           
        }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            xPhoneBox.Text = "";
            xFirstNameBox.Text = "";
            xLastNameBox.Text = "";
            xHeigthInchesBox.Text = "";
            xWeightLbsBox.Text = "";
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);   

        }

        private void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            Customer customer1 = new Customer();
            customer1.lastName = xLastNameBox.Text;
            customer1.firstName = xFirstNameBox.Text;
            customer1.phoneNumber = xPhoneBox.Text;

            int currentWeight = 0;
            int currentHeight = 0;
            Int32.TryParse(xWeightLbsBox.Text, out currentWeight);
            Int32.TryParse(xHeigthInchesBox.Text, out currentHeight);
            customer1.weightIbs = currentWeight;
            customer1.heigthInches = currentHeight;

            int bmi;
            bmi = 703 * customer1.weightIbs / (customer1.heigthInches * customer1.heigthInches);
            customer1.custBMI = bmi;

            string yourBMIStatuse = "NA";
            customer1.statusTitle = yourBMIStatuse;

            //MessageBox.Show($"The customer's name is{customer1.firstName}{customer1.lastName} and they can be reached at {customer1.phoneNumber}. They are {customer1.heigthInches} inches tall. Their weight is {customer1.lastName}. Their BMI is{bmi}");
            if (bmi < 18.5) {
                Console.WriteLine("According to CDC.gov BMI Calculator you are underweight.");
                customer1.statusTitle = "Underweight"; 
            }
            if (bmi < 24.9) {
                Console.WriteLine("According to CDC.gov BMI Calculator you have a normal body weight.");
                customer1.statusTitle = "Normal";
            }

            if (bmi < 29.9)
            {
                Console.WriteLine("According to CDC.gov BMI Calculator you are overweight.");
                customer1.statusTitle = "Overweight";
            }
            else 
            { 
                Console.WriteLine("According to CDC.gov BMI Calculator you are obese.");
                customer1.statusTitle = "Obese";
            }

            TextWriter writer = new StreamWriter(FilePath + FileName);
            XmlSerializer ser = new XmlSerializer(typeof(Customer));
            ser.Serialize(writer, customer1);
            writer.Close();
        }
    }
}
