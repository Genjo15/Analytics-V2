using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Analytics_V2
{
    [Serializable]
    public class Process
    {
        /********************************************* Declaration of variables *********************************************/

        #region Variables

        private String _Name;    // Name of the process.
        private String _Comment; // The comment of the process. 
        private int _OrderId;    // Order ID of the process.

        private DataTable _DataTable; // DataTable.

        #endregion

        /**************************************************** Constructor ****************************************************/

        #region Constructor

        public Process(String name, int orderId, DataTable dt)
        {
            _Name = name;
            _OrderId = orderId;
            _Comment = null;
            _DataTable = dt;
        }

        #endregion

        /****************************************************** Methods ******************************************************/

        #region Methods



        #endregion

        #region Accessors

        public String Get_Name()
        {
            return _Name;
        }

        public int Get_OrderId()
        {
            return _OrderId;
        }

        public String Get_Comment()
        {
            return _Comment;
        }

        public DataTable Get_Datatable()
        {
            return _DataTable;
        }

        public void Set_Comment(string comment)
        {
            _Comment = comment;
        }

        #endregion
    }
}
