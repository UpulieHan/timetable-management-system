
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
    /// Interaction logic for Tab_Stat_Student.xaml
    /// </summary>
    public partial class Tab_Stat_Student : UserControl
    {
        
        public List<GroupId> GrpIdList { get; private set; }
        public List<GroupNumber> GrpNumberList { get; private set; }

        public Hashtable HashTable = new Hashtable();
        public List<StdStatGrid> StudentStatList = new List<StdStatGrid>();



        public Tab_Stat_Student()
        {
            InitializeComponent();
          
            _ = this.LoadProgramme();
            dataGridstd.ItemsSource = StudentStatList;

        }

        private void button_Click_std(object sender, RoutedEventArgs e)
        {

        }

        private async Task LoadProgramme()
        {
            GroupIdDataService programmeDataService = new GroupIdDataService(new EntityFramework.TimetableManagerDbContext());
            GrpIdList = await programmeDataService.GetGroupId();

            int sumY1 = 0;
            int sumY2 = 0;
            int sumY3 = 0;
            int sumY4 = 0;


            GrpIdList.ForEach(g =>
            {

                if (g.GroupID.StartsWith("Y1"))
                {
                  
                    sumY1 = sumY1 + 1;
                   
                } if (g.GroupID.StartsWith("Y2"))
                {
                    
                    sumY2 = sumY2 + 1;
                } if(g.GroupID.StartsWith("Y3"))
                {
                    sumY3 = sumY3 + 1;
                }
                if(g.GroupID.StartsWith("Y4")){
                    sumY4 = sumY4 + 1;
                }

            
            });

            StudentStatList.Add(new StdStatGrid { Year1 = sumY1 , Year2 = sumY2, Year3 = sumY3, Year4 = sumY4 });

        }


    }

    public class StdStatGrid
    {

        public int Year1 { get; set; }

        public int Year2 { get; set; }

        public int Year3 { get; set; }

        public int Year4 { get; set; }
    }
}
