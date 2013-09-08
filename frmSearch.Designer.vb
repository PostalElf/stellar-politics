<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSearch
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbSupply = New System.Windows.Forms.ComboBox()
        Me.cmbDemand = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.cmbStar = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbCities = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmbPrefix = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cmbSuffix = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cmbGovernment = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cmbAgents = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.colName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colCities = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colPrefix = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colSuffix = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colSupply = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colDemand = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colAgents = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(291, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(42, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Supply:"
        '
        'cmbSupply
        '
        Me.cmbSupply.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSupply.FormattingEnabled = True
        Me.cmbSupply.Location = New System.Drawing.Point(339, 16)
        Me.cmbSupply.Name = "cmbSupply"
        Me.cmbSupply.Size = New System.Drawing.Size(132, 21)
        Me.cmbSupply.TabIndex = 1
        '
        'cmbDemand
        '
        Me.cmbDemand.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDemand.FormattingEnabled = True
        Me.cmbDemand.Location = New System.Drawing.Point(339, 45)
        Me.cmbDemand.Name = "cmbDemand"
        Me.cmbDemand.Size = New System.Drawing.Size(132, 21)
        Me.cmbDemand.TabIndex = 5
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(283, 48)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(50, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Demand:"
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AllowUserToResizeRows = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colName, Me.colCities, Me.colPrefix, Me.colSuffix, Me.colSupply, Me.colDemand, Me.colAgents})
        Me.DataGridView1.Location = New System.Drawing.Point(12, 145)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.RowHeadersVisible = False
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView1.Size = New System.Drawing.Size(480, 150)
        Me.DataGridView1.TabIndex = 6
        '
        'cmbStar
        '
        Me.cmbStar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbStar.FormattingEnabled = True
        Me.cmbStar.Location = New System.Drawing.Point(67, 16)
        Me.cmbStar.Name = "cmbStar"
        Me.cmbStar.Size = New System.Drawing.Size(132, 21)
        Me.cmbStar.TabIndex = 8
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(32, 19)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(29, 13)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Star:"
        '
        'cmbCities
        '
        Me.cmbCities.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCities.FormattingEnabled = True
        Me.cmbCities.Location = New System.Drawing.Point(67, 45)
        Me.cmbCities.Name = "cmbCities"
        Me.cmbCities.Size = New System.Drawing.Size(132, 21)
        Me.cmbCities.TabIndex = 10
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(26, 48)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(35, 13)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "Cities:"
        '
        'cmbPrefix
        '
        Me.cmbPrefix.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPrefix.FormattingEnabled = True
        Me.cmbPrefix.Items.AddRange(New Object() {"", "Mining", "Industrial", "Research", "Prison", "Agrarian", "Cultural", "Tourist", "Commercial"})
        Me.cmbPrefix.Location = New System.Drawing.Point(67, 72)
        Me.cmbPrefix.Name = "cmbPrefix"
        Me.cmbPrefix.Size = New System.Drawing.Size(132, 21)
        Me.cmbPrefix.TabIndex = 12
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(25, 75)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(36, 13)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "Prefix:"
        '
        'cmbSuffix
        '
        Me.cmbSuffix.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSuffix.FormattingEnabled = True
        Me.cmbSuffix.Location = New System.Drawing.Point(67, 99)
        Me.cmbSuffix.Name = "cmbSuffix"
        Me.cmbSuffix.Size = New System.Drawing.Size(132, 21)
        Me.cmbSuffix.TabIndex = 14
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(25, 102)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(36, 13)
        Me.Label6.TabIndex = 13
        Me.Label6.Text = "Suffix:"
        '
        'cmbGovernment
        '
        Me.cmbGovernment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbGovernment.FormattingEnabled = True
        Me.cmbGovernment.Items.AddRange(New Object() {"", "Autocracy", "Oligarchy", "Democracy", "Anarchy"})
        Me.cmbGovernment.Location = New System.Drawing.Point(339, 72)
        Me.cmbGovernment.Name = "cmbGovernment"
        Me.cmbGovernment.Size = New System.Drawing.Size(132, 21)
        Me.cmbGovernment.TabIndex = 16
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(265, 75)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(68, 13)
        Me.Label7.TabIndex = 15
        Me.Label7.Text = "Government:"
        '
        'cmbAgents
        '
        Me.cmbAgents.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAgents.FormattingEnabled = True
        Me.cmbAgents.Items.AddRange(New Object() {"", "None", "At least 1", "More than 1"})
        Me.cmbAgents.Location = New System.Drawing.Point(339, 99)
        Me.cmbAgents.Name = "cmbAgents"
        Me.cmbAgents.Size = New System.Drawing.Size(132, 21)
        Me.cmbAgents.TabIndex = 18
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(290, 102)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(43, 13)
        Me.Label8.TabIndex = 17
        Me.Label8.Text = "Agents:"
        '
        'colName
        '
        Me.colName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.colName.Frozen = True
        Me.colName.HeaderText = "Name"
        Me.colName.MinimumWidth = 100
        Me.colName.Name = "colName"
        Me.colName.ReadOnly = True
        Me.colName.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'colCities
        '
        Me.colCities.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.colCities.HeaderText = "Cities"
        Me.colCities.Name = "colCities"
        Me.colCities.ReadOnly = True
        Me.colCities.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colCities.Width = 45
        '
        'colPrefix
        '
        Me.colPrefix.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.colPrefix.HeaderText = "Pre."
        Me.colPrefix.Name = "colPrefix"
        Me.colPrefix.ReadOnly = True
        Me.colPrefix.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colPrefix.Width = 40
        '
        'colSuffix
        '
        Me.colSuffix.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.colSuffix.HeaderText = "Suf."
        Me.colSuffix.Name = "colSuffix"
        Me.colSuffix.ReadOnly = True
        Me.colSuffix.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colSuffix.Width = 40
        '
        'colSupply
        '
        Me.colSupply.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.colSupply.HeaderText = "Supply"
        Me.colSupply.MinimumWidth = 100
        Me.colSupply.Name = "colSupply"
        Me.colSupply.ReadOnly = True
        Me.colSupply.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        '
        'colDemand
        '
        Me.colDemand.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.colDemand.HeaderText = "Demand"
        Me.colDemand.MinimumWidth = 100
        Me.colDemand.Name = "colDemand"
        Me.colDemand.ReadOnly = True
        '
        'colAgents
        '
        Me.colAgents.HeaderText = "Agents"
        Me.colAgents.Name = "colAgents"
        Me.colAgents.ReadOnly = True
        Me.colAgents.Width = 50
        '
        'frmSearch
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(507, 307)
        Me.Controls.Add(Me.cmbAgents)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.cmbGovernment)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.cmbSuffix)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.cmbPrefix)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.cmbCities)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.cmbStar)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.cmbDemand)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cmbSupply)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmSearch"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Search"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbSupply As System.Windows.Forms.ComboBox
    Friend WithEvents cmbDemand As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents cmbStar As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbCities As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmbPrefix As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmbSuffix As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cmbGovernment As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cmbAgents As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents colName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colCities As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colPrefix As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colSuffix As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colSupply As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colDemand As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colAgents As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
