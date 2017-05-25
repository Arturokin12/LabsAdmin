<%@ Page Title="" Language="C#" MasterPageFile="~/LabsAdmin.Master" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="LabsAdminASP.Users" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <script>

            </script>
            <section class="content-header">
                <h1>Labs Administration 1.0
        <small>Panel de Administración de usuarios</small>
                </h1>
                <ol class="breadcrumb">
                    <li><a href="#"><i class="fa fa-dashboard"></i>Panel</a></li>
                    <li class="active">Usuarios</li>
                </ol>
            </section>
            <section class="container" style="margin-top: 1%">
                <asp:LinkButton runat="server" data-toggle="modal" data-target="#modalAddUsuario" ID="btModalAddUsuario" CssClass="btn btn-success">Agregar Usuario <i class="fa fa-plus"></i></asp:LinkButton>
                <br />
                <br />
                <div class="table-responsive">
                    <asp:GridView ID="tablaUsuarios" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered" OnRowDeleting="tablaUsuarios_RowDeleting" OnSelectedIndexChanging="tablaUsuarios_SelectedIndexChanging">
                        <Columns>
                            <asp:TemplateField HeaderText="#" SortExpression="id_usuario">
                                <EditItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("id_usuario") %>'></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("id_usuario") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Nombre" SortExpression="nombre">
                                <EditItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("nombre") %>'></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("nombre") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Nick" SortExpression="nick">
                                <EditItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("nick") %>'></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("nick") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Editar">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" CssClass="btn btn-primary btn-sm" runat="server" CausesValidation="false" CommandName="Select" Text=""><i class="fa fa-pencil-square-o"></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Eliminar">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton2" CssClass="btn btn-danger btn-sm" runat="server" CausesValidation="false" CommandName="Delete" Text=""><i class="fa fa-trash"></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="tableHeader"/>
                        <AlternatingRowStyle CssClass="tableAlternating"/>
                    </asp:GridView>
                    <!-- Modal -->
                </div>
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
            </section>
            <asp:Button ID="btModaledit" CssClass="hidden" runat="server" Text="Button" />
            <%-- Modal Editar Usuario --%>
            <div id="modalEditUsuario" class="modal-dialog animated slideInDown">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" style="display: inline" id="tituloModalEditUsuario">Editar Usuario</h5>
                            <asp:LinkButton runat="server" ID="btCancel" class="close" Style="display: inline" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </asp:LinkButton>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="txtNombre">Nombre: </label>
                                        <asp:TextBox ID="txtNombreEdit" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="txtNick">Nick (nombre de dominio): </label>
                                        <asp:TextBox ID="txtNickEdit" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <asp:Label CssClass="hidden" ID="lbidUsuarioEditar" runat="server" Text=""></asp:Label>
                                <asp:Label Style="margin-left: 2%" ID="lbMensajeEditUsuario" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button runat="server" ID="btOk" CssClass="btn btn-secondary" data-dismiss="modal" Text="Cancelar" />
                            <asp:Button ID="btEditarUsuario" runat="server" OnClick="editarUsuario" Text="Editar" UseSubmitBehavior="false" CssClass="btn btn-info" />
                        </div>
                    </div>
                </div>
            </div>
            <asp:ModalPopupExtender ID="modalPopupExtenderEditar" OkControlID="btOk" CancelControlID="btCancel" TargetControlID="btModaledit" PopupControlID="modalEditUsuario" runat="server"></asp:ModalPopupExtender>

            <asp:Button ID="btHiddenMensaje" CssClass="hidden" runat="server" Text="Button" />
            <%-- Modal Editar Usuario --%>
            <div id="modalPupoMensaje" class="modal-dialog animated slideInDown">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" style="display: inline" id="titulo">
                                <asp:Label ID="lbTituloMensaje" style="display:inline" runat="server" Text="Alerta"></asp:Label></h5>
                            <asp:LinkButton runat="server" ID="LinkButton3" class="close" Style="display: inline" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </asp:LinkButton>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <p><asp:Label ID="lbMensaje" runat="server" Text=""></asp:Label></p>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btOkMensaje" runat="server" Text="Ok" UseSubmitBehavior="false" CssClass="btn btn-info" />
                        </div>
                    </div>
                </div>
            </div>
            <asp:ModalPopupExtender ID="modalPupoExtenderMensaje" OkControlID="btOkMensaje" CancelControlID="btCancelMensaje" TargetControlID="btHiddenMensaje" PopupControlID="modalPupoMensaje" runat="server"></asp:ModalPopupExtender>

            <asp:Button ID="btHiddenEliminar" CssClass="hidden" runat="server" Text="Button" />
            <%-- Modal Editar Usuario --%>
            <div id="modalPupoEliminar" class="modal-dialog modal-sm animated slideInDown">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" style="display: inline">Eliminar Usuario</h5>
                            <asp:LinkButton runat="server" ID="btCerrarEliminar" class="close" Style="display: inline" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </asp:LinkButton>
                        </div>
                        <div class="modal-body">
                            <div class="row" style="text-align:center">
                                <asp:Label ID="lbidUsuarioEliminar" CssClass="hidden" runat="server" Text=""></asp:Label>
                                <p><asp:Label ID="Label5" runat="server" Text="">¿Está seguro que desea eliminar el usuario?</asp:Label></p>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btSI" runat="server" OnClick="eliminarUsuario" Text="Si" UseSubmitBehavior="false" CssClass="btn btn-default" />
                            <asp:Button ID="btNo" runat="server" Text="No" UseSubmitBehavior="false" CssClass="btn btn-default" />
                        </div>
                    </div>
                </div>
            </div>
            <asp:ModalPopupExtender ID="modalPopupExtenderEliminar" OkControlID="btNo" CancelControlID="btCerrarEliminar" TargetControlID="btHiddenEliminar" PopupControlID="modalPupoEliminar" runat="server"></asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    <%-- Modal Agregar Usuario --%>
    <div class="modal fade" id="modalAddUsuario" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="tituloModalAddUsuario" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" style="display: inline" id="tituloModalAddUsuario">Agregar Usuario</h5>
                            <button type="button" class="close" style="display: inline" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="txtNombre">Nombre: </label>
                                        <asp:TextBox ID="txtNombre" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="txtNick">Nick (nombre de dominio): </label>
                                        <asp:TextBox ID="txtNick" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <asp:Label Style="margin-left: 2%" ID="lbMensajeAddUsuario" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                            <asp:Button ID="btAddUsuario" runat="server" OnClick="AgregarUsuario" Text="Agregar" UseSubmitBehavior="false" CssClass="btn btn-success" />
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
