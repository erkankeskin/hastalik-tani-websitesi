<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="StyleSheet.css" rel="stylesheet" />
    <script src="JavaScript.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <img id="adam" src="body.png" />
        <div id="solkol" onclick="getarea('3')"></div>
        <div id="sagkol" onclick="getarea('3')"></div>
        <div id="bacak" onclick="getarea('4')"></div>
        <div id="govde" onclick="getarea('2')"></div>
        <div id="kafa" onclick="getarea('1')"></div>

        <asp:HiddenField ID="part" runat="server"/>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
    </div> 
        <div id="datatable">
        <asp:GridView ID="symtomes" runat="server">
        </asp:GridView></div>  
        <div id="symptomdata">
        <asp:DataList ID="dl1" runat="server" CellPadding="4" ForeColor="#333333">
            <AlternatingItemStyle BackColor="White" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <ItemStyle BackColor="#EFF3FB" />
            <ItemTemplate>
                <asp:CheckBox ID="cb" runat="server" Text='<%# Eval("symptomName") %>' symID='<%# Eval("symptomID") %>' AutoPostBack="True" OnCheckedChanged="cb_CheckedChanged" />
            </ItemTemplate>
            <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        </asp:DataList>

        </div> 
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        <asp:HiddenField ID="symString" runat="server" />
    </form>
</body>
</html>
