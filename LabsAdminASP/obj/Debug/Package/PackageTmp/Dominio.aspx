<%@ Page Title="" Language="C#" MasterPageFile="~/LabsAdmin.Master" AutoEventWireup="true" CodeBehind="Dominio.aspx.cs" Inherits="LabsAdminASP.Dominio" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <section class="content-header">
                <h1>Labs Administration 1.0
        <small>Agregar Computadoras</small>
                </h1>
                <ol class="breadcrumb">
                    <li><a href="#"><i class="fa fa-dashboard"></i>Panel</a></li>
                    <li class="active">Computadoras</li>
                </ol>
            </section>
            <asp:Label ID="lbid_usuario" runat="server" Text="" CssClass="hidden"></asp:Label>
            <div class="row">
                <div class="col-md-12">
                    <div class="x_panel">
                        <div class="x_title">
                            <h2>Agregar computadoras <small>
                                <asp:Label ID="Num_Lab" runat="server" Text="Label"></asp:Label>
                            </small></h2>
                            <ul class="nav navbar-right panel_toolbox">
                                <li><a class="collapse-link"><i class="fa fa-chevron-up hidden"></i></a>
                                    <li><a class="collapse-link" data-toggle="collapse" href="#panelAddPCs"><i class="fa fa-chevron-up"></i></a>
                                    </li>
                                    <li class="dropdown">
                                        <a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-expanded="false"><i class="fa fa-wrench"></i></a>
                                        <ul class="dropdown-menu" role="menu">
                                            <li>
                                                <asp:LinkButton ID="btActualizar" OnClick="btActualizar_Click" runat="server">Actualizar</asp:LinkButton>
                                            </li>
                                            <li><a href="#">No lo se</a>
                                            </li>
                                        </ul>
                                    </li>
                            </ul>
                            <div class="clearfix"></div>
                        </div>
                        <div class="panel-collapse collapse in" id="panelAddPCs">
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
                        </div>
                    </div>
                </div>
                <div class="col-md-12">
                    <div class="x_panel">
                        <div class="x_title">
                            <h2>Computadoras Registradas <small>
                                <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
                            </small></h2>
                            <ul class="nav navbar-right panel_toolbox">
                                <li><a data-toggle="collapse" href="#panelTablaPcs"><i class="fa fa-chevron-up hidden"></i></a>
                                    <li><a class="collapse-link" data-toggle="collapse" href="#panelTablaPcs"><i class="fa fa-chevron-up"></i></a>
                                    </li>
                                    <li class="dropdown">
                                        <a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-expanded="false"><i class="fa fa-wrench"></i></a>
                                        <ul class="dropdown-menu" role="menu">
                                            <li>
                                                <asp:LinkButton ID="LinkButton3" OnClick="btActualizar_Click" runat="server">Actualizar</asp:LinkButton>
                                            </li>
                                            <li><a href="#">No lo se</a>
                                            </li>
                                        </ul>
                                    </li>
                            </ul>
                            <div class="clearfix"></div>
                        </div>
                        <div class="panel-collapse collapse in" id="panelTablaPcs">
                            <div class="table table-responsive">
                                <div class="form-group">
                                    <asp:Label ID="Label4" runat="server" Text="Seleccione laboratorio: "></asp:Label>
                                    <asp:DropDownList ID="cbLaboratorio2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cbLaboratorio2_SelectedIndexChanged" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div>
                                    <asp:GridView ID="tablaComputadoras" CssClass="table table-bordered" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanging="tablaComputadoras_SelectedIndexChanging" OnRowDeleting="tablaComputadoras_RowDeleting">

                                        <Columns>
                                            <asp:TemplateField HeaderText="#" SortExpression="id_computadora">
                                                <EditItemTemplate>
                                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("id_computadora") %>'></asp:Label>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("id_computadora") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Marca" SortExpression="marca">
                                                <EditItemTemplate>
                                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("marca") %>'></asp:Label>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("marca") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Modelo" SortExpression="modelo">
                                                <EditItemTemplate>
                                                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("modelo") %>'></asp:Label>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("modelo") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="SO" SortExpression="SO">
                                                <EditItemTemplate>
                                                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("SO") %>'></asp:Label>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("SO") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="MAC" SortExpression="Mac">
                                                <EditItemTemplate>
                                                    <asp:Label ID="Label5" runat="server" Text='<%# Eval("Mac") %>'></asp:Label>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("Mac") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="IPV4" SortExpression="ip">
                                                <EditItemTemplate>
                                                    <asp:Label ID="Label6" runat="server" Text='<%# Eval("ip") %>'></asp:Label>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("ip") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Descripción" SortExpression="descripcion">
                                                <EditItemTemplate>
                                                    <asp:Label ID="Label7" runat="server" Text='<%# Eval("descripcion") %>'></asp:Label>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label7" runat="server" Text='<%# Bind("descripcion") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Editar">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-info btn-new btn-sm" CausesValidation="false" CommandName="Select" Text=""><i class="fa fa-pencil-square-o"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Eliminar">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LinkButton2" runat="server" CssClass="btn btn-danger btn-sm" CausesValidation="false" CommandName="Delete" Text=""><i class="fa fa-trash-o"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle CssClass="tableHeader" />
                                        <AlternatingRowStyle CssClass="tableAlternating" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <%-- Modal Agregar Desde Dominio --%>
            <asp:Button ID="bthiddenModalAdd1" runat="server" Text="Button" CssClass="hidden" />
            <div id="modalPupoAgregarDesdeDom" class="modal-dialog animated slideInDown">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" style="display: inline" id="titulo2">
                                <asp:Label ID="Label2" Style="display: inline" runat="server" Text="Agregar Computadora desde Dominio"></asp:Label></h5>
                            <asp:LinkButton runat="server" ID="LinkButton2" OnClick="LinkButton2_Click" class="close" Style="display: inline">
                                <span aria-hidden="true">&times;</span>
                            </asp:LinkButton>
                        </div>
                        <div class="modal-body">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div>
                                        <div class="row">
                                            <asp:Label ID="lbNombreComputadora" runat="server" Text="" CssClass="hidden"></asp:Label>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>Nombre Equipo: </label>
                                                    <asp:TextBox ID="txtNombre" CssClass="form-control" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNombre" ErrorMessage="Campo Requerido"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group">
                                                    <label>Marca: </label>
                                                    <asp:TextBox ID="txtMarca" CssClass="form-control" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtMarca" ErrorMessage="Campo Requerido"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group">
                                                    <label>Modelo: </label>
                                                    <asp:TextBox ID="txtModelo" CssClass="form-control" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtModelo" ErrorMessage="Campo Requerido"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group">
                                                    <label>Descripcion: </label>
                                                    <asp:TextBox ID="txtDes" CssClass="form-control" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtDes" ErrorMessage="Campo Requerido"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>Sistema operativo: </label>
                                                    <asp:TextBox ID="txtOS" CssClass="form-control" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtOS" ErrorMessage="Campo Requerido"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group">
                                                    <label>Laboratorio: </label>
                                                    <asp:DropDownList ID="cbLaboratorio" CssClass="form-control" runat="server"></asp:DropDownList>
                                                    <label>
                                                </div>
                                                <div class="form-group form-inline">
                                                    <label>Dirección IPV4: </label>
                                                    <asp:TextBox ID="txtIP" CssClass="form-control" runat="server"></asp:TextBox>
                                                    <asp:LinkButton ID="btGetIP" runat="server" OnClick="btGetIP_Click" CssClass="btn btn-info btn-sm" ToolTip="Obtener ip del pc"><i class="fa fa-search" aria-hidden="true"></i></asp:LinkButton>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtIP" ErrorMessage="Campo Requerido"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group form-inline">
                                                    <label>Dirección MAC: </label>
                                                    <asp:TextBox ID="txtMac" CssClass="form-control" runat="server"></asp:TextBox>
                                                    <asp:LinkButton ID="btgetMac" data-toggle="tooltip" data-placement="top" OnClick="btgetMac_Click" CssClass="btn btn-info btn-sm" runat="server" ToolTip="Obtener MAC y Nombre del pc"><i class="fa fa-search" aria-hidden="true"></i></asp:LinkButton>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtMac" ErrorMessage="Campo Requerido"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group" style="width: 100px; margin: 0 auto">
                                                <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="UpdatePanel2" runat="server">
                                                    <ProgressTemplate>
                                                        <img src="images/ring-alt.gif" style="width: 60px; height: 60px" />
                                                        <label>Cargando...</label>
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>
                                            </div>
                                        </div>
                                        <asp:LinkButton ID="btEditComputadora" CssClass="btn btn-info btn-new btn-sm" OnClick="btEditComputadora_Click" Style="float: right" runat="server">Editar <i class="fa fa-pencil-square"></i></asp:LinkButton>
                                        <asp:LinkButton ID="btAddComputadora1" CssClass="btn btn-info btn-new btn-sm" OnClick="agregarComputadora1" Style="float: right" runat="server">Agregar <i class="fa fa-plus"></i></asp:LinkButton>
                                        <asp:Label ID="lbResultadoModal1" runat="server" Text=""></asp:Label>
                                        <asp:Button ID="btOkModal1" runat="server" CssClass="hidden" />
                                        <br />
                                        <br />
                                    </div>
                                    </label>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
            <asp:ModalPopupExtender ID="modalPupoAgregarDom"
                TargetControlID="bthiddenModalAdd1" PopupControlID="modalPupoAgregarDesdeDom" CancelControlID="LinkButton2" runat="server">
            </asp:ModalPopupExtender>

            <%-- Modal Mensaje --%>
            <asp:Button ID="btHiddenMensaje" CssClass="hidden" runat="server" Text="Button" />
            <div id="modalPupoMensaje" class="modal-dialog animated slideInDown">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" style="display: inline" id="titulo">
                                <asp:Label ID="lbTituloMensaje" Style="display: inline" runat="server" Text="Alerta"></asp:Label></h5>
                            <asp:LinkButton runat="server" ID="btCancelMensaje" OnClick="btCancelMensaje_Click" class="close" Style="display: inline" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </asp:LinkButton>
                        </div>
                        <div class="modal-body">
                            <div class="row">
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


            <%-- Modal Eliminar --%>
            <asp:Button ID="Button1" CssClass="hidden" runat="server" Text="Button" />
            <div id="modalpupoEliminar" class="modal-dialog animated slideInDown">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" style="display: inline">
                                <asp:Label ID="Label8" Style="display: inline" runat="server" Text="Eliminar Computadora"></asp:Label></h5>
                            <asp:LinkButton runat="server" ID="LinkButton4" OnClick="btCancelMensaje_Click" class="close" Style="display: inline" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </asp:LinkButton>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <p>
                                    <asp:Label ID="lbidEliminar" runat="server" Text="" CssClass="hidden"></asp:Label>
                                    <asp:Label ID="lbMensajeEliminar" runat="server" Text=""></asp:Label>
                                </p>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btEliminarPc" OnClick="btEliminarPc_Click" runat="server" Text="Si" UseSubmitBehavior="false" CssClass="btn btn-info" />
                            <asp:Button ID="Button3" runat="server" Text="No" UseSubmitBehavior="false" CssClass="btn btn-info" />
                        </div>
                    </div>
                </div>
            </div>
            <asp:ModalPopupExtender ID="modalPupoEliminarPc" OkControlID="Button3" CancelControlID="LinkButton4" TargetControlID="Button1" PopupControlID="modalpupoEliminar" runat="server"></asp:ModalPopupExtender>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
