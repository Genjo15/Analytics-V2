using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;

namespace Analytics_V2
{
    public partial class SpecificCountries : UserControl
    {
        /********************************************* Declaration of variables *********************************************/

        #region Variables

        private List<String> _SpecificCountriesList;

        #endregion

        /**************************************************** Constructor ****************************************************/

        #region Constructor

        public SpecificCountries()
        { 
            InitializeComponent();
            this.Dock = System.Windows.Forms.DockStyle.Fill;

            _SpecificCountriesList = new List<string>();
            FillSpecificCountriesList();
            SpecificCountriesListBox.DataSource = _SpecificCountriesList;
        }

        #endregion


        /****************************************************** Methods ******************************************************/

        #region Methods

        /***************************\
         * Fill list of countries. *
        \***************************/

        private void FillSpecificCountriesList()
        {
            _SpecificCountriesList.Add("Canada - Quebec QH");
            //_SpecificCountriesList.Add("Czech Republic");
            _SpecificCountriesList.Add("Finland");
            _SpecificCountriesList.Add("Ireland");
            _SpecificCountriesList.Add("Italy Sky");
            _SpecificCountriesList.Add("Matching Canada (Genres + Complementary)");
            _SpecificCountriesList.Add("TV5");
        }

        /**************************************************\
         * Launch specific country process.               *
         *     - Instanciate process of the imported dll. *
         *     - Run it.                                  *
        \**************************************************/

        public void LaunchSpecificCountry(String country)
        {
            switch (country)
            {
                case "Canada - Quebec QH": CanadaQH.CanadaQH canadaProcess = new CanadaQH.CanadaQH();
                                           canadaProcess.Run();
                                           break;

                //case "Czech Republic": Czech_ReProcessing.CzechReProcessing czechRepublicProcess = new Czech_ReProcessing.CzechReProcessing();
                //                       czechRepublicProcess.Run();
                //                       break;

                case "Finland": Finland.Finland finlandProcess = new Finland.Finland();
                                finlandProcess.Run();
                                break;

                case "Ireland": Ireland.Ireland irelandProcess = new Ireland.Ireland();
                                irelandProcess.Run();
                                break;

                case "Italy Sky": skyit.skyit skyItProcess = new skyit.skyit();
                                  skyItProcess.StartPosition = FormStartPosition.CenterScreen;
                                  skyItProcess.Show();
                                  break;

                case "Matching Canada (Genres + Complementary)": MatchingCanada.MatchingCanada matchingCanadaProcess = new MatchingCanada.MatchingCanada();
                                                                 matchingCanadaProcess.Run();
                                                                 break;

                case "TV5": TV5_2012.TV5_2012 tv5Process = new TV5_2012.TV5_2012();
                            tv5Process.StartPosition = FormStartPosition.CenterScreen;
                            tv5Process.Show();
                            break;
            }
        }
        
        #endregion
    }
}
