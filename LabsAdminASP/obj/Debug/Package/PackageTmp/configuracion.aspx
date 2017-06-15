<%@ Page Title="" Language="C#" MasterPageFile="~/LabsAdmin.Master" AutoEventWireup="true" CodeBehind="configuracion.aspx.cs" Inherits="LabsAdminASP.configuracion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content-header">
        <h1>Labs Administration 1.0
        <small>Panel de configuraciónes</small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Panel</a></li>
            <li class="active">Configuración</li>
        </ol>
    </section>
    <div class="row">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="col-lg-5">
                    <div class="panel panel-default">
                        <div class="panel panel-heading">
                            <h3>Establecer dominio Windows</h3>
                        </div>
                        <div class="panel-body">
                            <div class="form-group">
                                <label>Nombre Dominio (EJ: Labsadmin.cl): </label>
                                <asp:TextBox ID="txtDominio" CssClass="form-control" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtDominio" runat="server" ErrorMessage="Campo Requerido"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group">
                                <label>Nombre Pc Dominio: </label>
                                <asp:TextBox ID="txtNombreDominio" CssClass="form-control" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtNombreDominio" runat="server" ErrorMessage="Campo Requerido"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group">
                                <label>IPV4 Dominio: </label>
                                <asp:TextBox ID="txtIPDominio" CssClass="form-control" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtIPDominio" runat="server" ErrorMessage="Campo Requerido"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group form-inline">
                                
                                <asp:UpdatePanel ID="UpdatePanelSwitch1" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <label>Utilizar dominio para obtener ip's y Mac's: </label>
                                        <div class="btn-group" style="display:inline">
                                            <asp:Button ID="btNoDom" OnClick="btNoDom_Click" CssClass="btn btn-info" runat="server" Text="No" />
                                            <asp:Button ID="btYesDom" OnClick="btYesDom_Click" CssClass="btn btn-danger" runat="server" Text="Si" />
                                        </div>
                                        <i style="margin-left: 20px; display: inline" data-toggle="tooltip" data-placement="top" title="Ésta función es para topologías de red que tengan, por ejemplo, división de Vlan's, por lo que las dos redes no se comunican entre sí directamente, de esta manera las funciones de obtener las MAC's y las IP's no dan resultado, activar esta funcion utiliza la IP del dominio para obtener estos datos desde esa estación." class="fa fa-question-circle" aria-hidden="true"></i>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <br />
                            <asp:Label ID="lbResDom" runat="server" Text=""></asp:Label>
                            <asp:LinkButton ID="btGuardarDominio" CssClass="btn btn-info btn-new btn-sm" runat="server" Style="float: right" OnClick="btGuardarDominio_Click">Guardar Datos</asp:LinkButton>
                        </div>
                    </div>
                </div>
                <div class="col-md-2"></div>
                <div class="col-md-5">
                    <div class="panel panel-default">
                        <div class="panel panel-heading">
                            <h3>Usuario Administrador</h3>
                        </div>
                        <div class="panel-body">
                            <div class="form-group">
                                <asp:UpdatePanel ID="UpdatePanelSwitch2" runat="server" UpdateMode="Conditional">
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btGuardarUsuarioDom"/>
                                    </Triggers>
                                    <ContentTemplate>
                                        <label style="display:inline">Utilizar Usuario para ejecutar tareas: </label>
                                        <div class="btn-group">
                                            <asp:Button ID="btNoUser" OnClick="btNoUser_Click" CssClass="btn btn-info" runat="server" Text="No" />
                                            <asp:Button ID="btSiUser" OnClick="btSiUser_Click" CssClass="btn btn-danger" runat="server" Text="Si" />
                                        </div>
                                        <i style="margin-left: 20px; display: inline" data-toggle="tooltip" data-placement="top" title="Activar esta función no utilizará su usuario y contraseña personal para ejecutar las tareas, sino el aquí especificado, éste usuario debe ser administrador de dominio y de cada pc para poder ejecutar cada una de las funciones." class="fa fa-question-circle" aria-hidden="true"></i>
                                        <br />
                                        <br />
                                        <br />
                                        <asp:Panel ID="panelUsuario" runat="server">
                                            <div class="form-group">
                                                <label>Usuario</label>
                                                <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtUsuario" runat="server" ErrorMessage="Campo Requerido"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group">
                                                <label>Contraseña:</label>
                                                <asp:TextBox ID="txtPass" TextMode="Password" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtPass" runat="server" ErrorMessage="Campo Requerido"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group">
                                                <label>Confirme contraseña: </label>
                                                <asp:TextBox ID="txtPass2" TextMode="Password" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="txtPass2" runat="server" ErrorMessage="Campo Requerido"></asp:RequiredFieldValidator>
                                            </div>
                                            <asp:Label ID="lbRes2" runat="server" Text="" style="float:left"></asp:Label>
                                            <asp:LinkButton style="float:right" ID="btGuardarUsuarioDom" OnClick="btGuardarUsuarioDom_Click" CssClass="btn btn-info btn-new btn-sm" runat="server">Guardar Datos <i class="fa fa-floppy-o"></i></asp:LinkButton>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
