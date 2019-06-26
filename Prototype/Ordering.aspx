<%@ Page Title="" Language="VB" MasterPageFile="MasterPage.master" AutoEventWireup="false" CodeFile="Ordering.aspx.vb" Inherits="Ordering" %>


<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server" >
    <div id="text" style="margin-right:10%; margin-left:10%; text-align:center;">
    <br />

    <h3> Sample Request Form:</h3>

 
        <fieldset>

            <legend>Personal Information </legend>

            <label>Name:</label>&nbsp;
            <asp:TextBox ID="nameBox" runat="server"></asp:TextBox>

            <br />
            <br />

            <label>Business Address:</label>&nbsp;
            <asp:TextBox ID="addressBox" runat="server"></asp:TextBox>

            <br />
            <br />

            <label>City:</label>&nbsp;
            <asp:TextBox ID="cityBox" runat="server"></asp:TextBox>

            <br />
            <br />

            <label>State:</label>&nbsp;
            <asp:DropDownList ID="stateDDL" runat="server">
                <asp:ListItem>Select a state</asp:ListItem>
                <asp:ListItem>NJ</asp:ListItem>
                <asp:ListItem>PA</asp:ListItem>
                <asp:ListItem>DE</asp:ListItem>
            </asp:DropDownList>

            <br />
            <br />

            <label>Zip:</label>&nbsp;
            <asp:TextBox ID="zipBox" runat="server"></asp:TextBox>

        </fieldset>

    <br />

        <fieldset>

          


            <legend>Requested Samples</legend>

            <p>Please indicate which products you would like to receive a sample of:</p>

            <asp:CheckBox ID="apronsCh" runat="server" Text="Aprons" />
            &nbsp;
            <asp:CheckBox ID="sleevesCh" runat="server" Text="Sleeves" />
            &nbsp;
            <asp:CheckBox ID="capsCh" runat="server" Text="Caps" />
            &nbsp;
            <asp:CheckBox ID="shoeCh" runat="server" Text="Shoe Covers" />
            &nbsp;
            <asp:CheckBox ID="coverallsCh" runat="server" Text="Coveralls" />



        </fieldset>


        <br />
       


    <asp:Button ID="btnSubmit" runat="server" Text="Submit" style="margin:auto;"/>
        </div>
</asp:Content>

