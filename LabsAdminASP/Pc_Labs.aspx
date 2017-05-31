<%@ Page Title="" Language="C#" MasterPageFile="~/LabsAdmin.Master" AutoEventWireup="true" CodeBehind="Pc_Labs.aspx.cs" Async="true" Inherits="LabsAdminASP.Pc_Labs" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <div class="row">
                <asp:Label ID="lbid_usuario" runat="server" Text=""></asp:Label>
                <div class="col-md-12">
                    <div class="x_panel">
                        <div class="x_title">
                            <h2>Laboratorio <small>
                                <asp:Label ID="Num_Lab" runat="server" Text="Label"></asp:Label>
                            </small></h2>
                            <ul class="nav navbar-right panel_toolbox">
                                <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                                </li>
                                <li class="dropdown">
                                    <a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-expanded="false"><i class="fa fa-wrench"></i></a>
                                    <ul class="dropdown-menu" role="menu">
                                        <li><a href="#">Settings 1</a>
                                        </li>
                                        <li><a href="#">Settings 2</a>
                                        </li>
                                    </ul>
                                </li>
                                <li><a class="close-link"><i class="fa fa-close"></i></a>
                                </li>
                            </ul>
                            <div class="clearfix"></div>
                        </div>
                        <div class="x_content">

                            <div class="row" runat="server" id="PCs">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <asp:Button ID="btHiddenMensaje" CssClass="hidden" runat="server" Text="Button" />
            <%-- Modal Editar Usuario --%>
            <div id="modalPupoMensaje" class="modal-dialog animated slideInDown">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" style="display: inline" id="titulo">
                                <asp:Label ID="lbTituloMensaje" Style="display: inline" runat="server" Text="Alerta"></asp:Label></h5>
                            <asp:LinkButton runat="server" ID="btCancelMensaje" class="close" Style="display: inline" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </asp:LinkButton>
                        </div>
                        <div class="modal-body">
                            <div class="row container" style="margin-left:10px">
                                <p>
                                    <asp:Label ID="lbMensaje" runat="server" Text=""></asp:Label>
                                </p>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btOkMensaje" runat="server" Text="Ok" UseSubmitBehavior="false" CssClass="btn btn-info" />
                        </div>
                    </div>
                </div>
            </div>
            <asp:ModalPopupExtender ID="modalPupoExtenderMensaje" OkControlID="btOkMensaje" CancelControlID="btCancelMensaje" TargetControlID="btHiddenMensaje" PopupControlID="modalPupoMensaje" runat="server"></asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
