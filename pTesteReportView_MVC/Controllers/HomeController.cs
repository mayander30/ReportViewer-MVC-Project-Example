using Microsoft.Reporting.WebForms;
using pTesteReportView_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;

namespace pTesteReportView_MVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RDados(System.Web.Mvc.FormCollection form)
        {
            ViewBag.Title_Relatorio = "Dados da Lista";

            var reportViewer = ConfigReportViewer();
            
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"\Reports\rDados.rdlc";

            System.Windows.Forms.BindingSource source = new BindingSource();
            source.DataSource = new DadosReport().getLstDados();

            ReportDataSource datasource = new ReportDataSource("Dados", source);
            reportViewer.LocalReport.DataSources.Add(datasource);

            ViewBag.ReportViewer = reportViewer;

            return View("~/Views/Report/ViewReport.cshtml");
        }

        public static Microsoft.Reporting.WebForms.ReportViewer
            ConfigReportViewer()
        {
            var reportViewer = new Microsoft.Reporting.WebForms.ReportViewer();
            reportViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;

            reportViewer.ViewStateMode = System.Web.UI.ViewStateMode.Inherit;
            reportViewer.SizeToReportContent = true;
            reportViewer.Width = System.Web.UI.WebControls.Unit.Percentage(110);
            reportViewer.Height = System.Web.UI.WebControls.Unit.Percentage(100);
            reportViewer.AsyncRendering = true;
            reportViewer.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.FullPage;
            reportViewer.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFBF7");
            reportViewer.ShowPrintButton = true;
            reportViewer.ShowRefreshButton = false;

            reportViewer.ShowZoomControl = true;

            PermissionSet permissions = new PermissionSet(PermissionState.None);
            permissions.AddPermission(new FileIOPermission(PermissionState.Unrestricted));
            permissions.AddPermission(new SecurityPermission(SecurityPermissionFlag.Execution));

            reportViewer.LocalReport.SetBasePermissionsForSandboxAppDomain(permissions);

            Assembly asm = Assembly.Load(Assembly.GetExecutingAssembly().FullName);
            AssemblyName asm_name = asm.GetName();
            reportViewer.LocalReport.AddFullTrustModuleInSandboxAppDomain(new StrongName(new StrongNamePublicKeyBlob(asm_name.GetPublicKeyToken()), asm_name.Name, asm_name.Version));

            return reportViewer;

        }
    }
}