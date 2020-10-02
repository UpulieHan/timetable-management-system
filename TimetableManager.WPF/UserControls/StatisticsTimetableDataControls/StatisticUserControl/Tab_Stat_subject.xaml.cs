
﻿using System;
using System.Collections;
using System.Collections.Generic;
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
using TimetableManager.Domain.Models;
using TimetableManager.EntityFramework.Services;


namespace TimetableManager.WPF.StatisticsTimetableDataControls.StatisticUserControl
{
    /// <summary>
    /// Interaction logic for Tab_Stat_subject.xaml
    /// </summary>
    public partial class Tab_Stat_subject : UserControl
    {

        public List<Subject> SubjectList { get; private set; }
      
        public List<SubStatGrid> SubStatList = new List<SubStatGrid>();

        public Tab_Stat_subject()
        {
            InitializeComponent();
         
            _ = this.LoadSubjectData();
            dataGridstd.ItemsSource = SubStatList;
        }

        private async Task LoadSubjectData()
        {
            SubjectDataService subjectDataService = new SubjectDataService(new EntityFramework.TimetableManagerDbContext());

            SubjectList = await subjectDataService.GetSubjectsAsync();

            SubjectList.ForEach(f =>
            {
                int sum = f.LectureHours + f.EvaluationHours + f.TutorialHours + f.LabHours;
                SubStatList.Add(new SubStatGrid { module = f.SubjectName, count = sum });
                
            });

            
        }

        private void button_Click_sub(object sender, RoutedEventArgs e)
        {

        }
    }

    public class SubStatGrid
    {
        public string module { get; set; }

        public int count { get; set; }

    }
}
