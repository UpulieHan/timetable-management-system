using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TimetableManager.Domain.Models;
using TimetableManager.EntityFramework;

namespace TimetableManager.WPF.Views
{
    /// <summary>
    /// Interaction logic for TimetablePopup.xaml

    public partial class TimetablePopup : Window
    {
        public TimetablePopup()
        {
            InitializeComponent();

            int noOfCols = 6;
            int noOfRows = 10;

            //adding rows
            for (int i = 0; i < noOfCols; i++)
            {
                RowDefinition gridRow = new RowDefinition();
                gridRow.Height = new GridLength(45);
                timetableGrid.RowDefinitions.Add(gridRow);
            }

            //adding cols
            for (int i = 0; i < noOfRows; i++)
            {
                ColumnDefinition gridCols = new ColumnDefinition();
                timetableGrid.ColumnDefinitions.Add(gridCols);
            }

            // Add first column header
            TextBlock txtBlock1 = new TextBlock();
            txtBlock1.Text = "Author Name";
            txtBlock1.FontSize = 14;
            txtBlock1.FontWeight = FontWeights.Bold;
            txtBlock1.Foreground = new SolidColorBrush(Colors.Green);
            txtBlock1.VerticalAlignment = VerticalAlignment.Top;
            Grid.SetRow(txtBlock1, 0);
            Grid.SetColumn(txtBlock1, 0);

            timetableGrid.Children.Add(txtBlock1);


        }
    }
}



