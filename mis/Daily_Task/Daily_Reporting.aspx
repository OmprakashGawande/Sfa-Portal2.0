<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Daily_Reporting.aspx.cs" Inherits="mis_Daily_Task_Daily_Reporting" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <%--<link href="../css/simplemde.min.css" rel="stylesheet" />--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
    <%--Confirmation Modal Start --%>
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div style="display: table; height: 100%; width: 100%;">
            <div class="modal-dialog" style="width: 340px; display: table-cell; vertical-align: middle;">
                <div class="modal-content" style="width: inherit; height: inherit; margin: 0 auto;">
                    <div class="modal-header" style="background-color: #d9d9d9;">

                        <span class="modal-title" style="float: left" id="myModalLabel">Confirmation</span>
                        <button type="button" class="close" data-dismiss="modal">
                            <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>
                        </button>
                    </div>
                    <div class="clearfix"></div>
                    <div class="modal-body">
                        <p>
                            <%--<img src="../assets/images/question-circle.png" width="30" />--%>&nbsp;&nbsp; 
                           <i class="fa fa-question-circle"></i>
                            <asp:Label ID="lblPopupAlert" runat="server"></asp:Label>
                        </p>
                    </div>
                    <div class="modal-footer">
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes" OnClick="btnSendReport_Click" Style="margin-top: 20px; width: 50px;" />
                        <asp:Button ID="btnNo" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />
                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
    </div>
    <%--ConfirmationModal End --%>
    <div class="content-wrapper">
        <section class="content">
            <div class="container-fluid">

                <div class="box box-success">
                    <div class="box-header with-border">
                        <h3 class="box-title" id="Label1">Daily Reporting</h3>
                    </div>
                    <asp:Label runat="server" ID="lblMsg"></asp:Label>
                    <div class="box-body">
                        <div class="row">
                            <div class="col-md-12">
                                <p style="color:red;"><b>Note :- </b>Previous date task only allowed till 10:00 am .</p>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <label runat="server">
                                    DATE 
                                    <label style="color: red;">*</label>
                                </label>
                                <div class="form-group">
                                    <asp:TextBox runat="server" ID="txtDate"
                                      
                                        data-provide="datepicker" placeholder="DD/MM/YYYY"
                                        autocomplete="off" data-date-format="dd/mm/yyyy"
                                        data-date-autoclose="true" CssClass="form-control disableFuturedate"
                                        OnTextChanged="txtDate_TextChanged" AutoPostBack="true"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-5"></div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label runat="server">EMPLOYEE NAME  </label>
                                    <label style="color: red;">*</label>
                                    <asp:TextBox runat="server" ID="txtEmp" CssClass="form-control">

                                    </asp:TextBox>
                                </div>
                            </div>


                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <div class="row">
                                    <div class=" col-md-6" runat="server">
                                        <label runat="server">
                                            PROJECT NAME
                                            <label style="color: red;">*</label></label>
                                        <span class="fa-pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="a"
                                                ErrorMessage="SELECT PROJECT NAME " InitialValue="0" ForeColor="Red"
                                                Text="<i class='fa fa-exclamation-circle' title='Select Project !'></i>"
                                                ControlToValidate="ddlProject" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <div class="ms form-group">
                                            <asp:DropDownList runat="server" ID="ddlProject" ClientIDMode="Static"
                                                CssClass="form-control select2">
                                                <asp:ListItem Value="0">No record found</asp:ListItem>

                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class=" col-md-6" runat="server">
                                        <label runat="server">
                                            WORK CATEGORY
                                            <label style="color: red;">*</label></label>
                                        <span class="fa-pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="a"
                                                ErrorMessage="SELECT WORK CATEGORY" InitialValue="0" ForeColor="Red"
                                                Text="<i class='fa fa-exclamation-circle' title='Select Work Category !'></i>"
                                                ControlToValidate="ddlWorkCategoryId" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <div class="ms form-group">
                                            <asp:DropDownList runat="server" ID="ddlWorkCategoryId" ClientIDMode="Static"
                                                CssClass="form-control select2">
                                                <asp:ListItem Value="0">No record found</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">

                                    <div class=" col-md-6" runat="server">
                                        <div class="form-group">
                                            <label runat="server">
                                                HOURS 
                                                <label style="color: red;">*</label></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a" Enabled="true"
                                                    ErrorMessage="ENTER HOURS" Text="<i class='fa fa-exclamation-circle' title='ENTER HOURS !'></i>"
                                                    ControlToValidate="txtTotalHours" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ErrorMessage="Please enter numeric  value Ex - (00) !"
                                                    Text="<i class='fa fa-exclamation-circle' title='Please enter numeric value Ex - (00) !'></i>"
                                                    ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtTotalHours"
                                                    SetFocusOnError="true"
                                                    ForeColor="Red" ValidationExpression="^[0-9]{1,10}$"></asp:RegularExpressionValidator>
                                            </span>
                                            <asp:TextBox runat="server" TextMode="Number" max="12" min="0" ClientIDMode="Static" ID="txtTotalHours" CssClass="form-control noEnterSubmit"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class=" col-md-6" runat="server">
                                        <div class="form-group">
                                            <label runat="server">
                                                MINUTES 
                                                <label style="color: red;">*</label></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a" Enabled="true"
                                                    ErrorMessage="ENTER MINUTES" Text="<i class='fa fa-exclamation-circle' title='ENTER MINUTES !'></i>"
                                                    ControlToValidate="txtMinutes" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="rev" ErrorMessage="Please enter numeric  value Ex - (00) !"
                                                    Text="<i class='fa fa-exclamation-circle' title='Please enter numeric value Ex - (00) !'></i>"
                                                    ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtMinutes"
                                                    SetFocusOnError="true"
                                                    ForeColor="Red" ValidationExpression="^[0-9]{1,10}$"></asp:RegularExpressionValidator>
                                            </span>
                                            <asp:TextBox runat="server" TextMode="Number" max="60" min="0" ClientIDMode="Static" ID="txtMinutes" CssClass="form-control noEnterSubmit"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">

                                    <div class=" col-md-12" runat="server">
                                        <div class="form-group">
                                            <label runat="server">
                                                ASSIGNED BY 
                                            <label style="color: red;">*</label></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="a" Enabled="true"
                                                    ErrorMessage="ENTER NAME OF THE PERSON ASSIGNED BY" Text="<i class='fa fa-exclamation-circle' title='ENTER NAME OF THE PERSON ASSIGNED BY !'></i>"
                                                    ControlToValidate="txtAssignedBy" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                            </span>
                                            <asp:TextBox runat="server" ClientIDMode="Static" MaxLength="40" ID="txtAssignedBy" CssClass="form-control noEnterSubmit"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-8">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label runat="server">
                                                WORK DESCRIPTION 
                                                <label style="color: red;">*</label></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="a" Enabled="true"
                                                    ErrorMessage="ENTER WORK DESCRIPTION " Text="<i class='fa fa-exclamation-circle' title='ENTER WORK DESCRIPTION  !'></i>"
                                                    ControlToValidate="txtDescription" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                            </span>
                                            <%-- onkeyDown="checkTextAreaMaxLength(this,event,'1000');"--%>
                                            <asp:TextBox TextMode="MultiLine" Rows="10" runat="server" ID="txtDescription" CssClass="form-control">

                                            </asp:TextBox>
                                            <asp:Label runat="server" ID="lblCounter"></asp:Label>
                                            <asp:HiddenField runat="server" ID="hfDescription" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <hr />
                        <div class="row" style="margin-top: 10px;">
                            <div class="col-md-4">
                            </div>
                            <div class="col-md-2">
                                <asp:Button runat="server" Text="Add" ValidationGroup="a" ID="btnAdd" OnClick="btnAdd_Click" CssClass="btn btn-block btn-success" />

                            </div>
                            <div class="col-md-2">
                                <a href="Daily_Reporting.aspx" class="btn btn-block btn-default">Clear</a>
                            </div>

                        </div>
                    </div>
                    <div class="box-footer">
                        <fieldset>
                            <legend>Details</legend>
                            <div class="table-responsive">
                                <asp:HiddenField runat="server" ID="hfTask_Id_ChildTemp" />
                                <asp:GridView runat="server" AutoGenerateColumns="false" ID="gridvew1" OnRowCommand="gridvew1_RowCommand"
                                    CssClass="table table-bordered table-hover">

                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No." HeaderStyle-Width="13">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text="<%# Container.DataItemIndex + 1 %>"></asp:Label>
                                                <asp:Label ID="lblTask_Id_ChildTemp" runat="server" Text='<%# Eval("Task_Id_ChildTemp").ToString() %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PROJECT NAME">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%#Eval("Project_Name").ToString() %>'></asp:Label>
                                                <asp:Label runat="server" ID="lblProject_Id" Text='<%#Eval("Project_Id").ToString() %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="WORK CATEGORY">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%#Eval("WorkCategory").ToString() %>'></asp:Label>
                                                <asp:Label runat="server" ID="lblWorkCategoryId" Text='<%#Eval("WorkCategoryId").ToString() %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="HOURS">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblTask_Hours" Text='<%#Eval("Task_Hours").ToString() %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="MINUTES">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblTask_Minutes" Text='<%#Eval("Task_Minutes").ToString() %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ASSIGNED BY">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblAssignedBy" Text='<%#Eval("AssignedBy").ToString() %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="WORK DESCRIPTION">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblWork_Description" Text='<%#Eval("Work_Description").ToString() %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="STATUS">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIsActive" runat="server" Text='<%# Eval("IsActive").ToString() %>' Visible="false"></asp:Label>
                                                <asp:LinkButton ID="linkdelete" runat="server" CommandName="btnStatus" CommandArgument='<%# Eval("Task_Id_ChildTemp").ToString() %>' CssClass='<%# Eval("IsActive").ToString() =="True"?"label label-success":"label label-danger" %>' Text='<%# Eval("IsActive").ToString() =="True"?"Active":"Deactive" %>'></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ACTION">
                                            <ItemTemplate>

                                                <asp:LinkButton ID="linkUpdate" runat="server" CommandName="btnUpdate" CommandArgument='<%# Eval("Task_Id_ChildTemp").ToString() %>' CssClass="btn btn-sm btn-primary"><i class="fa fa-edit"></i></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </fieldset>
                        <div class="row" style="margin-top: 15px;">
                            <div class="col-md-5">
                            </div>
                            <div class="col-md-2">
                                <asp:Button runat="server" Text="Send Report" ID="btnSendReport" OnClientClick="return ValidatePage() " OnClick="btnSendReport_Click" CssClass="btn btn-block btn-success" />

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script type="text/javascript">
        function ValidatePage() {
            document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Submit this record?";
            $('#myModal').modal('show');
            return false;
        }
        $(document).ready(function () {
            $('.noEnterSubmit').keypress(function (e) {
                if (e.which == 13) return false;
                //or...
                if (e.which == 13) e.preventDefault();
            });
        });

    </script>

    <%--    <script src="../js/simplemde.min.js"></script>
    <script>
        // Initialize SimpleMDE
        var simplemde = new SimpleMDE({
            element: document.getElementById('<%= txtDescription.ClientID %>'),
            autofocus: true,
            spellChecker: false, // Optional: Disable spell checker
            forceSync: true, // Optional: Force sync with the textarea
            toolbar: [
                "bold", "italic", "heading", "|",
                //"quote", "code",
                "unordered-list", "ordered-list", "|",
                "link",
                //"image",
                "|",
                "undo", "redo", "|",
                "preview"

            ],
            toolbarTips: true

        });

        //var textarea = simplemde.codemirror.getTextArea();
        //textarea.setAttribute("ariaValueMax", 10);
        //simplemde.ariaValueMax = 10;
        //console.log(simplemde);
    </script>--%>


    <script>
        CharactersCount(1000);
        const element = document.getElementById('<%=txtDescription.ClientID%>');
        // Pass CharactersCount directly with a parameter using an inline arrow function
        element.addEventListener("keyup", (event) => CharactersCount(1000));

        function CharactersCount(_length) {
            var txtMsg = document.getElementById('<%=txtDescription.ClientID%>');
            var lblCount = document.getElementById('<%=lblCounter.ClientID%>');
            if (txtMsg.value.length > _length) {
                txtMsg.value = txtMsg.value.substring(0, _length);
            }
            // Calculate and display the remaining characters
            const remaining = _length - txtMsg.value.length;
            lblCount.innerHTML = `${remaining} characters remaining`;
        }

        function checkTextAreaMaxLength(textBox, e, length) {

            var mLen = textBox["MaxLength"];
            if (null == mLen)
                mLen = length;

            var maxLength = parseInt(mLen);
            if (!checkSpecialKeys(e)) {
                if (textBox.value.length > maxLength - 1) {
                    if (window.event)//IE
                        e.returnValue = false;
                    else//Firefox
                        e.preventDefault();
                }
            }
        }
        function checkSpecialKeys(e) {
            if (e.keyCode != 8 && e.keyCode != 46 && e.keyCode != 37 && e.keyCode != 38 && e.keyCode != 39 && e.keyCode != 40)
                return false;
            else
                return true;
        }
    </script>
</asp:Content>

