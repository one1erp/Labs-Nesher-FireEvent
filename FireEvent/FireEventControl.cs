using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Common;
using DAL;
using LSExtensionWindowLib;
using LSSERVICEPROVIDERLib;
//using Oracle.DataAccess.Client;
using MSXML;
using Oracle.DataAccess.Client;

namespace FireEvent
{

    [ComVisible(true)]
    [ProgId("NautilusExtensions.FireEvent")]

    public partial class FireEventControl : UserControl, IExtensionWindow
    {


        #region Ctor
        public FireEventControl()
        {
            try
            {


                InitializeComponent();
                BackColor = Color.FromName("Control");
            }
            catch (Exception e)
            {
                Logger.WriteLogFile(e);

            }
        }
        #endregion

        #region Private members

        private IExtensionWindowSite _ntlsSite;

        private INautilusProcessXML _processXml;

        private INautilusServiceProvider _sp;

        private OracleConnection _connection;

        private string _tableName;

        private string _eventName;

        private string _barcodeField;

        private string _whereClause;

        private string _titleName;

        private string _displayFields;

        private Dictionary<string, string> _entityIcons;

        private ImageList _smallImagesList;

        #endregion

        #region Implementing IExtensionWindow
        //public OracleConnection GetAdoConnection()
        //{
        //    //Create the connection
        //    OracleConnection connection = new OracleConnection(_adoConString);

        //    // Open the connection
        //    connection.Open();

        //    // Get lims user password
        //    string limsUserPassword = _nautilusDbConnection.GetLimsUserPwd();

        //    // Set role lims user
        //    string roleCommand;
        //    if (limsUserPassword == "")
        //    {
        //        // LIMS_USER is not password protected
        //        roleCommand = "set role lims_user";
        //    }
        //    else
        //    {
        //        // LIMS_USER is password protected.
        //        roleCommand = "set role lims_user identified by " + limsUserPassword;
        //    }

        //    // set the Oracle user for this connecition
        //    OracleCommand command = new OracleCommand(roleCommand, connection);

        //    // Try/Catch block
        //    try
        //    {
        //        // Execute the command
        //        command.ExecuteNonQuery();
        //    }
        //    catch (Exception f)
        //    {
        //        // Throw the exception
        //        throw new Exception("Inconsistent role Security : " + f.Message);
        //    }

        //    // Get the session id
        //    double sessionId = _nautilusDbConnection.GetSessionId();

        //    // Connect to the same session
        //    string sSql = string.Format("call lims.lims_env.connect_same_session({0})", sessionId);

        //    // Build the command
        //    command = new OracleCommand(sSql, connection);

        //    // Execute the command
        //    command.ExecuteNonQuery();

        //    return connection;
        //}

        public void PreDisplay()
        {

            INautilusDBConnection dbConnection = Utils.GetNtlsCon(_sp);
            Utils.CreateConstring(dbConnection);
            var dal = new DataLayer();
            dal.Connect();
            _connection = dal.GetOracleConnection();
            // Get the connection
            //   = General.Connections.GetConnection(dbConnection);

        }

        public void SetParameters(string parameters)
        {

            try
            {


                if (listViewEntities.Columns.Count <= 0)//first time
                {
                    int index = 0;
                    var splitedParameters = parameters.Split(';');
                    this._tableName = splitedParameters[index++];
                    this._barcodeField = splitedParameters[index++];
                    this._displayFields = splitedParameters[index++];
                    this._whereClause = splitedParameters[index++];
                    this._eventName = splitedParameters[index++];
                    this._titleName = splitedParameters[index++];

                    InitControls();
                    LoadPictures();
                }
            }
            catch (Exception e)
            {

                Logger.WriteLogFile(e);
            }
        }

        public bool CloseQuery()
        {
            if (_connection != null) _connection.Close();
            return true;
        }

        public void Internationalise()
        {
        }

