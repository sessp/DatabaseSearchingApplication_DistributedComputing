Imports System.Web.Mvc
Imports System.Web.Routing
Imports System.Web.Http
Imports Webservice.App_Start
Public Class MvcApplication
    Inherits System.Web.HttpApplication

    Protected Sub Application_Start()
        AreaRegistration.RegisterAllAreas()
        RouteConfig.RegisterRoutes(RouteTable.Routes)
    End Sub
End Class
