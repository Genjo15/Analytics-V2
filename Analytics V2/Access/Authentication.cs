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
    public partial class Authentication : UserControl
    {
        /***************************************************** Variables *****************************************************/

        #region Variables

        private string  _AccessType;
        private Boolean _NetworkAvailable; 

        #endregion

        /**************************************************** Constructor ****************************************************/

        #region Constructor

        public Authentication(string accessType)
        {
            InitializeComponent();
            _AccessType = accessType;
            _NetworkAvailable = false;
        }

        #endregion

        /****************************************************** Methods ******************************************************/

        #region Methods

        /*******************************************************\
         * Authenticate user (simple mode):                    *
         *   - Try to get access_type regarding login/password *
         *        |-> If ok, update access type & return true  *
         *        |-> if not, return false                     *
        \*******************************************************/

        public Boolean Authenticate()
        {
            try
            {
                AnalyticsWebService.AnalyticsSoapClient service = new AnalyticsWebService.AnalyticsSoapClient();

                if (LoginTextBox.Text.Equals("") || PasswordTextBox.Text.Equals("") || LoginTextBox.Text.Contains(" ") || PasswordTextBox.Text.Contains(" "))
                {
                    var result = KryptonMessageBox.Show("Incorrect Login / Password", "Error",
                             MessageBoxButtons.OK,
                             MessageBoxIcon.Stop);
                }

                else
                {
                    string access_type = null;

                    access_type = service.Authenticate(LoginTextBox.Text, PasswordTextBox.Text);
                    if (!access_type.Equals(""))
                    {
                        _AccessType = access_type;
                        service.Close();
                        return true;
                    }

                    else
                    {
                        var result = KryptonMessageBox.Show("Incorrect Login / Password", "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Stop);
                        service.Close();
                        return false;
                    }
                }
            }

            catch (Exception ex)
            {
                var result = KryptonMessageBox.Show("No connection found, please ensure that your connection is functionning.", "Connection Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Stop);
                return false;
            }

            return false;
        }

        /*****************************************************************************\
         * Authenticate user (hardware lock mode):                                   *
         *   - 1st verify if login/password is correct                               *
         *   - If correct, then try to insert new user (with PUC and access_type_id) *
         *         |-> If user already exists, update access type                    *
         *   - If not, return false.                                                 *
        \*****************************************************************************/

        public Boolean AuthenticateAndRemember()
        {
            try
            {
                AnalyticsWebService.AnalyticsSoapClient service = new AnalyticsWebService.AnalyticsSoapClient();

                if (LoginTextBox.Text.Equals("") || PasswordTextBox.Text.Equals("") || LoginTextBox.Text.Contains(" ") || PasswordTextBox.Text.Contains(" "))
                {
                    var result = KryptonMessageBox.Show("Incorrect Login / Password", "Error",
                             MessageBoxButtons.OK,
                             MessageBoxIcon.Stop);
                }

                else
                {
                    string access_type = null;

                    int accessTypeId = service.Get_password_id(LoginTextBox.Text, PasswordTextBox.Text);

                    if (accessTypeId != 666)
                    {
                        Boolean inserted = false;

                        string computer = System.Environment.MachineName;
                        inserted = service.Insert_user(computer, accessTypeId);

                        if (!inserted)
                            service.Update_access_type_id(computer, accessTypeId);

                        access_type = service.Authenticate(LoginTextBox.Text, PasswordTextBox.Text);
                        if (!access_type.Equals(""))
                        {
                            _AccessType = access_type;
                            service.Close();
                            return true;
                        }
                    }

                    else
                    {
                        var result = KryptonMessageBox.Show("Incorrect Login / Password", "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Stop);
                        service.Close();
                        return false;
                    }
                }
            }

            catch (Exception ex)
            {
                var result = KryptonMessageBox.Show("No connection found, please ensure that your connection is functionning.", "Connection Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Stop);
                return false;
            }

            return false;
        }

        /******************************************************\
         * Check if PUC exists in DB (case of hardware lock): *
         * Check also if connection available                 *
        \******************************************************/

        public void CheckSavedPUC(string PUC)
        {
            try
            {
                AnalyticsWebService.AnalyticsSoapClient service = new AnalyticsWebService.AnalyticsSoapClient();
                _AccessType = service.Get_access_type(PUC);
                if (_AccessType.Equals(""))
                    _AccessType = "user";
                service.Close();
                _NetworkAvailable = true;
            }

            catch (Exception ex)
            {
                _NetworkAvailable = false;
                _AccessType = "user";
            }
        }


        /******************************************************************\
         * Check if user has the right to perform some action :           *
         *                                                                *
         *                               |  USER  |  ADMIN  |  SUPERADMIN *
         *                               ----------------------------------
         *  Launch Config in Prod        |  YES   |   YES   |     YES     *
         *  Launch Config in PreProd     |  NO    |   YES   |     YES     * 
         *  Create Config (P/PP)         |  NO    |   YES   |     YES     * 
         *  Edit/Save Config (P/PP)      |  NO    |   YES   |     YES     * 
         *  Suppress Config (P/PP)       |  NO    |   YES   |     YES     * 
         *  Move Config (P/PP)           |  NO    |   YES   |     YES     * 
         *  Rename Config (P/PP)         |  NO    |   YES   |     YES     * 
         *  Copy/Cut/Paste Config (P/PP) |  NO    |   YES   |     YES     * 
         *  --------------------------------------------------------------
         *  Create Folder (P/PP)         |  NO    |   YES   |     YES     * 
         *  Suppress Folder (P/PP)       |  NO    |   YES   |     YES     * 
         *  Move Folder (P/PP)           |  NO    |   YES   |     YES     * 
         *  Rename Folder (P/PP)         |  NO    |   YES   |     YES     * 
         *  --------------------------------------------------------------
         *  Personnal TreeView                                            *
         *  (all actions listed above    |  YES   |   YES   |     YES     * 
         *  --------------------------------------------------------------
         *  Administration Module        |  NO    |   NO    |     YES     *
         *                                                                *
        \*****************************************************************/


        public Boolean CheckIfAccessGranted(string actionType)
        {
            Boolean accessGranted = false;

            switch (actionType)
            {
                case "newDirectory" :
                case "dragDrop":
                case "newConfig":
                case "rename":
                case "suppress":
                case "edit":
                case "cut":
                case "copy":
                case "launchConfigPreProd" :
                    switch (_AccessType)
                    {
                        case "user": accessGranted = false; break;
                        case "admin": accessGranted = true; break;
                        case "superadmin": accessGranted = true; break;
                    }
                    break;

                default: 
                    accessGranted = false;
                    break;
                         
            }

            if (!accessGranted)
            {
                var result = KryptonMessageBox.Show("Access Denied !!", "Access Denied",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
            }

            return accessGranted;
        }

        #endregion

        #region Accessors

        public string GetAccessType() { return _AccessType; }
        public Boolean GetNetworkAvailable() 
        {
            if (!_NetworkAvailable)
            {
                var result = KryptonMessageBox.Show("No connection found, please ensure that your connection is functionning.", "Connection Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Stop);
            }

            return _NetworkAvailable; 
        }
        public void SetAccessType(string str) { _AccessType = str; }

        #endregion

    }
}
