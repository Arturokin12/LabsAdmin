<%@ Page Title="" Language="C#" MasterPageFile="~/LabsAdmin.Master" AutoEventWireup="true" CodeBehind="Perfil.aspx.cs" Inherits="LabsAdminASP.Perfil" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content-header">
        <h1>Labs Administration 1.0
        <small>Perfil de administrador</small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Panel</a></li>
            <li class="active">Perfil</li>
        </ol>
    </section>
    <style>
        #shop {
            background-repeat: repeat-x;
            height: 121px;
            width: 984px;
            margin-left: 20px;
            margin-top: 13px;
            display: inline;
        }

            #shop .content {
                width: 182px; /*328 co je 1/3 - 20margin left*/
                height: 121px;
                line-height: 20px;
                margin-top: 0px;
                margin-left: 9px;
                margin-right: 0px;
                display: inline-block;
                position: relative;
            }

                #shop .content a {
                    position: absolute;
                    bottom: 5px;
                    right: 5px;
                    background: blue;
                    color: #FFF;
                }
    </style>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btGuardarUser" />
        </Triggers>
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="x_panel">
                        <div class="x_title">
                            <h2>Datos personales<small>
                                <asp:Label ID="Num_Lab" runat="server" Text=""></asp:Label>
                            </small></h2>
                            <ul class="nav navbar-right panel_toolbox">
                                <li><a class="collapse-link"><i class="fa fa-chevron-up hidden"></i></a></li>
                                <li><a class="collapse-link" data-toggle="collapse" href="#panelAddPCs"><i class="fa fa-chevron-up"></i></a>
                                </li>
                                <li class="dropdown">
                                    <a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-expanded="false"><i class="fa fa-wrench"></i></a>
                                    <ul class="dropdown-menu" role="menu">
                                        <li>
                                            <asp:LinkButton ID="btActualizar" runat="server">Actualizar</asp:LinkButton>
                                        </li>
                                        <li>
                                            <asp:LinkButton ID="btModalPass" OnClick="btModalPass_Click" runat="server">Establecer contraseña de administrador</asp:LinkButton>
                                        </li>
                                    </ul>
                                </li>
                            </ul>
                            <div class="clearfix"></div>
                        </div>
                        <div class="panel-collapse collapse in" id="panelAddPCs">
                            <div class="row">
                                <div class="col-md-3"></div>
                                <div class="col-lg-6">
                                    <div class="panel panel-default">
                                        <div class="panel-heading">
                                            <h3>Definir datos personales para administrar red local</h3>
                                        </div>
                                        <div class="panel-body">
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <div id="shop">
                                                        <div class="content">
                                                            <asp:Image Width="100%" Height="100%" ID="imgUsuario" runat="server" CssClass="img-thumbnail" />
                                                            <asp:LinkButton ID="btCambiarImagen" OnClick="btCambiarImagen_Click" CssClass="btn btn-info btn-new btn-sm" runat="server">Cambiar imagen <i class="fa fa-image"></i></asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:Label ID="lbid_usuario" runat="server" Text="" CssClass="hidden"></asp:Label>
                                                    <div class="form-group">
                                                        <label>Nombre: </label>
                                                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtNombre" runat="server" ErrorMessage="Campo Requerido"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group">
                                                        <label>Nombre de dominio (usuario): </label>
                                                        <asp:TextBox ID="txtNick" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtNick" runat="server" ErrorMessage="Campo Requerido"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <asp:LinkButton ID="btGuardarUser" Style="float: right" runat="server" OnClick="btGuardarUser_Click" CssClass="btn btn-info btn-new btn-sm">Guardar Datos <i class="fa fa-plus"></i></asp:LinkButton>
                                            </div>
                                            <asp:Label ID="lbRes" runat="server" Text=""></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-md-3"></div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Button ID="btHiddenPopup1" CssClass="hidden" runat="server" Text="" />
    <%-- Modal Editar Usuario --%>
    <div id="modalPopup1" class="modal-dialog animated slideInDown">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" style="display: inline" id="titulo">
                        <asp:Label ID="lbTituloPopup1" Style="display: inline" runat="server" Text="Alerta"></asp:Label></h5>
                    <asp:LinkButton runat="server" ID="LinkButton3" class="close" Style="display: inline" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                    </asp:LinkButton>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <asp:FileUpload ID="FileUpload1" accept=".png,.jpg,.jpeg,.gif" CssClass="form-control" runat="server" />
                        <br />
                        <asp:Button Style="float: right" ID="btSubirImagen" OnClick="btSubirImagen_Click" CssClass="btn btn-info btn-new btn-sm" runat="server" Text="Guardar" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:ModalPopupExtender ID="modalPupoExtenderPopup1" CancelControlID="LinkButton3" TargetControlID="btHiddenPopup1" PopupControlID="modalPopup1" runat="server"></asp:ModalPopupExtender>
    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Button ID="btHiddenPopup2" CssClass="hidden" runat="server" Text="" />
            <%-- Modal Editar Usuario --%>
            <div id="modalPopup2" class="modal-dialog animated slideInDown">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" style="display: inline" id="titulo2">
                                <asp:Label ID="Label1" Style="display: inline" runat="server" Text="Establecer Contraseña"></asp:Label></h5>
                            <asp:LinkButton runat="server" ID="LinkButton1" class="close" Style="display: inline" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </asp:LinkButton>
                        </div>
                        <div class="modal-body">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="form-group">
                                        <label>Contraseña: </label>
                                        <asp:TextBox ID="txtPass" TextMode="Password" CssClass="form-control" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPass" ErrorMessage="Campo Requerido"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group">
                                        <label>Confirme Contraseña: </label>
                                        <asp:TextBox ID="txtPass2" TextMode="Password" CssClass="form-control" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtPass2" ErrorMessage="Campo Requerido"></asp:RequiredFieldValidator>
                                    </div>
                                    <asp:Label ID="lbResPass" runat="server" Text=""></asp:Label>
                                    <asp:Button ID="btPass" Style="float: right" OnClick="btPass_Click" CssClass="btn btn-info btn-new btn-sm" runat="server" Text="Establecer contraseña" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
            <asp:ModalPopupExtender ID="ModalPopupCambiarPass" CancelControlID="LinkButton1" TargetControlID="btHiddenPopup2" PopupControlID="modalPopup2" runat="server"></asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
