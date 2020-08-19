using System;
using System.Linq;
using System.Collections.Generic;
using DevExpress.XtraEditors;
using DevExpress.Utils.MVVM;
using DevExpress.Utils.MVVM.Services;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Base;
using MMPPL;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using MMPPL.Report;
using DevExpress.XtraReports.UI;

namespace MMPPL.Views.LabelPrintingView
{
    public partial class LabelPrintingView : XtraUserControl
    {
        SerialPortManager _spManager;
        public LabelPrintingView()
        {
            InitializeComponent();


            if (!mvvmContext.IsDesignMode)
                InitBindings();
        }
        private void UserInitialization()
        {
            _spManager = new SerialPortManager();
            SerialSettings mySerialSettings = _spManager.CurrentSerialSettings;
            serialSettingsBindingSource.DataSource = mySerialSettings;
            portNameComboBox.DataSource = mySerialSettings.PortNameCollection;
            baudRateComboBox.DataSource = mySerialSettings.BaudRateCollection;
            dataBitsComboBox.DataSource = mySerialSettings.DataBitsCollection;
            parityComboBox.DataSource = Enum.GetValues(typeof(System.IO.Ports.Parity));
            stopBitsComboBox.DataSource = Enum.GetValues(typeof(System.IO.Ports.StopBits));

            mySerialSettings.PortName = "COM4";
            mySerialSettings.BaudRate = 9600;
            mySerialSettings.DataBits = 8;
            mySerialSettings.Parity = System.IO.Ports.Parity.None;
            mySerialSettings.StopBits = System.IO.Ports.StopBits.One;
            _spManager.NewSerialDataRecieved += new EventHandler<SerialDataEventArgs>(_spManager_NewSerialDataRecieved);

        }


        void _spManager_NewSerialDataRecieved(object sender, SerialDataEventArgs e)
        {
            if (this.InvokeRequired)
            {
                // Using this.Invoke causes deadlock when closing serial port, and BeginInvoke is good practice anyway.
                this.BeginInvoke(new EventHandler<SerialDataEventArgs>(_spManager_NewSerialDataRecieved), new object[] { sender, e });
                return;
            }


            // This application is connected to a GPS sending ASCCI characters, so data is converted to text
            string str = Encoding.ASCII.GetString(e.Data);
            // MessageBox.Show(str);
            GrossWeightTextEdit.Text = String.Format("{0:0.000}", Convert.ToDouble(str));
            GrossWeightTextEdit.ScrollToCaret();

        }



        void InitBindings()
        {
            var fluentAPI = mvvmContext.OfType<MMPPL.ViewModels.LabelPrintingViewModel>();
            fluentAPI.WithEvent(this, "Load").EventToCommand(x => x.OnLoaded());
            fluentAPI.SetObjectDataSourceBinding(
                labelPrintingViewBindingSource, x => x.Entity, x => x.Update());
            // Binding for ColorMaster LookUp editor
            fluentAPI.SetBinding(ColorMasterLookUpEdit.Properties, p => p.DataSource, x => x.LookUpColorMasters.Entities);
            // Binding for Dispatch LookUp editor
            fluentAPI.SetBinding(DispatchLookUpEdit.Properties, p => p.DataSource, x => x.LookUpDispatches.Entities);
            // Binding for GradeMaster LookUp editor
            fluentAPI.SetBinding(GradeMasterLookUpEdit.Properties, p => p.DataSource, x => x.LookUpGradeMasters.Entities);
            // Binding for MicronMaster LookUp editor
            fluentAPI.SetBinding(MicronMasterLookUpEdit.Properties, p => p.DataSource, x => x.LookUpMicronMasters.Entities);
            // Binding for SizeMaster LookUp editor
            fluentAPI.SetBinding(SizeMasterLookUpEdit.Properties, p => p.DataSource, x => x.LookUpSizeMasters.Entities);
            UserInitialization();
            bbiCustomize.ItemClick += (s, e) => { dataLayoutControl1.ShowCustomizationForm(); };
        }

        private void LabelPrintingView_VisibleChanged(object sender, EventArgs e)
        {
            _spManager.Dispose();
        }



        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string curBatch = SRNOTextEdit.Text;
            bbiSaveAndNew.PerformClick();
            MMPPL.Report.LabelPrintCall.PrintReprt(curBatch);
        }

        private void btnStart_Click_1(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            btnStop.Enabled = true;
            _spManager.StartListening();

            // MessageBox.Show("Start : " + _spManager.CurrentSerialSettings.PortName + ":" + _spManager.CurrentSerialSettings.BaudRate + ":" + _spManager.CurrentSerialSettings.DataBits + ":" + _spManager.CurrentSerialSettings.Parity + ":" + _spManager.CurrentSerialSettings.StopBits );
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = true;
            btnStop.Enabled = false;
            _spManager.StopListening();
        }

        private void btmCustPrint_Click(object sender, EventArgs e)
        {
            string curBatch = SRNOTextEdit.Text;
            bbiSaveAndNew.PerformClick();
            MMPPL.Report.LabelPrintCall.PrintReprt(curBatch);
        }
    }
}
