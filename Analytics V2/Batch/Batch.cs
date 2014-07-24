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
        Dictionary<string, Tuple<string,string>> _BatchElements;              // Batch (single config) -> For an element : Target path - Config Name/Config path.
        Dictionary<string, List<Tuple<string, string>>> _BatchElementsMulti; // Batch (multi config) -> For an element : Target path - Tuple(Config Name / Config path).

        /**************************************************** Constructor *****************************************************/

        #region Constructor

        public Batch(string name, string type)
        {
            _Name = name;
            _Type = type;
            _BatchElements = new Dictionary<string, Tuple<string, string>>();
            _BatchElementsMulti = new Dictionary<string,List<Tuple<string,string>>>();
        }

        #endregion

        /****************************************************** Methods *******************************************************/

        #region Methods

        /***************************************\
         * Add element of batch (single/multi) *
        \***************************************/

        public void AddBatchElement(string targetPath, string configName, string configPath)
        {
            try
            {
                _BatchElements.Add(targetPath, Tuple.Create(configName, configPath));
            }
            catch
            {
                KryptonMessageBox.Show(targetPath + " as target path already exists.", "Error",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }

        public void AddBatchElement(string targetPath, Dictionary<string, string> configsInfo)
        {
            try
            {
                List<Tuple<string,string>> listOfConfigsForOneElement = new List<Tuple<string,string>>();

                foreach (KeyValuePair<string, string> configInfo in configsInfo)
                {
                    listOfConfigsForOneElement.Add(Tuple.Create(configInfo.Key, configInfo.Value));
                }

                _BatchElementsMulti.Add(targetPath,listOfConfigsForOneElement);
	
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

        public Dictionary<string, Tuple<string, string>> Get_BatchElements()
        {
            return _BatchElements;
        }

        public Dictionary<string, List<Tuple<string,string>>> Get_BatchElementsMulti()
        {
            return _BatchElementsMulti;
        }

        #endregion
    }
}
