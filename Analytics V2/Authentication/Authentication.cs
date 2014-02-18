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
    public partial class ConnectionScreen : UserControl
    {
        /***************************************************** Variables *****************************************************/

        #region Variables

        private string _AccessType;

        #endregion

        /**************************************************** Constructor ****************************************************/

        #region Constructor

        public ConnectionScreen(string accessType)
        {
            InitializeComponent();
            _AccessType = accessType;
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
                    {
                        //var result = KryptonMessageBox.Show("PUC already exists", "Warning",
                        // MessageBoxButtons.OK,
                        // MessageBoxIcon.Information);
                        service.Update_access_type_id(computer, accessTypeId);
                    }

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
            return false;
        }

        /******************************************************\
         * Check if PUC exists in DB (case of hardware lock): *
        \******************************************************/

        public void CheckSavedPUC(string PUC)
        {
            AnalyticsWebService.AnalyticsSoapClient service = new AnalyticsWebService.AnalyticsSoapClient();
            _AccessType = service.Get_access_type(PUC);
            if(_AccessType.Equals(""))
                _AccessType = "user";
            service.Close();
        }

        #endregion

        #region Accessors

        public string GetAccessType() { return _AccessType; }
        public void SetAccessType(string str) { _AccessType = str; }

        #endregion

    }
}
