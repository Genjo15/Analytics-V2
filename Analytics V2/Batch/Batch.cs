using ComponentFactory.Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Analytics_V2
{
    [Serializable]
    public class Batch
    {
        private string _Name;
        private string _Type;
        Dictionary<string, Tuple<string,string,string>> _BatchElements;                     // Batch (single config) -> For an element : Target path - Config Name/Config path/Region FTP.
        Dictionary<Tuple<string, string>, List<Tuple<string, string>>> _BatchElementsMulti; // Batch (multi config) -> For an element : Target path/FTP Region - Tuple(Config Name / Config path).

        /**************************************************** Constructor *****************************************************/

        #region Constructor

        public Batch(string name, string type)
        {
            _Name = name;
            _Type = type;
            _BatchElements = new Dictionary<string, Tuple<string, string, string>>();
            _BatchElementsMulti = new Dictionary<Tuple<string,string>, List<Tuple<string, string>>>();
        }
        public Batch()
        {
        }

        #endregion

        /****************************************************** Methods *******************************************************/

        #region Methods

        /***************************************\
         * Add element of batch (single/multi) *
        \***************************************/

        public void AddBatchElement(string targetPath, string configName, string configPath, string regionFTP)
        {
            try
            {
                _BatchElements.Add(targetPath, Tuple.Create(configName, configPath, regionFTP));
            }
            catch
            {
                KryptonMessageBox.Show(targetPath + " as target path already exists.", "Error",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }

        public void AddBatchElement(string targetPath, string ftpRegion, Dictionary<string, string> configsInfo)
        {
            try
            {
                List<Tuple<string,string>> listOfConfigsForOneElement = new List<Tuple<string,string>>();

                foreach (KeyValuePair<string, string> configInfo in configsInfo)
                {
                    listOfConfigsForOneElement.Add(Tuple.Create(configInfo.Key, configInfo.Value));
                }

                _BatchElementsMulti.Add(Tuple.Create(targetPath,ftpRegion),listOfConfigsForOneElement);
	
            }
            catch
            {
                KryptonMessageBox.Show(targetPath + " as target path already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        #endregion

        #region Accessors

        public string Get_Name()
        {
            return _Name;
        }

        public string Get_Type()
        {
            return _Type;
        }

        public Dictionary<string, Tuple<string, string, string>> Get_BatchElements()
        {
            return _BatchElements;
        }

        public Dictionary<Tuple<string,string>, List<Tuple<string,string>>> Get_BatchElementsMulti()
        {
            return _BatchElementsMulti;
        }

        #endregion
    }
}
