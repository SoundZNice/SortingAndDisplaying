using System;
using System.Collections.Generic;
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
using SortAlgorythms;

namespace TryToWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void generateNewArray(int elementsNumber, int maxValue)
        {
            Random rnd = new Random();

            int n = 0;

            while (n < elementsNumber)
            {
                int a = rnd.Next(elementsNumber);

                bool isEqual = false;

                foreach (int value in sortingArray)
                {
                    isEqual = value == a ? true : false;
                }

                if (!isEqual)
                {
                    sortingArray[n] = a;
                    n++;
                }
            }

            drawArray();
        }
    }
}
