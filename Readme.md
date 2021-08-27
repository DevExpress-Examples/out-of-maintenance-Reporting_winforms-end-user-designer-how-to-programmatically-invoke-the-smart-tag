<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128605007/14.2.3%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T317195)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [Form1.cs](./CS/WindowsFormsApplication1/Form1.cs) (VB: [Form1.vb](./VB/WindowsFormsApplication1/Form1.vb))
* [XtraReport1.cs](./CS/WindowsFormsApplication1/XtraReport1.cs) (VB: [XtraReport1.vb](./VB/WindowsFormsApplication1/XtraReport1.vb))
<!-- default file list end -->
# WinForms End-User Designer - How to programmatically invoke the smart tag once an XRControl is added to a report


This example illustrates how to immediately invoke a control's smart tag once it is placed onto the XtraReport design surface (in the <strong>End-User Designer</strong>). The main idea is to handle the <strong>XRDesignPanel.ComponentAdded</strong> event and call the postponed <strong>XRSmartTagService.ShowPopup</strong> method in the event handler.<br><br>See also:<br><a href="https://www.devexpress.com/Support/Center/p/T230369">End-User Designer - How to customize the control's Smart Tag menu</a>

<br/>


