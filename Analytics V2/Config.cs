using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;


namespace Analytics_V2
{
    public class Config
    {
        /************************************************* Variables *************************************************/

        #region Variables

        private String _Name;               // Name of the config.
        private String _Path;               // Path of the config.
        private String _Warning;            // Warning before launching the config.
        private String _XmlEncoding;        // Xml encoding.
        private XDocument _XmlDocument;     // Xml document.
        private List<Process> _ProcessList; // List of used process.

        private String _DataSeparator;    ////
        private String _OutputSeparator;  //
        private String _DecimalSeparator; //
        private int _Headerlines;         // Header.
        private int _TargetsNumber;       //
        private int _EncodingInput;       //
        private String _EncodingOutput;   //
        private String _CountryCode;      //

        #endregion

        /**************************************************** Constructor ****************************************************/

        #region Constructor

        public Config(string name, string path)
        {
            _Name = name;
            _Path = path;

            _Warning = null;
            _XmlEncoding = null;
            _DataSeparator = null;
            _OutputSeparator = null;
            _DecimalSeparator = null;
            _EncodingOutput = null;
            _CountryCode = null;

            _XmlDocument = new XDocument();
            _ProcessList = new List<Process>();

            LoadConfigXml();
            SetWarning();
            GetHeader();
            CreateProcess();
        }

        public Config()
        {
            _XmlDocument = new XDocument();
            _ProcessList = new List<Process>();
        }

        #endregion

        /****************************************************** Methods ******************************************************/

        #region Methods

        /********************************\
         * Load Xml & get xml encoding. *
        \********************************/

        public void LoadConfigXml()
        {
            try
            {
                _XmlDocument = XDocument.Load(@_Path);
                _XmlEncoding = _XmlDocument.Declaration.Encoding;
            }

            catch (Exception ex) { Console.WriteLine(ex); }
        }

        /**************************************************\
         * Get Header: Query in the XML to find:          *
         *   - The data separator & the output separator. *
         *   - The country code.                          *
         *   - The decimal separator.                     *
         *   - The targets number.                        *
         *   - The headerlines.                           *
         *   - The encoding input.                        *
         *   - The encoding output.                       *
        \**************************************************/

