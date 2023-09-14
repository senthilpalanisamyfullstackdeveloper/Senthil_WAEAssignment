<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Senthil.WAE.Assignment._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h4>Assignment</h4>
        <label>
            We have supplied alongside this assignment some example data to help organise. The data contains multiple channels of data that need to be separated on the following conditions:
            <ul> 
               <li>Channel 1 is == 2</li> 
               <li>Channel 3 is &lt; 3</li> 
            </ul>
            When these conditions are met, the data must be separated into multiple files, with the data beforehand in one file
            and afterwards in another.
        </label>
    </div>

    <div class="row">
         <asp:Panel ID="PanelFileUpload" runat="server">
             <Label>Select a file to upload:</Label> <asp:FileUpload ID="InputFileUpload" runat="server" accept=".dat,.txt,.csv" />
         </asp:Panel>
         <br />
        <div>
        <asp:Label ID="LblChannel" runat="server" Text="Channel"></asp:Label> : <asp:TextBox ID="txtChannel" runat="server" Text="1" MaxLength="2" Width="5%"></asp:TextBox>
        <asp:RegularExpressionValidator ID='vldNumber' ControlToValidate='txtChannel' Display='Dynamic' ErrorMessage='Please enter a channel number' ForeColor="Red" ValidationExpression='(^([0-9]*|\d*\d{1}?\d*)$)' Runat='server'>
        </asp:RegularExpressionValidator>
        </div><br /><div>
            <asp:Label ID="LblOperator" runat="server" Text="Operator"></asp:Label> :
        <asp:DropDownList ID="DdlOperators" runat="server">
            <asp:ListItem Enabled="true" Text="Select Operator" Value="0"></asp:ListItem>
            <asp:ListItem Text="==" Value="13"></asp:ListItem>
            <asp:ListItem Text="<" Value="20"></asp:ListItem>
            <asp:ListItem Text="<=" Value="21"></asp:ListItem>
            <asp:ListItem Text=">" Value="14"></asp:ListItem>
            <asp:ListItem Text=">=" Value="15"></asp:ListItem>
            <asp:ListItem Text="<>" Value="35"></asp:ListItem>
        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="DdlOperators" ForeColor="Red" InitialValue="0" runat="server" ErrorMessage="Please select operator"></asp:RequiredFieldValidator>
        </div><br /><div>
        <asp:Label ID="LblValue" runat="server" Text="Value"></asp:Label> : <asp:TextBox ID="txtValue" runat="server" Text="1" MaxLength="6" Width="5%"></asp:TextBox>
            <asp:RegularExpressionValidator ID='RegularExpressionValidator1' ControlToValidate='txtValue' Display='Dynamic' ErrorMessage='Please enter a value' ForeColor="Red" ValidationExpression='(^(-[0-9]*|[0-9]*|[0-9]*.\d{2}|-[0-9]*.\d{2})$)' Runat='server'>
        </asp:RegularExpressionValidator>
        </div>
        <br /><br />
        <div>
             <asp:Button ID="ProcessButton" class="btn btn-default" 
                   Text="Process" 
                   OnClick="ProcessButton_Click"
                   runat="server">
               </asp:Button> 
            </div>
            <asp:Label ID="lblStatusMessage" runat="server" ForeColor="Red"></asp:Label> 
    </div>
</asp:Content>
