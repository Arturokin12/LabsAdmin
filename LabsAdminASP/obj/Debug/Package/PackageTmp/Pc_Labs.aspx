﻿<%@ Page Title="" Language="C#" MasterPageFile="~/LabsAdmin.Master" AutoEventWireup="true" CodeBehind="Pc_Labs.aspx.cs" Async="true" Inherits="LabsAdminASP.Pc_Labs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="x_panel">
                        <div class="x_title">
                            <asp:Label ID="lbid_usuario" CssClass="hidden" runat="server" Text=""></asp:Label>
                            <h2>Laboratorio <small>
                                <asp:Label ID="Num_Lab" runat="server" Text=""></asp:Label>
                            </small></h2>
                            <ul class="nav navbar-right panel_toolbox">
                                <li><a class="collapse-link" data-toggle="collapse" href="#divArchivos"><i class="fa fa-chevron-up"></i></a>
                                </li>
                                <li class="dropdown">
                                    <a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-expanded="false"><i class="fa fa-wrench"></i></a>
                                    <ul class="dropdown-menu" role="menu">
                                        <asp:LinkButton ID="lnkEncender" OnClick="panelEncenderTodos" CssClass="fa fa-sun-o" runat="server">Encender Laboratorio</asp:LinkButton>
                                        <asp:LinkButton ID="lnkApagar" OnClick="panelApagarTodos" CssClass="fa fa-power-off" runat="server">Apagar Laboratorio</asp:LinkButton>
                                        <asp:LinkButton ID="lnkCopiar" OnClick="CopiarPopuTodos" CssClass="fa fa-scissors" runat="server">Copiar a Laboratorio</asp:LinkButton>
                                    </ul>
                                </li>
                            </ul>
                            <div class="clearfix"></div>
                        </div>
                        <div id="divArchivos" class="panel-collapse collapse in">
                            <div class="row">
                                <div class="col-md-10">
                                    <div class="row" runat="server" id="PCs">
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <asp:LinkButton ID="lnkEncenderSelect" OnClick="panelEncenderSelected" Width="100%" CssClass="btn btn-info btn-new disabled" Enabled="false" runat="server"><i class="fa fa-sun-o"></i> Endender Seleccionados</asp:LinkButton>
                                    <asp:LinkButton ID="lnkApagarSelect" OnClick="panelApagarSelected" Width="100%" CssClass="btn btn-info btn-new disabled" Enabled="false" runat="server"><i class="fa fa-power-off"></i> Apagar Seleccionados</asp:LinkButton>
                                    <asp:LinkButton ID="lnkCopiarSelect" OnClick="CopiarPopuSelected" Width="100%" CssClass="btn btn-info btn-new disabled" Enabled="false" runat="server"><i class="fa fa-scissors"></i> Copiar a Seleccionados</asp:LinkButton>
                                    <asp:LinkButton ID="lnkLimpiarCuentas" OnClick="panelLimpiarCuentasSelected" Width="100%" CssClass="btn btn-info btn-new disabled" Enabled="false" runat="server"><i class="fa fa-eraser"></i> Limpiar Cuentas</asp:LinkButton>
                                </div>
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
                            <div class="row container" style="margin-left: 10px">
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

            <asp:Button ID="bthiddenModalConfirm" CssClass="hidden" runat="server" Text="Button" />
            <%-- Modal Editar Usuario --%>
            <div id="modalpupoConfirmar" class="modal-dialog modal-sm animated slideInDown">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" style="display: inline">
                                <asp:Label ID="Label7" Style="display: inline" runat="server" Text="Confirmar Acción"></asp:Label></h5>
                            <asp:LinkButton runat="server" ID="LinkButton1" class="close" Style="display: inline" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </asp:LinkButton>
                        </div>
                        <div class="modal-body">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <h3>
                                        <asp:Label ID="lbMensajeConfirmar" runat="server" Text="Label"></asp:Label></h3>
                                    <asp:Label ID="lbid_pcAccion" runat="server" Text="" CssClass="hidden"></asp:Label>
                                    <div style="text-align: center">
                                        <asp:Panel ID="panelconIP" runat="server">
                                            <asp:CheckBox ID="chkIP" Text="Obtener ip" runat="server" />
                                        </asp:Panel>
                                        <asp:UpdateProgress ID="updateProgressConfirmar" AssociatedUpdatePanelID="UpdatePanel3" runat="server">
                                            <ProgressTemplate>
                                                <asp:Image ID="Image1" ImageUrl="images/ring-alt.gif" runat="server" />
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                        <asp:Button ID="btConfirmarApagar" OnClick="apagar" CssClass="btn btn-default" runat="server" Text="Si" />
                                        <asp:Button ID="btConfirmarApagarTodos" OnClick="btConfirmarApagarTodos_Click" CssClass="btn btn-default" runat="server" Text="Si" />
                                        <asp:Button ID="btConfirmarApagarSelected" OnClick="ConfirmarApagarSelected" CssClass="btn btn-default" runat="server" Text="Si" />
                                        
                                        <asp:Button ID="btConfirmarLimpiarCuentas" OnClick="confirmarLimpiarCuentas" CssClass="btn btn-default" runat="server" Text="Si" />
                                        <asp:Button ID="btConfirmarLimpiarCuentasTodos" OnClick="confirmarLimpiarCuentasTodos" CssClass="btn btn-default" runat="server" Text="Si" />
                                        <asp:Button ID="btConfirmarLimpiarCuentasSelected" OnClick="LimpiarCuentasSelected" CssClass="btn btn-default" runat="server" Text="Si" />
                                        
                                        <asp:Button ID="btConfirmarEncender" OnClick="encender" CssClass="btn btn-default" runat="server" Text="Si" />
                                        <asp:Button ID="btConfirmarEncenderTodos" OnClick="ConfirmarEncenderTodos" CssClass="btn btn-default" runat="server" Text="Si" />
                                        <asp:Button ID="btConfirmarEncenderSelected" OnClick="encenderSelected" CssClass="btn btn-default" runat="server" Text="Si" />
                                        
                                        <asp:Button ID="btNoConfirmar" CssClass="btn btn-default" OnClick="hideModalConfirm" runat="server" Text="No" />
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
            <asp:ModalPopupExtender ID="modalPopupConfirmar" CancelControlID="LinkButton1" TargetControlID="bthiddenModalConfirm" PopupControlID="modalpupoConfirmar" runat="server"></asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Button ID="btModaledit" CssClass="hidden" runat="server" Text="Button" />
    <%-- Modal Editar Usuario --%>
    <div id="modalEditUsuario" class="modal-dialog animated slideInDown">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h2 class="modal-title" style="display: inline" id="tituloModalEditUsuario">Copiar <small>Archivos o Carpetas</small></h2>
                    <asp:LinkButton runat="server" ID="btCancel" class="close" Style="display: inline" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                    </asp:LinkButton>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
                        <ContentTemplate>
                            <div class="x_title">
                                <asp:Button ID="btArchivos" runat="server" Text="Archivo" OnClick="ViewCopiarArchivos" />
                                <asp:Button ID="btCarpetas" runat="server" Text="Carpeta" OnClick="ViewCopiarCarpetas" /><br />
                                <div class="clearfix"></div>
                            </div>
                            <asp:Label ID="lbSelectedPc" runat="server" Text=""></asp:Label>
                            <br />
                            <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                                <asp:View ID="ViewArchivos" runat="server">
                                    <div class="row">
                                        <div class="form-group">
                                            <asp:AjaxFileUpload ID="CargarArchivos" runat="server" OnUploadComplete="AjaxFileUpload1_UploadComplete" />
                                            <br />
                                            <asp:Label runat="server" ClientIDMode="Static" ID="lbruta2"></asp:Label>
                                        </div>
                                        <br />

                                        <div class="form-group">
                                            <div style="display: inline">
                                                <div class="col-md-7">
                                                    <asp:RadioButton ID="rbEscritorio" runat="server" GroupName="ruta" Checked="true" /><asp:Label ID="Label1" runat="server" Text="Label">Escritorio Publico</asp:Label>
                                                    <asp:RadioButton ID="rbDiscoC" runat="server" GroupName="ruta" /><asp:Label ID="Label2" runat="server" Text="Label">Disco Local</asp:Label>
                                                    <asp:RadioButton ID="rbPersonalizado" runat="server" GroupName="ruta" /><asp:Label ID="Label3" runat="server" Text="Label">Personalizado</asp:Label>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txt_rutaNueva" runat="server" CssClass="form-control"></asp:TextBox><br />
                                                    <br />
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="clearfix"></div>
                                        <asp:Button ID="btCopiarArchivo" runat="server" OnClick="CopiarArchivos" Text="Copiar Archivo" Width="20%" Style="margin-left: 40%" />
                                        <asp:Button ID="btCopiarArchivotodos" runat="server" OnClick="CopiarArchivosTodos" Text="Copiar Archivo" Width="20%" Style="margin-left: 40%" />
                                        <asp:Button ID="btCopiarArchivosSelected" runat="server" OnClick="CopiarArchivosSelected" Text="Copiar Archivo" Width="20%" Style="margin-left: 40%" />
                                        <asp:Label ID="lbMovidos" runat="server" Text="Label"></asp:Label>
                                </asp:View>
                                <asp:View ID="ViewCarpetas" runat="server">
                                    <div class="row">
                                        <div class="form-group">
                                            <div class="col-md-8">
                                                <asp:TextBox ID="txtRuta" runat="server" CssClass="form-control" Width="100%"></asp:TextBox>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:Label ID="Label4" runat="server" Text="Label">Ingrese Ruta De La Carpeta</asp:Label>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <br />
                                            <br />
                                            <br />
                                            <br />
                                            <div style="display: inline">
                                                <div class="col-md-7">
                                                    <asp:RadioButton ID="RbEscritorioPublico2" runat="server" GroupName="ruta" Checked="true" /><asp:Label ID="Label5" runat="server" Text="Label">Escritorio Publico</asp:Label>
                                                    <asp:RadioButton ID="RbDiscoLocal2" runat="server" GroupName="ruta" /><asp:Label ID="Label6" runat="server" Text="Label">Disco Local</asp:Label>
                                                    <asp:RadioButton ID="RbPersonalizado2" runat="server" GroupName="ruta" /><asp:Label ID="Label8" runat="server" Text="Label">Personalizado</asp:Label>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtPersonalizada2" runat="server" CssClass="form-control"></asp:TextBox><br />
                                                    <br />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="clearfix"></div>
                                        <asp:Button ID="btnCOpiarCarpeta" runat="server" OnClick="CopiarCarpetas" Text="Copiar Carpeta" Width="20%" Style="margin-left: 40%" />
                                        <asp:Button ID="BtnCopiarCArpetasTodos" runat="server" OnClick="CopiarCarpetasTodos" Text="Copiar Carpeta" Width="20%" Style="margin-left: 40%" />
                                        <asp:Label ID="lbCarpMovidas" runat="server" Text="Label"></asp:Label>
                                </asp:View>
                            </asp:MultiView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
    <asp:ModalPopupExtender ID="modalPopupExtenderCopiar" CancelControlID="btCancel" TargetControlID="btModaledit" PopupControlID="modalEditUsuario" runat="server">
    </asp:ModalPopupExtender>
</asp:Content>
