using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraReports.Design;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.UserDesigner;
using DevExpress.XtraReports.UserDesigner.Native;

namespace WindowsFormsApplication1 {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            XtraReport1 report = new XtraReport1();
            ReportDesignTool designTool = new ReportDesignTool(report);
            designTool.DesignForm.DesignMdiController.DesignPanelLoaded += DesignMdiController_DesignPanelLoaded;
            designTool.ShowDesignerDialog();
        }

        void DesignMdiController_DesignPanelLoaded(object sender, DevExpress.XtraReports.UserDesigner.DesignerLoadedEventArgs e) {
            XRDesignPanel panel = sender as XRDesignPanel;
            panel.ComponentAdded += panel_ComponentAdded;
        }

        void panel_ComponentAdded(object sender, System.ComponentModel.Design.ComponentEventArgs e) {
            XRDesignPanel panel = sender as XRDesignPanel;
            if(checkedListBoxControl1.Items.Contains(e.Component.GetType().Name) && checkedListBoxControl1.Items[e.Component.GetType().Name].CheckState == CheckState.Checked)
                panel.BeginInvoke(new SmartTagInvoker(ShowSmartTag), new object[] { panel, e.Component });
        }

        public void ShowSmartTag(XRDesignPanel panel, IComponent component) {
            XRDesignerHost host = panel.GetService(typeof(IDesignerHost)) as XRDesignerHost;
            XRControlDesigner designer = host.GetDesigner(component) as XRControlDesigner;

            if(designer == null)
                return;

            XRSmartTagService tagSvc = panel.GetService(typeof(XRSmartTagService)) as XRSmartTagService;
            IBandViewInfoService bandSvc = panel.GetService(typeof(IBandViewInfoService)) as IBandViewInfoService;
            RectangleF bounds = bandSvc.GetControlScreenBounds((XRControl)component);


            tagSvc.ShowPopup(new Point((int)bounds.Right, (int)bounds.Top - 15), designer, null);
        }

        public delegate void SmartTagInvoker(XRDesignPanel panel, IComponent component);

        private void Form1_Load(object sender, EventArgs e) {
            List<Type> types = typeof(XtraReport).Assembly.GetTypes().Where(t => t.Namespace == typeof(XtraReport).Namespace).ToList();
            foreach(Type type in types) {
                if(type.IsSubclassOf(typeof(XRControl))) {
                    object[] attributes = type.GetCustomAttributes(typeof(ToolboxItemAttribute), true);
                    if(attributes != null && attributes.Length != 0 && (attributes[0] as ToolboxItemAttribute).ToolboxItemType != null)
                        checkedListBoxControl1.Items.Add(type.Name);
                }
            }
            checkedListBoxControl1.CheckAll();
        }
    }
}
