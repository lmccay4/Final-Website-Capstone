Imports System.Data
Imports System.Data.SqlClient

Partial Class CheckOut
    Inherits System.Web.UI.Page
    Dim ObjDT As System.Data.DataTable
    Dim ObjDR As System.Data.DataRow
    Dim ds As New DataSet


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        GridView1.DataSource = Session("Cart")
        GridView1.DataBind()
        UnobtrusiveValidationMode = UnobtrusiveValidationMode.None
    End Sub


    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click

        makeNewDataTable()
        Dim strItemsOrdered As String = ""
        Dim strTotal As Decimal = 0

        Dim strConnection As String = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Products.mdf;Integrated Security=True"
        Dim objConnection As New SqlConnection(strConnection)
        Dim mySqlDataAdapter As New SqlDataAdapter("Select * From ToBe_Shipped", objConnection)


        ObjDT = Session("Cart")
        For Each ObjDR In ObjDT.Rows
            strItemsOrdered += ObjDR("Item_ID") & "(" & ObjDR("Item_Quantity") & ")" & ", "
            strTotal += ObjDR("Item_Quantity") * ObjDR("Item_Price")

        Next



        Dim myDataRow As DataRow
        Dim myDataRowsCommandBuilder As New SqlCommandBuilder(mySqlDataAdapter)
        mySqlDataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey

        mySqlDataAdapter.Fill(ds, "ToBe_Shipped")
        myDataRow = ds.Tables("ToBe_Shipped").NewRow()


        myDataRow("FName") = txtFName.Text
        myDataRow("LName") = txtLName.Text
        myDataRow("Street") = txtStreet.Text
        myDataRow("City") = txtCity.Text
        myDataRow("State") = txtState.Text
        myDataRow("Zip") = txtZip.Text
        myDataRow("Phone") = txtPhone.Text
        myDataRow("CardType") = ddlCType.SelectedValue
        myDataRow("CardNumber") = txtCNumber.Text
        myDataRow("Products") = strItemsOrdered
        myDataRow("Total") = strTotal

        ds.Tables("ToBe_Shipped").Rows.Add(myDataRow)
        mySqlDataAdapter.Update(ds, "ToBe_Shipped")

        Server.Transfer("Confirmation.aspx")


    End Sub

    Function makeNewDataTable()
        ObjDT = New System.Data.DataTable("Cart")
        ObjDT.Columns.Add("Item_ID", GetType(Int64))
        ObjDT.Columns.Add("Item_Name", GetType(String))
        ObjDT.Columns.Add("Item_Price", GetType(Decimal))
        ObjDT.Columns.Add("Item_Quantity", GetType(Integer))

    End Function

End Class