        private void GetHeader()
        {
            var header1 = from item in _XmlDocument.Element("region").Elements("DataSeparator")
                          select new
                          {
                              dataSeparator = item.Attribute("Val"),
                              outputSeptarator = item.Attribute("Output_Separator")
                          };
            foreach (var element in header1)
            {
                if (!String.IsNullOrEmpty(element.dataSeparator.ToString().Split(new string[] { "\"" }, StringSplitOptions.None)[1]))
                    _DataSeparator = element.dataSeparator.ToString().Split(new string[] { "\"" }, StringSplitOptions.None)[1];
                else ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("DataSeparator manquant !", "Analytics", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                _OutputSeparator = element.outputSeptarator.ToString().Split(new string[] { "\"" }, StringSplitOptions.None)[1];
            }

            var header2 = from item in _XmlDocument.Element("region").Elements("CodePays")
                          select new
                          {
                              countryCode = item.Attribute("Val"),
                          };
            foreach (var element in header2)
            {
                if (!String.IsNullOrEmpty(element.countryCode.ToString().Split(new string[] { "\"" }, StringSplitOptions.None)[1]))
                    _CountryCode = element.countryCode.ToString().Split(new string[] { "\"" }, StringSplitOptions.None)[1];
                else ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("Code Pays manquant !", "Analytics", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            var header3 = from item in _XmlDocument.Element("region").Elements("DecimalSeparator")
                          select new
                          {
                              decimalSeparator = item.Attribute("Val"),
                          };
            foreach (var element in header3)
            {
                if (!String.IsNullOrEmpty(element.decimalSeparator.ToString().Split(new string[] { "\"" }, StringSplitOptions.None)[1]))
                    _DecimalSeparator = element.decimalSeparator.ToString().Split(new string[] { "\"" }, StringSplitOptions.None)[1];
                else ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("DecimalSeparator manquant !", "Analytics", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            var header4 = from item in _XmlDocument.Element("region").Elements("TargetsNumber")
                          select new
                          {
                              targetsNumber = item.Attribute("Val"),
                          };
            foreach (var element in header4)
            {
                if (!String.IsNullOrEmpty(element.targetsNumber.ToString().Split(new string[] { "\"" }, StringSplitOptions.None)[1]))
                    _TargetsNumber = int.Parse(element.targetsNumber.ToString().Split(new string[] { "\"" }, StringSplitOptions.None)[1]);
                else ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("TargetsNumber manquant !", "Analytics", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            var header5 = from item in _XmlDocument.Element("region").Elements("HeaderLines")
                          select new
                          {
                              headerLines = item.Attribute("Val"),
                          };
            foreach (var element in header5)
            {
                if (!String.IsNullOrEmpty(element.headerLines.ToString().Split(new string[] { "\"" }, StringSplitOptions.None)[1]))
                    _Headerlines = int.Parse(element.headerLines.ToString().Split(new string[] { "\"" }, StringSplitOptions.None)[1]);
                else ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("HeaderLine manquant !", "Analytics", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            var header6 = from item in _XmlDocument.Element("region").Elements("Encoding_INPUT")
                          select new
                          {
                              encodingInput = item.Attribute("Val"),
                          };
            foreach (var element in header6)
            {
                if (!String.IsNullOrEmpty(element.encodingInput.ToString().Split(new string[] { "\"" }, StringSplitOptions.None)[1]))
                    _EncodingInput = int.Parse(element.encodingInput.ToString().Split(new string[] { "\"" }, StringSplitOptions.None)[1]);
                else ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("Encoding_INPUT manquant !", "Analytics", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            var header7 = from item in _XmlDocument.Element("region").Elements("Encoding_OUTPUT")
                          select new
                          {
                              encodingOutput = item.Attribute("Val"),
                          };
            foreach (var element in header7)
            {
                if (!String.IsNullOrEmpty(element.encodingOutput.ToString().Split(new string[] { "\"" }, StringSplitOptions.None)[1]))
                    _EncodingOutput = element.encodingOutput.ToString().Split(new string[] { "\"" }, StringSplitOptions.None)[1];
                else ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("Encoding_OUTPUT manquant !", "Analytics", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            //Console.WriteLine(_DataSeparator + " " + _OutputSeparator + " " + _CountryCode + " " + _DecimalSeparator + " " + _TargetsNumber + " " + _Headerlines + " " + _EncodingInput + " " + _EncodingOutput);
        }

        /*************************************************************************\
         * Create Process: Query in the XML to find :                            *
         *   - The name and the orderID of the function.                         *
         *   - The dataTable.                                                    *
         *   - The comment of the function.                                      *
         *   - Instanciate the process.                                          *
        \*************************************************************************/

        public void CreateProcess()
        {
            // Retrieve name & order ID of EACH function.
            var header = from item in _XmlDocument.Element("region").Elements("Function")
                         select new
                         {
                             name = item.Attribute("Name"),
                             orderId = item.Attribute("Order_ID"),
                         };

            foreach (var processes in header)
            {
                DataTable dataTable = new DataTable();

                // Fill DataTable : name of DataTable + Order_ID
                dataTable.TableName = processes.name.ToString().Split(new string[] { "\"" }, StringSplitOptions.None)[1];
                dataTable.Columns.Add("Order_ID");
                dataTable.Rows.Add();
                dataTable.Rows[0].SetField("Order_ID", int.Parse(processes.orderId.ToString().Split(new string[] { "\"" }, StringSplitOptions.None)[1]));

                var parameters = from item in _XmlDocument.Element("region").Elements("Function").Elements("Field")
                                 where item.Parent.Attribute("Name").Equals(processes.name) && item.Parent.Attribute("Order_ID").Equals(processes.orderId)
                                 select item;

                // Fill the rest of the datatable
                foreach (XElement element in parameters)
                {
                    // Case if the process is an unique treatment one.
                    if (!element.Attribute("Name").Value.Equals("Params"))
                    {
                        if (!dataTable.Columns.Contains(element.Attribute("Name").Value))
                        {
                            dataTable.Columns.Add(element.Attribute("Name").Value);
                            dataTable.Rows[0].SetField(element.Attribute("Name").Value, element.Attribute("Val").Value);
                        }
                    }

                    // Case if the process is an multi treatments one.
                    else if (element.Attribute("Name").Value.ToString().Equals("Params"))
                    {
                        foreach (XAttribute attribute in element.Attributes())
                        {
                            if (!dataTable.Columns.Contains(attribute.Name.ToString()))
                            {
                                dataTable.Columns.Add(attribute.Name.ToString());
                                dataTable.Rows[0].SetField(attribute.Name.ToString(), attribute.Value);
                            }
                            else if (String.IsNullOrEmpty(dataTable.Rows[dataTable.Rows.Count - 1][attribute.Name.ToString()].ToString()))
                            {
                                dataTable.Rows[dataTable.Rows.Count - 1].SetField(attribute.Name.ToString(), attribute.Value);
                            }
                            else if (dataTable.Columns.Contains(attribute.Name.ToString()))
                            {
                                dataTable.Rows.Add();
                                dataTable.Rows[dataTable.Rows.Count - 1].SetField(attribute.Name.ToString(), attribute.Value);
                            }
                        }
                    }
                }
                    
                /////////////////////////////////////////
                // DEBUG : For displaying the datatable
                // Console.WriteLine(dataTable.TableName + " :");
                // foreach (DataColumn column in dataTable.Columns)
                // {
                //     Console.Write(column.ColumnName);
                //     Console.Write(" | ");
                // }
                // Console.WriteLine();
                // foreach (DataRow row in dataTable.Rows)
                // {  
                //    foreach (DataColumn column in dataTable.Columns)
                //    {
                //        Console.Write((row[column]).ToString());
                //        Console.Write(" | ");
                //    }
                //    Console.Write("\r\n--------------\r\n");
                // }
                // Console.WriteLine();

                // Finaly, create the process
                _ProcessList.Add(new Process(processes.name.ToString().Split(new string[] { "\"" }, StringSplitOptions.None)[1], int.Parse(processes.orderId.ToString().Split(new string[] { "\"" }, StringSplitOptions.None)[1]), dataTable));
            }

            
            // Retrieve comment
            var com = from item in _XmlDocument.Element("region").DescendantNodes().OfType<XComment>()
                        where item.NextNode.GetType().ToString().Split(new string[] { "." }, StringSplitOptions.None)[3].Equals("XElement") && item.NextNode.ToString().Split(new string[] { " Name" }, StringSplitOptions.None)[0].Equals("<Function")
                        select new
                        {
                            comment = item,
                            nextNodeOrderId = item.NextNode.ToString().Split(new string[] { "\"" }, StringSplitOptions.None)[3]
                        };


            // Add comment to the process (search the matching function)
            try
            {
                foreach (var c in com)
                {
                    foreach (Process element in _ProcessList)
                        if (element.Get_OrderId() == int.Parse(c.nextNodeOrderId))
                            element.Set_Comment(c.comment.ToString());
                }
            }
            catch (Exception ex) { Console.WriteLine(ex); }
        }

        /****************\
         * Set _Warning *
        \****************/

        private void SetWarning()
        {
            _Warning = "";

            // Retrieve comment
            var war = from item in _XmlDocument.Element("region").DescendantNodes().OfType<XComment>()
                      where item.NextNode.GetType().ToString().Split(new string[] { "." }, StringSplitOptions.None)[3].Equals("XComment")
                      select new
                      {
                          warning = item
                      };

            foreach (var w in war)
                _Warning = _Warning + w.warning.ToString();
        }

        /********************\
         * Clone the object *
        \********************/

        public Config Clone()
        {
            Config clonedConfig = new Config();
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream stream = new MemoryStream();

            clonedConfig._Name = this._Name;
            clonedConfig._Path = this._Path;
            clonedConfig._Warning = this._Warning;
            clonedConfig._XmlEncoding = this._XmlEncoding;
            clonedConfig._XmlDocument = this._XmlDocument;

            formatter.Serialize(stream, _ProcessList);
            stream.Position = 0;
            clonedConfig._ProcessList = (List<Process>)formatter.Deserialize(stream);

            clonedConfig._DataSeparator = this._DataSeparator;
            clonedConfig._OutputSeparator = this._OutputSeparator;
            clonedConfig._DecimalSeparator = this._DecimalSeparator;
            clonedConfig._Headerlines = this._Headerlines;
            clonedConfig._TargetsNumber = this._TargetsNumber;
            clonedConfig._EncodingInput = this._EncodingInput;
            clonedConfig._EncodingOutput = this._EncodingOutput;
            clonedConfig._CountryCode = this._CountryCode;

            return clonedConfig;
        }

        #endregion


        #region Accessors

        public String Get_Name()
        {
            return _Name;
        }

        public String Get_Path()
        {
            return _Path;
        }

        public String Get_Warning()
        {
            return _Warning;
        }

        public String Get_XmlEncoding()
        {
            return _XmlEncoding;
        }

        public int Get_EncodingInput()
        {
            return _EncodingInput;
        }

        public String Get_EncodingOutput()
        {
            return _EncodingOutput;
        }
        public String Get_OutputSeparator()
        {
            return _OutputSeparator;
        }

        public String Get_DecimalSeparator()
        {
            return _DecimalSeparator;
        }

        public int Get_Headerlines()
        {
            return _Headerlines;
        }

        public String Get_DataSeparator()
        {
            return _DataSeparator;
        }

        public String Get_CountryCode()
        {
            return _CountryCode;
        }

        public int Get_TargetsNumber()
        {
            return _TargetsNumber;
        }

        public List<Process> Get_ProcessList()
        {
            return _ProcessList;
        }

        #endregion
    }
}
