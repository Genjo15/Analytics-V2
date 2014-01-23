Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports MySql.Data

<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class Analytics
    Inherits System.Web.Services.WebService
    'CONNEXION
    <WebMethod()> _
    Private Function _connexion() As MySqlClient.MySqlConnection
        Dim logs_conn As New MySqlClient.MySqlConnection
        logs_conn = New MySqlClient.MySqlConnection
        logs_conn.ConnectionString = "Server=localhost;Database=ANALYTICS;Uid=afiz;Pwd=yelena03;allow user variables=true;"
        Return logs_conn
    End Function
    <WebMethod()> _
    Private Sub close_connexion(ByVal conn As MySqlClient.MySqlConnection)
        MySqlClient.MySqlConnection.ClearPool(conn)
        conn.Close()
        conn.Dispose()
    End Sub

    <WebMethod()> _
    Public Function Get_Path(ByVal type As String) As String
        Dim tmp_str As String = ""
        Dim connec As New MySqlClient.MySqlConnection
        connec = _connexion()
        connec.Open()
        Dim dr As MySqlClient.MySqlDataReader
        Dim sql_query As New MySqlClient.MySqlCommand
        sql_query.Connection = connec
        sql_query.CommandText = "select path_pa from an_path where path_type='" & type & "';"
        dr = sql_query.ExecuteReader()

        While dr.Read()
            If Not dr.IsDBNull(0) Then
                tmp_str = dr("path_pa")
            End If
        End While
        dr.Close()
        close_connexion(connec)
        Return tmp_str
    End Function
    <WebMethod()> _
    Public Function Set_Path(ByVal type As String, ByVal path As String) As Boolean
        Try
            Dim connec As New MySqlClient.MySqlConnection
            connec = _connexion()
            connec.Open()
            Dim sql_query As MySqlClient.MySqlCommand = New MySqlClient.MySqlCommand()
            sql_query.Connection = connec
            sql_query.CommandText = "update an_path set path_pa='" & path.Replace("\", "\\").Replace("'", "\'") & "' where path_type='" & type & "';"
            sql_query.ExecuteNonQuery()
            close_connexion(connec)
            Return True
        Catch ex As Exception
        End Try
    End Function
    <WebMethod()> _
    Public Function Get_Function_Mandatory(ByVal type As String) As List(Of String)
        Dim tmp_str As New List(Of String)

        Dim connec As New MySqlClient.MySqlConnection
        connec = _connexion()
        connec.Open()
        Dim dr As MySqlClient.MySqlDataReader
        Dim sql_query As New MySqlClient.MySqlCommand
        sql_query.Connection = connec
        sql_query.CommandText = "SELECT fct_name FROM an_function WHERE fct_type = '" & type & "';"
        dr = sql_query.ExecuteReader()

        While dr.Read()
            If Not dr.IsDBNull(0) Then
                tmp_str.Add(dr("fct_name"))
            End If
        End While
        dr.Close()
        close_connexion(connec)
        Return tmp_str
    End Function
    <WebMethod()> _
    Public Function Get_Countries() As List(Of String)
        Dim tmp_str As New List(Of String)

        Dim connec As New MySqlClient.MySqlConnection
        connec = _connexion()
        connec.Open()
        Dim dr As MySqlClient.MySqlDataReader
        Dim sql_query As New MySqlClient.MySqlCommand
        sql_query.Connection = connec
        sql_query.CommandText = "SELECT distinct tr_pays FROM an_traitement;"
        dr = sql_query.ExecuteReader()

        While dr.Read()
            If Not dr.IsDBNull(0) Then
                tmp_str.Add(dr("tr_pays"))
            End If
        End While
        dr.Close()
        close_connexion(connec)
        Return tmp_str
    End Function
    <WebMethod()> _
    Public Function Get_Types() As List(Of String)
        Dim tmp_str As New List(Of String)

        Dim connec As New MySqlClient.MySqlConnection
        connec = _connexion()
        connec.Open()
        Dim dr As MySqlClient.MySqlDataReader
        Dim sql_query As New MySqlClient.MySqlCommand
        sql_query.Connection = connec
        sql_query.CommandText = "SELECT distinct tr_type FROM an_traitement;"
        dr = sql_query.ExecuteReader()

        While dr.Read()
            If Not dr.IsDBNull(0) Then
                tmp_str.Add(dr("tr_type"))
            End If
        End While
        dr.Close()
        close_connexion(connec)
        Return tmp_str
    End Function
    <WebMethod()> _
    Public Function Get_Pucs() As List(Of String)
        Dim tmp_str As New List(Of String)

        Dim connec As New MySqlClient.MySqlConnection
        connec = _connexion()
        connec.Open()
        Dim dr As MySqlClient.MySqlDataReader
        Dim sql_query As New MySqlClient.MySqlCommand
        sql_query.Connection = connec
        sql_query.CommandText = "SELECT distinct tr_poste FROM an_traitement;"
        dr = sql_query.ExecuteReader()

        While dr.Read()
            If Not dr.IsDBNull(0) Then
                tmp_str.Add(dr("tr_poste"))
            End If
        End While
        dr.Close()
        close_connexion(connec)
        Return tmp_str
    End Function
    <WebMethod()> _
    Public Function Requete(ByVal condition As String) As DataSet
        Dim sql_query As MySqlClient.MySqlCommand = New MySqlClient.MySqlCommand()
        Dim monds As New DataSet
        Dim connec As New MySqlClient.MySqlConnection
        connec = _connexion()
        connec.Open()
        sql_query.Connection = connec
        sql_query.CommandText = "SELECT tr_poste as Puc, DATE_FORMAT(tr_date, '%d/%m/%Y') as Date, tr_type as Type, tr_pays as Pays, tr_data as Data, tr_datamod as Datamod, tr_timeprocess as TimeProcess FROM an_traitement "
        If condition.Length = 0 Then
            sql_query.CommandText += ";"
        Else
            sql_query.CommandText += "WHERE " & condition & ";"
        End If
        Dim da As New MySqlClient.MySqlDataAdapter
        da.SelectCommand = sql_query
        da.Fill(monds, "tab_result")
        da.Dispose()
        close_connexion(connec)
        Return monds
    End Function
    <WebMethod()> _
    Public Function Count_by_puc(ByVal condition As String) As DataSet
        Dim sql_query As MySqlClient.MySqlCommand = New MySqlClient.MySqlCommand()
        Dim monds As New DataSet
        Dim connec As New MySqlClient.MySqlConnection
        connec = _connexion()
        connec.Open()
        sql_query.Connection = connec
        sql_query.CommandText = "SELECT count(tr_poste) as nombre, tr_poste as Puc FROM an_traitement "
        If condition.Length <> 0 Then
            sql_query.CommandText += "WHERE " & condition & " "
        End If
        sql_query.CommandText += "GROUP BY tr_poste;"
        Dim da As New MySqlClient.MySqlDataAdapter
        da.SelectCommand = sql_query
        da.Fill(monds, "tab_result")
        da.Dispose()
        close_connexion(connec)
        Return monds
    End Function
    <WebMethod()> _
    Public Function Count_by_month(ByVal condition As String) As DataSet
        Dim sql_query As MySqlClient.MySqlCommand = New MySqlClient.MySqlCommand()
        Dim monds As New DataSet
        Dim connec As New MySqlClient.MySqlConnection
        connec = _connexion()
        connec.Open()
        sql_query.Connection = connec
        sql_query.CommandText = "SELECT count(month (tr_date)) as nombre, DATE_FORMAT(tr_date, '%m/%Y') as Mois, tr_poste as Puc FROM an_traitement "
        If condition.Length <> 0 Then
            sql_query.CommandText += "WHERE " & condition & " "
        End If
        sql_query.CommandText += "GROUP BY month (tr_date) desc, tr_poste;"
        Dim da As New MySqlClient.MySqlDataAdapter
        da.SelectCommand = sql_query
        da.Fill(monds, "tab_result")
        da.Dispose()
        close_connexion(connec)
        Return monds
    End Function
    <WebMethod()> _
    Public Function Count_by_years(ByVal condition As String) As DataSet
        Dim sql_query As MySqlClient.MySqlCommand = New MySqlClient.MySqlCommand()
        Dim monds As New DataSet
        Dim connec As New MySqlClient.MySqlConnection
        connec = _connexion()
        connec.Open()
        sql_query.Connection = connec
        sql_query.CommandText = "SELECT count(year (tr_date)) as nombre, year (tr_date) as Année, tr_poste as Puc FROM an_traitement "
        If condition.Length <> 0 Then
            sql_query.CommandText += "WHERE " & condition & " "
        End If
        sql_query.CommandText += "GROUP BY year (tr_date), tr_poste;"
        Dim da As New MySqlClient.MySqlDataAdapter
        da.SelectCommand = sql_query
        da.Fill(monds, "tab_result")
        da.Dispose()
        close_connexion(connec)
        Return monds
    End Function
    <WebMethod()> _
    Public Function Count_by_types(ByVal condition As String) As DataSet
        Dim sql_query As MySqlClient.MySqlCommand = New MySqlClient.MySqlCommand()
        Dim monds As New DataSet
        Dim connec As New MySqlClient.MySqlConnection
        connec = _connexion()
        connec.Open()
        sql_query.Connection = connec
        sql_query.CommandText = "SELECT count(tr_type) as nombre, tr_type as Type, tr_poste as Puc FROM an_traitement "
        If condition.Length <> 0 Then
            sql_query.CommandText += "WHERE " & condition & " "
        End If
        sql_query.CommandText += "GROUP BY tr_type, tr_poste;"
        Dim da As New MySqlClient.MySqlDataAdapter
        da.SelectCommand = sql_query
        da.Fill(monds, "tab_result")
        da.Dispose()
        close_connexion(connec)
        Return monds
    End Function
    <WebMethod()> _
    Public Function Count_by_countries(ByVal condition As String) As DataSet
        Dim sql_query As MySqlClient.MySqlCommand = New MySqlClient.MySqlCommand()
        Dim monds As New DataSet
        Dim connec As New MySqlClient.MySqlConnection
        connec = _connexion()
        connec.Open()
        sql_query.Connection = connec
        sql_query.CommandText = "SELECT count(tr_pays) as nombre, tr_pays as Pays, tr_poste as Puc FROM an_traitement "
        If condition.Length <> 0 Then
            sql_query.CommandText += "WHERE " & condition & " "
        End If
        sql_query.CommandText += "GROUP BY tr_pays, tr_poste;"
        Dim da As New MySqlClient.MySqlDataAdapter
        da.SelectCommand = sql_query
        da.Fill(monds, "tab_result")
        da.Dispose()
        close_connexion(connec)
        Return monds
    End Function
    <WebMethod()> _
    Public Function Insert(ByVal puc As String, ByVal type As String, ByVal pays As String, ByVal data As String, ByVal datamod As String, ByVal timeprocess As Integer) As Boolean
        Try
            Dim connec As New MySqlClient.MySqlConnection
            connec = _connexion()
            connec.Open()
            Dim sql_query As MySqlClient.MySqlCommand = New MySqlClient.MySqlCommand()
            sql_query.Connection = connec
            sql_query.CommandText = "INSERT INTO an_traitement (tr_poste, tr_date, tr_type, tr_pays, tr_data, tr_datamod, tr_timeprocess) "
            sql_query.CommandText += "VALUES ('" & puc & "', '" & DateTime.Now.ToString("yyyy-MM-dd") & "', '" & type & "', '" & pays & "', '" & data & "', '" & datamod & "', '" & timeprocess & "');"
            sql_query.ExecuteNonQuery()
            close_connexion(connec)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
End Class