﻿Imports System
Imports System.Data
Imports System.Windows.Forms

Public Class Form1
    Inherits Form
    Protected DataSet1 As DataSet
    
' <Snippet1>
Private Sub TryRemove(dataSet As DataSet)
    Try
        Dim customersTable As DataTable = dataSet.Tables("Customers")
        Dim constraint As Constraint = customersTable.Constraints(0)
        Console.WriteLine($"Can remove? {customersTable.Constraints.CanRemove(constraint).ToString()}")

    Catch ex As Exception
        ' Process exception and return.
        Console.WriteLine($"Exception of type {ex.GetType().ToString()} occurred.")
    End Try
End Sub
' </Snippet1>
End Class
