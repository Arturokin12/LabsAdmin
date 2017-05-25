<%@ Page Title="" Language="C#" MasterPageFile="~/LabsAdmin.Master" AutoEventWireup="true" CodeBehind="Dominio.aspx.cs" Inherits="LabsAdminASP.Dominio" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <div class="row">
                <div class="col-lg-6">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3>Agregar Computadoras desde el dominio</h3>
                        </div>
                        <div class="panel-body">
                            <div class="table-responsive">
                                <asp:GridView ID="tablaPcsDominio" CssClass="table table-bordered" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanging="tablaPcsDominio_SelectedIndexChanging">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Computadora" SortExpression="nombre">
                                            <EditItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("nombre") %>'></asp:Label>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("nombre") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sincronizar">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton1" CssClass="btn btn-info" runat="server" CausesValidation="false" CommandName="Select" Text=""><i class="fa fa-pencil-square-o"></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Button ID="btHiddenModal1" CssClass="hidden" runat="server" Text="Button" />
    <%-- Modal Editar Usuario --%>
    <div id="modalPupoAgregarDesdeDominio" class="modal-dialog modal-sm animated slideInDown">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" style="display: inline">Eliminar Usuario</h5>
                    <asp:LinkButton runat="server" ID="btCerrarModal1" class="close" Style="display: inline" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                    </asp:LinkButton>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div>
                                <asp:Label ID="lbIP" runat="server" Text=""></asp:Label>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
    <asp:ModalPopupExtender ID="modalPopupExtenderAgregarDesdeDom" CancelControlID="btCerrarModal1" TargetControlID="btHiddenModal1" PopupControlID="modalPupoAgregarDesdeDominio" runat="server"></asp:ModalPopupExtender>
</asp:Content>
