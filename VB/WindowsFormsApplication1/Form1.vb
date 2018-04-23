Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.ComponentModel.Design
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.XtraReports.Design
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraReports.UserDesigner
Imports DevExpress.XtraReports.UserDesigner.Native

Namespace WindowsFormsApplication1
	Partial Public Class Form1
		Inherits Form

		Public Sub New()
			InitializeComponent()
		End Sub

		Private Sub button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button1.Click
			Dim report As New XtraReport1()
			Dim designTool As New ReportDesignTool(report)
			AddHandler designTool.DesignForm.DesignMdiController.DesignPanelLoaded, AddressOf DesignMdiController_DesignPanelLoaded
			designTool.ShowDesignerDialog()
		End Sub

		Private Sub DesignMdiController_DesignPanelLoaded(ByVal sender As Object, ByVal e As DevExpress.XtraReports.UserDesigner.DesignerLoadedEventArgs)
			Dim panel As XRDesignPanel = TryCast(sender, XRDesignPanel)
			AddHandler panel.ComponentAdded, AddressOf panel_ComponentAdded
		End Sub

		Private Sub panel_ComponentAdded(ByVal sender As Object, ByVal e As System.ComponentModel.Design.ComponentEventArgs)
			Dim panel As XRDesignPanel = TryCast(sender, XRDesignPanel)
			If checkedListBoxControl1.Items.Contains(DirectCast(e.Component, Object).GetType().Name) AndAlso checkedListBoxControl1.Items(DirectCast(e.Component, Object).GetType().Name).CheckState = CheckState.Checked Then
				panel.BeginInvoke(New SmartTagInvoker(AddressOf ShowSmartTag), New Object() { panel, e.Component })
			End If
		End Sub

		Public Sub ShowSmartTag(ByVal panel As XRDesignPanel, ByVal component As IComponent)
			Dim host As XRDesignerHost = TryCast(panel.GetService(GetType(IDesignerHost)), XRDesignerHost)
			Dim designer As XRControlDesigner = TryCast(host.GetDesigner(component), XRControlDesigner)

			If designer Is Nothing Then
				Return
			End If

			Dim tagSvc As XRSmartTagService = TryCast(panel.GetService(GetType(XRSmartTagService)), XRSmartTagService)
			Dim bandSvc As IBandViewInfoService = TryCast(panel.GetService(GetType(IBandViewInfoService)), IBandViewInfoService)
            Dim bounds_Renamed As RectangleF = bandSvc.GetControlScreenBounds(DirectCast(component, XRControl))


            tagSvc.ShowPopup(New System.Drawing.Point(CInt(Math.Truncate(bounds_Renamed.Right)), CInt(Math.Truncate(bounds_Renamed.Top)) - 15), designer, Nothing)
		End Sub

		Public Delegate Sub SmartTagInvoker(ByVal panel As XRDesignPanel, ByVal component As IComponent)

		Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
			Dim types As List(Of Type) = GetType(XtraReport).Assembly.GetTypes().Where(Function(t) t.Namespace = GetType(XtraReport).Namespace).ToList()
			For Each type As Type In types
				If type.IsSubclassOf(GetType(XRControl)) Then
					Dim attributes() As Object = type.GetCustomAttributes(GetType(ToolboxItemAttribute), True)
					If attributes IsNot Nothing AndAlso attributes.Length <> 0 AndAlso (TryCast(attributes(0), ToolboxItemAttribute)).ToolboxItemType IsNot Nothing Then
						checkedListBoxControl1.Items.Add(type.Name)
					End If
				End If
			Next type
			checkedListBoxControl1.CheckAll()
		End Sub
	End Class
End Namespace
