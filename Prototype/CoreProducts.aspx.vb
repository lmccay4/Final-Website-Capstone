Imports System.Data
Imports System.Data.SqlClient


Partial Class CoreProducts

    Inherits System.Web.UI.Page

    Dim ObjDT As System.Data.DataTable
    Dim ObjDR As System.Data.DataRow
    Dim ds As New DataSet



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            makeCart()
        End If
    End Sub



    Function makeCart()
        ObjDT = New System.Data.DataTable("Cart")
        ObjDT.Columns.Add("Item_ID", GetType(Int64))
        ObjDT.Columns.Add("Item_Price", GetType(Decimal))
        ObjDT.Columns.Add("Item_Name", GetType(String))
        ObjDT.Columns.Add("Item_Quantity", GetType(Integer))

        Session("Cart") = ObjDT

    End Function



    Public Sub DropDownList1_ItemSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownList1.SelectedIndexChanged

        Dim intCategory As Integer
        Dim strSQL As String

        Dim strConnection As String = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Products.mdf;Integrated Security=True"
        Dim objConnection As New SqlConnection(strConnection)


        intCategory = DropDownList1.SelectedValue



        Select Case intCategory
            Case 0

                GridView1.DataSource = Nothing
                GridView1.DataBind()
                Exit Sub
            Case 1
                strSQL = "SELECT * FROM [Headgear]"
                lblProd.Visible = True
            Case 2
                strSQL = "SELECT * FROM [Aprons/Sleeves]"
                lblProd.Visible = True
            Case 3
                strSQL = "SELECT * FROM [Coveralls]"
                lblProd.Visible = True
            Case 4
                strSQL = "SELECT * FROM [ShoeCovers]"
                lblProd.Visible = True
            Case 5
                strSQL = "SELECT * FROM [LabCoats]"
                lblProd.Visible = True
        End Select


        objConnection.Open()


        Dim objAdapter As New SqlDataAdapter(strSQL, objConnection)

        Using objConnection
            Select Case intCategory
                Case 1
                    objAdapter.Fill(ds, "Headgear")
                Case 2
                    objAdapter.Fill(ds, "Aprons/Sleeves")
                Case 3
                    objAdapter.Fill(ds, "Coveralls")
                Case 4
                    objAdapter.Fill(ds, "ShoeCovers")
                Case 5
                    objAdapter.Fill(ds, "LabCoats")

            End Select
        End Using

        Session("ds") = ds

        GridView1.DataSource = ds
        GridView1.DataBind()


        objConnection.Close()

    End Sub


    Function AddItemToCart()

        Dim intProductKey, intCurrentKey As Integer
        Dim i As Integer = 0
        Dim blnFound As Boolean = False
        Dim intCategory As Integer

        ds = Session("ds")

        intProductKey = GridView1.SelectedValue
        ObjDT = Session("Cart")

        For Each ObjDR In ObjDT.Rows
            If ObjDR("Item_ID") = intProductKey Then
                ObjDR("Item_Quantity") += 1
                blnFound = True
                Exit For
            End If
        Next

        If Not blnFound Then
            ObjDR = ObjDT.NewRow
            ObjDR("Item_ID") = intProductKey
            ObjDR("Item_Quantity") = 1

            intCategory = DropDownList1.SelectedValue
            Select Case intCategory
                Case 1
                    Do While i <= (ds.Tables("Headgear").Rows.Count - 1)
                        intCurrentKey = ds.Tables("Headgear").Rows(i).Item("Item_ID")
                        If intCurrentKey = intProductKey Then
                            ObjDR("Item_Name") = ds.Tables("Headgear").Rows(i).Item("Item_Name")
                            ObjDR("Item_Price") = ds.Tables("Headgear").Rows(i).Item("Item_Price")
                        End If
                        i += 1
                    Loop
                Case 2
                    Do While i <= (ds.Tables("Aprons/Sleeves").Rows.Count - 1)
                        intCurrentKey = ds.Tables("Aprons/Sleeves").Rows(i).Item("Item_ID")
                        If intCurrentKey = intProductKey Then
                            ObjDR("Item_Name") = ds.Tables("Aprons/Sleeves").Rows(i).Item("Item_Name")
                            ObjDR("Item_Price") = ds.Tables("Aprons/Sleeves").Rows(i).Item("Item_Price")
                        End If
                        i += 1
                    Loop
                Case 3
                    Do While i <= (ds.Tables("Coveralls").Rows.Count - 1)
                        intCurrentKey = ds.Tables("Coveralls").Rows(i).Item("Item_ID")
                        If intCurrentKey = intProductKey Then
                            ObjDR("Item_Name") = ds.Tables("Coveralls").Rows(i).Item("Item_Name")
                            ObjDR("Item_Price") = ds.Tables("Coveralls").Rows(i).Item("Item_Price")
                        End If
                        i += 1
                    Loop
                Case 4
                    Do While i <= (ds.Tables("ShoeCovers").Rows.Count - 1)
                        intCurrentKey = ds.Tables("ShoeCovers").Rows(i).Item("Item_ID")
                        If intCurrentKey = intProductKey Then
                            ObjDR("Item_Name") = ds.Tables("ShoeCovers").Rows(i).Item("Item_Name")
                            ObjDR("Item_Price") = ds.Tables("ShoeCovers").Rows(i).Item("Item_Price")
                        End If
                        i += 1
                    Loop
                Case 5
                    Do While i <= (ds.Tables("LabCoats").Rows.Count - 1)
                        intCurrentKey = ds.Tables("LabCoats").Rows(i).Item("Item_ID")
                        If intCurrentKey = intProductKey Then
                            ObjDR("Item_Name") = ds.Tables("LabCoats").Rows(i).Item("Item_Name")
                            ObjDR("Item_Price") = ds.Tables("LabCoats").Rows(i).Item("Item_Price")
                        End If
                        i += 1
                    Loop

            End Select

            ObjDT.Rows.Add(ObjDR)

        End If

        Session("Cart") = ObjDT

    End Function

    Function ShowItemsInCart()

        ObjDT = Session("Cart")
        GridView2.DataSource = ObjDT
        GridView2.DataBind()

    End Function

    Function ShowCartTotal()
        lblTotal.Visible = True
        lblAmount.Visible = True
        btnCheckOut.Visible = True
        lblAmount.Text = "$" & GetItemTotals()
        lblTotal.Text = "Total:"
    End Function

    Function CloseCartTotal()
        lblTotal.Visible = False
        lblAmount.Visible = False
        btnCheckOut.Visible = False
        lblTotal.Text = ""
    End Function

    Function GetItemTotals()

        Dim intCounter As Integer
        Dim decRunningTotal As Integer
        ObjDT = Session("Cart")

        For intCounter = 0 To ObjDT.Rows.Count - 1
            ObjDR = ObjDT.Rows(intCounter)
            decRunningTotal += (ObjDR("Item_Price") * ObjDR("Item_Quantity") * 1000)
        Next

        Return decRunningTotal

    End Function


    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged
        AddItemToCart()
        ShowItemsInCart()
        ShowCartTotal()
    End Sub


    Protected Sub GridView2_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridView2.RowDeleting
        ObjDT = Session("Cart")
        Dim i As Integer = 0


        For Each ObjDR In ObjDT.Rows
            If i = e.RowIndex Then
                If ObjDR("Item_Quantity") = 1 Then
                    ObjDT.Rows.Item(e.RowIndex).Delete()
                    Exit For
                Else
                    ObjDR("Item_Quantity") -= 1
                    Exit For
                End If
            End If
            i += 1
        Next

        Session("Cart") = ObjDT

        If ObjDT.Rows.Count >= 1 Then
            ShowItemsInCart()
            ShowCartTotal()
        Else
            ShowItemsInCart()
            CloseCartTotal()
        End If


    End Sub

    Protected Sub btnCheckOut_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCheckOut.Click
        Server.Transfer("CheckOut.aspx")
    End Sub
End Class
