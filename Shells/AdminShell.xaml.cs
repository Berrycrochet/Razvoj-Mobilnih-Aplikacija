using SHM_ver1.Pages.Admin;

namespace SHM_ver1.Shells;

public partial class AdminShell : Shell
{
	public AdminShell()
	{
		InitializeComponent();
        Routing.RegisterRoute(nameof(AdminAddJobPage), typeof(AdminAddJobPage));
        Routing.RegisterRoute(nameof(AdminApplicationsPage), typeof(AdminApplicationsPage));


    }



}