        public void SetSite(object site)
        {
            _ntlsSite = (IExtensionWindowSite)site;
            _ntlsSite.SetWindowInternalName("TEST");
            _ntlsSite.SetWindowRegistryName("TEST");
            _ntlsSite.SetWindowTitle("Fire Event");

        }

        public WindowButtonsType GetButtons()
        {
            return LSExtensionWindowLib.WindowButtonsType.windowButtonsNone;
        }

        public bool SaveData()
        {
            return false;
        }

        public void SaveSettings(int hKey)
        {
        }

        public void Setup()
        {
        }

        public void refresh()
        {

        }

        public WindowRefreshType DataChange()
        {
            return LSExtensionWindowLib.WindowRefreshType.windowRefreshNone;
        }

        public WindowRefreshType ViewRefresh()
        {
            return LSExtensionWindowLib.WindowRefreshType.windowRefreshNone;
        }

        public void SetServiceProvider(object serviceProvider)
        {
            _sp = serviceProvider as NautilusServiceProvider;

            _processXml = Utils.GetXmlProcessor(_sp);


        }

        public void RestoreSettings(int hKey)
        {

        }

        #endregion

        #region Events

        public int imageIndex = 0;

        private void texEditEntity_KeyPress(object sender, KeyPressEventArgs e)
        {
            string sql = "";
            try
            {



                if (e.KeyChar == (char)13 && txtEditEntity.Text != "")//Enter
                {
                    //Checks if it's already in list view
                    if (!ListViewContains())
                    {


                        //Build query
                        if (!string.IsNullOrEmpty(txtEditEntity.Text))
                        {
                            sql = "select  " + _barcodeField + ",";

                            if (!string.IsNullOrEmpty(_displayFields))
                                sql += _displayFields + ",";

                            sql += "Status  from " + _tableName
                                + " where " + _barcodeField + " = '" + txtEditEntity.Text + "'";
                        }

                        //Add condition to query
                        if (!string.IsNullOrEmpty(_whereClause))
                        {
                            sql += " and " + _whereClause;
                        }

                        OracleCommand cmd = new OracleCommand(sql, _connection);
                        OracleDataReader reader = cmd.ExecuteReader();

                        //Checks if it exists
                        if (!reader.HasRows)
                        {
                            MessageBox.Show(_tableName + "  " + txtEditEntity.Text + " does not exist!", "Nautilus", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        }
                        else
                        {

                            ListViewItem li = null;
                            while (reader.Read())
                            {

                                for (int i = 0; i < reader.FieldCount; i++)
                                {

                                    //First time
                                    if (i == 0)
                                    {
                                        li = new ListViewItem(reader[i].ToString(), imageIndex++);
                                    }
                                    else
                                    {
                                        var obj = reader[i];
                                        li.SubItems.Add(obj.ToString());
                                    }
                                }

                                listViewEntities.Items.Add(li);
                                txtEnterdEntity.Text = txtEditEntity.Text;
                                txtEditEntity.Text = string.Empty;
                                var path = _entityIcons[reader["Status"].ToString()];
                                //Add icon
                                _smallImagesList.Images.Add(Bitmap.FromFile(path));
                                listViewEntities.SmallImageList = _smallImagesList; ;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show(_tableName + " " + txtEditEntity.Text + " is already exists");
                    }
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show("Error" + e1.Message);
                Logger.WriteLogFile(sql, false);
                Logger.WriteLogFile(e1);
            }

        }

        private void listViewEntities_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)//Delete
            {
                //Remove all selected rows
                foreach (ListViewItem item in listViewEntities.SelectedItems)
                {
                    listViewEntities.Items.Remove(item);
                }
            }
        }


        private bool ListViewContains()
        {
            foreach (ListViewItem item in listViewEntities.Items)
            {
                if (item.SubItems[0].Text == txtEditEntity.Text)
                    return true;
            }
            return false;
        }

        private void Ok_button_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (ListViewItem item in listViewEntities.Items)
                {
                    //gets entity name
                    var entityName = item.SubItems[0].Text;
                    //run event
                    RunEvent(entityName);

                }
                //Empties the list
                foreach (ListViewItem item in listViewEntities.Items)
                {
                    listViewEntities.Items.Remove(item);
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show("Error" + e1.Message);

                Logger.WriteLogFile(e1);
            }
        }

        private void Close_button_Click(object sender, EventArgs e)
        {
            if (listViewEntities.Items.Count > 0)
            {
                DialogResult dialogResult = MessageBox.Show("האם אתה בטוח שברצונך לצאת ממסך זה ללא אישור? ", "יציאה", MessageBoxButtons.YesNoCancel);
                if (dialogResult == DialogResult.Yes)
                {
                    listViewEntities = null;
                    _ntlsSite.CloseWindow();
                }
            }
            else
            {
                listViewEntities = null;
                _ntlsSite.CloseWindow();
            }
        }

        #endregion

        #region Private methods

        private void InitControls()
        {

            //Set title
            lblTitle.Text = this._titleName;

            // Add barcodeField column
            listViewEntities.Columns.Add(_barcodeField, _barcodeField, -1, HorizontalAlignment.Left, 0);
            //Add other columns
            var columns = _displayFields.Split(',');
            foreach (var item in columns)
            {
                listViewEntities.Columns.Add(item, item, -2, HorizontalAlignment.Left, 0);

            }
            //Initilaizes list images
            _smallImagesList = new ImageList();



        }

        private void LoadPictures()
        {


            var path = "E://Program Files//Thermo//Nautilus//Resource//";
            path = @"C:\Program Files (x86)\Thermo\Nautilus\Resource\";
            _entityIcons = new Dictionary<string, string>();
            _entityIcons.Add("not status", _tableName + ".ico");
            _entityIcons.Add("A", path + _tableName + "a" + ".ico");
            _entityIcons.Add("C", path + _tableName + "c" + ".ico");
            _entityIcons.Add("P", path + _tableName + "p" + ".ico");
            _entityIcons.Add("I", path + _tableName + "i" + ".ico");
            _entityIcons.Add("R", path + _tableName + "r" + ".ico");
            _entityIcons.Add("S", path + _tableName + "s" + ".ico");
            _entityIcons.Add("U", path + _tableName + "u" + ".ico");
            _entityIcons.Add("V", path + _tableName + "v" + ".ico");
            _entityIcons.Add("X", path + _tableName + "x" + ".ico");

        }

        private void RunEvent(string entityName)
        {


            //Creates fire event xml
            var doc = Create_XML(entityName);

            //creates object for respone
            var res = new DOMDocument();

            _processXml.ProcessXMLWithResponse(doc, res);

            res.save(@"C:\temp\resENTITY.xml");

            doc.save(@"C:\temp\docENTITY.xml");
        }

        private DOMDocument Create_XML(string entityName)
        {
            DOMDocument objDom;


            objDom = new DOMDocument();

            //Creates lims request element
            var objLimsElem = objDom.createElement("lims-request");
            objDom.appendChild(objLimsElem);

            // Creates login request element
            var objLoginElem = objDom.createElement("login-request");
            objLimsElem.appendChild(objLoginElem);

            // Creates Entity element
            var objEntityElem = objDom.createElement(_tableName);
            objLoginElem.appendChild(objEntityElem);


            // Creates   find-by-name element
            var objFindByNameElem = objDom.createElement("find-by-name");
            objEntityElem.appendChild(objFindByNameElem);
            objFindByNameElem.text = entityName;


            //Creates fire-event element
            var objFireEvent = objDom.createElement("fire-event");
            objEntityElem.appendChild(objFireEvent);
            objFireEvent.text = _eventName;

            return objDom;
        }






        #endregion




    }


}














